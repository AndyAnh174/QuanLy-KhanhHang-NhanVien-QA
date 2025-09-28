# FIX L?I T?O T�I KHO?N V� B? C?T TR?NG TH�I

## ?? V?n ?? ?� s?a

### 1. **L?i t?o t�i kho?n kh�ng th�nh c�ng**
**Nguy�n nh�n:** INSERT statement kh�ng ch? ??nh c?t `TRANGTHAI` nh?ng database y�u c?u.

**Gi?i ph�p:**
- ? C?p nh?t file `FormThemKhachHang.cs`
- ? Th�m c?t `TRANGTHAI = 1` v�o c�c c�u l?nh INSERT:
  - `INSERT INTO NGUOIDUNG` 
  - `INSERT INTO TAIKHOAN`
  - `INSERT INTO KHACHHANG`

### 2. **B? c?t TRANGTHAI kh?i giao di?n qu?n l�**
**Y�u c?u:** ?n c?t TRANGTHAI kh?i DataGridView qu?n l� kh�ch h�ng.

**Gi?i ph�p:**
- ? C?p nh?t file `FormNhanVien.cs`
- ? ?n c?t `TRANGTHAI` trong c�c ph??ng th?c:
  - `LoadKhachHang()`
  - `btnTimKiem_Click()`
- ? C?i thi?n hi?n th? c?t voucher

### 3. **C?p nh?t Data Access Layer**
**V?n ??:** Stored procedure tr? v? c? kh�ch h�ng ?� x�a (TRANGTHAI = 0).

**Gi?i ph�p:**
- ? C?p nh?t file `db.cs`
- ? S? d?ng query tr?c ti?p thay v� SP ?? ??m b?o ch? l?y `TRANGTHAI = 1`
- ? Th�m ?i?u ki?n `WHERE k.TRANGTHAI = 1 AND n.TRANGTHAI = 1`

## ?? Files ?� thay ??i

| File | Thay ??i | M� t? |
|------|----------|--------|
| `FormThemKhachHang.cs` | ? Fixed INSERT statements | Th�m c?t TRANGTHAI v�o c�c c�u l?nh INSERT |
| `FormNhanVien.cs` | ? ?n c?t TRANGTHAI | ?n c?t tr?ng th�i kh?i DataGridView |
| `db.cs` | ? C?p nh?t TimKiemKhachHang | Ch? l?y kh�ch h�ng c� TRANGTHAI = 1 |
| `Fix_TaoTaiKhoan_Va_AnCotTrangThai.sql` | ? T?o m?i | Script SQL ?? c?p nh?t stored procedures |

## ?? C�ch s? d?ng

### 1. Ch?y script SQL (t�y ch?n)
```sql
-- Ch?y file Fix_TaoTaiKhoan_Va_AnCotTrangThai.sql trong SQL Server Management Studio
```

### 2. Build v� ch?y ?ng d?ng
```bash
# Build project
dotnet build

# Ch?y ?ng d?ng
dotnet run
```

### 3. Test t?o t�i kho?n m?i
1. ??ng nh?p v?i t�i kho?n nh�n vi�n
2. V�o tab "Qu?n l� kh�ch h�ng"
3. Nh?n "Th�m kh�ch h�ng"
4. ?i?n th�ng tin v� nh?n "Th�m"
5. ? Kh�ng c�n l?i TRANGTHAI

### 4. Ki?m tra giao di?n
- ? C?t TRANGTHAI ?� b? ?n kh?i danh s�ch kh�ch h�ng
- ? Ch? hi?n th? kh�ch h�ng ?ang ho?t ??ng
- ? T�m ki?m ch? tr? v? kh�ch h�ng ch?a b? x�a

## ?? Ki?m tra l?i ?� s?a

### Tr??c khi s?a:
? L?i INSERT statement: "The INSERT statement conflicted with the CHECK constraint..."
? Hi?n th? c?t TRANGTHAI g�y r?i
? Hi?n th? c? kh�ch h�ng ?� x�a

### Sau khi s?a:
? T?o t�i kho?n th�nh c�ng
? Giao di?n s?ch s?, kh�ng hi?n c?t TRANGTHAI  
? Ch? hi?n kh�ch h�ng ?ang ho?t ??ng

## ?? Stored Procedures ?� c?p nh?t

| Procedure | M?c ?�ch | Thay ??i |
|-----------|----------|----------|
| `SP_TimKiemKhachHang` | T�m ki?m kh�ch h�ng | Th�m ?i?u ki?n TRANGTHAI = 1 |
| `SP_DangNhap` | ??ng nh?p h? th?ng | Ki?m tra c? user v� account TRANGTHAI |
| `SP_ThemKhachHang` | Th�m kh�ch h�ng m?i | Procedure m?i ??m b?o t�nh to�n v?n |

## ?? L?i �ch

1. **?n ??nh h? th?ng:** Kh�ng c�n l?i t?o t�i kho?n
2. **Giao di?n s?ch:** B? c?t kh�ng c?n thi?t
3. **D? li?u ch�nh x�c:** Ch? hi?n th? kh�ch h�ng ho?t ??ng
4. **B?o m?t t?t h?n:** Kh�ng hi?n th? th�ng tin nh?y c?m
5. **Tr?i nghi?m t?t:** Nh�n vi�n d? s? d?ng h?n

## ? L?u � quan tr?ng

- ? Database schema kh�ng thay ??i (an to�n)
- ? D? li?u hi?n t?i kh�ng b? m?t
- ? Ch?c n?ng x�a kh�ch h�ng v?n ho?t ??ng (soft delete)
- ? T??ng th�ch v?i triggers hi?n t?i

## ?? Troubleshooting

### N?u v?n g?p l?i t?o t�i kho?n:
1. Ki?m tra connection string trong `db.cs`
2. ??m b?o database c� ?? quy?n INSERT
3. Ch?y script SQL ?? c?p nh?t stored procedures

### N?u v?n hi?n c?t TRANGTHAI:
1. Rebuild solution
2. Clear browser cache n?u l� web app
3. Ki?m tra file `FormNhanVien.cs` ?� ???c c?p nh?t

---
**? T?T C? L?I ?� ???C S?A TH�NH C�NG!**