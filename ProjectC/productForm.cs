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
using System.Timers;
namespace ProjectC
{
    public partial class productForm : Form
    {
        bool whenclickdessert = false;
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cafe_ohmycub;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        string pricefromdb;
        public productForm()
        {
            InitializeComponent();
            panel7.Height = guna2Button4.Height;
            panel7.Top = guna2Button4.Top;
            label1.Text = "";
        }

        private void showorder()
        {
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            //cmd.CommandText = "SELECT No AS 'ลำดับ',Name AS 'ผู้สั่งซื้อ', Menu AS 'รายการที่สั่ง',type AS 'ชนิดเครื่องดื่ม', price AS 'ราคา'  FROM sorderbuyer";
            cmd.CommandText = "SELECT `No`, `Name`, `Menu`, `type`, `price` FROM sorderbuyer";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
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
            if (guna2ComboBox2.Text == "mineral water(น้ำแร่)")
            {
                picturemenuBox.Image = Resource1.minerral_water; 
            }
            if (guna2ComboBox2.Text == "water(น้ำดื่ม)")
            {
                picturemenuBox.Image = Resource1.water;
            }
            if (guna2ComboBox2.Text == "Chocolate Banana Waffles")
            {
                picturemenuBox.Image = Resource1.Chocolate_Banana_Waffles;
            }
            if (guna2ComboBox2.Text == "Coconut Cake")
            {
                picturemenuBox.Image = Resource1.Coconut_Cake;
            }
            if (guna2ComboBox2.Text == "cookie")
            {
                picturemenuBox.Image = Resource1.cookie;
            }
            if (guna2ComboBox2.Text == "fruit tart")
            {
                picturemenuBox.Image = Resource1.fruit_tart;
            }
            if (guna2ComboBox2.Text == "soft chocolate cake")
            {
                picturemenuBox.Image = Resource1.soft_chocolate_cake;
            }
            if (guna2ComboBox2.Text == "Souffle Cheesecake")
            {
                picturemenuBox.Image = Resource1.Souffle_Cheesecake;
            }
            if (guna2ComboBox2.Text == "Almon milk")
            {

            }
        } 
        
        private void productForm_Load(object sender, EventArgs e)
        {
            showorder();
        }

        private void Backbutton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("ต้องการออกจากระบบใช่หรือไม่", "OH MY CUP", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                loginForm form = new loginForm();
                form.Show();
            }
        }

        private void Orderbutton_Click(object sender, EventArgs e)
        {
            if (whenclickdessert == false && guna2ComboBox1.Text == "dessert")
            {
                MessageBox.Show("ประเภทอาหารนี้ สำหรับเมนู Dessert เท่านั้น");
                guna2ComboBox1.SelectedIndex = 0;
            }
            else if (guna2ComboBox2.Text != "")
            {
                MySqlConnection conn = DatabaseConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + Program.outputDB + " WHERE Namemenu = '" + guna2ComboBox2.Text + "'", conn);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    pricefromdb = read.GetString(guna2ComboBox1.Text).ToString();
                }
                conn.Close();
                if (whenclickdessert == false)
                {
                    DB db = new DB();
                    //int typesmenu = passwordTextBox.Text;
                    MySqlConnection conn1 = DatabaseConnection();
                    conn1.Open();

                    string time = guna2DateTimePicker1.Value.ToString();
                    string sql = "INSERT INTO `sorderbuyer`(`Name`, `Menu`,`type`,`price`) VALUES ('" + Program.username + "','" + guna2ComboBox2.Text + "','" + guna2ComboBox1.Text + "','" + pricefromdb + "')";
                    MySqlCommand command = new MySqlCommand(sql, conn1);
                    //MessageBox.Show(sql);
                    command.ExecuteReader();
                    conn1.Close();
                    showorder();
                }
                else if (whenclickdessert == true)
                {
                    DB db = new DB();
                    //int typesmenu = passwordTextBox.Text;
                    MySqlConnection conn2 = DatabaseConnection();
                    conn2.Open();

                    string time = guna2DateTimePicker1.Value.ToString();
                    string sql = "INSERT INTO `sorderbuyer`(`Name`, `Menu`,`type`,`price`) VALUES ('" + Program.username + "','" + guna2ComboBox2.Text + "','" + guna2ComboBox1.Text + "','" + pricefromdb + "')";
                    MySqlCommand command = new MySqlCommand(sql, conn2);
                    //MessageBox.Show(sql);
                    command.ExecuteReader();
                    conn2.Close();
                    showorder();
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
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
                MessageBox.Show("ลบรายการที่สั่งเรียบร้อยแล้ว", "OH MY CUP");
                showorder();
            }
        }

        private void CheckbillButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Billform form = new Billform();
            form.Show();
        }

        private void optionButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            optionForm form = new optionForm();
            form.Show();
        }

        private void CofButton_Click(object sender, EventArgs e)
        {
            guna2ComboBox1.Enabled = true;
            guna2ComboBox1.Text = "";
            Program.outputDB = "coffee";
            label1.Text = "COFFEE";
            panel7.Height = guna2Button4.Height;
            panel7.Top = guna2Button4.Top;
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
            guna2ComboBox1.SelectedIndex = 0;
            whenclickdessert = false;
        }

        private void TeaButton_Click(object sender, EventArgs e)
        {
            guna2ComboBox1.Enabled = true;
            guna2ComboBox1.Text = "";
            Program.outputDB = "tea";
            label1.Text = "TEA";
            panel7.Height = guna2Button5.Height;
            panel7.Top = guna2Button5.Top;
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
            guna2ComboBox1.SelectedIndex = 0;
            whenclickdessert = false;
        }

        private void MilkButton_Click(object sender, EventArgs e)
        {
            guna2ComboBox1.Enabled = true;
            guna2ComboBox1.Text = "";
            Program.outputDB = "milk";
            label1.Text = "MILK";
            panel7.Height = guna2Button6.Height;
            panel7.Top = guna2Button6.Top;
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
            guna2ComboBox1.SelectedIndex = 0;
            whenclickdessert = false;
        }

        private void watButton_Click(object sender, EventArgs e)
        {
            guna2ComboBox1.Text = "Iced";
            guna2ComboBox1.Enabled = false;
            Program.outputDB = "water";
            label1.Text = "WATER";
            panel7.Height = guna2Button7.Height;
            panel7.Top = guna2Button7.Top;
            guna2ComboBox2.ResetText();
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM water";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds, "water");
            conn.Close();
            guna2ComboBox2.DataSource = ds.Tables["water"];
            guna2ComboBox2.DisplayMember = "Namemenu";
            whenclickdessert = false;
        }

        private void DessButton_Click(object sender, EventArgs e)
        {
            guna2ComboBox1.Enabled = false;
            Program.outputDB = "dessert";
            label1.Text = "DESSERT";
            panel7.Height = guna2Button8.Height;
            panel7.Top = guna2Button8.Top;
            guna2ComboBox2.ResetText();
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM dessert";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds, "dessert");
            conn.Close();
            guna2ComboBox2.DataSource = ds.Tables["dessert"];
            guna2ComboBox2.DisplayMember = "Namemenu";
            guna2ComboBox1.Text = "dessert";
            whenclickdessert = true;
        }
    }
}
