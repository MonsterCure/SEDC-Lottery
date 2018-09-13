using Lottery.Service;
using Lottery.View.Model;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Lottery.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LotteryController : ApiController
    {
        private readonly ILotteryManager _lotteryManager;

        public LotteryController(ILotteryManager lotteryManager)
        {
            _lotteryManager = lotteryManager;
        }

        [HttpPost]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public AwardModel SubmitCode([FromBody]UserCodeModel userCodeModel)
        {
            return _lotteryManager.CheckCode(userCodeModel);
        }

        [HttpGet]
        //[Route("lottery/getAllWinners")]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public List<UserCodeAwardModel> GetAllWinners()
        {
            return _lotteryManager.GetAllWinners();
        }
    }
}
