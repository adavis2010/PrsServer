using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrsServer.Models;

namespace PrsServer.Data
{
    public class PrsServerDbContext : DbContext
    {
        public PrsServerDbContext (DbContextOptions<PrsServerDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) { // makes identifier unique
            }

        }
    }

