﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TST.Data.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TSTEntities : DbContext
    {
        public TSTEntities()
            : base("name=TSTEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TST_Employees> TST_Employees { get; set; }
        public virtual DbSet<TST_EmployeeStatuses> TST_EmployeeStatuses { get; set; }
        public virtual DbSet<TST_SupportTickets> TST_SupportTickets { get; set; }
        public virtual DbSet<TST_TechNotes> TST_TechNotes { get; set; }
        public virtual DbSet<TST_TicketPriorities> TST_TicketPriorities { get; set; }
        public virtual DbSet<TST_TicketStatuses> TST_TicketStatuses { get; set; }
        public virtual DbSet<TST_Departments> TST_Departments { get; set; }
    }
}
