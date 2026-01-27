using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Presistance.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Repository;

public class GenericRepository<T,Tkey>(StoreDbContext dbContext) : IGenericRepository<T,Tkey> where T : BaseEntity<Tkey>
{
    public async Task AddAcync(T entity) => await dbContext.Set<T>().AddAsync(entity);


    public async Task<IEnumerable<T>> GetAllAcync() => await dbContext.Set<T>().ToListAsync();


    public async Task<T?> GetByIdAcync(Tkey id) => await dbContext.Set<T>().FindAsync(id);
    

    public void Update(T entity) => dbContext.Set<T>().Update(entity);
    
    public void Remove(T entity) => dbContext.Set<T>().Remove(entity);
    
}
