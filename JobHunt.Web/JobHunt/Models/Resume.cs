using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHunt.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string EducationTitle1 { get; set; }
        public string EducationDetails1 { get; set; }
        public string EducationTitle2 { get; set; }
        public string EducationDetails2 { get; set; }
        public string EducationTitle3 { get; set; }
        public string EducationDetails3 { get; set; }
        public string EducationTitle4 { get; set; }
        public string EducationDetails4 { get; set; }
        public string ExperienceTitle1 { get; set; }
        public string ExperienceDetails1 { get; set; }
        public string ExperienceTitle2 { get; set; }
        public string ExperienceDetails2 { get; set; }
        public string ExperienceTitle3 { get; set; }
        public string ExperienceDetails3 { get; set; }
        public string ExperienceTitle4 { get; set; }
        public string ExperienceDetails4 { get; set; }
        public string ActivitiesTitle1 { get; set; }
        public string ActivitiesDetails1 { get; set; }
        public string ActivitiesTitle2 { get; set; }
        public string ActivitiesDetails2 { get; set; }
        public string ActivitiesTitle3 { get; set; }
        public string ActivitiesDetails3 { get; set; }
        public string ActivitiesTitle4 { get; set; }
        public string ActivitiesDetails4 { get; set; }
        public virtual Lead Lead { get; set; } 
    }
    
}