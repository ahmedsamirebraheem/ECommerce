using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerce.Service.Specifications;

public abstract class BaseSpecifications<T, Tkey> : ISpecifications<T, Tkey> where T : BaseEntity<Tkey>
{
    public Expression<Func<T, bool>>? Criteria { get; }
    protected BaseSpecifications()
    {
    }
    protected BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
    {
        Criteria = criteriaExpression;
    }


    public ICollection<Expression<Func<T, object>>> IncludeExpression { get; } = [];

   

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        IncludeExpression.Add(includeExpression);
    }


    public Expression<Func<T, object>> OrderBy { get; private set; }

    public Expression<Func<T, object>> OrderByDescending { get; private set; }


    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpression)
    {
        OrderByDescending = orderByExpression;
    }
    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPaginated { get; private set; }

    protected void ApplyPagination(int pageSize,int pageIndex)
    {
        IsPaginated = true;
        Take = pageSize;
        Skip = (pageIndex-1)*pageSize;
    }
}