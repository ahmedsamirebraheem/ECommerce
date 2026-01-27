using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts;

public interface IGenericRepository<T,Tkey> where T : BaseEntity<Tkey>
{
    Task<IEnumerable<T>> GetAllAcync();
    Task<T?> GetByIdAcync(Tkey id);
    Task AddAcync(T entity);
    void Update(T entity);
    void Remove(T entity);
    
}
