using System.Collections.Generic;

namespace JobHunt.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TwitterHandle { get; set; }
        public int Phone { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        //public byte[] emails { get; set; }

        public virtual List<Lead> Leads { get; set; }
        public virtual Company Company { get; set; }
    }
    public class ContactHelper
    {
        public Contact contact { get; set; }
    }
}