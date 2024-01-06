using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data;
using System.Collections.Generic;
namespace MTRemake
{
    public partial class Mainpage : Form
    {
        static private string database = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ATM.db");
        SQLiteConnection connection = new SQLiteConnection("Data Source=" + database);
        String uBin = "";
        String uPass = "";
        public Mainpage(String Bin)
        {
            InitializeComponent();
            if (Bin.Length == 4)
            {
                uBin = Bin;
            }
            else
            {
                uPass = Bin;
            }
           
            setimage();
            open_connection();
            check_image();
            set_dept();
            connect_db();
        }
        private void setimage()
        {
            open_connection();
            if (uPass.Length > 4)
            {
                try
                {
                    String sql = "Select Picture from Users where Pass = @Bin";
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Bin", uPass);
                    SQLiteDataReader dr;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        byte[] pic = (byte[])dr["Picture"];
                        if (pic != null)
                        {
                            using (MemoryStream ms = new MemoryStream(pic))
                            {
                                UserPicture.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            UserPicture.Image = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (UserPicture.Image != null)
                    {
                        MessageBox.Show("Error in Picture in SQL : " + ex, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                try
                {
                    String sql = "Select Picture from Users where BIN= @Bin";
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Bin", uBin);
                    SQLiteDataReader dr;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        byte[] pic = (byte[])dr["Picture"];
                        if (pic != null)
                        {
                            using (MemoryStream ms = new MemoryStream(pic))
                            {
                                UserPicture.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            UserPicture.Image = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (UserPicture.Image != null)
                    {
                        MessageBox.Show("Error in Picture in SQL : " + ex, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
        private void open_connection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        private void connect_db()
        {
            open_connection();
            if (uPass.Length > 4)
            {
                try
                {
                    String sql = "Select Balance , Name , ID  from Users where Pass = @Bin";
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                    cmd.Parameters.AddWithValue("@Bin", uPass);

                    SQLiteDataReader dr;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ID_Text.Text = dr["ID"].ToString();
                        Name_Text.Text = dr["Name"].ToString();
                        Balance_Text.Text = dr["Balance"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in SQL While Connecting to DB : " + ex, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    String sql = "Select Balance , Name , ID from Users where BIN = @Bin";
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Bin", uBin);
                    SQLiteDataReader dr;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ID_Text.Text = dr["ID"].ToString();
                        Name_Text.Text = dr["Name"].ToString();
                        Balance_Text.Text = dr["Balance"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in SQL While Connecting to DB  : " + ex, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void Balance_bot_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
            set_dept();
            connect_db();
        }
        private void check_image()
        {
            if (UserPicture.Image == null)
            {
                label7.Visible = true;
            }
            else
            {
                label7.Visible = false;
            }
        }
        private void UserPicture_Click(object sender, EventArgs e)
        {
            open_connection();
            FileChooser.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png";
            if (FileChooser.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = FileChooser.FileName;

                // Load the image and display it in the PictureBox
                UserPicture.Image = Image.FromFile(filePath);
                byte[] imageBytes = File.ReadAllBytes(filePath);
                if (uPass.Length > 4)
                {
                    try
                    {
                        String sql = "Update  Users set Picture = @Img where Pass =@Bin";
                        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                        cmd.Parameters.AddWithValue("@Bin", uPass);
                        cmd.Parameters.AddWithValue("@Img", imageBytes);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error in Inserting Image into database Dueto: " + ex, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        String sql = "Update  Users set Picture = @Img where BIN=@Bin";
                        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                        cmd.Parameters.AddWithValue("@Bin", uBin);
                        cmd.Parameters.AddWithValue("@Img", imageBytes);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error in Inserting Image into database Dueto: " + ex, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        private void back()
        {
            StartPage mp = new StartPage();
            Application.Run(mp);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Thread Threads;
            Close();
            Threads = new Thread(back);
            Threads.SetApartmentState(ApartmentState.STA);
            Threads.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            set_table_dept();
            connect_db();
        }
        private void set_table_dept()
        {
            open_connection();
            try
            {
                string sql = "SELECT *FROM Dept";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(dr);
                            Dept_View.DataSource = dataTable;
                            Bin_Dept_text.Enabled = true;
                            ID_Dept_Text_Box.Enabled = true;
                        }
                        else
                        {
                            Bin_Dept_text.Enabled = false;
                            ID_Dept_Text_Box.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Loading Table Due to: " + ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void set_dept()
        {
            try
            {
                string sql = "select *from Dept";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    SQLiteDataReader dr;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (DBNull.Value.Equals(dr["ID"]) || dr["ID"].ToString().Equals("0"))
                        {
                            Dept_Text.Text = "No Dept Found";
                        }
                        else
                        {
                            Dept_Text.Text = "Dept Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Inserting Data in the Users_Table Due to: " + ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private String get_balance()
        {
            String balance = "";
            try
            {
                String sql = "Select Balance from Users where BIN =" + uBin;
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                SQLiteDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    balance = dr["Balance"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cannot get balance due to: " + ex, "ERROR IN DATABASE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return balance;
        }
        private void set_history(double added_money)
        {
            open_connection();
            try
            {
                String sql = "insert into History (ID , Name , Amount_Added , Add_Time , Amount_Taken , WithDraw_Time , Day , Balance) values(@ID,@Name,@AD,@AT,@ATaken,@WT,@Day,@Balance)";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", ID_Text.Text);
                cmd.Parameters.AddWithValue("@Name", Name_Text.Text);
                cmd.Parameters.AddWithValue("@AD", added_money.ToString());
                cmd.Parameters.AddWithValue("@AT", DateTime.Now.ToString("hh:mm:ss"));
                cmd.Parameters.AddWithValue("@ATaken", "00.00");
                cmd.Parameters.AddWithValue("@WT", "00:00:00");
                cmd.Parameters.AddWithValue("@Day", DateTime.Now.DayOfWeek.ToString() + " (" + DateTime.Now.ToString("dd:MM:yyyy") + ")");
                cmd.Parameters.AddWithValue("@Balance", get_balance());
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cannot Set History due to: " + ex, "ERROR IN DATABASE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void set_history_withdraw(double taken_money)
        {
            open_connection();
            try
            {
                String sql = "insert into History (ID , Name , Amount_Added , Add_Time , Amount_Taken , WithDraw_Time , Day , Balance) values(@ID,@Name,@AD,@AT,@ATaken,@WT,@Day,@Balance)";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", ID_Text.Text);
                cmd.Parameters.AddWithValue("@Name", Name_Text.Text);
                cmd.Parameters.AddWithValue("@AD", "00.00");
                cmd.Parameters.AddWithValue("@AT", "00:00:00");
                cmd.Parameters.AddWithValue("@ATaken", taken_money.ToString());
                cmd.Parameters.AddWithValue("@WT", DateTime.Now.ToString("hh:mm:ss"));
                cmd.Parameters.AddWithValue("@Day", DateTime.Now.DayOfWeek.ToString() + " (" + DateTime.Now.ToString("dd:MM:yyyy") + ")");
                cmd.Parameters.AddWithValue("@Balance", get_balance());
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cannot Set History due to: " + ex, "ERROR IN DATABASE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void update_balance_dept()
        {
            open_connection();
            double current_money = double.Parse(get_balance());
            double taken_money = double.Parse(Dept_Payed_Text_Box.Text);
            current_money -= taken_money;
            try
            {
                String sql = "UPDATE Users SET Balance = @B WHERE BIN =" + uBin;
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@B", current_money.ToString());
                cmd.ExecuteNonQuery();
                Balance_Text.Text = current_money.ToString();
                Take_money_txt.Text = "0";
                set_history_withdraw((-taken_money));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cannot Update balance due to: " + ex, "ERROR IN DATABASE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private String get_dept_amount()
        {
            String dept_money = "";
            open_connection();
            try
            {
                String sql = "Select Dept_Amount from Dept where ID=" + ID_Dept_Text_Box.Text;
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                SQLiteDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dept_money = dr["Dept_Amount"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not get dept_amount due to : " + ex, "SQL Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dept_money;
        }
        private void set_history_table()
        {
            open_connection();
            try
            {
                string sql = "SELECT *FROM History";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(dr);
                            History_Table.DataSource = dataTable;
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("ERROR in SQL History due to: " + ex.Message, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            set_history_table();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            EURO_Price.Text = "32.5680";
            KSA_Price.Text = "8.24";
            Dollars_Price.Text = "30.89";
        }
        private void Set_page()
        {
            open_connection();
            try
            {
                if(uPass.Length > 4)
                {
                    string sql = "SELECT * FROM Users WHERE Pass = @uPass";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@uPass", uPass);
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                ID_text_set.Text = dr["ID"].ToString();
                                Name_text_set.Text = dr["Name"].ToString();
                                Email_set.Text = dr["Email"].ToString();
                                Pass_set.Text = dr["Pass"].ToString();
                                Bin_set.Text = dr["BIN"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    string sql = "SELECT *FROM Users WHERE BIN = @uBin";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@uBin", uBin);
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                ID_text_set.Text = dr["ID"].ToString();
                                Name_text_set.Text = dr["Name"].ToString();
                                Email_set.Text = dr["Email"].ToString();
                                Pass_set.Text = dr["Pass"].ToString();
                                Bin_set.Text = dr["BIN"].ToString();
                                byte[] pic = (byte[])dr["Picture"];
                                if (pic != null)
                                {
                                    using (MemoryStream ms = new MemoryStream(pic))
                                    {
                                        pictureBox8.Image = Image.FromStream(ms);
                                    }
                                }
                                else
                                {
                                    pictureBox8.Image = null;
                                }
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error In Connection: " + ex, "ERROR SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
            Set_page();
        }
        private void button24_Click(object sender, EventArgs e)
        {
            int Start_Hours = int.Parse(StartH.Text);
            int Start_Min = int.Parse(StartMin.Text);
            int End_Hours = int.Parse(EndH.Text);
            int End_Min = int.Parse(EndMin.Text);
            int Total_Hours = Math.Abs(Start_Hours - End_Hours);
            int Total_min = 0;
            Total_min = Start_Min - End_Min;
            WorkedHours.Text = Total_Hours.ToString() + " Hours And " + Total_min.ToString() + " Mins";
            double money_Recived = Math.Abs((double.Parse(Money_Per_Hour.Text) * Total_Hours) + (double.Parse(Money_Per_Hour.Text) * (Total_min / 60)));
            Money_calc_txt.Text = money_Recived.ToString();
        }
        private void button25_Click(object sender, EventArgs e)
        {
            Money_t.Text = Money_type.SelectedItem.ToString();
            Money_to_convert.Text = Money_to_convert_box.SelectedItem.ToString();
            if (Money_to_convert.Text == "KSA" && Money_t.Text == "LE")
            {
                ChosenMoney.Text = Entered_Money.Text + " " + Money_t.Text;
                Chosen_Money_Conv.Text = Money_to_convert.Text;
                CalcMoney.Text = (double.Parse(EURO_Price.Text) / double.Parse(Entered_Money.Text)).ToString();
            }
            else if (Money_to_convert.Text == "$" && Money_t.Text == "LE")
            {
                ChosenMoney.Text = Money_t.Text;
                Chosen_Money_Conv.Text = Money_to_convert.Text;
                CalcMoney.Text = (double.Parse(Dollars_Price.Text) / double.Parse(Entered_Money.Text)).ToString();
            }
            else if (Money_to_convert.Text == "Euro" && Money_t.Text == "LE")
            {
                ChosenMoney.Text = Money_t.Text;
                Chosen_Money_Conv.Text = Money_to_convert.Text;
                CalcMoney.Text = (double.Parse(EURO_Price.Text) / double.Parse(Entered_Money.Text)).ToString();
            }
            else if (Money_to_convert.Text == "LE" && Money_t.Text == "KSA")
            {
                ChosenMoney.Text = Money_t.Text;
                Chosen_Money_Conv.Text = Money_to_convert.Text;
                CalcMoney.Text = (double.Parse(EURO_Price.Text) * double.Parse(Entered_Money.Text)).ToString();
            }
            else if (Money_to_convert.Text == "LE" && Money_t.Text == "$")
            {
                ChosenMoney.Text = Money_t.Text;
                Chosen_Money_Conv.Text = Money_to_convert.Text;
                CalcMoney.Text = (double.Parse(Dollars_Price.Text) * double.Parse(Entered_Money.Text)).ToString();
            }
            else if (Money_to_convert.Text == "LE" && Money_t.Text == "Euro")
            {
                ChosenMoney.Text = Money_t.Text;
                Chosen_Money_Conv.Text = Money_to_convert.Text;
                CalcMoney.Text = (double.Parse(EURO_Price.Text) * double.Parse(Entered_Money.Text)).ToString();
            }
            else
            {
                MessageBox.Show("This Is only For Egy Use Wait the New Update Soon !", "Update Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int id;
            // Open the SQLite database connection
            open_connection();
            do
            {
                id = rand.Next(1000000);
            } while (id != rand.Next(1000000));
            try
            {
                string sql = "INSERT INTO Dept (ID, Name, Dept_Amount, Time_Added, Amount_Paid, Time_Paid) VALUES (@ID, @Name, @DA, @TA, @AP, @TP)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Name", Add_Name.Text);
                    cmd.Parameters.AddWithValue("@DA", Add_amount.Text);
                    cmd.Parameters.AddWithValue("@TA", DateTime.Now.ToString("hh:mm:ss:yy:MM:dd"));
                    cmd.Parameters.AddWithValue("@AP", "00");
                    cmd.Parameters.AddWithValue("@TP", "00");
                    cmd.ExecuteNonQuery();
                    set_dept();
                    set_table_dept();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Inserting Data in the Table Due to: " + ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            open_connection();
            double current_money = double.Parse(get_balance());
            if (double.Parse(Dept_Payed_Text_Box.Text) > current_money)
            {
                MessageBox.Show("Your Money is less than the Entered Money as your current balance is: " + current_money, "Paying Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (double.Parse(Dept_Payed_Text_Box.Text) < 0)
            {
                MessageBox.Show("You cannot enter negative money try again with valid input", "Paying Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                double money_dept_got = double.Parse(get_dept_amount());
                double set_money_fordept = double.Parse(Dept_Payed_Text_Box.Text);
                if (set_money_fordept >= money_dept_got)
                {
                    try
                    {
                        string sql = "UPDATE Dept SET (Dept_Amount,Amount_Paid,Time_Paid) = (@DA,@AP,@TP)WHERE ID = @DeptID";
                        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                        cmd.Parameters.AddWithValue("@DA", "0");
                        cmd.Parameters.AddWithValue("@AP", set_money_fordept.ToString());
                        cmd.Parameters.AddWithValue("@TP", DateTime.Now.ToString("hh:mm:ss dd:MM:yyyy"));
                        cmd.Parameters.AddWithValue("@DeptID", Dept_Payed_Text_Box.Text);
                        cmd.ExecuteNonQuery();
                        update_balance_dept();
                        set_table_dept();
                        MessageBox.Show("All Dept Payed!", "Payment Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR in SQL_Dept_Data due to: " + ex.Message, "Payment Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (set_money_fordept < money_dept_got)
                {
                    try
                    {
                        string sql = "UPDATE Dept SET Dept_Amount = @DA, Amount_Paid = @AP, Time_Paid = @TP WHERE ID = @DeptID";
                        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                        cmd.Parameters.AddWithValue("@DA", (money_dept_got - set_money_fordept).ToString());
                        cmd.Parameters.AddWithValue("@AP", set_money_fordept.ToString());
                        cmd.Parameters.AddWithValue("@TP", DateTime.Now.ToString("hh:mm:ss dd:MM:yyyy"));
                        cmd.Parameters.AddWithValue("@DeptID", Dept_Payed_Text_Box.Text);
                        cmd.ExecuteNonQuery();
                        update_balance_dept();
                        set_table_dept();
                        MessageBox.Show("Dept Payed and the remaining amount is: " + (money_dept_got - set_money_fordept).ToString(), "Payment Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR in SQL_Dept_Data due to: " + ex.Message, "Payment Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            open_connection();
            double taken_money = double.Parse(Take_money_txt.Text);
            double current_money = double.Parse(get_balance());
            if (current_money >= taken_money)
            {
                current_money -= taken_money;
                try
                {
                    String sql = "UPDATE Users SET Balance = @B WHERE BIN =" + uBin;
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@B", current_money.ToString());
                    cmd.ExecuteNonQuery();
                    Balance_Text.Text = current_money.ToString();
                    Take_money_txt.Text = "0";
                    set_history_withdraw((-taken_money));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error cannot Update balance due to: " + ex, "ERROR IN DATABASE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Your Money is less than the taken money as your current balance is: " + current_money, "WithDraw Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            open_connection();
            String money = get_balance();
            double added_money = 0.0;
            added_money += double.Parse(Add_money_text.Text) + double.Parse(get_balance());
            try
            {
                String sql = "UPDATE Users SET Balance = @B WHERE BIN =" + uBin;
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@B", added_money.ToString());
                cmd.ExecuteNonQuery();
                Balance_Text.Text = added_money.ToString();
                set_history(double.Parse(Add_money_text.Text));
                Add_money_text.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cannot Update balance due to: " + ex, "ERROR IN DATABASE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ID_Dept_Text_Box_TextChanged(object sender, EventArgs e)
        {
            open_connection();
            get_dept_amount();
            try
            {
                String sql = "Select Name , Dept_Amount From Dept Where ID = @ID";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", ID_Dept_Text_Box.Text);
                SQLiteDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Name_Dept_text_Box.Text = dr["Name"].ToString();
                    Dept_Text_Box.Text = dr["Dept_Amount"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR in SQL due to: " + ex.Message, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Name_Dept_text_Box.Text = "User Not Found";
                Dept_Text_Box.Text = "00.00";
            }
        }

        private void Bin_Dept_text_TextChanged(object sender, EventArgs e)
        {
            open_connection();
            try
            {
                String sql = "Select Name , Balance From Users Where BIN = @Bin";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Bin", Bin_Dept_text.Text);
                SQLiteDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    User_Dept_Name_Box.Text = dr["Name"].ToString();
                    Balance_Dept_text_Box.Text = dr["Balance"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR in SQL due to: " + ex.Message, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Name_Dept_text_Box.Text = "User Not Found";
                Balance_Dept_text_Box.Text = "00.00";
            }
        }
    }
}
