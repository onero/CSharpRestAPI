using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using RestAppDAL.Entities;

namespace RestAppDAL.Context
{
    public sealed class SQLContext : DbContext, ICustomerContext
    {
        private static readonly string DBConnectionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DBConnection.txt");
        private static readonly string ConnectionString = File.ReadAllText(DBConnectionPath);

        public SQLContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AddressId + CustomerId = PK
            modelBuilder.Entity<CustomerAddress>()
                .HasKey(ca => new {ca.AddressId, ca.CustomerId});

            /* One address in CustomerAddress is one address from Address table,
             * however that one address can have many customers,
             * the address links to addressId as foreign key
             * */
            modelBuilder.Entity<CustomerAddress>()
                .HasOne(ca => ca.Address)
                .WithMany(a => a.Customers)
                .HasForeignKey(ca => ca.AddressId);

            /* One customer in CustomerAddress is one customer from Customer table,
             * however that one customer can have many addresses,
             * the customer links to customerId as foreign key
            */
            modelBuilder.Entity<CustomerAddress>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(ca => ca.CustomerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}