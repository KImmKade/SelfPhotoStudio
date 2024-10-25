using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Net;
using System.Diagnostics.Eventing.Reader;

namespace wpfTest.View
{
    /// <summary>
    /// Final.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Final : Page
    {
        int takenshot;
        DispatcherTimer timer = new DispatcherTimer();
        int timesecond;

        public Final()
        {
            Source.Log.log.Info("Level Final 진입");
            InitializeComponent();
        }

        #region INI import

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        #endregion

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() 시작 | Final Level 설정 값 로드");

                BackImg.Source = MainWindow.final;

                timesecond = 5;
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_TikTok);
                timer.Start();
                Source.Log.log.Info("Final 타이머 시작");
            }
            catch(Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Timer_TikTok(object sender, EventArgs e)
        {
            try
            {
                string day = DateTime.Today.ToString("dd");
                timesecond--;
                if (timesecond == -1)
                {
                    timer.Stop();
                    Source.Log.log.Info("Final Level 타이머 종료");
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(MainWindow.ResizePath);
                        foreach (FileInfo files in di.EnumerateFiles())
                        {
                            files.Delete();
                            Source.Log.log.Debug("Resize 폴더 안 이미지 삭제 완료");
                        }
                        DirectoryInfo print = new DirectoryInfo(MainWindow.Printpath);
                        foreach (FileInfo file in print.EnumerateFiles())
                        {
                            file.Delete();
                            Source.Log.log.Debug("Print 폴더 안 이미지 삭제 완료");
                        }
                        DirectoryInfo gray = new DirectoryInfo(MainWindow.GrayFilterPath);
                        foreach (FileInfo file in gray.EnumerateFiles())
                        {
                            file.Delete();
                            Source.Log.log.Debug("gray 폴더 안 이미지 삭제 완료");
                        }
                    }
                    catch (Exception ex)
                    {
                        Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() -" + ex.Message);
                    }
                    Dispose();
                    if (MainWindow.takenshot.ToString() == "0")
                    {
                        takenshot = 0;
                        takenshot++;
                        MainWindow.takenshot.Clear();
                        MainWindow.takenshot.Append(takenshot);
                        WritePrivateProfileString("TakenShot", day, MainWindow.takenshot.ToString(), MainWindow.bankbookinipath);
                    }
                    else
                    {
                        if(MainWindow.day == day)
                        {
                            takenshot = Convert.ToInt32(MainWindow.takenshot.ToString());
                            takenshot++;
                            MainWindow.takenshot.Clear();
                            MainWindow.takenshot.Append(takenshot);
                            WritePrivateProfileString("TakenShot", day, MainWindow.takenshot.ToString(), MainWindow.bankbookinipath);
                        }
                        else
                        {
                            MainWindow.takenshot.Clear();
                            MainWindow.takenshot.Append(0);
                            takenshot = Convert.ToInt32(MainWindow.takenshot.ToString());
                            takenshot++;
                            MainWindow.takenshot.Clear();
                            MainWindow.takenshot.Append(takenshot);
                            WritePrivateProfileString("TakenShot", day, MainWindow.takenshot.ToString(), MainWindow.bankbookinipath);
                        }
                    }
                    TakePic.photonum = 0;
                    MainWindow.checkretakenum = 0;
                    MainWindow.cycle++;
                    if (MainWindow.camnumber.ToString() == "1")
                    {
                        Cameradispose();
                    }
                    Source.Log.log.Info("현재 사이클 수 : " + MainWindow.cycle);
                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Dispose()
        {
            BackImg.Source = null;
            BackImg = null;
            timer.Tick -= new EventHandler(Timer_TikTok);
            timer = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Cameradispose()
        {
            Main.MainCamera.Dispose();
            Main.MainCamera = null;
            Main.APIHandler.Dispose();
            Main.APIHandler = null;
            Main.SetImageAction2 = null;
            Main.SetImageAction = null;
        }
    }
}
