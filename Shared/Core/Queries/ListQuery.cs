
using QuickFix.Shared.Abstractions.Model;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.Shared.Core.Queries;

public record ListQuery<TResponse> : IListQuery<TResponse>
    where TResponse : notnull
{
    public IList<string>? Includes { get; init; }
    public IList<FilterModel>? Filters { get; init; }
    public IList<string>? Sorts { get; init; }
    public int Page { get; init; } = 0;
    public int PageSize { get; init; } = 5;
}
