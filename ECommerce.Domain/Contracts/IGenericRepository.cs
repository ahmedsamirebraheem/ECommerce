using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts;

public interface IGenericRepository<T,Tkey> where T : BaseEntity<Tkey>
{
    Task<IEnumerable<T>> GetAllAcync();
    Task<IEnumerable<T>> GetAllAcync(ISpecifications<T,Tkey> specifications);
    Task<T?> GetByIdAcync(Tkey id);
    Task<T?> GetByIdAsync(ISpecifications<T, Tkey> specifications);
    Task AddAcync(T entity);
    void Update(T entity);
    void Remove(T entity);
    
}
