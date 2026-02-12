using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts;

public interface IGenericRepository<T,Tkey> where T : BaseEntity<Tkey>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(ISpecifications<T,Tkey> specifications);
    Task<T?> GetByIdAsync(Tkey id);
    Task<T?> GetByIdAsync(ISpecifications<T, Tkey> specifications);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    
}
