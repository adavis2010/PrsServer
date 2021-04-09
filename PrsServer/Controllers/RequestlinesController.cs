using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsServer.Data;
using PrsServer.Models;

namespace PrsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestlinesController : ControllerBase
    {
        private readonly PrsServerDbContext _context;

        public RequestlinesController(PrsServerDbContext context)
        {
            _context = context;
        }

        private async Task recalculatetotal(int requestId) {
            var request = await _context.Request.FindAsync(requestId);
            if (request == null) throw new Exception("Cannot find request");
            request.Total = (from r in _context.Requestlines
                             join p in _context.Product
                             on r.ProductId equals p.Id
                             where r.RequestId == requestId
                             select new { linetotal = r.Quantity * r.Product.Price }).Sum(lt => lt.linetotal);
            await _context.SaveChangesAsync();

        }
            private async Task refreshrequestline(Requestline requestline) {
            _context.Entry(requestline).State = EntityState.Detached;
            await _context.Requestline.FindAsync(requestline.Id);
        }

        // GET: api/Requestlines(Get All)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requestline>>> GetRequestline()
        {
            return await _context.Requestline.ToListAsync();
        }

        // GET: api/Requestlines/5 (PK)
        [HttpGet("{id}")]
        public async Task<ActionResult<Requestline>> GetRequestline(int id)
        {
            var requestline = await _context.Requestline.Include(x => x.Product).SingleOrDefaultAsync(x => x.Id == id);

            if (requestline == null)
            {
                return NotFound();
            }
            await refreshrequestline(requestline);
            await recalculatetotal(requestline.RequestId);
            return requestline;
        }

        // PUT: api/Requestlines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestline(int id, Requestline requestline)
        {
            if (id != requestline.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestlineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await refreshrequestline(requestline);
            await recalculatetotal(requestline.RequestId);

            return NoContent();
        }

        // POST: api/Requestlines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Requestline>> PostRequestline(Requestline requestline)
        {
            _context.Requestline.Add(requestline);
            await _context.SaveChangesAsync();

            await refreshrequestline(requestline);
            await recalculatetotal(requestline.RequestId);

            return CreatedAtAction("GetRequestline", new { id = requestline.Id }, requestline);
        }

        // DELETE: api/Requestlines/id #
        [HttpDelete("{id}")]
        public async Task<ActionResult<Requestline>> DeleteRequestline(int id)
        {
            var requestline = await _context.Requestline.FindAsync(id);
            if (requestline == null)
            {
                return NotFound();
            }

            _context.Requestline.Remove(requestline);
            await _context.SaveChangesAsync();

            await refreshrequestline(requestline);
            await recalculatetotal(requestline.RequestId);

            return requestline;
        }

        private bool RequestlineExists(int id)

        {
            return _context.Requestline.Any(e => e.Id == id);
        }
    }
}
