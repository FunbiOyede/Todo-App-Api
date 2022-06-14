namespace TodoApp.Data.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<Item>> GetAll();
        Task<Item> Create(Item item);
        Task Update(Item item);
        Task<Item> Delete(Item item);
        Task<Item> GetById(int id);

    }
}
