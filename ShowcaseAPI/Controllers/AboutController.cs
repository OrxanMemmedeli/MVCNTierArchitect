using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ShowcaseAPI.Models.Context;
using ShowcaseAPI.Models.Entity;

namespace ShowcaseAPI.Controllers
{
    public class AboutController : ApiController
    {
        private ShowcaseContext db = new ShowcaseContext();

        // GET: api/About
        public IQueryable<About> GetAbouts()
        {
            return db.Abouts;
        }

        // GET: api/About/5
        [ResponseType(typeof(About))]
        public async Task<IHttpActionResult> GetAbout(int id)
        {
            About about = await db.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }

            return Ok(about);
        }

        // PUT: api/About/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAbout(int id, About about)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != about.ID)
            {
                return BadRequest();
            }

            db.Entry(about).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AboutExists(id))
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

        // POST: api/About
        [ResponseType(typeof(About))]
        public async Task<IHttpActionResult> PostAbout(About about)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Abouts.Add(about);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = about.ID }, about);
        }

        // DELETE: api/About/5
        [ResponseType(typeof(About))]
        public async Task<IHttpActionResult> DeleteAbout(int id)
        {
            About about = await db.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }

            db.Abouts.Remove(about);
            await db.SaveChangesAsync();

            return Ok(about);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AboutExists(int id)
        {
            return db.Abouts.Count(e => e.ID == id) > 0;
        }
    }
}