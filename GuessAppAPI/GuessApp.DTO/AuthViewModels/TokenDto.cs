using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GuessApp.DTO.AuthViewModels
{
    public class TokenDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string TokenValue { get; set; }
    }
}
