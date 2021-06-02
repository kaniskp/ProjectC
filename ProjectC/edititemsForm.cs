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
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=menu;";

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
            MySqlConnection conn = DatabaseConnection();
            String sql = "INSERT INTO " + Program.outputDB + "(Namemenu,Hot, Iced, Freppe) VALUES('" + textBox1.Text + "' ,'" + textBox2.Text + "' , '" + textBox2.Text + "', '" + textBox3.Text + "')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("เพิ่มข้อมูลสำเร็จ");
                showmenu();
            }
        }

        private void edititemsForm_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Namemenu"].FormattedValue.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Hot"].FormattedValue.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Iced"].FormattedValue.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["Freppe"].FormattedValue.ToString();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
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
}
