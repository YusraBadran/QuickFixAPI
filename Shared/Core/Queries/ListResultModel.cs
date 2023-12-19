namespace QuickFix.Shared.Core.Queries;

public record ListResultModel<T>(List<T> Data, long TotalItems, int Page, int PageSize, int totalPages, int currentStartIndex)
    where T : notnull
{
    public static ListResultModel<T> Empty => new(Enumerable.Empty<T>().ToList(), 0, 0, 0, 0, 0);

    public static ListResultModel<T> Create(List<T> Data, long totalItems = 0, int page = 0, int pageSize = 5, int totalPages = 0, int currentStartIndex = 1)
    {
        return new ListResultModel<T>(Data, totalItems, page, pageSize, totalPages, currentStartIndex);
    }

    public ListResultModel<U> Map<U>(Func<T, U> map)
    {
        return ListResultModel<U>.Create(Data.Select(map).ToList(), TotalItems, Page, PageSize, totalPages, currentStartIndex);
    }
}
