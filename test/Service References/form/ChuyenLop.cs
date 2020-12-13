﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;

namespace test.form
{
    public partial class ChuyenLop : DevExpress.XtraEditors.XtraForm
    {
        private int STATE = 1;
        private String masv;

        public ChuyenLop()
        {
            InitializeComponent();
        }

        private void frmChuyenLop_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLDSVDataSet.SINHVIEN' table. You can move, or remove it, as needed.
            this.sINHVIENTableAdapter.Fill(this.qLDSVDataSet.SINHVIEN);
            // TODO: This line of code loads data into the 'qLDSVDataSet.LOP' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'qLDSVDataSet.KHOA' table. You can move, or remove it, as needed.
            this.kHOATableAdapter.Fill(this.qLDSVDataSet.KHOA);

            func.BindingDataToComBo(cbKhoa, Program.Bds_Dspm);
            this.lOPTableAdapter.Connection.ConnectionString = Program.connstr; // lấy data của Site tương ứng
            this.lOPTableAdapter.Fill(this.qLDSVDataSet.LOP);
            //this.labelKhoa.Text = ((DataRowView)Program.Bds_Dspm[Program.mKhoa])["TENCN"].ToString();
        }

        // ============================ EVENT BUTTON ============================ //
        private void buttonOK_Click(object sender, EventArgs e)
        {

            // D16CQCN3 là mã lớp cần chuyển đến cùng khoa
            // D16CQVT1 là mã lớp cần chuyển đến khác khoa
            var selectedSV = lookUpEditChuyenLop.GetSelectedDataRow() as DataRowView;
            string maSVTextBox = masv;
            string maLopChuyenDenTextBox = cbLop.SelectedValue.ToString();
            string QUERY = "";
            string MASV = "";

            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = new SqlConnection(Program.connstr))
            {
                // OPEN CONNECTION
                connection.Open();


                if (STATE == 1)// CHUYỂN LỚP CÙNG KHOA
                {
                    //MessageBox.Show("LỚP TỒN TẠI Ở SITE HIỆN TẠI");
                    // TODO : Cập nhập mã lớp cho sinh viên ở chính site hiện tại
                    QUERY = "Update SINHVIEN SET MALOP='" + maLopChuyenDenTextBox + "' WHERE MASV='" + maSVTextBox + "'";
                    command = new SqlCommand(QUERY, connection);
                    adapter.UpdateCommand = new SqlCommand(QUERY, connection);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    XtraMessageBox.Show("Chuyển Sinh Viên Cùng Khoa, Vào Lớp "+ maLopChuyenDenTextBox + " Thành Công", "Info", MessageBoxButtons.OK);
                    return;
                }

                if (STATE == 2) // INFO : CHUYỂN LỚP KHÁC KHOA
                {

                    MessageBox.Show(masv);
                    QUERY = "SP_CHUYENLOP";
                    command = new SqlCommand(QUERY, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("MASV", maSVTextBox);// masv cũ
                    command.Parameters.AddWithValue("MASVMOI", textEdit1.Text);// masv mới
                    command.Parameters.AddWithValue("MALOPCHUYENDEN", maLopChuyenDenTextBox);// mã lớp chuyển đến

                    try
                    {
                        command.ExecuteNonQuery();
                        XtraMessageBox.Show("Chuyển Sinh Viên Qua Lớp " + maLopChuyenDenTextBox + " Thành Công", "INFO", MessageBoxButtons.OK);
                        return;
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK);
                    }
                    command.Dispose();
                }        
                connection.Close();
                
            }// END USING

        }



        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ============================ FUNCTION ============================ //
    

    


        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbKhoa.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    Program.servername = cbKhoa.SelectedValue.ToString();
                }
                if (cbKhoa.SelectedIndex != Program.mKhoa)
                {
                    Program.mlogin = Program.remotelogin;
                    Program.password = Program.remotepassword;
                    STATE = 2;
                }
                else
                {
                    Program.mlogin = Program.mloginDN;
                    Program.password = Program.passwordDN;
                    STATE = 1;
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
                        this.lOPTableAdapter.Connection.ConnectionString = Program.connstr; // lấy data của Site tương ứng
                        this.lOPTableAdapter.Fill(this.qLDSVDataSet.LOP);
                    }
                    catch (Exception) { }
                }


            }
            catch
            {
                return;
            }
        }

        private void lookUpEditChuyenLop_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void lookUpEditChuyenLop_EditValueChanged(object sender, EventArgs e)
        {
            var selectedSV = lookUpEditChuyenLop.GetSelectedDataRow() as DataRowView;
            string nghiHoc = selectedSV.Row["NGHIHOC"].ToString().Trim();
            if (nghiHoc.Equals("True"))
            {
                MessageBox.Show("Không thể thực hiện, sinh viên này đã nghỉ học!!");
                return;
            }
            txtTenSV.Text = selectedSV.Row["HO"] + " " + selectedSV.Row["TEN"];
            txtMaLop.Text = selectedSV.Row["MALOP"].ToString();
            masv = selectedSV.Row["MASV"].ToString();
      
            cbKhoa.Enabled = true;
        }

        private void cbKhoa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbLop.Enabled = true;
        }

        private void cbLop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            textEdit1.Enabled = true;
        }

        private void textEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            for(int i = 0; i < sINHVIENBindingSource1.Count; i++)
            {
                DataRowView tmp = sINHVIENBindingSource1[i] as DataRowView;
                if (tmp.Row[0].ToString().Trim().Equals(""))
                {
                    label4.ForeColor = System.Drawing.Color.Red;
                    label4.Text = "Mã Không được để rôn";
                    buttonOK.Enabled = true;
                    return;
                }
                if (textEdit1.Text.Trim().Equals(tmp.Row[0].ToString().Trim()))
                {
                    label4.ForeColor = System.Drawing.Color.Red;
                    label4.Text = "Mã đã tồn tại, hãy chọn mã khác";
                    buttonOK.Enabled = true;
                    return;
                }
            }
            label4.ForeColor = System.Drawing.Color.Green;
            label4.Text = "OK";
            buttonOK.Enabled = true;
               
        }
    }
}