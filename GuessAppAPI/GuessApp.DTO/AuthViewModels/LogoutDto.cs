using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GuessApp.DTO.AuthViewModels
{
    public class LogoutDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
