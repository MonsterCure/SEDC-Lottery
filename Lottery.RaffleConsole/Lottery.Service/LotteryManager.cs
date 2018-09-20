using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottery.Data;
using Lottery.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Service
{
    public class LotteryManager : ILotteryManager
    {
        private readonly IRepository<Award> _awardRepository;
        private readonly IRepository<UserCodeAward> _userCodeAwardRepository;
        private readonly IRepository<UserCode> _userCodeRepository;

        public LotteryManager(IRepository<Award> awardRepository, IRepository<UserCodeAward> userCodeAwardRepository, IRepository<UserCode> userCodeRepository)
        {
            _awardRepository = awardRepository;
            _userCodeAwardRepository = userCodeAwardRepository;
            _userCodeRepository = userCodeRepository;
        }

        public void GiveAward(RaffledType type)
        {
            //TODO: get all not winning users per type of submission (daily, immediately, finally)
            //these are all users, suitable for a final draw
            var users = _userCodeRepository.GetAll().Include(x => x.Code).Where(x => !x.Code.IsWinning);

            if(type == RaffledType.PerDay)
            {
                //all users from one day, suitable for a daily draw
                users = users.Where(x => x.SentAt.Date == DateTime.Now.Date);
            }

            var usersList = users.ToList();

            //TODO: get random user from list above
            var rnd = new Random();
            var randomAwardIndex = rnd.Next(0, usersList.Count - 1);
            var winningUser = usersList[randomAwardIndex];

            //TODO: get random award per type
            //TODO: match user with award
            GetRandomAward(type);
        }

        private Award GetRandomAward(RaffledType type)
        {
            var awards = _awardRepository.GetAll().Where(x => x.RaffledType == (byte)type).ToList();
            var awardedAwards = _userCodeAwardRepository
                .GetAll()
                .Where(x => x.Award.RaffledType == (byte)type)
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
