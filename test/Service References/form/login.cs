using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace test.form
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet1.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet1.GIANGVIEN' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'qLDSVDataSet1.V_DS_PHANMANH' table. You can move, or remove it, as needed.
            string kn = "Data Source=DESKTOP-J8Q68TT;Initial Catalog=" + Program.database + ";Integrated Security=True";
            Program.conn.ConnectionString = kn;
            DataTable data = new DataTable();
            //gọi 1 view và trả về dưới dạng datatable
            data = Program.ExecSqlDataTable("SELECT * FROM V_DS_PHANMANH");
            Program.Bds_Dspm.DataSource = data;
            //// cất dt vào biến toàn cục Bds_Dspm
            Program.Bds_Dspm.Sort = "TENCN ASC";
            // đoạn code liên kết giữa bds với combobox
            func.BindingDataToComBo(cb, data);
            Program.servername = cb.SelectedValue.ToString();
          
                
            
        }



        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (edtTaiKhoan.Text.Trim() == "" || edtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("tài khoản hoặc mật khẩu không được để trống!", "Lỗi đăng nhập", MessageBoxButtons.YesNoCancel);
                edtTaiKhoan.Focus();
                return;
            }
            Program.mKhoa =cb.SelectedIndex;
            Program.mKhoatmp = cb.SelectedIndex;
            Program.mlogin = edtTaiKhoan.Text;
            Program.password = edtMatKhau.Text;
            if (Program.KetNoi() == 0) return;
            MessageBox.Show("Đăng nhập thành công");
            string strLenh = "EXEC SP_DANGNHAP '" + Program.mlogin + "'";
            Program.myReader = Program.ExecSqlDataReader(strLenh);
            if (Program.myReader == null) return;
            Program.myReader.Read();

            Program.username = Program.myReader.GetString(0);
            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Tên đăng nhập không có quyền truy cập dữ liệu.\nBạn xem lại Tên đăng nhập và Mật khẩu", "Thông báo !", MessageBoxButtons.OK);
                return;
            }
            Program.mHoten = Program.myReader.GetString(1);
            Program.mGroup = Program.myReader.GetString(2);
            Program.mloginDN = Program.mlogin;
            Program.passwordDN = Program.password;
            Program.myReader.Close();


            Program.formMain = new formMain();

            Program.login.Visible = false;
            Program.formMain.Show();
     
        }

    

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cb.SelectedValue.ToString());
            try
            {
                Program.servername = cb.SelectedValue.ToString();
            }
            catch
            {
                return;
            }

        }



     

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Program.conn.Close();
            if(Program.login != null)
                Program.login.Close();
            Close();
        }

        internal void Form1_Load()
        {
            cb.SelectedIndex = Program.mKhoa;
            Program.servername = cb.SelectedValue.ToString();
        }

        private void mASVLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            sINHVIENBindingSource.Position = mASVLookUpEdit.ItemIndex;
        }
    }
}
