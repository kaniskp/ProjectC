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
using System.Text.RegularExpressions;

namespace ProjectC
{
    public partial class signupForm : Form
    {
        public signupForm()
        {
            InitializeComponent();
            
            
        }

        private void ShowPW1_CheckedChanged(object sender, EventArgs e)
        {
            if (showPW1.Checked)
            {
                string a = passBox.Text;
                passBox.PasswordChar = '\0';
                passBox2.PasswordChar = '\0';

            }
            else
            {
                passBox.PasswordChar = '*';
                passBox2.PasswordChar = '*';
            }

        }

        
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //สร้าง user
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `login`(`username`, `password`, `phonenumber`) VALUES (@usn, @pass, @pnb )", db.GetConnection());

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = userBox.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passBox.Text;
            command.Parameters.Add("@pnb", MySqlDbType.VarChar).Value = phonenumBox.Text;
            //เปิดดาต้า

            db.openConnection();
            Regex r = new Regex(@"^[0-9]{10}$");
           
                if (!checkTextBoxValues())
            {
                
                    if (passBox.Text.Equals(passBox2.Text))
                    {
                        if (r.IsMatch(phonenumBox.Text))
                        {
                           
                            if (checkuser())
                            {
                                MessageBox.Show("This username already has information.", "OH MY CUP");
                            }

                            else
                            {
                                if (command.ExecuteNonQuery() == 1)
                                {
                                    MessageBox.Show("ACCOUNT CREATED", "OH MY CUP");
                                    this.Hide();
                                    loginForm form = new loginForm();
                                    form.Show();
                                }
                                else
                                {
                                    MessageBox.Show("ERROR", "OH MY CUP");
                                }
                            }
                        }  
                        else
                        {
                            MessageBox.Show("กรุณากรอกเบอร์โทรให้ถูกต้อง ", "OH MY CUP");
                        }

                    }
                    else
                    {
                       MessageBox.Show("Passwords do not match.", "OH MY CUP");
                    }
                 
            }
            else
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบก่อนสร้างบัญชี");
            }

            //ปิด ดาต้า
            db.closeConnection();
        }
        public Boolean checkuser()
        {
            DB db = new DB();
            String username = userBox.Text;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `login` WHERE `username` = @usn", db.GetConnection());


            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = username;

            adapter.SelectCommand = command;

            adapter.Fill(table);
            // เช็ค user ในดาต้า ว่ามีuser ซ้ำไหม
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        public Boolean checkTextBoxValues()
        {
            String user = userBox.Text;
            String pass = passBox.Text;
            String phonenum = phonenumBox.Text;
            

            if (user.Equals("") || pass.Equals("") || phonenum.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm form = new loginForm();
            form.Show();
        }

        private void passBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 65 && (int)e.KeyChar <= 122) || ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8 || (int)e.KeyChar == 13)
            {
                e.Handled = false; 
            }
            else
            {
                e.Handled = true;  
                MessageBox.Show("กรุณาตรวจสอบอักขระ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void userBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 65 && (int)e.KeyChar <= 122) || ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8 || (int)e.KeyChar == 13)
            {
                e.Handled = false; 
            }
            else
            {
                e.Handled = true;  
                MessageBox.Show("กรุณาตรวจสอบอักขระ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void passBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 65 && (int)e.KeyChar <= 122) || ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8 || (int)e.KeyChar == 13)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("กรุณาตรวจสอบอักขระ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        

        private void phonenumBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
