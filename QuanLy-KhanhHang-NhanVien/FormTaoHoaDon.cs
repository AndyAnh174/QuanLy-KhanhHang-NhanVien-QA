using System;
using System.Windows.Forms;

namespace QuanLy_KhanhHang_NhanVien
{
    public partial class FormTaoHoaDon : Form
    {
        private DataAccess dataAccess;

        public FormTaoHoaDon()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            GenerateMaHD();
        }

        private void GenerateMaHD()
        {
            txtMaHD.Text = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            if (numTongTien.Value <= 0)
            {
                MessageBox.Show("Vui long nhap tong tien hop le!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maVoucher = string.IsNullOrWhiteSpace(txtMaVoucher.Text) ? null : txtMaVoucher.Text;
                string result = dataAccess.TaoHoaDon(
                    txtMaHD.Text,
                    Session.MaND,
                    numTongTien.Value,
                    maVoucher,
                    1,
                    txtGhiChu.Text
                );

                MessageBox.Show(result, "Ket qua", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if (result.Contains("thanh cong"))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi tao hoa don: {ex.Message}", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}