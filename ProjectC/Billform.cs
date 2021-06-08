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
    public partial class Billform : Form
    {
        List<Bill> allbill = new List<Bill>();
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cafe_ohmycub;";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }
        private void showorder1()
        {
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT No AS 'ลำดับ',Name AS 'ผู้สั่งซื้อ', Menu AS 'รายการที่สั่ง',type AS 'ชนิดเครื่องดื่ม', price AS 'ราคา' FROM sorderbuyer";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        private void delorder1()
        {
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM `sorderbuyer`";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
        }
        public Billform()
        {
            InitializeComponent();
            label4.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            productForm form = new productForm();
            form.Show();
        }
        private void moneyBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != (char)Keys.Enter))
            {
                MessageBox.Show("กรุณาใส่จำนวนเงินตัวเลข", "OH MY CUP");
                 e.Handled = true;
            }
            if(e.KeyChar == (char)Keys.Enter)
            {
                totalButton_Click(totalButton, e);
            }
            
        }

        private void Billform_Load(object sender, EventArgs e)
        {
            showorder1();
            allbill.Clear();
            MySqlConnection conn = DatabaseConnection();
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM sorderbuyer", conn);
            conn.Open();
            MySqlDataReader adapter = cmd1.ExecuteReader();
            Program.sum = 0;
            while (adapter.Read())
            {
                Program.sum = Program.sum + int.Parse(adapter.GetString("price"));
                Program.menu = adapter.GetString("Menu").ToString();
                Program.type = adapter.GetString("type").ToString();
                Program.price = adapter.GetString("price").ToString();
                Bill item = new Bill()
                {
                    menu = Program.menu,
                    type = Program.type,
                    price = Program.price
                };
                allbill.Add(item);

            }

            guna2TextBox1.Text = Program.sum.ToString();
            conn.Close();
           
            
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void totalButton_Click(object sender, EventArgs e)
        {
                if(moneyBox.Text == "")
                {
                    MessageBox.Show("กรุณากรอกจำนวนเงินให้เรียบร้อยด้วย ", "OH MY CUB");
                }
                else
                {
                    double givemoney = double.Parse(moneyBox.Text);
                    if (givemoney >= Program.sum)
                    {
                        string date = DateTime.Now.ToString("dd / MM / yyyy");
                        string time = DateTime.Now.ToString("h:mm tt");
                        MySqlConnection conn = DatabaseConnection();
                        conn.Open();
                        MySqlCommand cmd;
                        cmd = conn.CreateCommand();
                        cmd.CommandText = $"SELECT * FROM sorderbuyer WHERE Name =\"{ Program.username}\"";
                        MySqlDataReader row = cmd.ExecuteReader();
                        if (row.HasRows)
                        {
                            while (row.Read())
                            {
                                MySqlConnection conn1 = DatabaseConnection();
                                conn1.Open();
                                MySqlCommand command1 = new MySqlCommand("INSERT INTO `sales_history`(`Date`,`Name`, `Menu`,`Type`,`Price`,`Time`) VALUES ('" + date + "','" + Program.username + "','" + row.GetString(2) + "','" + row.GetString(3) + "','" + row.GetString(4) + "','" + time + "')", conn1);
                                command1.ExecuteReader();
                                conn1.Close();
                            }
                        }
                        changemoneyBox.Text = Convert.ToString(givemoney - Program.sum);
                        printPreviewDialog1.Document = printDocument1;
                        printPreviewDialog1.ShowDialog();
                        //DB db = new DB();
                        delorder1();
                        this.Hide();
                        productForm form = new productForm();
                        form.Show();

                    }
                    else if (givemoney < Program.sum)
                    {
                        MessageBox.Show("จำนวนเงินไม่พียงพอ", "OH MY CUP");
                    }
                } 
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("ใบเสร็จ", new Font("supermarket", 20, FontStyle.Bold), Brushes.Black, new Point(400, 50));
            e.Graphics.DrawString("OH MY CUB", new Font("supermarket", 24, FontStyle.Bold), Brushes.Black, new Point(355, 90));
            e.Graphics.DrawString("พิมพ์เมื่อ " + System.DateTime.Now.ToString("dd/MM/yyyy HH : mm : ss น."), new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(525, 150));
            e.Graphics.DrawString("ข้อมูลร้าน : กณิศ กองพอด 0951893539", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 150));
            e.Graphics.DrawString("              หอพักชายที่ 27 มหาวิทยาลัยขอนแก่น เลขที่ 123 หมู่ 16 ถนนมิตรภาพ ", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 195));
            e.Graphics.DrawString("              ตำบลในเมือง อำเภอเมืองขอนแก่น จังหวัดขอนแก่น 40002", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 240));
            e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 285));
            e.Graphics.DrawString("    ลำดับ          ชื่อเมนู                                    ชนิดเครื่องดื่ม                ราคา (บาท)", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 315));
            e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 345));
            int y = 345;
            int number = 1;
            foreach(var i in allbill)
            {
                y = y + 35;
                e.Graphics.DrawString("   "+number.ToString(), new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(100, y));
                e.Graphics.DrawString("   " + i.menu, new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(190, y));//icecream
                e.Graphics.DrawString("   " + i.type, new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(480, y));//iced
                e.Graphics.DrawString("   " + i.price, new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(680, y));//20
                number = number + 1;
            }
            e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, y+30));
            e.Graphics.DrawString("รวมทั้งสิ้น         "+ Program.sum.ToString() + " บาท", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(570, (y + 30)+45));
            e.Graphics.DrawString("ชื่อผู้ให้บริการ        " + Program.username.ToString(), new Font("supermarket", 16, FontStyle.Bold), Brushes.Black, new Point(80, (y + 30) + 45));
            e.Graphics.DrawString("รับเงิน            " + moneyBox.Text + " บาท", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(570, ((y + 30) + 45) +45));
            e.Graphics.DrawString("เงินทอน           " + changemoneyBox.Text + " บาท", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(570, (((y + 30) + 45) + 45) +45));
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}
