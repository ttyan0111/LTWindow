using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web;
using System.Windows.Forms;

namespace LTWINDOW_
{
    internal class ListTaiKhoan
    {
        List<TaiKhoan> list;

        public ListTaiKhoan()
        {
            list = new List<TaiKhoan>();
        }

        public List<TaiKhoan> List { get { return list; } }

        public void queryTaiKhoan(string query)
        {
            SQL sQL = new SQL();
            using (SqlConnection conn = sQL.Conn)
            {
                try
                {
                    conn.Open ();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataReader data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        list.Add(new TaiKhoan(data["UserName"].ToString(), data["Passcode"].ToString()));
                    } 
                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
                

            }    
        }

        public bool isExit(string username, string password)
        {
            SQL sQL = new SQL();
            bool result = false;
            using (SqlConnection conn = sQL.Conn)
            {
                try
                {
                    conn.Open();

                    string query = @"select * from NhanVien where UserName = @UserName and Passcode = @password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("UserName", username);
                    cmd.Parameters.AddWithValue("password", password);

                    SqlDataReader data = cmd.ExecuteReader();
                    if (data.HasRows) result = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return result;
        }
    }
}
