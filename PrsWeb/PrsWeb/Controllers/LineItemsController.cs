using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsWeb.Models;

namespace PrsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineItemsController : ControllerBase
    {
        private readonly PrsDBContext _context;

        public LineItemsController(PrsDBContext context)
        {
            _context = context;
        }

        // GET: api/LineItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineItem>>> GetLineItems()
        {
            return await _context.LineItems.ToListAsync();
        }

        // GET: api/LineItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineItem>> GetLineItem(int id)
        {
            var lineItem = await _context.LineItems.FindAsync(id);

            if (lineItem == null)
            {
                return NotFound();
            }

            return lineItem;
        }

        //get line items by request (HAH)
        [HttpGet("lines-for-req/{reqId}")]
        public async Task<ActionResult<IEnumerable<LineItem>>> GetRevRequests(int reqId)
        {
            var lineRequests = await _context.LineItems.Include(l => l.Product).Include(l => l.Request).Where(l=>l.RequestId == reqId)
                .ToListAsync();

            return lineRequests;
        }

        // PUT: api/LineItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLineItem(int id)
        {
            LineItem lineItem = await _context.LineItems.FindAsync(id);


            if (id != lineItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(lineItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Request r = lineItem.Request;
            r.Total = recalcTotal();

            return NoContent();
        }

        // POST: api/LineItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LineItem>> PostLineItem(LineItem lineItem)
        {
            _context.LineItems.Add(lineItem);
            await _context.SaveChangesAsync();

            Request r = lineItem.Request;
            r.Total = recalcTotal();

            return CreatedAtAction("GetLineItem", new { id = lineItem.Id }, lineItem);
        }

        // DELETE: api/LineItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineItem(int id)
        {
            var lineItem = await _context.LineItems.FindAsync(id);
            if (lineItem == null)
            {
                return NotFound();
            }

            _context.LineItems.Remove(lineItem);
            await _context.SaveChangesAsync();

            Request r = lineItem.Request;
            r.Total = recalcTotal();

            return NoContent();
        }

        public decimal recalcTotal()
        {
            decimal total = 0;
            foreach (var lineItem in _context.LineItems)
            { decimal cost = lineItem.Product.Price * lineItem.Quantity; 
            total = total + cost;
            }
            
            return total;
        }

        private bool LineItemExists(int id)
        {
            return _context.LineItems.Any(e => e.Id == id);
        }
    }
}
