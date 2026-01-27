using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts;

public interface IUnitOfWork
{
    Task<int> SaveChangeAsync();
    IGenericRepository<T,Tkey> GerRepository<T, Tkey>() where T : BaseEntity<Tkey>;
}
