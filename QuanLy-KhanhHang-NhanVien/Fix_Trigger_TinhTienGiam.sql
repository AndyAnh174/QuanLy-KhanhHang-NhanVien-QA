-- =============================================
-- SCRIPT CAP NHAT TRIGGER TINH TOAN VOUCHER (PHIEN BAN SUA LOI CUOI CUNG)
-- Database: QuanLyKhachHang
-- Mo ta: Sua lai trigger de xu ly dung loi voucher - phien ban cuoi cung
-- =============================================

USE QuanLyKhachHang;
GO

-- Xoa trigger cu
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_CalculateInvoiceAmount')
    DROP TRIGGER TR_CalculateInvoiceAmount;
GO

-- Tao lai trigger tinh toan tien giam (phien ban sua loi cuoi cung)
CREATE TRIGGER TR_CalculateInvoiceAmount
ON HOADONCHITIET
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Xu ly INSERT voi logic sua loi
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
            WHEN i.MAVOUCHER IS NULL OR LTRIM(RTRIM(ISNULL(i.MAVOUCHER, ''))) = '' 
            THEN 0.00
            ELSE
                ISNULL((
                    SELECT TOP 1
                        CASE 
                            WHEN v.LOAIGIAM = 'Phan tram' 
                            THEN CAST(ROUND(i.TONGTIEN * v.GIATRIGIAM / 100.0, 0) AS DECIMAL(18,2))
                            ELSE CAST(v.GIATRIGIAM AS DECIMAL(18,2))
                        END
                    FROM VOUCHER v 
                    INNER JOIN KHACHHANG k ON i.MAND_KH = k.MAND
                    WHERE v.MAVOUCHER = i.MAVOUCHER
                        AND v.TRANGTHAI = 1
                        AND GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC  
                        AND (v.DASUDUNG < v.SOLUONG OR v.SOLUONG = 0)
                        AND ISNULL(k.DIEMTICHLUY, 0) >= ISNULL(v.DIEMTOITHIEU, 0)
                        AND (v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = k.MAHANG)
                ), 0.00)
        END AS TIENGIAM,
        
        -- Tinh thanh tien = tong tien - tien giam
        i.TONGTIEN - CASE 
            WHEN i.MAVOUCHER IS NULL OR LTRIM(RTRIM(ISNULL(i.MAVOUCHER, ''))) = '' 
            THEN 0.00
            ELSE
                ISNULL((
                    SELECT TOP 1
                        CASE 
                            WHEN v.LOAIGIAM = 'Phan tram' 
                            THEN CAST(ROUND(i.TONGTIEN * v.GIATRIGIAM / 100.0, 0) AS DECIMAL(18,2))
                            ELSE CAST(v.GIATRIGIAM AS DECIMAL(18,2))
                        END
                    FROM VOUCHER v 
                    INNER JOIN KHACHHANG k ON i.MAND_KH = k.MAND
                    WHERE v.MAVOUCHER = i.MAVOUCHER
                        AND v.TRANGTHAI = 1
                        AND GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC  
                        AND (v.DASUDUNG < v.SOLUONG OR v.SOLUONG = 0)
                        AND ISNULL(k.DIEMTICHLUY, 0) >= ISNULL(v.DIEMTOITHIEU, 0)
                        AND (v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = k.MAHANG)
                ), 0.00)
        END AS THANHTIEN,
        
        ISNULL(i.PHUONGTHUCTHANHTOAN, 'Tien mat'),
        ISNULL(i.TRANGTHAI, 'Chua thanh toan'),
        
        -- Cap nhat ghi chu voi thong tin voucher
        CASE 
            WHEN i.MAVOUCHER IS NOT NULL AND LTRIM(RTRIM(ISNULL(i.MAVOUCHER, ''))) != ''
                AND EXISTS (
                    SELECT 1 FROM VOUCHER v 
                    INNER JOIN KHACHHANG k ON i.MAND_KH = k.MAND
                    WHERE v.MAVOUCHER = i.MAVOUCHER
                        AND v.TRANGTHAI = 1
                        AND GETDATE() BETWEEN v.NGAYBATDAU AND v.NGAYKETTHUC  
                        AND (v.DASUDUNG < v.SOLUONG OR v.SOLUONG = 0)
                        AND ISNULL(k.DIEMTICHLUY, 0) >= ISNULL(v.DIEMTOITHIEU, 0)
                        AND (v.MAHANG_APDUNG IS NULL OR v.MAHANG_APDUNG = k.MAHANG)
                )
            THEN 
                ISNULL(i.GHICHU, '') + 
                CASE 
                    WHEN ISNULL(i.GHICHU, '') != '' THEN ' | ' 
                    ELSE '' 
                END + 
                'Su dung voucher: ' + i.MAVOUCHER
            ELSE ISNULL(i.GHICHU, '')
        END
        
    FROM inserted i;
END;
GO

-- Cap nhat lai trigger cap nhat diem (giu nguyen)
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_UpdateDiemTichLuy')
    DROP TRIGGER TR_UpdateDiemTichLuy;
GO

CREATE TRIGGER TR_UpdateDiemTichLuy
ON HOADONCHITIET
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Cap nhat diem tich luy: 0.1% = 0.001 (1 trieu = 1000 diem)
    UPDATE k
    SET DIEMTICHLUY = k.DIEMTICHLUY + CAST(ISNULL(i.THANHTIEN, 0) * 0.001 AS INT)
    FROM KHACHHANG k
    INNER JOIN inserted i ON k.MAND = i.MAND_KH
    WHERE ISNULL(i.TRANGTHAI, '') = 'Da thanh toan';
    
    -- Cap nhat hang thanh vien dua tren diem tich luy moi
    UPDATE k
    SET MAHANG = (
        SELECT TOP 1 h.MAHANG 
        FROM HANGTHANHVIEN h 
        WHERE ISNULL(k.DIEMTICHLUY, 0) >= ISNULL(h.DIEMTOITHIEU, 0)
        ORDER BY ISNULL(h.DIEMTOITHIEU, 0) DESC
    )
    FROM KHACHHANG k
    INNER JOIN inserted i ON k.MAND = i.MAND_KH
    WHERE ISNULL(i.TRANGTHAI, '') = 'Da thanh toan'
    AND EXISTS (
        SELECT 1 FROM HANGTHANHVIEN h 
        WHERE ISNULL(k.DIEMTICHLUY, 0) >= ISNULL(h.DIEMTOITHIEU, 0)
    );
END;
GO

-- Cap nhat trigger lich su giao dich (giu nguyen)
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_CreateLichSuGiaoDich')
    DROP TRIGGER TR_CreateLichSuGiaoDich;
GO

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
        ISNULL(i.NGAYTHANHTOAN, GETDATE()),
        'Thanh toan',
        CASE 
            WHEN i.MAVOUCHER IS NOT NULL AND LTRIM(RTRIM(ISNULL(i.MAVOUCHER, ''))) != ''
            THEN 'HD: ' + i.MAHD + ' | Voucher: ' + i.MAVOUCHER + 
                 ' | Tong: ' + FORMAT(ISNULL(i.TONGTIEN, 0), 'N0') + '?' +
                 ' | Giam: ' + FORMAT(ISNULL(i.TIENGIAM, 0), 'N0') + '?' +
                 ' | Thanh toan: ' + FORMAT(ISNULL(i.THANHTIEN, 0), 'N0') + '?'
            ELSE 'HD: ' + i.MAHD + ' | Thanh toan: ' + FORMAT(ISNULL(i.THANHTIEN, 0), 'N0') + '?'
        END,
        ISNULL(i.THANHTIEN, 0)
    FROM inserted i
    WHERE ISNULL(i.TRANGTHAI, '') = 'Da thanh toan';
END;
GO

PRINT '=============================================';
PRINT 'DA CAP NHAT THANH CONG CÁC TRIGGER (PHIEN BAN CUOI):';
PRINT '1. TR_CalculateInvoiceAmount - Sua loi xu ly voucher';
PRINT '2. TR_UpdateDiemTichLuy - An toan voi NULL';  
PRINT '3. TR_CreateLichSuGiaoDich - Format dep hon';
PRINT '=============================================';

-- Test nhanh
DECLARE @TestResult TABLE (
    TestCase VARCHAR(50),
    TONGTIEN DECIMAL(18,2),
    MAVOUCHER VARCHAR(50), 
    TIENGIAM DECIMAL(18,2),
    THANHTIEN DECIMAL(18,2),
    Status VARCHAR(20)
);

-- Kiem tra cac voucher hien co
SELECT 
    'VOUCHER SYSTEM STATUS:' as INFO,
    COUNT(*) as TOTAL_VOUCHERS,
    SUM(CASE WHEN TRANGTHAI = 1 THEN 1 ELSE 0 END) as ACTIVE_VOUCHERS,
    SUM(CASE WHEN GETDATE() BETWEEN NGAYBATDAU AND NGAYKETTHUC THEN 1 ELSE 0 END) as VALID_DATE_VOUCHERS
FROM VOUCHER;

-- Hien thi voucher chi tiet
SELECT 
    MAVOUCHER,
    LEFT(TENVOUCHER, 30) + '...' as TENVOUCHER,
    GIATRIGIAM,
    LOAIGIAM,
    DIEMTOITHIEU,
    MAHANG_APDUNG,
    CASE WHEN TRANGTHAI = 1 THEN 'ACTIVE' ELSE 'INACTIVE' END as STATUS,
    CASE WHEN GETDATE() BETWEEN NGAYBATDAU AND NGAYKETTHUC THEN 'VALID' ELSE 'EXPIRED' END as DATE_STATUS,
    (SOLUONG - DASUDUNG) as REMAINING
FROM VOUCHER
ORDER BY DIEMTOITHIEU;

PRINT '=============================================';
PRINT 'TRIGGER CAP NHAT HOAN THANH!';
PRINT 'VOUCHER SYSTEM READY FOR TESTING!';
PRINT '=============================================';