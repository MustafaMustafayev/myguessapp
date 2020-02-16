using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessAppAPI
{
    public class PostModel
    {
        public IFormFile file { get; set; }
        public string UserName { get; set; }
    }
}
