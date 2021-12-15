using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TraSuaWeb.Models
{
    public partial class DBtrasuaContext : DbContext
    {
        public DBtrasuaContext()
        {
        }

        public DBtrasuaContext(DbContextOptions<DBtrasuaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<GioiThieu> GioiThieus { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSp> LoaiSps { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<TinhTrang> TinhTrangs { get; set; }
        public virtual DbSet<Tintuc> Tintucs { get; set; }
        public virtual DbSet<TuHang> TuHangs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=HKING77\\SQLEXPRESS;Database=DBtrasua;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Account__RoleId__4BAC3F29");
            });

            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasKey(e => e.MaCthd)
                    .HasName("PK__ChiTietH__1E4FA771B3DB8943");

                entity.ToTable("ChiTietHoaDon");

                entity.Property(e => e.MaCthd).HasColumnName("MaCTHD");

                entity.Property(e => e.MaHd).HasColumnName("MaHD");

                entity.Property(e => e.MaSize)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaHd)
                    .HasConstraintName("FK__ChiTietHoa__MaHD__4D94879B");

                entity.HasOne(d => d.MaSizeNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaSize)
                    .HasConstraintName("FK__ChiTietHo__MaSiz__4CA06362");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK__ChiTietHoa__MaSP__4E88ABD4");
            });

            modelBuilder.Entity<GioiThieu>(entity =>
            {
                entity.HasKey(e => e.IdGt)
                    .HasName("PK__GioiThie__B773F218226CEE07");

                entity.ToTable("GioiThieu");

                entity.Property(e => e.IdGt).HasColumnName("IdGT");

                entity.Property(e => e.Anh).HasMaxLength(50);

                entity.Property(e => e.MoTa).HasMaxLength(2000);

                entity.Property(e => e.TieuDe).HasMaxLength(20);
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd)
                    .HasName("PK__HoaDon__2725A6E0E485C457");

                entity.ToTable("HoaDon");

                entity.Property(e => e.MaHd).HasColumnName("MaHD");

                entity.Property(e => e.DiaChiGiaoHang).HasMaxLength(50);

                entity.Property(e => e.HiperId).HasColumnName("HiperID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.MaTu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NgayDat).HasColumnType("datetime");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.HasOne(d => d.Hiper)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.HiperId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__HoaDon__HiperID__4F7CD00D");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_HoaDon_TinhTrang");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__HoaDon__MaKH__5070F446");

                entity.HasOne(d => d.MaTuNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaTu)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__HoaDon__MaTu__5165187F");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK__KhachHan__2725CF1ED9FD23E4");

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Sðt)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SÐT");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");
            });

            modelBuilder.Entity<LoaiSp>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiSP__730A575937EB8C89");

                entity.ToTable("LoaiSP");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Anh).HasMaxLength(250);

                entity.Property(e => e.TenLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.NameWithType).HasMaxLength(255);

                entity.Property(e => e.PathWithType).HasMaxLength(255);

                entity.Property(e => e.Slug).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleDesciptions).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__SanPham__2725081CFF961116");

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.AnhSp)
                    .HasMaxLength(50)
                    .HasColumnName("AnhSP");

                entity.Property(e => e.MaLoai)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MaSize)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.TenSp)
                    .HasMaxLength(100)
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SanPham__MaLoai__52593CB8");

                entity.HasOne(d => d.MaSizeNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaSize)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SanPham__MaSize__534D60F1");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.HasKey(e => e.HiperId)
                    .HasName("PK__Shipper__2811CB0051ECBF05");

                entity.ToTable("Shipper");

                entity.Property(e => e.HiperId).HasColumnName("HiperID");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TenShiper).HasMaxLength(100);
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasKey(e => e.MaSize)
                    .HasName("PK__Size__A787E7EDB42F06FB");

                entity.ToTable("Size");

                entity.Property(e => e.MaSize)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.LoaiSize)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TinhTrang>(entity =>
            {
                entity.ToTable("TinhTrang");

                entity.Property(e => e.Mota).HasMaxLength(2000);

                entity.Property(e => e.TrangThai).HasMaxLength(50);
            });

            modelBuilder.Entity<Tintuc>(entity =>
            {
                entity.HasKey(e => e.IdTt)
                    .HasName("PK__Tintuc__B7701AAA5C3917C8");

                entity.ToTable("Tintuc");

                entity.Property(e => e.IdTt).HasColumnName("IdTT");

                entity.Property(e => e.AnhGt)
                    .HasMaxLength(50)
                    .HasColumnName("AnhGT");

                entity.Property(e => e.MoTa).HasMaxLength(2000);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.TieuDe).HasMaxLength(20);
            });

            modelBuilder.Entity<TuHang>(entity =>
            {
                entity.HasKey(e => e.MaTu)
                    .HasName("PK__TuHang__2725005A947078DA");

                entity.ToTable("TuHang");

                entity.Property(e => e.MaTu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TenTu).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
