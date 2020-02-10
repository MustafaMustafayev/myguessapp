using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GuessApp.DTO.ViewModels
{
    public class UpdatePostDto
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string PostContent { get; set; }
    }
}
