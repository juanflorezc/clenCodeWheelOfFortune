using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCWFortune.Services.Interfaces
{
    public interface IWheelServices
    {
        Task<List<Wheel>> GetResponses();
        Task<Guid> CreateWheelOFFortune();
        Task<bool> OpenWheelOFFortune(Guid id);
        Task<bool> BetWheelOFFortune(Bet bet);
        Task<List<Bet>> CloseWheelOFFortune(Guid id);
    }
}
