namespace IkeCode.Core
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Encapsulate result list into a normalized, ready for pagination
    /// </summary>
    /// <typeparam name="TResult">Type of Items List</typeparam>
    public class PagedResult<TResult> : IPagedResult<TResult>
    {
        /// <summary>
        /// Items to be skipped
        /// </summary>
        public int Offset { get; private set; }
        
        /// <summary>
        /// Max number of items to be returned
        /// </summary>
        public int Limit { get; private set; }

        /// <summary>
        /// Total of all items os collection
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Item to be returned
        /// </summary>
        public ICollection<TResult> Items { get; private set; }

        /// <summary>
        /// Generic constructor
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="totalCount"></param>
        /// <param name="items"></param>
        public PagedResult(ICollection<TResult> items, int offset, int limit, int totalCount)
        {
            Offset = offset;
            Limit = limit;
            TotalCount = totalCount;
            Items = items;
        }
        
        /// <summary>
        /// Paginate IQueryable objects
        /// </summary>
        /// <param name="source">Must to .OrderBy(...) before call PagedList</param>
        /// <param name="offset">Items to be skipped</param>
        /// <param name="limit">Max number of items to be returned</param>
        public PagedResult(IQueryable<TResult> source, int offset, int limit)
        {
            TotalCount = source.Count();
            Offset = offset < 1 ? 0 : offset;
            Limit = limit;
            
            var items = source.Skip(Offset).Take(Limit).ToList();

            Items = items;
        }
    }
}
