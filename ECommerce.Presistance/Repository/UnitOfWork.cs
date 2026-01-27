using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Presistance.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Repository;

public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repository = [];
    public IGenericRepository<T, Tkey> GerRepository<T, Tkey>() where T : BaseEntity<Tkey>
    {
        var EntityType = typeof(T);
        if(_repository.TryGetValue(EntityType,out object? repository))
        {
            return (IGenericRepository<T, Tkey>)repository;
        }
        var newRepo = new GenericRepository<T, Tkey>(dbContext);
        _repository.Add(EntityType, newRepo);
        return newRepo;
    }

    public async Task<int> SaveChangeAsync() => await dbContext.SaveChangesAsync();
   
}
