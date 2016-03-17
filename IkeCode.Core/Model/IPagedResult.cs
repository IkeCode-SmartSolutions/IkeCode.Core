namespace IkeCode.Core
{
    using System.Collections.Generic;

    public interface IPagedResult<TResult>
    {
        int TotalCount { get; }
        int Offset { get; }
        int Limit { get; }

        ICollection<TResult> Items { get; }
    }
}
