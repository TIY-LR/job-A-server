using System;

namespace JobHunt.Models
{
    public class PostHelper
    {
        public LeadCreateVM lead { get; set; }
    }
    public class LeadCreateVM
    {
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public int Id { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime Deadline { get; set; }
    }
}