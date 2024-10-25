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
using System.Windows.Shapes;

namespace wpfTest.View
{
    /// <summary>
    /// Password.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Password : Window
    {

        string[] pw = new string[4];

        public Password()
        {
            Source.Log.log.Info("PassWord 창 진입");
            InitializeComponent();
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            if (pw[0] == null)
            {
                pw[0] = "7";
            }
            else if (pw[0] != null && pw[1] == null)
            {
                pw[1] = "7";
            }
            else if (pw[1] != null && pw[2] == null)
            {
                pw[2] = "7";
            }
            else if (pw[2] != null && pw[3] == null)
            {
                pw[3] = "7";
            }

            showpw();
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            if (pw[0] == null)
            {
                pw[0] = "8";
            }
            else if (pw[0] != null && pw[1] == null)
            {
                pw[1] = "8";
            }
            else if (pw[1] != null && pw[2] == null)
            {
                pw[2] = "8";
            }
            else if (pw[2] != null && pw[3] == null)
            {
                pw[3] = "8";
            }

            showpw();
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            if (pw[0] == null)
            {
                pw[0] = "9";
            }
            else if (pw[0] != null && pw[1] == null)
            {
                pw[1] = "9";
            }
            else if (pw[1] != null && pw[2] == null)
            {
                pw[2] = "9";
            }
            else if (pw[2] != null && pw[3] == null)
            {
                pw[3] = "9";
            }

            showpw();
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (pw[0] == null)
            {
                pw[0] = "4";
            }
            else if (pw[0] != null && pw[1] == null)
            {
                pw[1] = "4";
            }
            else if (pw[1] != null && pw[2] == null)
            {
                pw[2] = "4";
            }
            else if (pw[2] != null && pw[3] == null)
            {
                pw[3] = "4";
            }

            showpw();
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            if (pw[0] == null)
            {
                pw[0] = "5";
            }
            else if (pw[0] != null && pw[1] == null)
            {
                pw[1] = "5";
            }
            else if (pw[1] != null && pw[2] == null)
            {
                pw[2] = "5";
            }
            else if (pw[2] != null && pw[3] == null)
            {
                pw[3] = "5";
            }

            showpw();
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            if (pw[0] == null)
            {
                pw[0] = "6";
            }
            else if (pw[0] != null && pw[1] == null)
            {
                pw[1] = "6";
            }
            else if (pw[1] != null && pw[2] == null)
            {
                pw[2] = "6";
            }
            else if (pw[2] != null && pw[3] == null)
            {
                pw[3] = "6";
            }

            showpw();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (pw[0] == null)
            {
                pw[0] = "1";
            }
            else if (pw[0] != null && pw[1] == null)
            {
                pw[1] = "1";
            }
            else if (pw[1] != null && pw[2] == null)
            {
                pw[2] = "1";
            }
            else if (pw[2] != null && pw[3] == null)
            {
                pw[3] = "1";
            }

            showpw();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (pw[0] == null)
            {
                pw[0] = "2";
            }
            else if (pw[0] != null && pw[1] == null)
            {
                pw[1] = "2";
            }
            else if (pw[1] != null && pw[2] == null)
            {
                pw[2] = "2";
            }
            else if (pw[2] != null && pw[3] == null)
            {
                pw[3] = "2";
            }

            showpw();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (pw[0] == null)
            {
                pw[0] = "3";
            }
            else if (pw[0] != null && pw[1] == null)
            {
                pw[1] = "3";
            }
            else if (pw[1] != null && pw[2] == null)
            {
                pw[2] = "3";
            }
            else if (pw[2] != null && pw[3] == null)
            {
                pw[3] = "3";
            }

            showpw();
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            if (pw[0] == null)
            {
                pw[0] = "0";
            }
            else if (pw[0] != null && pw[1] == null)
            {
                pw[1] = "0";
            }
            else if (pw[1] != null && pw[2] == null)
            {
                pw[2] = "0";
            }
            else if (pw[2] != null && pw[3] == null)
            {
                pw[3] = "0";
            }

            showpw();
        }

        private void btndel_Click(object sender, RoutedEventArgs e)
        {
            if (pw[3] != null)
            {
                pw[3] = null;
            }
            else if (pw[3] == null && pw[2] != null)
            {
                pw[2] = null;
            }
            else if (pw[2] == null && pw[1] != null)
            {
                pw[1] = null;
            }
            else if (pw[1] == null && pw[0] != null)
            {
                pw[0] = null;
            }

            showpw();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void showpw()
        {
            //passwordtb.Text = pw[0] + pw[1] + pw[3] + pw[4];

            if (pw[0] == null)
            {
                passwordtb.Password = null;
            }
            else if (pw[0] != null && pw[1] == null)
            {
                passwordtb.Password = pw[0];
            }
            else if (pw[1] != null && pw[2] == null)
            {
                passwordtb.Password = pw[0] + pw[1];
            }
            else if (pw[2] != null && pw[3] == null)
            {
                passwordtb.Password = pw[0] + pw[1] + pw[2];
            }
            else if (pw[2] != null && pw[3] != null)
            {
                passwordtb.Password = pw[0] + pw[1] + pw[2] + pw[3];
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (passwordtb.Password == "6677")
            {
                MainWindow.Adminlogin = 1;
                Window.GetWindow(this).Close();
                Source.Log.log.Info("관리자 로그인 페이지 닫음");
            }
            else
            {
                statlabel.Content = "비밀번호 오류.";
            }
        }
    }
}
