namespace LocalDriveApi.Dtos
{
    public class CreateFolderDto
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
    }
}
