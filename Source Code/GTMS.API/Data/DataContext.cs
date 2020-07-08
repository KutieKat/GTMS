using Microsoft.EntityFrameworkCore;
using GTMS.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GTMS.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<HuongNghienCuu> DanhSachHuongNghienCuu { get; set; }
        public DbSet<DoAn> DanhSachDoAn { get; set; }
        public DbSet<GiangVien> DanhSachGiangVien { get; set; }
        public DbSet<HocKy> DanhSachHocKy { get; set; }
        public DbSet<HuongDanDoAn> DanhSachHuongDanDoAn { get; set; }
        public DbSet<Khoa> DanhSachKhoa { get; set; }
        public DbSet<KhoaDaoTao> DanhSachKhoaDaoTao { get; set; }
        public DbSet<Lop> DanhSachLop { get; set; }
        public DbSet<PhanBienDoAn> DanhSachPhanBienDoAn { get; set; }
        public DbSet<SinhVien> DanhSachSinhVien { get; set; }
        public DbSet<TaiKhoan> DanhSachTaiKhoan { get; set; }
        public DbSet<ThanhVienHDBV> DanhSachThanhVienHDBV { get; set; }
        public DbSet<QuyDinh> DanhSachQuyDinh { get; set; }
        public DbSet<CaiDat> DanhSachCaiDat { get; set; }
        public DbSet<DiemDoAn> DanhSachDiemDoAn { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Keys
            modelBuilder.Entity<HuongNghienCuu>().HasKey(x => x.MaHuongNghienCuu);
            modelBuilder.Entity<DoAn>().HasKey(x => x.MaDoAn);
            modelBuilder.Entity<GiangVien>().HasKey(x => x.MaGiangVien);
            modelBuilder.Entity<HocKy>().HasKey(x => x.MaHocKy);
            modelBuilder.Entity<HuongDanDoAn>().HasKey(x => new { x.MaGiangVien, x.MaDoAn });
            modelBuilder.Entity<Khoa>().HasKey(x => x.MaKhoa);
            modelBuilder.Entity<KhoaDaoTao>().HasKey(x => x.MaKhoaDaoTao);
            modelBuilder.Entity<Lop>().HasKey(x => x.MaLop);
            modelBuilder.Entity<PhanBienDoAn>().HasKey(x => new { x.MaGiangVien, x.MaDoAn});
            modelBuilder.Entity<SinhVien>().HasKey(x => x.MaSinhVien);
            modelBuilder.Entity<TaiKhoan>().HasKey(x => x.MaTaiKhoan);
            modelBuilder.Entity<ThanhVienHDBV>().HasKey(x => new { x.MaDoAn, x.MaGiangVien });
            modelBuilder.Entity<QuyDinh>().HasKey(x => x.MaQuyDinh);
            modelBuilder.Entity<CaiDat>().HasKey(x => x.MaCaiDat);
            modelBuilder.Entity<QuyDinh>().HasKey(x => x.MaQuyDinh);
            modelBuilder.Entity<DiemDoAn>().HasKey(x => new { x.MaDoAn, x.MaGiangVien });

            // Requirements
            modelBuilder.Entity<Khoa>().HasIndex(x => x.TenKhoa).IsUnique();
            modelBuilder.Entity<Khoa>().Property(x => x.TenKhoa).IsRequired();
            modelBuilder.Entity<Khoa>().HasIndex(x => x.TenVietTat).IsUnique();
            modelBuilder.Entity<Khoa>().Property(x => x.TenVietTat).IsRequired();

            modelBuilder.Entity<HocKy>().HasIndex(x => x.TenHocKy).IsUnique();
            modelBuilder.Entity<HocKy>().Property(x => x.TenHocKy).IsRequired();

            modelBuilder.Entity<HuongNghienCuu>().HasIndex(x => x.TenHuongNghienCuu).IsUnique();
            modelBuilder.Entity<HuongNghienCuu>().Property(x => x.TenHuongNghienCuu).IsRequired();

            modelBuilder.Entity<KhoaDaoTao>().HasIndex(x => x.TenKhoaDaoTao).IsUnique();
            modelBuilder.Entity<KhoaDaoTao>().Property(x => x.TenKhoaDaoTao).IsRequired();
            modelBuilder.Entity<KhoaDaoTao>().HasIndex(x => x.TenVietTat).IsUnique();
            modelBuilder.Entity<KhoaDaoTao>().Property(x => x.TenVietTat).IsRequired();

            modelBuilder.Entity<Lop>().HasIndex(x => x.TenLop).IsUnique();
            modelBuilder.Entity<Lop>().Property(x => x.TenLop).IsRequired();
            modelBuilder.Entity<Lop>().HasIndex(x => x.TenVietTat).IsUnique();
            modelBuilder.Entity<Lop>().Property(x => x.TenVietTat).IsRequired();

            modelBuilder.Entity<SinhVien>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<SinhVien>().HasIndex(x => x.SoDienThoai).IsUnique();

            modelBuilder.Entity<DiemDoAn>().Property(x => x.Diem).IsRequired();
            modelBuilder.Entity<ThanhVienHDBV>().Property(x => x.ChucVu).IsRequired();

            // Relationships
            modelBuilder.Entity<DoAn>()
                .HasOne(x => x.HuongNghienCuu)
                .WithMany(x => x.DoAn)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DoAn>()
                .HasOne(x=>x.HocKy)
                .WithMany(x=>x.DoAn)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GiangVien>()
                .HasOne(x => x.Khoa)
                .WithMany(x => x.GiangVien)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HuongDanDoAn>()
                .HasOne(x => x.GiangVien)
                .WithMany(x => x.HuongDanDoAn)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HuongDanDoAn>()
                .HasOne(x => x.DoAn)
                .WithMany(x => x.HuongDanDoAn)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lop>()
                .HasOne(x => x.Khoa)
                .WithMany(x => x.Lop)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lop>()
                .HasOne(x => x.KhoaDaoTao)
                .WithMany(x => x.Lop)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhanBienDoAn>()
                .HasOne(x => x.GiangVien)
                .WithMany(x => x.PhanBienDoAn)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhanBienDoAn>()
                .HasOne(x => x.DoAn)
                .WithMany(x => x.PhanBienDoAn)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SinhVien>()
                .HasOne(x => x.Lop)
                .WithMany(x => x.SinhVien)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SinhVien>()
                .HasOne(x => x.DoAn)
                .WithMany(x => x.SinhVien)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ThanhVienHDBV>()
                .HasOne(x => x.GiangVien)
                .WithMany(x => x.ThanhVienHDBV)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ThanhVienHDBV>()
                .HasOne(x => x.DoAn)
                .WithMany(x => x.ThanhVienHDBV)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DiemDoAn>()
                .HasOne(x => x.DoAn)
                .WithMany(x => x.DiemDoAn)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DiemDoAn>()
                .HasOne(x => x.GiangVien)
                .WithMany(x => x.DiemDoAn) 
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}