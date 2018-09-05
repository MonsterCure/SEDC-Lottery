using System;

namespace Lottery.View.Model
{
    public class UserCodeAwardModel
    {
        public UserCodeAwardModel()
        {
            UserCode = new UserCodeModel();
            Award = new AwardModel();
        }

        public UserCodeModel UserCode { get; set; }

        public AwardModel Award { get; set; }

        public DateTime WonAt { get; set; }
    }
}
