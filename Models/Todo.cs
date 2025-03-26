namespace WebApplication.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
