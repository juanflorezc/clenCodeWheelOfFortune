using CCWFortune.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCWFortune.Services
{
    public class WheelServices:IWheelServices
    {
        public async Task<List<int>> GetResponses()
        {
            return Enumerable.Range(1, 5).ToList();
        }
    }
}
