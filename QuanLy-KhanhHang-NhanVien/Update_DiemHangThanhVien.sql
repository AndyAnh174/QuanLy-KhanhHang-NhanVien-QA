-- =============================================
-- SCRIPT CAP NHAT MUC DIEM HANG THANH VIEN
-- Database: QuanLyKhachHang
-- Mo ta: Tang muc diem can thiet de len hang
-- =============================================

USE QuanLyKhachHang;
GO

PRINT '=============================================';
PRINT 'BAT DAU CAP NHAT MUC DIEM HANG THANH VIEN';
PRINT '=============================================';

-- Cap nhat muc diem cua cac hang thanh vien
UPDATE HANGTHANHVIEN SET DIEMTOITHIEU = 750 WHERE MAHANG = 'SILVER';
UPDATE HANGTHANHVIEN SET DIEMTOITHIEU = 1500 WHERE MAHANG = 'GOLD';  
UPDATE HANGTHANHVIEN SET DIEMTOITHIEU = 2000 WHERE MAHANG = 'DIAMOND';

PRINT 'Da cap nhat muc diem hang thanh vien:';
PRINT '- BAC: 750 diem (truoc day: 75)';
PRINT '- VANG: 1500 diem (truoc day: 150)';
PRINT '- KIM CUONG: 2000 diem (truoc day: 250)';

-- Cap nhat lai hang thanh vien cua khach hang dua tren diem hien tai
PRINT '=============================================';
PRINT 'CAP NHAT LAI HANG THANH VIEN CUA KHACH HANG';
PRINT '=============================================';

-- Kiem tra khach hang co diem >= 2000 -> Kim cuong
UPDATE KHACHHANG 
SET MAHANG = 'DIAMOND' 
WHERE DIEMTICHLUY >= 2000;

-- Kiem tra khach hang co diem >= 1500 nhung < 2000 -> Vang
UPDATE KHACHHANG 
SET MAHANG = 'GOLD' 
WHERE DIEMTICHLUY >= 1500 AND DIEMTICHLUY < 2000;

-- Kiem tra khach hang co diem >= 750 nhung < 1500 -> Bac
UPDATE KHACHHANG 
SET MAHANG = 'SILVER' 
WHERE DIEMTICHLUY >= 750 AND DIEMTICHLUY < 1500;

-- Kiem tra khach hang co diem < 750 -> Dong
UPDATE KHACHHANG 
SET MAHANG = 'BRONZE' 
WHERE DIEMTICHLUY < 750;

-- Cap nhat voucher dieu kien diem toi thieu
PRINT '=============================================';
PRINT 'CAP NHAT DIEU KIEN VOUCHER';
PRINT '=============================================';

-- Cap nhat voucher cho hang bac
UPDATE VOUCHER 
SET DIEMTOITHIEU = 750 
WHERE MAHANG_APDUNG = 'SILVER' AND DIEMTOITHIEU < 750;

-- Cap nhat voucher cho hang vang
UPDATE VOUCHER 
SET DIEMTOITHIEU = 1500 
WHERE MAHANG_APDUNG = 'GOLD' AND DIEMTOITHIEU < 1500;

-- Cap nhat voucher cho hang kim cuong
UPDATE VOUCHER 
SET DIEMTOITHIEU = 2000 
WHERE MAHANG_APDUNG = 'DIAMOND' AND DIEMTOITHIEU < 2000;

-- Hien thi thong ke sau khi cap nhat
PRINT '=============================================';
PRINT 'THONG KE KHACH HANG THEO HANG MOI';
PRINT '=============================================';

SELECT 
    h.TENHANG,
    h.DIEMTOITHIEU,
    COUNT(k.MAND) as SO_KHACH_HANG
FROM HANGTHANHVIEN h
LEFT JOIN KHACHHANG k ON h.MAHANG = k.MAHANG AND k.TRANGTHAI = 1
GROUP BY h.MAHANG, h.TENHANG, h.DIEMTOITHIEU
ORDER BY h.DIEMTOITHIEU;

PRINT '=============================================';
PRINT 'HOAN THANH CAP NHAT!';
PRINT 'MUC DIEM MOI DA DUOC AP DUNG CHO TAT CA';
PRINT 'KHACH HANG VA VOUCHER TRONG HE THONG';
PRINT '=============================================';