using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjectC
{
    public partial class loginForm : Form
    {
        
        public loginForm()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        

        private void loginButton_Click(object sender, EventArgs e)
        {

            DB db = new DB();
            String username = userTextBox.Text;
            Program.username = userTextBox.Text;
            String password = passwordTextBox.Text;
            db.openConnection();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `login` WHERE `username` = @usn and `password` = @pass", db.GetConnection());


            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("เข้าสู่ระบบสำเร็จ", "OH MY CUP");
                db.closeConnection();
                this.Hide();
                productForm formx = new productForm();
                formx.Show();
            }
            else
            {
                MessageBox.Show("การเข้าสู่ระบบไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง", "OH MY CUP");
            }


        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.PasswordChar == '*')
            {
                string a = passwordTextBox.Text;
                passwordTextBox.PasswordChar = '\0';
            }
            else
            {
                passwordTextBox.PasswordChar = '*';
            }
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                loginButton_Click(loginButton, e);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            signupForm form = new signupForm();
            form.Show();
        }

        private void userTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 65 && (int)e.KeyChar <= 122) || ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8 || (int)e.KeyChar == 13)
            {
                e.Handled = false; // OS will handle this event.
            }
            else
            {
                e.Handled = true;  // OS will not handle this event.
                MessageBox.Show("กรุณาตรวจสอบอักขระ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 65 && (int)e.KeyChar <= 122) || ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8 || (int)e.KeyChar == 13)
            {
                e.Handled = false; // OS will handle this event.
            }
            else
            {
                e.Handled = true;  // OS will not handle this event.
                MessageBox.Show("กรุณาตรวจสอบอักขระ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
