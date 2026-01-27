using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Domain.Contracts;

public interface ISpecifications<T,Tkey> where T : BaseEntity<Tkey>
{
    public Expression<Func<T, bool>>? Criteria { get; }
    public ICollection<Expression<Func<T,object>>> IncludeExpression { get; }
    public Expression<Func<T, object>> OrderBy { get; }
    public Expression<Func<T, object>> OrderByDescending { get; }
    public int Take { get;}
    public int Skip { get;}
    public bool IsPaginated { get; }
}
