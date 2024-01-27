using  QuickFix.Identity.Shared.Models;
using  QuickFix.Shared.Abstractions.Model;
using  QuickFix.Shared.Abstractions.Queries;
using System.ComponentModel;

namespace  QuickFix.Shared.Core.Queries;

public record ListQuery<TResponse> : IListQuery<TResponse>
    where TResponse : notnull
{
    public IList<string>? Includes { get; init; }
    public IList<FilterModel>? Filters { get; init; }
    public IList<string>? Sorts { get; init; }
    [DefaultValue(1)]
    public int Page { get; init; } = 1;
    [DefaultValue(10)]
    public int PageSize { get; init; } = 10;
}
