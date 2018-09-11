using Lottery.Data;
using Lottery.Data.Model;
using Lottery.View.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lottery.Service
{
    public class LotteryManagerV0 : ILotteryManager
    {
        public AwardModel CheckCode(UserCodeModel userCodeModel)
        {
            var _codeRepository = new Repository<Code>(new DbContext("LotteryDb"));
            var code = _codeRepository.GetAll().FirstOrDefault(x => x.CodeValue == userCodeModel.Code.CodeValue);

            if(code == null)
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


            var _userCodeRepository = new Repository<UserCode>(new DbContext("LotteryDb"));
            _userCodeRepository.Insert(userCode);

            Award award = new Award()
            {
                RaffledType = (byte)RaffledType.Immediate,
                AwardName = "Another 0.5 bottle",
                AwardQuantity = 1
            };

            var userCodeAward = new UserCodeAward
            {
                Award = award,
                UserCode = userCode,
                WonAt = DateTime.Now
            };


            var _userCodeAwardRepository = new Repository<UserCodeAward>(new DbContext("LotteryDb"));
            _userCodeAwardRepository.Insert(userCodeAward);

            return new AwardModel()
            {
                AwardName = award.AwardName,
                AwardDescription = award.AwardDescription
            };
        }

        public List<UserCodeAwardModel> GetAllWinners()
        {
            return new List<UserCodeAwardModel>();
        }
    }
}
