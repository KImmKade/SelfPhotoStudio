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
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace wpfTest.View
{
    /// <summary>
    /// bankbook.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class bankbook : Window
    {
        StringBuilder checkyear = new StringBuilder();
        StringBuilder checkmonth = new StringBuilder();

        string inipath;
        #region //ini값 불러오기

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        private static List<BankData> instance;

        string[] date = new string[]
        { "01일", "02일", "03일", "04일", "05일", "06일", "07일", "08일", "09일", "10일",
            "11일", "12일", "13일", "14일", "15일", "16일", "17일", "18일", "19일", "20일",
            "21일", "22일", "23일", "24일", "25일", "26일", "27일", "28일", "29일", "30일", "31일"
        };

        StringBuilder[] stringbuilder_money = new StringBuilder[31];
        StringBuilder[] stringbuilder_printedpage = new StringBuilder[31];
        StringBuilder[] stringbuilder_takenshot = new StringBuilder[31];
        StringBuilder[] stringbuilder_remainpaper = new StringBuilder[31];
        StringBuilder[] stringbuilder_chargedshot = new StringBuilder[31];
        StringBuilder[] stringbuilder_freeshot = new StringBuilder[31];
        StringBuilder[] stringbuilder_couponshot = new StringBuilder[31];

        public bankbook()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 그리드 로드 후 리스트뷰 데이터 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "()");
                checkmonth.Clear();
                checkyear.Clear();
                checkmonth.Append(DateTime.Today.ToString("MM"));
                checkyear.Append(DateTime.Today.ToString("yyyy"));
                DataUpload();
            }
            catch(Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        class BankData
        {
            public string Data_Date { get; set; }
            public string Data_Money { get; set; }
            public string Data_PrintedPage { get; set; }
            public string Data_TakenShot { get; set; }
            public string Data_Remain { get; set; }
            public string Data_ChargedShot { get; set; }
            public string Data_FreeShot { get; set; }
            public string Data_CouponShot { get; set; }

            public static List<BankData> Getinstance()
            {
                if(instance == null)
                {
                    instance = new List<BankData>();
                }
                return instance;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            BankLV.SelectAll();
            foreach (BankData item in BankLV.SelectedItems)
            {
                instance.Remove(item);
            }
            BankLV.Items.Refresh();
        }

        private void PreViewMonth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 이전달 버튼 클릭");
                int checkprevmonth = Convert.ToInt32(checkmonth.ToString());
                int checkprevyear = Convert.ToInt32(checkyear.ToString());
                checkprevmonth--;
                if (checkprevmonth == 0)
                {
                    checkprevyear--;
                    checkprevmonth = 12;
                    if (checkprevmonth < 10)
                    {
                        checkyear.Clear();
                        checkyear.Append(checkprevyear);
                        checkmonth.Clear();
                        checkmonth.Append("0" + checkprevmonth);
                    }
                    else
                    {
                        checkyear.Clear();
                        checkyear.Append(checkprevyear);
                        checkmonth.Clear();
                        checkmonth.Append(checkprevmonth);
                    }
                }
                else
                {
                    if (checkprevmonth < 10)
                    {
                        checkyear.Clear();
                        checkyear.Append(checkprevyear);
                        checkmonth.Clear();
                        checkmonth.Append("0" + checkprevmonth);
                    }
                    else
                    {
                        checkyear.Clear();
                        checkyear.Append(checkprevyear);
                        checkmonth.Clear();
                        checkmonth.Append(checkprevmonth);
                    }
                }
                string filepath = MainWindow.savePath + @"Data\BankBookData\" + checkyear + "-" + checkmonth + ".ini";
                FileInfo fileinfo = new FileInfo(filepath);
                if (!fileinfo.Exists)
                {
                    MessageBox.Show("이전 달 데이터가 없습니다.");
                    checkprevmonth++;
                    if (checkprevmonth == 13)
                    {
                        checkprevyear++;
                        checkprevmonth = 1;
                        if (checkprevmonth < 10)
                        {
                            checkyear.Clear();
                            checkyear.Append(checkprevyear);
                            checkmonth.Clear();
                            checkmonth.Append("0" + checkprevmonth);
                        }
                        else
                        {
                            checkyear.Clear();
                            checkyear.Append(checkprevyear);
                            checkmonth.Clear();
                            checkmonth.Append(checkprevmonth);
                        }
                    }
                    else
                    {
                        if (checkprevmonth < 10)
                        {
                            checkyear.Clear();
                            checkyear.Append(checkprevyear);
                            checkmonth.Clear();
                            checkmonth.Append("0" + checkprevmonth);
                        }
                        else
                        {
                            checkyear.Clear();
                            checkyear.Append(checkprevyear);
                            checkmonth.Clear();
                            checkmonth.Append(checkprevmonth);
                        }
                    }
                    return;
                }
                DataUpload();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 다음달 버튼 클릭");
                int checkprevmonth = Convert.ToInt32(checkmonth.ToString());
                int checkprevyear = Convert.ToInt32(checkyear.ToString());
                checkprevmonth++;
                if (checkprevmonth == 13)
                {
                    checkprevyear++;
                    checkprevmonth = 1;
                    if(checkprevmonth < 10)
                    {
                        checkyear.Clear();
                        checkyear.Append(checkprevyear);
                        checkmonth.Clear();
                        checkmonth.Append("0" + checkprevmonth);
                    }
                    else
                    {
                        checkyear.Clear();
                        checkyear.Append(checkprevyear);
                        checkmonth.Clear();
                        checkmonth.Append(checkprevmonth);
                    }
                }
                else
                {
                    if (checkprevmonth < 10)
                    {
                        checkyear.Clear();
                        checkyear.Append(checkprevyear);
                        checkmonth.Clear();
                        checkmonth.Append("0" + checkprevmonth);
                    }
                    else
                    {
                        checkyear.Clear();
                        checkyear.Append(checkprevyear);
                        checkmonth.Clear();
                        checkmonth.Append(checkprevmonth);
                    }
                }
                string filepath = MainWindow.savePath + @"Data\BankBookData\" + checkyear + "-" + checkmonth + ".ini";
                FileInfo fileinfo = new FileInfo(filepath);
                if (!fileinfo.Exists)
                {
                    MessageBox.Show("다음 달 데이터가 없습니다.");
                    checkprevmonth--;
                    if (checkprevmonth == 0)
                    {
                        checkprevyear--;
                        checkprevmonth = 12;
                        if (checkprevmonth < 10)
                        {
                            checkyear.Clear();
                            checkyear.Append(checkprevyear);
                            checkmonth.Clear();
                            checkmonth.Append("0" + checkprevmonth);
                        }
                        else
                        {
                            checkyear.Clear();
                            checkyear.Append(checkprevyear);
                            checkmonth.Clear();
                            checkmonth.Append(checkprevmonth);
                        }
                    }
                    else
                    {
                        if (checkprevmonth < 10)
                        {
                            checkyear.Clear();
                            checkyear.Append(checkprevyear);
                            checkmonth.Clear();
                            checkmonth.Append("0" + checkprevmonth);
                        }
                        else
                        {
                            checkyear.Clear();
                            checkyear.Append(checkprevyear);
                            checkmonth.Clear();
                            checkmonth.Append(checkprevmonth);
                        }
                    }
                    return;
                }
                DataUpload();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void DataUpload()
        {
            try
            {
                BankLV.SelectAll();
                foreach (BankData item in BankLV.SelectedItems)
                {
                    instance.Remove(item);
                }
                BankLV.Items.Refresh();
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() - 데이터 업로드 시작");
                string month = DateTime.Today.ToString("yyyy-MM");
                if (month == checkyear.ToString() + "-" + checkmonth.ToString())
                {
                    DateTimeTB.Text = month;
                }
                else
                {
                    DateTimeTB.Text = checkyear.ToString() + "-" + checkmonth.ToString();
                }
                int totalcashmoney = 0;
                int totalcardmoney = 0;
                int[] cardsum = new int[31];
                int[] sum = new int[31];
                if(month == checkyear.ToString() + "-" + checkmonth.ToString())
                {
                    inipath = MainWindow.savePath + @"Data\BankBookData\" + month + ".ini";
                }
                else
                {
                    inipath = MainWindow.savePath + @"Data\BankBookData\" + checkyear.ToString() + "-" + checkmonth.ToString() + ".ini";
                }
                for (int i = 0; i < 31; i++)
                {
                    if (i < 9)
                    {
                        stringbuilder_money[i] = new StringBuilder();
                        GetPrivateProfileString("Money", "0" + (i + 1).ToString(), "", stringbuilder_money[i], stringbuilder_money[i].Capacity, inipath);
                        stringbuilder_printedpage[i] = new StringBuilder();
                        sum[i] = Convert.ToInt32(stringbuilder_money[i].ToString());
                        totalcashmoney = sum.Sum();
                        GetPrivateProfileString("PrintedPage", "0" + (i + 1).ToString(), "", stringbuilder_printedpage[i], stringbuilder_printedpage[i].Capacity, inipath);
                        stringbuilder_remainpaper[i] = new StringBuilder();
                        GetPrivateProfileString("RemainPaper", "0" + (i + 1).ToString(), "", stringbuilder_remainpaper[i], stringbuilder_remainpaper[i].Capacity, inipath);
                        stringbuilder_takenshot[i] = new StringBuilder();
                        GetPrivateProfileString("TakenShot", "0" + (i + 1).ToString(), "", stringbuilder_takenshot[i], stringbuilder_takenshot[i].Capacity, inipath);
                        stringbuilder_chargedshot[i] = new StringBuilder();
                        GetPrivateProfileString("ChargedShot", "0" + (i + 1).ToString(), "", stringbuilder_chargedshot[i], stringbuilder_chargedshot[i].Capacity, inipath);
                        stringbuilder_freeshot[i] = new StringBuilder();
                        GetPrivateProfileString("FreeShot", "0" + (i + 1).ToString(), "", stringbuilder_freeshot[i], stringbuilder_freeshot[i].Capacity, inipath);
                        stringbuilder_couponshot[i] = new StringBuilder();
                        GetPrivateProfileString("CouponShot", "0" + (i + 1).ToString(), "", stringbuilder_couponshot[i], stringbuilder_couponshot[i].Capacity, inipath);
                        cardsum[i] = Convert.ToInt32(stringbuilder_couponshot[i].ToString());
                        totalcardmoney = cardsum.Sum();
                    }
                    else
                    {
                        stringbuilder_money[i] = new StringBuilder();
                        GetPrivateProfileString("Money", (i + 1).ToString(), "", stringbuilder_money[i], stringbuilder_money[i].Capacity, inipath);
                        stringbuilder_printedpage[i] = new StringBuilder();
                        sum[i] = Convert.ToInt32(stringbuilder_money[i].ToString());
                        totalcashmoney = sum.Sum();
                        GetPrivateProfileString("PrintedPage", (i + 1).ToString(), "", stringbuilder_printedpage[i], stringbuilder_printedpage[i].Capacity, inipath);
                        stringbuilder_remainpaper[i] = new StringBuilder();
                        GetPrivateProfileString("RemainPaper", (i + 1).ToString(), "", stringbuilder_remainpaper[i], stringbuilder_remainpaper[i].Capacity, inipath);
                        stringbuilder_takenshot[i] = new StringBuilder();
                        GetPrivateProfileString("TakenShot", (i + 1).ToString(), "", stringbuilder_takenshot[i], stringbuilder_takenshot[i].Capacity, inipath);
                        stringbuilder_chargedshot[i] = new StringBuilder();
                        GetPrivateProfileString("ChargedShot", (i + 1).ToString(), "", stringbuilder_chargedshot[i], stringbuilder_chargedshot[i].Capacity, inipath);
                        stringbuilder_freeshot[i] = new StringBuilder();
                        GetPrivateProfileString("FreeShot", (i + 1).ToString(), "", stringbuilder_freeshot[i], stringbuilder_freeshot[i].Capacity, inipath);
                        stringbuilder_couponshot[i] = new StringBuilder();
                        GetPrivateProfileString("CouponShot", (i + 1).ToString(), "", stringbuilder_couponshot[i], stringbuilder_couponshot[i].Capacity, inipath);
                        cardsum[i] = Convert.ToInt32(stringbuilder_couponshot[i].ToString());
                        totalcardmoney = cardsum.Sum();
                    }
                    
                    BankData.Getinstance().Add(new BankData()
                    {
                        Data_Date = date[i],
                        Data_Money = stringbuilder_money[i].ToString(),
                        Data_PrintedPage = stringbuilder_printedpage[i].ToString(),
                        Data_Remain = stringbuilder_remainpaper[i].ToString(),
                        Data_TakenShot = stringbuilder_takenshot[i].ToString(),
                        Data_ChargedShot = stringbuilder_chargedshot[i].ToString(),
                        Data_FreeShot = stringbuilder_freeshot[i].ToString(),
                        Data_CouponShot = stringbuilder_couponshot[i].ToString()
                    });
                }
                cardMoney.Content = "\\" + totalcardmoney.ToString("#,##0");
                cashMoney.Content = "\\" + totalcashmoney.ToString("#,##0");
                TotalMoney.Content = "\\" + (totalcardmoney + totalcashmoney).ToString("#,##0");
                BankLV.ItemsSource = BankData.Getinstance();
            }
            catch(Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
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