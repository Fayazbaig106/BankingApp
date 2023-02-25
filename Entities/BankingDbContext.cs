using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Entities;

public partial class BankingDbContext : DbContext
{
    public BankingDbContext()
    {
    }

    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Purpose> Purposes { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-VLO9VHK;Initial Catalog=Banking;Integrated Security=SSPI;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Purpose>(entity =>
        {
            entity.Property(e => e.PurposeId).HasColumnName("PurposeID");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.AmountReceivedInInr)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("AmountReceivedInINR");
            entity.Property(e => e.AmountSendInUsd)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("AmountSendInUSD");
            entity.Property(e => e.PurposeId).HasColumnName("PurposeID");
            entity.Property(e => e.Receiver).HasMaxLength(200);
            entity.Property(e => e.Sender).HasMaxLength(200);

            entity.HasOne(d => d.Purpose).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PurposeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Purposes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
