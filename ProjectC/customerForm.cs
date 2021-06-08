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
using System.Configuration;

namespace ProjectC
{
    public partial class customerForm : Form
    {
        public customerForm()
        {
            InitializeComponent();
        }
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cafe_ohmycub;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            optionForm form = new optionForm();
            form.Show();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (passTextBox.PasswordChar == '*')
            {
                string a = passTextBox.Text;
                passTextBox.PasswordChar = '\0';
            }
            else
            {
                passTextBox.PasswordChar = '*';
            }
            
        }


        private void customerForm_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = DatabaseConnection();
            
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM login WHERE username = '" + Program.username + "'", conn);
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                userTextBox.Text = read.GetString("username");
                passTextBox.Text = read.GetString("password");
                callTextBox.Text = read.GetString("phonenumber");
            }
            conn.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = DatabaseConnection();

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE `login` SET `username`='"+userTextBox.Text+ "',`password`='" + passTextBox.Text + "',`phonenumber`='" + callTextBox.Text + "' WHERE username = '"+Program.username+"'", conn);
            MySqlDataReader read = cmd.ExecuteReader();
            MessageBox.Show("แก้ไขข้อมูลเรียบร้อยแล้ว", "OH MY CUP");
            conn.Close();
            Program.username = userTextBox.Text;
            this.Hide();
            optionForm form = new optionForm();
            form.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void callTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void emailTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
