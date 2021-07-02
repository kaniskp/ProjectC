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
        List<Bill> allbillhis = new List<Bill>();
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
            cmd.CommandText = "SELECT `No`, `Date`, `Menu`, `type`, `price`, `Name` FROM sales_history";
            //cmd.CommandText = "SELECT `No`, `Date`, `Menu`, `type`, `price`, `Name` FROM sales_history WHERE Name='" + Program.username + "'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            total = 0; //ตัวแปรยอดรวมทั้งหมดในการขาย
            conn.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                total = total + int.Parse(read.GetString(4));
            }
            TotalTextBox.Text = $"{total}";
            conn.Close();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            optionForm form = new optionForm();
            form.Show();
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if(searchtxtbox.Text != "")
            {
                MySqlConnection conn = DatabaseConnection();

                DataSet ds = new DataSet();

                conn.Open();
                MySqlCommand cmd;

                cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT * FROM sales_history WHERE Date between @date1 and @date2 and Name=@data OR Menu=@data ";

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.SelectCommand.Parameters.AddWithValue("@date1", guna2DateTimePicker1.Value.ToString("dd / MM / yyyy")); 
                adapter.SelectCommand.Parameters.AddWithValue("@date2", guna2DateTimePicker2.Value.ToString("dd / MM / yyyy"));
                adapter.SelectCommand.Parameters.AddWithValue("@data", searchtxtbox.Text);
                adapter.Fill(ds);
                conn.Close();
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                total = 0; //ตัวแปรยอดรวมของรายชื่อ
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    total = total + int.Parse(read.GetString(5));
                }
                TotalTextBox.Text = $"{total}";
                conn.Close();
            }
            else
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
                total = 0;//ตัวแปรยอดรวมของระหว่างวันที่
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
        private void loaddatahistory()
        {
            if (searchtxtbox.Text != "")
            {
                MySqlConnection conn = new MySqlConnection("host=127.0.0.1;username=root;password=;database=cafe_ohmycub;");
                conn.Open();

                MySqlCommand cmd;

                cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT * FROM sales_history WHERE Date between @date1 and @date2 and Name=@data OR Menu=@data ";

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand.Parameters.AddWithValue("@date1", guna2DateTimePicker1.Value.ToString("dd / MM / yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@date2", guna2DateTimePicker2.Value.ToString("dd / MM / yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@data", searchtxtbox.Text);

                MySqlDataReader adapter = cmd.ExecuteReader();

                while (adapter.Read())
                {
                    Program.menu = adapter.GetString("Menu");
                    Program.price = adapter.GetString("Price");
                    Program.date = adapter.GetString("Date");
                    Program.name = adapter.GetString("Name");

                    Bill item = new Bill()
                    {
                        menu = Program.menu,
                        price = Program.price,
                        date = Program.date,
                        name = Program.name,

                    };
                    allbillhis.Add(item);
                }
            }
            else
            {
                MySqlConnection conn = new MySqlConnection("host=127.0.0.1;username=root;password=;database=cafe_ohmycub;");
                conn.Open();

                MySqlCommand cmd;

                cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT * FROM sales_history WHERE Date between @date1 and @date2";

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand.Parameters.AddWithValue("@date1", guna2DateTimePicker1.Value.ToString("dd / MM / yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@date2", guna2DateTimePicker2.Value.ToString("dd / MM / yyyy"));

                MySqlDataReader adapter = cmd.ExecuteReader();

                while (adapter.Read())
                {
                    Program.menu = adapter.GetString("Menu");
                    Program.price = adapter.GetString("Price");
                    Program.date = adapter.GetString("Date");
                    Program.name = adapter.GetString("Name");

                    Bill item = new Bill()
                    {
                        menu = Program.menu,
                        price = Program.price,
                        date = Program.date,
                        name = Program.name,

                    };
                    allbillhis.Add(item);
                }
            }
        }
        
            

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Image logo = Resource1.cof;
            e.Graphics.DrawImage(logo, new Point(80, 40));
            e.Graphics.DrawString("ประวัติการขาย", new Font("supermarket", 20, FontStyle.Bold), Brushes.Black, new Point(365, 50));
            e.Graphics.DrawString("OH MY CUB", new Font("supermarket", 24, FontStyle.Bold), Brushes.Black, new Point(355, 90));
            e.Graphics.DrawString("พิมพ์เมื่อ " + System.DateTime.Now.ToString("dd/MM/yyyy HH : mm : ss น."), new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(525, 150));
            e.Graphics.DrawString("ข้อมูลร้าน : กณิศ กองพอด 0951893539", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 150));
            e.Graphics.DrawString("              หอพักชายที่ 27 มหาวิทยาลัยขอนแก่น เลขที่ 123 หมู่ 16 ถนนมิตรภาพ ", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 195));
            e.Graphics.DrawString("              ตำบลในเมือง อำเภอเมืองขอนแก่น จังหวัดขอนแก่น 40002", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 240));
            e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 285));
            e.Graphics.DrawString("    ลำดับ          ชื่อเมนู                       วันที่ขาย           ราคา (บาท)         ชื่อคนขาย", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 315));
            e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, 345));
            int y = 345;
            int number = 1;
            allbillhis.Clear();
            loaddatahistory();
            foreach (var i in allbillhis)
            {
                y = y + 35;
                e.Graphics.DrawString("   " + number.ToString(), new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(100, y));
                e.Graphics.DrawString("   " + i.menu, new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(190, y));//icecream
                e.Graphics.DrawString("   " + i.date, new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(370, y));//iced
                e.Graphics.DrawString("   " + i.price, new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(550, y));//20
                e.Graphics.DrawString("   " + i.name, new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(690, y));//20
                number = number + 1;
            }
            e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, y + 30));
            e.Graphics.DrawString("รวมทั้งสิ้น         " + TotalTextBox.Text + " บาท", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(570, (y + 30) + 45));
            
            

        }
    }
}
