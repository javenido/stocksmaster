using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StocksMasterAPI.Models
{
    public partial class StocksMasterDBContext : DbContext
    {
        public StocksMasterDBContext()
        {
        }

        public StocksMasterDBContext(DbContextOptions<StocksMasterDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<StocksDatum> StocksData { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Data Source=sqlserverapiv1.chtfsohnvags.us-east-1.rds.amazonaws.com,1433;database=StocksMasterDB;User ID=marvindelara;Password=marvindelara;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CompanySymbol).HasMaxLength(30);
            });

            modelBuilder.Entity<StocksDatum>(entity =>
            {
                entity.HasKey(e => e.StocksDataId)
                    .HasName("PK__StocksDa__25F1CAFDD631207D");

                entity.Property(e => e.Date).HasMaxLength(50);

                entity.Property(e => e.StocksPrice).HasColumnType("decimal(10, 2)");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.StocksData)
                //    .HasForeignKey(d => d.CompanyId)
                //    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
