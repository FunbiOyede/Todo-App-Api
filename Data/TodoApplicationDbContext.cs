

namespace TodoApp.Data
{
    public class TodoApplicationDbContext : DbContext
    {
        public TodoApplicationDbContext(DbContextOptions<TodoApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
    }
}
