using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GuessApp.DTO.ViewModels
{
    public class UpdatePasswordDto
    {
        [Required]
        public int UserId { get; set; }


        [Required]
        public string UpdatedPassword { get; set; }

    }
}
