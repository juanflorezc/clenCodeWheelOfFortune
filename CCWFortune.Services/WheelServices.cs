using CCWFortune.Services.Interfaces;
using Jil;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CCWFortune.Services
{
    public class WheelServices:IWheelServices
    {
        private readonly RedisService _redisService;

        public WheelServices(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<bool> BetWheelOFFortune(Bet bet)
        {
            List<Wheel> wheels = this.getWheels();
            if(!this.validBet(bet))
            {
                return false;
            }
            if(!string.IsNullOrEmpty(bet.UserID))
            {
                bet.HasCredit = true;
            }
            bool exist = false;
            foreach (var wheel in wheels)
            {
                if (wheel.Id == bet.IdWheelOfFortune)
                {
                    List<Bet> bets = wheel.Bets;
                    if(bets==null)
                    {
                        bets = new List<Bet>();
                    }                    
                    bets.Add(bet);
                    wheel.Bets=bets;
                    exist = true;
                }
            }
            if(exist)
            {
                return _redisService.Set("Wheels", JSON.Serialize(wheels));
            }
            else
            {
                return false;
            }            
        }

        private bool validBet(Bet bet)
        {
            if (bet.Money > 100000)
            {
                return false;
            }
            if(bet.BetNumber!=null)
            {
                if(bet.BetNumber>36 || bet.BetNumber < 0)
                {
                    return false;
                }
            }
            if(bet.BetNumber==null&&bet.ColorRed==null)
            {
                return false;
            }            
            return true;
        }

        public async Task<List<Bet>> CloseWheelOFFortune(Guid id)
        {
            List<Wheel> wheels = this.getWheels();
            List<Bet> bets= new List<Bet>();
            
            foreach (var wheel in wheels)
            {
                if (wheel.Id == id)
                {                    
                    wheel.State = "Close";
                    bets = wheel.Bets;
                }
            }
            if(bets.Count>0)
            {
                int winnerNumber = this.getWinnerNumber();
                bool redWinner = winnerNumber % 2 == 0;
                foreach (var bet in bets)
                {
                    if (bet.BetNumber != null)
                    {
                        if (winnerNumber == bet.BetNumber)
                        {
                            bet.Winner = true;
                            bet.MoneyWon = bet.Money * 5;
                        }
                    }
                    if (bet.ColorRed != null)
                    {

                        if (bet.ColorRed == true && redWinner)
                        {
                            bet.Winner = true;
                            bet.MoneyWon = bet.Money * 1.8;
                        }
                        if (bet.ColorRed == false && !redWinner)
                        {
                            bet.Winner = true;
                            bet.MoneyWon = bet.Money * 1.8;
                        }
                    }
                }
            }            
            return bets;            
        }

        private int getWinnerNumber()
        {
            Random rnd = new Random();
            return rnd.Next(0, 37);
        }

        public async Task<Guid> CreateWheelOFFortune()
        {
            try
            {                                
                List<Wheel> wheels = this.getWheels();                
                var guid = Guid.NewGuid();
                var message = new Wheel
                {
                    Id = guid,
                    State = "Pending",
                };
                wheels.Add(message);
                var creado = _redisService.Set("Wheels", JSON.Serialize(wheels));
                return guid;
            }
            catch(Exception e)
            {
                return Guid.Empty;
            }                        
        }

        public async Task<List<Wheel>> GetResponses()
        {
            return this.getWheels();
        }

        public async Task<bool> OpenWheelOFFortune(Guid id)
        {
            List<Wheel> wheels = this.getWheels();
            bool exist = false;
            foreach(var wheel in wheels)
            {
                if(wheel.Id==id)
                {
                    exist = true;
                    wheel.State = "Open";
                }
            }
            if(exist)
            {
                return _redisService.Set("Wheels", JSON.Serialize(wheels));
            }
            else
            {
                return false;
            }
        }

        private List<Wheel> getWheels()
        {
            List<Wheel> wheels = new List<Wheel>();
            string dbWheel = _redisService.Get("Wheels");
            if (!string.IsNullOrEmpty(dbWheel))
            {
                wheels = JSON.Deserialize<List<Wheel>>(dbWheel);
            }
            return wheels;
        }
    }
}
