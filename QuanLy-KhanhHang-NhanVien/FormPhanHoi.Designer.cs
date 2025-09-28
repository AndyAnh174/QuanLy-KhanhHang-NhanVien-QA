namespace QuanLy_KhanhHang_NhanVien
{
    partial class FormPhanHoi
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblKhachHang;
        private TextBox txtNoiDungCu;
        private TextBox txtPhanHoi;
        private Button btnGui;
        private Button btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblKhachHang = new Label();
            this.txtNoiDungCu = new TextBox();
            this.txtPhanHoi = new TextBox();
            this.btnGui = new Button();
            this.btnHuy = new Button();
            this.SuspendLayout();

            // lblKhachHang
            this.lblKhachHang.Location = new Point(20, 20);
            this.lblKhachHang.Size = new Size(350, 20);
            this.lblKhachHang.Text = "Khach hang:";
            
            // Label tin nhan cu
            var lblTinNhanCu = new Label();
            lblTinNhanCu.Location = new Point(20, 50);
            lblTinNhanCu.Size = new Size(100, 20);
            lblTinNhanCu.Text = "Tin nhan:";
            this.Controls.Add(lblTinNhanCu);

            // txtNoiDungCu
            this.txtNoiDungCu.Location = new Point(20, 75);
            this.txtNoiDungCu.Size = new Size(350, 80);
            this.txtNoiDungCu.Multiline = true;
            this.txtNoiDungCu.ReadOnly = true;
            this.txtNoiDungCu.ScrollBars = ScrollBars.Vertical;

            // Label phan hoi
            var lblPhanHoi = new Label();
            lblPhanHoi.Location = new Point(20, 165);
            lblPhanHoi.Size = new Size(100, 20);
            lblPhanHoi.Text = "Phan hoi:";
            this.Controls.Add(lblPhanHoi);

            // txtPhanHoi
            this.txtPhanHoi.Location = new Point(20, 190);
            this.txtPhanHoi.Size = new Size(350, 80);
            this.txtPhanHoi.Multiline = true;
            this.txtPhanHoi.ScrollBars = ScrollBars.Vertical;

            // btnGui
            this.btnGui.Location = new Point(150, 290);
            this.btnGui.Size = new Size(75, 30);
            this.btnGui.Text = "Gui";
            this.btnGui.BackColor = Color.Green;
            this.btnGui.ForeColor = Color.White;
            this.btnGui.Click += new EventHandler(this.btnGui_Click);

            // btnHuy
            this.btnHuy.Location = new Point(240, 290);
            this.btnHuy.Size = new Size(75, 30);
            this.btnHuy.Text = "Huy";
            this.btnHuy.BackColor = Color.Gray;
            this.btnHuy.ForeColor = Color.White;
            this.btnHuy.Click += new EventHandler(this.btnHuy_Click);

            // FormPhanHoi
            this.ClientSize = new Size(400, 340);
            this.Controls.Add(this.lblKhachHang);
            this.Controls.Add(this.txtNoiDungCu);
            this.Controls.Add(this.txtPhanHoi);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.btnHuy);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormPhanHoi";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Phan hoi khach hang";
            this.ResumeLayout(false);
        }
    }
}