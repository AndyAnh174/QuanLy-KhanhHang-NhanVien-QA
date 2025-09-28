``
````````sql
-- =============================================
-- HỆ THỐNG QUẢN LÝ KHÁCH HÀNG
-- Database: QuanLyKhachHang
-- Tác giả: Generated from ERD
-- Ngày tạo: 2024
-- =============================================

-- Tạo database
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'QuanLyKhachHang')
    DROP DATABASE QuanLyKhachHang;
GO

CREATE DATABASE QuanLyKhachHang;
GO

USE QuanLyKhachHang;
GO

-- =============================================
-- TẠO CÁC BẢNG
-- =============================================

-- 1. Bảng NGUOIDUNG (Người dùng)
CREATE TABLE NGUOIDUNG (
    MAND VARCHAR(20) PRIMARY KEY,
    HOTEN NVARCHAR(100) NOT NULL,
    NGAYSINH DATE,
    GIOITINH NVARCHAR(10) CHECK (GIOITINH IN ('Nam', 'Nu', 'Khac')),
    EMAIL VARCHAR(100),
    SODIENTHOAI VARCHAR(15) NOT NULL UNIQUE,
    DIACHI NVARCHAR(255),
    CMND VARCHAR(20),
    NGAYTAO DATETIME DEFAULT GETDATE(),
    TRANGTHAI BIT DEFAULT 1 -- 1: Hoat dong, 0: Khong hoat dong
);
GO

-- 2. Bảng TAIKHOAN (Tài khoản)
CREATE TABLE TAIKHOAN (
    MAND VARCHAR(20) PRIMARY KEY,
    TENDANGNHAP VARCHAR(50) NOT NULL UNIQUE,
    MATKHAU VARCHAR(255) NOT NULL,
    VAITRO VARCHAR(50) NOT NULL CHECK (VAITRO IN ('ADMIN', 'NHANVIEN', 'KHACHHANG')),
    NGAYTAO DATETIME DEFAULT GETDATE(),
    TRANGTHAI BIT DEFAULT 1,
    FOREIGN KEY (MAND) REFERENCES NGUOIDUNG(MAND) ON DELETE CASCADE
);
GO

-- 3. Bảng HANGTHANHVIEN (Hạng thành viên)
CREATE TABLE HANGTHANHVIEN (
    MAHANG VARCHAR(10) PRIMARY KEY,
    TENHANG NVARCHAR(50) NOT NULL,
    MOTA NVARCHAR(255),
    DIEMTOITHIEU INT NOT NULL DEFAULT 0,
    UUDI NVARCHAR(255), -- Mo ta uu dai cua hang
    NGAYTAO DATETIME DEFAULT GETDATE()
);
GO

-- 4. Bảng NHANVIEN (Nhân viên)
CREATE TABLE NHANVIEN (
    MAND VARCHAR(20) PRIMARY KEY,
    MANHANVIEN VARCHAR(20) NOT NULL UNIQUE, -- Ma nhan vien rieng
    CHUCVU NVARCHAR(50),
    PHONGBAN NVARCHAR(50),
    NGAYVAOLAM DATE,
    LUONGCOBAN DECIMAL(18,2),
    TRANGTHAI BIT DEFAULT 1,
    FOREIGN KEY (MAND) REFERENCES NGUOIDUNG(MAND) ON DELETE CASCADE
);
GO

-- 5. Bảng KHACHHANG (Khách hàng)
CREATE TABLE KHACHHANG (
    MAND VARCHAR(20) PRIMARY KEY,
    MAHANG VARCHAR(10),
    MAND_QL VARCHAR(20), -- Ma nhan vien quan ly
    DIEMTICHLUY INT DEFAULT 0,
    NGAYDANGKY DATE DEFAULT CAST(GETDATE() AS DATE),
    TRANGTHAI BIT DEFAULT 1,
    FOREIGN KEY (MAND) REFERENCES NGUOIDUNG(MAND) ON DELETE CASCADE,
    FOREIGN KEY (MAHANG) REFERENCES HANGTHANHVIEN(MAHANG),
    FOREIGN KEY (MAND_QL) REFERENCES NHANVIEN(MAND)
);
GO

-- 6. Bảng CONGVIEC (Công việc)
CREATE TABLE CONGVIEC (
    MACONGVIEC VARCHAR(20) PRIMARY KEY,
    MAND_NV VARCHAR(20) NOT NULL,
    TIEUDECONGVIEC NVARCHAR(200) NOT NULL,
    MOTACONGVIEC NVARCHAR(500),
    TRANGTHAICONGVIEC NVARCHAR(50) DEFAULT 'Chua bat dau' 
        CHECK (TRANGTHAICONGVIEC IN ('Chua bat dau', 'Dang thuc hien', 'Hoan thanh', 'Huy bo')),
    NGAYBATDAU DATETIME,
    NGAYTHUCHIEN DATETIME,
    NGAYHETHAN DATETIME,
    UUTIEN NVARCHAR(20) DEFAULT 'Trung binh' 
        CHECK (UUTIEN IN ('Thap', 'Trung binh', 'Cao', 'Khan cap')),
    NGAYTAO DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MAND_NV) REFERENCES NHANVIEN(MAND)
);
GO

-- 7. Bảng TUONGTACKHACHHANG (Tương tác khách hàng)
CREATE TABLE TUONGTACKHACHHANG (
    MATT INT IDENTITY(1,1) PRIMARY KEY,
    MAND_KH VARCHAR(20) NOT NULL,
    MAND_NV VARCHAR(20) NOT NULL,
    MAGD_FK INT, -- Ma giao dich lien quan
    NGAYTUONGTAC DATETIME NOT NULL DEFAULT GETDATE(),
    HINHTHUC NVARCHAR(50) NOT NULL 
        CHECK (HINHTHUC IN ('Dien thoai', 'Email', 'Truc tiep', 'Chat', 'Khac')),
    NOIDUNG TEXT,
    KETQUA NVARCHAR(500), -- Ket qua tuong tac
    TRANGTHAI NVARCHAR(20) DEFAULT 'Chua xu ly' 
        CHECK (TRANGTHAI IN ('Chua xu ly', 'Dang xu ly', 'Da xu ly', 'Huy bo')),
    FOREIGN KEY (MAND_KH) REFERENCES KHACHHANG(MAND),
    FOREIGN KEY (MAND_NV) REFERENCES NHANVIEN(MAND)
);
GO

-- 8. Bảng VOUCHER
CREATE TABLE VOUCHER (
    MAVOUCHER VARCHAR(50) PRIMARY KEY,
    TENVOUCHER NVARCHAR(100) NOT NULL,
    MOTA NVARCHAR(255),
    GIATRIGIAM DECIMAL(18,2) NOT NULL DEFAULT 0,
    LOAIGIAM NVARCHAR(20) DEFAULT 'Tien mat' 
        CHECK (LOAIGIAM IN ('Tien mat', 'Phan tram')),
    NGAYBATDAU DATE NOT NULL,
    NGAYKETTHUC DATE NOT NULL,
    MATT_FK INT, -- Ma tuong tac phat sinh voucher
    SOLUONG INT DEFAULT 1, -- So luong voucher
    DASUDUNG INT DEFAULT 0, -- So luong da su dung
    DIEMTOITHIEU INT DEFAULT 0, -- Diem toi thieu de su dung
    MAHANG_APDUNG VARCHAR(10), -- Hang thanh vien ap dung
    TRANGTHAI BIT DEFAULT 1,
    NGAYTAO DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MATT_FK) REFERENCES TUONGTACKHACHHANG(MATT),
    FOREIGN KEY (MAHANG_APDUNG) REFERENCES HANGTHANHVIEN(MAHANG),
    CONSTRAINT CHK_VOUCHER_NGAY CHECK (NGAYBATDAU <= NGAYKETTHUC),
    CONSTRAINT CHK_VOUCHER_GIATRI CHECK (GIATRIGIAM >= 0)
);
GO

-- 9. Bảng HOADONCHITIET (Hóa đơn chi tiết)
CREATE TABLE HOADONCHITIET (
    MAHD VARCHAR(20) PRIMARY KEY,
    MAND_KH VARCHAR(20) NOT NULL,
    NGAYTHANHTOAN DATETIME NOT NULL DEFAULT GETDATE(),
    TONGTIEN DECIMAL(18,2) NOT NULL DEFAULT 0,
    MAVOUCHER VARCHAR(50),
    SOLUONG INT DEFAULT 1,
    TIENTRUOCGIAM DECIMAL(18,2), -- Tien truoc khi giam
    TIENGIAM DECIMAL(18,2) DEFAULT 0, -- Tien duoc giam
    THANHTIEN DECIMAL(18,2), -- Tien cuoi cùng phai tra
    PHUONGTHUCTHANHTOAN NVARCHAR(50) DEFAULT 'Tien mat' 
        CHECK (PHUONGTHUCTHANHTOAN IN ('Tien mat', 'Chuyen khoan', 'The', 'Khac')),
    TRANGTHAI NVARCHAR(20) DEFAULT 'Chua thanh toan' 
        CHECK (TRANGTHAI IN ('Chua thanh toan', 'Da thanh toan', 'Huy bo', 'Hoan tra')),
    GHICHU NVARCHAR(500),
    FOREIGN KEY (MAND_KH) REFERENCES KHACHHANG(MAND),
    FOREIGN KEY (MAVOUCHER) REFERENCES VOUCHER(MAVOUCHER),
    CONSTRAINT CHK_HOADON_TONGTIEN CHECK (TONGTIEN >= 0)
);
GO

-- 10. Bảng LICHSUGIAODICH (Lịch sử giao dịch)
CREATE TABLE LICHSUGIAODICH (
    MAGD INT IDENTITY(1,1) PRIMARY KEY,
    MAKH VARCHAR(20),
    MAHD VARCHAR(20),
    MACONGVIEC VARCHAR(20),
    NGAYGD DATETIME NOT NULL DEFAULT GETDATE(),
    LOAIGD NVARCHAR(50) NOT NULL 
        CHECK (LOAIGD IN ('Thanh toan', 'Tao voucher', 'Tuong tac', 'Cap nhat thong tin', 'Khac')),
    GHICHU NVARCHAR(500),
    SOTIEN DECIMAL(18,2) DEFAULT 0,
    FOREIGN KEY (MAKH) REFERENCES KHACHHANG(MAND),
    FOREIGN KEY (MAHD) REFERENCES HOADONCHITIET(MAHD),
    FOREIGN KEY (MACONGVIEC) REFERENCES CONGVIEC(MACONGVIEC)
);
GO

-- =============================================
-- TẠO CÁC INDEX ĐỂ TỐI ƯU HIỆU SUẤT
-- =============================================

-- Index cho bảng NGUOIDUNG
CREATE INDEX IX_NGUOIDUNG_SODIENTHOAI ON NGUOIDUNG(SODIENTHOAI);
CREATE INDEX IX_NGUOIDUNG_EMAIL ON NGUOIDUNG(EMAIL);
CREATE INDEX IX_NGUOIDUNG_HOTEN ON NGUOIDUNG(HOTEN);

-- Index cho bảng TAIKHOAN
CREATE INDEX IX_TAIKHOAN_TENDANGNHAP ON TAIKHOAN(TENDANGNHAP);
CREATE INDEX IX_TAIKHOAN_VAITRO ON TAIKHOAN(VAITRO);

-- Index cho bảng KHACHHANG
CREATE INDEX IX_KHACHHANG_MAHANG ON KHACHHANG(MAHANG);
CREATE INDEX IX_KHACHHANG_MAND_QL ON KHACHHANG(MAND_QL);
CREATE INDEX IX_KHACHHANG_DIEMTICHLUY ON KHACHHANG(DIEMTICHLUY);

-- Index cho bảng CONGVIEC
CREATE INDEX IX_CONGVIEC_MAND_NV ON CONGVIEC(MAND_NV);
CREATE INDEX IX_CONGVIEC_TRANGTHAI ON CONGVIEC(TRANGTHAICONGVIEC);
CREATE INDEX IX_CONGVIEC_NGAYTHUCHIEN ON CONGVIEC(NGAYTHUCHIEN);

-- Index cho bảng TUONGTACKHACHHANG
CREATE INDEX IX_TUONGTACKHACHHANG_MAND_KH ON TUONGTACKHACHHANG(MAND_KH);
CREATE INDEX IX_TUONGTACKHACHHANG_MAND_NV ON TUONGTACKHACHHANG(MAND_NV);
CREATE INDEX IX_TUONGTACKHACHHANG_NGAYTUONGTAC ON TUONGTACKHACHHANG(NGAYTUONGTAC);

-- Index cho bảng VOUCHER
CREATE INDEX IX_VOUCHER_NGAYBATDAU ON VOUCHER(NGAYBATDAU);
CREATE INDEX IX_VOUCHER_NGAYKETTHUC ON VOUCHER(NGAYKETTHUC);
CREATE INDEX IX_VOUCHER_TRANGTHAI ON VOUCHER(TRANGTHAI);

-- Index cho bảng HOADONCHITIET
CREATE INDEX IX_HOADONCHITIET_MAND_KH ON HOADONCHITIET(MAND_KH);
CREATE INDEX IX_HOADONCHITIET_NGAYTHANHTOAN ON HOADONCHITIET(NGAYTHANHTOAN);
CREATE INDEX IX_HOADONCHITIET_TRANGTHAI ON HOADONCHITIET(TRANGTHAI);

-- Index cho bảng LICHSUGIAODICH
CREATE INDEX IX_LICHSUGIAODICH_MAKH ON LICHSUGIAODICH(MAKH);
CREATE INDEX IX_LICHSUGIAODICH_MAHD ON LICHSUGIAODICH(MAHD);
CREATE INDEX IX_LICHSUGIAODICH_NGAYGD ON LICHSUGIAODICH(NGAYGD);

-- =============================================
-- TẠO CÁC TRIGGER
-- =============================================

-- Trigger tự động cập nhật điểm tích lũy khi có giao dịch
GO
CREATE TRIGGER TR_UpdateDiemTichLuy
ON HOADONCHITIET
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE k
    SET DIEMTICHLUY = k.DIEMTICHLUY + CAST(i.THANHTIEN * 0.001 AS INT)
    FROM KHACHHANG k
    INNER JOIN inserted i ON k.MAND = i.MAND_KH
    WHERE i.TRANGTHAI = 'Da thanh toan';
    
    -- Cập nhật hạng thành viên dựa trên điểm tích lũy
    UPDATE k
    SET MAHANG = h.MAHANG
    FROM KHACHHANG k
    INNER JOIN HANGTHANHVIEN h ON k.DIEMTICHLUY >= h.DIEMTOITHIEU
    WHERE k.DIEMTICHLUY >= h.DIEMTOITHIEU
    AND h.DIEMTOITHIEU = (
        SELECT MAX(DIEMTOITHIEU) 
        FROM HANGTHANHVIEN h2 
        WHERE k.DIEMTICHLUY >= h2.DIEMTOITHIEU
    );
END;
GO

-- Trigger tự động tạo lịch sử giao dịch khi có hóa đơn
CREATE TRIGGER TR_CreateLichSuGiaoDich
ON HOADONCHITIET
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO LICHSUGIAODICH (MAKH, MAHD, NGAYGD, LOAIGD, GHICHU, SOTIEN)
    SELECT 
        i.MAND_KH,
        i.MAHD,
        i.NGAYTHANHTOAN,
        'Thanh toan',
        'Hoa don: ' + i.MAHD + CASE WHEN i.MAVOUCHER IS NOT NULL THEN ' - Su dung voucher: ' + i.MAVOUCHER ELSE '' END,
        i.THANHTIEN
    FROM inserted i;
END;
GO

-- Trigger tự động tính toán tiền giảm và thành tiền
GO
CREATE TRIGGER TR_CalculateInvoiceAmount
ON HOADONCHITIET
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Thực hiện INSERT với tính toán tiền giảm
    INSERT INTO HOADONCHITIET (
        MAHD, MAND_KH, NGAYTHANHTOAN, TONGTIEN, MAVOUCHER, SOLUONG, 
        TIENTRUOCGIAM, TIENGIAM, THANHTIEN, PHUONGTHUCTHANHTOAN, 
        TRANGTHAI, GHICHU
    )
    SELECT 
        i.MAHD,
        i.MAND_KH,
        i.NGAYTHANHTOAN,
        i.TONGTIEN,
        i.MAVOUCHER,
        i.SOLUONG,
        i.TONGTIEN AS TIENTRUOCGIAM,
        CASE 
            WHEN v.MAVOUCHER IS NOT NULL AND v.TRANGTHAI = 1 
                 AND GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC
                 AND (v.DASUDUNG < v.SOLUONG OR v.SOLUONG = 0)
            THEN CASE 
                WHEN v.LOAIGIAM = 'Phan tram' THEN i.TONGTIEN * v.GIATRIGIAM / 100
                ELSE v.GIATRIGIAM
            END
            ELSE 0
        END AS TIENGIAM,
        i.TONGTIEN - CASE 
            WHEN v.MAVOUCHER IS NOT NULL AND v.TRANGTHAI = 1 
                 AND GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC
                 AND (v.DASUDUNG < v.SOLUONG OR v.SOLUONG = 0)
            THEN CASE 
                WHEN v.LOAIGIAM = 'Phan tram' THEN i.TONGTIEN * v.GIATRIGIAM / 100
                ELSE v.GIATRIGIAM
            END
            ELSE 0
        END AS THANHTIEN,
        ISNULL(i.PHUONGTHUCTHANHTOAN, 'Tien mat'),
        ISNULL(i.TRANGTHAI, 'Chua thanh toan'),
        i.GHICHU
    FROM inserted i
    LEFT JOIN VOUCHER v ON i.MAVOUCHER = v.MAVOUCHER;
END;
GO

-- =============================================
-- THÊM DỮ LIỆU MẪU
-- =============================================

-- Them hang thanh vien (CAP NHAT MUC DIEM MOI)
INSERT INTO HANGTHANHVIEN (MAHANG, TENHANG, MOTA, DIEMTOITHIEU, UUDI) VALUES
('BRONZE', 'Dong', 'Hang thanh vien co ban', 0, 'Khong co uu dai dac biet'),
('SILVER', 'Bac', 'Hang thanh vien bac', 750, 'Giam gia 5% cho tat ca san pham'),
('GOLD', 'Vang', 'Hang thanh vien vang', 1500, 'Giam gia 10% + mien phi van chuyen'),
('DIAMOND', 'Kim cuong', 'Hang thanh vien cao cap nhat', 2000, 'Giam gia 15% + uu dai dac biet');

-- Them nguoi dung mau
INSERT INTO NGUOIDUNG (MAND, HOTEN, NGAYSINH, GIOITINH, EMAIL, SODIENTHOAI, DIACHI, CMND) VALUES
('AD001', 'Nguyen Van Admin', '1985-01-15', 'Nam', 'admin@company.com', '0901234567', '123 Duong ABC, Quan 1, TP.HCM', '123456789'),
('NV001', 'Tran Thi Nhan Vien', '1990-05-20', 'Nu', 'nhanvien1@company.com', '0901234568', '456 Duong DEF, Quan 2, TP.HCM', '123456790'),
('NV002', 'Le Van Quan Ly', '1988-03-10', 'Nam', 'quanly@company.com', '0901234569', '789 Duong GHI, Quan 3, TP.HCM', '123456791'),
('KH001', 'Pham Thi Khach Hang', '1995-07-25', 'Nu', 'khachhang1@gmail.com', '0901234570', '321 Duong JKL, Quan 4, TP.HCM', '123456792'),
('KH002', 'Hoang Van Mua Hang', '1992-11-12', 'Nam', 'muahang@gmail.com', '0901234571', '654 Duong MNO, Quan 5, TP.HCM', '123456793'),
('KH003', 'Vo Thi Thuong Xuyen', '1987-09-08', 'Nu', 'thuongxuyen@gmail.com', '0901234572', '987 Duong PQR, Quan 6, TP.HCM', '123456794');

-- Thêm tài khoản
INSERT INTO TAIKHOAN (MAND, TENDANGNHAP, MATKHAU, VAITRO) VALUES
('AD001', 'admin', 'admin123', 'ADMIN'),
('NV001', 'nhanvien1', 'nv123', 'NHANVIEN'),
('NV002', 'quanly', 'ql123', 'NHANVIEN'),
('KH001', 'khachhang1', 'kh123', 'KHACHHANG'),
('KH002', 'muahang', 'mh123', 'KHACHHANG'),
('KH003', 'thuongxuyen', 'tx123', 'KHACHHANG');

-- Them nhan vien
INSERT INTO NHANVIEN (MAND, MANHANVIEN, CHUCVU, PHONGBAN, NGAYVAOLAM, LUONGCOBAN) VALUES
('NV001', 'NV001', 'Nhan vien tu van', 'Kinh doanh', '2020-01-15', 8000000),
('NV002', 'NV002', 'Truong phong', 'Kinh doanh', '2018-06-01', 15000000);

-- Thêm khách hàng (CAP NHAT LAI DIEM DE PHU HOP VOI MUC MOI)
INSERT INTO KHACHHANG (MAND, MAHANG, MAND_QL, DIEMTICHLUY) VALUES
('KH001', 'BRONZE', 'NV001', 250),     -- 250 diem - hang Dong
('KH002', 'BRONZE', 'NV001', 800),     -- 800 diem - hang Bac  
('KH003', 'SILVER', 'NV002', 1600);    -- 1600 diem - hang Vang

-- Them cong viec
INSERT INTO CONGVIEC (MACONGVIEC, MAND_NV, TIEUDECONGVIEC, MOTACONGVIEC, TRANGTHAICONGVIEC, NGAYBATDAU, NGAYHETHAN, UUTIEN) VALUES
('CV001', 'NV001', 'Tu van khach hang moi', 'Lien he va tu van cho khach hang moi dang ky', 'Dang thuc hien', '2024-01-01', '2024-01-31', 'Cao'),
('CV002', 'NV002', 'Quan ly doi ngu', 'Giam sat va ho tro nhan vien trong phong', 'Hoan thanh', '2023-12-01', '2023-12-31', 'Trung binh'),
('CV003', 'NV001', 'Cham soc khach hang VIP', 'Cham soc dac biet cho khach hang hang vang va kim cuong', 'Chua bat dau', '2024-02-01', '2024-02-28', 'Cao');

-- Them tuong tac khach hang
INSERT INTO TUONGTACKHACHHANG (MAND_KH, MAND_NV, NGAYTUONGTAC, HINHTHUC, NOIDUNG, KETQUA, TRANGTHAI) VALUES
('KH001', 'NV001', '2024-01-15 09:30:00', 'Dien thoai', 'Khach hang hoi ve san pham moi', 'Da tu van chi tiet va gui catalog', 'Da xu ly'),
('KH002', 'NV001', '2024-01-16 14:20:00', 'Truc tiep', 'Khach hang den cua hang xem san pham', 'Da demo san pham va bao gia', 'Da xu ly'),
('KH003', 'NV002', '2024-01-17 10:15:00', 'Email', 'Khach hang yeu cau ho tro ky thuat', 'Da chuyen cho bo phan ky thuat', 'Dang xu ly');

-- Them voucher (CAP NHAT MUC DIEM TOI THIEU)
INSERT INTO VOUCHER (MAVOUCHER, TENVOUCHER, MOTA, GIATRIGIAM, LOAIGIAM, NGAYBATDAU, NGAYKETTHUC, SOLUONG, DIEMTOITHIEU, MAHANG_APDUNG) VALUES
('V001', 'Giam gia 10% cho khach hang moi', 'Ap dung cho lan mua dau tien', 10, 'Phan tram', '2024-01-01', '2024-12-31', 100, 0, 'BRONZE'),
('V002', 'Giam 50,000 VND cho thanh vien bac', 'Voucher danh cho thanh vien bac', 50000, 'Tien mat', '2024-01-01', '2024-06-30', 50, 750, 'SILVER'),
('V003', 'Giam 15% cho thanh vien vang', 'Danh rieng cho khach hang hang vang', 15, 'Phan tram', '2024-01-01', '2024-12-31', 30, 1500, 'GOLD'),
('V004', 'Giam 20% cho thanh vien kim cuong', 'Voucher cao cap nhat', 20, 'Phan tram', '2024-01-01', '2024-12-31', 20, 2000, 'DIAMOND');

-- Them hoa don chi tiet
INSERT INTO HOADONCHITIET (MAHD, MAND_KH, NGAYTHANHTOAN, TONGTIEN, MAVOUCHER, SOLUONG, TRANGTHAI, GHICHU) VALUES
('HD001', 'KH001', '2024-01-15 15:30:00', 1500000, 'V001', 1, 'Da thanh toan', 'Don hang dau tien'),
('HD002', 'KH002', '2024-01-16 16:45:00', 2500000, 'V002', 2, 'Da thanh toan', 'Mua san pham cao cap'),
('HD003', 'KH003', '2024-01-17 11:20:00', 3000000, 'V003', 1, 'Da thanh toan', 'Khach hang VIP');

-- =============================================
-- TẠO CÁC STORED PROCEDURES
-- =============================================

-- SP: Dang nhap
GO
CREATE PROCEDURE SP_DangNhap
    @Tendangnhap VARCHAR(50),
    @Matkhau VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.MAND,
        n.HOTEN,
        t.VAITRO,
        n.EMAIL,
        n.SODIENTHOAI,
        t.TRANGTHAI
    FROM TAIKHOAN t
    INNER JOIN NGUOIDUNG n ON t.MAND = n.MAND
    WHERE t.TENDANGNHAP = @Tendangnhap 
    AND t.MATKHAU = @Matkhau
    AND t.TRANGTHAI = 1;
END;
GO

-- SP: Tim kiem khach hang
CREATE PROCEDURE SP_TimKiemKhachHang
    @Keyword NVARCHAR(100) = NULL,
    @Mahang VARCHAR(10) = NULL,
    @Mand_QL VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        k.MAND,
        n.HOTEN,
        n.EMAIL,
        n.SODIENTHOAI,
        n.DIACHI,
        h.TENHANG,
        nv_nd.HOTEN AS TEN_QUANLY,
        k.DIEMTICHLUY,
        k.NGAYDANGKY,
        k.TRANGTHAI
    FROM KHACHHANG k
    INNER JOIN NGUOIDUNG n ON k.MAND = n.MAND
    LEFT JOIN HANGTHANHVIEN h ON k.MAHANG = h.MAHANG
    LEFT JOIN NHANVIEN nv ON k.MAND_QL = nv.MAND
    LEFT JOIN NGUOIDUNG nv_nd ON nv.MAND = nv_nd.MAND
    WHERE k.TRANGTHAI = 1
    AND (@Keyword IS NULL OR n.HOTEN LIKE '%' + @Keyword + '%' 
         OR n.EMAIL LIKE '%' + @Keyword + '%' 
         OR n.SODIENTHOAI LIKE '%' + @Keyword + '%')
    AND (@Mahang IS NULL OR k.MAHANG = @Mahang)
    AND (@Mand_QL IS NULL OR k.MAND_QL = @Mand_QL)
    ORDER BY n.HOTEN;
END;
GO

-- SP: Tao hoa don moi
CREATE PROCEDURE SP_TaoHoaDon
    @Mahd VARCHAR(20),
    @Mand_KH VARCHAR(20),
    @Tongtien DECIMAL(18,2),
    @Mavoucher VARCHAR(50) = NULL,
    @Soluong INT = 1,
    @Ghichu NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Kiem tra voucher co hop le khong
        IF @Mavoucher IS NOT NULL
        BEGIN
            IF NOT EXISTS (
                SELECT 1 FROM VOUCHER 
                WHERE MAVOUCHER = @Mavoucher 
                AND TRANGTHAI = 1 
                AND GETDATE() BETWEEN NGAYBATDAU AND NGAYKETTHUC
                AND (DASUDUNG < SOLUONG OR SOLUONG = 0)
            )
            BEGIN
                RAISERROR('Voucher khong hop le hoac da het han', 16, 1);
                RETURN;
            END
        END
        
        -- Tao hoa don
        INSERT INTO HOADONCHITIET (MAHD, MAND_KH, TONGTIEN, MAVOUCHER, SOLUONG, GHICHU)
        VALUES (@Mahd, @Mand_KH, @Tongtien, @Mavoucher, @Soluong, @Ghichu);
        
        -- Cap nhat so luong da su dung cua voucher
        IF @Mavoucher IS NOT NULL
        BEGIN
            UPDATE VOUCHER 
            SET DASUDUNG = DASUDUNG + 1 
            WHERE MAVOUCHER = @Mavoucher;
        END
        
        COMMIT TRANSACTION;
        SELECT 'Tao hoa don thanh cong' AS KETQUA;
        
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT ERROR_MESSAGE() AS KETQUA;
    END CATCH
END;
GO

-- SP: Bao cao doanh thu theo ngay
CREATE PROCEDURE SP_BaoCaoDoanhThu
    @NgayBatDau DATE,
    @NgayKetThuc DATE
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CAST(NGAYTHANHTOAN AS DATE) AS NGAY,
        COUNT(*) AS SO_HOA_DON,
        SUM(THANHTIEN) AS TONG_DOANH_THU,
        AVG(THANHTIEN) AS DOANH_THU_TRUNG_BINH,
        SUM(TIENGIAM) AS TONG_TIEN_GIAM,
        COUNT(MAVOUCHER) AS SO_VOUCHER_SU_DUNG
    FROM HOADONCHITIET
    WHERE TRANGTHAI = 'Da thanh toan'
    AND CAST(NGAYTHANHTOAN AS DATE) BETWEEN @NgayBatDau AND @NgayKetThuc
    GROUP BY CAST(NGAYTHANHTOAN AS DATE)
    ORDER BY NGAY;
END;
GO

-- SP: Cap nhat hang thanh vien
CREATE PROCEDURE SP_CapNhatHangThanhVien
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE k
    SET MAHANG = h.MAHANG
    FROM KHACHHANG k
    INNER JOIN HANGTHANHVIEN h ON k.DIEMTICHLUY >= h.DIEMTOITHIEU
    WHERE k.DIEMTICHLUY >= h.DIEMTOITHIEU
    AND h.DIEMTOITHIEU = (
        SELECT MAX(DIEMTOITHIEU) 
        FROM HANGTHANHVIEN h2 
        WHERE k.DIEMTICHLUY >= h2.DIEMTOITHIEU
    );
    
    SELECT 'Cap nhat hang thanh vien thanh cong' AS KETQUA;
END;
GO

-- =============================================
-- TẠO CÁC VIEW
-- =============================================

-- View: Thong tin khach hang day du
CREATE VIEW VW_ThongTinKhachHang AS
SELECT 
    k.MAND,
    n.HOTEN,
    n.EMAIL,
    n.SODIENTHOAI,
    n.DIACHI,
    n.NGAYSINH,
    n.GIOITINH,
    h.TENHANG,
    h.UUDI,
    k.DIEMTICHLUY,
    nv_nd.HOTEN AS TEN_QUANLY,
    k.NGAYDANGKY,
    k.TRANGTHAI
FROM KHACHHANG k
INNER JOIN NGUOIDUNG n ON k.MAND = n.MAND
LEFT JOIN HANGTHANHVIEN h ON k.MAHANG = h.MAHANG
LEFT JOIN NHANVIEN nv ON k.MAND_QL = nv.MAND
LEFT JOIN NGUOIDUNG nv_nd ON nv.MAND = nv_nd.MAND;
GO

-- View: Bao cao tuong tac khach hang
CREATE VIEW VW_BaoCaoTuongTac AS
SELECT 
    t.MATT,
    k_nd.HOTEN AS TEN_KHACH_HANG,
    nv_nd.HOTEN AS TEN_NHAN_VIEN,
    t.NGAYTUONGTAC,
    t.HINHTHUC,
    t.NOIDUNG,
    t.KETQUA,
    t.TRANGTHAI
FROM TUONGTACKHACHHANG t
INNER JOIN KHACHHANG k ON t.MAND_KH = k.MAND
INNER JOIN NGUOIDUNG k_nd ON k.MAND = k_nd.MAND
INNER JOIN NHANVIEN nv ON t.MAND_NV = nv.MAND
INNER JOIN NGUOIDUNG nv_nd ON nv.MAND = nv_nd.MAND;
GO

-- View: Thong ke doanh thu
CREATE VIEW VW_ThongKeDoanhThu AS
SELECT 
    CAST(NGAYTHANHTOAN AS DATE) AS NGAY,
    COUNT(*) AS SO_HOA_DON,
    SUM(THANHTIEN) AS TONG_DOANH_THU,
    AVG(THANHTIEN) AS DOANH_THU_TRUNG_BINH,
    SUM(TIENGIAM) AS TONG_TIEN_GIAM,
    COUNT(MAVOUCHER) AS SO_VOUCHER_SU_DUNG
FROM HOADONCHITIET
WHERE TRANGTHAI = 'Da thanh toan'
GROUP BY CAST(NGAYTHANHTOAN AS DATE);
GO

-- =============================================
-- TẠO CÁC FUNCTION
-- =============================================

-- Function: Kiem tra voucher co hop le khong
CREATE FUNCTION FN_KiemTraVoucher(@Mavoucher VARCHAR(50))
RETURNS BIT
AS
BEGIN
    DECLARE @KetQua BIT = 0;
    
    IF EXISTS (
        SELECT 1 FROM VOUCHER 
        WHERE MAVOUCHER = @Mavoucher 
        AND TRANGTHAI = 1 
        AND GETDATE() BETWEEN NGAYBATDAU AND NGAYKETTHUC
        AND (DASUDUNG < SOLUONG OR SOLUONG = 0)
    )
    BEGIN
        SET @KetQua = 1;
    END
    
    RETURN @KetQua;
END;
GO

-- Function: Tinh tien giam tu voucher
CREATE FUNCTION FN_TinhTienGiam(@Mavoucher VARCHAR(50), @Tongtien DECIMAL(18,2))
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @TienGiam DECIMAL(18,2) = 0;
    
    SELECT @TienGiam = CASE 
        WHEN LOAIGIAM = 'Phan tram' THEN @Tongtien * GIATRIGIAM / 100
        ELSE GIATRIGIAM
    END
    FROM VOUCHER 
    WHERE MAVOUCHER = @Mavoucher 
    AND TRANGTHAI = 1 
    AND GETDATE() BETWEEN NGAYBATDAU AND NGAYKETTHUC
    AND (DASUDUNG < SOLUONG OR SOLUONG = 0);
    
    RETURN ISNULL(@TienGiam, 0);
END;
GO

-- =============================================
-- TẠO CÁC RULE VÀ CONSTRAINT BỔ SUNG
-- =============================================

-- Rang buoc: Email phai dung dinh dang
ALTER TABLE NGUOIDUNG
ADD CONSTRAINT CHK_EMAIL_FORMAT 
CHECK (EMAIL LIKE '%@%.%' OR EMAIL IS NULL);
GO

-- Rang buoc: So dien thoai phai co it nhat 9 chu so
ALTER TABLE NGUOIDUNG
ADD CONSTRAINT CHK_SODIENTHOAI_LENGTH 
CHECK (LEN(SODIENTHOAI) >= 9 AND SODIENTHOAI NOT LIKE '%[^0-9]%');
GO

-- Rang buoc: Ngay sinh khong duoc lon hon ngay hien tai
ALTER TABLE NGUOIDUNG
ADD CONSTRAINT CHK_NGAYSINH 
CHECK (NGAYSINH <= CAST(GETDATE() AS DATE));
GO

-- Rang buoc: Diem tich luy khong duoc am
ALTER TABLE KHACHHANG
ADD CONSTRAINT CHK_DIEMTICHLUY 
CHECK (DIEMTICHLUY >= 0);
GO

-- Rang buoc: Tong tien hoa don phai duong
ALTER TABLE HOADONCHITIET
ADD CONSTRAINT CHK_TONGTIEN_POSITIVE 
CHECK (TONGTIEN > 0);
GO

-- =============================================
-- TẠO CÁC ROLE VÀ PERMISSION
-- =============================================

-- Tao role cho cac nhom nguoi dung
CREATE ROLE db_admin;
CREATE ROLE db_nhanvien;
CREATE ROLE db_khachhang;

-- Phan quyen cho admin
GRANT CONTROL ON DATABASE::QuanLyKhachHang TO db_admin;

-- Phan quyen cho nhan vien
GRANT SELECT, INSERT, UPDATE ON NGUOIDUNG TO db_nhanvien;
GRANT SELECT, INSERT, UPDATE ON KHACHHANG TO db_nhanvien;
GRANT SELECT, INSERT, UPDATE ON TUONGTACKHACHHANG TO db_nhanvien;
GRANT SELECT, INSERT, UPDATE ON VOUCHER TO db_nhanvien;
GRANT SELECT, INSERT, UPDATE ON HOADONCHITIET TO db_nhanvien;
GRANT SELECT, INSERT, UPDATE ON LICHSUGIAODICH TO db_nhanvien;
GRANT SELECT ON HANGTHANHVIEN TO db_nhanvien;
GRANT SELECT ON NHANVIEN TO db_nhanvien;
GRANT SELECT ON CONGVIEC TO db_nhanvien;

-- Phan quyen cho khach hang
GRANT SELECT ON VW_ThongTinKhachHang TO db_khachhang;
GRANT SELECT ON VW_ThongKeDoanhThu TO db_khachhang;
GRANT SELECT ON HANGTHANHVIEN TO db_khachhang;
GRANT SELECT ON VOUCHER TO db_khachhang;

-- =============================================
-- TẠO CÁC JOB BACKUP (Tùy chọn)
-- =============================================

-- Tao job backup hang ngay (can SQL Server Agent)
/*
EXEC dbo.sp_add_job
    @job_name = N'Backup_QuanLyKhachHang_Daily';

EXEC dbo.sp_add_jobstep
    @job_name = N'Backup_QuanLyKhachHang_Daily',
    @step_name = N'Backup Database',
    @command = N'BACKUP DATABASE [QuanLyKhachHang] TO DISK = ''C:\Backup\QuanLyKhachHang_$(ESCAPE_SQUOTE(DATE))_$(ESCAPE_SQUOTE(TIME)).bak'' WITH FORMAT, INIT, NAME = ''QuanLyKhachHang-Full Database Backup'';';

EXEC dbo.sp_add_schedule
    @schedule_name = N'Daily_Backup_Schedule',
    @freq_type = 4,
    @freq_interval = 1,
    @active_start_time = 020000;

EXEC dbo.sp_attach_schedule
    @job_name = N'Backup_QuanLyKhachHang_Daily',
    @schedule_name = N'Daily_Backup_Schedule';

EXEC dbo.sp_add_jobserver
    @job_name = N'Backup_QuanLyKhachHang_Daily';
*/

-- =============================================
-- KẾT THÚC SCRIPT
-- =============================================

PRINT '=============================================';
PRINT 'HOAN THANH TAO DATABASE QUAN LY KHACH HANG';
PRINT '=============================================';
PRINT 'Database: QuanLyKhachHang';
PRINT 'So bang: 10';
PRINT 'So stored procedure: 5';
PRINT 'So view: 3';
PRINT 'So function: 2';
PRINT 'So trigger: 3';
PRINT '=============================================';
PRINT 'HANG THANH VIEN MOI:';
PRINT '- DONG: 0 diem';
PRINT '- BAC: 750 diem';
PRINT '- VANG: 1500 diem'; 
PRINT '- KIM CUONG: 2000 diem';
PRINT '=============================================';
PRINT 'Du lieu mau da duoc them vao cac bang';
PRINT 'Co the bat dau phat trien ung dung WinForms';
PRINT '=============================================';
