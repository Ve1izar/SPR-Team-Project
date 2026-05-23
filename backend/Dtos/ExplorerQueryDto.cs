namespace LocalDriveApi.Dtos;

public class ExplorerQueryDto
{
    public string Path { get; set; } = string.Empty;

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;

    public string SortBy { get; set; } = "name";

    public string SortOrder { get; set; } = "asc";
}