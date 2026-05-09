namespace LocalDriveApi.Models
{
    public class FileItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string PhysicalPath { get; set; } = string.Empty;

    }
}
