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
    public class ResumesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Resumes
        public IQueryable<Resume> GetResumes()
        {
            return db.Resumes;
        }

        // GET: api/Resumes/5
        [ResponseType(typeof(Resume))]
        public IHttpActionResult GetResume(int id)
        {
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return NotFound();
            }

            return Ok(resume);
        }

        // PUT: api/Resumes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResume(int id, Resume resume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resume.Id)
            {
                return BadRequest();
            }

            db.Entry(resume).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResumeExists(id))
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

        // POST: api/Resumes
        [ResponseType(typeof(Resume))]
        public IHttpActionResult PostResume(Resume resume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resumes.Add(resume);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ResumeExists(resume.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = resume.Id }, resume);
        }

        // DELETE: api/Resumes/5
        [ResponseType(typeof(Resume))]
        public IHttpActionResult DeleteResume(int id)
        {
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return NotFound();
            }

            db.Resumes.Remove(resume);
            db.SaveChanges();

            return Ok(resume);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResumeExists(int id)
        {
            return db.Resumes.Count(e => e.Id == id) > 0;
        }
    }
}