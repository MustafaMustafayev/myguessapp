using System;
using System.Collections.Generic;

namespace GuessAppAPI
{
    public partial class Token
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string TokenValue { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual User User { get; set; }
    }
}
