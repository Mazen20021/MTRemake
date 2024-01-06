using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Threading;
namespace MTRemake
{
    public partial class SignUp : Form
    {
        static private int ID = 0;
        static private string database = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ATM.db");
        SQLiteConnection connection = new SQLiteConnection("Data Source=" + database);
        Thread threads;
        public SignUp()
        {
            InitializeComponent();
            open_connection();
        }
        public String save_email()
        {
            return EmailBox.Text;
        }
        public String save_pass()
        {
            return PassBox.Text;
        }
        private void open_connection()
        {
           if(connection.State == System.Data.ConnectionState.Closed) 
            { 
                connection.Open(); 
            }     
        }
        private void IDGenerator()
        {
            int random_num = 0;
            Random r = new Random();
            do
            {
                random_num = r.Next(100000);
            } while (ID == random_num);
            ID = Math.Abs(random_num);
        }
        private void Mainpage()
        {
            StartPage m = new StartPage();
            Application.Run(m);
        }
        private void main_Page()
        {
            Close();
            threads = new Thread(Mainpage);
            threads.SetApartmentState(ApartmentState.STA);
            threads.Start();
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            IDGenerator();
            open_connection();
            try
            {
                string sql = "INSERT INTO Users (Email, Pass, Name, ID, Reg_Date , Balance , Dept , Picture ,BIN) VALUES (@Em, @Ps, @Name, @ID, @RD , @B , @Dpt, @Pic,@Bin)";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Em", EmailBox.Text);
                cmd.Parameters.AddWithValue("@Ps", PassBox.Text);
                cmd.Parameters.AddWithValue("@Name", NameBox.Text);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@RD", DateTime.Now.Date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@B", 0);
                cmd.Parameters.AddWithValue("@Dpt", 0);
                cmd.Parameters.AddWithValue("@Pic", null);
                cmd.Parameters.AddWithValue("@Bin", Bin_Box.Text);
                cmd.ExecuteNonQuery();
                main_Page();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sign Up failed due to User Existed Or DataBase Error: " + ex.Message, "User Not Added", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PassBox_TextChanged(object sender, EventArgs e)
        {
            if (PassBox.Text == "Password" || PassBox.Text == "")
            {
                PassBox.UseSystemPasswordChar = false;
            }
            else
            {
                PassBox.UseSystemPasswordChar = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(Bin_Box.Text == "Bin" || Bin_Box.Text == "")
            {
                Bin_Box.UseSystemPasswordChar = false;
            }
            else
            {
                Bin_Box.UseSystemPasswordChar = true;
            }
        }
        private void login_page()
        {
            StartPage st = new StartPage();
            Application.Run(st);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Close();
            threads = new Thread(login_page);
            threads.SetApartmentState(ApartmentState.STA);
            threads.Start();
        }
    }
}
