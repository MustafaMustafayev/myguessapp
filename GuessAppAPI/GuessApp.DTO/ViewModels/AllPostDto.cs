using System;
using System.Collections.Generic;
using System.Text;

namespace GuessApp.DTO.ViewModels
{
    public class AllPostDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PostId { get; set; }
        public string PostContent { get; set; }
        public string PostCreatedDate { get; set; }
        public int PostCondition { get; set; }
        public int UserConditionId { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public int UnknownCount { get; set; }
        public string UserSlug { get; set; }
        public string PostImagePath { get; set; }
    }
}
