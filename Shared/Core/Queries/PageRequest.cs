using  QuickFix.Shared.Abstractions.Model;
using  QuickFix.Shared.Abstractions.Queries;
using System.ComponentModel;

namespace  QuickFix.Shared.Core.Queries;

public record PageRequest : IPageRequest
{
    [DefaultValue(1)]
    public int Page { get; init; } = 1;
    [DefaultValue(10)]
    public int PageSize { get; init; } = 10;
    public IList<string>? Includes { get; init; }
    public IList<FilterModel>? Filters { get; init; }
    public IList<string>? Sorts { get; init; }
}
