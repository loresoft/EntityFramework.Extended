﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tracker.MySql.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TrackerEntities : DbContext
    {
        public TrackerEntities()
            : base("name=TrackerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<audit> audits { get; set; }
        public virtual DbSet<item> items { get; set; }
        public virtual DbSet<item_2> item_2 { get; set; }
        public virtual DbSet<priority> priorities { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<productsummary> productsummaries { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<task> tasks { get; set; }
        public virtual DbSet<taskextended> taskextendeds { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
