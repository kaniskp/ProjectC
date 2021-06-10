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
    public partial class edititemsForm : Form
    {
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cafe_ohmycub;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        public edititemsForm()
        {
            InitializeComponent();
            
        }
        private void showmenu()
        {
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM "+Program.outputDB+"";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            optionForm form = new optionForm();
            form.Show();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            label3.Show();
            label4.Show();
            textBox3.Show();
            textBox4.Show();
            Program.outputDB = "coffee";
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM coffee";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            label2.Text = "Hot";
            label3.Show();
            label4.Show();
            textBox3.Show();
            textBox4.Show();
            Program.outputDB = "milk";
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM milk";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            label2.Text = "Hot";
            label3.Show();
            label4.Show();
            textBox3.Show();
            textBox4.Show();
            Program.outputDB = "tea";
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM tea";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (Program.outputDB == "water")
            {
                MySqlConnection conn = DatabaseConnection();
                String sql1 = "INSERT INTO " + Program.outputDB + "(Namemenu, Iced) VALUES('" + textBox1.Text + "' ,'" + textBox2.Text + "' )";
                MySqlCommand cmd = new MySqlCommand(sql1, conn);
                conn.Open();
                int rowss = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowss > 0)
                {
                    MessageBox.Show("เพิ่มข้อมูลสำเร็จ");
                    showmenu();
                }
            }
            else if (Program.outputDB == "dessert")
            {
                MySqlConnection conn = DatabaseConnection();
                String sql1 = "INSERT INTO " + Program.outputDB + "(Namemenu, dessert) VALUES('" + textBox1.Text + "' ,'" + textBox2.Text + "' )";
                MySqlCommand cmd = new MySqlCommand(sql1, conn);
                conn.Open();
                int rowss = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowss > 0)
                {
                    MessageBox.Show("เพิ่มข้อมูลสำเร็จ");
                    showmenu();
                }
            }
            else
            {
                MySqlConnection conn1 = DatabaseConnection();
                String sql = "INSERT INTO " + Program.outputDB + "(Namemenu,Hot, Iced, Freppe) VALUES('" + textBox1.Text + "' ,'" + textBox2.Text + "' , '" + textBox2.Text + "', '" + textBox3.Text + "')";
                MySqlCommand cmd1 = new MySqlCommand(sql, conn1);
                conn1.Open();
                int rows = cmd1.ExecuteNonQuery();
                conn1.Close();
                if (rows > 0)
                {
                    MessageBox.Show("เพิ่มข้อมูลสำเร็จ");
                    showmenu();
                }
            }
            
        }

        private void edititemsForm_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (Program.outputDB == "water")
            {
                int selectedRow = dataGridView1.CurrentCell.RowIndex;
                Console.WriteLine(selectedRow);
                string nameforedit = dataGridView1.Rows[selectedRow].Cells["Namemenu"].FormattedValue.ToString();
                Console.WriteLine(nameforedit);
                MySqlConnection conn = DatabaseConnection();
                String sql = "UPDATE " + Program.outputDB + " SET Iced ='" + textBox2.Text + "'WHERE Namemenu = '" + nameforedit + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("แก้ไขข้อมูลสำเร็จ");
                    showmenu();
                }
            }
            else if (Program.outputDB == "dessert")
            {
                int selectedRow = dataGridView1.CurrentCell.RowIndex;
                Console.WriteLine(selectedRow);
                string nameforedit = dataGridView1.Rows[selectedRow].Cells["Namemenu"].FormattedValue.ToString();
                Console.WriteLine(nameforedit);
                MySqlConnection conn = DatabaseConnection();
                String sql = "UPDATE " + Program.outputDB + " SET dessert ='" + textBox2.Text + "'WHERE Namemenu = '" + nameforedit + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("แก้ไขข้อมูลสำเร็จ");
                    showmenu();
                }
            }
            else
            {
                int selectedRow = dataGridView1.CurrentCell.RowIndex;
                Console.WriteLine(selectedRow);
                string nameforedit = dataGridView1.Rows[selectedRow].Cells["Namemenu"].FormattedValue.ToString();
                Console.WriteLine(nameforedit);
                MySqlConnection conn = DatabaseConnection();
                String sql = "UPDATE " + Program.outputDB + " SET  Hot='" + textBox2.Text + "',Iced='" + textBox3.Text + "',Freppe='" + textBox4.Text + "' WHERE Namemenu = '" + nameforedit + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("แก้ไขข้อมูลสำเร็จ");
                    showmenu();
                }
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Program.outputDB == "water")
            {
                dataGridView1.CurrentRow.Selected = true;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Namemenu"].FormattedValue.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Iced"].FormattedValue.ToString();
            }
            else if (Program.outputDB == "dessert")
            {
                dataGridView1.CurrentRow.Selected = true;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Namemenu"].FormattedValue.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["dessert"].FormattedValue.ToString();
            }
            else
            {
                dataGridView1.CurrentRow.Selected = true;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Namemenu"].FormattedValue.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Hot"].FormattedValue.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Iced"].FormattedValue.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["Freppe"].FormattedValue.ToString();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (Program.outputDB == "water")
            {
                int selectedRow = dataGridView1.CurrentCell.RowIndex;
                string namefordel = dataGridView1.Rows[selectedRow].Cells["Namemenu"].FormattedValue.ToString();
                MySqlConnection conn = DatabaseConnection();
                String sql = "DELETE FROM " + Program.outputDB + "  WHERE Namemenu = '" + namefordel + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว");
                    showmenu();
                }
            }
            else if (Program.outputDB == "water")
            {
                int selectedRow = dataGridView1.CurrentCell.RowIndex;
                string namefordel = dataGridView1.Rows[selectedRow].Cells["Namemenu"].FormattedValue.ToString();
                MySqlConnection conn = DatabaseConnection();
                String sql = "DELETE FROM " + Program.outputDB + "  WHERE Namemenu = '" + namefordel + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว");
                    showmenu();
                }
            }
            else
            {
                int selectedRow = dataGridView1.CurrentCell.RowIndex;
                string namefordel = dataGridView1.Rows[selectedRow].Cells["Namemenu"].FormattedValue.ToString();
                MySqlConnection conn = DatabaseConnection();
                String sql = "DELETE FROM " + Program.outputDB + "  WHERE Namemenu = '" + namefordel + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว");
                    showmenu();
                }
            }

            
        }

        private void guna2CircleButton5_Click(object sender, EventArgs e)
        {
            label2.Text = "Iced";
            label3.Hide();
            label4.Hide();
            textBox3.Hide();
            textBox4.Hide();
            Program.outputDB = "water";
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM water";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void guna2CircleButton6_Click(object sender, EventArgs e)
        {
            label2.Text = "dessert";
            label3.Hide();
            label4.Hide();
            textBox3.Hide();
            textBox4.Hide();
            Program.outputDB = "dessert";
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM dessert";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
    }
}
