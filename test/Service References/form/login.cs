using System;
using System.Windows.Forms;

namespace test.form
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLDSVDataSet1.V_DS_PHANMANH' table. You can move, or remove it, as needed.
            this.v_DS_PHANMANHTableAdapter1.Fill(this.qLDSVDataSet1.V_DS_PHANMANH);
           
            Program.servername = cb.SelectedValue.ToString();
            try { cb.Items.RemoveAt(2); } catch { }
                
            
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

      
    }
}
