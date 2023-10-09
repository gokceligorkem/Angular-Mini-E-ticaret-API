using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Common;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Contexts
{
    public class EticaretDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public EticaretDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FileBase> Files { get; set; }
        public DbSet<FileImage> FilesImages { get; set; }
        public DbSet<InvoiceFile> Invoices { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(b=>b.ID);
            builder.Entity<Basket>().HasOne(b => b.Order)
                .WithOne(b => b.Basket)
                .HasForeignKey<Order>(b => b.ID);

            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker Entityler üzerinde yapılan değişiklerin  ya da yeni eklenen verilerin yakalanmasını sağlayan propertydir.Update operasyonlarında verileri yakalayıp elde etmemizi sağlar
            var datas=ChangeTracker.Entries<BaseEntity>();
                foreach (var data in datas)
                {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedTime = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedTime = DateTime.UtcNow,
                    _=> DateTime.UtcNow
                } ;
                }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
