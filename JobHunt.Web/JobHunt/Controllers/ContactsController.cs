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
    public class ContactsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Contacts
        public object GetContacts()
        {
            var query = db.Contacts.Select(t => new
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                TwitterHandle = t.TwitterHandle,
                Phone = t.Phone,
                Title = t.Title,
                Notes = t.Notes
            });
            return new { Contacts = query.ToList() };
        }

        // GET: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult GetContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContact(int id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            db.Entry(contact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        public IHttpActionResult PostContact(ContactHelper contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newContact = new Contact() { FirstName = contact.contact.FirstName, LastName = contact.contact.LastName, Title = contact.contact.Title, Email = contact.contact.Email, Phone = contact.contact.Phone, TwitterHandle = contact.contact.TwitterHandle, Notes = contact.contact.Notes };

            var existingCompany = db.Companies.Find(contact.contact.Company);
            if (existingCompany != null)
            {
                newContact.Company = existingCompany;
            }
            else
            {
                return BadRequest("You must select a company.");
            }

            var existingLead = db.Leads.Find(contact.contact.Lead);
            if (existingLead != null && newContact.Company.Leads.Any(x => x.Id == existingLead.Id))
            {
                newContact.Company.Leads.Add(existingLead);
            }

            db.Contacts.Add(newContact);
            db.SaveChanges();

            contact.contact.Id = newContact.Id;

            return Ok(new { contact = contact.contact });
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contact);
            db.SaveChanges();

            return Ok(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactExists(int id)
        {
            return db.Contacts.Count(e => e.Id == id) > 0;
        }
    }
}