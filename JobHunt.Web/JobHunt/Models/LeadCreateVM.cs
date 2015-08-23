using System;

namespace JobHunt.Models
{
    public class PostHelper
    {
        public LeadCreateVM lead { get; set; }
    }
    public class LeadCreateVM
    {
        //Lead stuff
        public string JobTitle { get; set; }
        public int Id { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime Deadline { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public string Resume { get; set; }
        public string CoverLetter { get; set; }

        //Data integrity: Application, Interview, Negotiation
        public string Status { get; set; }

        //Company stuff
        public int Company { get; set; }
        public string CompanyName { get; set; }
        public string Url { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }

        //Contact stuff
        public int? Contact { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactPosition { get; set; }
        public string ContactTwitterHandle { get; set; }
    }
}