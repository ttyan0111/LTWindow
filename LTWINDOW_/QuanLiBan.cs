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


namespace LTWINDOW_
{
    public partial class QuanLiBan : Form
    {
        public QuanLiBan()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void loadData()
        {
            SQL sql = new SQL();

            using (SqlConnection connect = sql.Conn)
            {
                try
                {
                    connect.Open();
                    string query = @"select * from Ban";
                    // command.
                    SqlCommand command = new SqlCommand(query, connect);

                    // adapter.
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    // tạo dataTable rỗng để chứa  dữ liệu
                    DataTable table = new DataTable();

                    // đổ dữ liệu vào table.
                    adapter.Fill(table);

                    // hiển thị dữ liệu lên bảng.
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
                
            }

        }

        private void QuanLiBan_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // khởi tạo form thêm bàng.
            AddBan addBan = new AddBan();

            // thực thi xong form thêm bàn mới chạy câu lệnh tiếp theo.
            addBan.ShowDialog();

            // load lại datagrid view.
            loadData();

        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                var getValueColumnCurrent = dataGridView1.SelectedRows[0].Cells[0].Value;
                int MaBan = int.Parse(getValueColumnCurrent.ToString());

                // câu truy vấn.
                string query = "delete from Ban where MaBan = @MaBan";

                SQL sql = new SQL();

                // kiểm tra xem có xóa được dữ liệu chưa.
                int check = 0;
                using (SqlConnection connection = sql.Conn)
                {
                    try
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(query, connection);

                        // add parameter
                        command.Parameters.AddWithValue("@MaBan", MaBan);

                        // thực thi không tri vấn
                        command.ExecuteNonQuery();

                        // dã xóa dữ liệu.
                        check = 1;
                    }
                    catch  (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    } 
                    
                }

                // nếu xóa được load lại datagridView.
                if (check == 1) loadData();
            }
            else MessageBox.Show("không có dòng nào để xóa");
        }
    }
}
