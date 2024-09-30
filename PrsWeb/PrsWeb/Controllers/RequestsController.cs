
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PrsWeb.Models;
using System.Text;

namespace PrsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PrsDBContext _context;

        public RequestsController(PrsDBContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        //get requests ready for review (HAH)
        [HttpGet("list-review/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRevRequests(int userId)
        {
            var revRequests = await _context.Requests.Include(r => r.User).
                Where(r => r.Status == "REVIEW").Where(r => r.UserId != userId).ToListAsync();
            return revRequests;
        }


        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        //Editing one's own request (HAH)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id || request.Status != "NEW")
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

        //Submitting one's own request (HAH)
        [HttpPut("submit-review/{id}")]
        public async Task<IActionResult> SubmitRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (id != request.Id || request.Status != "NEW")
            {
                return BadRequest();
            }

            request.SubmittedDate = DateTime.Now;

            if (request.Total <= 50.00M) { request.Status = "APPROVED"; }

            request.Status = "REVIEW";

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

        //approving request (HAH)
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            Request request = await _context.Requests.FindAsync(id);
             
            if (id != request.Id || request.Status != "REVIEW") { return BadRequest(); }

            request.Status = "APPROVED";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestNumCheck(id.ToString()))
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

        //rejecting request (HAH)
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> Reject(int id, string rejectionReason)
        {
            Request request = await _context.Requests.FindAsync(id);
             
            if (id != request.Id) { return BadRequest(); }

            request.Status = "REJECTED";
            request.ReasonForRejection = rejectionReason;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestNumCheck(id.ToString()))
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

        //// POST: api/Requests
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Request>> PostRequest(Request request)
        //{
        //    _context.Requests.Add(request);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        //}

        public string incrementRNum(string maxRNum)
        {
            string nextRNum = "";
            int num = Int32.Parse(maxRNum.Substring(7));
            num++;
            string numStr = num.ToString();
            numStr = numStr.PadLeft(4, '0');
            nextRNum += numStr;
            return nextRNum;
        }

        public string RNum(DateOnly today)
        {
            StringBuilder requestNumber = new StringBuilder();

            today = DateOnly.FromDateTime(DateTime.Now);
            string rNumerals = String.Format("{0:yyMMdd}", today);

            requestNumber.Append("R");
            requestNumber.Append(rNumerals);

            return requestNumber.ToString();
        }

        //create a new request (HAH)
        [HttpPost]
        public async Task<ActionResult<Request>> AddRequest(RequestForm requestForm)
        {
            Request request = new();
            string maxReq = _context.Requests.Max(r => r.RequestNumber);

            request.UserId = requestForm.UserId;
            request.Description = requestForm.Description;
            request.Justification = requestForm.Justification;
            request.DateNeeded = requestForm.DateNeeded;
            request.DeliveryMode = requestForm.DeliveryMode;
            request.RequestNumber = RNum(DateOnly.FromDateTime(DateTime.Now)) + incrementRNum(maxReq);
            request.Status = "NEW";
            request.Total = 0.00M;
            request.SubmittedDate = DateTime.Now;
            request.ReasonForRejection = null;


            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }

        private bool RequestNumCheck(string requestNumber)
        {
            return _context.Requests.Any(e => e.RequestNumber == requestNumber);
        }
    }
}
