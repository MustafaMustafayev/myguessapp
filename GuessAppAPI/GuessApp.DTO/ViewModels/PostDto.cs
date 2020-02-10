using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GuessApp.DTO.ViewModels
{
    public class PostDto
    {
        public int PostId { get; set; }

      /*  [Required]
        [DataType(DataType.Text)] */
        [MaxLength(2000)]
        public string PostContent { get; set; }

        [Required]
        public string UserId { get; set; }

        public string PostImagePath { get; set; }
        public string Condition { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }


    }
}
