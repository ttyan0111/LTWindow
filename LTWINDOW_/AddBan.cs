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
    public partial class AddBan : Form
    {
        public AddBan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text, TrangThai = cmbStatus.SelectedItem.ToString();

            if (name.Trim() != "")
            {
                // khởi tạo class SQL để lấy chuổi kết nối.
                SQL sql = new SQL();

                using (SqlConnection connection = sql.Conn)
                {
                    try
                    {
                        String query = $"insert into Ban(TenBan, TrangThai) Values (N'{name.Trim()}', N'{TrangThai.Trim()}')";
                        connection.Open();

                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();

                        if (MessageBox.Show("bàn đã được thêm\nnếu muốn thêm tiếp nhấp Yes không nhập No?", "Question", MessageBoxButtons.YesNo) == DialogResult.No)
                            this.Close();
                        else txtName.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }
            else MessageBox.Show("bạn chưa nhập tên bàn");

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn thoát", "Warnning", MessageBoxButtons.OKCancel) == DialogResult.OK) this.Close();
        }



        private void AddBan_Load(object sender, EventArgs e)
        {
            //  đảm bạo chọn item còn trống khi bắt đầu chạy.
            cmbStatus.SelectedIndex = 0;
        }
    }
}
