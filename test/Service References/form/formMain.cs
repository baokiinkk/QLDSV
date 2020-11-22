using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test.form
{
    public partial class formMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public formMain()
        {
            InitializeComponent();
            loadStatusBar();
            txtUsername.Text ="MaGV: "+ Program.username;
            txtHoten.Text = "HoTen: " + Program.mHoten;
            txtGroup.Text = "Nhom: " + Program.mGroup;
            if (Program.mKhoa == 2)
            {
                btnClass.Enabled = btnStudent.Enabled = btnMark.Enabled = btnSubject.Enabled = false;
                btnInDSSV.Enabled = btnInDSThi.Enabled = btnInDiem.Enabled = btnInPD.Enabled = false;
            }
            else
            {
                btnFee.Enabled = false;
                btnInHP.Enabled = false;
            }
        }

        //load thông tin bên dưới statustrip
        public void loadStatusBar()
        {
            txtUsername.Text = "Mã giảng viên: " + Program.username.Trim() + " ||";
            txtHoten.Text = "Họ tên: " + Program.mHoten.Trim() + " ||";
            txtGroup.Text = "Nhóm: " + Program.mGroup;
        }

        // kiểm tra form có tồn tại hay ko
        private Form CheckExists(Type ftype)
        {
            foreach(Form f in this.MdiChildren)
            {
                if(f.GetType() == ftype)
                {
                    return f;
                }
            }
            return null;
        }

        //mở form Lớp
        private void btnClass_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Lop frm =(Lop)CheckExists(typeof(Lop));
            if (frm != null) frm.Activate();
            else
            {
                Program.lop = new Lop();
               
                Program.lop.MdiParent = this;
               
                Program.lop.Show();
            }
        }

        //mở form Điểm
        private void btnMark_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        //mở form Sinh viên
        private void btnStudent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        //mở form Môn học
        private void btnSubject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        //mở form Học phí
        private void btnFee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult ds = MessageBox.Show("Bạn chắc chắn muốn đăng xuất không ?", "Thông báo !", MessageBoxButtons.YesNo);
            if (ds == DialogResult.Yes)
            {
                Close();   
                Program.login.Visible = true;
            }
        }

        private void formMain_Load(object sender, EventArgs e)
        {
           
        }
    }
}
