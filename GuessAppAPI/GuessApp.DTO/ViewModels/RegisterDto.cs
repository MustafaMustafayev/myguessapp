using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GuessApp.DTO.ViewModels
{
    public class RegisterDto
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string UserMail { get; set; }
        [Required]
        public string UserPhone { get; set; }
        [Required]
        public string UserAim { get; set; }
        [Required]
        public string UserPassword { get; set; }

    }
}
