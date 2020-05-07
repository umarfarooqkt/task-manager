using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pratice_api.Models;

namespace pratice_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketScaController : ControllerBase
    {
        private readonly TicketContext _context;

        public TicketScaController(TicketContext context)
        {
            _context = context;
        }

        // GET: api/TicketSca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketItems()
        {
            return await _context.TicketItems.ToListAsync();
        }

        // GET: api/TicketSca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(long id)
        {
            var ticket = await _context.TicketItems.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/TicketSca/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(long id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/TicketSca
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            _context.TicketItems.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        // DELETE: api/TicketSca/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ticket>> DeleteTicket(long id)
        {
            var ticket = await _context.TicketItems.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.TicketItems.Remove(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }

        private bool TicketExists(long id)
        {
            return _context.TicketItems.Any(e => e.Id == id);
        }
    }
}
