using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GuessApp.DTO.AuthViewModels
{
    public class ForgotPasswordDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserMail { get; set; }
    }
}
