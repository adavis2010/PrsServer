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
    public class RequestsController : ControllerBase
    {
        private readonly PrsServerDbContext _context;

        public RequestsController(PrsServerDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests  (Get All)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Requests
                                    .Include(x => x.User)
                                    .ToListAsync();

        }

        // GET: api/Requests/5 (PK)
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests
                .Include(x => x.User)
                .Include(x => x.Requestlines)
                .ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }


        //GET: Review Request /// GetReviews(userId) - retrieving requests in review status and not owned by userId
        [HttpGet("review/{userid}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsInReview(int userid) {
            return await _context.Requests
                                    .Where(v => v.Status == Models.Request.StatusReview
                                             && v.UserId != userid)
                                         .ToListAsync();

        }

        


        //PUT: Reject request//
        [HttpPut("reject")]
        public async Task<ActionResult<IEnumerable<Request>>> PutRequestsInReject(int userid) {
            return await _context.Requests
                                    .Where(v => v.Status == Models.Request.StatusReject
                                             && v.UserId != userid)
                                         .ToListAsync();
        }

        //PUT: 50:00 auto approved
        [HttpPut("review")] //Add method on status
        public async Task<IActionResult> SetRequestStatusToReview(Request request) {
            //whatever gets passed in on url gets passed in on this method
            if (request == null) {
                return NotFound();
            }
            request.Status = (request.Total <= 50) ? "APPROVED" : "REVIEW"; // 
            return await PutRequest(request.Id, request); // Set property to string Edit
        }


            //PUT: unconditionally setting to approve // 
            [HttpPut("approve")] //
        public async Task<IActionResult> SetRequestStatusToApproved(Request request) {
            //whatever gets passed in on url gets passed in on this method
            
            if (request == null) {
                return BadRequest();
            }
            request.Status = Models.Request.StatusApproved; 
            return await PutRequest(request.Id, request); // Set property to string Edit

        }

            // PUT: api/Requests/5 (Change)
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Requests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
