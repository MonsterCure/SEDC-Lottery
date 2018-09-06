using Lottery.Data;
using Lottery.Data.Model;
using Lottery.Service.UoW;
using Lottery.View.Model;
using Lottery.Mapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lottery.Service
{
    public class LotteryManager : ILotteryManager
    {
        private readonly DbContext _dbContext;
        private readonly IRepository<Code> _codeRepository;
        private readonly IRepository<Award> _awardRepository;
        private readonly IRepository<UserCode> _userCodeRepository;
        private readonly IRepository<UserCodeAward> _userCodeAwardRepository;

        public LotteryManager(IRepository<Code> codeRepository,
            IRepository<Award> awardRepository,
            IRepository<UserCode> userCodeRepository,
            IRepository<UserCodeAward> userCodeAwardRepository,
            DbContext dbContext)
        {
            _dbContext = dbContext;
            _codeRepository = codeRepository;
            _awardRepository = awardRepository;
            _userCodeRepository = userCodeRepository;
            _userCodeAwardRepository = userCodeAwardRepository;
        }

        public AwardModel CheckCode(UserCodeModel userCodeModel)
        {
            using(var uow = new UnitOfWork(_dbContext))
            {
                var code = _codeRepository.GetAll().FirstOrDefault(x => x.CodeValue == userCodeModel.Code.CodeValue);

                if (code == null)
                    throw new ApplicationException("Invalid code.");
                if (code.IsUsed)
                    throw new ApplicationException("Code has already been used.");

                var userCode = new UserCode
                {
                    Code = code,
                    FirstName = userCodeModel.FirstName,
                    LastName = userCodeModel.LastName,
                    EMail = userCodeModel.EMail,
                    SentAt = DateTime.Now
                };

                _userCodeRepository.Insert(userCode);

                Award award = null;

                if (code.IsWinning)
                {
                    award = GetRandomAward(RaffledType.Immediate);

                    var userCodeAward = new UserCodeAward
                    {
                        Award = award,
                        UserCode = userCode,
                        WonAt = DateTime.Now
                    };

                    _userCodeAwardRepository.Insert(userCodeAward);
                }

                code.IsUsed = true;

                uow.Commit();

                return award?.Map<Award, AwardModel>();
            }
        }

        private Award GetRandomAward(RaffledType type)
        {
            var awards = _awardRepository.GetAll().Where(x => x.RaffledType == (byte)type).ToList();
            var awardedAwards = _userCodeAwardRepository
                .GetAll()
                .Where(x => x.Award.RaffledType == (byte) type)
                .Select(x => x.Award)
                .GroupBy(x => x.Id)
                .ToList();

            var availableAwards = new List<Award>();

            foreach (var award in awards)
            {
                var numberOfAwardedAwards = awardedAwards
                    .FirstOrDefault(x => x.Key == award.Id)?.Count() ?? 0;
                var awardsLeft = award.AwardQuantity - numberOfAwardedAwards;
                availableAwards.AddRange(Enumerable.Repeat(award, awardsLeft));
            }

            if (availableAwards.Count == 0)
                throw new ApplicationException("We are out of awards. Sorry!");

            var rnd = new Random();
            var randomAwardIndex = rnd.Next(0, availableAwards.Count);
            return availableAwards[randomAwardIndex];
        }
    }
}
