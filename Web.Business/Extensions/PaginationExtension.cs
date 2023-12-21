using Microsoft.EntityFrameworkCore;
using Web.Contracts.Models;

namespace Web.Business.Extensions
{
    public static class DataPagerExtension
    {
        public static async Task<PagedResponseModel<T>> PaginateAsync<T>
            (this IQueryable<T> query, int page, int limit) where T : class
        {
            var paged = new PagedResponseModel<T>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;

            var startRow = (page - 1) * limit;

            paged.Items = await query.Skip(startRow)
                                .Take(limit)
                                .ToListAsync();
            paged.TotalItems = await query.CountAsync();
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }
    }
}