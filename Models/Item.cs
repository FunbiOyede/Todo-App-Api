
namespace TodoApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name field  is required")]
        public string Name { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}
