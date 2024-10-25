using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Reflection;
using OpenCvSharp;
using EOSDigital.SDK;
using System.CodeDom;

namespace wpfTest.View
{
    /// <summary>
    /// WebCam.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WebCam : Page
    {

        public static int photonum = 0;
        public static string photonumber;
        int count;
        int smallimg;
        DispatcherTimer timer = new DispatcherTimer();
        Thread videothread;
        bool checkvideothread;
        bool phototime = false;

        Mat frame = new Mat();
        Mat flipframe = new Mat();
        Bitmap bitmap;
        Bitmap savebitmap;

        //비디오 녹화 변수

        Thread recordthread;
        int recordstart = 0;
        private static bool isRecording = false;
        public static string videoname;
        VideoWriter writer;
        Mat croppedimage;
        Mat image;

        int startx;
        int starty;
        int width;
        int height;

        public WebCam()
        {
            Source.Log.log.Info("웹캠 촬영 페이지 진입");
            InitializeComponent();
        }

        #region /// 페이지 로드 ///

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                backimg.Source = MainWindow.liveview;

                compose1.Source = MainWindow.composeimg;
                compose2.Source = MainWindow.composeimg2;

                tblState.Foreground = MainWindow.textbrush;
                count = Convert.ToInt32(MainWindow.photocount);
                tblState.Text = Convert.ToString(count);

                photonum = 0;

                checkvideothread = true;

                videothread = new Thread(new ThreadStart(webcamthread));
                videothread.Start();
                Delay(2000);

                if (MainWindow.SorR == "R")
                {
                    switch (MainWindow.picratio)
                    {
                        case 1:
                            if (MainWindow.tempoption == 1)
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        BitmapImage one = new BitmapImage();
                                        one.BeginInit();
                                        one.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                        one.CacheOption = BitmapCacheOption.OnLoad;
                                        one.DecodePixelWidth = 1080;
                                        one.DecodePixelHeight = 720;
                                        one.EndInit();
                                        frontlvimg.Source = one;
                                        break;
                                    case 2:
                                        BitmapImage two = new BitmapImage();
                                        two.BeginInit();
                                        two.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                        two.CacheOption = BitmapCacheOption.OnLoad;
                                        two.DecodePixelWidth = 1080;
                                        two.DecodePixelHeight = 720;
                                        two.EndInit();
                                        frontlvimg.Source = two;
                                        break;
                                    case 3:
                                        BitmapImage three = new BitmapImage();
                                        three.BeginInit();
                                        three.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                        three.CacheOption = BitmapCacheOption.OnLoad;
                                        three.DecodePixelWidth = 1080;
                                        three.DecodePixelHeight = 720;
                                        three.EndInit();
                                        frontlvimg.Source = three;
                                        break;
                                    case 4:
                                        BitmapImage four = new BitmapImage();
                                        four.BeginInit();
                                        four.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                        four.CacheOption = BitmapCacheOption.OnLoad;
                                        four.DecodePixelWidth = 1080;
                                        four.DecodePixelHeight = 720;
                                        four.EndInit();
                                        frontlvimg.Source = four;
                                        break;
                                    case 5:
                                        BitmapImage five = new BitmapImage();
                                        five.BeginInit();
                                        five.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                        five.CacheOption = BitmapCacheOption.OnLoad;
                                        five.DecodePixelWidth = 1080;
                                        five.DecodePixelHeight = 720;
                                        five.EndInit();
                                        frontlvimg.Source = five;
                                        break;
                                    case 6:
                                        BitmapImage six = new BitmapImage();
                                        six.BeginInit();
                                        six.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                        six.CacheOption = BitmapCacheOption.OnLoad;
                                        six.DecodePixelWidth = 1080;
                                        six.DecodePixelHeight = 720;
                                        six.EndInit();
                                        frontlvimg.Source = six;
                                        break;
                                }
                            }
                            else
                            {
                                BitmapImage one = new BitmapImage();
                                one.BeginInit();
                                one.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                one.CacheOption = BitmapCacheOption.OnLoad;
                                one.DecodePixelWidth = 1080;
                                one.DecodePixelHeight = 720;
                                one.EndInit();
                                frontlvimg.Source = one;
                            }

                            if (MainWindow.checkvideo == "1")
                            {
                                startx = 420;
                                starty = 180;
                                width = 1080;
                                height = 720;
                            }
                            break;
                        case 2:
                            compose1.Width = 280;
                            compose2.Width = 280;

                            Canvas.SetLeft(compose2, 1320);

                            frontlvimg.Width = 720;
                            Canvas.SetLeft(frontlvimg, 600);

                            compose1.Opacity = 1;
                            compose2.Opacity = 1;

                            if (MainWindow.checkvideo == "1")
                            {
                                startx = 600;
                                starty = 180;
                                width = 720;
                                height = 720;
                            }

                            FirstPic.Width = 170;
                            SecondPic.Width = 170;
                            ThirdPic.Width = 170;
                            FourthPic.Width = 170;
                            FirstPicFront.Width = 170;
                            SecondPicFront.Width = 170;
                            ThirdPicFront.Width = 170;
                            FourthPicFront.Width = 170;

                            FirstPic.Margin = new Thickness(1340, 180, 410, 735);
                            SecondPic.Margin = new Thickness(1340, 365, 410, 550);
                            ThirdPic.Margin = new Thickness(1340, 550, 410, 365);
                            FourthPic.Margin = new Thickness(1340, 735, 410, 180);
                            FirstPicFront.Margin = new Thickness(1340, 180, 410, 735);
                            SecondPicFront.Margin = new Thickness(1340, 365, 410, 550);
                            ThirdPicFront.Margin = new Thickness(1340, 550, 410, 365);
                            FourthPicFront.Margin = new Thickness(1340, 735, 410, 180);

                            if (MainWindow.tempoption == 1)
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        BitmapImage one = new BitmapImage();
                                        one.BeginInit();
                                        one.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                        one.CacheOption = BitmapCacheOption.OnLoad;
                                        one.DecodePixelWidth = 1080;
                                        one.DecodePixelHeight = 1080;
                                        one.EndInit();
                                        frontlvimg.Source = one;
                                        break;
                                    case 2:
                                        BitmapImage two = new BitmapImage();
                                        two.BeginInit();
                                        two.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                        two.CacheOption = BitmapCacheOption.OnLoad;
                                        two.DecodePixelWidth = 1080;
                                        two.DecodePixelHeight = 1080;
                                        two.EndInit();
                                        frontlvimg.Source = two;
                                        break;
                                    case 3:
                                        BitmapImage three = new BitmapImage();
                                        three.BeginInit();
                                        three.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                        three.CacheOption = BitmapCacheOption.OnLoad;
                                        three.DecodePixelWidth = 1080;
                                        three.DecodePixelHeight = 1080;
                                        three.EndInit();
                                        frontlvimg.Source = three;
                                        break;
                                    case 4:
                                        BitmapImage four = new BitmapImage();
                                        four.BeginInit();
                                        four.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                        four.CacheOption = BitmapCacheOption.OnLoad;
                                        four.DecodePixelWidth = 1080;
                                        four.DecodePixelHeight = 1080;
                                        four.EndInit();
                                        frontlvimg.Source = four;
                                        break;
                                    case 5:
                                        BitmapImage five = new BitmapImage();
                                        five.BeginInit();
                                        five.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                        five.CacheOption = BitmapCacheOption.OnLoad;
                                        five.DecodePixelWidth = 1080;
                                        five.DecodePixelHeight = 1080;
                                        five.EndInit();
                                        frontlvimg.Source = five;
                                        break;
                                    case 6:
                                        BitmapImage six = new BitmapImage();
                                        six.BeginInit();
                                        six.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                        six.CacheOption = BitmapCacheOption.OnLoad;
                                        six.DecodePixelWidth = 1080;
                                        six.DecodePixelHeight = 1080;
                                        six.EndInit();
                                        frontlvimg.Source = six;
                                        break;
                                }
                            }
                            else
                            {
                                BitmapImage one = new BitmapImage();
                                one.BeginInit();
                                one.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                one.CacheOption = BitmapCacheOption.OnLoad;
                                one.DecodePixelWidth = 1080;
                                one.DecodePixelHeight = 1080;
                                one.EndInit();
                                frontlvimg.Source = one;
                            }
                            break;
                        case 3:

                            compose1.Width = 400;
                            compose2.Width = 400;

                            compose1.Opacity = 1;
                            compose2.Opacity = 1;

                            Canvas.SetLeft(compose2, 1200);
                            frontlvimg.Width = 480;
                            Canvas.SetLeft(frontlvimg, 720);

                            if (MainWindow.checkvideo == "1")
                            {
                                startx = 720;
                                starty = 180;
                                width = 480;
                                height = 720;
                            }

                            FirstPic.Width = 243;
                            FirstPic.Height = 365;
                            SecondPic.Width = 243;
                            SecondPic.Height = 365;
                            ThirdPic.Width = 243;
                            ThirdPic.Height = 365;
                            FourthPic.Width = 243;
                            FourthPic.Height = 365;

                            FirstPicFront.Width = 243;
                            FirstPicFront.Height = 365;
                            SecondPicFront.Width = 243;
                            SecondPicFront.Height = 365;
                            ThirdPicFront.Width = 243;
                            ThirdPicFront.Height = 365;
                            FourthPicFront.Width = 243;
                            FourthPicFront.Height = 365;

                            FirstPic.Margin = new Thickness(1220, 180, 457, 535);
                            SecondPic.Margin = new Thickness(1483, 180, 194, 535);
                            ThirdPic.Margin = new Thickness(1220, 565, 457, 180);
                            FourthPic.Margin = new Thickness(1483, 565, 194, 180);
                            FirstPicFront.Margin = new Thickness(1220, 180, 457, 535);
                            SecondPicFront.Margin = new Thickness(1483, 180, 194, 535);
                            ThirdPicFront.Margin = new Thickness(1220, 565, 457, 180);
                            FourthPicFront.Margin = new Thickness(1483, 565, 194, 180);

                            if (MainWindow.tempoption == 1)
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        BitmapImage one = new BitmapImage();
                                        one.BeginInit();
                                        one.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                        one.CacheOption = BitmapCacheOption.OnLoad;
                                        one.DecodePixelWidth = 720;
                                        one.DecodePixelHeight = 1080;
                                        one.EndInit();
                                        frontlvimg.Source = one;
                                        break;
                                    case 2:
                                        BitmapImage two = new BitmapImage();
                                        two.BeginInit();
                                        two.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                        two.CacheOption = BitmapCacheOption.OnLoad;
                                        two.DecodePixelWidth = 720;
                                        two.DecodePixelHeight = 1080;
                                        two.EndInit();
                                        frontlvimg.Source = two;
                                        break;
                                    case 3:
                                        BitmapImage three = new BitmapImage();
                                        three.BeginInit();
                                        three.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                        three.CacheOption = BitmapCacheOption.OnLoad;
                                        three.DecodePixelWidth = 720;
                                        three.DecodePixelHeight = 1080;
                                        three.EndInit();
                                        frontlvimg.Source = three;
                                        break;
                                    case 4:
                                        BitmapImage four = new BitmapImage();
                                        four.BeginInit();
                                        four.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                        four.CacheOption = BitmapCacheOption.OnLoad;
                                        four.DecodePixelWidth = 720;
                                        four.DecodePixelHeight = 1080;
                                        four.EndInit();
                                        frontlvimg.Source = four;
                                        break;
                                    case 5:
                                        BitmapImage five = new BitmapImage();
                                        five.BeginInit();
                                        five.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                        five.CacheOption = BitmapCacheOption.OnLoad;
                                        five.DecodePixelWidth = 720;
                                        five.DecodePixelHeight = 1080;
                                        five.EndInit();
                                        frontlvimg.Source = five;
                                        break;
                                    case 6:
                                        BitmapImage six = new BitmapImage();
                                        six.BeginInit();
                                        six.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                        six.CacheOption = BitmapCacheOption.OnLoad;
                                        six.DecodePixelWidth = 720;
                                        six.DecodePixelHeight = 1080;
                                        six.EndInit();
                                        frontlvimg.Source = six;
                                        break;
                                }
                            }
                            else
                            {
                                BitmapImage one = new BitmapImage();
                                one.BeginInit();
                                one.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                one.CacheOption = BitmapCacheOption.OnLoad;
                                one.DecodePixelWidth = 720;
                                one.DecodePixelHeight = 1080;
                                one.EndInit();
                                frontlvimg.Source = one;
                            }
                            break;
                    }

                    switch (MainWindow.photonum)
                    {
                        case 1:
                            FourthPic.Margin = FirstPic.Margin;
                            FourthPicFront.Margin = FirstPicFront.Margin;
                            break;
                        case 2:
                            FourthPic.Margin = SecondPic.Margin;
                            FourthPicFront.Margin = SecondPicFront.Margin;
                            break;
                        case 3:
                            FourthPic.Margin = ThirdPic.Margin;
                            FourthPicFront.Margin = ThirdPicFront.Margin;
                            break;
                        case 5:
                            switch (MainWindow.picratio)
                            {
                                case 1:
                                    FirstPic.Width = 165;
                                    FirstPic.Height = 110;
                                    SecondPic.Width = 165;
                                    SecondPic.Height = 110;
                                    ThirdPic.Width = 165;
                                    ThirdPic.Height = 110;
                                    FourthPic.Width = 165;
                                    FourthPic.Height = 110;
                                    FifthPic.Width = 165;
                                    SixthPic.Height = 110;

                                    FirstPicFront.Width = 165;
                                    FirstPicFront.Height = 110;
                                    SecondPicFront.Width = 165;
                                    SecondPicFront.Height = 110;
                                    ThirdPicFront.Width = 165;
                                    ThirdPicFront.Height = 110;
                                    FourthPicFront.Width = 165;
                                    FourthPicFront.Height = 110;
                                    FifthPicFront.Width = 165;
                                    FifthPicFront.Height = 110;
                                    SixthPicFront.Width = 165;
                                    SixthPicFront.Height = 110;

                                    FirstPic.Margin = new Thickness(1520, 180, 235, 790);
                                    SecondPic.Margin = new Thickness(1520, 302, 235, 668);
                                    ThirdPic.Margin = new Thickness(1520, 424, 235, 546);
                                    FourthPic.Margin = new Thickness(1520, 546, 235, 424);
                                    FifthPic.Margin = new Thickness(1520, 668, 235, 302);
                                    SixthPic.Margin = new Thickness(1520, 790, 235, 180);

                                    FirstPicFront.Margin = FirstPic.Margin;
                                    SecondPicFront.Margin = SecondPic.Margin;
                                    ThirdPicFront.Margin = ThirdPic.Margin;
                                    FourthPicFront.Margin = FourthPic.Margin;
                                    FifthPicFront.Margin = FifthPic.Margin;
                                    SixthPicFront.Margin = SixthPic.Margin;

                                    if (MainWindow.checkvideo == "1")
                                    {
                                        startx = 420;
                                        starty = 180;
                                        width = 1080;
                                        height = 720;
                                    }
                                    break;
                                case 2:
                                    compose1.Width = 280;
                                    compose2.Width = 280;
                                    Canvas.SetLeft(compose2, 1320);

                                    frontlvimg.Width = 720;
                                    Canvas.SetLeft(frontlvimg, 600);

                                    FirstPic.Width = 110;
                                    FirstPic.Height = 110;
                                    SecondPic.Width = 110;
                                    SecondPic.Height = 110;
                                    ThirdPic.Width = 110;
                                    ThirdPic.Height = 110;
                                    FourthPic.Width = 110;
                                    FourthPic.Height = 110;
                                    FifthPic.Width = 110;
                                    SixthPic.Height = 110;

                                    FirstPicFront.Width = 110;
                                    FirstPicFront.Height = 110;
                                    SecondPicFront.Width = 110;
                                    SecondPicFront.Height = 110;
                                    ThirdPicFront.Width = 110;
                                    ThirdPicFront.Height = 110;
                                    FourthPicFront.Width = 110;
                                    FourthPicFront.Height = 110;
                                    FifthPicFront.Width = 110;
                                    FifthPicFront.Height = 110;
                                    SixthPicFront.Width = 110;
                                    SixthPicFront.Height = 110;

                                    FirstPic.Margin = new Thickness(1520, 180, 290, 790);
                                    SecondPic.Margin = new Thickness(1520, 302, 290, 668);
                                    ThirdPic.Margin = new Thickness(1520, 424, 290, 546);
                                    FourthPic.Margin = new Thickness(1520, 546, 290, 424);
                                    FifthPic.Margin = new Thickness(1520, 668, 290, 302);
                                    SixthPic.Margin = new Thickness(1520, 790, 290, 180);

                                    FirstPicFront.Margin = FirstPic.Margin;
                                    SecondPicFront.Margin = SecondPic.Margin;
                                    ThirdPicFront.Margin = ThirdPic.Margin;
                                    FourthPicFront.Margin = FourthPic.Margin;
                                    FifthPicFront.Margin = FifthPic.Margin;
                                    SixthPicFront.Margin = SixthPic.Margin;

                                    if (MainWindow.checkvideo == "1")
                                    {
                                        startx = 600;
                                        starty = 180;
                                        width = 720;
                                        height = 720;
                                    }
                                    break;
                                case 3:

                                    compose1.Width = 400;
                                    compose2.Width = 400;

                                    compose1.Opacity = 1;
                                    compose2.Opacity = 1;

                                    Canvas.SetLeft(compose2, 1200);
                                    frontlvimg.Width = 480;
                                    Canvas.SetLeft(frontlvimg, 720);

                                    if (MainWindow.checkvideo == "1")
                                    {
                                        startx = 720;
                                        starty = 180;
                                        width = 480;
                                        height = 720;
                                    }
                                    FirstPic.Width = 73;
                                    FirstPic.Height = 110;
                                    SecondPic.Width = 73;
                                    SecondPic.Height = 110;
                                    ThirdPic.Width = 73;
                                    ThirdPic.Height = 110;
                                    FourthPic.Width = 73;
                                    FourthPic.Height = 110;
                                    FifthPic.Width = 73;
                                    SixthPic.Height = 110;

                                    FirstPicFront.Width = 73;
                                    FirstPicFront.Height = 110;
                                    SecondPicFront.Width = 73;
                                    SecondPicFront.Height = 110;
                                    ThirdPicFront.Width = 73;
                                    ThirdPicFront.Height = 110;
                                    FourthPicFront.Width = 73;
                                    FourthPicFront.Height = 110;
                                    FifthPicFront.Width = 73;
                                    FifthPicFront.Height = 110;
                                    SixthPicFront.Width = 73;
                                    SixthPicFront.Height = 110;

                                    FirstPic.Margin = new Thickness(1520, 180, 235, 790);
                                    SecondPic.Margin = new Thickness(1520, 302, 235, 668);
                                    ThirdPic.Margin = new Thickness(1520, 424, 235, 546);
                                    FourthPic.Margin = new Thickness(1520, 546, 235, 424);
                                    FifthPic.Margin = new Thickness(1520, 668, 235, 302);
                                    SixthPic.Margin = new Thickness(1520, 790, 235, 180);

                                    FirstPicFront.Margin = FirstPic.Margin;
                                    SecondPicFront.Margin = SecondPic.Margin;
                                    ThirdPicFront.Margin = ThirdPic.Margin;
                                    FourthPicFront.Margin = FourthPic.Margin;
                                    FifthPicFront.Margin = FifthPic.Margin;
                                    SixthPicFront.Margin = SixthPic.Margin;

                                    if (MainWindow.checkvideo == "1")
                                    {
                                        startx = 420;
                                        starty = 180;
                                        width = 1080;
                                        height = 720;
                                    }
                                    break;
                            }
                            break;
                        case 6:
                            switch (MainWindow.picratio)
                            {
                                case 1:
                                    FirstPic.Width = 165;
                                    FirstPic.Height = 110;
                                    SecondPic.Width = 165;
                                    SecondPic.Height = 110;
                                    ThirdPic.Width = 165;
                                    ThirdPic.Height = 110;
                                    FourthPic.Width = 165;
                                    FourthPic.Height = 110;
                                    FifthPic.Width = 165;
                                    SixthPic.Height = 110;

                                    FirstPicFront.Width = 165;
                                    FirstPicFront.Height = 110;
                                    SecondPicFront.Width = 165;
                                    SecondPicFront.Height = 110;
                                    ThirdPicFront.Width = 165;
                                    ThirdPicFront.Height = 110;
                                    FourthPicFront.Width = 165;
                                    FourthPicFront.Height = 110;
                                    FifthPicFront.Width = 165;
                                    FifthPicFront.Height = 110;
                                    SixthPicFront.Width = 165;
                                    SixthPicFront.Height = 110;

                                    FirstPic.Margin = new Thickness(1520, 180, 235, 790);
                                    SecondPic.Margin = new Thickness(1520, 302, 235, 668);
                                    ThirdPic.Margin = new Thickness(1520, 424, 235, 546);
                                    FourthPic.Margin = new Thickness(1520, 546, 235, 424);
                                    FifthPic.Margin = new Thickness(1520, 668, 235, 302);
                                    SixthPic.Margin = new Thickness(1520, 790, 235, 180);

                                    FirstPicFront.Margin = FirstPic.Margin;
                                    SecondPicFront.Margin = SecondPic.Margin;
                                    ThirdPicFront.Margin = ThirdPic.Margin;
                                    FourthPicFront.Margin = FourthPic.Margin;
                                    FifthPicFront.Margin = FifthPic.Margin;
                                    SixthPicFront.Margin = SixthPic.Margin;

                                    if (MainWindow.checkvideo == "1")
                                    {
                                        startx = 420;
                                        starty = 180;
                                        width = 1080;
                                        height = 720;
                                    }
                                    break;
                                case 2:
                                    compose1.Width = 280;
                                    compose2.Width = 280;
                                    Canvas.SetLeft(compose2, 1320);

                                    frontlvimg.Width = 720;
                                    Canvas.SetLeft(frontlvimg, 600);

                                    FirstPic.Width = 110;
                                    FirstPic.Height = 110;
                                    SecondPic.Width = 110;
                                    SecondPic.Height = 110;
                                    ThirdPic.Width = 110;
                                    ThirdPic.Height = 110;
                                    FourthPic.Width = 110;
                                    FourthPic.Height = 110;
                                    FifthPic.Width = 110;
                                    SixthPic.Height = 110;

                                    FirstPicFront.Width = 110;
                                    FirstPicFront.Height = 110;
                                    SecondPicFront.Width = 110;
                                    SecondPicFront.Height = 110;
                                    ThirdPicFront.Width = 110;
                                    ThirdPicFront.Height = 110;
                                    FourthPicFront.Width = 110;
                                    FourthPicFront.Height = 110;
                                    FifthPicFront.Width = 110;
                                    FifthPicFront.Height = 110;
                                    SixthPicFront.Width = 110;
                                    SixthPicFront.Height = 110;

                                    FirstPic.Margin = new Thickness(1520, 180, 290, 790);
                                    SecondPic.Margin = new Thickness(1520, 302, 290, 668);
                                    ThirdPic.Margin = new Thickness(1520, 424, 290, 546);
                                    FourthPic.Margin = new Thickness(1520, 546, 290, 424);
                                    FifthPic.Margin = new Thickness(1520, 668, 290, 302);
                                    SixthPic.Margin = new Thickness(1520, 790, 290, 180);

                                    FirstPicFront.Margin = FirstPic.Margin;
                                    SecondPicFront.Margin = SecondPic.Margin;
                                    ThirdPicFront.Margin = ThirdPic.Margin;
                                    FourthPicFront.Margin = FourthPic.Margin;
                                    FifthPicFront.Margin = FifthPic.Margin;
                                    SixthPicFront.Margin = SixthPic.Margin;

                                    if (MainWindow.checkvideo == "1")
                                    {
                                        startx = 600;
                                        starty = 180;
                                        width = 720;
                                        height = 720;
                                    }
                                    break;
                                case 3:

                                    compose1.Width = 400;
                                    compose2.Width = 400;

                                    compose1.Opacity = 1;
                                    compose2.Opacity = 1;

                                    Canvas.SetLeft(compose2, 1200);
                                    frontlvimg.Width = 480;
                                    Canvas.SetLeft(frontlvimg, 720);

                                    if (MainWindow.checkvideo == "1")
                                    {
                                        startx = 720;
                                        starty = 180;
                                        width = 480;
                                        height = 720;
                                    }
                                    FirstPic.Width = 73;
                                    FirstPic.Height = 110;
                                    SecondPic.Width = 73;
                                    SecondPic.Height = 110;
                                    ThirdPic.Width = 73;
                                    ThirdPic.Height = 110;
                                    FourthPic.Width = 73;
                                    FourthPic.Height = 110;
                                    FifthPic.Width = 73;
                                    SixthPic.Height = 110;

                                    FirstPicFront.Width = 73;
                                    FirstPicFront.Height = 73;
                                    SecondPicFront.Width = 165;
                                    SecondPicFront.Height = 110;
                                    ThirdPicFront.Width = 73;
                                    ThirdPicFront.Height = 110;
                                    FourthPicFront.Width = 73;
                                    FourthPicFront.Height = 110;
                                    FifthPicFront.Width = 73;
                                    FifthPicFront.Height = 110;
                                    SixthPicFront.Width = 73;
                                    SixthPicFront.Height = 110;

                                    FirstPic.Margin = new Thickness(1520, 180, 235, 790);
                                    SecondPic.Margin = new Thickness(1520, 302, 235, 668);
                                    ThirdPic.Margin = new Thickness(1520, 424, 235, 546);
                                    FourthPic.Margin = new Thickness(1520, 546, 235, 424);
                                    FifthPic.Margin = new Thickness(1520, 668, 235, 302);
                                    SixthPic.Margin = new Thickness(1520, 790, 235, 180);

                                    FirstPicFront.Margin = FirstPic.Margin;
                                    SecondPicFront.Margin = SecondPic.Margin;
                                    ThirdPicFront.Margin = ThirdPic.Margin;
                                    FourthPicFront.Margin = FourthPic.Margin;
                                    FifthPicFront.Margin = FifthPic.Margin;
                                    SixthPicFront.Margin = SixthPic.Margin;

                                    if (MainWindow.checkvideo == "1")
                                    {
                                        startx = 420;
                                        starty = 180;
                                        width = 1080;
                                        height = 720;
                                    }
                                    break;
                            }
                            break;
                    }
                }
                else if (MainWindow.SorR == "S")
                {
                    if (MainWindow.inifoldername == "r80")
                    {
                        MainWindow.picratio = 1;


                        switch (TempSelect.temp)
                        {
                            case 1:
                                MainWindow.photonum = 4;
                                FirstPicFront.Margin = FirstPic.Margin;
                                SecondPicFront.Margin = SecondPic.Margin;
                                ThirdPicFront.Margin = ThirdPic.Margin;
                                FourthPicFront.Margin = FourthPic.Margin;
                                break;
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                MainWindow.photonum = 6;

                                FirstPic.Width = 165;
                                FirstPic.Height = 110;
                                SecondPic.Width = 165;
                                SecondPic.Height = 110;
                                ThirdPic.Width = 165;
                                ThirdPic.Height = 110;
                                FourthPic.Width = 165;
                                FourthPic.Height = 110;
                                FifthPic.Width = 165;
                                SixthPic.Height = 110;

                                FirstPicFront.Width = 165;
                                FirstPicFront.Height = 110;
                                SecondPicFront.Width = 165;
                                SecondPicFront.Height = 110;
                                ThirdPicFront.Width = 165;
                                ThirdPicFront.Height = 110;
                                FourthPicFront.Width = 165;
                                FourthPicFront.Height = 110;
                                FifthPicFront.Width = 165;
                                FifthPicFront.Height = 110;
                                SixthPicFront.Width = 165;
                                SixthPicFront.Height = 110;

                                FirstPic.Margin = new Thickness(1520, 180, 235, 790);
                                SecondPic.Margin = new Thickness(1520, 302, 235, 668);
                                ThirdPic.Margin = new Thickness(1520, 424, 235, 546);
                                FourthPic.Margin = new Thickness(1520, 546, 235, 424);
                                FifthPic.Margin = new Thickness(1520, 668, 235, 302);
                                SixthPic.Margin = new Thickness(1520, 790, 235, 180);

                                FirstPicFront.Margin = FirstPic.Margin;
                                SecondPicFront.Margin = SecondPic.Margin;
                                ThirdPicFront.Margin = ThirdPic.Margin;
                                FourthPicFront.Margin = FourthPic.Margin;
                                FifthPicFront.Margin = FifthPic.Margin;
                                SixthPicFront.Margin = SixthPic.Margin;
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "r81" || MainWindow.inifoldername == "r83")
                    {
                        MainWindow.picratio = 1;
                        MainWindow.photonum = 6;

                        FirstPic.Width = 165;
                        FirstPic.Height = 110;
                        SecondPic.Width = 165;
                        SecondPic.Height = 110;
                        ThirdPic.Width = 165;
                        ThirdPic.Height = 110;
                        FourthPic.Width = 165;
                        FourthPic.Height = 110;
                        FifthPic.Width = 165;
                        SixthPic.Height = 110;

                        FirstPicFront.Width = 165;
                        FirstPicFront.Height = 110;
                        SecondPicFront.Width = 165;
                        SecondPicFront.Height = 110;
                        ThirdPicFront.Width = 165;
                        ThirdPicFront.Height = 110;
                        FourthPicFront.Width = 165;
                        FourthPicFront.Height = 110;
                        FifthPicFront.Width = 165;
                        FifthPicFront.Height = 110;
                        SixthPicFront.Width = 165;
                        SixthPicFront.Height = 110;

                        FirstPic.Margin = new Thickness(1520, 180, 235, 790);
                        SecondPic.Margin = new Thickness(1520, 302, 235, 668);
                        ThirdPic.Margin = new Thickness(1520, 424, 235, 546);
                        FourthPic.Margin = new Thickness(1520, 546, 235, 424);
                        FifthPic.Margin = new Thickness(1520, 668, 235, 302);
                        SixthPic.Margin = new Thickness(1520, 790, 235, 180);

                        FirstPicFront.Margin = FirstPic.Margin;
                        SecondPicFront.Margin = SecondPic.Margin;
                        ThirdPicFront.Margin = ThirdPic.Margin;
                        FourthPicFront.Margin = FourthPic.Margin;
                        FifthPicFront.Margin = FifthPic.Margin;
                        SixthPicFront.Margin = SixthPic.Margin;

                        if (MainWindow.checkvideo == "1")
                        {
                            startx = 420;
                            starty = 180;
                            width = 1080;
                            height = 720;
                        }
                    }
                    else if (MainWindow.inifoldername == "r30")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                MainWindow.picratio = 1;
                                MainWindow.photonum = 3;
                                break;
                            case 2:
                            case 3:
                            case 4:
                                MainWindow.picratio = 1;
                                MainWindow.photonum = 4;
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "r28")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                            case 3:
                            case 4:
                                compose1.Width = 280;
                                compose2.Width = 280;
                                Canvas.SetLeft(compose2, 1320);

                                frontlvimg.Width = 720;
                                Canvas.SetLeft(frontlvimg, 600);

                                FirstPic.Width = 110;
                                FirstPic.Height = 110;
                                SecondPic.Width = 110;
                                SecondPic.Height = 110;
                                ThirdPic.Width = 110;
                                ThirdPic.Height = 110;
                                FourthPic.Width = 110;
                                FourthPic.Height = 110;
                                FifthPic.Width = 110;
                                SixthPic.Height = 110;

                                FirstPicFront.Width = 110;
                                FirstPicFront.Height = 110;
                                SecondPicFront.Width = 110;
                                SecondPicFront.Height = 110;
                                ThirdPicFront.Width = 110;
                                ThirdPicFront.Height = 110;
                                FourthPicFront.Width = 110;
                                FourthPicFront.Height = 110;
                                FifthPicFront.Width = 110;
                                FifthPicFront.Height = 110;
                                SixthPicFront.Width = 110;
                                SixthPicFront.Height = 110;

                                FirstPic.Margin = new Thickness(1520, 180, 290, 790);
                                SecondPic.Margin = new Thickness(1520, 302, 290, 668);
                                ThirdPic.Margin = new Thickness(1520, 424, 290, 546);
                                FourthPic.Margin = new Thickness(1520, 546, 290, 424);
                                FifthPic.Margin = new Thickness(1520, 668, 290, 302);
                                SixthPic.Margin = new Thickness(1520, 790, 290, 180);

                                FirstPicFront.Margin = FirstPic.Margin;
                                SecondPicFront.Margin = SecondPic.Margin;
                                ThirdPicFront.Margin = ThirdPic.Margin;
                                FourthPicFront.Margin = FourthPic.Margin;
                                FifthPicFront.Margin = FifthPic.Margin;
                                SixthPicFront.Margin = SixthPic.Margin;

                                if (MainWindow.checkvideo == "1")
                                {
                                    startx = 600;
                                    starty = 180;
                                    width = 720;
                                    height = 720;
                                }
                                break;
                            case 2:
                                FirstPic.Width = 165;
                                FirstPic.Height = 110;
                                SecondPic.Width = 165;
                                SecondPic.Height = 110;
                                ThirdPic.Width = 165;
                                ThirdPic.Height = 110;
                                FourthPic.Width = 165;
                                FourthPic.Height = 110;
                                FifthPic.Width = 165;
                                SixthPic.Height = 110;

                                FirstPicFront.Width = 165;
                                FirstPicFront.Height = 110;
                                SecondPicFront.Width = 165;
                                SecondPicFront.Height = 110;
                                ThirdPicFront.Width = 165;
                                ThirdPicFront.Height = 110;
                                FourthPicFront.Width = 165;
                                FourthPicFront.Height = 110;
                                FifthPicFront.Width = 165;
                                FifthPicFront.Height = 110;
                                SixthPicFront.Width = 165;
                                SixthPicFront.Height = 110;

                                FirstPic.Margin = new Thickness(1520, 180, 235, 790);
                                SecondPic.Margin = new Thickness(1520, 302, 235, 668);
                                ThirdPic.Margin = new Thickness(1520, 424, 235, 546);
                                FourthPic.Margin = new Thickness(1520, 546, 235, 424);
                                FifthPic.Margin = new Thickness(1520, 668, 235, 302);
                                SixthPic.Margin = new Thickness(1520, 790, 235, 180);

                                FirstPicFront.Margin = FirstPic.Margin;
                                SecondPicFront.Margin = SecondPic.Margin;
                                ThirdPicFront.Margin = ThirdPic.Margin;
                                FourthPicFront.Margin = FourthPic.Margin;
                                FifthPicFront.Margin = FifthPic.Margin;
                                SixthPicFront.Margin = SixthPic.Margin;

                                if (MainWindow.checkvideo == "1")
                                {
                                    startx = 420;
                                    starty = 180;
                                    width = 1080;
                                    height = 720;
                                }
                                break;
                        }

                        BitmapImage one = new BitmapImage();
                        one.BeginInit();
                        switch (TempSelect.temp)
                        {
                            case 1:
                                one.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                one.CacheOption = BitmapCacheOption.OnLoad;
                                one.DecodePixelWidth = 1080;
                                one.DecodePixelHeight = 1080;
                                break;
                            case 2:
                                one.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                one.CacheOption = BitmapCacheOption.OnLoad;
                                one.DecodePixelWidth = 1080;
                                one.DecodePixelHeight = 720;
                                break;
                            case 3:
                                one.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                one.CacheOption = BitmapCacheOption.OnLoad;
                                one.DecodePixelWidth = 1080;
                                one.DecodePixelHeight = 1080;
                                break;
                            case 4:
                                one.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                one.CacheOption = BitmapCacheOption.OnLoad;
                                one.DecodePixelWidth = 1080;
                                one.DecodePixelHeight = 1080;
                                break;
                        }
                        one.EndInit();
                        frontlvimg.Source = one;


                        switch (TempSelect.temp)
                        {
                            case 1:
                                MainWindow.photonum = 6;
                                break;
                            case 2:
                                MainWindow.photonum = 6;
                                break;
                            case 3:
                                MainWindow.photonum = 5;
                                break;
                            case 4:
                                MainWindow.photonum = 4;
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "r54")
                    {
                        MainWindow.photonum = 6;

                        FirstPic.Width = 165;
                        FirstPic.Height = 110;
                        SecondPic.Width = 165;
                        SecondPic.Height = 110;
                        ThirdPic.Width = 165;
                        ThirdPic.Height = 110;
                        FourthPic.Width = 165;
                        FourthPic.Height = 110;
                        FifthPic.Width = 165;
                        SixthPic.Height = 110;

                        FirstPicFront.Width = 165;
                        FirstPicFront.Height = 110;
                        SecondPicFront.Width = 165;
                        SecondPicFront.Height = 110;
                        ThirdPicFront.Width = 165;
                        ThirdPicFront.Height = 110;
                        FourthPicFront.Width = 165;
                        FourthPicFront.Height = 110;
                        FifthPicFront.Width = 165;
                        FifthPicFront.Height = 110;
                        SixthPicFront.Width = 165;
                        SixthPicFront.Height = 110;

                        FirstPic.Margin = new Thickness(1520, 180, 235, 790);
                        SecondPic.Margin = new Thickness(1520, 302, 235, 668);
                        ThirdPic.Margin = new Thickness(1520, 424, 235, 546);
                        FourthPic.Margin = new Thickness(1520, 546, 235, 424);
                        FifthPic.Margin = new Thickness(1520, 668, 235, 302);
                        SixthPic.Margin = new Thickness(1520, 790, 235, 180);

                        FirstPicFront.Margin = FirstPic.Margin;
                        SecondPicFront.Margin = SecondPic.Margin;
                        ThirdPicFront.Margin = ThirdPic.Margin;
                        FourthPicFront.Margin = FourthPic.Margin;
                        FifthPicFront.Margin = FifthPic.Margin;
                        SixthPicFront.Margin = SixthPic.Margin;

                        if (MainWindow.checkvideo == "1")
                        {
                            startx = 420;
                            starty = 180;
                            width = 1080;
                            height = 720;
                        }
                    }
                }

                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_Tik);
                timer.Start();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (DllNotFoundException)
            {
                Source.Log.log.Error("Canon DLLs not found!");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #endregion

        #region /// 웹캠 동작 ///

        private void webcamthread()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                MainWindow.video.FrameWidth = 1920;
                MainWindow.video.FrameHeight = 1080;

                while (checkvideothread)
                {
                    while (MainWindow.video.PosFrames != 0)
                    {
                        frame = new Mat();
                        flipframe = new Mat();

                        if (MainWindow.video.Read(frame))
                        {
                            if (phototime)
                            {
                                //Delay(1000);
                            }
                            else
                            {
                                if (MainWindow.checkingflip == "Using")
                                {
                                    flipframe = frame;
                                    bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(flipframe);
                                }
                                else
                                {
                                    Cv2.Flip(frame, flipframe, FlipMode.Y);
                                    bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(flipframe);
                                }

                                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                        if (checkvideothread)
                                        {
                                            var bi = new BitmapImage();
                                            bi.BeginInit();
                                            bi.StreamSource = ms;
                                            bi.CacheOption = BitmapCacheOption.OnLoad;
                                            bi.DecodePixelWidth = 1920;
                                            bi.DecodePixelHeight = 1080;
                                            bi.EndInit();
                                            LiveView.Source = bi;
                                            ms.Dispose();
                                        }
                                    }
                                }));
                            }
                        }
                        frame.Dispose();
                        flipframe.Dispose();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #endregion

        #region /// 타이머 ///

        private void Timer_Tik(object sender, EventArgs e)
        {
            try
            {
                if ((photonum == 0 && recordstart == 0) || (photonum == 4 && recordstart == 0))
                {
                    if (MainWindow.checkvideo == "1")
                    {
                        recordstart = 1;
                        isRecording = true;
                        videoname = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string VideoOutput = MainWindow.Videopath + @"\original" + "\\" + "Video_" + videoname + ".mp4";
                        recordthread = new Thread(new ThreadStart(StartRecord));
                        recordthread.Start();
                    }
                }
                if (count != 0)
                {
                    count -= 1;
                    tblState.Text = count.ToString();

                    if (count == 5)
                    {
                        MainWindow.fivetik.Play();
                    }
                    else if (count == 4)
                    {
                        MainWindow.fourtik.Play();
                    }
                    else if (count == 3)
                    {
                        MainWindow.threetik.Play();
                    }
                    else if (count == 2)
                    {
                        MainWindow.twotik.Play();
                    }
                    else if (count == 1)
                    {
                        MainWindow.onetik.Play();
                    }
                    else if (count == 0)
                    {
                        MainWindow.shot.Play();
                        timer.Stop();
                        Source.Log.log.Info("타이머 종료");

                        photonum++;

                        if (photonum > 999)
                        {
                            photonumber = Convert.ToString(photonum);
                        }
                        if (photonum < 1000 && photonum > 99)
                        {
                            photonumber = "0" + Convert.ToString(photonum);
                        }
                        if (photonum < 100 && photonum >= 10)
                        {
                            photonumber = "00" + Convert.ToString(photonum);
                        }
                        if (photonum < 10)
                        {
                            photonumber = "000" + Convert.ToString(photonum);
                        }

                        if (MainWindow.checkflip.ToString() == "Using")
                        {
                            phototime = true;

                            Delay(1000);

                            //savebitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(flipframe);
                            savebitmap = bitmap;
                            savebitmap.SetResolution(72, 72);

                            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

                            EncoderParameters encoderParams = new EncoderParameters(1);
                            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L); // JPEG 품질 조절

                            savebitmap.Save(MainWindow.PhotoPath + @"\IMG_" + photonumber + ".JPG", jpegCodec, encoderParams);
                        }
                        else
                        {
                            phototime = true;

                            Delay(1000);

                            //savebitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(flipframe);
                            savebitmap = bitmap;
                            savebitmap.SetResolution(72, 72);

                            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

                            EncoderParameters encoderParams = new EncoderParameters(1);
                            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L); // JPEG 품질 조절

                            savebitmap.Save(MainWindow.PhotoPath + @"\IMG_" + photonumber + ".JPG", jpegCodec, encoderParams);
                        }

                        phototime = false;

                        smallimg = photonum % MainWindow.photonum;

                        //Delay(1000);

                        CutImg(MainWindow.PhotoPath + @"\IMG_" + photonumber + ".JPG");

                        if (MainWindow.SorR == "S")
                        {
                            if (MainWindow.inifoldername == "r80")
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        switch (smallimg)
                                        {
                                            case 1:
                                                MainWindow.firstphoto = new BitmapImage();
                                                MainWindow.firstphoto.BeginInit();
                                                MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                MainWindow.firstphoto.DecodePixelHeight = 720;
                                                MainWindow.firstphoto.EndInit();
                                                FirstPic.Source = MainWindow.firstphoto;
                                                break;
                                            case 2:
                                                MainWindow.secondphoto = new BitmapImage();
                                                MainWindow.secondphoto.BeginInit();
                                                MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                MainWindow.secondphoto.DecodePixelHeight = 720;
                                                MainWindow.secondphoto.EndInit();
                                                SecondPic.Source = MainWindow.secondphoto;
                                                break;
                                            case 3:
                                                MainWindow.thirdphoto = new BitmapImage();
                                                MainWindow.thirdphoto.BeginInit();
                                                MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                MainWindow.thirdphoto.DecodePixelHeight = 720;
                                                MainWindow.thirdphoto.EndInit();
                                                ThirdPic.Source = MainWindow.thirdphoto;
                                                break;
                                            case 0:
                                                MainWindow.fourthphoto = new BitmapImage();
                                                MainWindow.fourthphoto.BeginInit();
                                                MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fourthphoto.DecodePixelHeight = 720;
                                                MainWindow.fourthphoto.EndInit();
                                                FourthPic.Source = MainWindow.fourthphoto;
                                                Delay(200);
                                                break;
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                    case 6:
                                        switch (smallimg)
                                        {
                                            case 1:
                                                MainWindow.firstphoto = new BitmapImage();
                                                MainWindow.firstphoto.BeginInit();
                                                MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                MainWindow.firstphoto.DecodePixelHeight = 720;
                                                MainWindow.firstphoto.EndInit();
                                                FirstPic.Source = MainWindow.firstphoto;
                                                break;
                                            case 2:
                                                MainWindow.secondphoto = new BitmapImage();
                                                MainWindow.secondphoto.BeginInit();
                                                MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                MainWindow.secondphoto.DecodePixelHeight = 720;
                                                MainWindow.secondphoto.EndInit();
                                                SecondPic.Source = MainWindow.secondphoto;
                                                break;
                                            case 3:
                                                MainWindow.thirdphoto = new BitmapImage();
                                                MainWindow.thirdphoto.BeginInit();
                                                MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                MainWindow.thirdphoto.DecodePixelHeight = 720;
                                                MainWindow.thirdphoto.EndInit();
                                                ThirdPic.Source = MainWindow.thirdphoto;
                                                break;
                                            case 4:
                                                MainWindow.fourthphoto = new BitmapImage();
                                                MainWindow.fourthphoto.BeginInit();
                                                MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fourthphoto.DecodePixelHeight = 720;
                                                MainWindow.fourthphoto.EndInit();
                                                FourthPic.Source = MainWindow.fourthphoto;
                                                break;
                                            case 5:
                                                MainWindow.fifthphoto = new BitmapImage();
                                                MainWindow.fifthphoto.BeginInit();
                                                MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fifthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fifthphoto.DecodePixelHeight = 720;
                                                MainWindow.fifthphoto.EndInit();
                                                FifthPic.Source = MainWindow.fifthphoto;
                                                break;
                                            case 0:
                                                MainWindow.sixthphoto = new BitmapImage();
                                                MainWindow.sixthphoto.BeginInit();
                                                MainWindow.sixthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.sixthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.sixthphoto.DecodePixelWidth = 1080;
                                                MainWindow.sixthphoto.DecodePixelHeight = 720;
                                                MainWindow.sixthphoto.EndInit();
                                                SixthPic.Source = MainWindow.sixthphoto;
                                                break;
                                        }
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "r30")
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        switch (smallimg)
                                        {
                                            case 1:
                                                MainWindow.firstphoto = new BitmapImage();
                                                MainWindow.firstphoto.BeginInit();
                                                MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                MainWindow.firstphoto.EndInit();
                                                FirstPic.Source = MainWindow.firstphoto;
                                                break;
                                            case 2:
                                                MainWindow.secondphoto = new BitmapImage();
                                                MainWindow.secondphoto.BeginInit();
                                                MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                MainWindow.secondphoto.EndInit();
                                                SecondPic.Source = MainWindow.secondphoto;
                                                break;
                                            case 0:
                                                MainWindow.thirdphoto = new BitmapImage();
                                                MainWindow.thirdphoto.BeginInit();
                                                MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                MainWindow.thirdphoto.EndInit();
                                                ThirdPic.Source = MainWindow.thirdphoto;
                                                break;
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                    case 4:
                                        switch (smallimg)
                                        {
                                            case 1:
                                                MainWindow.firstphoto = new BitmapImage();
                                                MainWindow.firstphoto.BeginInit();
                                                MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                MainWindow.firstphoto.EndInit();
                                                FirstPic.Source = MainWindow.firstphoto;
                                                break;
                                            case 2:
                                                MainWindow.secondphoto = new BitmapImage();
                                                MainWindow.secondphoto.BeginInit();
                                                MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                MainWindow.secondphoto.EndInit();
                                                SecondPic.Source = MainWindow.secondphoto;
                                                break;
                                            case 3:
                                                MainWindow.thirdphoto = new BitmapImage();
                                                MainWindow.thirdphoto.BeginInit();
                                                MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                MainWindow.thirdphoto.EndInit();
                                                ThirdPic.Source = MainWindow.thirdphoto;
                                                break;
                                            case 0:
                                                MainWindow.fourthphoto = new BitmapImage();
                                                MainWindow.fourthphoto.BeginInit();
                                                MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fourthphoto.DecodePixelHeight = 1080;
                                                MainWindow.fourthphoto.EndInit();
                                                FourthPic.Source = MainWindow.fourthphoto;
                                                break;
                                        }
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "r28")
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        switch (smallimg)
                                        {
                                            case 1:
                                                MainWindow.firstphoto = new BitmapImage();
                                                MainWindow.firstphoto.BeginInit();
                                                MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                MainWindow.firstphoto.EndInit();
                                                FirstPic.Source = MainWindow.firstphoto;

                                                BitmapImage pic1front = new BitmapImage();
                                                pic1front.BeginInit();
                                                pic1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                pic1front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic1front.DecodePixelWidth = 1080;
                                                pic1front.DecodePixelHeight = 1080;
                                                pic1front.EndInit();
                                                FirstPicFront.Source = pic1front;
                                                break;
                                            case 2:
                                                MainWindow.secondphoto = new BitmapImage();
                                                MainWindow.secondphoto.BeginInit();
                                                MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                MainWindow.secondphoto.EndInit();
                                                SecondPic.Source = MainWindow.secondphoto;

                                                BitmapImage pic2front = new BitmapImage();
                                                pic2front.BeginInit();
                                                pic2front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                pic2front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic2front.DecodePixelWidth = 1080;
                                                pic2front.DecodePixelHeight = 1080;
                                                pic2front.EndInit();
                                                SecondPicFront.Source = pic2front;
                                                break;
                                            case 3:
                                                MainWindow.thirdphoto = new BitmapImage();
                                                MainWindow.thirdphoto.BeginInit();
                                                MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                MainWindow.thirdphoto.EndInit();
                                                ThirdPic.Source = MainWindow.thirdphoto;

                                                BitmapImage pic3front = new BitmapImage();
                                                pic3front.BeginInit();
                                                pic3front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                pic3front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic3front.DecodePixelWidth = 1080;
                                                pic3front.DecodePixelHeight = 1080;
                                                pic3front.EndInit();
                                                ThirdPicFront.Source = pic3front;
                                                break;
                                            case 4:
                                                MainWindow.fourthphoto = new BitmapImage();
                                                MainWindow.fourthphoto.BeginInit();
                                                MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fourthphoto.DecodePixelHeight = 1080;
                                                MainWindow.fourthphoto.EndInit();
                                                FourthPic.Source = MainWindow.fourthphoto;

                                                BitmapImage pic4front = new BitmapImage();
                                                pic4front.BeginInit();
                                                pic4front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                pic4front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic4front.DecodePixelWidth = 1080;
                                                pic4front.DecodePixelHeight = 1080;
                                                pic4front.EndInit();
                                                FourthPicFront.Source = pic4front;
                                                break;
                                            case 5:
                                                MainWindow.fifthphoto = new BitmapImage();
                                                MainWindow.fifthphoto.BeginInit();
                                                MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fifthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fifthphoto.DecodePixelHeight = 1080;
                                                MainWindow.fifthphoto.EndInit();
                                                FifthPic.Source = MainWindow.fifthphoto;

                                                BitmapImage pic5front = new BitmapImage();
                                                pic5front.BeginInit();
                                                pic5front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                pic5front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic5front.DecodePixelWidth = 1080;
                                                pic5front.DecodePixelHeight = 1080;
                                                pic5front.EndInit();
                                                FifthPicFront.Source = pic5front;
                                                break;
                                            case 0:
                                                MainWindow.sixthphoto = new BitmapImage();
                                                MainWindow.sixthphoto.BeginInit();
                                                MainWindow.sixthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.sixthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.sixthphoto.DecodePixelWidth = 1080;
                                                MainWindow.sixthphoto.DecodePixelHeight = 1080;
                                                MainWindow.sixthphoto.EndInit();
                                                SixthPic.Source = MainWindow.sixthphoto;

                                                BitmapImage pic6front = new BitmapImage();
                                                pic6front.BeginInit();
                                                pic6front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                pic6front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic6front.DecodePixelWidth = 1080;
                                                pic6front.DecodePixelHeight = 1080;
                                                pic6front.EndInit();
                                                SixthPicFront.Source = pic6front;
                                                break;
                                        }
                                        break;
                                    case 2:
                                        switch (smallimg)
                                        {
                                            case 1:
                                                MainWindow.firstphoto = new BitmapImage();
                                                MainWindow.firstphoto.BeginInit();
                                                MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                MainWindow.firstphoto.DecodePixelHeight = 720;
                                                MainWindow.firstphoto.EndInit();
                                                FirstPic.Source = MainWindow.firstphoto;

                                                BitmapImage pic1front = new BitmapImage();
                                                pic1front.BeginInit();
                                                pic1front.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                                pic1front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic1front.DecodePixelWidth = 1080;
                                                pic1front.DecodePixelHeight = 720;
                                                pic1front.EndInit();
                                                FirstPicFront.Source = pic1front;
                                                break;
                                            case 2:
                                                MainWindow.secondphoto = new BitmapImage();
                                                MainWindow.secondphoto.BeginInit();
                                                MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                MainWindow.secondphoto.DecodePixelHeight = 720;
                                                MainWindow.secondphoto.EndInit();
                                                SecondPic.Source = MainWindow.secondphoto;

                                                BitmapImage pic2front = new BitmapImage();
                                                pic2front.BeginInit();
                                                pic2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                pic2front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic2front.DecodePixelWidth = 1080;
                                                pic2front.DecodePixelHeight = 720;
                                                pic2front.EndInit();
                                                SecondPicFront.Source = pic2front;
                                                break;
                                            case 3:
                                                MainWindow.thirdphoto = new BitmapImage();
                                                MainWindow.thirdphoto.BeginInit();
                                                MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                MainWindow.thirdphoto.DecodePixelHeight = 720;
                                                MainWindow.thirdphoto.EndInit();
                                                ThirdPic.Source = MainWindow.thirdphoto;

                                                BitmapImage pic3front = new BitmapImage();
                                                pic3front.BeginInit();
                                                pic3front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                pic3front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic3front.DecodePixelWidth = 1080;
                                                pic3front.DecodePixelHeight = 720;
                                                pic3front.EndInit();
                                                ThirdPicFront.Source = pic3front;
                                                break;
                                            case 4:
                                                MainWindow.fourthphoto = new BitmapImage();
                                                MainWindow.fourthphoto.BeginInit();
                                                MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fourthphoto.DecodePixelHeight = 720;
                                                MainWindow.fourthphoto.EndInit();
                                                FourthPic.Source = MainWindow.fourthphoto;

                                                BitmapImage pic4front = new BitmapImage();
                                                pic4front.BeginInit();
                                                pic4front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                pic4front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic4front.DecodePixelWidth = 1080;
                                                pic4front.DecodePixelHeight = 720;
                                                pic4front.EndInit();
                                                FourthPicFront.Source = pic4front;
                                                break;
                                            case 5:
                                                MainWindow.fifthphoto = new BitmapImage();
                                                MainWindow.fifthphoto.BeginInit();
                                                MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fifthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fifthphoto.DecodePixelHeight = 720;
                                                MainWindow.fifthphoto.EndInit();
                                                FifthPic.Source = MainWindow.fifthphoto;

                                                BitmapImage pic5front = new BitmapImage();
                                                pic5front.BeginInit();
                                                pic5front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                pic5front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic5front.DecodePixelWidth = 1080;
                                                pic5front.DecodePixelHeight = 720;
                                                pic5front.EndInit();
                                                FifthPicFront.Source = pic5front;
                                                break;
                                            case 0:
                                                MainWindow.sixthphoto = new BitmapImage();
                                                MainWindow.sixthphoto.BeginInit();
                                                MainWindow.sixthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.sixthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.sixthphoto.DecodePixelWidth = 1080;
                                                MainWindow.sixthphoto.DecodePixelHeight = 720;
                                                MainWindow.sixthphoto.EndInit();
                                                SixthPic.Source = MainWindow.sixthphoto;

                                                BitmapImage pic6front = new BitmapImage();
                                                pic6front.BeginInit();
                                                pic6front.UriSource = new Uri(MainWindow.TempPath + @"\2-6.png", UriKind.RelativeOrAbsolute);
                                                pic6front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic6front.DecodePixelWidth = 1080;
                                                pic6front.DecodePixelHeight = 720;
                                                pic6front.EndInit();
                                                SixthPicFront.Source = pic6front;
                                                break;
                                        }
                                        break;
                                    case 3:
                                        switch (smallimg)
                                        {
                                            case 1:
                                                MainWindow.firstphoto = new BitmapImage();
                                                MainWindow.firstphoto.BeginInit();
                                                MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                MainWindow.firstphoto.EndInit();
                                                FirstPic.Source = MainWindow.firstphoto;

                                                BitmapImage pic1front = new BitmapImage();
                                                pic1front.BeginInit();
                                                pic1front.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                                pic1front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic1front.DecodePixelWidth = 1080;
                                                pic1front.DecodePixelHeight = 1080;
                                                pic1front.EndInit();
                                                FirstPicFront.Source = pic1front;
                                                break;
                                            case 2:
                                                MainWindow.secondphoto = new BitmapImage();
                                                MainWindow.secondphoto.BeginInit();
                                                MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                MainWindow.secondphoto.EndInit();
                                                SecondPic.Source = MainWindow.secondphoto;

                                                BitmapImage pic2front = new BitmapImage();
                                                pic2front.BeginInit();
                                                pic2front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                pic2front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic2front.DecodePixelWidth = 1080;
                                                pic2front.DecodePixelHeight = 1080;
                                                pic2front.EndInit();
                                                SecondPicFront.Source = pic2front;
                                                break;
                                            case 3:
                                                MainWindow.thirdphoto = new BitmapImage();
                                                MainWindow.thirdphoto.BeginInit();
                                                MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                MainWindow.thirdphoto.EndInit();
                                                ThirdPic.Source = MainWindow.thirdphoto;

                                                BitmapImage pic3front = new BitmapImage();
                                                pic3front.BeginInit();
                                                pic3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                pic3front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic3front.DecodePixelWidth = 1080;
                                                pic3front.DecodePixelHeight = 1080;
                                                pic3front.EndInit();
                                                ThirdPicFront.Source = pic3front;
                                                break;
                                            case 4:
                                                MainWindow.fourthphoto = new BitmapImage();
                                                MainWindow.fourthphoto.BeginInit();
                                                MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fourthphoto.DecodePixelHeight = 1080;
                                                MainWindow.fourthphoto.EndInit();
                                                FourthPic.Source = MainWindow.fourthphoto;

                                                BitmapImage pic4front = new BitmapImage();
                                                pic4front.BeginInit();
                                                pic4front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                pic4front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic4front.DecodePixelWidth = 1080;
                                                pic4front.DecodePixelHeight = 1080;
                                                pic4front.EndInit();
                                                FourthPicFront.Source = pic4front;
                                                break;
                                            case 0:
                                                MainWindow.fifthphoto = new BitmapImage();
                                                MainWindow.fifthphoto.BeginInit();
                                                MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fifthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fifthphoto.DecodePixelHeight = 1080;
                                                MainWindow.fifthphoto.EndInit();
                                                FifthPic.Source = MainWindow.fifthphoto;

                                                BitmapImage pic5front = new BitmapImage();
                                                pic5front.BeginInit();
                                                pic5front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                pic5front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic5front.DecodePixelWidth = 1080;
                                                pic5front.DecodePixelHeight = 1080;
                                                pic5front.EndInit();
                                                FifthPicFront.Source = pic5front;
                                                break;
                                        }
                                        break;
                                    case 4:
                                        switch (smallimg)
                                        {
                                            case 1:
                                                MainWindow.firstphoto = new BitmapImage();
                                                MainWindow.firstphoto.BeginInit();
                                                MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                MainWindow.firstphoto.EndInit();
                                                FirstPic.Source = MainWindow.firstphoto;

                                                BitmapImage pic1front = new BitmapImage();
                                                pic1front.BeginInit();
                                                pic1front.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                                pic1front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic1front.DecodePixelWidth = 1080;
                                                pic1front.DecodePixelHeight = 1080;
                                                pic1front.EndInit();
                                                FirstPicFront.Source = pic1front;
                                                break;
                                            case 2:
                                                MainWindow.secondphoto = new BitmapImage();
                                                MainWindow.secondphoto.BeginInit();
                                                MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                MainWindow.secondphoto.EndInit();
                                                SecondPic.Source = MainWindow.secondphoto;

                                                BitmapImage pic2front = new BitmapImage();
                                                pic2front.BeginInit();
                                                pic2front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                pic2front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic2front.DecodePixelWidth = 1080;
                                                pic2front.DecodePixelHeight = 1080;
                                                pic2front.EndInit();
                                                SecondPicFront.Source = pic2front;
                                                break;
                                            case 3:
                                                MainWindow.thirdphoto = new BitmapImage();
                                                MainWindow.thirdphoto.BeginInit();
                                                MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                MainWindow.thirdphoto.EndInit();
                                                ThirdPic.Source = MainWindow.thirdphoto;

                                                BitmapImage pic3front = new BitmapImage();
                                                pic3front.BeginInit();
                                                pic3front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                pic3front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic3front.DecodePixelWidth = 1080;
                                                pic3front.DecodePixelHeight = 1080;
                                                pic3front.EndInit();
                                                ThirdPicFront.Source = pic3front;
                                                break;
                                            case 0:
                                                MainWindow.fourthphoto = new BitmapImage();
                                                MainWindow.fourthphoto.BeginInit();
                                                MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                MainWindow.fourthphoto.DecodePixelHeight = 1080;
                                                MainWindow.fourthphoto.EndInit();
                                                FourthPic.Source = MainWindow.fourthphoto;

                                                BitmapImage pic4front = new BitmapImage();
                                                pic4front.BeginInit();
                                                pic4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                pic4front.CacheOption = BitmapCacheOption.OnLoad;
                                                pic4front.DecodePixelWidth = 1080;
                                                pic4front.DecodePixelHeight = 1080;
                                                pic4front.EndInit();
                                                FourthPicFront.Source = pic4front;
                                                break;
                                        }
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "r81" || MainWindow.inifoldername == "r83")
                            {
                                switch (smallimg)
                                {
                                    case 1:
                                        MainWindow.firstphoto = new BitmapImage();
                                        MainWindow.firstphoto.BeginInit();
                                        MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.firstphoto.DecodePixelWidth = 1080;
                                        MainWindow.firstphoto.DecodePixelHeight = 720;
                                        MainWindow.firstphoto.EndInit();
                                        FirstPic.Source = MainWindow.firstphoto;
                                        break;
                                    case 2:
                                        MainWindow.secondphoto = new BitmapImage();
                                        MainWindow.secondphoto.BeginInit();
                                        MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.secondphoto.DecodePixelWidth = 1080;
                                        MainWindow.secondphoto.DecodePixelHeight = 720;
                                        MainWindow.secondphoto.EndInit();
                                        SecondPic.Source = MainWindow.secondphoto;
                                        break;
                                    case 3:
                                        MainWindow.thirdphoto = new BitmapImage();
                                        MainWindow.thirdphoto.BeginInit();
                                        MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                        MainWindow.thirdphoto.DecodePixelHeight = 720;
                                        MainWindow.thirdphoto.EndInit();
                                        ThirdPic.Source = MainWindow.thirdphoto;
                                        break;
                                    case 4:
                                        MainWindow.fourthphoto = new BitmapImage();
                                        MainWindow.fourthphoto.BeginInit();
                                        MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                        MainWindow.fourthphoto.DecodePixelHeight = 720;
                                        MainWindow.fourthphoto.EndInit();
                                        FourthPic.Source = MainWindow.fourthphoto;
                                        break;
                                    case 5:
                                        MainWindow.fifthphoto = new BitmapImage();
                                        MainWindow.fifthphoto.BeginInit();
                                        MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.fifthphoto.DecodePixelWidth = 1080;
                                        MainWindow.fifthphoto.DecodePixelHeight = 720;
                                        MainWindow.fifthphoto.EndInit();
                                        FifthPic.Source = MainWindow.fifthphoto;
                                        break;
                                    case 0:
                                        MainWindow.sixthphoto = new BitmapImage();
                                        MainWindow.sixthphoto.BeginInit();
                                        MainWindow.sixthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.sixthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.sixthphoto.DecodePixelWidth = 1080;
                                        MainWindow.sixthphoto.DecodePixelHeight = 720;
                                        MainWindow.sixthphoto.EndInit();
                                        SixthPic.Source = MainWindow.sixthphoto;
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "r54")
                            {
                                switch (smallimg)
                                {
                                    case 1:
                                        MainWindow.firstphoto = new BitmapImage();
                                        MainWindow.firstphoto.BeginInit();
                                        MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.firstphoto.DecodePixelWidth = 1080;
                                        MainWindow.firstphoto.DecodePixelHeight = 720;
                                        MainWindow.firstphoto.EndInit();
                                        FirstPic.Source = MainWindow.firstphoto;
                                        break;
                                    case 2:
                                        MainWindow.secondphoto = new BitmapImage();
                                        MainWindow.secondphoto.BeginInit();
                                        MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.secondphoto.DecodePixelWidth = 1080;
                                        MainWindow.secondphoto.DecodePixelHeight = 720;
                                        MainWindow.secondphoto.EndInit();
                                        SecondPic.Source = MainWindow.secondphoto;
                                        break;
                                    case 3:
                                        MainWindow.thirdphoto = new BitmapImage();
                                        MainWindow.thirdphoto.BeginInit();
                                        MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                        MainWindow.thirdphoto.DecodePixelHeight = 720;
                                        MainWindow.thirdphoto.EndInit();
                                        ThirdPic.Source = MainWindow.thirdphoto;
                                        break;
                                    case 4:
                                        MainWindow.fourthphoto = new BitmapImage();
                                        MainWindow.fourthphoto.BeginInit();
                                        MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                        MainWindow.fourthphoto.DecodePixelHeight = 720;
                                        MainWindow.fourthphoto.EndInit();
                                        FourthPic.Source = MainWindow.fourthphoto;
                                        break;
                                    case 5:
                                        MainWindow.fifthphoto = new BitmapImage();
                                        MainWindow.fifthphoto.BeginInit();
                                        MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.fifthphoto.DecodePixelWidth = 1080;
                                        MainWindow.fifthphoto.DecodePixelHeight = 720;
                                        MainWindow.fifthphoto.EndInit();
                                        FifthPic.Source = MainWindow.fifthphoto;
                                        break;
                                    case 0:
                                        MainWindow.sixthphoto = new BitmapImage();
                                        MainWindow.sixthphoto.BeginInit();
                                        MainWindow.sixthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                        MainWindow.sixthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                        MainWindow.sixthphoto.DecodePixelWidth = 1080;
                                        MainWindow.sixthphoto.DecodePixelHeight = 720;
                                        MainWindow.sixthphoto.EndInit();
                                        SixthPic.Source = MainWindow.sixthphoto;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (MainWindow.picratio)
                            {
                                case 1:
                                    switch (MainWindow.photonum)
                                    {
                                        case 1:
                                        case 2:
                                        case 3:
                                        case 4:
                                            switch (smallimg)
                                            {
                                                case 1:
                                                    MainWindow.firstphoto = new BitmapImage();
                                                    MainWindow.firstphoto.BeginInit();
                                                    MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                    MainWindow.firstphoto.DecodePixelHeight = 720;
                                                    MainWindow.firstphoto.EndInit();
                                                    FirstPic.Source = MainWindow.firstphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                FirstPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                FirstPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                FirstPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                FirstPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                FirstPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        FirstPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 2:
                                                    MainWindow.secondphoto = new BitmapImage();
                                                    MainWindow.secondphoto.BeginInit();
                                                    MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                    MainWindow.secondphoto.DecodePixelHeight = 720;
                                                    MainWindow.secondphoto.EndInit();
                                                    SecondPic.Source = MainWindow.secondphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                SecondPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                SecondPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                SecondPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                SecondPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                SecondPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                SecondPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        SecondPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 3:
                                                    MainWindow.thirdphoto = new BitmapImage();
                                                    MainWindow.thirdphoto.BeginInit();
                                                    MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                    MainWindow.thirdphoto.DecodePixelHeight = 720;
                                                    MainWindow.thirdphoto.EndInit();
                                                    ThirdPic.Source = MainWindow.thirdphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                ThirdPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                ThirdPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                ThirdPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                ThirdPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                ThirdPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                ThirdPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        ThirdPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 0:
                                                    MainWindow.fourthphoto = new BitmapImage();
                                                    MainWindow.fourthphoto.BeginInit();
                                                    MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.fourthphoto.DecodePixelHeight = 720;
                                                    MainWindow.fourthphoto.EndInit();
                                                    FourthPic.Source = MainWindow.fourthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                FourthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                FourthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                FourthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                FourthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                FourthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        FourthPicFront.Source = Temp1front;
                                                    }
                                                    Delay(200);
                                                    break;
                                            }
                                            break;
                                        case 5:
                                            switch (smallimg)
                                            {
                                                case 1:
                                                    MainWindow.firstphoto = new BitmapImage();
                                                    MainWindow.firstphoto.BeginInit();
                                                    MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                    MainWindow.firstphoto.DecodePixelHeight = 720;
                                                    MainWindow.firstphoto.EndInit();
                                                    FirstPic.Source = MainWindow.firstphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                FirstPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                FirstPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                FirstPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                FirstPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                FirstPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        FirstPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 2:
                                                    MainWindow.secondphoto = new BitmapImage();
                                                    MainWindow.secondphoto.BeginInit();
                                                    MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                    MainWindow.secondphoto.DecodePixelHeight = 720;
                                                    MainWindow.secondphoto.EndInit();
                                                    SecondPic.Source = MainWindow.secondphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                SecondPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                SecondPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                SecondPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                SecondPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                SecondPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                SecondPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        SecondPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 3:
                                                    MainWindow.thirdphoto = new BitmapImage();
                                                    MainWindow.thirdphoto.BeginInit();
                                                    MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                    MainWindow.thirdphoto.DecodePixelHeight = 720;
                                                    MainWindow.thirdphoto.EndInit();
                                                    ThirdPic.Source = MainWindow.thirdphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                ThirdPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                ThirdPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                ThirdPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                ThirdPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                ThirdPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                ThirdPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        ThirdPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 4:
                                                    MainWindow.fourthphoto = new BitmapImage();
                                                    MainWindow.fourthphoto.BeginInit();
                                                    MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.fourthphoto.DecodePixelHeight = 720;
                                                    MainWindow.fourthphoto.EndInit();
                                                    FourthPic.Source = MainWindow.fourthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                FourthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                FourthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                FourthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                FourthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                FourthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        FourthPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 0:
                                                    MainWindow.fifthphoto = new BitmapImage();
                                                    MainWindow.fifthphoto.BeginInit();
                                                    MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fifthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.fifthphoto.DecodePixelHeight = 720;
                                                    MainWindow.fifthphoto.EndInit();
                                                    FifthPic.Source = MainWindow.fifthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                FifthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                FifthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                FifthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                FifthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                FifthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        FifthPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                            }
                                            break;
                                        case 6:
                                            switch (smallimg)
                                            {
                                                case 1:
                                                    MainWindow.firstphoto = new BitmapImage();
                                                    MainWindow.firstphoto.BeginInit();
                                                    MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                    MainWindow.firstphoto.DecodePixelHeight = 720;
                                                    MainWindow.firstphoto.EndInit();
                                                    FirstPic.Source = MainWindow.firstphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                FirstPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                FirstPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                FirstPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                FirstPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                FirstPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        FirstPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 2:
                                                    MainWindow.secondphoto = new BitmapImage();
                                                    MainWindow.secondphoto.BeginInit();
                                                    MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                    MainWindow.secondphoto.DecodePixelHeight = 720;
                                                    MainWindow.secondphoto.EndInit();
                                                    SecondPic.Source = MainWindow.secondphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                SecondPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                SecondPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                SecondPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                SecondPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                SecondPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                SecondPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        SecondPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 3:
                                                    MainWindow.thirdphoto = new BitmapImage();
                                                    MainWindow.thirdphoto.BeginInit();
                                                    MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                    MainWindow.thirdphoto.DecodePixelHeight = 720;
                                                    MainWindow.thirdphoto.EndInit();
                                                    ThirdPic.Source = MainWindow.thirdphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                ThirdPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                ThirdPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                ThirdPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                ThirdPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                ThirdPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                ThirdPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        ThirdPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 4:
                                                    MainWindow.fourthphoto = new BitmapImage();
                                                    MainWindow.fourthphoto.BeginInit();
                                                    MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.fourthphoto.DecodePixelHeight = 720;
                                                    MainWindow.fourthphoto.EndInit();
                                                    FourthPic.Source = MainWindow.fourthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                FourthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                FourthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                FourthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                FourthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                FourthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        FourthPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 5:
                                                    MainWindow.fifthphoto = new BitmapImage();
                                                    MainWindow.fifthphoto.BeginInit();
                                                    MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fifthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.fifthphoto.DecodePixelHeight = 720;
                                                    MainWindow.fifthphoto.EndInit();
                                                    FifthPic.Source = MainWindow.fifthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                FifthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                FifthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                FifthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                FifthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                FifthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        FifthPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 0:
                                                    MainWindow.sixthphoto = new BitmapImage();
                                                    MainWindow.sixthphoto.BeginInit();
                                                    MainWindow.sixthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.sixthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.sixthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.sixthphoto.DecodePixelHeight = 720;
                                                    MainWindow.sixthphoto.EndInit();
                                                    SixthPic.Source = MainWindow.sixthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                SixthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                SixthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                SixthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                SixthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 720;
                                                                Temp5front.EndInit();
                                                                SixthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 720;
                                                                Temp6front.EndInit();
                                                                SixthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 720;
                                                        Temp1front.EndInit();
                                                        FourthPicFront.Source = Temp1front;
                                                    }
                                                    Delay(200);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case 2:
                                    switch (MainWindow.photonum)
                                    {
                                        case 1:
                                        case 2:
                                        case 3:
                                        case 4:
                                            switch (smallimg)
                                            {
                                                case 1:
                                                    MainWindow.firstphoto = new BitmapImage();
                                                    MainWindow.firstphoto.BeginInit();
                                                    MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                    MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                    MainWindow.firstphoto.EndInit();
                                                    FirstPic.Source = MainWindow.firstphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FirstPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FirstPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FirstPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FirstPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FirstPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FirstPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 2:
                                                    MainWindow.secondphoto = new BitmapImage();
                                                    MainWindow.secondphoto.BeginInit();
                                                    MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                    MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                    MainWindow.secondphoto.EndInit();
                                                    SecondPic.Source = MainWindow.secondphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                SecondPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                SecondPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                SecondPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                SecondPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                SecondPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                SecondPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        SecondPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 3:
                                                    MainWindow.thirdphoto = new BitmapImage();
                                                    MainWindow.thirdphoto.BeginInit();
                                                    MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                    MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                    MainWindow.thirdphoto.EndInit();
                                                    ThirdPic.Source = MainWindow.thirdphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                ThirdPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                ThirdPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                ThirdPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                ThirdPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                ThirdPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                ThirdPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        ThirdPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 0:
                                                    MainWindow.fourthphoto = new BitmapImage();
                                                    MainWindow.fourthphoto.BeginInit();
                                                    MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.fourthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.fourthphoto.EndInit();
                                                    FourthPic.Source = MainWindow.fourthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FourthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FourthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FourthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FourthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FourthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FourthPicFront.Source = Temp1front;
                                                    }
                                                    Delay(200);
                                                    break;
                                            }
                                            break;
                                        case 5:
                                            switch (smallimg)
                                            {
                                                case 1:
                                                    MainWindow.firstphoto = new BitmapImage();
                                                    MainWindow.firstphoto.BeginInit();
                                                    MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                    MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                    MainWindow.firstphoto.EndInit();
                                                    FirstPic.Source = MainWindow.firstphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FirstPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FirstPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FirstPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FirstPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FirstPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FirstPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 2:
                                                    MainWindow.secondphoto = new BitmapImage();
                                                    MainWindow.secondphoto.BeginInit();
                                                    MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                    MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                    MainWindow.secondphoto.EndInit();
                                                    SecondPic.Source = MainWindow.secondphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                SecondPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                SecondPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                SecondPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                SecondPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                SecondPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                SecondPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        SecondPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 3:
                                                    MainWindow.thirdphoto = new BitmapImage();
                                                    MainWindow.thirdphoto.BeginInit();
                                                    MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                    MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                    MainWindow.thirdphoto.EndInit();
                                                    ThirdPic.Source = MainWindow.thirdphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                ThirdPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                ThirdPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                ThirdPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                ThirdPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                ThirdPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                ThirdPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        ThirdPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 4:
                                                    MainWindow.fourthphoto = new BitmapImage();
                                                    MainWindow.fourthphoto.BeginInit();
                                                    MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.fourthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.fourthphoto.EndInit();
                                                    FourthPic.Source = MainWindow.fourthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FourthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FourthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FourthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FourthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FourthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FourthPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 0:
                                                    MainWindow.fifthphoto = new BitmapImage();
                                                    MainWindow.fifthphoto.BeginInit();
                                                    MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fifthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.fifthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.fifthphoto.EndInit();
                                                    FifthPic.Source = MainWindow.fifthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FifthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FifthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FifthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FifthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FifthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FifthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FifthPicFront.Source = Temp1front;
                                                    }
                                                    Delay(200);
                                                    break;
                                            }
                                            break;
                                        case 6:
                                            switch (smallimg)
                                            {
                                                case 1:
                                                    MainWindow.firstphoto = new BitmapImage();
                                                    MainWindow.firstphoto.BeginInit();
                                                    MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.firstphoto.DecodePixelWidth = 1080;
                                                    MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                    MainWindow.firstphoto.EndInit();
                                                    FirstPic.Source = MainWindow.firstphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FirstPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FirstPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FirstPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FirstPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FirstPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FirstPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 2:
                                                    MainWindow.secondphoto = new BitmapImage();
                                                    MainWindow.secondphoto.BeginInit();
                                                    MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.secondphoto.DecodePixelWidth = 1080;
                                                    MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                    MainWindow.secondphoto.EndInit();
                                                    SecondPic.Source = MainWindow.secondphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                SecondPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                SecondPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                SecondPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                SecondPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                SecondPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                SecondPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        SecondPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 3:
                                                    MainWindow.thirdphoto = new BitmapImage();
                                                    MainWindow.thirdphoto.BeginInit();
                                                    MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.thirdphoto.DecodePixelWidth = 1080;
                                                    MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                    MainWindow.thirdphoto.EndInit();
                                                    ThirdPic.Source = MainWindow.thirdphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                ThirdPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                ThirdPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                ThirdPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                ThirdPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                ThirdPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                ThirdPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        ThirdPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 4:
                                                    MainWindow.fourthphoto = new BitmapImage();
                                                    MainWindow.fourthphoto.BeginInit();
                                                    MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fourthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.fourthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.fourthphoto.EndInit();
                                                    FourthPic.Source = MainWindow.fourthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FourthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FourthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FourthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FourthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FourthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FourthPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 5:
                                                    MainWindow.fifthphoto = new BitmapImage();
                                                    MainWindow.fifthphoto.BeginInit();
                                                    MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fifthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.fifthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.fifthphoto.EndInit();
                                                    FifthPic.Source = MainWindow.fifthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FifthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FifthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FifthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FifthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FifthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FifthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FifthPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 0:
                                                    MainWindow.sixthphoto = new BitmapImage();
                                                    MainWindow.sixthphoto.BeginInit();
                                                    MainWindow.sixthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.sixthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.sixthphoto.DecodePixelWidth = 1080;
                                                    MainWindow.sixthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.sixthphoto.EndInit();
                                                    SixthPic.Source = MainWindow.sixthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                SixthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                SixthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                SixthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                SixthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                SixthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 1080;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                SixthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 1080;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FifthPicFront.Source = Temp1front;
                                                    }
                                                    Delay(200);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case 3:
                                    switch (MainWindow.photonum)
                                    {
                                        case 1:
                                        case 2:
                                        case 3:
                                        case 4:
                                            switch (smallimg)
                                            {
                                                case 1:
                                                    MainWindow.firstphoto = new BitmapImage();
                                                    MainWindow.firstphoto.BeginInit();
                                                    MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.firstphoto.DecodePixelWidth = 720;
                                                    MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                    MainWindow.firstphoto.EndInit();
                                                    FirstPic.Source = MainWindow.firstphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FirstPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FirstPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FirstPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FirstPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FirstPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FirstPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 2:
                                                    MainWindow.secondphoto = new BitmapImage();
                                                    MainWindow.secondphoto.BeginInit();
                                                    MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.secondphoto.DecodePixelWidth = 720;
                                                    MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                    MainWindow.secondphoto.EndInit();
                                                    SecondPic.Source = MainWindow.secondphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                SecondPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                SecondPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                SecondPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                SecondPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                SecondPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        SecondPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 3:
                                                    MainWindow.thirdphoto = new BitmapImage();
                                                    MainWindow.thirdphoto.BeginInit();
                                                    MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.thirdphoto.DecodePixelWidth = 720;
                                                    MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                    MainWindow.thirdphoto.EndInit();
                                                    ThirdPic.Source = MainWindow.thirdphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                ThirdPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                ThirdPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                ThirdPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                ThirdPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                ThirdPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        ThirdPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 0:
                                                    MainWindow.fourthphoto = new BitmapImage();
                                                    MainWindow.fourthphoto.BeginInit();
                                                    MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fourthphoto.DecodePixelWidth = 720;
                                                    MainWindow.fourthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.fourthphoto.EndInit();
                                                    FourthPic.Source = MainWindow.fourthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FourthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FourthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FourthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FourthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FourthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FourthPicFront.Source = Temp1front;
                                                    }
                                                    Delay(200);
                                                    break;
                                            }
                                            break;
                                        case 5:
                                            switch (smallimg)
                                            {
                                                case 1:
                                                    MainWindow.firstphoto = new BitmapImage();
                                                    MainWindow.firstphoto.BeginInit();
                                                    MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.firstphoto.DecodePixelWidth = 720;
                                                    MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                    MainWindow.firstphoto.EndInit();
                                                    FirstPic.Source = MainWindow.firstphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FirstPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FirstPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FirstPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FirstPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FirstPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FirstPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 2:
                                                    MainWindow.secondphoto = new BitmapImage();
                                                    MainWindow.secondphoto.BeginInit();
                                                    MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.secondphoto.DecodePixelWidth = 720;
                                                    MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                    MainWindow.secondphoto.EndInit();
                                                    SecondPic.Source = MainWindow.secondphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                SecondPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                SecondPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                SecondPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                SecondPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                SecondPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        SecondPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 3:
                                                    MainWindow.thirdphoto = new BitmapImage();
                                                    MainWindow.thirdphoto.BeginInit();
                                                    MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.thirdphoto.DecodePixelWidth = 720;
                                                    MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                    MainWindow.thirdphoto.EndInit();
                                                    ThirdPic.Source = MainWindow.thirdphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                ThirdPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                ThirdPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                ThirdPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                ThirdPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                ThirdPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        ThirdPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 4:
                                                    MainWindow.fourthphoto = new BitmapImage();
                                                    MainWindow.fourthphoto.BeginInit();
                                                    MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fourthphoto.DecodePixelWidth = 720;
                                                    MainWindow.fourthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.fourthphoto.EndInit();
                                                    FourthPic.Source = MainWindow.fourthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FourthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FourthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FourthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FourthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FourthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FourthPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 0:
                                                    MainWindow.fifthphoto = new BitmapImage();
                                                    MainWindow.fifthphoto.BeginInit();
                                                    MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fifthphoto.DecodePixelWidth = 720;
                                                    MainWindow.fifthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.fifthphoto.EndInit();
                                                    FifthPic.Source = MainWindow.fifthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FifthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FifthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FifthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FifthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FifthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FifthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FifthPicFront.Source = Temp1front;
                                                    }
                                                    Delay(200);
                                                    break;
                                            }
                                            break;
                                        case 6:
                                            switch (smallimg)
                                            {
                                                case 1:
                                                    MainWindow.firstphoto = new BitmapImage();
                                                    MainWindow.firstphoto.BeginInit();
                                                    MainWindow.firstphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.firstphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.firstphoto.DecodePixelWidth = 720;
                                                    MainWindow.firstphoto.DecodePixelHeight = 1080;
                                                    MainWindow.firstphoto.EndInit();
                                                    FirstPic.Source = MainWindow.firstphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FirstPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FirstPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FirstPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FirstPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FirstPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-1.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-1.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FirstPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 2:
                                                    MainWindow.secondphoto = new BitmapImage();
                                                    MainWindow.secondphoto.BeginInit();
                                                    MainWindow.secondphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.secondphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.secondphoto.DecodePixelWidth = 720;
                                                    MainWindow.secondphoto.DecodePixelHeight = 1080;
                                                    MainWindow.secondphoto.EndInit();
                                                    SecondPic.Source = MainWindow.secondphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                SecondPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                SecondPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                SecondPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                SecondPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                SecondPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        SecondPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 3:
                                                    MainWindow.thirdphoto = new BitmapImage();
                                                    MainWindow.thirdphoto.BeginInit();
                                                    MainWindow.thirdphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.thirdphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.thirdphoto.DecodePixelWidth = 720;
                                                    MainWindow.thirdphoto.DecodePixelHeight = 1080;
                                                    MainWindow.thirdphoto.EndInit();
                                                    ThirdPic.Source = MainWindow.thirdphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                ThirdPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                ThirdPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                ThirdPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                ThirdPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                ThirdPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FirstPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        ThirdPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 4:
                                                    MainWindow.fourthphoto = new BitmapImage();
                                                    MainWindow.fourthphoto.BeginInit();
                                                    MainWindow.fourthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fourthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fourthphoto.DecodePixelWidth = 720;
                                                    MainWindow.fourthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.fourthphoto.EndInit();
                                                    FourthPic.Source = MainWindow.fourthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FourthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FourthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FourthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FourthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FourthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FourthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FourthPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 5:
                                                    MainWindow.fifthphoto = new BitmapImage();
                                                    MainWindow.fifthphoto.BeginInit();
                                                    MainWindow.fifthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.fifthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.fifthphoto.DecodePixelWidth = 720;
                                                    MainWindow.fifthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.fifthphoto.EndInit();
                                                    FifthPic.Source = MainWindow.fifthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                FifthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                FifthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                FifthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                FifthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                FifthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                FifthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        FifthPicFront.Source = Temp1front;
                                                    }
                                                    break;
                                                case 0:
                                                    MainWindow.sixthphoto = new BitmapImage();
                                                    MainWindow.sixthphoto.BeginInit();
                                                    MainWindow.sixthphoto.UriSource = new Uri(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", UriKind.RelativeOrAbsolute);
                                                    MainWindow.sixthphoto.CacheOption = BitmapCacheOption.OnLoad;
                                                    MainWindow.sixthphoto.DecodePixelWidth = 720;
                                                    MainWindow.sixthphoto.DecodePixelHeight = 1080;
                                                    MainWindow.sixthphoto.EndInit();
                                                    SixthPic.Source = MainWindow.sixthphoto;

                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (TempSelect.temp)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                SixthPicFront.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                SixthPicFront.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                SixthPicFront.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                SixthPicFront.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                SixthPicFront.Source = Temp5front;
                                                                break;
                                                            case 6:
                                                                BitmapImage Temp6front = new BitmapImage();
                                                                Temp6front.BeginInit();
                                                                Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp6front.DecodePixelWidth = 720;
                                                                Temp6front.DecodePixelHeight = 1080;
                                                                Temp6front.EndInit();
                                                                SixthPicFront.Source = Temp6front;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        BitmapImage Temp1front = new BitmapImage();
                                                        Temp1front.BeginInit();
                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                        Temp1front.DecodePixelWidth = 720;
                                                        Temp1front.DecodePixelHeight = 1080;
                                                        Temp1front.EndInit();
                                                        SixthPicFront.Source = Temp1front;
                                                    }
                                                    Delay(200);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                        }

                        if (MainWindow.SorR == "S")
                        {
                            if (MainWindow.inifoldername == "r81" || MainWindow.inifoldername == "r83")
                            {
                                if (FirstPic.Source != null && SixthPic.Source != null)
                                {
                                    nextpage();
                                }
                                else
                                {
                                    count = Convert.ToInt32(MainWindow.photocount.ToString());
                                    tblState.Text = count.ToString();
                                    timer.Start();
                                    Source.Log.log.Info("캐논타이머 시작");
                                }
                            }
                            else if (MainWindow.inifoldername == "r54")
                            {
                                if (FirstPic.Source != null && SixthPic.Source != null)
                                {
                                    nextpage();
                                }
                                else
                                {
                                    count = Convert.ToInt32(MainWindow.photocount.ToString());
                                    tblState.Text = count.ToString();
                                    timer.Start();
                                    Source.Log.log.Info("캐논타이머 시작");
                                }
                            }
                            else if (MainWindow.inifoldername == "r30")
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        if (FirstPic.Source != null && ThirdPic.Source != null)
                                        {
                                            nextpage();
                                        }
                                        else
                                        {
                                            count = Convert.ToInt32(MainWindow.photocount.ToString());
                                            tblState.Text = count.ToString();
                                            timer.Start();
                                            Source.Log.log.Info("캐논타이머 시작");
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                    case 4:
                                        if (FirstPic.Source != null && FourthPic.Source != null)
                                        {
                                            nextpage();
                                        }
                                        else
                                        {
                                            count = Convert.ToInt32(MainWindow.photocount.ToString());
                                            tblState.Text = count.ToString();
                                            timer.Start();
                                            Source.Log.log.Info("캐논타이머 시작");
                                        }
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "r28")
                            {
                                if (MainWindow.SorR == "S")
                                {
                                    switch (TempSelect.temp)
                                    {
                                        case 1:
                                            if (FirstPic.Source != null && SixthPic.Source != null)
                                            {
                                                nextpage();
                                            }
                                            else
                                            {
                                                switch (photonum)
                                                {
                                                    case 1:
                                                        BitmapImage pic1 = new BitmapImage();
                                                        pic1.BeginInit();
                                                        pic1.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                        pic1.DecodePixelWidth = 1080;
                                                        pic1.DecodePixelHeight = 1080;
                                                        pic1.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic1.EndInit();

                                                        frontlvimg.Source = pic1;
                                                        break;
                                                    case 2:
                                                        BitmapImage pic2 = new BitmapImage();
                                                        pic2.BeginInit();
                                                        pic2.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                        pic2.DecodePixelWidth = 1080;
                                                        pic2.DecodePixelHeight = 1080;
                                                        pic2.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic2.EndInit();

                                                        frontlvimg.Source = pic2;
                                                        break;
                                                    case 3:
                                                        BitmapImage pic3 = new BitmapImage();
                                                        pic3.BeginInit();
                                                        pic3.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                        pic3.DecodePixelWidth = 1080;
                                                        pic3.DecodePixelHeight = 1080;
                                                        pic3.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic3.EndInit();

                                                        frontlvimg.Source = pic3;
                                                        break;
                                                    case 4:
                                                        BitmapImage pic4 = new BitmapImage();
                                                        pic4.BeginInit();
                                                        pic4.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                        pic4.DecodePixelWidth = 1080;
                                                        pic4.DecodePixelHeight = 1080;
                                                        pic4.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic4.EndInit();

                                                        frontlvimg.Source = pic4;
                                                        break;
                                                    case 5:
                                                        BitmapImage pic5 = new BitmapImage();
                                                        pic5.BeginInit();
                                                        pic5.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                        pic5.DecodePixelWidth = 1080;
                                                        pic5.DecodePixelHeight = 1080;
                                                        pic5.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic5.EndInit();

                                                        frontlvimg.Source = pic5;
                                                        break;
                                                }
                                                count = Convert.ToInt32(MainWindow.photocount.ToString());
                                                tblState.Text = count.ToString();
                                                timer.Start();
                                                Source.Log.log.Info("캐논타이머 시작");
                                            }
                                            break;
                                        case 2:
                                            if (FirstPic.Source != null && SixthPic.Source != null)
                                            {
                                                nextpage();
                                            }
                                            else
                                            {
                                                switch (photonum)
                                                {
                                                    case 1:
                                                        BitmapImage pic1 = new BitmapImage();
                                                        pic1.BeginInit();
                                                        pic1.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                        pic1.DecodePixelWidth = 1080;
                                                        pic1.DecodePixelHeight = 720;
                                                        pic1.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic1.EndInit();

                                                        frontlvimg.Source = pic1;
                                                        break;
                                                    case 2:
                                                        BitmapImage pic2 = new BitmapImage();
                                                        pic2.BeginInit();
                                                        pic2.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                        pic2.DecodePixelWidth = 1080;
                                                        pic2.DecodePixelHeight = 720;
                                                        pic2.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic2.EndInit();

                                                        frontlvimg.Source = pic2;
                                                        break;
                                                    case 3:
                                                        BitmapImage pic3 = new BitmapImage();
                                                        pic3.BeginInit();
                                                        pic3.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                        pic3.DecodePixelWidth = 1080;
                                                        pic3.DecodePixelHeight = 720;
                                                        pic3.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic3.EndInit();

                                                        frontlvimg.Source = pic3;
                                                        break;
                                                    case 4:
                                                        BitmapImage pic4 = new BitmapImage();
                                                        pic4.BeginInit();
                                                        pic4.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                        pic4.DecodePixelWidth = 1080;
                                                        pic4.DecodePixelHeight = 720;
                                                        pic4.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic4.EndInit();

                                                        frontlvimg.Source = pic4;
                                                        break;
                                                    case 5:
                                                        BitmapImage pic5 = new BitmapImage();
                                                        pic5.BeginInit();
                                                        pic5.UriSource = new Uri(MainWindow.TempPath + @"\2-6.png", UriKind.RelativeOrAbsolute);
                                                        pic5.DecodePixelWidth = 1080;
                                                        pic5.DecodePixelHeight = 720;
                                                        pic5.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic5.EndInit();

                                                        frontlvimg.Source = pic5;
                                                        break;
                                                }
                                                count = Convert.ToInt32(MainWindow.photocount.ToString());
                                                tblState.Text = count.ToString();
                                                timer.Start();
                                                Source.Log.log.Info("캐논타이머 시작");
                                            }
                                            break;
                                        case 3:
                                            if (FirstPic.Source != null && FifthPic.Source != null)
                                            {
                                                nextpage();
                                            }
                                            else
                                            {
                                                switch (photonum)
                                                {
                                                    case 1:
                                                        BitmapImage pic1 = new BitmapImage();
                                                        pic1.BeginInit();
                                                        pic1.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                        pic1.DecodePixelWidth = 1080;
                                                        pic1.DecodePixelHeight = 720;
                                                        pic1.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic1.EndInit();

                                                        frontlvimg.Source = pic1;
                                                        break;
                                                    case 2:
                                                        BitmapImage pic2 = new BitmapImage();
                                                        pic2.BeginInit();
                                                        pic2.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                        pic2.DecodePixelWidth = 1080;
                                                        pic2.DecodePixelHeight = 720;
                                                        pic2.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic2.EndInit();

                                                        frontlvimg.Source = pic2;
                                                        break;
                                                    case 3:
                                                        BitmapImage pic3 = new BitmapImage();
                                                        pic3.BeginInit();
                                                        pic3.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                        pic3.DecodePixelWidth = 1080;
                                                        pic3.DecodePixelHeight = 720;
                                                        pic3.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic3.EndInit();

                                                        frontlvimg.Source = pic3;
                                                        break;
                                                    case 4:
                                                        BitmapImage pic4 = new BitmapImage();
                                                        pic4.BeginInit();
                                                        pic4.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                        pic4.DecodePixelWidth = 1080;
                                                        pic4.DecodePixelHeight = 720;
                                                        pic4.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic4.EndInit();

                                                        frontlvimg.Source = pic4;
                                                        break;
                                                }
                                                count = Convert.ToInt32(MainWindow.photocount.ToString());
                                                tblState.Text = count.ToString();
                                                timer.Start();
                                                Source.Log.log.Info("캐논타이머 시작");
                                            }
                                            break;
                                        case 4:
                                            if (FirstPic.Source != null && FourthPic.Source != null)
                                            {
                                                nextpage();
                                            }
                                            else
                                            {
                                                switch (photonum)
                                                {
                                                    case 1:
                                                        BitmapImage pic1 = new BitmapImage();
                                                        pic1.BeginInit();
                                                        pic1.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                        pic1.DecodePixelWidth = 1080;
                                                        pic1.DecodePixelHeight = 720;
                                                        pic1.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic1.EndInit();

                                                        frontlvimg.Source = pic1;
                                                        break;
                                                    case 2:
                                                        BitmapImage pic2 = new BitmapImage();
                                                        pic2.BeginInit();
                                                        pic2.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                        pic2.DecodePixelWidth = 1080;
                                                        pic2.DecodePixelHeight = 720;
                                                        pic2.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic2.EndInit();

                                                        frontlvimg.Source = pic2;
                                                        break;
                                                    case 3:
                                                        BitmapImage pic3 = new BitmapImage();
                                                        pic3.BeginInit();
                                                        pic3.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                        pic3.DecodePixelWidth = 1080;
                                                        pic3.DecodePixelHeight = 720;
                                                        pic3.CacheOption = BitmapCacheOption.OnLoad;
                                                        pic3.EndInit();

                                                        frontlvimg.Source = pic3;
                                                        break;
                                                }
                                                count = Convert.ToInt32(MainWindow.photocount.ToString());
                                                tblState.Text = count.ToString();
                                                timer.Start();
                                                Source.Log.log.Info("캐논타이머 시작");
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            switch (MainWindow.photonum)
                            {
                                case 4:
                                    if (FirstPic.Source != null && FourthPic.Source != null)
                                    {
                                        nextpage();
                                    }
                                    else
                                    {
                                        if (MainWindow.SorR == "S")
                                        {
                                            if (MainWindow.inifoldername == "r80")
                                            {
                                                switch (TempSelect.temp)
                                                {
                                                    case 1:
                                                    case 2:
                                                    case 3:
                                                    case 4:
                                                    case 5:
                                                    case 6:
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                        break;

                                                }
                                            }
                                        }
                                        else
                                        {
                                            switch (MainWindow.picratio)
                                            {
                                                case 1:
                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                frontlvimg.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                frontlvimg.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                frontlvimg.Source = Temp3front;
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                frontlvimg.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                frontlvimg.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                frontlvimg.Source = Temp3front;
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                frontlvimg.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                frontlvimg.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                frontlvimg.Source = Temp3front;
                                                                break;
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        count = Convert.ToInt32(MainWindow.photocount.ToString());
                                        tblState.Text = count.ToString();
                                        timer.Start();
                                        Source.Log.log.Info("캐논타이머 시작");
                                    }
                                    break;
                                case 5:
                                    if (FirstPic.Source != null && FifthPic.Source != null)
                                    {
                                        nextpage();
                                    }
                                    else
                                    {
                                        if (MainWindow.SorR == "S")
                                        {
                                            if (MainWindow.inifoldername == "r80")
                                            {
                                                switch (TempSelect.temp)
                                                {
                                                    case 1:
                                                    case 2:
                                                    case 3:
                                                    case 4:
                                                    case 5:
                                                    case 6:
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                        break;

                                                }
                                            }
                                        }
                                        else
                                        {
                                            switch (MainWindow.picratio)
                                            {
                                                case 1:
                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 4:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                frontlvimg.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                frontlvimg.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                frontlvimg.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                frontlvimg.Source = Temp4front;
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 4:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 5:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                frontlvimg.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                frontlvimg.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                frontlvimg.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                frontlvimg.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                frontlvimg.Source = Temp5front;
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 4:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 5:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                frontlvimg.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                frontlvimg.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                frontlvimg.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                frontlvimg.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                frontlvimg.Source = Temp5front;
                                                                break;
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        count = Convert.ToInt32(MainWindow.photocount.ToString());
                                        tblState.Text = count.ToString();
                                        timer.Start();
                                        Source.Log.log.Info("캐논타이머 시작");
                                    }
                                    break;
                                case 6:
                                    if (FirstPic.Source != null && SixthPic.Source != null)
                                    {
                                        nextpage();
                                    }
                                    else
                                    {
                                        if (MainWindow.SorR == "S")
                                        {
                                            if (MainWindow.inifoldername == "r80")
                                            {
                                                switch (TempSelect.temp)
                                                {
                                                    case 1:
                                                    case 2:
                                                    case 3:
                                                    case 4:
                                                    case 5:
                                                    case 6:
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                        break;

                                                }
                                            }
                                        }
                                        else
                                        {
                                            switch (MainWindow.picratio)
                                            {
                                                case 1:
                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 4:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 720;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 720;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 720;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 720;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 720;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 720;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 720;
                                                                Temp1front.EndInit();
                                                                frontlvimg.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 720;
                                                                Temp2front.EndInit();
                                                                frontlvimg.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 720;
                                                                Temp3front.EndInit();
                                                                frontlvimg.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 720;
                                                                Temp4front.EndInit();
                                                                frontlvimg.Source = Temp4front;
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 4:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 5:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 1080;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 1080;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 1080;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 1080;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 1080;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 1080;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 1080;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                frontlvimg.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 1080;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                frontlvimg.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 1080;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                frontlvimg.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 1080;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                frontlvimg.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 1080;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                frontlvimg.Source = Temp5front;
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    if (MainWindow.tempoption == 1)
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-2.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 2:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-3.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 3:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-4.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 4:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-5.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                            case 5:
                                                                switch (TempSelect.temp)
                                                                {
                                                                    case 1:
                                                                        BitmapImage Temp1front = new BitmapImage();
                                                                        Temp1front.BeginInit();
                                                                        Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp1front.DecodePixelWidth = 720;
                                                                        Temp1front.DecodePixelHeight = 1080;
                                                                        Temp1front.EndInit();
                                                                        frontlvimg.Source = Temp1front;
                                                                        break;
                                                                    case 2:
                                                                        BitmapImage Temp2front = new BitmapImage();
                                                                        Temp2front.BeginInit();
                                                                        Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\2-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp2front.DecodePixelWidth = 720;
                                                                        Temp2front.DecodePixelHeight = 1080;
                                                                        Temp2front.EndInit();
                                                                        frontlvimg.Source = Temp2front;
                                                                        break;
                                                                    case 3:
                                                                        BitmapImage Temp3front = new BitmapImage();
                                                                        Temp3front.BeginInit();
                                                                        Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\3-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp3front.DecodePixelWidth = 720;
                                                                        Temp3front.DecodePixelHeight = 1080;
                                                                        Temp3front.EndInit();
                                                                        frontlvimg.Source = Temp3front;
                                                                        break;
                                                                    case 4:
                                                                        BitmapImage Temp4front = new BitmapImage();
                                                                        Temp4front.BeginInit();
                                                                        Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\4-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp4front.DecodePixelWidth = 720;
                                                                        Temp4front.DecodePixelHeight = 1080;
                                                                        Temp4front.EndInit();
                                                                        frontlvimg.Source = Temp4front;
                                                                        break;
                                                                    case 5:
                                                                        BitmapImage Temp5front = new BitmapImage();
                                                                        Temp5front.BeginInit();
                                                                        Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\5-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp5front.DecodePixelWidth = 720;
                                                                        Temp5front.DecodePixelHeight = 1080;
                                                                        Temp5front.EndInit();
                                                                        frontlvimg.Source = Temp5front;
                                                                        break;
                                                                    case 6:
                                                                        BitmapImage Temp6front = new BitmapImage();
                                                                        Temp6front.BeginInit();
                                                                        Temp6front.UriSource = new Uri(MainWindow.TempPath + @"\6-6.png", UriKind.RelativeOrAbsolute);
                                                                        Temp6front.CacheOption = BitmapCacheOption.OnLoad;
                                                                        Temp6front.DecodePixelWidth = 720;
                                                                        Temp6front.DecodePixelHeight = 1080;
                                                                        Temp6front.EndInit();
                                                                        frontlvimg.Source = Temp6front;
                                                                        break;
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (photonum)
                                                        {
                                                            case 1:
                                                                BitmapImage Temp1front = new BitmapImage();
                                                                Temp1front.BeginInit();
                                                                Temp1front.UriSource = new Uri(MainWindow.TempPath + @"\1-2.png", UriKind.RelativeOrAbsolute);
                                                                Temp1front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp1front.DecodePixelWidth = 720;
                                                                Temp1front.DecodePixelHeight = 1080;
                                                                Temp1front.EndInit();
                                                                frontlvimg.Source = Temp1front;
                                                                break;
                                                            case 2:
                                                                BitmapImage Temp2front = new BitmapImage();
                                                                Temp2front.BeginInit();
                                                                Temp2front.UriSource = new Uri(MainWindow.TempPath + @"\1-3.png", UriKind.RelativeOrAbsolute);
                                                                Temp2front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp2front.DecodePixelWidth = 720;
                                                                Temp2front.DecodePixelHeight = 1080;
                                                                Temp2front.EndInit();
                                                                frontlvimg.Source = Temp2front;
                                                                break;
                                                            case 3:
                                                                BitmapImage Temp3front = new BitmapImage();
                                                                Temp3front.BeginInit();
                                                                Temp3front.UriSource = new Uri(MainWindow.TempPath + @"\1-4.png", UriKind.RelativeOrAbsolute);
                                                                Temp3front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp3front.DecodePixelWidth = 720;
                                                                Temp3front.DecodePixelHeight = 1080;
                                                                Temp3front.EndInit();
                                                                frontlvimg.Source = Temp3front;
                                                                break;
                                                            case 4:
                                                                BitmapImage Temp4front = new BitmapImage();
                                                                Temp4front.BeginInit();
                                                                Temp4front.UriSource = new Uri(MainWindow.TempPath + @"\1-5.png", UriKind.RelativeOrAbsolute);
                                                                Temp4front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp4front.DecodePixelWidth = 720;
                                                                Temp4front.DecodePixelHeight = 1080;
                                                                Temp4front.EndInit();
                                                                frontlvimg.Source = Temp4front;
                                                                break;
                                                            case 5:
                                                                BitmapImage Temp5front = new BitmapImage();
                                                                Temp5front.BeginInit();
                                                                Temp5front.UriSource = new Uri(MainWindow.TempPath + @"\1-6.png", UriKind.RelativeOrAbsolute);
                                                                Temp5front.CacheOption = BitmapCacheOption.OnLoad;
                                                                Temp5front.DecodePixelWidth = 720;
                                                                Temp5front.DecodePixelHeight = 1080;
                                                                Temp5front.EndInit();
                                                                frontlvimg.Source = Temp5front;
                                                                break;
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        count = Convert.ToInt32(MainWindow.photocount.ToString());
                                        tblState.Text = count.ToString();
                                        timer.Start();
                                        Source.Log.log.Info("캐논타이머 시작");
                                    }
                                    break;
                            }
                        }
                    }
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

        #endregion

        #region /// Delay ///

        private static DateTime Delay(int MS)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 딜레이 시작 딜레이 시간(단위 : ms) : " + MS);

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

        #endregion

        #region /// 사진 endcoding ///

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
            // 모든 이미지 포맷의 이미지 코덱 가져오기
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // 지정된 MIME 유형과 일치하는 코덱 찾기
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == mimeType)
                {
                    return codec;
                }
            }

            return null;
        }

        #endregion

        #region /// 사진 사이즈 조절 ///

        private void CutImg(string path)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작 | 이미지 위치 : " + path);
                Mat src = new Mat(path, ImreadModes.Color);
                Mat dst = new Mat();

                if (MainWindow.SorR == "S")
                {
                    if (MainWindow.inifoldername == "r80")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                dst = src.SubMat(new OpenCvSharp.Rect((src.Width - (src.Height / 2 * 3)) / 2, 0, src.Height / 2 * 3, src.Height));
                                break;
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                dst = src.SubMat(new OpenCvSharp.Rect((src.Width - (src.Height / 2 * 3)) / 2, 0, src.Height / 2 * 3, src.Height));
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "r81" || MainWindow.inifoldername == "r83")
                    {
                        dst = src.SubMat(new OpenCvSharp.Rect((src.Width - (src.Height / 2 * 3)) / 2, 0, src.Height / 2 * 3, src.Height));
                    }
                    else if (MainWindow.inifoldername == "r30")
                    {
                        dst = src.SubMat(new OpenCvSharp.Rect((src.Width - (src.Height / 2 * 3)) / 2, 0, src.Height / 2 * 3, src.Height));
                    }
                    else if (MainWindow.inifoldername == "r28")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                            case 3:
                            case 4:
                                dst = src.SubMat(new OpenCvSharp.Rect((src.Width - src.Height) / 2, 0, src.Height, src.Height));
                                break;
                            case 2:
                                dst = src.SubMat(new OpenCvSharp.Rect((src.Width - (src.Height / 2 * 3)) / 2, 0, src.Height / 2 * 3, src.Height));
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "r54")
                    {
                        dst = src.SubMat(new OpenCvSharp.Rect((src.Width - (src.Height / 2 * 3)) / 2, 0, src.Height / 2 * 3, src.Height));
                    }
                }
                else
                {
                    switch (MainWindow.picratio)
                    {
                        case 1:
                            dst = src.SubMat(new OpenCvSharp.Rect((src.Width - (src.Height / 2 * 3)) / 2, 0, src.Height / 2 * 3, src.Height));
                            break;
                        case 2:
                            dst = src.SubMat(new OpenCvSharp.Rect((src.Width - src.Height) / 2, 0, src.Height, src.Height));
                            break;
                        case 3:
                            dst = src.SubMat(new OpenCvSharp.Rect((src.Width - (src.Height / 3 * 2)) / 2, 0, src.Height / 3 * 2, src.Height));
                            break;
                    }
                }

                Cv2.ImWrite(MainWindow.ResizePath + @"\RPhoto" + photonumber + ".JPG", dst);

                dst.Dispose();
                src.Dispose();
                dst = null;
                src = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #endregion

        #region /// 리소스 정리 ///

        private async void nextpage()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                isRecording = false;

                if (MainWindow.checkvideo == "1")
                {
                    recordthread.Abort();
                    writer.Dispose();
                    recordthread = null;
                    image.Dispose();
                }
                checkvideothread = false;

                videothread.Abort();
                timer.Tick -= new EventHandler(Timer_Tik);

                Delay(2000);
                frame.Dispose();
                flipframe.Dispose();
                bitmap.Dispose();
                savebitmap.Dispose();
                videothread = null;

                FirstPic.Source = null;
                SecondPic.Source = null;
                ThirdPic.Source = null;
                FourthPic.Source = null;
                FirstPicFront.Source = null;
                SecondPicFront.Source = null;
                ThirdPicFront.Source = null;
                FourthPicFront.Source = null;
                frontlvimg.Source = null;

                FirstPic = null;
                SecondPic = null;
                ThirdPic = null;
                FourthPic = null;
                FirstPicFront = null;
                SecondPicFront = null;
                ThirdPicFront = null;
                FourthPicFront = null;
                frontlvimg = null;
                if (LiveView != null)
                {
                    LiveView.Source = null;
                }
                backimg = null;
                tblState.Text = null;
                tblState = null;
                if (MainWindow.SorR == "S")
                {
                    if (MainWindow.inifoldername == "r81" || MainWindow.inifoldername == "r83")
                    {
                        MainWindow.fifthphoto = null;
                        MainWindow.sixthphoto = null;
                    }
                    else if (MainWindow.inifoldername == "r80")
                    {
                        if (TempSelect.temp == 1)
                        {
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
                        }
                        else
                        {
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
                            MainWindow.fifthphoto = null;
                            MainWindow.sixthphoto = null;
                        }
                    }
                    else if (MainWindow.inifoldername == "r30")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                MainWindow.firstphoto = null;
                                MainWindow.secondphoto = null;
                                MainWindow.thirdphoto = null;
                                break;
                            case 2:
                            case 3:
                            case 4:
                                MainWindow.firstphoto = null;
                                MainWindow.secondphoto = null;
                                MainWindow.thirdphoto = null;
                                MainWindow.fourthphoto = null;
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "r28")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                            case 2:
                                MainWindow.firstphoto = null;
                                MainWindow.secondphoto = null;
                                MainWindow.thirdphoto = null;
                                MainWindow.fourthphoto = null;
                                MainWindow.fifthphoto = null;
                                MainWindow.sixthphoto = null;
                                break;
                            case 3:
                                MainWindow.firstphoto = null;
                                MainWindow.secondphoto = null;
                                MainWindow.thirdphoto = null;
                                MainWindow.fourthphoto = null;
                                MainWindow.fifthphoto = null;
                                break;
                            case 4:
                                MainWindow.firstphoto = null;
                                MainWindow.secondphoto = null;
                                MainWindow.thirdphoto = null;
                                MainWindow.fourthphoto = null;
                                break;
                        }
                    }
                }
                else
                {
                    MainWindow.firstphoto = null;
                    MainWindow.secondphoto = null;
                    MainWindow.thirdphoto = null;
                    MainWindow.fourthphoto = null;
                    MainWindow.fifthphoto = null;
                    MainWindow.sixthphoto = null;
                }


                timer = null;

                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #endregion

        #region /// 비디오 녹화 ///

        private void StartRecord()
        {
            try
            {
                string outputpath = MainWindow.Videopath + "\\" + "Video_" + videoname + ".mp4";
                var outputsize = new OpenCvSharp.Size(width, height);
                using (writer = new VideoWriter(outputpath, FourCC.H264, 60, outputsize, true))
                {
                    image = new Mat();

                    while (isRecording)
                    {
                        MainWindow.video.Read(image);

                        croppedimage = new Mat();

                        switch (MainWindow.picratio)
                        {
                            case 1:
                                croppedimage = image.SubMat(new OpenCvSharp.Rect((image.Width - image.Height / 2 * 3) / 2, 0, image.Height / 2 * 3, image.Height));
                                break;
                            case 2:
                                croppedimage = image.SubMat(new OpenCvSharp.Rect((image.Width - image.Height) / 2, 0, image.Height, image.Height));
                                break;
                            case 3:
                                croppedimage = image.SubMat(new OpenCvSharp.Rect((image.Width - image.Height / 3 * 2) / 2, 0, image.Height / 3 * 2, image.Height));
                                break;
                        }
                        Cv2.Resize(croppedimage, croppedimage, new OpenCvSharp.Size(width, height));

                        if (MainWindow.checkingflip == "NoUsing")
                        {
                            Cv2.Flip(croppedimage, croppedimage, FlipMode.Y);
                        }

                        writer.Write(croppedimage);
                    }
                    writer.Release();
                }

                Console.WriteLine("녹화 시작");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #endregion
    }
}
