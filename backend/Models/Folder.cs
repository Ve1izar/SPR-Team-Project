namespace LocalDriveApi.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public Folder? Parent { get; set; }
        public ICollection<Folder> Children { get; set; } = [];
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime CreatDate { get; set; } = DateTime.UtcNow;
    }
}

