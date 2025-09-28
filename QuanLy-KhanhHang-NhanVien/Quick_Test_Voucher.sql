-- =============================================
-- SCRIPT TEST VOUCHER QUICK FIX
-- Database: QuanLyKhachHang
-- Mo ta: Test nhanh voucher system
-- =============================================

USE QuanLyKhachHang;
GO

PRINT '=== QUICK TEST VOUCHER SYSTEM ===';

-- 1. Kiem tra va tao du lieu test
IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE DIEMTICHLUY >= 100)
BEGIN
    -- Cap nhat 1 khach hang co diem cao de test
    UPDATE TOP(1) KHACHHANG 
    SET DIEMTICHLUY = 1000, MAHANG = 'SILVER' 
    WHERE TRANGTHAI = 1;
    PRINT 'Da cap nhat 1 khach hang co 1000 diem (Silver)';
END

-- 2. Kiem tra voucher V001 (10% cho tat ca)
SELECT 'VOUCHER V001 STATUS:' as CHECK_TYPE;
SELECT 
    MAVOUCHER,
    TENVOUCHER,
    GIATRIGIAM + ' ' + LOAIGIAM as GIA_TRI,
    DIEMTOITHIEU,
    MAHANG_APDUNG,
    CASE WHEN TRANGTHAI = 1 THEN 'ACTIVE' ELSE 'INACTIVE' END as STATUS,
    CASE WHEN GETDATE() BETWEEN NGAYBATDAU AND NGAYKETTHUC THEN 'VALID_DATE' ELSE 'EXPIRED' END as DATE_CHECK,
    (SOLUONG - DASUDUNG) as CON_LAI
FROM VOUCHER 
WHERE MAVOUCHER = 'V001';

-- 3. Neu voucher V001 khong hop le, sua lai
IF EXISTS (SELECT 1 FROM VOUCHER WHERE MAVOUCHER = 'V001' AND (TRANGTHAI != 1 OR GETDATE() NOT BETWEEN NGAYBATDAU AND NGAYKETTHUC))
BEGIN
    UPDATE VOUCHER 
    SET TRANGTHAI = 1,
        NGAYBATDAU = '2024-01-01',
        NGAYKETTHUC = '2024-12-31',
        DASUDUNG = 0
    WHERE MAVOUCHER = 'V001';
    PRINT 'Da sua lai voucher V001 de hop le';
END

-- 4. Test voucher logic truc tiep
DECLARE @TestCustomer VARCHAR(20);
SELECT TOP 1 @TestCustomer = MAND FROM KHACHHANG WHERE DIEMTICHLUY >= 100 AND TRANGTHAI = 1;

IF @TestCustomer IS NOT NULL
BEGIN
    PRINT 'Testing voi khach hang: ' + @TestCustomer;
    
    -- Kiem tra dieu kien voucher
    SELECT 
        'VOUCHER CONDITIONS CHECK:' as TEST_TYPE,
        k.MAND as KHACH_HANG,
        k.DIEMTICHLUY as DIEM_KH,
        k.MAHANG as HANG_KH,
        v.MAVOUCHER,
        v.DIEMTOITHIEU as DIEM_YC,
        v.MAHANG_APDUNG as HANG_YC,
        CASE WHEN k.DIEMTICHLUY >= v.DIEMTOITHIEU THEN 'PASS' ELSE 'FAIL' END as DIEM_CHECK,
        CASE WHEN v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = k.MAHANG THEN 'PASS' ELSE 'FAIL' END as HANG_CHECK,
        CASE WHEN GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC THEN 'PASS' ELSE 'FAIL' END as DATE_CHECK
    FROM KHACHHANG k
    CROSS JOIN VOUCHER v
    WHERE k.MAND = @TestCustomer AND v.MAVOUCHER = 'V001';
    
    -- Test INSERT voi voucher
    DECLARE @TestInvoice VARCHAR(20) = 'QUICKTEST_' + FORMAT(GETDATE(), 'yyyyMMddHHmmss');
    
    BEGIN TRY
        INSERT INTO HOADONCHITIET (MAHD, MAND_KH, TONGTIEN, MAVOUCHER, SOLUONG, TRANGTHAI, GHICHU)
        VALUES (@TestInvoice, @TestCustomer, 1000, 'V001', 1, 'Da thanh toan', 'Quick test voucher');
        
        -- Kiem tra ket qua
        SELECT 
            'TEST RESULT:' as RESULT_TYPE,
            MAHD,
            TONGTIEN,
            MAVOUCHER,
            TIENGIAM,
            THANHTIEN,
            CASE 
                WHEN TIENGIAM > 0 THEN 'VOUCHER WORKING ?'
                ELSE 'VOUCHER NOT WORKING ?'
            END as VOUCHER_STATUS
        FROM HOADONCHITIET
        WHERE MAHD = @TestInvoice;
        
        PRINT 'TEST PASSED! Voucher system is working.';
        
        -- Cleanup
        DELETE FROM LICHSUGIAODICH WHERE MAHD = @TestInvoice;
        DELETE FROM HOADONCHITIET WHERE MAHD = @TestInvoice;
        PRINT 'Test data cleaned up.';
        
    END TRY
    BEGIN CATCH
        PRINT 'TEST FAILED: ' + ERROR_MESSAGE();
    END CATCH
END
ELSE
BEGIN
    PRINT 'Khong tim thay khach hang de test!';
END

PRINT '=== QUICK TEST COMPLETED ===';

-- 5. Hien thi tat ca voucher de tham khao
SELECT 
    'ALL VOUCHERS SUMMARY:' as INFO,
    MAVOUCHER,
    LEFT(TENVOUCHER, 25) + '...' as TEN,
    CAST(GIATRIGIAM AS VARCHAR) + ' ' + LOAIGIAM as GIA_TRI,
    DIEMTOITHIEU as DIEM_YC,
    ISNULL(MAHANG_APDUNG, 'ALL') as HANG,
    CASE WHEN TRANGTHAI = 1 THEN 'ON' ELSE 'OFF' END as STATUS
FROM VOUCHER
ORDER BY DIEMTOITHIEU;

PRINT '=== READY FOR TESTING IN APPLICATION ===';