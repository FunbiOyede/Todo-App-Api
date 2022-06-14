namespace TodoApp.Data.Services
{
    public class TaskServices : ITaskService
    {
        private readonly TodoApplicationDbContext dbContext;
        public TaskServices(TodoApplicationDbContext db)
        {
            dbContext = db;

        }
        public async Task<Item> Create(Item item)
        {
            dbContext.Items.Add(item);
            await dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item> Delete(Item item)
        {
            dbContext.Items.Remove(item);
            await dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            var items = await dbContext.Items.ToListAsync();
            return items;
        }

        public async Task<Item> GetById(int id)
        {
            var item = await dbContext.Items.Where(x => x.Id == id).FirstOrDefaultAsync();
            // FindAsync(id);
            return item;
        }

        public async Task Update(Item item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

    }
}
