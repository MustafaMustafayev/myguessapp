using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GuessApp.DTO.AuthViewModels
{
    public class LoginDto
    {
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

    }
}
