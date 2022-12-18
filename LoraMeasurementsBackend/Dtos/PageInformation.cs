namespace LoraMeasurementsBackend.Dtos;

public class PageInformation
{
    public int PageNumber { get; set; }
    public int RowsPerPage { get; set; }
    public string SortBy { get; set; }
    public string SortType { get; set; }
}