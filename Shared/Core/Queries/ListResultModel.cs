namespace QuickFix.Shared.Core.Queries;

public record ListResultModel<T>(List<T> Data, long TotalItems, int Page, int PageSize, int totalPages, int currentStartIndex, int currentEndIndex)
    where T : notnull
{
    public static ListResultModel<T> Empty => new(Enumerable.Empty<T>().ToList(), 0, 0, 0, 0, 0, 0);

    public static ListResultModel<T> Create(List<T> Data, long totalItems = 0, int page = 1, int pageSize = 10, int totalPages = 0, int currentStartIndex = 1, int currentEndIndex = 5)
    {
        return new ListResultModel<T>(Data, totalItems, page, pageSize, totalPages, currentStartIndex, currentEndIndex);
    }

    public ListResultModel<U> Map<U>(Func<T, U> map)
    {
        return ListResultModel<U>.Create(Data.Select(map).ToList(), TotalItems, Page, PageSize, totalPages, currentStartIndex, currentEndIndex);
    }
}
