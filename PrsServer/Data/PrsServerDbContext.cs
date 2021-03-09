using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrsServer.Models;

namespace PrsServer.Data {
    public class PrsServerDbContext : DbContext {
        public PrsServerDbContext(DbContextOptions<PrsServerDbContext> options)
            : base(options) {
        }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Requestline> Requestlines { get; set; }




        protected override void OnModelCreating(ModelBuilder builder) { // makes unique identifier
            builder.Entity<User>(e => {e.HasIndex(u => u.Id).IsUnique(true);//unique identifier for User
            }); 

            builder.Entity<Vendor>(e => {
                e.HasIndex(u => u.Id).IsUnique(true);//unique for Vendor
            });

            builder.Entity<Product>(e => e.HasIndex(u => u.PartNbr).IsUnique(true));//unique for Product



        }


        public DbSet<PrsServer.Models.Product> Product { get; set; }

        public DbSet<PrsServer.Models.Request> Request { get; set; }

        public DbSet<PrsServer.Models.Requestline> Requestline { get; set; }






    }




}

