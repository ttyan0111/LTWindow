using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTWINDOW_
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text, passsword = txtPassword.Text;

            ListTaiKhoan ltk = new ListTaiKhoan();
            ltk.queryTaiKhoan(@"select NhanVien.UserName, NhanVien.Passcode from NhanVien");
            List<TaiKhoan> taiKhoans = ltk.List;

            errorProvider1.Clear();
            if(username.Trim() == "") errorProvider1.SetError(txtUserName, "Bạn chưa nhập tài khoản.");
            if(passsword.Trim() == "") errorProvider1.SetError(txtPassword, "Bạn chưa nhập mật khẩu.");
            if (username.Trim() != "" && passsword.Trim() != "")
            {
                if (taiKhoans.Count > 0)
                {
                    int check = 0;
                    foreach (TaiKhoan tk in taiKhoans)
                    {
                        if (tk.UserName == username && tk.Password == passsword)
                        {
                            check = 1;
                            break;
                        }
                    }

                    if (check == 1)
                    {
                        lblThongBaoInvalid.Visible = false;
                        this.Hide();
                        Form1 form = new Form1();
                        form.Show();
                    }
                    else lblThongBaoInvalid.Visible = true;
                }
            }
            

            //if (username.Trim() != "" && passsword.Trim() != "")
            //{
            //    if (ltk.isExit(username, passsword))
            //    {
            //        this.Hide();
            //        Form1 form1 = new Form1();
            //        form1.Show();
            //    }
            //    else MessageBox.Show("Tài Khoản hoặc mật khẩu không chính xác.");
            //}
            //else MessageBox.Show("Bạn chưa nhập mật khẩu hoặc Tài Khoản");


        }

        

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("bạn có muốn chắc chắc muốn thoát", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) e.Cancel = true;
        }
    }
}
