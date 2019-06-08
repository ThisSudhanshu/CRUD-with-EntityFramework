using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RajShoeApp.Model
{
    public class ShoeModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string category { get; set; }
        public float Price { get; set; }
        public string Brand { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
        public float Weight { get; set; }

        
        public dynamic this[string access]
        {
            get
            {
                var property = typeof(ShoeModels).GetProperty(access);
                return property.GetValue(this) as dynamic;
            }
        }
    }

    public class ShoeModelContext : DbContext
    {
        public ShoeModelContext(DbContextOptions<ShoeModelContext> options) :
            base(options)
        {
        }
        public DbSet<ShoeModels> ShoeData { get; set; }
    }
}
