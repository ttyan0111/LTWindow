-- Create database QuanLyQuanNuoc
CREATE DATABASE QuanLyQuanNuoc;

USE QuanLyQuanNuoc;

-- Create table NhanVien
CREATE TABLE NhanVien (
    MaNhanVien INT IDENTITY(1, 1), 
    TenNhanVien NVARCHAR(100) NOT NULL, 
    UserName VARCHAR(20) NOT NULL UNIQUE, -- Đảm bảo không trùng username
    Passcode VARCHAR(16) NOT NULL,
    ChucVu NVARCHAR(100) NOT NULL,
    trangThaiHoatDong BIT NOT NULL,

    CONSTRAINT pk_MaNhanVien_NV PRIMARY KEY (MaNhanVien)
);

-- Create table LoaiMon
CREATE TABLE LoaiMon (
    MaLoaiMon VARCHAR(4), 
    TenLoaiMon NVARCHAR(10) NOT NULL UNIQUE,

    CONSTRAINT pk_MaLoaiMon_LM PRIMARY KEY (MaLoaiMon)
);

-- Create table DonHang
CREATE TABLE DonHang (
    MaDonHang INT IDENTITY(1, 1),
    MaNhanVien INT,
    NgayDatHang DATETIME, 
    TongTien FLOAT,

    CONSTRAINT pk_MaDonHang_DH PRIMARY KEY (MaDonHang),
    CONSTRAINT fk_MaNhanVien_DH FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- Create table DanhSachMon
CREATE TABLE DanhSachMon (
    MaMon VARCHAR(4), 
    MaLoaiMon VARCHAR(4), 
    TenMon NVARCHAR(100), 
    Gia FLOAT DEFAULT 10000.0, 
    SoLuong INT DEFAULT 0,

    CONSTRAINT pk_MaMon_DSM PRIMARY KEY (MaMon),
    CONSTRAINT fk_MaLoaiMon_DSM FOREIGN KEY (MaLoaiMon) REFERENCES LoaiMon(MaLoaiMon)
);

-- Create table ChiTietDonHang
CREATE TABLE ChiTietDonHang (
    MaChiTietDonHang INT IDENTITY(1, 1), 
    MaMon VARCHAR(4), 
    MaDonHang INT, 
    SoLuongMon INT, 
    TongPhu FLOAT,

    CONSTRAINT pk_MaChiTietDonHang_CTDH PRIMARY KEY (MaChiTietDonHang),
    CONSTRAINT fk_MaMon_CTDH FOREIGN KEY (MaMon) REFERENCES DanhSachMon(MaMon),
    CONSTRAINT fk_MaDonHang_CTDH FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang)
);

-- Create table Ban
CREATE TABLE Ban (
    MaBan INT IDENTITY(1, 1),
    TenBan NVARCHAR(10),
    TrangThai BIT DEFAULT 0,

    CONSTRAINT pk_MaBan_B PRIMARY KEY (MaBan)
);

-- Select data from tables for validation
SELECT * FROM NhanVien;
SELECT * FROM DanhSachMon;
SELECT * FROM ChiTietDonHang;
SELECT * FROM DonHang;
SELECT * FROM LoaiMon;

-- Insert sample data into NhanVien table ensuring unique UserName
INSERT INTO NhanVien (TenNhanVien, UserName, Passcode, ChucVu, trangThaiHoatDong) VALUES
(N'Nguyễn Văn Dũ', 'nvd', 'pass1234', N'Nhân viên', 1),
(N'Đang Lê Hoàng Danh', 'dlhd', 'pass1236', N'Nhân viên', 1),
(N'Vũ Huy Minh', 'vhminh', 'pass1237', N'Nhân viên', 1),
(N'Trương Trường Giang', 'ttg', 'pass1235', N'Nhân viên', 1);

-- Insert sample data into LoaiMon table ensuring unique MaLoaiMon
INSERT INTO LoaiMon (MaLoaiMon, TenLoaiMon) VALUES
('LM01', N'Trà Sữa'),
('LM02', N'	Cà Phê'),
('LM03', N'	Nước Ép');

-- Insert sample data into DonHang table
INSERT INTO DonHang (MaNhanVien, NgayDatHang, TongTien) VALUES
('1', '2024-12-08 10:00:00', 50000.0),
('2', '2024-12-10 11:30:00', 70000.0),
('3', '2024-12-12 14:00:00', 90000.0);

-- Insert sample data into DanhSachMon table
INSERT INTO DanhSachMon (MaMon, MaLoaiMon, TenMon, Gia, SoLuong) VALUES
('MN01', 'LM01', N'Trà Sữa Trân Châu', 25000.0, 100),
('MN02', 'LM02', N'	Cà Phê Đen', 20000.0, 50),
('MN03', 'LM03', N'	Nước Ép Cam', 	30000.0, 70);

-- Insert sample data into ChiTietDonHang table
INSERT INTO ChiTietDonHang (MaMon, MaDonHang, SoLuongMon, TongPhu) VALUES
('MN01', '1', 2, 50000.0),
('MN02', '2', 3, 60000.0),
('MN03', '3', 3, 90000.0);

-- Insert sample data into Ban table
INSERT INTO Ban (TenBan) VALUES
(N'Bàn 5'),
(N'Bàn 6'),
(N'Bàn 3'),
(N'Bàn 4');

-- Validate data after insertion
SELECT * FROM dbo.ChiTietDonHang;
SELECT * FROM dbo.Ban;
