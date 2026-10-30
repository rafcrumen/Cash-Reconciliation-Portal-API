using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DomainModel = Domain.Model;
namespace Repository
{
    public class DataContext : DbContext
    {
        public DataContext(
            DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<DomainModel.Deposit> Deposits => Set<DomainModel.Deposit>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DomainModel.Deposit>(entity =>
            {
                entity.HasKey(x => x.DepositId);

                entity.HasIndex(x => x.DepositNumber)
                      .IsUnique();

                entity.Property(x => x.ExpectedAmount)
                      .HasPrecision(18, 2);

                entity.Property(x => x.ActualAmount)
                      .HasPrecision(18, 2);

                entity.Property(x => x.Variance)
                      .HasPrecision(18, 2);
            });
        }
    }
}
