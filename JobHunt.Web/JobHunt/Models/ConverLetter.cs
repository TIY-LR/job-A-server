using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHunt.Models
{
    public class CoverLetter
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public Lead Lead { get; set; }
    }
}