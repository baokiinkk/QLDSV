using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace test.form
{
    public partial class Lop : DevExpress.XtraEditors.XtraForm
    {
        int num = -1;
        int indexLast = -10000;
        public Lop()
        {
            InitializeComponent();
        }

        private void Lop_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLDSVDataSet.SINHVIEN' table. You can move, or remove it, as needed.
 
            // TODO: This line of code loads data into the 'qLDSVDataSet.SINHVIEN' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'qLDSVDataSet.V_DS_PHANMANH' table. You can move, or remove it, as needed.
            this.v_DS_PHANMANHTableAdapter.Fill(this.qLDSVDataSet.V_DS_PHANMANH);

            // TODO: This line of code loads data into the 'qLDSVDataSet.LOP' table. You can move, or remove it, as needed.
            load();


            comboBox1.SelectedIndex = Program.mKhoa;
            if (Program.mGroup.Equals("Khoa"))
                comboBox1.Enabled = false;
            // TODO: This line of code loads data into the 'qLDSVDataSet.LOP' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'qLDSVDataSet.LOP' table. You can move, or remove it, as needed.
            gridView1.ShowingEditor += (s, ex) =>
            {
                readOnly("MALOP");
            };
            

        }

        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
              String ma = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MALOP").ToString();
              String ten = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TENLOP").ToString();
                if (CheckLoiLop(ma, ten))
                {
                    Validate();
                    this.lOPBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.qLDSVDataSet);

                    this.Validate();
                    this.sINHVIENBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.qLDSVDataSet);
                    MessageBox.Show("Thành công");
                }
            }
            catch(Exception ex)
            {
               MessageBox.Show("Tên lớp không được phép trùng!!");
                return;

            }

        }

 

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    Program.servername = comboBox1.SelectedValue.ToString();
                }
                if (comboBox1.SelectedIndex != Program.mKhoa)
                {
                    Program.mlogin = Program.remotelogin;
                    Program.password = Program.remotepassword;
                }
                else
                {
                    Program.mlogin = Program.mloginDN;
                    Program.password = Program.passwordDN;
                }

                if (Program.KetNoi() == 0)
                {
                    MessageBox.Show("Lỗi chuyển khoa", "Thông báo !", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    try
                    {
                        load();

                    }
                    catch (Exception) { }
                }


            }
            catch
            {
                return;
            }
        }

        private bool CheckLoiLop(String ma,String ten)
        {

            if (ma.Equals(""))
            {
                gridView1.SetColumnError(gridView1.Columns["MALOP"], "Mã rỗng!!",DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
                return false;
            }
            if (ten.Equals(""))
            {
                gridView1.SetColumnError(gridView1.Columns["TENLOP"], "Mã rỗng!!", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);

                return false;
            }
            if (num == 1)
            {
               
                //TODO: Check mã lớp có tồn tại chưa
                string query1 = "DECLARE  @return_value int \n"
                            + "EXEC  @return_value = SP_CHECKID \n"
                            + "@Ma = N'" + ma + "',@Type = N'MALOP' \n"
                            + "SELECT  'Return Value' = @return_value ";

                SqlDataReader resultMa = Program.ExecSqlDataReader(query1);
                if (resultMa == null)
                {
                    MessageBox.Show("Server Lỗi");
                    return false;
                }
                resultMa.Read();
                if (resultMa.GetInt32(0) == 1)
                {
                    gridView1.SetColumnError(gridView1.Columns["MALOP"], "Mã tồn tại ở khoa này!!", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);

                    return false;
                }
                if (resultMa.GetInt32(0) == 2)
                {
                    gridView1.SetColumnError(gridView1.Columns["MALOP"], "Mã tồn tại ở khoa khác!!", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);

                    return false;
                }


                // check ten lop
               
                num = -1;
            }
            
            return true;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            
            num = 1;
            if (comboBox1.SelectedIndex == 1)
                gridView1.SetFocusedRowCellValue("MAKH", "CNTT");
            else if (comboBox1.SelectedIndex == 0)
                gridView1.SetFocusedRowCellValue("MAKH", "VT");
                 
        }
        private void readOnly(String col)
        {
            if(num == 1)
            {

                if (gridView1.FocusedRowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                {
                    gridView1.Columns[col].OptionsColumn.ReadOnly = false;
                }
                else
                {
                    if (gridView1.FocusedRowHandle == gridView1.RowCount - 1)
                        gridView1.Columns[col].OptionsColumn.ReadOnly = false;
                    else
                        gridView1.Columns[col].OptionsColumn.ReadOnly = true;
                }

            }
            else
            {
                gridView1.Columns[col].OptionsColumn.ReadOnly = true;
            }
        }

        private void gridView2_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(sINHVIENGridControl, e.Location);
            }
        }
        private void load()
        {
         
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr; // lấy data của Site tương ứng
            this.lOPTableAdapter.Fill(this.qLDSVDataSet.LOP);
            this.sINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet.SINHVIEN);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã load lại dữ liệu");
            load();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            gridView2.AddNewRow();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            gridView2.DeleteRow(gridView2.FocusedRowHandle);
        }
    }
}