using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GuessApp.DTO.ViewModels
{
    public class ProfileStatisticDto
    {
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public int UnknownCount { get; set; }
        public string UserName  { get; set; }
        public string UserPhone { get; set; }
        public int UserId { get; set; }

    }
}
