using System;
using System.Collections.Generic;
using System.Text;

namespace GuessApp.DTO.ViewModels
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserMail { get; set; }
        public string UserPhone { get; set; }
        public string UserSlug { get; set; }
        public string StatusCode { get; set; }
        public string responseText { get; set; }
        public string token { get; set; }
    }
}
