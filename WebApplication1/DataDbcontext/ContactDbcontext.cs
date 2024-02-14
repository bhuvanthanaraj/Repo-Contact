using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DataDbcontext
{
    public class ContactDbcontext : DbContext

    {
        public ContactDbcontext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact> contacts { get; set; }

    }
}
