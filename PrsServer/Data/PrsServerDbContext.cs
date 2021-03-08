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





        protected override void OnModelCreating(ModelBuilder builder) { // makes unique identifier
            builder.Entity<User>(e => {
                e.HasIndex(u => u.Id).IsUnique(true);
                builder.Entity<Vendor>(e => {
                    e.HasIndex(u => u.Id).IsUnique(true);


                });
            });
            
            }
    }



        
    } 

