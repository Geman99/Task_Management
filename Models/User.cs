namespace TaskManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // For simplicity, store plain text (never do this in production!)
        public Role Role { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }
    }

}
