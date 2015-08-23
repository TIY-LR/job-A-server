using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHunt.Models
{
    public class ContactCreateVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TwitterHandle { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }

        public int Company { get; set; }
        public int Lead { get; set; }
    }

    public class ContactHelper
    {
        public ContactCreateVM contact { get; set; }
    }
}