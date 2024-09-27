using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DemoEFAPISalesDB.Models;

public partial class SALESDBContext : DbContext
{
    //commenting this out.  dunno why.
    //public SALESDBContext()
    //{
    //}

    public SALESDBContext(DbContextOptions<SALESDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalesDenormalized> SalesDenormalizeds { get; set; }

    public virtual DbSet<SalesPerson> SalesPeople { get; set; }

    //we may need this later, but maybe not, per Mike

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-E9NT78D\\SQLEXPRESS;Database=SALESDB;Integrated Security=True;TrustServerCertificate=True;");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Customer>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07920024FB");
//        });

//        modelBuilder.Entity<Region>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Region__3214EC0756A29CB8");
//        });

//        modelBuilder.Entity<Sale>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Sales__3214EC073BCC6597");

//            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__Sales__CustomerI__300424B4");

//            entity.HasOne(d => d.SalesPerson).WithMany(p => p.Sales)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__Sales__SalesPers__2F10007B");
//        });

//        modelBuilder.Entity<SalesDenormalized>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Sales_De__3214EC07168E1A49");
//        });

//        modelBuilder.Entity<SalesPerson>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__SalesPer__3214EC073C6995BA");

//            entity.HasOne(d => d.Region).WithMany(p => p.SalesPeople)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__SalesPers__Regio__2C3393D0");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
