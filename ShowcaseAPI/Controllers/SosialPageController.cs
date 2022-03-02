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
    public class SosialPageController : ApiController
    {
        private ShowcaseContext db = new ShowcaseContext();

        // GET: api/SosialPage
        public IQueryable<SosialPage> GetSosialPages()
        {
            return db.SosialPages;
        }

        // GET: api/SosialPage/5
        [ResponseType(typeof(SosialPage))]
        public async Task<IHttpActionResult> GetSosialPage(int id)
        {
            SosialPage sosialPage = await db.SosialPages.FindAsync(id);
            if (sosialPage == null)
            {
                return NotFound();
            }

            return Ok(sosialPage);
        }

        // PUT: api/SosialPage/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSosialPage(int id, SosialPage sosialPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sosialPage.ID)
            {
                return BadRequest();
            }

            db.Entry(sosialPage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SosialPageExists(id))
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

        // POST: api/SosialPage
        [ResponseType(typeof(SosialPage))]
        public async Task<IHttpActionResult> PostSosialPage(SosialPage sosialPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SosialPages.Add(sosialPage);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sosialPage.ID }, sosialPage);
        }

        // DELETE: api/SosialPage/5
        [ResponseType(typeof(SosialPage))]
        public async Task<IHttpActionResult> DeleteSosialPage(int id)
        {
            SosialPage sosialPage = await db.SosialPages.FindAsync(id);
            if (sosialPage == null)
            {
                return NotFound();
            }

            db.SosialPages.Remove(sosialPage);
            await db.SaveChangesAsync();

            return Ok(sosialPage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SosialPageExists(int id)
        {
            return db.SosialPages.Count(e => e.ID == id) > 0;
        }
    }
}