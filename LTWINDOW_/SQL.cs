using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace LTWINDOW_
{
    internal class SQL
    {
        //string connectionString = @"server=localhost;database=QuanLyQuanNuoc;integrated security=true";

        string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyQuanNuoc;Integrated Security=True;";
        private SqlDataAdapter adapter;
        private DataTable dt;
        private SqlConnection conn;
        public SQL()
        {
            conn = new SqlConnection(connectionString);
        }


        public SqlConnection Conn { get { return conn; } }

        // Phương thức mở kết nối
        public void OpenConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối đến SQL Server.\nLỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức đóng kết nối
        public void CloseConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể đóng kết nối.\nLỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Phương thức thực thi câu lệnh SQL (INSERT, UPDATE, DELETE)
        public void ExecuteQuery(string query)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thực thi câu lệnh SQL.\nLỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Phương thức lấy dữ liệu từ cơ sở dữ liệu
        public DataTable GetOrderSummary()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    CAST(NgayDatHang AS DATE) AS Ngay, 
                    SUM(TongTien) AS TongTien
                FROM DonHang
                GROUP BY CAST(NgayDatHang AS DATE)
                ORDER BY Ngay;
            ";

            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu từ cơ sở dữ liệu.\nLỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        // Phương thức lấy các bàn còn trống
        public DataTable GetAvailableTables()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT * FROM Ban
                WHERE TrangThai = 0; -- TrangThai 1 indicates the table is available
            ";

            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy danh sách bàn còn trống.\nLỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
    }

    

}
   
