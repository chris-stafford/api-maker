using IMOv2.Api.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMOv2.Api.Database;

public class ImoV2DbContext : DbContext
{
    public virtual DbSet<XxxEntity> XxxEntities { get; set; }

    public override DbSet<TEntity> Set<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "password=qwe123;user id=root;server=127.0.0.1;port=3306;database=ImoV2";
        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString),
            null);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new XxxConfiguration().Configure(modelBuilder.Entity<XxxEntity>());
        
        base.OnModelCreating(modelBuilder);
    }
}