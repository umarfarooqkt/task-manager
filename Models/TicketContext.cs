using System;
using Microsoft.EntityFrameworkCore;

namespace pratice_api.Models
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> TicketItems { get; set; }
    }
}
