-- =============================================
-- SCRIPT DEBUG VA SUA LOI VOUCHER KIM CUONG
-- Database: QuanLyKhachHang
-- Mo ta: Debug va sua loi voucher khong giam tien
-- =============================================

USE QuanLyKhachHang;
GO

PRINT '=============================================';
PRINT 'BAT DAU DEBUG VOUCHER SYSTEM';
PRINT '=============================================';

-- 1. Kiem tra du lieu khach hang Kim cuong
SELECT 'THONG TIN KHACH HANG KIM CUONG:' as DEBUG_STEP;
SELECT 
    k.MAND,
    n.HOTEN,
    k.DIEMTICHLUY,
    k.MAHANG,
    h.TENHANG,
    h.DIEMTOITHIEU
FROM KHACHHANG k
INNER JOIN NGUOIDUNG n ON k.MAND = n.MAND
INNER JOIN HANGTHANHVIEN h ON k.MAHANG = h.MAHANG
WHERE k.MAHANG = 'DIAMOND' OR k.DIEMTICHLUY >= 2000;

-- 2. Kiem tra voucher danh cho Kim cuong
SELECT 'VOUCHER DANH CHO KIM CUONG:' as DEBUG_STEP;
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
    DASUDUNG,
    (SOLUONG - DASUDUNG) as CON_LAI
FROM VOUCHER
WHERE MAHANG_APDUNG = 'DIAMOND' OR DIEMTOITHIEU >= 2000;

-- 3. Kiem tra hoa don gan day co su dung voucher
SELECT 'HOA DON CO SU DUNG VOUCHER:' as DEBUG_STEP;
SELECT TOP 5
    hd.MAHD,
    hd.MAND_KH,
    hd.TONGTIEN,
    hd.MAVOUCHER,
    hd.TIENTRUOCGIAM,
    hd.TIENGIAM,
    hd.THANHTIEN,
    hd.TRANGTHAI,
    hd.NGAYTHANHTOAN
FROM HOADONCHITIET hd
WHERE hd.MAVOUCHER IS NOT NULL
ORDER BY hd.NGAYTHANHTOAN DESC;

-- 4. Test trigger truc tiep
PRINT '=============================================';
PRINT 'TEST TRIGGER VOI DU LIEU THAT:';
PRINT '=============================================';

-- Tim khach hang co diem cao nhat
DECLARE @TestCustomer VARCHAR(20);
DECLARE @TestPoints INT;
DECLARE @TestRank VARCHAR(10);

SELECT TOP 1 
    @TestCustomer = MAND,
    @TestPoints = DIEMTICHLUY,
    @TestRank = MAHANG
FROM KHACHHANG 
WHERE DIEMTICHLUY >= 2000 AND TRANGTHAI = 1
ORDER BY DIEMTICHLUY DESC;

IF @TestCustomer IS NOT NULL
BEGIN
    PRINT 'Khach hang test: ' + @TestCustomer;
    PRINT 'Diem: ' + CAST(@TestPoints AS VARCHAR(10));
    PRINT 'Hang: ' + @TestRank;
    
    -- Test voi voucher V004 (20% cho kim cuong)
    DECLARE @TestInvoice VARCHAR(20) = 'TEST_' + CONVERT(VARCHAR, GETDATE(), 112) + '_' + CONVERT(VARCHAR, DATEPART(SECOND, GETDATE()));
    DECLARE @TestAmount DECIMAL(18,2) = 1000000; -- 1 trieu de test
    
    PRINT 'Tao hoa don test: ' + @TestInvoice;
    PRINT 'So tien test: 1,000,000 VND';
    PRINT 'Voucher test: V004 (20% cho kim cuong)';
    
    -- Kiem tra voucher V004 truoc khi test
    SELECT 'KIEM TRA VOUCHER V004:' as DEBUG_STEP;
    SELECT 
        v.MAVOUCHER,
        v.GIATRIGIAM,
        v.LOAIGIAM,
        v.DIEMTOITHIEU,
        v.MAHANG_APDUNG,
        v.TRANGTHAI,
        CASE WHEN GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC THEN 'VALID' ELSE 'EXPIRED' END as TINHTRANG,
        CASE WHEN @TestPoints >= v.DIEMTOITHIEU THEN 'DU DIEM' ELSE 'KHONG DU DIEM' END as DIEM_CHECK,
        CASE WHEN v.MAHANG_APDUNG = @TestRank THEN 'DUNG HANG' ELSE 'SAI HANG' END as HANG_CHECK
    FROM VOUCHER v
    WHERE v.MAVOUCHER = 'V004';
    
    -- Thuc hien insert test
    BEGIN TRY
        INSERT INTO HOADONCHITIET (MAHD, MAND_KH, TONGTIEN, MAVOUCHER, SOLUONG, TRANGTHAI, GHICHU)
        VALUES (@TestInvoice, @TestCustomer, @TestAmount, 'V004', 1, 'Da thanh toan', 'Test voucher kim cuong');
        
        -- Kiem tra ket qua
        SELECT 'KET QUA SAU KHI INSERT:' as DEBUG_STEP;
        SELECT 
            MAHD,
            TONGTIEN,
            MAVOUCHER,
            TIENTRUOCGIAM,
            TIENGIAM,
            THANHTIEN,
            CASE 
                WHEN TIENGIAM > 0 THEN 'VOUCHER HOAT DONG'
                ELSE 'VOUCHER KHONG HOAT DONG'
            END as VOUCHER_STATUS
        FROM HOADONCHITIET
        WHERE MAHD = @TestInvoice;
        
    END TRY
    BEGIN CATCH
        PRINT 'LOI KHI INSERT: ' + ERROR_MESSAGE();
    END CATCH
    
END
ELSE
BEGIN
    PRINT 'KHONG TIM THAY KHACH HANG CO DIEM >= 2000';
END;

-- 5. Sua trigger neu co van de
PRINT '=============================================';
PRINT 'KIEM TRA VA SUA TRIGGER';
PRINT '=============================================';

-- Drop va tao lai trigger voi logic sua doi
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_CalculateInvoiceAmount')
    DROP TRIGGER TR_CalculateInvoiceAmount;
GO

CREATE TRIGGER TR_CalculateInvoiceAmount
ON HOADONCHITIET
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    PRINT 'TRIGGER TR_CalculateInvoiceAmount DUOC GOI';
    
    -- Thuc hien INSERT voi tinh toan tien giam chi tiet
    INSERT INTO HOADONCHITIET (
        MAHD, MAND_KH, NGAYTHANHTOAN, TONGTIEN, MAVOUCHER, SOLUONG, 
        TIENTRUOCGIAM, TIENGIAM, THANHTIEN, PHUONGTHUCTHANHTOAN, 
        TRANGTHAI, GHICHU
    )
    SELECT 
        i.MAHD,
        i.MAND_KH,
        ISNULL(i.NGAYTHANHTOAN, GETDATE()),
        i.TONGTIEN,
        i.MAVOUCHER,
        ISNULL(i.SOLUONG, 1),
        i.TONGTIEN AS TIENTRUOCGIAM,
        
        -- Logic tinh tien giam da sua
        CASE 
            WHEN i.MAVOUCHER IS NOT NULL 
                AND v.MAVOUCHER IS NOT NULL
                AND v.TRANGTHAI = 1 
                AND GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC
                AND (v.DASUDUNG < v.SOLUONG OR v.SOLUONG = 0)
                AND ISNULL(k.DIEMTICHLUY, 0) >= ISNULL(v.DIEMTOITHIEU, 0)
                AND (v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = k.MAHANG)
            THEN 
                CASE 
                    WHEN v.LOAIGIAM = 'Phan tram' 
                    THEN 
                        CASE 
                            WHEN (i.TONGTIEN * v.GIATRIGIAM / 100.0) > i.TONGTIEN 
                            THEN i.TONGTIEN 
                            ELSE (i.TONGTIEN * v.GIATRIGIAM / 100.0)
                        END
                    ELSE 
                        CASE 
                            WHEN v.GIATRIGIAM > i.TONGTIEN 
                            THEN i.TONGTIEN 
                            ELSE v.GIATRIGIAM 
                        END
                END
            ELSE 0
        END AS TIENGIAM,
        
        -- Tinh thanh tien = tong tien - tien giam  
        i.TONGTIEN - CASE 
            WHEN i.MAVOUCHER IS NOT NULL 
                AND v.MAVOUCHER IS NOT NULL
                AND v.TRANGTHAI = 1 
                AND GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC
                AND (v.DASUDUNG < v.SOLUONG OR v.SOLUONG = 0)
                AND ISNULL(k.DIEMTICHLUY, 0) >= ISNULL(v.DIEMTOITHIEU, 0)
                AND (v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = k.MAHANG)
            THEN 
                CASE 
                    WHEN v.LOAIGIAM = 'Phan tram' 
                    THEN 
                        CASE 
                            WHEN (i.TONGTIEN * v.GIATRIGIAM / 100.0) > i.TONGTIEN 
                            THEN i.TONGTIEN 
                            ELSE (i.TONGTIEN * v.GIATRIGIAM / 100.0)
                        END
                    ELSE 
                        CASE 
                            WHEN v.GIATRIGIAM > i.TONGTIEN 
                            THEN i.TONGTIEN 
                            ELSE v.GIATRIGIAM 
                        END
                END
            ELSE 0
        END AS THANHTIEN,
        
        ISNULL(i.PHUONGTHUCTHANHTOAN, 'Tien mat'),
        ISNULL(i.TRANGTHAI, 'Chua thanh toan'),
        CASE 
            WHEN i.MAVOUCHER IS NOT NULL AND v.MAVOUCHER IS NOT NULL
            THEN 'Su dung voucher: ' + i.MAVOUCHER + ' - Giam: ' + 
                 CAST(CASE 
                    WHEN v.LOAIGIAM = 'Phan tram' 
                    THEN (i.TONGTIEN * v.GIATRIGIAM / 100.0)
                    ELSE v.GIATRIGIAM 
                 END AS VARCHAR(20)) + ' VND'
            ELSE ISNULL(i.GHICHU, '')
        END
        
    FROM inserted i
    LEFT JOIN VOUCHER v ON i.MAVOUCHER = v.MAVOUCHER
    LEFT JOIN KHACHHANG k ON i.MAND_KH = k.MAND;
    
    -- Debug info
    DECLARE @RowCount INT = @@ROWCOUNT;
    PRINT 'Da insert ' + CAST(@RowCount AS VARCHAR(5)) + ' dong vao HOADONCHITIET';
END;
GO

PRINT '=============================================';
PRINT 'HOAN THANH DEBUG VA SUA LOI';
PRINT 'HAY CHAY LAI TEST VOUCHER KIM CUONG';
PRINT '=============================================';

-- Cleanup test data (optional)
-- DELETE FROM HOADONCHITIET WHERE MAHD LIKE 'TEST_%';
-- DELETE FROM LICHSUGIAODICH WHERE GHICHU LIKE '%TEST_%';