using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCWFortune.Services.Interfaces
{
    public interface IWheelServices
    {
        Task<List<int>> GetResponses();
    }
}
