using System;
using System.Collections.Generic;

namespace CCWFortune.Services.Interfaces
{
    public partial class Wheel
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public List<Bet> Bets { get; set; }
    }
}