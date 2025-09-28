-- =============================================
-- SCRIPT TAO KHACH HANG KIM CUONG VA TEST VOUCHER
-- Database: QuanLyKhachHang  
-- Mo ta: Tao du lieu test de kiem tra voucher kim cuong
-- =============================================

USE QuanLyKhachHang;
GO

PRINT '=============================================';
PRINT 'TAO KHACH HANG KIM CUONG DE TEST';
PRINT '=============================================';

-- 1. Tao khach hang test voi diem cao
IF NOT EXISTS (SELECT 1 FROM NGUOIDUNG WHERE MAND = 'KH_TEST')
BEGIN
    INSERT INTO NGUOIDUNG (MAND, HOTEN, NGAYSINH, GIOITINH, EMAIL, SODIENTHOAI, DIACHI, CMND) 
    VALUES ('KH_TEST', 'Khach Hang Test Kim Cuong', '1990-01-01', 'Nam', 'test@test.com', '0999999999', 'Dia chi test', '999999999');
    
    INSERT INTO TAIKHOAN (MAND, TENDANGNHAP, MATKHAU, VAITRO)
    VALUES ('KH_TEST', 'testdiamond', 'test123', 'KHACHHANG');
    
    INSERT INTO KHACHHANG (MAND, MAHANG, MAND_QL, DIEMTICHLUY)
    VALUES ('KH_TEST', 'DIAMOND', 'NV001', 2500);  -- 2500 diem = Kim cuong
    
    PRINT 'Da tao khach hang test: testdiamond / test123';
END
ELSE
BEGIN
    -- Cap nhat diem neu da ton tai
    UPDATE KHACHHANG SET DIEMTICHLUY = 2500, MAHANG = 'DIAMOND' WHERE MAND = 'KH_TEST';
    PRINT 'Da cap nhat khach hang test thanh Kim cuong';
END

-- 2. Kiem tra voucher V004
SELECT 'VOUCHER V004:' as INFO;
SELECT 
    MAVOUCHER,
    TENVOUCHER,
    GIATRIGIAM,
    LOAIGIAM,
    DIEMTOITHIEU,
    MAHANG_APDUNG,
    NGAYBATDAU,
    NGAYKETTHUC,
    TRANGTHAI,
    SOLUONG,
    DASUDUNG
FROM VOUCHER WHERE MAVOUCHER = 'V004';

-- 3. Neu voucher V004 khong ton tai, tao moi
IF NOT EXISTS (SELECT 1 FROM VOUCHER WHERE MAVOUCHER = 'V004')
BEGIN
    INSERT INTO VOUCHER (MAVOUCHER, TENVOUCHER, MOTA, GIATRIGIAM, LOAIGIAM, NGAYBATDAU, NGAYKETTHUC, SOLUONG, DIEMTOITHIEU, MAHANG_APDUNG)
    VALUES ('V004', 'Giam 20% cho thanh vien kim cuong', 'Voucher cao cap nhat', 20, 'Phan tram', '2024-01-01', '2024-12-31', 100, 2000, 'DIAMOND');
    PRINT 'Da tao voucher V004';
END
ELSE
BEGIN
    -- Reset voucher neu can
    UPDATE VOUCHER SET 
        TRANGTHAI = 1,
        DASUDUNG = 0,
        NGAYKETTHUC = '2024-12-31'
    WHERE MAVOUCHER = 'V004';
    PRINT 'Da reset voucher V004';
END

-- 4. Test voucher logic truc tiep
PRINT '=============================================';
PRINT 'TEST VOUCHER LOGIC:';

-- Kiem tra dieu kien voucher cho khach hang test
SELECT 
    'KIEM TRA DIEU KIEN:' as TEST_STEP,
    k.MAND as KHACH_HANG,
    k.DIEMTICHLUY as DIEM_KHACH_HANG,
    k.MAHANG as HANG_KHACH_HANG,
    v.MAVOUCHER,
    v.DIEMTOITHIEU as DIEM_TOI_THIEU,
    v.MAHANG_APDUNG as HANG_YEU_CAU,
    CASE WHEN k.DIEMTICHLUY >= v.DIEMTOITHIEU THEN 'DU DIEM' ELSE 'KHONG DU DIEM' END as KIEM_TRA_DIEM,
    CASE WHEN v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = k.MAHANG THEN 'DUNG HANG' ELSE 'SAI HANG' END as KIEM_TRA_HANG,
    CASE WHEN GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC THEN 'CON HAN' ELSE 'HET HAN' END as KIEM_TRA_HAN
FROM KHACHHANG k
CROSS JOIN VOUCHER v
WHERE k.MAND = 'KH_TEST' AND v.MAVOUCHER = 'V004';

-- 5. Test tao hoa don voi voucher
DECLARE @TestInvoice VARCHAR(20) = 'TEST_DIAMOND_' + FORMAT(GETDATE(), 'yyyyMMddHHmmss');
DECLARE @TestAmount DECIMAL(18,2) = 1000000; -- 1 trieu VND

PRINT 'Tao hoa don test: ' + @TestInvoice;
PRINT 'Tong tien: 1,000,000 VND';
PRINT 'Voucher: V004 (20% cho kim cuong)';
PRINT 'Tien giam mong doi: 200,000 VND';
PRINT 'Thanh toan mong doi: 800,000 VND';

BEGIN TRY
    INSERT INTO HOADONCHITIET (MAHD, MAND_KH, TONGTIEN, MAVOUCHER, SOLUONG, TRANGTHAI, GHICHU)
    VALUES (@TestInvoice, 'KH_TEST', @TestAmount, 'V004', 1, 'Da thanh toan', 'Test voucher kim cuong');
    
    -- Kiem tra ket qua
    SELECT 
        'KET QUA TEST:' as RESULT,
        MAHD,
        TONGTIEN,
        MAVOUCHER,
        TIENTRUOCGIAM,
        TIENGIAM,
        THANHTIEN,
        CASE 
            WHEN TIENGIAM = 200000 THEN 'VOUCHER HOAT DONG DUNG'
            WHEN TIENGIAM = 0 THEN 'VOUCHER KHONG HOAT DONG' 
            ELSE 'VOUCHER HOAT DONG SAI: ' + CAST(TIENGIAM AS VARCHAR)
        END as DANH_GIA,
        GHICHU
    FROM HOADONCHITIET
    WHERE MAHD = @TestInvoice;
    
    -- Kiem tra lich su giao dich
    SELECT 
        'LICH SU GIAO DICH:' as LOG,
        MAGD,
        GHICHU,
        SOTIEN
    FROM LICHSUGIAODICH
    WHERE MAHD = @TestInvoice;
    
    PRINT 'TEST THANH CONG!';
    
END TRY
BEGIN CATCH
    PRINT 'LOI KHI TEST: ' + ERROR_MESSAGE();
END CATCH

-- 6. Cleanup (xoa du lieu test)
PRINT '=============================================';
PRINT 'CLEANUP DU LIEU TEST (OPTIONAL)';
-- DELETE FROM HOADONCHITIET WHERE MAHD LIKE 'TEST_DIAMOND_%';
-- DELETE FROM LICHSUGIAODICH WHERE MAHD LIKE 'TEST_DIAMOND_%';
-- DELETE FROM KHACHHANG WHERE MAND = 'KH_TEST';
-- DELETE FROM TAIKHOAN WHERE MAND = 'KH_TEST';  
-- DELETE FROM NGUOIDUNG WHERE MAND = 'KH_TEST';

SELECT 'Khach hang test: testdiamond / test123' as LOGIN_INFO;
SELECT 'Voucher test: V004 - 20% cho kim cuong' as VOUCHER_INFO;

PRINT '=============================================';
PRINT 'TEST HOAN THANH!';
PRINT 'DANG NHAP VAO UNG DUNG VAO TEST:';
PRINT 'Username: testdiamond';
PRINT 'Password: test123';
PRINT '=============================================';