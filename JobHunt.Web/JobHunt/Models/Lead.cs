using System;
using System.IO;

namespace JobHunt.Models
{
    public enum Status
    {
        Application = 1,
        Interview = 2,
        Negotiation = 3
    }

    public class Lead
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime Deadline { get; set; }
        public string Resume { get; set; }
        public string CoverLetter { get; set; }
        public Status Status { get; set; }

        public virtual Company Company { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}