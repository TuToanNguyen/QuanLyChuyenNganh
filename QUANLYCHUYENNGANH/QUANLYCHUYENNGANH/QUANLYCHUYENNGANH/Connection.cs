using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace QUANLYCHUYENNGANH
{
    class Connection
    {
        //MultipleActiveResultSets=true
        //string strConn = "Data Source = 115.77.219.175,1433; Initial Catalog = QLChuyenNganh; Integrated Security = False; User Id = SQL Share; Password = system";

        
        string strConn;

        //string strConn = "server=NICOLAIPC\\SQLEXPRESS;database=QLChuyenNganh;uid=sa;pwd=system";
        public SqlConnection con { get; set; }
        public SqlDataReader drd { get; set; }
        public SqlCommand cmd { get; set; }
        SqlDataAdapter dar;
        DataTable table;
        //KẾT NOI
        public Connection()
        {
            //strConn = "server=" + frmKetnoiSQL.sv + ";database=" + frmKetnoiSQL.db + ";uid=" + frmKetnoiSQL.user + ";pwd=" + frmKetnoiSQL.pass;
           // MessageBox.Show(strConn);
            con = new SqlConnection("server=" + frmKetnoiSQL.sv + ";database=" + frmKetnoiSQL.db + ";uid=" + frmKetnoiSQL.user + ";pwd=" + frmKetnoiSQL.pass);
            drd = null;
            cmd = null;
        }
        public bool OpenConn()
        {
                this.con.Open();
                return true;

        }
        public bool CloseConn()
        {
            try

            {
                this.drd.Close();
                this.con.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //GỌI HÀM
        public SqlDataReader executeSQL(string strSQL)
        {

            this.cmd = new SqlCommand(strSQL, this.con);
            this.drd = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return this.drd;
        }

        public bool executeUpdate(string strUpdate)
        {
            this.cmd = new SqlCommand(strUpdate, this.con);
            int a = cmd.ExecuteNonQuery();
            if (a >= 1)
                return true;
            return false;
        }

        public int executeScala(string strSQL)
        {
            this.cmd = new SqlCommand(strSQL, this.con);
            int temp = (int)cmd.ExecuteScalar();
            return temp;
        }

        public bool ExecuteNonSQL(string sql)
        {
            this.cmd = new SqlCommand(sql, this.con);
            int count = (int)cmd.ExecuteNonQuery();
            if (count >= 1)
                return true;
            else
                return false;
        }
    }
}
