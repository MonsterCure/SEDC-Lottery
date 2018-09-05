using System.ComponentModel;

namespace Lottery.Data.Model
{
    public enum RaffledType
    {
        [Description("Award that can be won once the code has been sent.")]
        Immediate = 0,

        [Description("Award that can be won at the end of the day raffle.")]
        PerDay = 1,

        [Description("Award that can be won at the final raffle.")]
        Final = 2
    }
}
