namespace LocalDriveApi.Models
{
    public class FileItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        // Тип: "File" або "Folder"
        public string Type { get; set; } = string.Empty; 
        
        // Поля для файлів
        public string? ContentType { get; set; } 
        public long Size { get; set; }
        public string PhysicalPath { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Ієрархія
        public int? ParentId { get; set; }
        public FileItem? Parent { get; set; }
        public ICollection<FileItem> Children { get; set; } = [];

        // Зв'язок з користувачем
        public int UserId { get; set; } 
        public User User { get; set; } = null!;
    }
}