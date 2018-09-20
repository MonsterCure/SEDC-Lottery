using Lottery.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lottery.Service
{
    public interface ILotteryManager
    {
        void GiveAward(RaffledType type);
    }
}
