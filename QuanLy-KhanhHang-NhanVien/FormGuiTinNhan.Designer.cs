namespace QuanLy_KhanhHang_NhanVien
{
    partial class FormGuiTinNhan
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cboHinhThuc;
        private TextBox txtNoiDung;
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
            this.cboHinhThuc = new ComboBox();
            this.txtNoiDung = new TextBox();
            this.btnGui = new Button();
            this.btnHuy = new Button();
            this.SuspendLayout();

            // Hinh thuc
            var lblHinhThuc = new Label();
            lblHinhThuc.Location = new Point(20, 20);
            lblHinhThuc.Size = new Size(80, 20);
            lblHinhThuc.Text = "Hinh thuc:";
            this.Controls.Add(lblHinhThuc);

            this.cboHinhThuc.Location = new Point(100, 17);
            this.cboHinhThuc.Size = new Size(200, 23);
            this.cboHinhThuc.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboHinhThuc.Items.AddRange(new string[] { "Dien thoai", "Email", "Truc tiep", "Chat", "Khac" });
            this.cboHinhThuc.SelectedIndex = 3; // Chat

            // Noi dung
            var lblNoiDung = new Label();
            lblNoiDung.Location = new Point(20, 60);
            lblNoiDung.Size = new Size(80, 20);
            lblNoiDung.Text = "Noi dung:";
            this.Controls.Add(lblNoiDung);

            this.txtNoiDung.Location = new Point(20, 85);
            this.txtNoiDung.Size = new Size(350, 100);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.ScrollBars = ScrollBars.Vertical;
            this.txtNoiDung.PlaceholderText = "Nhap noi dung can tu van...";

            // Buttons
            this.btnGui.Location = new Point(150, 200);
            this.btnGui.Size = new Size(75, 30);
            this.btnGui.Text = "Gui";
            this.btnGui.BackColor = Color.Green;
            this.btnGui.ForeColor = Color.White;
            this.btnGui.Click += new EventHandler(this.btnGui_Click);

            this.btnHuy.Location = new Point(240, 200);
            this.btnHuy.Size = new Size(75, 30);
            this.btnHuy.Text = "Huy";
            this.btnHuy.BackColor = Color.Gray;
            this.btnHuy.ForeColor = Color.White;
            this.btnHuy.Click += new EventHandler(this.btnHuy_Click);

            // Form
            this.ClientSize = new Size(390, 250);
            this.Controls.Add(this.cboHinhThuc);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.btnHuy);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormGuiTinNhan";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Gui tin nhan tu van";
            this.ResumeLayout(false);
        }
    }
}