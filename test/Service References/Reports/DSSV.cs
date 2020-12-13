using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace test.Reports
{
    public partial class DSSV : DevExpress.XtraReports.UI.XtraReport
    {
        public DSSV(String mlop)
        {
            InitializeComponent();
            this.sP_ReportDSSVTableAdapter.Connection.ConnectionString = Program.connstr;
            this.sP_ReportDSSVTableAdapter.Fill(sP_ReportDSSV1._SP_ReportDSSV, mlop);
        }

    }
}
