using System;

namespace JobHunt.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime Deadline { get; set; }
        public byte[] Resume { get; set; }
        public byte[] CoverLetter { get; set; }
        public string Status { get; set; }

        public virtual Company Company { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}