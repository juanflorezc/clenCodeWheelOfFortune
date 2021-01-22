using System;

namespace CCWFortune.Services.Interfaces
{
    public partial class Bet
    {
        public Guid IdWheelOfFortune { get; set; }
        public int? BetNumber { get; set; }
        public bool? ColorRed { get; set; }
        public int Money { get; set; }
        //like a not mapped
        public string? UserID { get; set; }
        //like a not mapped
        public bool? HasCredit { get; set; }
        public bool? Winner { get; set; }
        public double? MoneyWon { get; set; }
    }
}