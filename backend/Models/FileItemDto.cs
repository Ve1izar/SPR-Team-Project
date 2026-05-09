namespace LocalDriveApi.Models
{
    public class FileItemDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public long? Size { get; set; }
        public DateTime LastModified { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}