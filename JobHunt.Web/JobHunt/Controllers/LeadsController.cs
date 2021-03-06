﻿using System;
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
            var query = db.Leads.Select(l => new LeadCreateVM()
            {
                Id = l.Id,
                JobTitle = l.Title,
                CompanyName = l.Company.Name,
                Status = l.Status.ToString(),
                Company = l.Company.Id,
                Contact = l.Contact.Id,
                Address1 = l.Company.Address1,
                Address2 = l.Company.Address2,
                City = l.Company.City,
                ContactEmail = l.Contact.Email,
                ContactFirstName = l.Contact.FirstName,
                ContactLastName = l.Contact.LastName,
                ContactPhone = l.Contact.Phone,
                ContactPosition = l.Contact.Title,
                ContactTwitterHandle = l.Contact.TwitterHandle,
                Deadline = l.Deadline,
                PostTime = l.DateAdded,
                Description = l.Description,
                Notes = l.Notes,
                State = l.Company.State,
                Url = l.Company.Url,
                Zipcode = l.Company.Zipcode
            });
            return new { leads = query.ToList() };
        }

        // GET: api/Leads/5
        [ResponseType(typeof(Lead))]
        public IHttpActionResult GetLead(int id)
        {
            var lead = db.Leads.Where(x=>x.Id == id).Select(l => new LeadCreateVM()
            {
                Id = l.Id,
                JobTitle = l.Title,
                CompanyName = l.Company.Name,
                Status = l.Status.ToString(),
                Company = l.Company.Id,
                Contact = l.Contact.Id,
                Address1 = l.Company.Address1,
                Address2 = l.Company.Address2,
                City = l.Company.City,
                ContactEmail = l.Contact.Email,
                ContactFirstName = l.Contact.FirstName,
                ContactLastName = l.Contact.LastName,
                ContactPhone = l.Contact.Phone,
                ContactPosition = l.Contact.Title,
                ContactTwitterHandle = l.Contact.TwitterHandle,
                Deadline = l.Deadline,
                PostTime = l.DateAdded,
                Description = l.Description,
                Notes = l.Notes,
                State = l.Company.State,
                Url = l.Company.Url,
                Zipcode = l.Company.Zipcode
            }).FirstOrDefault();

            if (lead == null)
            {
                return NotFound();
            }

            return Ok(new { lead = lead });
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
            var newlead = new Lead()
            {
                Title = lead.lead.JobTitle,
                Deadline = DateTime.Now.AddDays(7),
                DateAdded = DateTime.Now,
                Notes = lead.lead.Notes,
                Description = lead.lead.Description,
                //Resume = lead.lead.Resume,
                //CoverLetter = lead.lead.CoverLetter,
                Status = (Status)(Enum.Parse(typeof(Status), lead.lead.Status))
            };
            var existingCompany = db.Companies.Find(lead.lead.Company);
            if (existingCompany != null)
            {
                newlead.Company = existingCompany;
            }
            else
            {
                newlead.Company = new Company()
                {
                    Id = lead.lead.Company,
                    Name = lead.lead.CompanyName,
                    Url = lead.lead.Url,
                    Address1 = lead.lead.Address1,
                    Address2 = lead.lead.Address2,
                    City = lead.lead.City,
                    State = lead.lead.State,
                    Zipcode = lead.lead.Zipcode
                };
            }

            var existingContact = db.Contacts.Find(lead.lead.Contact);

            if (existingContact != null)
            {
                if (existingContact.Company != existingCompany)
                {
                    //BAD
                    return BadRequest("Trying to assoicate contact with a different company");
                }
                newlead.Contact = existingContact;
            }
            else
            {
            }


            db.Leads.Add(newlead);
            db.SaveChanges();

            lead.lead.CompanyName = newlead.Company.Name;
            lead.lead.Company = newlead.Company.Id;
            if (newlead.Contact != null)
            {
                lead.lead.Contact = newlead.Contact.Id;
                lead.lead.ContactFirstName = newlead.Contact.FirstName;
                lead.lead.ContactLastName = newlead.Contact.LastName;
            }
            else
            {
                lead.lead.Contact = 0;
                lead.lead.ContactFirstName = "";
                lead.lead.ContactLastName = "";
            }
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