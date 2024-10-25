using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics.Eventing.Reader;
using Onecut.View;
using System.Diagnostics;

namespace wpfTest.View
{
    /// <summary>
    /// TempSelect.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TempSelect : Page
    {

        #region///변수///

        int tsduration = Convert.ToInt32(MainWindow.count);
        public static int temp = 0;
        DispatcherTimer timer = new DispatcherTimer();
        string checkbill = MainWindow.checkfreeornot;

        BitmapImage temp1img = new BitmapImage();
        BitmapImage temp2img = new BitmapImage();
        BitmapImage temp3img = new BitmapImage();
        BitmapImage temp4img = new BitmapImage();
        BitmapImage temp5img = new BitmapImage();
        BitmapImage temp6img = new BitmapImage();

        BitmapImage temp4_pick = new BitmapImage();
        BitmapImage temp4_nonpick = new BitmapImage();

        BitmapImage Nextbtnimgoff = new BitmapImage();
        BitmapImage nextbtnimgon = new BitmapImage();

        #endregion

        public TempSelect()
        {
            Source.Log.log.Info("템플릿 선택 페이지 진입");
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                backgrondimg.Source = MainWindow.tempselectpage;
                NextImg.Source = MainWindow.nextbtn;
                NextImg.Width = MainWindow.nextbtn.Width;
                NextImg.Height = MainWindow.nextbtn.Height;
                BackBtn.Source = MainWindow.Backimg;
                BackBtn.Width = MainWindow.Backimg.Width;
                BackBtn.Height = MainWindow.Backimg.Height;

                Timer.Foreground = MainWindow.textbrush;


                if (MainWindow.SorR == "R")
                {

                    switch (MainWindow.optiontempnum)
                    {
                        case 2:
                            switch (MainWindow.printerratio)
                            {
                                case 1:
                                    Temp1Img.Margin = new Thickness(506, 230, 1920 - 200 - 506, 230);
                                    Temp2Img.Margin = new Thickness(1212, 230, 1920 - 200 - 1212, 230);
                                    temp1border.Margin = new Thickness(506 - 10, 230 - 10, 1920 - 200 - 506 - 10, 230 - 10);
                                    temp2border.Margin = new Thickness(1212 - 10, 230 - 10, 1920 - 200 - 1212 - 10, 230 - 10);
                                    Temp3Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp3Img.IsEnabled = false;
                                    Temp4Img.IsEnabled = false;
                                    Temp5Img.IsEnabled = false;
                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 200;
                                    temp1img.DecodePixelHeight = 600;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 200;
                                    temp2img.DecodePixelHeight = 600;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;
                                    break;
                                case 2:
                                    Temp1Img.Margin = new Thickness(373, 230, 1920 - 400 - 373, 230);
                                    Temp2Img.Margin = new Thickness(1147, 230, 373, 230);
                                    temp1border.Margin = new Thickness(373 - 10, 230 - 10, 1920 - 400 - 373 - 10, 230 - 10);
                                    temp2border.Margin = new Thickness(1147 - 10, 230 - 10, 373 - 10, 230 - 10);
                                    Temp3Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp3Img.IsEnabled = false;
                                    Temp4Img.IsEnabled = false;
                                    Temp5Img.IsEnabled = false;
                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 400;
                                    temp1img.DecodePixelHeight = 600;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 400;
                                    temp2img.DecodePixelHeight = 600;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;
                                    break;
                                case 3:
                                    Temp1Img.Margin = new Thickness(240, 340, 1920 - 600 - 240, 340);
                                    Temp2Img.Margin = new Thickness(1920 - 600 - 240, 340, 240, 340);
                                    temp1border.Margin = new Thickness(240 - 10, 340 - 10, 1920 - 600 - 240 - 10, 340 - 10);
                                    temp2border.Margin = new Thickness(1920 - 600 - 240 - 10, 340 - 10, 240 - 10, 340 - 10);
                                    Temp3Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp3Img.IsEnabled = false;
                                    Temp4Img.IsEnabled = false;
                                    Temp5Img.IsEnabled = false;
                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 600;
                                    temp1img.DecodePixelHeight = 400;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 600;
                                    temp2img.DecodePixelHeight = 400;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;
                                    break;
                            }
                            break;
                        case 3:
                            switch (MainWindow.printerratio)
                            {
                                case 1:
                                    Temp1Img.Margin = new Thickness(330, 230, 1920 - 200 - 330, 230);
                                    Temp2Img.Margin = new Thickness(860, 230, 1920 - 200 - 860, 230);
                                    Temp3Img.Margin = new Thickness(1390, 230, 1920 - 1390 - 200, 230);
                                    temp1border.Margin = new Thickness(330 - 10, 230 - 10, 1920 - 200 - 330 - 10, 230 - 10);
                                    temp2border.Margin = new Thickness(860 - 10, 230 - 10, 1920 - 200 - 860 - 10, 230 - 10);
                                    temp3border.Margin = new Thickness(1390 - 10, 230 - 10, 1920 - 1390 - 200 - 10, 230 - 10);
                                    Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp4Img.IsEnabled = false;
                                    Temp5Img.IsEnabled = false;
                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 200;
                                    temp1img.DecodePixelHeight = 600;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 200;
                                    temp2img.DecodePixelHeight = 600;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 200;
                                    temp3img.DecodePixelHeight = 600;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;
                                    break;
                                case 2:
                                    Temp1Img.Margin = new Thickness(180, 230, 1920 - 400 - 180, 230);
                                    Temp2Img.Margin = new Thickness(760, 230, 1920 - 400 - 760, 230);
                                    Temp3Img.Margin = new Thickness(1340, 230, 180, 230);
                                    temp1border.Margin = new Thickness(180 - 10, 230 - 10, 1920 - 400 - 180 - 10, 230 - 10);
                                    temp2border.Margin = new Thickness(760 - 10, 230 - 10, 1920 - 400 - 760 - 10, 230 - 10);
                                    temp3border.Margin = new Thickness(1340 - 10, 230 - 10, 180 - 10, 230 - 10);
                                    Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp4Img.IsEnabled = false;
                                    Temp5Img.IsEnabled = false;
                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 400;
                                    temp1img.DecodePixelHeight = 600;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 400;
                                    temp2img.DecodePixelHeight = 600;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 400;
                                    temp3img.DecodePixelHeight = 600;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;
                                    break;
                                case 3:
                                    Temp1Img.Margin = new Thickness(30, 330, 1920 - 600 - 30, 330);
                                    Temp2Img.Margin = new Thickness(660, 330, 1920 - 600 - 660, 330);
                                    Temp3Img.Margin = new Thickness(1290, 330, 30, 330);
                                    temp1border.Margin = new Thickness(30 - 10, 330 - 10, 1920 - 600 - 30 - 10, 330 - 10);
                                    temp2border.Margin = new Thickness(660 - 10, 330 - 10, 1920 - 600 - 660 - 10, 330 - 10);
                                    temp3border.Margin = new Thickness(1290 - 10, 330 - 10, 30 - 10, 330 - 10);
                                    Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp4Img.IsEnabled = false;
                                    Temp5Img.IsEnabled = false;
                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 600;
                                    temp1img.DecodePixelHeight = 400;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 600;
                                    temp2img.DecodePixelHeight = 400;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 600;
                                    temp3img.DecodePixelHeight = 400;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;
                                    break;
                            }
                            break;
                        case 4:
                            switch (MainWindow.printerratio)
                            {
                                case 1:
                                    Temp1Img.Margin = new Thickness(224, 230, 1920 - 200 - 224, 230);
                                    Temp2Img.Margin = new Thickness(648, 230, 1920 - 200 - 648, 230);
                                    Temp3Img.Margin = new Thickness(1072, 230, 1920 - 1072 - 200, 230);
                                    Temp4Img.Margin = new Thickness(1496, 230, 1920 - 1496 - 200, 230);
                                    temp1border.Margin = new Thickness(224 - 10, 230 - 10, 1920 - 200 - 224 - 10, 230 - 10);
                                    temp2border.Margin = new Thickness(648 - 10, 230 - 10, 1920 - 200 - 648 - 10, 230 - 10);
                                    temp3border.Margin = new Thickness(1072 - 10, 230 - 10, 1920 - 1072 - 200 - 10, 230 - 10);
                                    temp4border.Margin = new Thickness(1496 - 10, 230 - 10, 1920 - 1496 - 200 - 10, 230 - 10);
                                    Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp5Img.IsEnabled = false;
                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 200;
                                    temp1img.DecodePixelHeight = 600;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 200;
                                    temp2img.DecodePixelHeight = 600;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 200;
                                    temp3img.DecodePixelHeight = 600;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;

                                    temp4img = new BitmapImage();
                                    temp4img.BeginInit();
                                    temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                                    temp4img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp4img.DecodePixelWidth = 200;
                                    temp4img.DecodePixelHeight = 600;
                                    temp4img.EndInit();
                                    Temp4Img.Source = temp4img;
                                    break;
                                case 2:
                                    Temp1Img.Margin = new Thickness(660, 250, 1920 - 200 - 660, 530);
                                    Temp2Img.Margin = new Thickness(1060, 250, 660, 530);
                                    Temp3Img.Margin = new Thickness(660, 650, 1920 - 660 - 200, 130);
                                    Temp4Img.Margin = new Thickness(1060, 650, 660, 130);
                                    temp1border.Margin = new Thickness(660 - 10, 250 - 10, 1920 - 200 - 660 - 10, 530 - 10);
                                    temp2border.Margin = new Thickness(1060 - 10, 250 - 10, 660 - 10, 530 - 10);
                                    temp3border.Margin = new Thickness(660 - 10, 650 - 10, 1920 - 660 - 200 - 10, 130 - 10);
                                    temp4border.Margin = new Thickness(1060 - 10, 650 - 10, 660 - 10, 130 - 10);
                                    Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp5Img.IsEnabled = false;
                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 200;
                                    temp1img.DecodePixelHeight = 300;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 200;
                                    temp2img.DecodePixelHeight = 300;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 200;
                                    temp3img.DecodePixelHeight = 300;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;

                                    temp4img = new BitmapImage();
                                    temp4img.BeginInit();
                                    temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                                    temp4img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp4img.DecodePixelWidth = 200;
                                    temp4img.DecodePixelHeight = 300;
                                    temp4img.EndInit();
                                    Temp4Img.Source = temp4img;
                                    break;
                                case 3:
                                    Temp1Img.Margin = new Thickness(440, 250, 1920 - 440 - 300, 630);
                                    Temp2Img.Margin = new Thickness(1180, 250, 440, 630);
                                    Temp3Img.Margin = new Thickness(440, 550, 1920 - 440 - 300, 330);
                                    Temp4Img.Margin = new Thickness(1180, 550, 440, 330);
                                    temp1border.Margin = new Thickness(440 - 10, 250 - 10, 1920 - 440 - 300 - 10, 630 - 10);
                                    temp2border.Margin = new Thickness(1180 - 10, 250 - 10, 440 - 10, 630 - 10);
                                    temp3border.Margin = new Thickness(440 - 10, 550 - 10, 1920 - 440 - 300 - 10, 330 - 10);
                                    temp4border.Margin = new Thickness(1180 - 10, 550 - 10, 440 - 10, 330 - 10);
                                    Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp5Img.IsEnabled = false;
                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 300;
                                    temp1img.DecodePixelHeight = 200;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 300;
                                    temp2img.DecodePixelHeight = 200;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 300;
                                    temp3img.DecodePixelHeight = 200;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;

                                    temp4img = new BitmapImage();
                                    temp4img.BeginInit();
                                    temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                                    temp4img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp4img.DecodePixelWidth = 300;
                                    temp4img.DecodePixelHeight = 200;
                                    temp4img.EndInit();
                                    Temp4Img.Source = temp4img;
                                    break;
                            }
                            break;
                        case 5:
                            switch (MainWindow.printerratio)
                            {
                                case 1:
                                    Temp1Img.Margin = new Thickness(153, 230, 1920 - 200 - 153, 230);
                                    Temp2Img.Margin = new Thickness(506, 230, 1920 - 200 - 506, 230);
                                    Temp3Img.Margin = new Thickness(859, 230, 1920 - 859 - 200, 230);
                                    Temp4Img.Margin = new Thickness(1212, 230, 1920 - 1212 - 200, 230);
                                    Temp5Img.Margin = new Thickness(1565, 230, 1920 - 1565 - 200, 230);
                                    temp1border.Margin = new Thickness(153 - 10, 230 - 10, 1920 - 200 - 153 - 10, 230 - 10);
                                    temp2border.Margin = new Thickness(506 - 10, 230 - 10, 1920 - 200 - 506 - 10, 230 - 10);
                                    temp3border.Margin = new Thickness(859 - 10, 230 - 10, 1920 - 859 - 200 - 10, 230 - 10);
                                    temp4border.Margin = new Thickness(1212 - 10, 230 - 10, 1920 - 1212 - 200 - 10, 230 - 10);
                                    temp5border.Margin = new Thickness(1565 - 10, 230 - 10, 1920 - 1565 - 200 - 10, 230 - 10);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 200;
                                    temp1img.DecodePixelHeight = 600;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 200;
                                    temp2img.DecodePixelHeight = 600;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 200;
                                    temp3img.DecodePixelHeight = 600;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;

                                    temp4img = new BitmapImage();
                                    temp4img.BeginInit();
                                    temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                                    temp4img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp4img.DecodePixelWidth = 200;
                                    temp4img.DecodePixelHeight = 600;
                                    temp4img.EndInit();
                                    Temp4Img.Source = temp4img;

                                    temp5img = new BitmapImage();
                                    temp5img.BeginInit();
                                    temp5img.UriSource = new Uri(MainWindow.TempPath + @"\Temp5.png", UriKind.RelativeOrAbsolute);
                                    temp5img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp5img.DecodePixelWidth = 200;
                                    temp5img.DecodePixelHeight = 600;
                                    temp5img.EndInit();
                                    Temp5Img.Source = temp5img;
                                    break;
                                case 2:
                                    Temp1Img.Margin = new Thickness(330, 250, 1920 - 200 - 330, 530);
                                    Temp2Img.Margin = new Thickness(860, 230, 1920 - 200 - 860, 530);
                                    Temp3Img.Margin = new Thickness(1360, 230, 1920 - 1360 - 200, 530);
                                    Temp4Img.Margin = new Thickness(660, 650, 1920 - 660 - 200, 130);
                                    Temp5Img.Margin = new Thickness(1060, 650, 1920 - 1060 - 200, 130);
                                    temp1border.Margin = new Thickness(330 - 10, 250 - 10, 1920 - 200 - 330 - 10, 530 - 10);
                                    temp2border.Margin = new Thickness(860 - 10, 230 - 10, 1920 - 200 - 860 - 10, 530 - 10);
                                    temp3border.Margin = new Thickness(1360 - 10, 230 - 10, 1920 - 1360 - 200 - 10, 530 - 10);
                                    temp4border.Margin = new Thickness(660 - 10, 650 - 10, 1920 - 660 - 200 - 10, 130 - 10);
                                    temp5border.Margin = new Thickness(1060 - 10, 650 - 10, 1920 - 1060 - 200 - 10, 130 - 10);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 200;
                                    temp1img.DecodePixelHeight = 300;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 200;
                                    temp2img.DecodePixelHeight = 300;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 200;
                                    temp3img.DecodePixelHeight = 300;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;

                                    temp4img = new BitmapImage();
                                    temp4img.BeginInit();
                                    temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                                    temp4img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp4img.DecodePixelWidth = 200;
                                    temp4img.DecodePixelHeight = 300;
                                    temp4img.EndInit();
                                    Temp4Img.Source = temp4img;

                                    temp5img = new BitmapImage();
                                    temp5img.BeginInit();
                                    temp5img.UriSource = new Uri(MainWindow.TempPath + @"\Temp5.png", UriKind.RelativeOrAbsolute);
                                    temp5img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp5img.DecodePixelWidth = 200;
                                    temp5img.DecodePixelHeight = 300;
                                    temp5img.EndInit();
                                    Temp5Img.Source = temp5img;
                                    break;
                                case 3:
                                    Temp1Img.Margin = new Thickness(255, 250, 1920 - 300 - 255, 630);
                                    Temp2Img.Margin = new Thickness(810, 250, 1920 - 300 - 810, 630);
                                    Temp3Img.Margin = new Thickness(1365, 250, 1920 - 1365 - 300, 630);
                                    Temp4Img.Margin = new Thickness(440, 550, 1920 - 440 - 300, 330);
                                    Temp5Img.Margin = new Thickness(1180, 550, 1920 - 1180 - 300, 330);
                                    temp1border.Margin = new Thickness(255 - 10, 250 - 10, 1920 - 300 - 255 - 10, 630 - 10);
                                    temp2border.Margin = new Thickness(810 - 10, 250 - 10, 1920 - 300 - 810 - 10, 630 - 10);
                                    temp3border.Margin = new Thickness(1365 - 10, 250 - 10, 1920 - 1365 - 300 - 10, 630 - 10);
                                    temp4border.Margin = new Thickness(440 - 10, 550 - 10, 1920 - 440 - 300 - 10, 330 - 10);
                                    temp5border.Margin = new Thickness(1180 - 10, 550 - 10, 1920 - 1180 - 300 - 10, 330 - 10);
                                    Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                                    temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                    Temp6Img.IsEnabled = false;

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 300;
                                    temp1img.DecodePixelHeight = 200;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 300;
                                    temp2img.DecodePixelHeight = 200;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 300;
                                    temp3img.DecodePixelHeight = 200;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;

                                    temp4img = new BitmapImage();
                                    temp4img.BeginInit();
                                    temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                                    temp4img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp4img.DecodePixelWidth = 300;
                                    temp4img.DecodePixelHeight = 200;
                                    temp4img.EndInit();
                                    Temp4Img.Source = temp4img;

                                    temp5img = new BitmapImage();
                                    temp5img.BeginInit();
                                    temp5img.UriSource = new Uri(MainWindow.TempPath + @"\Temp5.png", UriKind.RelativeOrAbsolute);
                                    temp5img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp5img.DecodePixelWidth = 300;
                                    temp5img.DecodePixelHeight = 200;
                                    temp5img.EndInit();
                                    Temp5Img.Source = temp5img;
                                    break;
                            }
                            break;
                        case 6:
                            switch (MainWindow.printerratio)
                            {
                                case 1:
                                    Temp1Img.Margin = new Thickness(102, 230, 1920 - 200 - 102, 230);
                                    Temp2Img.Margin = new Thickness(404, 230, 1920 - 200 - 404, 230);
                                    Temp3Img.Margin = new Thickness(706, 230, 1920 - 706 - 200, 230);
                                    Temp4Img.Margin = new Thickness(1008, 230, 1920 - 1008 - 200, 230);
                                    Temp5Img.Margin = new Thickness(1310, 230, 1920 - 1310 - 200, 230);
                                    Temp6Img.Margin = new Thickness(1612, 230, 1920 - 1612 - 200, 230);
                                    temp1border.Margin = new Thickness(102 - 10, 230 - 10, 1920 - 200 - 102 - 10, 230 - 10);
                                    temp2border.Margin = new Thickness(404 - 10, 230 - 10, 1920 - 200 - 404 - 10, 230 - 10);
                                    temp3border.Margin = new Thickness(706 - 10, 230 - 10, 1920 - 706 - 200 - 10, 230 - 10);
                                    temp4border.Margin = new Thickness(1008 - 10, 230 - 10, 1920 - 1008 - 200 - 10, 230 - 10);
                                    temp5border.Margin = new Thickness(1310 - 10, 230 - 10, 1920 - 1310 - 200 - 10, 230 - 10);
                                    temp6border.Margin = new Thickness(1612 - 10, 230 - 10, 1920 - 1612 - 200 - 10, 230 - 10);

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 200;
                                    temp1img.DecodePixelHeight = 600;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 200;
                                    temp2img.DecodePixelHeight = 600;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 200;
                                    temp3img.DecodePixelHeight = 600;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;

                                    temp4img = new BitmapImage();
                                    temp4img.BeginInit();
                                    temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                                    temp4img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp4img.DecodePixelWidth = 200;
                                    temp4img.DecodePixelHeight = 600;
                                    temp4img.EndInit();
                                    Temp4Img.Source = temp4img;

                                    temp5img = new BitmapImage();
                                    temp5img.BeginInit();
                                    temp5img.UriSource = new Uri(MainWindow.TempPath + @"\Temp5.png", UriKind.RelativeOrAbsolute);
                                    temp5img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp5img.DecodePixelWidth = 200;
                                    temp5img.DecodePixelHeight = 600;
                                    temp5img.EndInit();
                                    Temp5Img.Source = temp5img;

                                    temp6img = new BitmapImage();
                                    temp6img.BeginInit();
                                    temp6img.UriSource = new Uri(MainWindow.TempPath + @"\Temp6.png", UriKind.RelativeOrAbsolute);
                                    temp6img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp6img.DecodePixelWidth = 200;
                                    temp6img.DecodePixelHeight = 600;
                                    temp6img.EndInit();
                                    Temp6Img.Source = temp6img;
                                    break;
                                case 2:
                                    Temp1Img.Margin = new Thickness(330, 250, 1920 - 200 - 330, 530);
                                    Temp2Img.Margin = new Thickness(860, 250, 1920 - 200 - 860, 530);
                                    Temp3Img.Margin = new Thickness(1360, 250, 1920 - 1360 - 200, 530);
                                    Temp4Img.Margin = new Thickness(330, 650, 1920 - 330 - 200, 130);
                                    Temp5Img.Margin = new Thickness(860, 650, 1920 - 860 - 200, 130);
                                    Temp6Img.Margin = new Thickness(1360, 650, 1920 - 1360 - 200, 130);
                                    temp1border.Margin = new Thickness(330 - 10, 250 - 10, 1920 - 200 - 330 - 10, 530 - 10);
                                    temp2border.Margin = new Thickness(860 - 10, 250 - 10, 1920 - 200 - 860 - 10, 530 - 10);
                                    temp3border.Margin = new Thickness(1360 - 10, 250 - 10, 1920 - 1360 - 200 - 10, 530 - 10);
                                    temp4border.Margin = new Thickness(330 - 10, 650 - 10, 1920 - 330 - 200 - 10, 130 - 10);
                                    temp5border.Margin = new Thickness(860 - 10, 650 - 10, 1920 - 860 - 200 - 10, 130 - 10);
                                    temp6border.Margin = new Thickness(1360 - 10, 650 - 10, 1920 - 1360 - 200 - 10, 130 - 10);

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 200;
                                    temp1img.DecodePixelHeight = 300;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 200;
                                    temp2img.DecodePixelHeight = 300;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 200;
                                    temp3img.DecodePixelHeight = 300;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;

                                    temp4img = new BitmapImage();
                                    temp4img.BeginInit();
                                    temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                                    temp4img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp4img.DecodePixelWidth = 200;
                                    temp4img.DecodePixelHeight = 300;
                                    temp4img.EndInit();
                                    Temp4Img.Source = temp4img;

                                    temp5img = new BitmapImage();
                                    temp5img.BeginInit();
                                    temp5img.UriSource = new Uri(MainWindow.TempPath + @"\Temp5.png", UriKind.RelativeOrAbsolute);
                                    temp5img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp5img.DecodePixelWidth = 200;
                                    temp5img.DecodePixelHeight = 300;
                                    temp5img.EndInit();
                                    Temp5Img.Source = temp5img;

                                    temp6img = new BitmapImage();
                                    temp6img.BeginInit();
                                    temp6img.UriSource = new Uri(MainWindow.TempPath + @"\Temp6.png", UriKind.RelativeOrAbsolute);
                                    temp6img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp6img.DecodePixelWidth = 200;
                                    temp6img.DecodePixelHeight = 600;
                                    temp6img.EndInit();
                                    Temp6Img.Source = temp6img;
                                    break;
                                case 3:
                                    Temp1Img.Margin = new Thickness(255, 250, 1920 - 300 - 255, 630);
                                    Temp2Img.Margin = new Thickness(810, 250, 1920 - 300 - 810, 630);
                                    Temp3Img.Margin = new Thickness(1365, 250, 1920 - 1365 - 300, 630);
                                    Temp4Img.Margin = new Thickness(255, 550, 1920 - 255 - 300, 330);
                                    Temp5Img.Margin = new Thickness(810, 550, 1920 - 810 - 300, 330);
                                    Temp6Img.Margin = new Thickness(1365, 550, 1920 - 1365 - 300, 330);
                                    temp1border.Margin = new Thickness(255 - 10, 250 - 10, 1920 - 300 - 255 - 10, 630 - 10);
                                    temp2border.Margin = new Thickness(810 - 10, 250 - 10, 1920 - 300 - 810 - 10, 630 - 10);
                                    temp3border.Margin = new Thickness(1365 - 10, 250 - 10, 1920 - 300 - 1365 - 10, 630 - 10);
                                    temp4border.Margin = new Thickness(255 - 10, 550 - 10, 1920 - 300 - 255 - 10, 330 - 10);
                                    temp5border.Margin = new Thickness(810 - 10, 550 - 10, 1920 - 300 - 810 - 10, 330 - 10);
                                    temp6border.Margin = new Thickness(1365 - 10, 550 - 10, 1920 - 300 - 1365 - 10, 330 - 10);

                                    temp1img = new BitmapImage();
                                    temp1img.BeginInit();
                                    temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                                    temp1img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp1img.DecodePixelWidth = 300;
                                    temp1img.DecodePixelHeight = 200;
                                    temp1img.EndInit();
                                    Temp1Img.Source = temp1img;

                                    temp2img = new BitmapImage();
                                    temp2img.BeginInit();
                                    temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                                    temp2img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp2img.DecodePixelWidth = 300;
                                    temp2img.DecodePixelHeight = 200;
                                    temp2img.EndInit();
                                    Temp2Img.Source = temp2img;

                                    temp3img = new BitmapImage();
                                    temp3img.BeginInit();
                                    temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                                    temp3img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp3img.DecodePixelWidth = 300;
                                    temp3img.DecodePixelHeight = 200;
                                    temp3img.EndInit();
                                    Temp3Img.Source = temp3img;

                                    temp4img = new BitmapImage();
                                    temp4img.BeginInit();
                                    temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                                    temp4img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp4img.DecodePixelWidth = 300;
                                    temp4img.DecodePixelHeight = 200;
                                    temp4img.EndInit();
                                    Temp4Img.Source = temp4img;

                                    temp5img = new BitmapImage();
                                    temp5img.BeginInit();
                                    temp5img.UriSource = new Uri(MainWindow.TempPath + @"\Temp5.png", UriKind.RelativeOrAbsolute);
                                    temp5img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp5img.DecodePixelWidth = 300;
                                    temp5img.DecodePixelHeight = 200;
                                    temp5img.EndInit();
                                    Temp5Img.Source = temp5img;

                                    temp6img = new BitmapImage();
                                    temp6img.BeginInit();
                                    temp6img.UriSource = new Uri(MainWindow.TempPath + @"\Temp6.png", UriKind.RelativeOrAbsolute);
                                    temp6img.CacheOption = BitmapCacheOption.OnLoad;
                                    temp6img.DecodePixelWidth = 300;
                                    temp6img.DecodePixelHeight = 200;
                                    temp6img.EndInit();
                                    Temp6Img.Source = temp6img;
                                    break;
                            }
                            break;
                    }

                    Temp1Img.IsEnabled = false;
                    Temp2Img.IsEnabled = true;
                    Temp3Img.IsEnabled = true;
                    Temp4Img.IsEnabled = true;
                    Temp5Img.IsEnabled = true;
                    Temp6Img.IsEnabled = true;
                    temp1border.Opacity = 1;
                    temp2border.Opacity = 0;
                    temp3border.Opacity = 0;
                    temp4border.Opacity = 0;
                    temp5border.Opacity = 0;
                    temp6border.Opacity = 0;
                }
                else
                {
                    if (MainWindow.inifoldername == "dearpic1" || MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3")
                    {
                        temp1fee.Margin = new Thickness(580, 775, 1920 - 200 - 580, 1080 - 775 - 30);
                        temp2fee.Margin = new Thickness(900, 775, 1920 - 200 - 900, 1080 - 775 - 30);
                        temp3fee.Margin = new Thickness(1220, 775, 1920 - 200 - 1220, 1080 - 775 - 30);

                        if (MainWindow.moneyset != "0")
                        {
                            temp1fee.Text = "\\ " + Convert.ToInt32(MainWindow.moneyset).ToString("#,##0") + "원";
                            temp2fee.Text = "\\ " + Convert.ToInt32(MainWindow.temp2fee).ToString("#,##0") + "원";
                            temp3fee.Text = "\\ " + Convert.ToInt32(MainWindow.temp3fee).ToString("#,##0") + "원";
                        }

                        Temp1Img.Source = MainWindow.temp1_pick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                        Temp3Img.Source = MainWindow.temp3_nonpick;

                        Temp1Img.Width = 302;
                        Temp1Img.Height = 493;
                        Temp2Img.Width = 302;
                        Temp2Img.Height = 493;
                        Temp3Img.Width = 302;
                        Temp3Img.Height = 493;

                        Temp1Img.Margin = new Thickness(491, 348, 1920 - 302 - 491, 1080 - 348 - 493);
                        Temp2Img.Margin = new Thickness(811, 348, 1920 - 811 - 302, 1080 - 348 - 493);
                        Temp3Img.Margin = new Thickness(1131, 348, 1920 - 1131 - 302, 1080 - 348 - 493);

                        Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp2border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);
                    }
                    else if (MainWindow.inifoldername == "dearpic2")
                    {
                        temp1fee.Margin = new Thickness(720, 775, 1920 - 720 - 200, 1080 - 30 - 775);
                        temp2fee.Margin = new Thickness(1097, 775, 1920 - 1097 - 200, 1080 - 30 - 775);

                        if (MainWindow.moneyset != "0")
                        {
                            temp1fee.Text = "\\ " + Convert.ToInt32(MainWindow.moneyset).ToString("#,##0") + "원";
                            temp2fee.Text = "\\ " + Convert.ToInt32(MainWindow.temp2fee).ToString("#,##0") + "원";
                        }

                        Temp1Img.Source = MainWindow.temp1_pick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;

                        Temp1Img.Width = 357;
                        Temp1Img.Height = 493;
                        Temp2Img.Width = 357;
                        Temp2Img.Height = 493;
                        Temp3Img.Width = 349;
                        Temp3Img.Height = 535;

                        Temp1Img.Margin = new Thickness(594, 348, 1920 - 357 - 594, 1080 - 348 - 493);
                        Temp2Img.Margin = new Thickness(971, 348, 1920 - 971 - 357, 1080 - 348 - 493);
                        Temp3Img.Margin = new Thickness(0, 0, 1920, 1080);

                        Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp2border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "animalhospital")
                    {
                        temp1fee.Margin = new Thickness(720, 775, 1920 - 720 - 200, 1080 - 30 - 775);
                        temp2fee.Margin = new Thickness(1097, 775, 1920 - 1097 - 200, 1080 - 30 - 775);

                        Temp1Img.Source = MainWindow.temp1_pick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;

                        Temp1Img.Width = 349;
                        Temp1Img.Height = 535;
                        Temp2Img.Width = 349;
                        Temp2Img.Height = 535;
                        Temp3Img.Width = 349;
                        Temp3Img.Height = 535;

                        Temp1Img.Margin = new Thickness(578, 304, 1920 - 578 - 349, 1080 - 304 - 535);
                        Temp2Img.Margin = new Thickness(992, 304, 1920 - 992 - 349, 1080 - 304 - 535);
                        Temp3Img.Margin = new Thickness(0, 0, 1920, 1080);

                        Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp2border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "r81")
                    {
                        Temp1Img.Source = MainWindow.temp1_pick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;

                        Temp1Img.Width = 346;
                        Temp1Img.Height = 532;
                        Temp2Img.Width = 346;
                        Temp2Img.Height = 532;
                        Temp3Img.Width = 349;
                        Temp3Img.Height = 535;

                        Temp1Img.Margin = new Thickness(599, 302, 1920 - 599 - 346, 1080 - 302 - 535);
                        Temp2Img.Margin = new Thickness(976, 302, 1920 - 976 - 346, 1080 - 302 - 535);
                        Temp3Img.Margin = new Thickness(0, 0, 1920, 1080);

                        Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp2border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "r80")
                    {
                        Temp2Img.Width = 400;

                        Temp1Img.Margin = new Thickness(610, 230, 1920 - 610 - 200, 230);
                        Temp2Img.Margin = new Thickness(910, 230, 1920 - 910 - 400, 230);
                        Temp3Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1border.Margin = new Thickness(610 - 10, 230 - 10, 1920 - 610 - 200 - 10, 230 - 10);
                        temp2border.Margin = new Thickness(910 - 10, 230 - 10, 1920 - 910 - 400 - 10, 230 - 10);
                        temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1fee.Margin = new Thickness(0 ,0 ,1920 ,1080);
                        temp2fee.Margin = new Thickness(0, 0, 1920, 1080);
                        temp3fee.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4fee.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5fee.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6fee.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1img = new BitmapImage();
                        temp1img.BeginInit();
                        temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                        temp1img.CacheOption = BitmapCacheOption.OnLoad;
                        temp1img.DecodePixelWidth = 200;
                        temp1img.DecodePixelHeight = 600;
                        temp1img.EndInit();
                        Temp1Img.Source = temp1img;

                        temp2img = new BitmapImage();
                        temp2img.BeginInit();
                        temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                        temp2img.CacheOption = BitmapCacheOption.OnLoad;
                        temp2img.DecodePixelWidth = 400;
                        temp2img.DecodePixelHeight = 600;
                        temp2img.EndInit();
                        Temp2Img.Source = temp2img;
                    }
                    else if (MainWindow.inifoldername == "r24" || MainWindow.inifoldername == "r25")
                    {
                        Temp2Img.Width = 400;

                        Temp1Img.Margin = new Thickness(610, 230, 1920 - 610 - 200, 230);
                        Temp2Img.Margin = new Thickness(910, 230, 1920 - 910 - 400, 230);
                        Temp3Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1border.Margin = new Thickness(610 - 10, 230 - 10, 1920 - 610 - 200 - 10, 230 - 10);
                        temp2border.Margin = new Thickness(910 - 10, 230 - 10, 1920 - 910 - 400 - 10, 230 - 10);
                        temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1fee.Margin = new Thickness(0, 0, 1920, 1080);
                        temp2fee.Margin = new Thickness(0, 0, 1920, 1080);
                        temp3fee.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4fee.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5fee.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6fee.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1img = new BitmapImage();
                        temp1img.BeginInit();
                        temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                        temp1img.CacheOption = BitmapCacheOption.OnLoad;
                        temp1img.DecodePixelWidth = 200;
                        temp1img.DecodePixelHeight = 600;
                        temp1img.EndInit();
                        Temp1Img.Source = temp1img;

                        temp2img = new BitmapImage();
                        temp2img.BeginInit();
                        temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                        temp2img.CacheOption = BitmapCacheOption.OnLoad;
                        temp2img.DecodePixelWidth = 400;
                        temp2img.DecodePixelHeight = 600;
                        temp2img.EndInit();
                        Temp2Img.Source = temp2img;
                    }
                    else if (MainWindow.inifoldername == "r28")
                    {
                        Temp1Img.Width = 400;
                        Temp2Img.Width = 400;
                        Temp3Img.Width = 400;
                        Temp4Img.Width = 400;

                        Temp1Img.Margin = new Thickness(64, 305, 1920 - 400 - 64, 1080 - 305 - 534);
                        Temp2Img.Margin = new Thickness(528, 305, 1920 - 400 - 528, 1080 - 305 - 534);
                        Temp3Img.Margin = new Thickness(992, 305, 1920 - 400 - 992, 1080 - 305 - 534);
                        Temp4Img.Margin = new Thickness(1456, 305, 1920 - 400 - 1456, 1080 - 305 - 534);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1border.Margin = new Thickness(64 - 10, 305 - 10, 1920 - 400 - 64 - 10, 1080 - 305 - 534 - 10);
                        temp2border.Margin = new Thickness(528 - 10, 305 - 10, 1920 - 400 - 528 - 10, 1080 - 305 - 534 - 10);
                        temp3border.Margin = new Thickness(992 - 10, 305 - 10, 1920 - 400 - 992 - 10, 1080 - 305 - 534 - 10);
                        temp4border.Margin = new Thickness(1456 - 10, 305 - 10, 1920 - 400 - 1456 - 10, 1080 - 305 - 534 - 10);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1img = new BitmapImage();
                        temp1img.BeginInit();
                        temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                        temp1img.CacheOption = BitmapCacheOption.OnLoad;
                        temp1img.DecodePixelWidth = 400;
                        temp1img.DecodePixelHeight = 600;
                        temp1img.EndInit();
                        Temp1Img.Source = temp1img;

                        temp2img = new BitmapImage();
                        temp2img.BeginInit();
                        temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                        temp2img.CacheOption = BitmapCacheOption.OnLoad;
                        temp2img.DecodePixelWidth = 400;
                        temp2img.DecodePixelHeight = 600;
                        temp2img.EndInit();
                        Temp2Img.Source = temp2img;

                        temp3img = new BitmapImage();
                        temp3img.BeginInit();
                        temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                        temp3img.CacheOption = BitmapCacheOption.OnLoad;
                        temp3img.DecodePixelWidth = 400;
                        temp3img.DecodePixelHeight = 600;
                        temp3img.EndInit();
                        Temp3Img.Source = temp3img;

                        temp4img = new BitmapImage();
                        temp4img.BeginInit();
                        temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                        temp4img.CacheOption = BitmapCacheOption.OnLoad;
                        temp4img.DecodePixelWidth = 400;
                        temp4img.DecodePixelHeight = 600;
                        temp4img.EndInit();
                        Temp4Img.Source = temp4img;
                    }
                    else if (MainWindow.inifoldername == "r30")
                    {
                        Temp1Img.Margin = new Thickness(224, 230, 1920 - 200 - 224, 230);
                        Temp2Img.Margin = new Thickness(648, 230, 1920 - 200 - 648, 230);
                        Temp3Img.Margin = new Thickness(1072, 230, 1920 - 1072 - 200, 230);
                        Temp4Img.Margin = new Thickness(1496, 230, 1920 - 1496 - 200, 230);
                        temp1border.Margin = new Thickness(224 - 10, 230 - 10, 1920 - 200 - 224 - 10, 230 - 10);
                        temp2border.Margin = new Thickness(648 - 10, 230 - 10, 1920 - 200 - 648 - 10, 230 - 10);
                        temp3border.Margin = new Thickness(1072 - 10, 230 - 10, 1920 - 1072 - 200 - 10, 230 - 10);
                        temp4border.Margin = new Thickness(1496 - 10, 230 - 10, 1920 - 1496 - 200 - 10, 230 - 10);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                        Temp5Img.IsEnabled = false;
                        Temp6Img.IsEnabled = false;

                        temp1img = new BitmapImage();
                        temp1img.BeginInit();
                        temp1img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                        temp1img.CacheOption = BitmapCacheOption.OnLoad;
                        temp1img.DecodePixelWidth = 200;
                        temp1img.DecodePixelHeight = 600;
                        temp1img.EndInit();
                        Temp1Img.Source = temp1img;

                        temp2img = new BitmapImage();
                        temp2img.BeginInit();
                        temp2img.UriSource = new Uri(MainWindow.TempPath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                        temp2img.CacheOption = BitmapCacheOption.OnLoad;
                        temp2img.DecodePixelWidth = 200;
                        temp2img.DecodePixelHeight = 600;
                        temp2img.EndInit();
                        Temp2Img.Source = temp2img;

                        temp3img = new BitmapImage();
                        temp3img.BeginInit();
                        temp3img.UriSource = new Uri(MainWindow.TempPath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                        temp3img.CacheOption = BitmapCacheOption.OnLoad;
                        temp3img.DecodePixelWidth = 200;
                        temp3img.DecodePixelHeight = 600;
                        temp3img.EndInit();
                        Temp3Img.Source = temp3img;

                        temp4img = new BitmapImage();
                        temp4img.BeginInit();
                        temp4img.UriSource = new Uri(MainWindow.TempPath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                        temp4img.CacheOption = BitmapCacheOption.OnLoad;
                        temp4img.DecodePixelWidth = 200;
                        temp4img.DecodePixelHeight = 600;
                        temp4img.EndInit();
                        Temp4Img.Source = temp4img;
                    }
                    else if (MainWindow.inifoldername == "tech")
                    {
                        Temp1Img.Width = 346;
                        Temp1Img.Height = 534;
                        Temp2Img.Width = 346;
                        Temp2Img.Height = 534;
                        Temp3Img.Width = 346;
                        Temp3Img.Height = 534;
                        Temp4Img.Width = 346;
                        Temp4Img.Height = 534;

                        Temp1Img.Margin = new Thickness(245, 305, 1920 - 346 - 245, 1080 - 305 - 534);
                        Temp2Img.Margin = new Thickness(606, 305, 1920 - 346 - 606, 1080 - 305 - 534);
                        Temp3Img.Margin = new Thickness(967, 305, 1920 - 346 - 967, 1080 - 305 - 534);
                        Temp4Img.Margin = new Thickness(1328, 305, 1920 - 346 - 1328, 1080 - 305 - 534);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp2border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;

                        temp4_pick = new BitmapImage();
                        temp4_pick.BeginInit();
                        temp4_pick.UriSource = new Uri(MainWindow.uipath + @"\Temp4_Pick.png", UriKind.RelativeOrAbsolute);
                        temp4_pick.CacheOption = BitmapCacheOption.OnLoad;
                        temp4_pick.EndInit();

                        temp4_nonpick = new BitmapImage();
                        temp4_nonpick.BeginInit();
                        temp4_nonpick.UriSource = new Uri(MainWindow.uipath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                        temp4_nonpick.CacheOption = BitmapCacheOption.OnLoad;
                        temp4_nonpick.EndInit();

                        Temp1Img.Source = MainWindow.temp1_pick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                        Temp3Img.Source = MainWindow.temp3_nonpick;
                        Temp4Img.Source = temp4_nonpick;
                    }
                    else if (MainWindow.inifoldername =="hhh")
                    {
                        Timer.Margin = new Thickness(1613, 130, 107, 850);
                        switch (MainWindow.optiontempnum)
                        {
                            case 2:
                                Temp1Img.Source = MainWindow.temp1_pick;
                                Temp2Img.Source = MainWindow.temp2_nonpick;

                                Temp1Img.Width = 323;
                                Temp1Img.Height = 477;
                                Temp2Img.Width = 323;
                                Temp2Img.Height = 532;
                                Temp3Img.Width = 323;
                                Temp3Img.Height = 535;

                                Temp1Img.Margin = new Thickness(745, 336, 1920 - 745 - 323, 1080 - 336 - 477);
                                Temp2Img.Margin = new Thickness(1174, 336, 1920 - 1174 - 323, 1080 - 336 - 477);
                                Temp3Img.Margin = new Thickness(0, 0, 1920, 1080);

                                Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                                Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                                temp1border.Margin = new Thickness(0, 0, 1920, 1080);
                                temp2border.Margin = new Thickness(0, 0, 1920, 1080);
                                temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                                temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                                temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                Temp1Img.IsEnabled = false;
                                Temp2Img.IsEnabled = true;
                                Temp3Img.IsEnabled = false;
                                temp1border.Opacity = 0;
                                temp2border.Opacity = 0;
                                temp3border.Opacity = 0;
                                temp4border.Opacity = 0;
                                temp5border.Opacity = 0;
                                temp6border.Opacity = 0;
                                break;
                            case 3:
                                Temp1Img.Source = MainWindow.temp1_pick;
                                Temp2Img.Source = MainWindow.temp2_nonpick;
                                Temp3Img.Source = MainWindow.temp3_nonpick;

                                Temp1Img.Width = 323;
                                Temp1Img.Height = 477;
                                Temp2Img.Width = 323;
                                Temp2Img.Height = 477;
                                Temp3Img.Width = 323;
                                Temp3Img.Height = 477;

                                Temp1Img.Margin = new Thickness(326, 336, 1920 - 323 - 326, 1080 - 336 - 477);
                                Temp2Img.Margin = new Thickness(755, 336, 1920 - 323 - 755, 1080 - 336 - 477);
                                Temp3Img.Margin = new Thickness(1183, 336, 1920 - 323 - 1183, 1080 - 336 - 477);

                                Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                                Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                                Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                                temp1border.Margin = new Thickness(0, 0, 1920, 1080);
                                temp2border.Margin = new Thickness(0, 0, 1920, 1080);
                                temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                                temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                                temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                                temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                                Temp1Img.IsEnabled = false;
                                Temp2Img.IsEnabled = true;
                                Temp3Img.IsEnabled = true;
                                temp1border.Opacity = 0;
                                temp2border.Opacity = 0;
                                temp3border.Opacity = 0;
                                temp4border.Opacity = 0;
                                temp5border.Opacity = 0;
                                temp6border.Opacity = 0;
                                break;
                        }
                    }
                    else
                    {
                        Temp1Img.Source = MainWindow.temp1_pick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                        Temp3Img.Source = MainWindow.temp3_nonpick;

                        Temp1Img.Width = 349;
                        Temp1Img.Height = 535;
                        Temp2Img.Width = 349;
                        Temp2Img.Height = 535;
                        Temp3Img.Width = 349;
                        Temp3Img.Height = 535;

                        Temp1Img.Margin = new Thickness(0, 0, 850, 0);
                        Temp2Img.Margin = new Thickness(0, 0, 0, 0);
                        Temp3Img.Margin = new Thickness(850, 0, 0, 0);

                        Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp2border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }

                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        Temp1Img.Margin = new Thickness(793, 292, 1920 - 304 - 793, 1080 - 292 - 545);
                        Temp2Img.Margin = new Thickness(335, 294, 1920 - 335 - 305, 1080 - 294 - 538);
                        Temp3Img.Margin = new Thickness(1247, 292, 1920 - 327 - 1247, 1080 - 292 - 540);

                        BackBtn.Width = 370;
                        BackBtn.Height = 100;
                        NextImg.Width = 370;
                        NextImg.Height = 100;

                        BackBtn.Margin = new Thickness(569, 927, 1920 - 370 - 569, 1080 - 927 - 100);
                        NextImg.Margin = new Thickness(979, 927, 1920 - 370 - 979, 1080 - 927 - 100);

                        Temp1Img.Source = MainWindow.temp1_nonpick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                        Temp3Img.Source = MainWindow.temp3_nonpick;

                        Temp1Img.Width = 305;
                        Temp1Img.Height = 538;
                        Temp2Img.Width = 304;
                        Temp2Img.Height = 545;
                        Temp3Img.Width = 327;
                        Temp3Img.Height = 540;

                        Temp4Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp5Img.Margin = new Thickness(0, 0, 1920, 1080);
                        Temp6Img.Margin = new Thickness(0, 0, 1920, 1080);

                        temp1border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp2border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp3border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp4border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp5border.Margin = new Thickness(0, 0, 1920, 1080);
                        temp6border.Margin = new Thickness(0, 0, 1920, 1080);

                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;

                        nextbtnimgon.BeginInit();
                        nextbtnimgon.UriSource = MainWindow.nextbtn.UriSource;
                        nextbtnimgon.CacheOption = BitmapCacheOption.OnLoad;
                        nextbtnimgon.EndInit();

                        Nextbtnimgoff.BeginInit();
                        Nextbtnimgoff.UriSource = new Uri(MainWindow.uipath + @"\Next_off.png", UriKind.RelativeOrAbsolute);
                        Nextbtnimgoff.CacheOption = BitmapCacheOption.OnLoad;
                        Nextbtnimgoff.EndInit();

                        NextImg.IsEnabled = false;

                        NextImg.Source = Nextbtnimgoff;
                    }
                }

                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    temp = 0;
                }
                else if (!MainWindow.inifoldername.Contains("mediagram"))
                {
                    temp = 1;
                }

                if (MainWindow.timerlocation == "left")
                {
                    Timer.Margin = new Thickness(0, 130, 1720, 0);
                }
                else
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        Timer.Margin = new Thickness(1720, 41, 0, 0);
                    }
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

        private void Temp1Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                temp = 1;

                if (MainWindow.SorR == "R")
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        BitmapImage temp1 = new BitmapImage();
                        temp1.BeginInit();
                        temp1.UriSource = new Uri(MainWindow.uipath + @"\Temp1_Pick.png", UriKind.RelativeOrAbsolute);
                        temp1.CacheOption = BitmapCacheOption.OnLoad;
                        temp1.DecodePixelWidth = 261;
                        temp1.DecodePixelHeight = 424;
                        temp1.EndInit();

                        BitmapImage temp2 = new BitmapImage();
                        temp2.BeginInit();
                        temp2.UriSource = new Uri(MainWindow.uipath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                        temp2.CacheOption = BitmapCacheOption.OnLoad;
                        temp2.DecodePixelWidth = 261;
                        temp2.DecodePixelHeight = 424;
                        temp2.EndInit();

                        BitmapImage temp3 = new BitmapImage();
                        temp3.BeginInit();
                        temp3.UriSource = new Uri(MainWindow.uipath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                        temp3.CacheOption = BitmapCacheOption.OnLoad;
                        temp3.DecodePixelWidth = 261;
                        temp3.DecodePixelHeight = 424;
                        temp3.EndInit();

                        BitmapImage temp4 = new BitmapImage();
                        temp4.BeginInit();
                        temp4.UriSource = new Uri(MainWindow.uipath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                        temp4.CacheOption = BitmapCacheOption.OnLoad;
                        temp4.DecodePixelWidth = 261;
                        temp4.DecodePixelHeight = 424;
                        temp4.EndInit();

                        Temp1Img.Source = temp1;
                        Temp2Img.Source = temp2;
                        Temp3Img.Source = temp3;
                        Temp4Img.Source = temp4;

                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                    }
                    else
                    {
                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                        temp1border.Opacity = 1;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                }
                else
                {
                    if (MainWindow.inifoldername == "dearpic2" || MainWindow.inifoldername == "animalhospital" || MainWindow.inifoldername == "r81")
                    {
                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;

                        Temp1Img.Source = MainWindow.temp1_pick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                    }
                    else if (MainWindow.inifoldername == "tech")
                    {
                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;

                        Temp1Img.Source = MainWindow.temp1_pick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                        Temp3Img.Source = MainWindow.temp3_nonpick;
                        Temp4Img.Source = temp4_nonpick;
                    }
                    else if (MainWindow.inifoldername == "r80")
                    {
                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                        temp1border.Opacity = 1;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "r30")
                    {
                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        temp1border.Opacity = 1;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "r24" || MainWindow.inifoldername == "r25")
                    {
                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                        temp1border.Opacity = 1;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "r28")
                    {
                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                        temp1border.Opacity = 1;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else
                    {
                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;

                        Temp1Img.Source = MainWindow.temp1_pick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                        Temp3Img.Source = MainWindow.temp3_nonpick;
                    }

                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        Temp1Img.IsEnabled = false;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = true;

                        Temp1Img.Source = MainWindow.temp1_pick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                        Temp3Img.Source = MainWindow.temp3_nonpick;

                        NextImg.Source = nextbtnimgon;

                        NextImg.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Temp2Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                temp = 2;

                if (MainWindow.SorR == "R")
                {
                    Temp1Img.IsEnabled = true;
                    Temp2Img.IsEnabled = false;
                    Temp3Img.IsEnabled = true;
                    Temp4Img.IsEnabled = true;
                    Temp5Img.IsEnabled = true;
                    Temp6Img.IsEnabled = true;
                    temp1border.Opacity = 0;
                    temp2border.Opacity = 1;
                    temp3border.Opacity = 0;
                    temp4border.Opacity = 0;
                    temp5border.Opacity = 0;
                    temp6border.Opacity = 0;
                }
                else
                {
                    if (MainWindow.inifoldername == "dearpic2" || MainWindow.inifoldername == "animalhospital" || MainWindow.inifoldername == "r81")
                    {
                        Temp1Img.Source = MainWindow.temp1_nonpick;
                        Temp2Img.Source = MainWindow.temp2_pick;

                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = false;
                    }
                    else if (MainWindow.inifoldername == "tech")
                    {
                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = false;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;

                        Temp1Img.Source = MainWindow.temp1_nonpick;
                        Temp2Img.Source = MainWindow.temp2_pick;
                        Temp3Img.Source = MainWindow.temp3_nonpick;
                        Temp4Img.Source = temp4_nonpick;
                    }
                    else if (MainWindow.inifoldername == "r80")
                    {
                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = false;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 1;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "r30")
                    {
                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = false;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 1;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "r24" || MainWindow.inifoldername == "r25")
                    {
                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = false;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 1;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "r28")
                    {
                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = false;
                        Temp3Img.IsEnabled = true;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 1;
                        temp3border.Opacity = 0;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else
                    {
                        Temp1Img.Source = MainWindow.temp1_nonpick;
                        Temp2Img.Source = MainWindow.temp2_pick;
                        Temp3Img.Source = MainWindow.temp3_nonpick;

                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = false;
                        Temp3Img.IsEnabled = true;
                    }

                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        Temp1Img.Source = MainWindow.temp1_nonpick;
                        Temp2Img.Source = MainWindow.temp2_pick;
                        Temp3Img.Source = MainWindow.temp3_nonpick;

                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = false;
                        Temp3Img.IsEnabled = true;

                        NextImg.Source = nextbtnimgon;

                        NextImg.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Temp3Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                temp = 3;

                if (MainWindow.SorR == "R")
                {
                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        BitmapImage temp1 = new BitmapImage();
                        temp1.BeginInit();
                        temp1.UriSource = new Uri(MainWindow.uipath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                        temp1.CacheOption = BitmapCacheOption.OnLoad;
                        temp1.DecodePixelWidth = 261;
                        temp1.DecodePixelHeight = 424;
                        temp1.EndInit();

                        BitmapImage temp2 = new BitmapImage();
                        temp2.BeginInit();
                        temp2.UriSource = new Uri(MainWindow.uipath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                        temp2.CacheOption = BitmapCacheOption.OnLoad;
                        temp2.DecodePixelWidth = 261;
                        temp2.DecodePixelHeight = 424;
                        temp2.EndInit();

                        BitmapImage temp3 = new BitmapImage();
                        temp3.BeginInit();
                        temp3.UriSource = new Uri(MainWindow.uipath + @"\Temp3_Pick.png", UriKind.RelativeOrAbsolute);
                        temp3.CacheOption = BitmapCacheOption.OnLoad;
                        temp3.DecodePixelWidth = 261;
                        temp3.DecodePixelHeight = 424;
                        temp3.EndInit();

                        BitmapImage temp4 = new BitmapImage();
                        temp4.BeginInit();
                        temp4.UriSource = new Uri(MainWindow.uipath + @"\Temp4.png", UriKind.RelativeOrAbsolute);
                        temp4.CacheOption = BitmapCacheOption.OnLoad;
                        temp4.DecodePixelWidth = 261;
                        temp4.DecodePixelHeight = 424;
                        temp4.EndInit();

                        Temp1Img.Source = temp1;
                        Temp2Img.Source = temp2;
                        Temp3Img.Source = temp3;
                        Temp4Img.Source = temp4;

                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                    }
                    else
                    {
                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 1;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                }
                else
                {
                    if (MainWindow.inifoldername == "tech")
                    {
                        Temp1Img.Source = MainWindow.temp1_nonpick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                        Temp3Img.Source = MainWindow.temp3_pick;
                        Temp4Img.Source = temp4_nonpick;

                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;
                        Temp4Img.IsEnabled = true;
                    }
                    else if (MainWindow.inifoldername == "r80")
                    {
                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 1;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "r30")
                    {
                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;
                        Temp4Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 1;
                        temp4border.Opacity = 0;
                    }
                    else if (MainWindow.inifoldername == "r28")
                    {
                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;
                        Temp4Img.IsEnabled = true;
                        Temp5Img.IsEnabled = true;
                        Temp6Img.IsEnabled = true;
                        temp1border.Opacity = 0;
                        temp2border.Opacity = 0;
                        temp3border.Opacity = 1;
                        temp4border.Opacity = 0;
                        temp5border.Opacity = 0;
                        temp6border.Opacity = 0;
                    }
                    else
                    {
                        Temp1Img.Source = MainWindow.temp1_nonpick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                        Temp3Img.Source = MainWindow.temp3_pick;

                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;
                    }

                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        Temp1Img.Source = MainWindow.temp1_nonpick;
                        Temp2Img.Source = MainWindow.temp2_nonpick;
                        Temp3Img.Source = MainWindow.temp3_pick;

                        Temp1Img.IsEnabled = true;
                        Temp2Img.IsEnabled = true;
                        Temp3Img.IsEnabled = false;

                        NextImg.Source = nextbtnimgon;

                        NextImg.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Temp4Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                temp = 4;

                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    BitmapImage temp1 = new BitmapImage();
                    temp1.BeginInit();
                    temp1.UriSource = new Uri(MainWindow.uipath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                    temp1.CacheOption = BitmapCacheOption.OnLoad;
                    temp1.DecodePixelWidth = 261;
                    temp1.DecodePixelHeight = 424;
                    temp1.EndInit();

                    BitmapImage temp2 = new BitmapImage();
                    temp2.BeginInit();
                    temp2.UriSource = new Uri(MainWindow.uipath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                    temp2.CacheOption = BitmapCacheOption.OnLoad;
                    temp2.DecodePixelWidth = 261;
                    temp2.DecodePixelHeight = 424;
                    temp2.EndInit();

                    BitmapImage temp3 = new BitmapImage();
                    temp3.BeginInit();
                    temp3.UriSource = new Uri(MainWindow.uipath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                    temp3.CacheOption = BitmapCacheOption.OnLoad;
                    temp3.DecodePixelWidth = 261;
                    temp3.DecodePixelHeight = 424;
                    temp3.EndInit();

                    BitmapImage temp4 = new BitmapImage();
                    temp4.BeginInit();
                    temp4.UriSource = new Uri(MainWindow.uipath + @"\Temp4_Pick.png", UriKind.RelativeOrAbsolute);
                    temp4.CacheOption = BitmapCacheOption.OnLoad;
                    temp4.DecodePixelWidth = 261;
                    temp4.DecodePixelHeight = 424;
                    temp4.EndInit();

                    Temp1Img.Source = temp1;
                    Temp2Img.Source = temp2;
                    Temp3Img.Source = temp3;
                    Temp4Img.Source = temp4;

                    Temp1Img.IsEnabled = true;
                    Temp2Img.IsEnabled = true;
                    Temp3Img.IsEnabled = true;
                    Temp4Img.IsEnabled = false;
                    Temp5Img.IsEnabled = true;
                    Temp6Img.IsEnabled = true;
                }
                else if (MainWindow.inifoldername.Contains("tech"))
                {
                    Temp1Img.Source = MainWindow.temp1_nonpick;
                    Temp2Img.Source = MainWindow.temp2_nonpick;
                    Temp3Img.Source = MainWindow.temp3_nonpick;
                    Temp4Img.Source = temp4_pick;

                    Temp1Img.IsEnabled = true;
                    Temp2Img.IsEnabled = true;
                    Temp3Img.IsEnabled = true;
                    Temp4Img.IsEnabled = false;
                }
                else
                {
                    Temp1Img.IsEnabled = true;
                    Temp2Img.IsEnabled = true;
                    Temp3Img.IsEnabled = true;
                    Temp4Img.IsEnabled = false;
                    Temp5Img.IsEnabled = true;
                    Temp6Img.IsEnabled = true;
                    temp1border.Opacity = 0;
                    temp2border.Opacity = 0;
                    temp3border.Opacity = 0;
                    temp4border.Opacity = 1;
                    temp5border.Opacity = 0;
                    temp6border.Opacity = 0;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Temp5Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                temp = 5;

                Temp1Img.IsEnabled = true;
                Temp2Img.IsEnabled = true;
                Temp3Img.IsEnabled = true;
                Temp4Img.IsEnabled = true;
                Temp5Img.IsEnabled = false;
                Temp6Img.IsEnabled = true;
                temp1border.Opacity = 0;
                temp2border.Opacity = 0;
                temp3border.Opacity = 0;
                temp4border.Opacity = 0;
                temp5border.Opacity = 1;
                temp6border.Opacity = 0;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Temp6Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                temp = 6;

                Temp1Img.IsEnabled = true;
                Temp2Img.IsEnabled = true;
                Temp3Img.IsEnabled = true;
                Temp4Img.IsEnabled = true;
                Temp5Img.IsEnabled = true;
                Temp6Img.IsEnabled = false;
                temp1border.Opacity = 0;
                temp2border.Opacity = 0;
                temp3border.Opacity = 0;
                temp4border.Opacity = 0;
                temp5border.Opacity = 0;
                temp6border.Opacity = 1;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void NextImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                NextImg.IsEnabled = false;

                timer.Stop();
                Dispose();
                if (MainWindow.pagenum == "1")
                {
                    if (MainWindow.checkfreeornot == "0")
                    {
                        switch (MainWindow.paymentini.ToString())
                        {
                            case "cash":
                                NavigationService.Navigate(new Uri("View/Payment.xaml", UriKind.RelativeOrAbsolute));
                                break;
                            case "card":
                                NavigationService.Navigate(new Uri("View/CreditPayment.xaml", UriKind.RelativeOrAbsolute));
                                break;
                            case "cardcash":
                            case "cashcoupon":
                            case "cardcoupon":
                                NavigationService.Navigate(new Uri("View/ChoicePayment.xaml", UriKind.RelativeOrAbsolute));
                                break;
                            case "coupon":
                                NavigationService.Navigate(new Uri("View/CouponPage.xaml", UriKind.RelativeOrAbsolute));
                                break;
                        }
                    }
                    else
                    {
                        if (MainWindow.camnumber.ToString() == "1")
                        {
                            NavigationService.Navigate(new Uri("View/TakePic.xaml", UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            NavigationService.Navigate(new Uri("View/WebCam.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                }
                else
                {
                    if (MainWindow.inifoldername == "dearpic2" || MainWindow.inifoldername == "harim")
                    {
                        NavigationService.Navigate(new Uri("View/AISelect.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        NavigationService.Navigate(new Uri("View/PageSelect.xaml", UriKind.RelativeOrAbsolute));
                    }
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

                temp1img = null;
                temp2img = null;
                temp3img = null;
                temp4img = null;
                temp5img = null;
                temp6img = null;
                Temp1Img.Source = null;
                Temp2Img.Source = null;
                Temp3Img.Source = null;
                Temp4Img.Source = null;
                Temp5Img.Source = null;
                Temp6Img.Source = null;
                Temp1Img = null;
                temp2img = null;
                temp3img = null;
                temp4img = null;
                temp5img = null;
                temp6img = null;
                backgrondimg.Source = null;
                backgrondimg = null;
                BackBtn.Source = null;
                BackBtn = null;

                temp4_nonpick = null;
                temp4_pick = null;

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
                Temp1Img.IsEnabled = false;
                Temp2Img.IsEnabled = false;
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

        private void BackBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                BackBtn.Source = null;

                timer.Stop();
                Dispose();
                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    if (SelectTakePic.checkversion == 2)
                    {
                        NavigationService.Navigate(new Uri("View/SelectPlayer.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        if (MainWindow.checkingtogether == "Use")
                        {
                            NavigationService.Navigate(new Uri("View/SelectTakePic.xaml", UriKind.RelativeOrAbsolute));
                        }
                        else if (MainWindow.checkingtogether != "Use")
                        {
                            NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                }
                else
                {
                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
