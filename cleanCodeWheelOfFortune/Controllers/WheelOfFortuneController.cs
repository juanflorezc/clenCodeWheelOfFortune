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
        public Task<List<int>> Get()
        {            
            return wheelServices.GetResponses();
        }
    }
}
