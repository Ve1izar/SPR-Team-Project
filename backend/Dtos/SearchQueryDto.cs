namespace LocalDriveApi.Dtos
{
    public class SearchQueryDto
    {
        public string Query { get; set; } = string.Empty;

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; } = "name";

        public string SortOrder { get; set; } = "asc";
    }
}