using System;
using System.Collections.Generic;

namespace GuessAppAPI
{
    public partial class UserCondition
    {
        public int UserConditionId { get; set; }
        public int UserId { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public int UnknownCount { get; set; }

        public virtual User User { get; set; }
    }
}
