using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Media;


namespace QUANLYCHUYENNGANH
{
    public partial class frmQuanlythongtinlophoc : Form
    {
        SqlCommand cm;
        public static string UsertName = "";
        public static string malop = "";

        public SoundPlayer error = new SoundPlayer(@"e:\Âm thanh thông báo\Windows Error.wav");

        public frmQuanlythongtinlophoc()
        {
            InitializeComponent();
        }
        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select distinct malop as 'Mã lớp',tennganh as 'Tên ngành',TENDT as 'Hệ đào tạo',tenlop as 'Tên lớp' from LOP, nganh, BACDAOTAO where nganh.manganh = lop.manganh and LOP.MADT = BACDAOTAO.MADT";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvthongtin.DataSource = dt;
            cn.CloseConn();
        }
        //public DataTable danhsachlop()
        //{
        //    Connection cn = new Connection();
        //    SqlDataAdapter da = new SqlDataAdapter("select MALOP  as 'Mã Lớp',MANGANH as 'Mã Ngành',MADT as 'Mã Đào Tạo', TENLOP as 'Tên Lớp' from LOP", cn.con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}
        public void Loadcbmanganh()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select distinct nganh.MANGANH,tennganh from NGANH", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmanganh.DataSource = ds.Tables[0];
            cbmanganh.ValueMember = "MANGANH";
            cbmanganh.DisplayMember = "tennganh";
        }
        public void Loadcbmandaotao()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select distinct MADT, TENDT from BACDAOTAO", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmadt.DataSource = ds.Tables[0];
            cbmadt.ValueMember = "MADT";
            cbmadt.DisplayMember = "TENDT";
        }

        private void frmQuanlythongtinlophoc_Load(object sender, EventArgs e)
        {
            hienthi();
            Loadcbmanganh();
            Loadcbmandaotao();
            //dgvthongtin.DataSource = danhsachlop();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string malop = txtmalop.Text;
            string manganh = cbmanganh.SelectedValue.ToString();
            string madt = cbmadt.SelectedValue.ToString();
            string tenlop = txttenlop.Text;
            cm = new SqlCommand("select MALOP from LOP where MALOP='" + malop + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (malop == "" || manganh == "" || madt == "" || tenlop == "")
            {
                MessageBox.Show("Thông tin lớp không được bỏ trống!!!");
            }
            else
            if (malop == ma)
            {
                error.Play();
                MessageBox.Show("Trùng mã lớp, thêm thất bại");
            }
            else
            {
                string sqlthem = "insert into LOP values('" + malop + "',N'" + manganh + "',N'" + madt + "',N'" + tenlop + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            hienthi();
            Loadcbmanganh();
            Loadcbmandaotao();
            //dgvthongtin.DataSource = danhsachlop();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string malop = txtmalop.Text;
            string manganh = cbmanganh.SelectedValue.ToString();
            string madt = cbmadt.SelectedValue.ToString();
            string tenlop = txttenlop.Text;
            cm = new SqlCommand("select MALOP from LOP where MALOP='" + malop + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (malop == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update LOP set MALOP='" + malop + "',MANGANH=N'" + manganh + "',MADT=N'" + madt + "',TENLOP=N'" + tenlop + "' where  MALOP='" + malop + "'";
                    SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Sửa thành công");
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Sửa thất bại!");
                    }
                }
            }
            else
                MessageBox.Show("Không trùng mã lớp!");
            hienthi();
            Loadcbmanganh();
            Loadcbmandaotao();
            //dgvthongtin.DataSource = danhsachlop();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string malop = txtmalop.Text;
            Connection cn = new Connection();
            cn.OpenConn();
            if (malop == "")
            {
                MessageBox.Show("Thông tin cần xóa hiện không tồn tại! ");
            }
            else
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete LOP where  MALOP='" + malop + "'";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        txtmalop.Clear();
                        txttenlop.Clear();
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
                hienthi();
                Loadcbmanganh();
                Loadcbmandaotao();
                //dgvthongtin.DataSource = danhsachlop();
            }
        }

        private void dgvthongtin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvthongtin.Rows[e.RowIndex];

                txtmalop.Text = row.Cells[0].Value.ToString();
                cbmanganh.Text = row.Cells[1].Value.ToString();
                cbmadt.Text = row.Cells[2].Value.ToString();
                txttenlop.Text = row.Cells[3].Value.ToString();

            }
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
            dgvthongtin.DataSource = hienthiTK("select * from LOP where MALOP like '%" + txtTK.Text.Trim() + "%'");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            malop = txtmalop.Text;
            if (txtmalop.TextLength != 0)
            {
                frmCovanlophocphan f = new frmCovanlophocphan();
                f.ShowDialog();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
