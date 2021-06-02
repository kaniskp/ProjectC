﻿using System;
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
            MySqlCommand command = new MySqlCommand("INSERT INTO `login`(`username`, `password`, `email`, `phonenumber`) VALUES (@usn, @pass, @email, @pnb )", db.GetConnection());

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = userBox.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passBox.Text;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailBox.Text;
            command.Parameters.Add("@pnb", MySqlDbType.VarChar).Value = phonenumBox.Text;
            //เปิดดาต้า
            db.openConnection();

            if (!checkTextBoxValues())
            {
                
                    if (passBox.Text.Equals(passBox2.Text))
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
                            }
                            else
                            {
                                MessageBox.Show("ERROR", "OH MY CUP");
                            }
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
            String email = emailBox.Text;
            String phonenum = phonenumBox.Text;
            

            if (user.Equals("") || pass.Equals("") ||
                email.Equals("") || phonenum.Equals(""))
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
    }
}