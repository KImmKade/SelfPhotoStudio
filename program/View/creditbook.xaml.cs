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
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Data;
using System.Net;

namespace wpfTest.View
{
    /// <summary>
    /// creditbook.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class creditbook : Window
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        DateTime nowdate = DateTime.Today;
        int nowyear;
        int nowmonth;
        string day;


        public creditbook()
        {
            InitializeComponent();
        }


        class CreditData
        {
            public string Data_CardNum { get; set; }
            public string Data_Date { get; set; }
            public string Data_Authorize { get; set; }
            public string Data_Vantr { get; set; }
            public string Data_Price { get; set; }
            public string Data_Day { get; set; }
        }

        private void DataUpload()
        {
            try
            {
                int totalmoney = 0;

                List<CreditData> crditlist = new List<CreditData>();
                StreamReader rd = new StreamReader(MainWindow.savePath + @"Data\BankBookData\cardrecord" + day + ".txt");

                DateTimeTB.Text = day;
                while (!rd.EndOfStream)
                {
                    string line = rd.ReadLine();
                    string[] cols = line.Split(',');

                    CreditData credit = new CreditData();

                    credit.Data_Day = cols[0];
                    credit.Data_CardNum = cols[1];
                    credit.Data_Date = cols[2];
                    credit.Data_Price = cols[3];
                    credit.Data_Authorize = cols[4];
                    credit.Data_Vantr = cols[5];

                    crditlist.Add(credit);
                    totalmoney += Convert.ToInt32(cols[3]);
                }

                rd.Close();

                TotalMoney.Content = "\\" + totalmoney.ToString("#,##0");
                BankLV.ItemsSource = crditlist;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
                if (ex.Message.Contains("파일을 찾을 수 없습니다."))
                {
                    MessageBox.Show("기록된 매출이 없습니다.");
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            nowyear = nowdate.Year;
            nowmonth = nowdate.Month;
            day = nowyear.ToString() + "." + nowmonth.ToString();
            DataUpload();
        }

        private void BankLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (BankLV.SelectedItems != null && BankLV.SelectedItems.Count > 0)
                {
                    Cardnum.Text = (BankLV.SelectedItems[0] as CreditData).Data_CardNum.ToString();
                    Date.Text = (BankLV.SelectedItems[0] as CreditData).Data_Date.ToString();
                    Authorize.Text = (BankLV.SelectedItems[0] as CreditData).Data_Authorize.ToString();
                    Vantr.Text = (BankLV.SelectedItems[0] as CreditData).Data_Vantr.ToString();
                    Price.Text = (BankLV.SelectedItems[0] as CreditData).Data_Price.ToString();
                }
                else
                {
                    return;
                }

                if (Cardnum.Text != null && Date.Text != null && Authorize.Text != null && Vantr.Text != null && Price.Text != null)
                {
                    requestcancle.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void requestcancle_Click(object sender, RoutedEventArgs e)
        {
            requestcancle.IsEnabled = false;
            IntPtr Handle;
            Handle = FindWindow(null, "MainWindow");
            Source.CreditCard credit = new Source.CreditCard();
            string recieve = "";
            if (MainWindow.cardnum == "기본값")
            {
                recieve = credit.Cancel(Convert.ToInt32(Price.Text), Handle, Authorize.Text.Trim(), Date.Text.Trim(), Vantr.Text, "8C89990196");
            }
            else
            {
                recieve = credit.Cancel(Convert.ToInt32(Price.Text), Handle, Authorize.Text.Trim(), Date.Text.Trim(), Vantr.Text, MainWindow.cardnum.ToString());
            }

            if (recieve.Contains("OK!"))
            {
                MessageBox.Show("카드취소처리 완료");
                deleterecord();
                if (MainWindow.CheckInternetConnection())
                {
                    removemoney();
                    SendTotalData();
                }
                BankLV.SelectAll();
                BankLV.Items.Refresh();
                Cardnum.Text = null;
                Date.Text = null;
                Price.Text = null;
                Authorize.Text = null;
                Vantr.Text = null;
                DataUpload();
            }
            else if (recieve.Contains("오류메세지 : "))
            {
                MessageBox.Show(recieve);
            }
            else if (recieve.Contains("Cancelled"))
            {
                MessageBox.Show("거래 취소됨");
            }
            else if (recieve.Contains("TimeOut"))
            {
                MessageBox.Show("시간 초과");
            }
            else if (recieve.Contains("Cancled"))
            {
                MessageBox.Show("이미 취소가 된 내역입니다.");
            }
            else if (recieve.Contains(""))
            {
                MessageBox.Show(recieve);
            }
        }

        private void deleterecord()
        {
            try
            {
                DateTime nowdate = DateTime.Now;
                int nowyear = nowdate.Year;
                int nowmonth = nowdate.Month;
                string now = DateTime.Now.ToString("yyyy.MM.dd.HH:mm");
                string filePath = MainWindow.savePath + @"Data\BankBookData\cardrecord" + nowyear + "." + nowmonth + ".txt";
                string existingContent = File.ReadAllText(filePath);
                string newContent = now + "," + Cardnum.Text + "," + Date.Text + "," + (-Convert.ToInt32(Price.Text)) + "," + Authorize.Text + "," + Vantr.Text + "\n" + existingContent;
                File.WriteAllText(filePath, newContent);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void PreViewMonth_Click(object sender, RoutedEventArgs e)
        {
            DateTime result;
            result = nowdate.AddMonths(-1);
            nowdate = result;
            nowyear = result.Year;
            nowmonth = result.Month;
            day = nowyear.ToString() + nowmonth.ToString();
            BankLV.SelectAll();
            BankLV.Items.Refresh();
            DataUpload();
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            DateTime result;
            result = nowdate.AddMonths(+1);
            nowdate = result;
            nowyear = result.Year;
            nowmonth = result.Month;
            day = nowyear.ToString() + nowmonth.ToString();
            BankLV.SelectAll();
            BankLV.Items.Refresh();
            DataUpload();
        }

        private void SendTotalData()
        {
            try
            {
                string url = "";

                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                string day = DateTime.Now.ToString("yyyy-MM-dd");

                string sendData = null;

                switch (MainWindow.Way.ToString())
                {
                    case "1":
                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                        break;
                    case "2":
                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + -Convert.ToInt32(Price.Text);
                        break;
                    case "3":
                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                        break;
                    case "4":
                        sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 0 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + -Convert.ToInt32(Price.Text);
                        break;
                }

                Source.Log.log.Info("매출 데이터 값 : " + sendData);

                byte[] buffer;
                buffer = Encoding.Default.GetBytes(sendData);
                request.ContentLength = buffer.Length;

                Stream sendStream = request.GetRequestStream();
                sendStream.Write(buffer, 0, buffer.Length);
                sendStream.Close();

                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                string responseMsg = reader.ReadToEnd();

                Console.WriteLine(responseMsg);

                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void removemoney()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                string day = DateTime.Today.ToString("dd");
                int allintmoney = 0;
                Delay(200);

                StringBuilder couponshot = new StringBuilder();

                GetPrivateProfileString("CouponShot", day, "", couponshot, couponshot.Capacity, MainWindow.bankbookinipath);

                WritePrivateProfileString("CouponShot", day, (Convert.ToInt32(couponshot.ToString()) - Convert.ToInt32(Price.Text.ToString())).ToString(), MainWindow.bankbookinipath);

            }
            catch (Exception ex)
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private static DateTime Delay(int MS)
        {
            DateTime thisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime afterMoment = thisMoment.Add(duration);

            while (afterMoment >= thisMoment)
            {
                if (System.Windows.Application.Current != null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate { }));
                }
                thisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                Window.GetWindow(this).Close();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
