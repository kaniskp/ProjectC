using MySql.Data;
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
    public partial class productForm : Form
    {
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=menu;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        string pricefromdb;
        public productForm()
        {
            InitializeComponent();
            panel7.Height = button1.Height;
            panel7.Top = button1.Top;
            label1.Hide();
            label2.Hide();
            label3.Hide();
        }

        private void showorder()
        {
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT No AS 'ลำดับ',Name AS 'ผู้สั่งซื้อ', Menu AS 'รายการที่สั่ง',type AS 'ชนิดเครื่องดื่ม', price AS 'ราคา'  FROM sorderbuyer";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            DialogResult result= MessageBox.Show("ต้องการออกจากระบบใช่หรือไม่","OH MY CUP",MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                this.Hide();
                loginForm form = new loginForm();
                form.Show();
            }
            
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox2.Text == "Espresso (เอสเพรสโซ่)")
            {
                picturemenuBox.Image = Resource1.Espresso;

            }
            if (guna2ComboBox2.Text == "Americano (อเมริกาโน่)")
            {
                picturemenuBox.Image = Resource1.Americano;
            }
            if (guna2ComboBox2.Text == "Cappuccino (คาปูชิโน)")
            {
                picturemenuBox.Image = Resource1.Cappuccino;
            }
            if (guna2ComboBox2.Text == "Macchiato (มัคคิอาโต้)")
            {
                picturemenuBox.Image = Resource1.Macchiato;
            }
            if (guna2ComboBox2.Text == "Latte (ลาเต้)")
            {
                picturemenuBox.Image = Resource1.Latte;
            }
            if (guna2ComboBox2.Text == "Tea(ชา)")
            {
                picturemenuBox.Image = Resource1.Tea;
            }
            if (guna2ComboBox2.Text == "Green Tea With Milk(ชาเขียวนม)")
            {
                picturemenuBox.Image = Resource1.Green_Tea_With_Milk;
            }
            if (guna2ComboBox2.Text == "Black tea(ชาดำ)")
            {
                picturemenuBox.Image = Resource1.Black_tea;
            }
            if (guna2ComboBox2.Text == "Tea With Milk(ชานม)")
            {
                picturemenuBox.Image = Resource1.Tea_With_Milk;
            }
            if (guna2ComboBox2.Text == "Lemon tea(ชามะนาว)")
            {
                picturemenuBox.Image = Resource1.Lemon_tea;
            }
            if (guna2ComboBox2.Text == "Fresh Milk (นมสด)")
            {
                picturemenuBox.Image = Resource1.Fresh_Milk;
            }
            if (guna2ComboBox2.Text == "Chocolate (ช็อกโกแลต)")
            {
                picturemenuBox.Image = Resource1.Chocolate;
            }
            if (guna2ComboBox2.Text == "Strawberry Cheesecake (สตรอเบอร์รี่ชีสเค้ก)")
            {
                picturemenuBox.Image = Resource1.Strawberry_Cheesecake;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.outputDB = "tea";
            label2.Show();
            label1.Hide();
            label3.Hide();
            panel7.Height = button2.Height;
            panel7.Top = button2.Top;
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM tea";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds, "tea");
            conn.Close();
            guna2ComboBox2.DataSource = ds.Tables["tea"];
            guna2ComboBox2.DisplayMember = "Namemenu";
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Program.outputDB = "coffee";
            label1.Show();
            label2.Hide();
            label3.Hide();
            panel7.Height = button1.Height;
            panel7.Top = button1.Top;
            guna2ComboBox2.ResetText();
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM coffee";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds, "coffee");
            conn.Close();
            guna2ComboBox2.DataSource = ds.Tables["coffee"];
            guna2ComboBox2.DisplayMember = "Namemenu";

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Program.outputDB = "milk";
            label1.Hide();
            label2.Hide();
            label3.Show();
            panel7.Height = button3.Height;
            panel7.Top = button3.Top;
            guna2ComboBox2.ResetText();
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM milk";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds, "milk");
            conn.Close();
            guna2ComboBox2.DataSource = ds.Tables["milk"];
            guna2ComboBox2.DisplayMember = "Namemenu";
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

       

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            optionForm form = new optionForm();
            form.Show();
        }

        private void productForm_Load(object sender, EventArgs e)
        {
            showorder();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel7.Height = button4.Height;
            panel7.Top = button4.Top;
            this.Hide();
            Billform form = new Billform();
            form.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            //int typesmenu = passwordTextBox.Text;
            MySqlConnection conn = DatabaseConnection();
            conn.Open();
            MySqlCommand command = new MySqlCommand("INSERT INTO `sorderbuyer`(`Name`, `Menu`,`type`,`price`) VALUES ('"+Program.username+"','"+guna2ComboBox2.Text+"','"+guna2ComboBox1.Text+"','"+ pricefromdb +"')", conn);
            command.ExecuteReader();
            conn.Close();
            showorder();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox2.Text != "")
            {
                MySqlConnection conn = DatabaseConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM "+ Program.outputDB +" WHERE Namemenu = '"+ guna2ComboBox2.Text +"'", conn);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    pricefromdb = read.GetString(guna2ComboBox1.Text);
                }
                conn.Close();

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.CurrentCell.RowIndex;
            int deletorder = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["No"].Value);
            MySqlConnection conn = DatabaseConnection();
            String sql = "DELETE FROM sorderbuyer WHERE No = '" + deletorder + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("ลบรายการที่สั่งเรียบร้อยแล้ว","OH MY CUP");
                showorder();
            }
        }
    }
}
