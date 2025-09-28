using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DataAccess
{
    private string connectionString = "Data Source=DESKTOP-2VU3708\\SQLEXPRESS;Initial Catalog=QuanLyKhachHang;Integrated Security=True";

    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }

    public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
        }
    }

    public object ExecuteScalar(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteScalar();
            }
        }
    }

    // Dang nhap
    public DataTable DangNhap(string tenDangNhap, string matKhau)
    {
        string query = "EXEC SP_DangNhap @TenDangNhap, @MatKhau";
        SqlParameter[] parameters = {
            new SqlParameter("@TenDangNhap", tenDangNhap),
            new SqlParameter("@MatKhau", matKhau)
        };
        return ExecuteQuery(query, parameters);
    }

    // Tim kiem khach hang
    public DataTable TimKiemKhachHang(string keyword = null, string maHang = null, string mandQL = null)
    {
        string query = "EXEC SP_TimKiemKhachHang @Keyword, @Mahang, @Mand_QL";
        SqlParameter[] parameters = {
            new SqlParameter("@Keyword", (object)keyword ?? DBNull.Value),
            new SqlParameter("@Mahang", (object)maHang ?? DBNull.Value),
            new SqlParameter("@Mand_QL", (object)mandQL ?? DBNull.Value)
        };
        return ExecuteQuery(query, parameters);
    }

    // Tao hoa don
    public string TaoHoaDon(string maHD, string mandKH, decimal tongTien, string maVoucher = null, int soLuong = 1, string ghiChu = null)
    {
        string query = "EXEC SP_TaoHoaDon @Mahd, @Mand_KH, @Tongtien, @Mavoucher, @Soluong, @Ghichu";
        SqlParameter[] parameters = {
            new SqlParameter("@Mahd", maHD),
            new SqlParameter("@Mand_KH", mandKH),
            new SqlParameter("@Tongtien", tongTien),
            new SqlParameter("@Mavoucher", (object)maVoucher ?? DBNull.Value),
            new SqlParameter("@Soluong", soLuong),
            new SqlParameter("@Ghichu", (object)ghiChu ?? DBNull.Value)
        };
        
        DataTable result = ExecuteQuery(query, parameters);
        return result.Rows.Count > 0 ? result.Rows[0]["KETQUA"].ToString() : "";
    }

    // Bao cao doanh thu
    public DataTable BaoCaoDoanhThu(DateTime ngayBatDau, DateTime ngayKetThuc)
    {
        string query = "EXEC SP_BaoCaoDoanhThu @NgayBatDau, @NgayKetThuc";
        SqlParameter[] parameters = {
            new SqlParameter("@NgayBatDau", ngayBatDau),
            new SqlParameter("@NgayKetThuc", ngayKetThuc)
        };
        return ExecuteQuery(query, parameters);
    }
}

// Models
public class NguoiDung
{
    public string MaND { get; set; }
    public string HoTen { get; set; }
    public DateTime? NgaySinh { get; set; }
    public string GioiTinh { get; set; }
    public string Email { get; set; }
    public string SoDienThoai { get; set; }
    public string DiaChi { get; set; }
    public string CMND { get; set; }
    public DateTime NgayTao { get; set; }
    public bool TrangThai { get; set; }
}

public class TaiKhoan
{
    public string MaND { get; set; }
    public string TenDangNhap { get; set; }
    public string MatKhau { get; set; }
    public string VaiTro { get; set; }
    public DateTime NgayTao { get; set; }
    public bool TrangThai { get; set; }
}

public class KhachHang
{
    public string MaND { get; set; }
    public string MaHang { get; set; }
    public string MaND_QL { get; set; }
    public int DiemTichLuy { get; set; }
    public DateTime NgayDangKy { get; set; }
    public bool TrangThai { get; set; }
}

public class HangThanhVien
{
    public string MaHang { get; set; }
    public string TenHang { get; set; }
    public string MoTa { get; set; }
    public int DiemToiThieu { get; set; }
    public string UuDai { get; set; }
    public DateTime NgayTao { get; set; }
}

public class Voucher
{
    public string MaVoucher { get; set; }
    public string TenVoucher { get; set; }
    public string MoTa { get; set; }
    public decimal GiaTriGiam { get; set; }
    public string LoaiGiam { get; set; }
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }
    public int SoLuong { get; set; }
    public int DaSuDung { get; set; }
    public int DiemToiThieu { get; set; }
    public string MaHangApDung { get; set; }
    public bool TrangThai { get; set; }
}

public class HoaDonChiTiet
{
    public string MaHD { get; set; }
    public string MaND_KH { get; set; }
    public DateTime NgayThanhToan { get; set; }
    public decimal TongTien { get; set; }
    public string MaVoucher { get; set; }
    public int SoLuong { get; set; }
    public decimal TienTruocGiam { get; set; }
    public decimal TienGiam { get; set; }
    public decimal ThanhTien { get; set; }
    public string PhuongThucThanhToan { get; set; }
    public string TrangThai { get; set; }
    public string GhiChu { get; set; }
}

public class TuongTacKhachHang
{
    public int MaTT { get; set; }
    public string MaND_KH { get; set; }
    public string MaND_NV { get; set; }
    public DateTime NgayTuongTac { get; set; }
    public string HinhThuc { get; set; }
    public string NoiDung { get; set; }
    public string KetQua { get; set; }
    public string TrangThai { get; set; }
}

// Session quan ly thong tin dang nhap
public static class Session
{
    public static string MaND { get; set; }
    public static string HoTen { get; set; }
    public static string VaiTro { get; set; }
    public static string Email { get; set; }
    public static string SoDienThoai { get; set; }
    public static bool TrangThai { get; set; }

    public static bool IsLoggedIn => !string.IsNullOrEmpty(MaND);
    public static bool IsAdmin => VaiTro == "ADMIN";
    public static bool IsNhanVien => VaiTro == "NHANVIEN";
    public static bool IsKhachHang => VaiTro == "KHACHHANG";

    public static void Clear()
    {
        MaND = null;
        HoTen = null;
        VaiTro = null;
        Email = null;
        SoDienThoai = null;
        TrangThai = false;
    }
}
