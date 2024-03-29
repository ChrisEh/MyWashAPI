﻿using Microsoft.EntityFrameworkCore;
using MyWashApi.Data.Models;
using System;

namespace MyWashApi.Data
{
    public partial class MyWashContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Pickup> Pickups { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public MyWashContext()
        {
        }

        public MyWashContext(DbContextOptions<MyWashContext> options)
            : base(options)
        {
        }

        internal object FindAsync<T>(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
