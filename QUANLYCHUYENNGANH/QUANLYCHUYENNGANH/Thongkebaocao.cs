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
    public partial class frmThongkebaocao : Form
    {
        SqlCommand cm;
        public frmThongkebaocao()
        {
            InitializeComponent();
        }

        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = @"select distinct SINHVIEN.MSSV,SINHVIEN.HOTEN as 'Họ tên', TENMH as 'Tên MH',SOTC, DIEMCC, DIEMGK, DIEMCK, DIEMHE10, DIEMHE4, 
            DIEMCHU as 'Điểm chữ', LANHOC as 'Lần học' from COVAN,CANBO,LOP,NGANH,CHUYENNGANH,MONHOC,HOC, sinhvien 
            where CANBO.MACB=COVAN.MACB and COVAN.MALOP = LOP.MALOP and LOP.MANGANH = NGANH.MANGANH and NGANH.MANGANH=CHUYENNGANH.MANGANH 
            and CHUYENNGANH.MACN = MONHOC.MACN and MONHOC.MAMH = HOC.MAMH and hoc.mssv = sinhvien.mssv and CANBO.MACB='" + frmForm1.UsertName + "'and sinhvien.macn='" + cbmacn.SelectedValue + "' and sinhvien.MALOP='" + cbmalop.SelectedValue + "' and HOC.MAMH ='" + cbmamh.SelectedValue + "'";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgthongtin.DataSource = dt;
            cn.CloseConn();
        }

        public void Loadcbmalop()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand(@"select DISTINCT LOP.MALOP,tenlop 
                                    from COVAN, LOP, CANBO
                                    where CANBO.MACB = COVAN.MACB and COVAN.MALOP = LOP.MALOP and CANBO.MACB = '" + frmForm1.UsertName + "' " +
                                    " AND LOP.MALOP IN( " +
                                    "SELECT MALOP FROM COVAN WHERE MACB = '" + frmForm1.UsertName + "' AND GETDATE() BETWEEN THOIGIANBD AND THOIGIANKT UNION ALL SELECT MALOP FROM COVAN WHERE MACB = '" + frmForm1.UsertName + "' AND THOIGIANKT IS NULL)", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmalop.DataSource = ds.Tables[0];
            cbmalop.ValueMember = "MALOP";
            cbmalop.DisplayMember = "tenlop";

            Loadcbmachuyennganh();
        }

        public void Loadcbmachuyennganh()
        {
            try
            {
                Connection cn = new Connection();
                cn.OpenConn();
                cm = new SqlCommand(@"select DISTINCT CHUYENNGANH.MACN,TENCN from COVAN,LOP,CANBO,NGANH,CHUYENNGANH 
                where CANBO.MACB = COVAN.MACB and COVAN.MALOP = LOP.MALOP and LOP.MANGANH = NGANH.MANGANH 
                and NGANH.MANGANH = CHUYENNGANH.MANGANH and CANBO.MACB='" + frmForm1.UsertName + "' AND LOP.MALOP='" + cbmalop.SelectedValue.ToString() + "'  ", cn.con);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cbmacn.DataSource = ds.Tables[0];
                cbmacn.ValueMember = "MACN";
                cbmacn.DisplayMember = "TENCN";
            }
            catch { }
        }

        public void Loadcbmamon()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand(@"select distinct MONHOC.MAMH, TENMH from COVAN, CANBO, LOP, NGANH, CHUYENNGANH, 
            MONHOC, sinhvien, hoc
            where CANBO.MACB = COVAN.MACB and COVAN.MALOP = LOP.MALOP and LOP.MANGANH = NGANH.MANGANH and 
            NGANH.MANGANH = CHUYENNGANH.MANGANH
            and CHUYENNGANH.MACN = MONHOC.MACN  
    and CANBO.MACB = '" + frmForm1.UsertName + "' and  chuyennganh.macn='" + cbmacn.SelectedValue.ToString() + "'", cn.con);

            //cm = new SqlCommand(@"select distinct MONHOC.MAMH, TENMH from COVAN, CANBO, LOP, NGANH, CHUYENNGANH, MONHOC
            //where CANBO.MACB = COVAN.MACB and COVAN.MALOP = LOP.MALOP and LOP.MANGANH = NGANH.MANGANH and NGANH.MANGANH = CHUYENNGANH.MANGANH
            //and CHUYENNGANH.MACN = MONHOC.MACN and CANBO.MACB = '" + frmForm1.UsertName + "' and  chuyennganh.macn='" + cbmacn.SelectedValue.ToString() + "'", cn.con);

            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmamh.DataSource = ds.Tables[0];
            cbmamh.ValueMember = "MAMH";
            cbmamh.DisplayMember = "TENMH";
        }

        private void Thongkebaocao_Load(object sender, EventArgs e)
        {
            Loadcbmalop();
            Loadcbmamon();
            Loadcbmachuyennganh();
            hienthi();
        }

        private void cbmalop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadcbmachuyennganh();
            hienthi();
        }

        private void cbmacn_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadcbmamon();
            hienthi();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ////Tạo các đối tượng Excel
            //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            ////Khởi tạo Workbook
            //Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
            ////Khởi tạo Worksheet
            //Microsoft.Office.Interop.Excel.Worksheet ws = null;
            //ws = wb.Sheets["Sheet1"];
            //ws = wb.ActiveSheet;
            //app.Visible = true;
            ////Đổ dl vào sheet
            //ws.Cells[1, 1] = "BANG DIEM";
            //ws.Cells[1, 1] = "BANG DIEM";

            ExportToExcel excel = new ExportToExcel();
            // Lấy về nguồn dữ liệu cần Export là 1 DataTable
            // DataTable này mỗi bạn lấy mỗi khác. 
            // Ở đây tôi dùng BindingSouce có tên bs nên tôi ép kiểu như sau:
            // Bạn nào gán trực tiếp vào DataGridView thì ép kiểu DataSource của
            // DataGridView nhé 
            DataTable dt = (DataTable)dgthongtin.DataSource;
            excel.Export(dt, "Danh sach", "THỐNG KÊ ĐIỂM THEO MÔN HỌC");

        }

        private void cbmamh_SelectedIndexChanged(object sender, EventArgs e)
        {
            hienthi();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
