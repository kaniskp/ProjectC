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
    public partial class historyForm : Form
    {
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cafe_ohmycub;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        public historyForm()
        {
            InitializeComponent();
        }
        int total = 0;
        private void historyForm_Load(object sender, EventArgs e)
        {
            guna2DateTimePicker1.Value = System.DateTime.Now;
            guna2DateTimePicker2.Value = System.DateTime.Now;
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            //cmd.CommandText = "SELECT No AS 'ลำดับ',Name AS 'ผู้สั่งซื้อ', Menu AS 'รายการที่สั่ง',type AS 'ชนิดเครื่องดื่ม', price AS 'ราคา'  FROM sorderbuyer";
            cmd.CommandText = "SELECT `No`, `Date`, `Menu`, `type`, `price`, `Time` FROM sales_history";
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
        private void SearchButton_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = DatabaseConnection();

            DataSet ds = new DataSet();

            conn.Open();
            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM sales_history WHERE Date between @date1 and @date2 ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.SelectCommand.Parameters.AddWithValue("@date1", guna2DateTimePicker1.Value.ToString("dd / MM / yyyy"));
            adapter.SelectCommand.Parameters.AddWithValue("@date2", guna2DateTimePicker2.Value.ToString("dd / MM / yyyy"));
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            total = 0; //ตัวแปรยอดรวมจำนวนเงิน
            conn.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                total = total + int.Parse(read.GetString(5));
            }
            TotalTextBox.Text = $"{total}";
            conn.Close();
        }

    }
}
