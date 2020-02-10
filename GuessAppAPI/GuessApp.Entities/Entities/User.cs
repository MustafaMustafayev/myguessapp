using System;
using System.Collections.Generic;

namespace GuessAppAPI
{
    public partial class User
    {
        public User()
        {
            Post = new HashSet<Post>();
            Token = new HashSet<Token>();
            UserCondition = new HashSet<UserCondition>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserMail { get; set; }
        public string UserPhone { get; set; }
        public string UserAim { get; set; }
        public int IsMailVerified { get; set; }
        public string UserSlug { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Token> Token { get; set; }
        public virtual ICollection<UserCondition> UserCondition { get; set; }
    }
}
