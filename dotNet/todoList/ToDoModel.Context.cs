﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace todoList
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ToDoModel : DbContext
    {
        public ToDoModel()
            : base("name=ToDoModel")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamTaskMatch> TeamTaskMatches { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTaskMatch> UserTaskMatches { get; set; }
        public virtual DbSet<UserTeamMatch> UserTeamMatches { get; set; }
    }
}