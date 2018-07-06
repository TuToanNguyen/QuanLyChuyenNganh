using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QUANLYCHUYENNGANH
{
    public partial class frmThongketheodiemtheomon : Form
    {
        public frmThongketheodiemtheomon()
        {
            InitializeComponent();
        }

        private void phansachtheoSLM()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "SELECT DISTINCT SINHVIEN.MSSV,HOTEN,MONHOC.MAMH,SOTC,DIEMCC,DIEMGK,DIEMCK,DIEMHE10,DIEMHE4,DIEMCHU,LANHOC FROM HOC,SINHVIEN,MONHOC where MONHOC.MAMH = 'TH0001'  ";
            //string sqlht = "select * from sach oder by solanmuon asc";
            SqlDataAdapter da = new SqlDataAdapter(sqlht, cn.con);
            crythongkediemall rpt = new crythongkediemall();
            crystalReportViewer1.ReportSource = rpt;
            cn.CloseConn();
        }

        private void frmThongketheodiemtheomon_Load(object sender, EventArgs e)
        {
            phansachtheoSLM();
        }
    }
}
