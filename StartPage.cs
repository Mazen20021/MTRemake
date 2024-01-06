using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Threading;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.IO;
namespace MTRemake
{
    public partial class StartPage : Form
    {
        static private string database = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ATM.db");
        SQLiteConnection connection = new SQLiteConnection("Data Source=" + database);
        private String Bins = "";
        bool connected = false;
        bool found = false;
        Thread thread;
        public StartPage()
        {
            InitializeComponent();
            open_connection();
            saveversion();
            CheckForUpdates();
        }
        private void CheckForUpdates()
        {
            try
            {
                WebClient client = new WebClient();
                string url = "https://raw.githubusercontent.com/Mazen20021/Projects/master/Desktop/MTRemake/version.json";
                string content = client.DownloadString(url);
                dynamic versionInfo = JsonConvert.DeserializeObject(content);
                string latestVersion = versionInfo.version;
                string downloadUrl = versionInfo.downloadUrl;
                if (latestVersion != GetCurrentVersion())
                {
                    // Prompt the user to update the application
                    DialogResult result = MessageBox.Show("A new version is available. Do you want to download and install it?", "Update Available", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(downloadUrl);
                        saveversion();
                        Environment.Exit(0);
                    }
                }
                else
                {
                    saveversion();
                    Application.Run();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Cannot Check For Updates At the monent Try Again Later: " + ex, "Updating Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Run();
            }
        }
        private string GetCurrentVersion()
        {
            return getversion();
        }
        private String getversion()
        {
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            string versionFilePath = Path.Combine(startupPath, "version.txt");
            if (File.Exists(versionFilePath))
            {
                string version = File.ReadAllText(versionFilePath);
                Version_text.Text = "V" + version;
                return version;
            }
            return "1.0.0.0";
        }
        private void saveversion()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            string versionFilePath = Path.Combine(startupPath, "version.txt");
            File.WriteAllText(versionFilePath, version);
    }
        private void open_connection()
        {
            try
            {
                connection.Open();
                connected = true;
                Console.WriteLine("Connected");
            }
            catch (Exception ex)
            {
                connected = false;
                MessageBox.Show("Error Couldn't Connect to DataBase Dueto: " + ex, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void save()
        {
            Bins = BinText.Text;
            
        }
       
        private void Mainpage()
        {
            Mainpage m = new Mainpage(Bins);
            Application.Run(m);
        }
        private void openmainpage()
        {
            Close();
            thread = new Thread(Mainpage);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT BIN , Email, Pass, Name FROM Users WHERE Name = @Name OR Email = @Em AND Pass = @Ps OR BIN = @Bin";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Bin", BinText.Text);
                cmd.Parameters.AddWithValue("@Em", Emailtext.Text);
                cmd.Parameters.AddWithValue("@Ps", BinText.Text);
                cmd.Parameters.AddWithValue("@Name", Emailtext.Text);
                SQLiteDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["Email"].ToString().Equals(Emailtext.Text) && dr["Pass"].ToString().Equals(BinText.Text) || dr["Name"].ToString().Equals(Emailtext.Text) && dr["Pass"].ToString().Equals(BinText.Text) || dr["Email"].ToString().Equals(Emailtext.Text) && dr["BIN"].ToString().Equals(BinText.Text) || dr["Name"].ToString().Equals(Emailtext.Text) && dr["BIN"].ToString().Equals(BinText.Text))
                    {
                        found = true;
                        save();
                        openmainpage();
                    }
                }
                if (found)
                {
                    found = false;
                }
                else if (!found)
                {
                    MessageBox.Show("Login Failed", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Error Dueto: " + ex, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void signup_page()
        {
            SignUp sp = new SignUp();
            Application.Run(sp);
        }
        private void SignUp_Click(object sender, EventArgs e)
        {
            Close();
            thread = new Thread(signup_page);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartPage());
        }
    }
}
