using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QUANLYCHUYENNGANH
{
    public partial class frmPhan_quyen : Form
    {
        xuly xl = new xuly();
        SqlCommand cm = new SqlCommand();
        Connection cn = new Connection();
       
        public frmPhan_quyen()
        {
            InitializeComponent();
        }
        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select MACB,MAKHOA,HOTEN,NGAYSINH,GIOITINH,CHUCVU,EMAIL,QUYENHAN from CANBO where QUYENHAN!='admin'" ;
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }
        public DataTable danhsachCB()
        {
            Connection cn = new Connection();
            SqlDataAdapter da = new SqlDataAdapter("select MACB  as 'Mã Cán Bộ',MAKHOA as 'Mã Khoa',HOTEN as 'Họ Tên',NGAYSINH as 'Ngày Sinh',GIOITINH as 'Giới Tính',CHUCVU as 'Chức vụ',EMAIL as 'Email',QUYENHAN as 'Quyền Hạn' from CANBO where QUYENHAN!='admin'", cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        private void frmPhan_quyen_Load(object sender, EventArgs e)
        {
            //hienthi();
            dgvthongtin.DataSource = danhsachCB();
        }

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                lbmacb.Text = row.Cells[0].Value.ToString();
                lbmakhoa.Text = row.Cells[1].Value.ToString();
                lbhoten.Text = row.Cells[2].Value.ToString();
                lbngsinh.Text = row.Cells[3].Value.ToString();
                lbgioitinh.Text = row.Cells[4].Value.ToString();
                lbchucvu.Text = row.Cells[5].Value.ToString();
                lbemail.Text = row.Cells[6].Value.ToString();
                lbqhan.Text = row.Cells[7].Value.ToString();
                cbquyen.Text = row.Cells[7].Value.ToString();
            }
        }

        private void btnxacnhan_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string mcb = lbmacb.Text;
            string quyen = cbquyen.Text;


            //KIEM TRA PHAN QUYEN 
            cm = new SqlCommand("select Count(QUYENHAN)  from CANBO where QUYENHAN= 'admin'  ", cn.con);
            int qh = (int)cm.ExecuteScalar();
            //MessageBox.Show(qh.ToString());
            ////for(int i =1; i>=3; i++)
            ////{
            //    if (qh+1 > 1 )
            //    {
            //        MessageBox.Show("Chỉ tồn tại 1 admin! Vui lòng chọn lại quyền hạn.", "THÔNG BÁO", MessageBoxButtons.OK);
            //    }
            //    //else
            //if (qh - 1 ==0)
            //{
            //    MessageBox.Show("can 1 admin .", "THÔNG BÁO", MessageBoxButtons.OK);
            //}
            ////}
            ////}
            //if(qh < 1)
            //{
            //    MessageBox.Show("phai co it nhat 1 admin");
            //}
            //if (qh > 3)
            //{
            //    MessageBox.Show("co khong qua 3 admin");
            //}

            //if (cbquyen.Text == "admin" && lbqhan.Text == "covan")
            //{
            //        MessageBox.Show("");                
            //}


            //if (cbquyen.Text == "admin" && lbqhan.Text != "admin")
            //{
            //    if (qh + 1 > 1)
            //    {
            //        MessageBox.Show("Chỉ tồn tại 1 admin! Vui lòng chọn lại quyền hạn.", "THÔNG BÁO", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //        if (DialogResult.OK == dlr)
            //        {
            //            string sqlsua = "update CANBO set QUYENHAN='" + quyen + "' where  MACB='" + mcb + "'";
            //            SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
            //            try
            //            {
            //                cmd.ExecuteNonQuery();
            //                cmd.Dispose();
            //                cn.CloseConn();
            //                MessageBox.Show("Thay đổi quyền thành công");
            //                frmPhan_quyen_Load(sender, e);
            //                hienthi();
            //                dgvthongtin.DataSource = danhsachCB();
            //            }
            //            catch
            //            {
            //                cmd.Dispose();
            //                cn.CloseConn();
            //                MessageBox.Show("Thay đổi quyền thất bại!");
            //            }
            //        }
            //    }
               
            //}
            ////////////////////////
            //else
            //if (cbquyen.Text == "admin" && lbqhan.Text != "admin")
            //{
            //    if (qh -1 == 0)
            //    {
            //        MessageBox.Show("phai co it nhat 1 admin ", "THÔNG BÁO", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //        if (DialogResult.OK == dlr)
            //        {
            //            string sqlsua = "update CANBO set QUYENHAN='" + quyen + "' where  MACB='" + mcb + "'";
            //            SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
            //            try
            //            {
            //                cmd.ExecuteNonQuery();
            //                cmd.Dispose();
            //                cn.CloseConn();
            //                MessageBox.Show("Thay đổi quyền thành công");
            //                frmPhan_quyen_Load(sender, e);
            //                hienthi();
            //                dgvthongtin.DataSource = danhsachCB();
            //            }
            //            catch
            //            {
            //                cmd.Dispose();
            //                cn.CloseConn();
            //                MessageBox.Show("Thay đổi quyền thất bại!");
            //            }
            //        }
            //    }

            //}     

            ////////////ngoi thuong
            //else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update CANBO set QUYENHAN='" + quyen + "' where  MACB='" + mcb + "'";
                    SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Thay đổi quyền thành công");
                        frmPhan_quyen_Load(sender, e);
                        hienthi();
                        dgvthongtin.DataSource = danhsachCB();
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Thay đổi quyền thất bại!");
                    }
                }
            }


            //UPDATE QUYEN HAN
            //else
            //{
            //    DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //    if (DialogResult.OK == dlr)
            //    {
            //        string sqlsua = "update CANBO set QUYENHAN='" + quyen + "' where  MACB='" + mcb + "'";
            //        SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
            //        try
            //        {
            //            cmd.ExecuteNonQuery();
            //            cmd.Dispose();
            //            cn.CloseConn();
            //            MessageBox.Show("Thay đổi quyền thành công");
            //            frmPhan_quyen_Load(sender, e);
            //            hienthi();
            //            dgvthongtin.DataSource = danhsachCB();
            //        }
            //        catch
            //        {
            //            cmd.Dispose();
            //            cn.CloseConn();
            //            MessageBox.Show("Thay đổi quyền thất bại!");
            //        }
            //    }
            //}
            hienthi();
            dgvthongtin.DataSource = danhsachCB();

        }

        public DataTable hienthiTK(string sqlht)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            SqlDataAdapter da = new SqlDataAdapter(sqlht, cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
            cn.CloseConn();
        }
        private void txtTK_TextChanged(object sender, EventArgs e)
        {
            dgvthongtin.DataSource = hienthiTK("select * from CANBO where MACB like '%" + txtTK.Text.Trim() + "%'");
        }


    }   
}
