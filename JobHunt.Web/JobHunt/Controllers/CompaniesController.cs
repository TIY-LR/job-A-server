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
    public class CompaniesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Companies
        public object GetCompanies()
        {
            var query = db.Companies.Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                Address1 = c.Address1,
                Address2 = c.Address2,
                City = c.City,
                State = c.State,
                Zipcode = c.Zipcode,
                Url = c.Url,
                Contacts = c.Contacts.Select(contact=> contact.Id),
                Leads = c.Leads.Select(l => l.Id)
            });
            return new { companies = query.ToList() };
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(int id)
        {
            var company = db.Companies.Where(x=>x.Id ==id ).Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                Address1 = c.Address1,
                Address2 = c.Address2,
                City = c.City,
                State = c.State,
                Zipcode = c.Zipcode,
                Url = c.Url,
                Contacts = c.Contacts.Select(contact => contact.Id),
                Leads = c.Leads.Select(l => l.Id)
            });

            if (company == null)
            {
                return NotFound();
            }

            return Ok(new { companies = company });
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompany(int id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.Id)
            {
                return BadRequest();
            }

            db.Entry(company).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Companies
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(CompanyHelper company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Companies.Add(company.company);
            db.SaveChanges();

            return Ok(new { company = company.company });
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            db.Companies.Remove(company);
            db.SaveChanges();

            return Ok(company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyExists(int id)
        {
            return db.Companies.Count(e => e.Id == id) > 0;
        }
    }
}