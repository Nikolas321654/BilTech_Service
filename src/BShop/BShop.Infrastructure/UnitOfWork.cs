using BShop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace BShop.Infrastructure;

public class UnitOfWork(ShopDbContext context) : IUnitOfWork
{
    private IDbContextTransaction _transaction;
    
    public async Task BeginTransactionAsync() => _transaction = await context.Database.BeginTransactionAsync();
    public async Task CommitAsync() => await _transaction.CommitAsync();
    public async Task RollbackAsync() => await _transaction.RollbackAsync();
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}