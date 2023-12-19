namespace QuickFix.Shared.Abstractions.Queries;

public interface IListQuery<out TResponse> : IPageRequest, IQuery<TResponse>
    where TResponse : notnull
{ }
