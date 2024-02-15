﻿using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.Infra
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Server=localhost;" + 
                "Port=5432;Database=webApi;" +
                "User Id=postgres;" +
                "Password=lucas123;");
        }
            
    }
}
