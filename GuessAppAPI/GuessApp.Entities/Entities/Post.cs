using System;
using System.Collections.Generic;

namespace GuessAppAPI
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string PostContent { get; set; }
        public int UserId { get; set; }
        public int Condition { get; set; }
        public string PostSlug { get; set; }
        public string PostImagePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual User User { get; set; }
    }
}
