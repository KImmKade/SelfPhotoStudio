using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Npgsql;
using System.Diagnostics;

namespace wpfTest.View
{
    /// <summary>
    /// login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class login : Page
    {
        string armedpassword;

        string dbpassword;
        string dbversion;
        int dbimgchage;
        string dbmachinecode;
        ProcessStartInfo startinfo = new ProcessStartInfo();
        public login()
        {
            Source.Log.log.Info("로그인 페이지 진입");
            InitializeComponent();
        }

        #region///INI Import///

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                armedpassword = ChangePassWord(PasswordBox.Password);

                string userid = IDTextBox.Text;

                string connectionString = "Host=175.125.92.65;Port=5432;Database=onecutdb;User Id=onecut;Password=one6677";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT password, version, imgchange, machinecode From account_tb WHERE id = @userid";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userid", userid);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dbpassword = reader.GetString(0);
                                dbversion = reader.GetString(1);
                                dbimgchage = reader.GetInt32(2);
                                dbmachinecode = reader.GetString(3);
                            }
                        }
                    }

                    connection.Close();
                }

                if (armedpassword != dbpassword)
                {
                    MessageBox.Show("아이디 혹은 비밀번호가 다릅니다.");
                }
                else
                {
                    Source.Log.log.Info("로그인 성공!, 로그인 한 아이디 : " + IDTextBox.Text);

                    MainWindow.inifoldername = IDTextBox.Text;
                    MainWindow.foldername.Clear();
                    MainWindow.foldername.Append(IDTextBox.Text);
                    WritePrivateProfileString("ApiTest", "foldername", IDTextBox.Text, MainWindow.iniPath);

                    MainWindow.inimachinecode = dbmachinecode;
                    MainWindow.ID.Clear();
                    MainWindow.ID.Append(dbmachinecode);
                    WritePrivateProfileString("ApiTest", "MachineID", dbmachinecode, MainWindow.iniPath);

                    if (autologinbtn.IsChecked == true)
                    {
                        if (MainWindow.iniautologin.ToString() == "0")
                        {
                            MainWindow.iniautologin.Clear();
                            MainWindow.iniautologin.Append("1");
                            WritePrivateProfileString("Setting", "autologin", "1", MainWindow.iniPath);

                            MainWindow.iniuserid.Clear();
                            MainWindow.iniuserid.Append(IDTextBox.Text);
                            WritePrivateProfileString("Login", "ID", IDTextBox.Text, MainWindow.iniPath);

                            MainWindow.iniuserpassword.Clear();
                            MainWindow.iniuserpassword.Append(armedpassword);
                            WritePrivateProfileString("Login", "PassWord", armedpassword, MainWindow.iniPath);
                        }
                    }

                    if (MainWindow.Version != dbversion || dbimgchage == 1)
                    {
                        Source.AutoClosingMessageBox.Show("프로그램의 버전이 다릅니다. 3초뒤 자동업데이트가 살행됩니다.", "알림", 3000);

                        startinfo.FileName = MainWindow.Updateprogrampath;
                        startinfo.Verb = "runas";

                        Process.Start(startinfo);

                        Environment.Exit(0);
                    }
                    else
                    {
                        NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                    }
                }
            }
            catch (MySqlException sqlex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + sqlex);
                MessageBox.Show("DB와의 연결이 불안정 하거나 인터넷 연결을 확인해주세요.");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private static string ChangePassWord(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashedBytes = sha256.ComputeHash(inputBytes);

                StringBuilder hexstring = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    hexstring.Append(b.ToString("x2"));
                }

                return hexstring.ToString();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.iniautologin.ToString() == "1")
                {
                    autologinbtn.IsChecked = true;
                    string userid = MainWindow.iniuserid.ToString();
                    IDTextBox.Text = MainWindow.iniuserid.ToString();

                    string connectionString = "Host=49.247.172.106;Port=5432;Database=onecutweb;User Id=postgres;Password=one1004";

                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        string sqlQuery = "SELECT password, version, imgchange, machineCode From account_tb WHERE id = @userid";

                        using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                        {
                            command.Parameters.AddWithValue("@userid", userid);

                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dbpassword = reader.GetString(0);
                                    dbversion = reader.GetString(1);
                                    dbimgchage = reader.GetInt32(2);
                                    dbmachinecode = reader.GetString(3);
                                }
                            }
                        }

                        connection.Close();
                    }

                    if (MainWindow.iniuserpassword.ToString() != dbpassword)
                    {
                        MessageBox.Show("아이디 혹은 비밀번호가 다릅니다.");
                    }
                    else
                    {
                        Source.Log.log.Info("로그인 성공");
                        Source.Log.log.Info("로그인 성공 아이디 : " + MainWindow.iniuserid.ToString());

                        MainWindow.inifoldername = IDTextBox.Text;
                        MainWindow.foldername.Clear();
                        MainWindow.foldername.Append(IDTextBox.Text);
                        WritePrivateProfileString("ApiTest", "foldername", IDTextBox.Text, MainWindow.iniPath);

                        MainWindow.inimachinecode = dbmachinecode;
                        MainWindow.ID.Clear();
                        MainWindow.ID.Append(dbmachinecode);
                        WritePrivateProfileString("ApiTest", "MachineID", dbmachinecode, MainWindow.iniPath);

                        if (MainWindow.Version != dbversion || dbimgchage == 1)
                        {
                            Source.AutoClosingMessageBox.Show("프로그램의 버전이 다릅니다. 3초뒤 자동업데이트가 살행됩니다.", "알림", 3000);

                            startinfo.FileName = MainWindow.Updateprogrampath;
                            startinfo.Verb = "runas";

                            Process.Start(startinfo);

                            Environment.Exit(0);
                        }
                        else
                        {
                            NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                }
                else
                {
                    autologinbtn.IsChecked = false;
                }

            }
            catch (MySqlException sqlex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + sqlex);
                MessageBox.Show("DB와의 연결이 불안정 하거나 인터넷 연결을 확인해주세요.");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void exitbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MessageBox.Show("종료하시겠습니까?", "경고", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Source.Log.log.Info("Yes 버튼 클릭");
                    MainWindow.ProgramClose();
                }
                else
                {
                    Source.Log.log.Info("No 버튼 클릭");
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void IDTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (IDTextBox.Text == "아이디")
                {
                    IDTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (PasswordBox.Password == "word")
                {
                    PasswordBox.Password = "";
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void IDTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (IDTextBox.Text == "")
                {
                    IDTextBox.Text = "아이디";
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (PasswordBox.Password == "")
                {
                    PasswordBox.Password = "word";
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void IDTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (e.Key == Key.Enter)
                {
                    armedpassword = ChangePassWord(PasswordBox.Password);

                    string userid = IDTextBox.Text;

                    string connectionString = "Host=175.125.92.65;Port=5432;Database=onecutdb;User Id=onecut;Password=one6677";

                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        string sqlQuery = "SELECT password, version, imgchange, machineCode From account_tb WHERE id = @userid";

                        using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                        {
                            command.Parameters.AddWithValue("@userid", userid);

                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dbpassword = reader.GetString(0);
                                    dbversion = reader.GetString(1);
                                    dbimgchage = reader.GetInt32(2);
                                    dbmachinecode = reader.GetString(3);
                                }
                            }
                        }

                        connection.Close();
                    }

                    if (armedpassword != dbpassword)
                    {
                        MessageBox.Show("아이디 혹은 비밀번호가 다릅니다.");
                    }
                    else
                    {
                        Source.Log.log.Info("로그인 성공!, 로그인 한 아이디 : " + IDTextBox.Text);

                        MainWindow.inifoldername = IDTextBox.Text;
                        MainWindow.foldername.Clear();
                        MainWindow.foldername.Append(IDTextBox.Text);
                        WritePrivateProfileString("ApiTest", "foldername", IDTextBox.Text, MainWindow.iniPath);

                        MainWindow.inimachinecode = dbmachinecode;
                        MainWindow.ID.Clear();
                        MainWindow.ID.Append(dbmachinecode);
                        WritePrivateProfileString("ApiTest", "MachineID", dbmachinecode, MainWindow.iniPath);

                        if (autologinbtn.IsChecked == true)
                        {
                            if (MainWindow.iniautologin.ToString() == "0")
                            {
                                MainWindow.iniautologin.Clear();
                                MainWindow.iniautologin.Append("1");
                                WritePrivateProfileString("Setting", "autologin", "1", MainWindow.iniPath);

                                MainWindow.iniuserid.Clear();
                                MainWindow.iniuserid.Append(IDTextBox.Text);
                                WritePrivateProfileString("Login", "ID", IDTextBox.Text, MainWindow.iniPath);

                                MainWindow.iniuserpassword.Clear();
                                MainWindow.iniuserpassword.Append(armedpassword);
                                WritePrivateProfileString("Login", "PassWord", armedpassword, MainWindow.iniPath);
                            }
                        }

                        if (MainWindow.Version != dbversion || dbimgchage == 1)
                        {
                            Source.AutoClosingMessageBox.Show("프로그램의 버전이 다릅니다. 3초뒤 자동업데이트가 살행됩니다.", "알림", 3000);

                            startinfo.FileName = MainWindow.Updateprogrampath;
                            startinfo.Verb = "runas";

                            Process.Start(startinfo);

                            Environment.Exit(0);
                        }
                        else
                        {
                            NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                }
                else if (e.Key == Key.Tab)
                {
                    PasswordBox.Focus();
                }
            }
            catch (MySqlException sqlex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + sqlex);
                MessageBox.Show("DB와의 연결이 불안정 하거나 인터넷 연결을 확인해주세요.");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (e.Key == Key.Enter)
                {
                    armedpassword = ChangePassWord(PasswordBox.Password);

                    string userid = IDTextBox.Text;

                    string connectionString = "Host=175.125.92.65;Port=5432;Database=onecutdb;User Id=onecut;Password=one6677";

                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        string sqlQuery = "SELECT password, version, imgchange, machineCode From account_tb WHERE id = @userid";

                        using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                        {
                            command.Parameters.AddWithValue("@userid", userid);

                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dbpassword = reader.GetString(0);
                                    dbversion = reader.GetString(1);
                                    dbimgchage = reader.GetInt32(2);
                                    dbmachinecode = reader.GetString(3);
                                }
                            }
                        }

                        connection.Close();
                    }

                    if (armedpassword != dbpassword)
                    {
                        MessageBox.Show("아이디 혹은 비밀번호가 다릅니다.");
                    }
                    else
                    {
                        Source.Log.log.Info("로그인 성공!, 로그인 한 아이디 : " + IDTextBox.Text);

                        MainWindow.inifoldername = IDTextBox.Text;
                        MainWindow.foldername.Clear();
                        MainWindow.foldername.Append(IDTextBox.Text);
                        WritePrivateProfileString("ApiTest", "foldername", IDTextBox.Text, MainWindow.iniPath);

                        MainWindow.inimachinecode = dbmachinecode;
                        MainWindow.ID.Clear();
                        MainWindow.ID.Append(dbmachinecode);
                        WritePrivateProfileString("ApiTest", "MachineID", dbmachinecode, MainWindow.iniPath);

                        if (autologinbtn.IsChecked == true)
                        {
                            if (MainWindow.iniautologin.ToString() == "0")
                            {
                                MainWindow.iniautologin.Clear();
                                MainWindow.iniautologin.Append("1");
                                WritePrivateProfileString("Setting", "autologin", "1", MainWindow.iniPath);

                                MainWindow.iniuserid.Clear();
                                MainWindow.iniuserid.Append(IDTextBox.Text);
                                WritePrivateProfileString("Login", "ID", IDTextBox.Text, MainWindow.iniPath);

                                MainWindow.iniuserpassword.Clear();
                                MainWindow.iniuserpassword.Append(armedpassword);
                                WritePrivateProfileString("Login", "PassWord", armedpassword, MainWindow.iniPath);
                            }
                        }

                        if (MainWindow.Version != dbversion || dbimgchage == 1)
                        {
                            Source.AutoClosingMessageBox.Show("프로그램의 버전이 다릅니다. 3초뒤 자동업데이트가 살행됩니다.", "알림", 3000);

                            startinfo.FileName = MainWindow.Updateprogrampath;
                            startinfo.Verb = "runas";

                            Process.Start(startinfo);

                            Environment.Exit(0);
                        }
                        else
                        {
                            NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                }
            }
            catch (MySqlException sqlex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + sqlex);
                MessageBox.Show("DB와의 연결이 불안정 하거나 인터넷 연결을 확인해주세요.");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Button_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                PasswordBox.Focus();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void autologinbtn_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                LoginBtn.Focus();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
