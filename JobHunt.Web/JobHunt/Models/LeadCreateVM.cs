using System;

namespace JobHunt.Models
{
    public class LeadCreateVM
    {
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public int Id { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime Deadline { get; set; }
    }
}