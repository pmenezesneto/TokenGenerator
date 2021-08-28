using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TokenGenerator.Models
{
    public interface IDbContext
    {
        DbSet<Card> Cards { get; set; }
        DbSet<Customer> Customers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Update<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
    }
}