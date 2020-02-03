using System;
using System.Collections.Generic;
using System.Text;
using BookApiWithCore.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookApiWithCore.Data
{
    public class DbContext : IdentityDbContext
    {
       

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}
