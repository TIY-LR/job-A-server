using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using JobHunt.Models;

namespace JobHunt.Controllers
{
    public class LeadsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Leads
        public object GetLeads()
        {
            var query = db.Leads.Select(l => new LeadCreateVM() { Id = l.Id, JobTitle = l.Title, CompanyName = l.Company.Name });
            return new { leads = query.ToList() };
        }

        // GET: api/Leads/5
        [ResponseType(typeof(Lead))]
        public IHttpActionResult GetLead(int id)
        {
            Lead lead = db.Leads.Find(id);
            if (lead == null)
            {
                return NotFound();
            }

            return Ok(lead);
        }

        // PUT: api/Leads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLead(int id, Lead lead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lead.Id)
            {
                return BadRequest();
            }

            db.Entry(lead).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Leads
        [ResponseType(typeof(Lead))]
        public IHttpActionResult PostLead(PostHelper lead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newlead = new Lead() { Title = lead.lead.JobTitle, Deadline = DateTime.Now.AddDays(7), DateAdded = DateTime.Now, Notes = lead.lead.Notes, Description = lead.lead.Description, Resume = lead.lead.Resume, CoverLetter = lead.lead.CoverLetter, Status = lead.lead.Status };
            newlead.Company = new Company() { Id = lead.lead.Company, Name = lead.lead.CompanyName, Url = lead.lead.Url, Address1 = lead.lead.Address1, Address2 = lead.lead.Address2, City = lead.lead.City, State = lead.lead.State, Zipcode = lead.lead.Zipcode };
            newlead.Contact = new Contact() { Id = lead.lead.Contact, FirstName = lead.lead.ContactFirstName, LastName = lead.lead.ContactFirstName, Email = lead.lead.ContactEmail, Phone = lead.lead.ContactPhone, Title = lead.lead.ContactPosition, TwitterHandle = lead.lead.ContactTwitterHandle };
            db.Leads.Add(newlead);

            db.SaveChanges();

            lead.lead.Id = newlead.Id;
            lead.lead.Deadline = newlead.Deadline;
            lead.lead.PostTime = newlead.DateAdded;
            return Ok(new { lead = lead.lead });
        }

        // DELETE: api/Leads/5
        [ResponseType(typeof(Lead))]
        public IHttpActionResult DeleteLead(int id)
        {
            Lead lead = db.Leads.Find(id);
            if (lead == null)
            {
                return NotFound();
            }

            db.Leads.Remove(lead);
            db.SaveChanges();

            return Ok(lead);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeadExists(int id)
        {
            return db.Leads.Count(e => e.Id == id) > 0;
        }
    }
}