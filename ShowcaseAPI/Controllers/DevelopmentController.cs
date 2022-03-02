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
    public class DevelopmentController : ApiController
    {
        private ShowcaseContext db = new ShowcaseContext();

        // GET: api/Development
        public IQueryable<Development> GetDevelopments()
        {
            return db.Developments;
        }

        // GET: api/Development/5
        [ResponseType(typeof(Development))]
        public async Task<IHttpActionResult> GetDevelopment(int id)
        {
            Development development = await db.Developments.FindAsync(id);
            if (development == null)
            {
                return NotFound();
            }

            return Ok(development);
        }

        // PUT: api/Development/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDevelopment(int id, Development development)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != development.ID)
            {
                return BadRequest();
            }

            db.Entry(development).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DevelopmentExists(id))
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

        // POST: api/Development
        [ResponseType(typeof(Development))]
        public async Task<IHttpActionResult> PostDevelopment(Development development)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Developments.Add(development);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = development.ID }, development);
        }

        // DELETE: api/Development/5
        [ResponseType(typeof(Development))]
        public async Task<IHttpActionResult> DeleteDevelopment(int id)
        {
            Development development = await db.Developments.FindAsync(id);
            if (development == null)
            {
                return NotFound();
            }

            db.Developments.Remove(development);
            await db.SaveChangesAsync();

            return Ok(development);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DevelopmentExists(int id)
        {
            return db.Developments.Count(e => e.ID == id) > 0;
        }
    }
}