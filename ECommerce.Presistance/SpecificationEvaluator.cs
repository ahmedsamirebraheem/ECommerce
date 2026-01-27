using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ECommerce.Presistance;

public static class SpecificationEvaluator
{
    public static IQueryable<T> CreateQuery<T, Tkey>(IQueryable<T> entryPoint,
        ISpecifications<T, Tkey> specifications) where T : BaseEntity<Tkey>
    {
        var query = entryPoint;

        if (specifications is not null)
        {

            // 1. إضافة شرط الفلترة (مثلاً: Where p => p.Id == id)
            if (specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }

            // 2. إضافة الـ Includes (مثلاً: Include Brand and Type)
            if (specifications.IncludeExpression is not null && specifications.IncludeExpression.Count != 0)
            {
                foreach (var item in specifications.IncludeExpression)
                {
                    query = query.Include(item);
                }
            }
            if(specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if(specifications.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }
            if (specifications.IsPaginated)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }
        }

        return query;
    }
}