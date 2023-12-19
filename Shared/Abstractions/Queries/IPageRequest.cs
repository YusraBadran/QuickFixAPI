
using QuickFix.Shared.Abstractions.Model;

namespace QuickFix.Shared.Abstractions.Queries;

public interface IPageRequest
{
    IList<string>? Includes { get; init; }
    IList<FilterModel>? Filters { get; init; }
    IList<string>? Sorts { get; init; }
    int Page { get; init; }
    int PageSize { get; init; }
}
