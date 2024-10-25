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
using System.Threading;
using System.Windows.Threading;

namespace wpfTest.View
{
    /// <summary>
    /// AISelect.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AISelect : Page
    {

        BitmapImage backimg = new BitmapImage();
        BitmapImage ai1img_on = new BitmapImage();
        BitmapImage ai2img_on = new BitmapImage();
        BitmapImage ai3img_on = new BitmapImage();
        BitmapImage ai4img_on = new BitmapImage();
        BitmapImage ai5img_on = new BitmapImage();
        BitmapImage ai6img_on = new BitmapImage();
        BitmapImage ai1img_off = new BitmapImage();
        BitmapImage ai2img_off = new BitmapImage();
        BitmapImage ai3img_off = new BitmapImage();
        BitmapImage ai4img_off = new BitmapImage();
        BitmapImage ai5img_off = new BitmapImage();
        BitmapImage ai6img_off = new BitmapImage();
        int tsduration = Convert.ToInt32(MainWindow.count);

        public static int aiselect;

        DispatcherTimer timer = new DispatcherTimer();

        public AISelect()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                aiselect = 1;

                backimg = new BitmapImage();
                backimg.BeginInit();
                backimg.UriSource = new Uri(MainWindow.uipath + @"\AiSelectpage.png", UriKind.RelativeOrAbsolute);
                backimg.CacheOption = BitmapCacheOption.OnLoad;
                backimg.EndInit();

                if (MainWindow.inifoldername == "dearpic2")
                {
                    AI5.Margin = new Thickness(0, 0, 1920, 1080);
                    AI6.Margin = new Thickness(0, 0, 1920, 1080);
                    ai1img_on = new BitmapImage();
                    ai1img_on.BeginInit();
                    ai1img_on.UriSource = new Uri(MainWindow.uipath + @"\ai1_on.png", UriKind.RelativeOrAbsolute);
                    ai1img_on.CacheOption = BitmapCacheOption.OnLoad;
                    ai1img_on.EndInit();

                    ai1img_off = new BitmapImage();
                    ai1img_off.BeginInit();
                    ai1img_off.UriSource = new Uri(MainWindow.uipath + @"\ai1_off.png", UriKind.RelativeOrAbsolute);
                    ai1img_off.CacheOption = BitmapCacheOption.OnLoad;
                    ai1img_off.EndInit();

                    ai2img_on = new BitmapImage();
                    ai2img_on.BeginInit();
                    ai2img_on.UriSource = new Uri(MainWindow.uipath + @"\ai2_on.png", UriKind.RelativeOrAbsolute);
                    ai2img_on.CacheOption = BitmapCacheOption.OnLoad;
                    ai2img_on.EndInit();

                    ai2img_off = new BitmapImage();
                    ai2img_off.BeginInit();
                    ai2img_off.UriSource = new Uri(MainWindow.uipath + @"\ai2_off.png", UriKind.RelativeOrAbsolute);
                    ai2img_off.CacheOption = BitmapCacheOption.OnLoad;
                    ai2img_off.EndInit();

                    ai3img_on = new BitmapImage();
                    ai3img_on.BeginInit();
                    ai3img_on.UriSource = new Uri(MainWindow.uipath + @"\ai3_on.png", UriKind.RelativeOrAbsolute);
                    ai3img_on.CacheOption = BitmapCacheOption.OnLoad;
                    ai3img_on.EndInit();

                    ai3img_off = new BitmapImage();
                    ai3img_off.BeginInit();
                    ai3img_off.UriSource = new Uri(MainWindow.uipath + @"\ai3_off.png", UriKind.RelativeOrAbsolute);
                    ai3img_off.CacheOption = BitmapCacheOption.OnLoad;
                    ai3img_off.EndInit();

                    ai4img_on = new BitmapImage();
                    ai4img_on.BeginInit();
                    ai4img_on.UriSource = new Uri(MainWindow.uipath + @"\ai4_on.png", UriKind.RelativeOrAbsolute);
                    ai4img_on.CacheOption = BitmapCacheOption.OnLoad;
                    ai4img_on.EndInit();

                    ai4img_off = new BitmapImage();
                    ai4img_off.BeginInit();
                    ai4img_off.UriSource = new Uri(MainWindow.uipath + @"\ai4_off.png", UriKind.RelativeOrAbsolute);
                    ai4img_off.CacheOption = BitmapCacheOption.OnLoad;
                    ai4img_off.EndInit();

                    Backgroundimg.Source = backimg;
                    NextImg.Source = MainWindow.nextbtn;
                    BackBtn.Source = MainWindow.Backimg;

                    AI1.Source = ai1img_on;
                    AI2.Source = ai2img_off;
                    AI3.Source = ai3img_off;
                    AI4.Source = ai4img_off;

                    AI1.IsEnabled = false;
                    AI2.IsEnabled = true;
                    AI3.IsEnabled = true;
                    AI4.IsEnabled = true;

                    aiselect = 1;
                }
                else if (MainWindow.inifoldername == "harim")
                {
                    NextImg.Source = MainWindow.nextbtn;
                    NextImg.Width = MainWindow.nextbtn.Width;
                    NextImg.Height = MainWindow.nextbtn.Height;
                    BackBtn.Source = MainWindow.Backimg;
                    BackBtn.Width = MainWindow.Backimg.Width;
                    BackBtn.Height = MainWindow.Backimg.Height;

                    ai1img_on = new BitmapImage();
                    ai1img_on.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai1img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_1_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai1img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_1_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai1img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_1_on.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai1img_on.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai1img_on.DecodePixelWidth = 266;
                            ai1img_on.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai1img_on.DecodePixelWidth = 266;
                            ai1img_on.DecodePixelHeight = 467;
                            break;
                    }
                    ai1img_on.EndInit();

                    ai1img_off = new BitmapImage();
                    ai1img_off.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai1img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_1_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai1img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_1_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai1img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_1_off.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai1img_off.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai1img_off.DecodePixelWidth = 266;
                            ai1img_off.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai1img_off.DecodePixelWidth = 266;
                            ai1img_off.DecodePixelHeight = 467;
                            break;
                    }
                    ai1img_off.EndInit();

                    ai2img_on = new BitmapImage();
                    ai2img_on.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai2img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_2_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai2img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_2_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai2img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_2_on.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai2img_on.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai2img_on.DecodePixelWidth = 266;
                            ai2img_on.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai2img_on.DecodePixelWidth = 266;
                            ai2img_on.DecodePixelHeight = 467;
                            break;
                    }
                    ai2img_on.EndInit();

                    ai2img_off = new BitmapImage();
                    ai2img_off.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai2img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_2_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai2img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_2_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai2img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_2_off.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai2img_off.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai2img_off.DecodePixelWidth = 266;
                            ai2img_off.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai2img_off.DecodePixelWidth = 266;
                            ai2img_off.DecodePixelHeight = 467;
                            break;
                    }
                    ai2img_off.EndInit();

                    ai3img_on = new BitmapImage();
                    ai3img_on.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai3img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_3_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai3img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_3_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai3img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_3_on.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai3img_on.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai3img_on.DecodePixelWidth = 266;
                            ai3img_on.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai3img_on.DecodePixelWidth = 266;
                            ai3img_on.DecodePixelHeight = 467;
                            break;
                    }
                    ai3img_on.EndInit();

                    ai3img_off = new BitmapImage();
                    ai3img_off.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai3img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_3_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai3img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_3_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai3img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_3_off.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai3img_off.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai3img_off.DecodePixelWidth = 266;
                            ai3img_off.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai3img_off.DecodePixelWidth = 266;
                            ai3img_off.DecodePixelHeight = 467;
                            break;
                    }
                    ai3img_off.EndInit();

                    ai4img_on = new BitmapImage();
                    ai4img_on.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai4img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_4_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai4img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_4_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai4img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_4_on.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai4img_on.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai4img_on.DecodePixelWidth = 266;
                            ai4img_on.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai4img_on.DecodePixelWidth = 266;
                            ai4img_on.DecodePixelHeight = 467;
                            break;
                    }
                    ai4img_on.EndInit();

                    ai4img_off = new BitmapImage();
                    ai4img_off.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai4img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_4_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai4img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_4_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai4img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_4_off.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai4img_off.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai4img_off.DecodePixelWidth = 266;
                            ai4img_off.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai4img_off.DecodePixelWidth = 266;
                            ai4img_off.DecodePixelHeight = 467;
                            break;
                    }
                    ai4img_off.EndInit();

                    ai5img_on = new BitmapImage();
                    ai5img_on.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai5img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_5_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai5img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_5_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai5img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_5_on.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai5img_on.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai5img_on.DecodePixelWidth = 266;
                            ai5img_on.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai5img_on.DecodePixelWidth = 266;
                            ai5img_on.DecodePixelHeight = 467;
                            break;
                    }
                    ai5img_on.EndInit();

                    ai5img_off = new BitmapImage();
                    ai5img_off.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai5img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_5_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai5img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_5_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai5img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_5_off.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai5img_off.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai5img_off.DecodePixelWidth = 266;
                            ai5img_off.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai5img_off.DecodePixelWidth = 266;
                            ai5img_off.DecodePixelHeight = 467;
                            break;
                    }
                    ai5img_off.EndInit();

                    ai6img_on = new BitmapImage();
                    ai6img_on.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai6img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_6_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai6img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_6_on.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai6img_on.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_6_on.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai6img_on.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai6img_on.DecodePixelWidth = 266;
                            ai6img_on.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai6img_on.DecodePixelWidth = 266;
                            ai6img_on.DecodePixelHeight = 467;
                            break;
                    }
                    ai6img_on.EndInit();

                    ai6img_off = new BitmapImage();
                    ai6img_off.BeginInit();
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai6img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_6_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 2:
                            ai6img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp2_6_off.png", UriKind.RelativeOrAbsolute);
                            break;
                        case 3:
                            ai6img_off.UriSource = new Uri(MainWindow.TempPath + @"\Temp3_6_off.png", UriKind.RelativeOrAbsolute);
                            break;
                    }
                    ai6img_off.CacheOption = BitmapCacheOption.OnLoad;
                    switch (TempSelect.temp)
                    {
                        case 1:
                            ai6img_off.DecodePixelWidth = 266;
                            ai6img_off.DecodePixelHeight = 467;
                            break;
                        case 2:
                        case 3:
                            ai6img_off.DecodePixelWidth = 266;
                            ai6img_off.DecodePixelHeight = 467;
                            break;
                    }
                    ai6img_off.EndInit();

                    switch (TempSelect.temp)
                    {
                        case 1:
                            AI1.Width = 266;
                            AI1.Height = 467;
                            AI2.Width = 266;
                            AI2.Height = 467;
                            AI3.Width = 266;
                            AI3.Height = 467;
                            AI4.Width = 266;
                            AI4.Height = 467;
                            AI5.Width = 266;
                            AI5.Height = 467;
                            AI6.Width = 266;
                            AI6.Height = 467;

                            AI1.Margin = new Thickness(102, 336, 1920 - 266 - 102, 227);
                            AI2.Margin = new Thickness(390, 336, 1920 - 266 - 390, 227);
                            AI3.Margin = new Thickness(680, 336, 1920 - 266 - 680, 227);
                            AI4.Margin = new Thickness(970, 336, 1920 - 266 - 970, 227);
                            AI5.Margin = new Thickness(1260, 336, 1920 - 266 - 1260, 227);
                            AI6.Margin = new Thickness(1550, 336, 1920 - 266 - 1550, 227);
                            break;
                        case 2:
                        case 3:
                            AI1.Width = 266;
                            AI1.Height = 467;
                            AI2.Width = 266;
                            AI2.Height = 467;
                            AI3.Width = 266;
                            AI3.Height = 467;
                            AI4.Width = 266;
                            AI4.Height = 467;
                            AI5.Width = 266;
                            AI5.Height = 467;
                            AI6.Width = 266;
                            AI6.Height = 467;

                            AI1.Margin = new Thickness(102, 336, 1920 - 266 - 102, 227);
                            AI2.Margin = new Thickness(390, 336, 1920 - 266 - 390, 227);
                            AI3.Margin = new Thickness(680, 336, 1920 - 266 - 680, 227);
                            AI4.Margin = new Thickness(970, 336, 1920 - 266 - 970, 227);
                            AI5.Margin = new Thickness(1260, 336, 1920 - 266 - 1260, 227);
                            AI6.Margin = new Thickness(1550, 336, 1920 - 266 - 1550, 227);
                            break;
                    }

                    AI1.Source = ai1img_on;
                    AI2.Source = ai2img_off;
                    AI3.Source = ai3img_off;
                    AI4.Source = ai4img_off;
                    AI5.Source = ai5img_off;
                    AI6.Source = ai6img_off;

                    Backgroundimg.Source = backimg;
                    NextImg.Source = MainWindow.nextbtn;
                    BackBtn.Source = MainWindow.Backimg;
                }

                if (MainWindow.timerlocation == "left")
                {
                    Timer.Margin = new Thickness(0, 130, 1720, 0);
                }

                if (MainWindow.Timer.ToString() == "Use")
                {
                    Timer.Text = tsduration.ToString();
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += new EventHandler(Timer_Tick);
                    timer.Start();
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Dispose()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                ai1img_off = null;
                ai2img_off = null;
                ai3img_off = null;
                ai4img_off = null;
                ai5img_off = null;
                ai6img_off = null;
                ai1img_on = null;
                ai2img_on = null;
                ai3img_on = null;
                ai4img_on = null;
                ai5img_on = null;
                ai6img_on = null;
                AI1.Source = null;
                AI2.Source = null;
                AI3.Source = null;
                AI4.Source = null;
                AI1 = null;
                AI2 = null;
                AI3 = null;
                AI4 = null;
                Backgroundimg.Source = null;
                Backgroundimg = null;
                BackBtn.Source = null;
                BackBtn = null;
                NextImg.Source = null;
                NextImg = null;

                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Tick -= new EventHandler(Timer_Tick);
                    Timer.Text = null;
                    Timer = null;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #region///타이머///

        private void Timer_Tick(object sender, EventArgs e)
        {

            tsduration--;
            if (tsduration < 0)
            {
                Timer.Text = "0";
            }
            else
            {
                Timer.Text = tsduration.ToString();
            }

            if (tsduration == 0)
            {
                NextImg.IsEnabled = false;
                BackBtn.IsEnabled = false;
                AI1.IsEnabled = false;
                AI2.IsEnabled = false;
                AI3.IsEnabled = false;
                AI4.IsEnabled = false;
            }

            if (tsduration == -1)
            {
                timer.Stop();
                Dispose();
                NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        #endregion

        private void NextImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                Dispose();
                NavigationService.Navigate(new Uri("View/PageSelect.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void BackBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                timer.Stop();
                Dispose();
                NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        

        private void AI1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                aiselect = 1;

                if (MainWindow.inifoldername == "harim")
                {
                    AI1.Source = ai1img_on;
                    AI2.Source = ai2img_off;
                    AI3.Source = ai3img_off;
                    AI4.Source = ai4img_off;
                    AI5.Source = ai5img_off;
                    AI6.Source = ai6img_off;

                    AI1.IsEnabled = false;
                    AI2.IsEnabled = true;
                    AI3.IsEnabled = true;
                    AI4.IsEnabled = true;
                    AI5.IsEnabled = true;
                    AI6.IsEnabled = true;
                }
                else
                {
                    AI1.Source = ai1img_on;
                    AI2.Source = ai2img_off;
                    AI3.Source = ai3img_off;
                    AI4.Source = ai4img_off;

                    AI1.IsEnabled = false;
                    AI2.IsEnabled = true;
                    AI3.IsEnabled = true;
                    AI4.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void AI2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                aiselect = 2;

                if (MainWindow.inifoldername == "harim")
                {
                    AI1.Source = ai1img_off;
                    AI2.Source = ai2img_on;
                    AI3.Source = ai3img_off;
                    AI4.Source = ai4img_off;
                    AI5.Source = ai5img_off;
                    AI6.Source = ai6img_off;

                    AI1.IsEnabled = true;
                    AI2.IsEnabled = false;
                    AI3.IsEnabled = true;
                    AI4.IsEnabled = true;
                    AI5.IsEnabled = true;
                    AI6.IsEnabled = true;
                }
                else
                {
                    AI1.Source = ai1img_off;
                    AI2.Source = ai2img_on;
                    AI3.Source = ai3img_off;
                    AI4.Source = ai4img_off;

                    AI1.IsEnabled = true;
                    AI2.IsEnabled = false;
                    AI3.IsEnabled = true;
                    AI4.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void AI3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                aiselect = 3;

                if (MainWindow.inifoldername == "harim")
                {
                    AI1.Source = ai1img_off;
                    AI2.Source = ai2img_off;
                    AI3.Source = ai3img_on;
                    AI4.Source = ai4img_off;
                    AI5.Source = ai5img_off;
                    AI6.Source = ai6img_off;

                    AI1.IsEnabled = true;
                    AI2.IsEnabled = true;
                    AI3.IsEnabled = false;
                    AI4.IsEnabled = true;
                    AI5.IsEnabled = true;
                    AI6.IsEnabled = true;
                }
                else
                {
                    AI1.Source = ai1img_off;
                    AI2.Source = ai2img_off;
                    AI3.Source = ai3img_on;
                    AI4.Source = ai4img_off;

                    AI1.IsEnabled = true;
                    AI2.IsEnabled = true;
                    AI3.IsEnabled = false;
                    AI4.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void AI4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                aiselect = 4;

                if (MainWindow.inifoldername == "harim")
                {
                    AI1.Source = ai1img_off;
                    AI2.Source = ai2img_off;
                    AI3.Source = ai3img_off;
                    AI4.Source = ai4img_on;
                    AI5.Source = ai5img_off;
                    AI6.Source = ai6img_off;

                    AI1.IsEnabled = true;
                    AI2.IsEnabled = true;
                    AI3.IsEnabled = true;
                    AI4.IsEnabled = false;
                    AI5.IsEnabled = true;
                    AI6.IsEnabled = true;
                }
                else
                {
                    AI1.Source = ai1img_off;
                    AI2.Source = ai2img_off;
                    AI3.Source = ai3img_off;
                    AI4.Source = ai4img_on;

                    AI1.IsEnabled = true;
                    AI2.IsEnabled = true;
                    AI3.IsEnabled = true;
                    AI4.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void AI5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                aiselect = 5;

                AI1.Source = ai1img_off;
                AI2.Source = ai2img_off;
                AI3.Source = ai3img_off;
                AI4.Source = ai4img_off;
                AI5.Source = ai5img_on;
                AI6.Source = ai6img_off;

                AI1.IsEnabled = true;
                AI2.IsEnabled = true;
                AI3.IsEnabled = true;
                AI4.IsEnabled = true;
                AI5.IsEnabled = false;
                AI6.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void AI6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                aiselect = 6;

                AI1.Source = ai1img_off;
                AI2.Source = ai2img_off;
                AI3.Source = ai3img_off;
                AI4.Source = ai4img_off;
                AI5.Source = ai5img_off;
                AI6.Source = ai6img_on;

                AI1.IsEnabled = true;
                AI2.IsEnabled = true;
                AI3.IsEnabled = true;
                AI4.IsEnabled = true;
                AI5.IsEnabled = true;
                AI6.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
