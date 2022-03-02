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
    public class ToolController : ApiController
    {
        private ShowcaseContext db = new ShowcaseContext();

        // GET: api/Tool
        public IQueryable<Tool> GetTools()
        {
            return db.Tools;
        }

        // GET: api/Tool/5
        [ResponseType(typeof(Tool))]
        public async Task<IHttpActionResult> GetTool(int id)
        {
            Tool tool = await db.Tools.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }

            return Ok(tool);
        }

        // PUT: api/Tool/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTool(int id, Tool tool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tool.ID)
            {
                return BadRequest();
            }

            db.Entry(tool).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolExists(id))
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

        // POST: api/Tool
        [ResponseType(typeof(Tool))]
        public async Task<IHttpActionResult> PostTool(Tool tool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tools.Add(tool);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tool.ID }, tool);
        }

        // DELETE: api/Tool/5
        [ResponseType(typeof(Tool))]
        public async Task<IHttpActionResult> DeleteTool(int id)
        {
            Tool tool = await db.Tools.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }

            db.Tools.Remove(tool);
            await db.SaveChangesAsync();

            return Ok(tool);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToolExists(int id)
        {
            return db.Tools.Count(e => e.ID == id) > 0;
        }
    }
}