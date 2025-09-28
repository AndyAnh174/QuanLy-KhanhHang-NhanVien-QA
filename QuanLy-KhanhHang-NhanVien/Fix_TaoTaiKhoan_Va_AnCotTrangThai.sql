-- =============================================
-- FIX LỖI TẠO TÀI KHOẢN VÀ BỎ CỘT TRẠNG THÁI
-- Database: QuanLyKhachHang
-- Mô tả: Fix lỗi INSERT statement và cập nhật stored procedures
-- =============================================

USE QuanLyKhachHang;
GO

PRINT '=============================================';
PRINT 'FIX LỖI TẠO TÀI KHOẢN VÀ CẬP NHẬT STORED PROCEDURES';
PRINT '=============================================';

-- 1. Cập nhật stored procedure tìm kiếm khách hàng
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_TimKiemKhachHang')
    DROP PROCEDURE SP_TimKiemKhachHang;
GO

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
    WHERE k.TRANGTHAI = 1 AND n.TRANGTHAI = 1  -- CHỈ LẤY KHÁCH HÀNG HOẠT ĐỘNG
    AND (@Keyword IS NULL OR n.HOTEN LIKE '%' + @Keyword + '%' 
         OR n.EMAIL LIKE '%' + @Keyword + '%' 
         OR n.SODIENTHOAI LIKE '%' + @Keyword + '%')
    AND (@Mahang IS NULL OR k.MAHANG = @Mahang)
    AND (@Mand_QL IS NULL OR k.MAND_QL = @Mand_QL)
    ORDER BY n.HOTEN;
END;
GO

-- 2. Cập nhật stored procedure đăng nhập để đảm bảo chỉ tài khoản hoạt động
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_DangNhap')
    DROP PROCEDURE SP_DangNhap;
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
    AND t.TRANGTHAI = 1    -- CHỈ TÀI KHOẢN HOẠT ĐỘNG
    AND n.TRANGTHAI = 1;   -- CHỈ NGƯỜI DÙNG HOẠT ĐỘNG
END;
GO

-- 3. Tạo stored procedure thêm khách hàng mới (để đảm bảo tính toàn vẹn)
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_ThemKhachHang')
    DROP PROCEDURE SP_ThemKhachHang;
GO

CREATE PROCEDURE SP_ThemKhachHang
    @MaND VARCHAR(20),
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(10),
    @Email VARCHAR(100),
    @SoDienThoai VARCHAR(15),
    @DiaChi NVARCHAR(255),
    @CMND VARCHAR(20),
    @TenDangNhap VARCHAR(50),
    @MatKhau VARCHAR(255),
    @MaNVQL VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Kiểm tra tên đăng nhập đã tồn tại chưa
        IF EXISTS (SELECT 1 FROM TAIKHOAN WHERE TENDANGNHAP = @TenDangNhap)
        BEGIN
            RAISERROR('Tên đăng nhập đã tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra số điện thoại đã tồn tại chưa
        IF EXISTS (SELECT 1 FROM NGUOIDUNG WHERE SODIENTHOAI = @SoDienThoai)
        BEGIN
            RAISERROR('Số điện thoại đã tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Thêm người dùng
        INSERT INTO NGUOIDUNG (MAND, HOTEN, NGAYSINH, GIOITINH, EMAIL, SODIENTHOAI, DIACHI, CMND, TRANGTHAI)
        VALUES (@MaND, @HoTen, @NgaySinh, @GioiTinh, @Email, @SoDienThoai, @DiaChi, @CMND, 1);
        
        -- Thêm tài khoản
        INSERT INTO TAIKHOAN (MAND, TENDANGNHAP, MATKHAU, VAITRO, TRANGTHAI)
        VALUES (@MaND, @TenDangNhap, @MatKhau, 'KHACHHANG', 1);
        
        -- Thêm khách hàng
        INSERT INTO KHACHHANG (MAND, MAHANG, MAND_QL, DIEMTICHLUY, TRANGTHAI)
        VALUES (@MaND, 'BRONZE', @MaNVQL, 0, 1);
        
        COMMIT TRANSACTION;
        SELECT 'Thêm khách hàng thành công!' AS KETQUA;
        
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT ERROR_MESSAGE() AS KETQUA;
    END CATCH
END;
GO

-- 4. Kiểm tra và sửa lỗi ràng buộc (nếu có)
-- Đảm bảo các ràng buộc không gây xung đột với việc thêm dữ liệu

-- Kiểm tra các bản ghi có TRANGTHAI = NULL và sửa
UPDATE NGUOIDUNG SET TRANGTHAI = 1 WHERE TRANGTHAI IS NULL;
UPDATE TAIKHOAN SET TRANGTHAI = 1 WHERE TRANGTHAI IS NULL;
UPDATE KHACHHANG SET TRANGTHAI = 1 WHERE TRANGTHAI IS NULL;

-- 5. Tạo view để dễ dàng quản lý (chỉ hiển thị khách hàng hoạt động)
IF EXISTS (SELECT * FROM sys.views WHERE name = 'VW_KhachHangHoatDong')
    DROP VIEW VW_KhachHangHoatDong;
GO

CREATE VIEW VW_KhachHangHoatDong AS
SELECT 
    k.MAND,
    n.HOTEN,
    n.EMAIL,
    n.SODIENTHOAI,
    n.DIACHI,
    n.NGAYSINH,
    n.GIOITINH,
    h.TENHANG AS HANG_THANH_VIEN,
    h.UUDI,
    k.DIEMTICHLUY,
    nv_nd.HOTEN AS TEN_QUAN_LY,
    k.NGAYDANGKY
FROM KHACHHANG k
INNER JOIN NGUOIDUNG n ON k.MAND = n.MAND
LEFT JOIN HANGTHANHVIEN h ON k.MAHANG = h.MAHANG
LEFT JOIN NHANVIEN nv ON k.MAND_QL = nv.MAND
LEFT JOIN NGUOIDUNG nv_nd ON nv.MAND = nv_nd.MAND
WHERE k.TRANGTHAI = 1 AND n.TRANGTHAI = 1;
GO

-- 6. Thống kê dữ liệu sau khi sửa
PRINT '=============================================';
PRINT 'THỐNG KÊ DỮ LIỆU SAU KHI SỬA:';

SELECT 'NGƯỜI DÙNG' as BANG, 
       COUNT(*) as TONG_SO, 
       SUM(CASE WHEN TRANGTHAI = 1 THEN 1 ELSE 0 END) as HOAT_DONG,
       SUM(CASE WHEN TRANGTHAI = 0 THEN 1 ELSE 0 END) as NGUNG_HOAT_DONG
FROM NGUOIDUNG
UNION ALL
SELECT 'TÀI KHOẢN' as BANG, 
       COUNT(*) as TONG_SO, 
       SUM(CASE WHEN TRANGTHAI = 1 THEN 1 ELSE 0 END) as HOAT_DONG,
       SUM(CASE WHEN TRANGTHAI = 0 THEN 1 ELSE 0 END) as NGUNG_HOAT_DONG
FROM TAIKHOAN
UNION ALL
SELECT 'KHÁCH HÀNG' as BANG, 
       COUNT(*) as TONG_SO, 
       SUM(CASE WHEN TRANGTHAI = 1 THEN 1 ELSE 0 END) as HOAT_DONG,
       SUM(CASE WHEN TRANGTHAI = 0 THEN 1 ELSE 0 END) as NGUNG_HOAT_DONG
FROM KHACHHANG;

PRINT '=============================================';
PRINT 'SỬA LỖI HOÀN THÀNH!';
PRINT 'CÁC THAY ĐỔI:';
PRINT '1. ✅ Fixed INSERT statements trong FormThemKhachHang.cs';
PRINT '2. ✅ Ẩn cột TRANGTHAI trong DataGridView';
PRINT '3. ✅ Cập nhật stored procedures để chỉ lấy dữ liệu hoạt động';
PRINT '4. ✅ Thêm SP_ThemKhachHang để đảm bảo tính toàn vẹn';
PRINT '5. ✅ Tạo view VW_KhachHangHoatDong';
PRINT '6. ✅ Sửa các giá trị NULL trong TRANGTHAI';
PRINT '=============================================';

-- Test tạo khách hàng mới (optional)
/*
DECLARE @TestMaND VARCHAR(20) = 'TEST_' + FORMAT(GETDATE(), 'yyyyMMddHHmmss');
EXEC SP_ThemKhachHang 
    @MaND = @TestMaND,
    @HoTen = N'Khách Hàng Test',
    @NgaySinh = '1990-01-01',
    @GioiTinh = N'Nam',
    @Email = 'test@test.com',
    @SoDienThoai = '0987654321',
    @DiaChi = N'Địa chỉ test',
    @CMND = '123456789',
    @TenDangNhap = 'testuser',
    @MatKhau = 'test123',
    @MaNVQL = 'NV001';
*/