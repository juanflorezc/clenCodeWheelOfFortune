using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCWFortune.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cleanCodeWheelOfFortune.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WheelOfFortuneController : ControllerBase
    {
        
        private readonly ILogger<WheelOfFortuneController> _logger;
        public readonly IWheelServices wheelServices;

        public WheelOfFortuneController(ILogger<WheelOfFortuneController> logger, IWheelServices prmWheelServices)
        {
            _logger = logger;
            wheelServices = prmWheelServices;
        }

        [HttpGet]
        public Task<List<Wheel>> Get()
        {            
            return wheelServices.GetResponses();
        }

        [HttpPost]
        [Route("create")]
        public Task<Guid> Create()
        {
            return wheelServices.CreateWheelOFFortune();
        }

        [HttpPost]
        [Route("Open")]
        public Task<bool> Open(Guid prmId)
        {
            return wheelServices.OpenWheelOFFortune(prmId);
        }

        [HttpPost]
        [Route("Close")]
        public Task<List<Bet>> Close(Guid prmId)
        {
            return wheelServices.CloseWheelOFFortune(prmId);
        }

        [HttpPost]
        [Route("Bet")]
        public Task<bool> Bet(Bet prmBet)
        {            
            if(Request.Headers.ContainsKey("userID"))
            {
                prmBet.UserID = Request.Headers["userID"];                
            }
            return wheelServices.BetWheelOFFortune(prmBet);
        }
    }
}
