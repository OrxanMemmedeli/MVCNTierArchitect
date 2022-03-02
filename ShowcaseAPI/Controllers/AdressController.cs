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
    public class AdressController : ApiController
    {
        private ShowcaseContext db = new ShowcaseContext();

        // GET: api/Adress
        public IQueryable<Adress> GetAdresses()
        {
            return db.Adresses;
        }

        // GET: api/Adress/5
        [ResponseType(typeof(Adress))]
        public async Task<IHttpActionResult> GetAdress(int id)
        {
            Adress adress = await db.Adresses.FindAsync(id);
            if (adress == null)
            {
                return NotFound();
            }

            return Ok(adress);
        }

        // PUT: api/Adress/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAdress(int id, Adress adress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adress.ID)
            {
                return BadRequest();
            }

            db.Entry(adress).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdressExists(id))
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

        // POST: api/Adress
        [ResponseType(typeof(Adress))]
        public async Task<IHttpActionResult> PostAdress(Adress adress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Adresses.Add(adress);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = adress.ID }, adress);
        }

        // DELETE: api/Adress/5
        [ResponseType(typeof(Adress))]
        public async Task<IHttpActionResult> DeleteAdress(int id)
        {
            Adress adress = await db.Adresses.FindAsync(id);
            if (adress == null)
            {
                return NotFound();
            }

            db.Adresses.Remove(adress);
            await db.SaveChangesAsync();

            return Ok(adress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdressExists(int id)
        {
            return db.Adresses.Count(e => e.ID == id) > 0;
        }
    }
}