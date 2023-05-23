using EticaretApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Persistence.Contexts
{
    public class EticaretApiDbContext : DbContext
    {

        public EticaretApiDbContext(DbContextOptions options) : base(options)
        { } //Bu kontainer IoC de doldurulucak koymazsak hata alırız 
        //ServiceRegistration olustur ust için 
        //DesingTimeDbContextFctory buradaki de .net cl ıcın olusturmamız gerekn sınıf

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
