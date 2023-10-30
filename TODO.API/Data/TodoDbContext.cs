using Microsoft.EntityFrameworkCore;
using TODO.API.Models;

namespace TODO.API.Data
{
    public class TodoDbContext : DbContext
    {
        public DbSet<ItemData> Items { get; set; }
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

    }
}
