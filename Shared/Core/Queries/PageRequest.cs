using QuickFix.Shared.Abstractions.Model;
using QuickFix.Shared.Abstractions.Queries;

namespace CleanUp.Shared.Core.Queries;

public record PageRequest : IPageRequest
{
    public int Page { get; init; } = 0;
    public int PageSize { get; init; } = 5;
    public IList<string>? Includes { get; init; }
    public IList<FilterModel>? Filters { get; init; }
    public IList<string>? Sorts { get; init; }
}
