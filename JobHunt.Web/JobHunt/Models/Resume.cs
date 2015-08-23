using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHunt.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public virtual List<ResumeItem> ResumeItems { get; set; }
        public virtual Lead Lead { get; set; }

    }

    public class ResumeItem
    {
        public int Id { get; set; }
        public virtual Resume Resume { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ResumeItemType Type { get; set; }
    }

    public enum ResumeItemType
    {
        Activity,
        Experience,
        Education,
    }
}