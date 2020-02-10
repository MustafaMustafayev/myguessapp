using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GuessApp.DTO.ViewModels
{
    public class PostConditionDto
    {
        [Required]
        public int UserId{ get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int Condition { get; set; }

    }
}
