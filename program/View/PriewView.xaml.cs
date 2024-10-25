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
using System.Windows.Threading;
using Npgsql;

namespace wpfTest.View
{
    /// <summary>
    /// PriewView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PriewView : Page
    {
        int temp1count;
        int temp2count;
        int temp3count;
        int ai1count;
        int ai2count;
        int ai3count;
        int ai4count;
        int ai5count;
        int ai6count;

        BitmapImage backgroundimg = new BitmapImage();
        DispatcherTimer timer = new DispatcherTimer();
        int count = 5;

        public PriewView()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                backgroundimg = new BitmapImage();
                backgroundimg.BeginInit();
                backgroundimg.UriSource = new Uri(MainWindow.uipath + @"\PriewView.png", UriKind.RelativeOrAbsolute);
                backgroundimg.CacheOption = BitmapCacheOption.OnLoad;
                backgroundimg.EndInit();
                BackImg.Source = backgroundimg;

                Timer.Text = count.ToString();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_Tik);
                timer.Start();
            }
            catch (Exception ex) 
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Timer_Tik(object sender, EventArgs e)
        {
            count--;
            Timer.Text = count.ToString();

            if (count == 0)
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                timer.Stop();
                dispose();

                updatedb();

                NavigationService.Navigate(new Uri("View/TakePic.xaml", UriKind.RelativeOrAbsolute));
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        private void dispose()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                backgroundimg = null;
                BackImg.Source = null;
                BackImg = null;
                timer.Tick -= new EventHandler(Timer_Tik);
                timer = null;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void updatedb()
        {
            try
            {
                string connectionString = "Host=175.125.92.65;Port=5432;Database=onecutdb;User Id=onecut;Password=one6677";
                string userid = MainWindow.inifoldername;
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT temp1, temp2, temp3 From tempselect_tb WHERE id = @userid";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userid", userid);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                temp1count = reader.GetInt32(0);
                                temp2count = reader.GetInt32(1);
                                temp3count = reader.GetInt32(2);
                            }
                        }
                    }

                    connection.Close();
                }

                using (NpgsqlConnection connection2 = new NpgsqlConnection(connectionString))
                {
                    connection2.Open();

                    string sqlQuery = "SELECT ai1, ai2, ai3, ai4 From tempselect_tb WHERE id = @userid";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection2))
                    {
                        command.Parameters.AddWithValue("@userid", userid);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ai1count = reader.GetInt32(0);
                                ai2count = reader.GetInt32(1);
                                ai3count = reader.GetInt32(2);
                                ai4count = reader.GetInt32(3);
                            }
                        }
                    }

                    connection2.Close();
                }

                switch (MainWindow.inifoldername)
                {
                    case "dearpic1":
                    case "dearpic4":
                    case "dearpic3":
                        switch (TempSelect.temp)
                        {
                            case 1:
                                temp1count++;

                                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                {
                                    connection.Open();

                                    string sql = "UPDATE tempselect_tb SET temp1 = @temp1count WHERE id = @userid";

                                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                    {
                                        // @temp1count 매개변수 추가
                                        command.Parameters.AddWithValue("@temp1count", temp1count); // temp1count는 실제 값으로 변경해야 합니다.

                                        // @userid 매개변수 추가
                                        command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                        command.ExecuteNonQuery();
                                    }

                                    connection.Close();
                                }
                                break;
                            case 2:
                                temp2count++;

                                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                {
                                    connection.Open();

                                    string sql = "UPDATE tempselect_tb SET temp2 = @temp2count WHERE id = @userid";

                                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                    {
                                        // @temp1count 매개변수 추가
                                        command.Parameters.AddWithValue("@temp2count", temp2count); // temp1count는 실제 값으로 변경해야 합니다.

                                        // @userid 매개변수 추가
                                        command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                        command.ExecuteNonQuery();
                                    }

                                    connection.Close();
                                }
                                break;
                            case 3:
                                temp3count++;

                                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                {
                                    connection.Open();

                                    string sql = "UPDATE tempselect_tb SET temp3 = @temp3count WHERE id = @userid";

                                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                    {
                                        // @temp1count 매개변수 추가
                                        command.Parameters.AddWithValue("@temp3count", temp3count); // temp1count는 실제 값으로 변경해야 합니다.

                                        // @userid 매개변수 추가
                                        command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                        command.ExecuteNonQuery();
                                    }

                                    connection.Close();
                                }
                                break;
                        }
                        break;
                    case "dearpic2":
                        switch (TempSelect.temp)
                        {
                            case 1:
                                switch (AISelect.aiselect)
                                {
                                    case 1:
                                        temp1count++;
                                        ai1count++;

                                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                        {
                                            connection.Open();

                                            string sql = "UPDATE tempselect_tb SET temp1 = @temp1count, ai1 = @ai1count WHERE id = @userid";

                                            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                            {
                                                // @temp3count 매개변수 추가
                                                command.Parameters.AddWithValue("@temp1count", temp1count); // temp3count는 실제 값으로 변경해야 합니다.

                                                // @ai1count 매개변수 추가
                                                command.Parameters.AddWithValue("@ai1count", ai1count); // ai1count는 실제 값으로 변경해야 합니다.

                                                // @userid 매개변수 추가
                                                command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                                command.ExecuteNonQuery();
                                            }

                                            connection.Close();
                                        }
                                        break;
                                    case 2:
                                        temp1count++;
                                        ai2count++;

                                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                        {
                                            connection.Open();

                                            string sql = "UPDATE tempselect_tb SET temp1 = @temp1count, ai2 = @ai2count WHERE id = @userid";

                                            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                            {
                                                // @temp3count 매개변수 추가
                                                command.Parameters.AddWithValue("@temp1count", temp1count); // temp3count는 실제 값으로 변경해야 합니다.

                                                // @ai1count 매개변수 추가
                                                command.Parameters.AddWithValue("@ai2count", ai2count); // ai1count는 실제 값으로 변경해야 합니다.

                                                // @userid 매개변수 추가
                                                command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                                command.ExecuteNonQuery();
                                            }

                                            connection.Close();
                                        }
                                        break;
                                    case 3:
                                        temp1count++;
                                        ai3count++;

                                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                        {
                                            connection.Open();

                                            string sql = "UPDATE tempselect_tb SET temp1 = @temp1count, ai3 = @ai3count WHERE id = @userid";

                                            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                            {
                                                // @temp3count 매개변수 추가
                                                command.Parameters.AddWithValue("@temp1count", temp1count); // temp3count는 실제 값으로 변경해야 합니다.

                                                // @ai1count 매개변수 추가
                                                command.Parameters.AddWithValue("@ai3count", ai3count); // ai1count는 실제 값으로 변경해야 합니다.

                                                // @userid 매개변수 추가
                                                command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                                command.ExecuteNonQuery();
                                            }

                                            connection.Close();
                                        }
                                        break;
                                    case 4:
                                        temp1count++;
                                        ai4count++;

                                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                        {
                                            connection.Open();

                                            string sql = "UPDATE tempselect_tb SET temp1 = @temp1count, ai4 = @ai4count WHERE id = @userid";

                                            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                            {
                                                // @temp3count 매개변수 추가
                                                command.Parameters.AddWithValue("@temp1count", temp1count); // temp3count는 실제 값으로 변경해야 합니다.

                                                // @ai1count 매개변수 추가
                                                command.Parameters.AddWithValue("@ai4count", ai4count); // ai1count는 실제 값으로 변경해야 합니다.

                                                // @userid 매개변수 추가
                                                command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                                command.ExecuteNonQuery();
                                            }

                                            connection.Close();
                                        }
                                        break;
                                }
                                break;
                            case 2:
                                switch (AISelect.aiselect)
                                {
                                    case 1:
                                        temp2count++;
                                        ai1count++;

                                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                        {
                                            connection.Open();

                                            string sql = "UPDATE tempselect_tb SET temp2 = @temp2count, ai1 = @ai1count WHERE id = @userid";

                                            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                            {
                                                // @temp3count 매개변수 추가
                                                command.Parameters.AddWithValue("@temp2count", temp2count); // temp3count는 실제 값으로 변경해야 합니다.

                                                // @ai1count 매개변수 추가
                                                command.Parameters.AddWithValue("@ai1count", ai1count); // ai1count는 실제 값으로 변경해야 합니다.

                                                // @userid 매개변수 추가
                                                command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                                command.ExecuteNonQuery();
                                            }

                                            connection.Close();
                                        }
                                        break;
                                    case 2:
                                        temp2count++;
                                        ai2count++;

                                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                        {
                                            connection.Open();

                                            string sql = "UPDATE tempselect_tb SET temp2 = @temp2count, ai2 = @ai2count WHERE id = @userid";

                                            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                            {
                                                // @temp3count 매개변수 추가
                                                command.Parameters.AddWithValue("@temp2count", temp2count); // temp3count는 실제 값으로 변경해야 합니다.

                                                // @ai1count 매개변수 추가
                                                command.Parameters.AddWithValue("@ai2count", ai2count); // ai1count는 실제 값으로 변경해야 합니다.

                                                // @userid 매개변수 추가
                                                command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                                command.ExecuteNonQuery();
                                            }

                                            connection.Close();
                                        }
                                        break;
                                    case 3:
                                        temp2count++;
                                        ai3count++;

                                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                        {
                                            connection.Open();

                                            string sql = "UPDATE tempselect_tb SET temp2 = @temp2count, ai3 = @ai3count WHERE id = @userid";

                                            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                            {
                                                // @temp3count 매개변수 추가
                                                command.Parameters.AddWithValue("@temp2count", temp2count); // temp3count는 실제 값으로 변경해야 합니다.

                                                // @ai1count 매개변수 추가
                                                command.Parameters.AddWithValue("@ai3count", ai3count); // ai1count는 실제 값으로 변경해야 합니다.

                                                // @userid 매개변수 추가
                                                command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                                command.ExecuteNonQuery();
                                            }

                                            connection.Close();
                                        }
                                        break;
                                    case 4:
                                        temp2count++;
                                        ai4count++;

                                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                                        {
                                            connection.Open();

                                            string sql = "UPDATE tempselect_tb SET temp2 = @temp2count, ai4 = @ai4count WHERE id = @userid";

                                            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                                            {
                                                // @temp3count 매개변수 추가
                                                command.Parameters.AddWithValue("@temp2count", temp2count); // temp3count는 실제 값으로 변경해야 합니다.

                                                // @ai1count 매개변수 추가
                                                command.Parameters.AddWithValue("@ai4count", ai4count); // ai1count는 실제 값으로 변경해야 합니다.

                                                // @userid 매개변수 추가
                                                command.Parameters.AddWithValue("@userid", userid); // userid는 실제 사용자 ID 값으로 변경해야 합니다.

                                                command.ExecuteNonQuery();
                                            }

                                            connection.Close();
                                        }
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
