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

        public void GiveAwards(RaffledType type)
        {
            var numberOfAwards = GetAwardQuantityPerType(type);

            for (var i = 0; i < numberOfAwards; i++)
            {
                GiveAwards(type);
            }

            //var users = _userCodeRepository.GetAll().Include(x => x.Code).Where(x => !x.Code.IsWinning);

            //if (type == RaffledType.PerDay)
            //{
            //    //all users from one day, suitable for a daily draw
            //    users = users.Where(x => x.SentAt.Date == DateTime.Now.Date);
            //}

            //var usersList = users.ToList();

            //var numberOfAwards = GetAwardQuantityPerType(type);

            //for (var i = 0; i < numberOfAwards; i++)
            //{
            //    if (usersList.Count() >= numberOfAwards)
            //    {
            //        var rnd = new Random();
            //        var randomAwardIndex = rnd.Next(0, usersList.Count - 1);
            //        var winningUser = usersList[randomAwardIndex];
            //        var award = GetRandomAward(type);

            //        var userCode = new UserCode
            //        {
            //            Code = winningUser.Code,
            //            FirstName = winningUser.FirstName,
            //            LastName = winningUser.LastName,
            //            EMail = winningUser.EMail,
            //            SentAt = winningUser.SentAt
            //        };

            //        _userCodeAwardRepository.Insert(new UserCodeAward()
            //        {
            //            Award = award,
            //            UserCode = userCode,
            //            WonAt = DateTime.Now
            //        });

            //        usersList.Remove(userCode);
            //    }
            //}

        }

        private int GetAwardQuantityPerType(RaffledType type)
        {
            var awardsQuantity = _awardRepository.GetAll().Where(x => x.RaffledType == (byte) type).Select(x => x.AwardQuantity).Sum();

            return awardsQuantity;
        }

        private void GiveAward(RaffledType type)
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

            //check that all in usersList have not already won an award, are not in the UserCodeAwards repository with an award
            var userCodeAwards = _userCodeAwardRepository.GetAll().ToList(); //get all winners
            usersList = usersList.Where(x => userCodeAwards.All(y => y.Id != x.Id)).ToList(); //filter usersList to check if they do not contain winners, remove any winners

            if (!usersList.Any()) return;

            //TODO: get random user from list above
            var rnd = new Random();
            var randomAwardIndex = rnd.Next(0, usersList.Count - 1);
            var winningUser = usersList[randomAwardIndex];

            //TODO: get random award per type
            var randomAward = GetRandomAward(type);

            //TODO: match user with award and insert into UserCodeAward repository/table
            _userCodeAwardRepository.Insert(new UserCodeAward()
            {
                Award = randomAward,
                UserCode = winningUser,
                WonAt = DateTime.Now
            });
        }

        private Award GetRandomAward(RaffledType type)
        {
            var awards = _awardRepository.GetAll().Where(x => x.RaffledType == (byte)type).ToList();
            var awardedAwards = _userCodeAwardRepository
                .GetAll()
                .Where(x => x.Award.RaffledType == (byte) type); //filter awarded awarda by type

            if (type == RaffledType.PerDay)
            {
                awardedAwards.Where(x => x.WonAt.Date == DateTime.Now.Date); //for the daily raffle, include only the awards that have been awarded that same day 
            }

            var awardedAwardsList = awardedAwards
                .Select(x => x.Award)
                .GroupBy(x => x.Id)
                .ToList();

            var availableAwards = new List<Award>();

            foreach (var award in awards)
            {
                var numberOfAwardedAwards = awardedAwardsList
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
