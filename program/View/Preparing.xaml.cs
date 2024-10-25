using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using System.Windows.Threading;
using OpenCvSharp;
using System.Reflection;
using System.IO;
using QRCoder;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Net;
using System.Runtime.InteropServices;

namespace wpfTest.View
{
    /// <summary>
    /// Preparing.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Preparing : Page
    {
        #region///ini import///

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        DispatcherTimer timer = new DispatcherTimer();
        int duration = 1;
        string outputfilepath;

        string photonumber = "";
        string photonumber1 = "";
        string photonumber2 = "";
        string photonumber3 = "";
        string photonumber4 = "";

        int photo1;
        int photo2;
        int photo3;
        int photo4;
        int photo5;
        int photo6;
        int photo7;
        int photo8;

        System.Drawing.Image gray1;
        System.Drawing.Image gray2;
        System.Drawing.Image gray3;
        System.Drawing.Image gray4;

        int papercount;
        int pagecount;
        public string correctedPrintername { get; private set; }

        public Preparing()
        {
            Source.Log.log.Info("Preparing 페이지 진입");
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 흑백이미지 제작");

                backimg.Source = MainWindow.preparing;

                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_Tick);
                timer.Start();

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch(Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Pic(string photo, string gray)
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 이미지 변환 시작");
                Mat src = new Mat();
                Mat dst;

                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                }
                else
                {
                    if (MainWindow.SorR == "R")
                    {
                        switch (MainWindow.camnumber.ToString())
                        {
                            case "1":
                                if (MainWindow.checkingflip == "Using")
                                {
                                    src = Cv2.ImRead(MainWindow.PhotoPath + photo, ImreadModes.Color);
                                }
                                else
                                {
                                    src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                                }
                                break;
                            case "2":
                                src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                                break;
                        }
                    }
                    else
                    {
                        if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                                    break;
                                case 2:
                                    src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                                    break;
                                case 3:
                                    src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                                    break;
                            }
                        }
                        else if (MainWindow.inifoldername == "tech")
                        {
                            src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                        }
                        else if (MainWindow.inifoldername == "r80")
                        {
                            src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                        }
                        else if (MainWindow.inifoldername == "r81" || MainWindow.inifoldername == "r83")
                        {
                            src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                        }
                        else if (MainWindow.inifoldername == "r24" || MainWindow.inifoldername == "r25")
                        {
                            src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                        }
                        else if (MainWindow.inifoldername == "r30")
                        {
                            src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                        }
                        else if (MainWindow.inifoldername == "r28")
                        {
                            src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                        }
                        else
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    if (MainWindow.checkingflip == "Using")
                                    {
                                        src = Cv2.ImRead(MainWindow.PhotoPath + photo, ImreadModes.Color);
                                    }
                                    else
                                    {
                                        src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                                    }
                                    break;
                                case 2:
                                    if (MainWindow.checkingflip == "Using")
                                    {
                                        src = Cv2.ImRead(MainWindow.PhotoPath + photo, ImreadModes.Color);
                                    }
                                    else
                                    {
                                        src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                                    }
                                    break;
                                case 3:
                                    src = Cv2.ImRead(MainWindow.ResizePath + photo, ImreadModes.Color);
                                    break;
                            }
                        }
                    }
                }

                dst = new Mat(src.Width, src.Height, MatType.CV_8UC1);
                
                Cv2.CvtColor(src, dst, ColorConversionCodes.BGR2GRAY);
                Delay(100);
                dst.SaveImage(MainWindow.GrayFilterPath + gray);


                src.Dispose();
                dst.Dispose();
                src = null;
                dst = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (OpenCvSharpException opencvex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + opencvex.Message);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void NextPage()
        {
            try
            {
                timer.Tick -= new EventHandler(Timer_Tick);
                timer = null;

                try
                {
                    DirectoryInfo sns = new DirectoryInfo(MainWindow.SnsPath);
                    foreach (FileInfo file in sns.EnumerateFiles())
                    {
                        file.Delete();
                        Source.Log.log.Debug("Kakao 폴더 안 이미지 삭제 완료");
                    }
                }
                catch (Exception delex)
                {
                    Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + delex.Message);
                }

                if (MainWindow.inifoldername == "hhh")
                {

                    if (MainWindow.checkcolor == "black")
                    {
                        Imgshow();
                        print();
                    }
                    NavigationService.Navigate(new Uri("View/Kakao.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    NavigationService.Navigate(new Uri("View/ImgCompose.xaml", UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }


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

        private async void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                duration--;
                if (duration == 0)
                {
                    timer.Stop();
                    int photonum = 0;
                    if (MainWindow.SorR == "R")
                    {
                        switch (MainWindow.camnumber.ToString())
                        {
                            case "1":
                                photonum = TakePic.photonum;
                                break;
                            case "2":
                                photonum = WebCam.photonum;
                                break;
                        }

                        switch (MainWindow.photonum)
                        {
                            case 1:
                                photo1 = photonum;

                                switch (MainWindow.picratio)
                                {
                                    case 1:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                break;
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                        break;
                                }
                                break;
                            case 2:
                                photo1 = photonum - 1;
                                photo2 = photonum;

                                switch (MainWindow.picratio)
                                {
                                    case 1:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                break;
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray2.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                break;
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                        break;
                                }
                                break;
                            case 3:
                                photo1 = photonum - 2;
                                photo2 = photonum - 1;
                                photo3 = photonum;

                                switch (MainWindow.picratio)
                                {
                                    case 1:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                break;
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray2.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                break;
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray3.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                                break;
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                        break;
                                }
                                break;
                            case 4:
                                photo1 = photonum - 3;
                                photo2 = photonum - 2;
                                photo3 = photonum - 1;
                                photo4 = photonum;

                                switch (MainWindow.picratio)
                                {
                                    case 1:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                break;
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray2.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                break;
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray3.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                                break;
                                        }

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray4.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                                break;
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                        break;
                                }
                                break;
                            case 5:
                                photo1 = photonum - 4;
                                photo2 = photonum - 3;
                                photo3 = photonum - 2;
                                photo4 = photonum - 1;
                                photo5 = photonum;

                                switch (MainWindow.picratio)
                                {
                                    case 1:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                break;
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray2.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                break;
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray3.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                                break;
                                        }

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray4.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                                break;
                                        }

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray5.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");
                                                break;
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");
                                        break;
                                }
                                break;
                            case 6:
                                photo1 = photonum - 5;
                                photo2 = photonum - 4;
                                photo3 = photonum - 3;
                                photo4 = photonum - 2;
                                photo5 = photonum - 1;
                                photo6 = photonum;

                                switch (MainWindow.picratio)
                                {
                                    case 1:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                break;
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray2.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                break;
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray3.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                                break;
                                        }

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray4.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                                break;
                                        }

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray5.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");
                                                break;
                                        }

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                if (MainWindow.checkingflip == "Using")
                                                {
                                                    Pic(@"\IMG_" + photonumber + ".JPG", @"\gray6.jpg");
                                                }
                                                else
                                                {
                                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                                }
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                                break;
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        photonum = TakePic.photonum;
                        if (MainWindow.inifoldername.Contains("mediagram"))
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    photo1 = photonum - 3;
                                    photo2 = photonum - 2;
                                    photo3 = photonum - 1;
                                    photo4 = photonum;

                                    if (photo1 > 999)
                                    {
                                        photonumber = Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 1000 && photo1 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 100 && photo1 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo1);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                    if (photo2 > 999)
                                    {
                                        photonumber = Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 1000 && photo2 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 100 && photo2 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo2);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                    if (photo3 > 999)
                                    {
                                        photonumber = Convert.ToString(photo3);
                                    }
                                    else if (photo3 < 1000 && photo3 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo3);
                                    }
                                    else if (photo3 < 100 && photo3 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo3);
                                    }
                                    else if (photo3 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo3);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                    if (photo4 > 999)
                                    {
                                        photonumber = Convert.ToString(photo4);
                                    }
                                    else if (photo4 < 1000 && photo4 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo4);
                                    }
                                    else if (photo4 < 100 && photo4 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo4);
                                    }
                                    else if (photo4 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo4);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                    break;
                                case 2:
                                    photo1 = photonum - 1;
                                    photo2 = photonum;

                                    if (photo1 > 999)
                                    {
                                        photonumber = Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 1000 && photo1 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 100 && photo1 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo1);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                    if (photo2 > 999)
                                    {
                                        photonumber = Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 1000 && photo2 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 100 && photo2 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo2);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                    break;
                                case 3:
                                    photo1 = photonum - 5;
                                    photo2 = photonum - 4;
                                    photo3 = photonum - 3;
                                    photo4 = photonum - 2;
                                    photo5 = photonum - 1;
                                    photo6 = photonum;

                                    if (photo1 > 999)
                                    {
                                        photonumber = Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 1000 && photo1 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 100 && photo1 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo1);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                    if (photo2 > 999)
                                    {
                                        photonumber = Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 1000 && photo2 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 100 && photo2 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo2);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                    if (photo3 > 999)
                                    {
                                        photonumber = Convert.ToString(photo3);
                                    }
                                    else if (photo3 < 1000 && photo3 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo3);
                                    }
                                    else if (photo3 < 100 && photo3 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo3);
                                    }
                                    else if (photo3 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo3);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                    if (photo4 > 999)
                                    {
                                        photonumber = Convert.ToString(photo4);
                                    }
                                    else if (photo4 < 1000 && photo4 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo4);
                                    }
                                    else if (photo4 < 100 && photo4 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo4);
                                    }
                                    else if (photo4 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo4);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                    if (photo5 > 999)
                                    {
                                        photonumber = Convert.ToString(photo5);
                                    }
                                    else if (photo5 < 1000 && photo5 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo5);
                                    }
                                    else if (photo5 < 100 && photo5 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo5);
                                    }
                                    else if (photo5 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo5);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                    if (photo6 > 999)
                                    {
                                        photonumber = Convert.ToString(photo6);
                                    }
                                    else if (photo6 < 1000 && photo6 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo6);
                                    }
                                    else if (photo6 < 100 && photo6 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo6);
                                    }
                                    else if (photo6 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo6);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                    break;
                            }
                        }
                        else
                        {
                            if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        photo1 = photonum - 3;
                                        photo2 = photonum - 2;
                                        photo3 = photonum - 1;
                                        photo4 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                        break;
                                    case 2:
                                    case 3:
                                        photo1 = photonum - 5;
                                        photo2 = photonum - 4;
                                        photo3 = photonum - 3;
                                        photo4 = photonum - 2;
                                        photo5 = photonum - 1;
                                        photo6 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "dearpic2")
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        photo1 = photonum - 2;
                                        photo2 = photonum - 1;
                                        photo3 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber1 = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber1 = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber1 = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber1 = "000" + Convert.ToString(photo1);
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber2 = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber2 = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber2 = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber2 = "000" + Convert.ToString(photo2);
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber3 = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber3 = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber3 = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber3 = "000" + Convert.ToString(photo3);
                                        }

                                        switch (SelectPic.firstselect)
                                        {
                                            case 1:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber1 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber1 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                break;
                                            case 2:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber2 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber2 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                break;
                                            case 3:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber3 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber3 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                break;
                                        }
                                        break;
                                    case 2:
                                        photo1 = photonum - 3;
                                        photo2 = photonum - 2;
                                        photo3 = photonum - 1;
                                        photo4 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber1 = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber1 = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber1 = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber1 = "000" + Convert.ToString(photo1);
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber2 = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber2 = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber2 = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber2 = "000" + Convert.ToString(photo2);
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber3 = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber3 = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber3 = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber3 = "000" + Convert.ToString(photo3);
                                        }

                                        if (photo4 > 999)
                                        {
                                            photonumber4 = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber4 = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber4 = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber4 = "000" + Convert.ToString(photo4);
                                        }

                                        switch (SelectPic.firstselect)
                                        {
                                            case 1:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber1 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber1 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                break;
                                            case 2:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber2 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber2 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                break;
                                            case 3:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber3 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber3 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                break;
                                            case 4:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber4 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber4 + ".JPG", MainWindow.GrayFilterPath + @"\gray1.jpg");
                                                }
                                                break;
                                        }

                                        switch (SelectPic.secondselect)
                                        {
                                            case 1:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber1 + ".JPG", MainWindow.GrayFilterPath + @"\gray2.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber1 + ".JPG", MainWindow.GrayFilterPath + @"\gray2.jpg");
                                                }
                                                break;
                                            case 2:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber2 + ".JPG", MainWindow.GrayFilterPath + @"\gray2.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber2 + ".JPG", MainWindow.GrayFilterPath + @"\gray2.jpg");
                                                }
                                                break;
                                            case 3:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber3 + ".JPG", MainWindow.GrayFilterPath + @"\gray2.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber3 + ".JPG", MainWindow.GrayFilterPath + @"\gray2.jpg");
                                                }
                                                break;
                                            case 4:
                                                if (AISelect.aiselect != 4)
                                                {
                                                    await makefilter(MainWindow.ResizePath + @"\RPhoto" + photonumber4 + ".JPG", MainWindow.GrayFilterPath + @"\gray2.jpg");
                                                }
                                                else
                                                {
                                                    await AIBeauty(MainWindow.ResizePath + @"\RPhoto" + photonumber4 + ".JPG", MainWindow.GrayFilterPath + @"\gray2.jpg");
                                                }
                                                break;
                                        }
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "tech")
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                        photo1 = photonum - 5;
                                        photo2 = photonum - 4;
                                        photo3 = photonum - 3;
                                        photo4 = photonum - 2;
                                        photo5 = photonum - 1;
                                        photo6 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                        break;
                                    case 4:
                                        photo1 = photonum - 7;
                                        photo2 = photonum - 6;
                                        photo3 = photonum - 5;
                                        photo4 = photonum - 4;
                                        photo5 = photonum - 3;
                                        photo6 = photonum - 2;
                                        photo7 = photonum - 1;
                                        photo8 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");

                                        if (photo7 > 999)
                                        {
                                            photonumber = Convert.ToString(photo7);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo7);
                                        }
                                        else if (photo7 < 100 && photo7 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo7);
                                        }
                                        else if (photo7 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo7);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray7.jpg");

                                        if (photo8 > 999)
                                        {
                                            photonumber = Convert.ToString(photo8);
                                        }
                                        else if (photo8 < 1000 && photo8 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo8);
                                        }
                                        else if (photo8 < 100 && photo8 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo8);
                                        }
                                        else if (photo8 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo8);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray8.jpg");
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "r80")
                            {
                                switch (MainWindow.camnumber.ToString())
                                {
                                    case "1":
                                        photonum = TakePic.photonum;
                                        break;
                                    case "2":
                                        photonum = WebCam.photonum;
                                        break;
                                }
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        photo1 = photonum - 3;
                                        photo2 = photonum - 2;
                                        photo3 = photonum - 1;
                                        photo4 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                                break;
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                                break;
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                                break;
                                        }

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        switch (MainWindow.camnumber.ToString())
                                        {
                                            case "1":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                                break;
                                            case "2":
                                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                                break;
                                        }
                                        break;
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                    case 6:
                                        photo1 = photonum - 5;
                                        photo2 = photonum - 4;
                                        photo3 = photonum - 3;
                                        photo4 = photonum - 2;
                                        photo5 = photonum - 1;
                                        photo6 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray2.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray3.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                        }

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray4.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                        }

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray5.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");
                                        }

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray6.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                        }
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "r81" || MainWindow.inifoldername == "r83")
                            {
                                switch (MainWindow.camnumber.ToString())
                                {
                                    case "1":
                                        photonum = TakePic.photonum;
                                        break;
                                    case "2":
                                        photonum = WebCam.photonum;
                                        break;
                                }

                                photo1 = photonum - 5;
                                photo2 = photonum - 4;
                                photo3 = photonum - 3;
                                photo4 = photonum - 2;
                                photo5 = photonum - 1;
                                photo6 = photonum;

                                if (photo1 > 999)
                                {
                                    photonumber = Convert.ToString(photo1);
                                }
                                else if (photo1 < 1000 && photo1 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo1);
                                }
                                else if (photo1 < 100 && photo1 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo1);
                                }
                                else if (photo1 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo1);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                if (photo2 > 999)
                                {
                                    photonumber = Convert.ToString(photo2);
                                }
                                else if (photo2 < 1000 && photo2 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo2);
                                }
                                else if (photo2 < 100 && photo2 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo2);
                                }
                                else if (photo2 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo2);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                if (photo3 > 999)
                                {
                                    photonumber = Convert.ToString(photo3);
                                }
                                else if (photo3 < 1000 && photo3 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo3);
                                }
                                else if (photo3 < 100 && photo3 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo3);
                                }
                                else if (photo3 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo3);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                if (photo4 > 999)
                                {
                                    photonumber = Convert.ToString(photo4);
                                }
                                else if (photo4 < 1000 && photo4 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo4);
                                }
                                else if (photo4 < 100 && photo4 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo4);
                                }
                                else if (photo4 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo4);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                if (photo5 > 999)
                                {
                                    photonumber = Convert.ToString(photo5);
                                }
                                else if (photo5 < 1000 && photo5 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo5);
                                }
                                else if (photo5 < 100 && photo5 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo5);
                                }
                                else if (photo5 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo5);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                if (photo6 > 999)
                                {
                                    photonumber = Convert.ToString(photo6);
                                }
                                else if (photo6 < 1000 && photo6 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo6);
                                }
                                else if (photo6 < 100 && photo6 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo6);
                                }
                                else if (photo6 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo6);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                            }
                            else if (MainWindow.inifoldername == "r30")
                            {
                                switch (MainWindow.camnumber.ToString())
                                {
                                    case "1":
                                        photonum = TakePic.photonum;
                                        break;
                                    case "2":
                                        photonum = WebCam.photonum;
                                        break;
                                }

                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        photo1 = photonum - 2;
                                        photo2 = photonum - 1;
                                        photo3 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                        break;
                                    case 2:
                                    case 3:
                                    case 4:
                                        photo1 = photonum - 3;
                                        photo2 = photonum - 2;
                                        photo3 = photonum - 1;
                                        photo4 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "r24" || MainWindow.inifoldername == "r25")
                            {
                                switch (MainWindow.camnumber.ToString())
                                {
                                    case "1":
                                        photonum = TakePic.photonum;
                                        break;
                                    case "2":
                                        photonum = WebCam.photonum;
                                        break;
                                }

                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        photo1 = photonum - 2;
                                        photo2 = photonum - 1;
                                        photo3 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                        break;
                                    case 2:
                                        photo1 = photonum - 5;
                                        photo2 = photonum - 4;
                                        photo3 = photonum - 3;
                                        photo4 = photonum - 2;
                                        photo5 = photonum - 1;
                                        photo6 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "r28")
                            {
                                switch (MainWindow.camnumber.ToString())
                                {
                                    case "1":
                                        photonum = TakePic.photonum;
                                        break;
                                    case "2":
                                        photonum = WebCam.photonum;
                                        break;
                                }

                                switch (TempSelect.temp)
                                {
                                    case 1:
                                    case 2:
                                        photo1 = photonum - 5;
                                        photo2 = photonum - 4;
                                        photo3 = photonum - 3;
                                        photo4 = photonum - 2;
                                        photo5 = photonum - 1;
                                        photo6 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                        break;
                                    case 3:
                                        photo1 = photonum - 4;
                                        photo2 = photonum - 3;
                                        photo3 = photonum - 2;
                                        photo4 = photonum - 1;
                                        photo5 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");
                                        break;
                                    case 4:
                                        photo1 = photonum - 3;
                                        photo2 = photonum - 2;
                                        photo3 = photonum - 1;
                                        photo4 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                        break;
                                }
                            }
                            else if (MainWindow.inifoldername == "r54")
                            {
                                switch (MainWindow.camnumber.ToString())
                                {
                                    case "1":
                                        photonum = TakePic.photonum;
                                        break;
                                    case "2":
                                        photonum = WebCam.photonum;
                                        break;
                                }
                                photo1 = photonum - 5;
                                photo2 = photonum - 4;
                                photo3 = photonum - 3;
                                photo4 = photonum - 2;
                                photo5 = photonum - 1;
                                photo6 = photonum;

                                if (photo1 > 999)
                                {
                                    photonumber = Convert.ToString(photo1);
                                }
                                else if (photo1 < 1000 && photo1 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo1);
                                }
                                else if (photo1 < 100 && photo1 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo1);
                                }
                                else if (photo1 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo1);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                if (photo2 > 999)
                                {
                                    photonumber = Convert.ToString(photo2);
                                }
                                else if (photo2 < 1000 && photo2 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo2);
                                }
                                else if (photo2 < 100 && photo2 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo2);
                                }
                                else if (photo2 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo2);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                if (photo3 > 999)
                                {
                                    photonumber = Convert.ToString(photo3);
                                }
                                else if (photo3 < 1000 && photo3 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo3);
                                }
                                else if (photo3 < 100 && photo3 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo3);
                                }
                                else if (photo3 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo3);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                if (photo4 > 999)
                                {
                                    photonumber = Convert.ToString(photo4);
                                }
                                else if (photo4 < 1000 && photo4 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo4);
                                }
                                else if (photo4 < 100 && photo4 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo4);
                                }
                                else if (photo4 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo4);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                if (photo5 > 999)
                                {
                                    photonumber = Convert.ToString(photo5);
                                }
                                else if (photo5 < 1000 && photo5 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo5);
                                }
                                else if (photo5 < 100 && photo5 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo5);
                                }
                                else if (photo5 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo5);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                if (photo6 > 999)
                                {
                                    photonumber = Convert.ToString(photo6);
                                }
                                else if (photo6 < 1000 && photo6 > 99)
                                {
                                    photonumber = "0" + Convert.ToString(photo6);
                                }
                                else if (photo6 < 100 && photo6 > 9)
                                {
                                    photonumber = "00" + Convert.ToString(photo6);
                                }
                                else if (photo6 < 10)
                                {
                                    photonumber = "000" + Convert.ToString(photo6);
                                }

                                Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                            }
                            else if (MainWindow.inifoldername == "hhh")
                            {
                                if (MainWindow.optiontempnum == 1)
                                {
                                    photo1 = photonum - 1;
                                    photo2 = photonum;

                                    if (photo1 > 999)
                                    {
                                        photonumber = Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 1000 && photo1 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 100 && photo1 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo1);
                                    }
                                    else if (photo1 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo1);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                    if (photo2 > 999)
                                    {
                                        photonumber = Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 1000 && photo2 > 99)
                                    {
                                        photonumber = "0" + Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 100 && photo2 > 9)
                                    {
                                        photonumber = "00" + Convert.ToString(photo2);
                                    }
                                    else if (photo2 < 10)
                                    {
                                        photonumber = "000" + Convert.ToString(photo2);
                                    }

                                    Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                }
                                else
                                {
                                    switch (TempSelect.temp)
                                    {
                                        case 1:
                                            photo1 = photonum - 1;
                                            photo2 = photonum;

                                            if (photo1 > 999)
                                            {
                                                photonumber = Convert.ToString(photo1);
                                            }
                                            else if (photo1 < 1000 && photo1 > 99)
                                            {
                                                photonumber = "0" + Convert.ToString(photo1);
                                            }
                                            else if (photo1 < 100 && photo1 > 9)
                                            {
                                                photonumber = "00" + Convert.ToString(photo1);
                                            }
                                            else if (photo1 < 10)
                                            {
                                                photonumber = "000" + Convert.ToString(photo1);
                                            }

                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                            if (photo2 > 999)
                                            {
                                                photonumber = Convert.ToString(photo2);
                                            }
                                            else if (photo2 < 1000 && photo2 > 99)
                                            {
                                                photonumber = "0" + Convert.ToString(photo2);
                                            }
                                            else if (photo2 < 100 && photo2 > 9)
                                            {
                                                photonumber = "00" + Convert.ToString(photo2);
                                            }
                                            else if (photo2 < 10)
                                            {
                                                photonumber = "000" + Convert.ToString(photo2);
                                            }

                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                            break;
                                        case 2:
                                        case 3:
                                            photo1 = photonum - 3;
                                            photo2 = photonum - 2;
                                            photo3 = photonum - 1;
                                            photo4 = photonum;

                                            if (photo1 > 999)
                                            {
                                                photonumber = Convert.ToString(photo1);
                                            }
                                            else if (photo1 < 1000 && photo1 > 99)
                                            {
                                                photonumber = "0" + Convert.ToString(photo1);
                                            }
                                            else if (photo1 < 100 && photo1 > 9)
                                            {
                                                photonumber = "00" + Convert.ToString(photo1);
                                            }
                                            else if (photo1 < 10)
                                            {
                                                photonumber = "000" + Convert.ToString(photo1);
                                            }

                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                            if (photo2 > 999)
                                            {
                                                photonumber = Convert.ToString(photo2);
                                            }
                                            else if (photo2 < 1000 && photo2 > 99)
                                            {
                                                photonumber = "0" + Convert.ToString(photo2);
                                            }
                                            else if (photo2 < 100 && photo2 > 9)
                                            {
                                                photonumber = "00" + Convert.ToString(photo2);
                                            }
                                            else if (photo2 < 10)
                                            {
                                                photonumber = "000" + Convert.ToString(photo2);
                                            }

                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                            if (photo3 > 999)
                                            {
                                                photonumber = Convert.ToString(photo3);
                                            }
                                            else if (photo3 < 1000 && photo3 > 99)
                                            {
                                                photonumber = "0" + Convert.ToString(photo3);
                                            }
                                            else if (photo3 < 100 && photo3 > 9)
                                            {
                                                photonumber = "00" + Convert.ToString(photo3);
                                            }
                                            else if (photo3 < 10)
                                            {
                                                photonumber = "000" + Convert.ToString(photo3);
                                            }

                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                            if (photo4 > 999)
                                            {
                                                photonumber = Convert.ToString(photo4);
                                            }
                                            else if (photo4 < 1000 && photo4 > 99)
                                            {
                                                photonumber = "0" + Convert.ToString(photo4);
                                            }
                                            else if (photo4 < 100 && photo4 > 9)
                                            {
                                                photonumber = "00" + Convert.ToString(photo4);
                                            }
                                            else if (photo4 < 10)
                                            {
                                                photonumber = "000" + Convert.ToString(photo4);
                                            }

                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                            break;
                                    }
                                }
                            }
                            else if (MainWindow.inifoldername == "harim")
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        photo1 = photonum - 3;
                                        photo2 = photonum - 2;
                                        photo3 = photonum - 1;
                                        photo4 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray2.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray3.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                        }

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray4.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                        }
                                        break;
                                    case 2:
                                        photo1 = photonum - 1;
                                        photo2 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray2.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                        }
                                        break;
                                    case 3:
                                        photo1 = photonum - 5;
                                        photo2 = photonum - 4;
                                        photo3 = photonum - 3;
                                        photo4 = photonum - 2;
                                        photo5 = photonum - 1;
                                        photo6 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                        break;
                                }
                            }
                            else
                            {
                                switch (TempSelect.temp)
                                {
                                    case 1:
                                        photo1 = photonum - 5;
                                        photo2 = photonum - 4;
                                        photo3 = photonum - 3;
                                        photo4 = photonum - 2;
                                        photo5 = photonum - 1;
                                        photo6 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray2.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray3.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                        }

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray4.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                        }

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray5.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");
                                        }

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray6.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");
                                        }
                                        break;
                                    case 2:

                                        photo1 = photonum - 3;
                                        photo2 = photonum - 2;
                                        photo3 = photonum - 1;
                                        photo4 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray1.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");
                                        }

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray2.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");
                                        }

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray3.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");
                                        }

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        if (MainWindow.checkingflip == "Using")
                                        {
                                            Pic(@"\IMG_" + photonumber + ".JPG", @"\gray4.jpg");
                                        }
                                        else
                                        {
                                            Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");
                                        }
                                        break;
                                    case 3:
                                        photo1 = photonum - 7;
                                        photo2 = photonum - 6;
                                        photo3 = photonum - 5;
                                        photo4 = photonum - 4;
                                        photo5 = photonum - 3;
                                        photo6 = photonum - 2;
                                        photo7 = photonum - 1;
                                        photo8 = photonum;

                                        if (photo1 > 999)
                                        {
                                            photonumber = Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 1000 && photo1 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 100 && photo1 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo1);
                                        }
                                        else if (photo1 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo1);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray1.jpg");

                                        if (photo2 > 999)
                                        {
                                            photonumber = Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 1000 && photo2 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 100 && photo2 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo2);
                                        }
                                        else if (photo2 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo2);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray2.jpg");

                                        if (photo3 > 999)
                                        {
                                            photonumber = Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 1000 && photo3 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 100 && photo3 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo3);
                                        }
                                        else if (photo3 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo3);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray3.jpg");

                                        if (photo4 > 999)
                                        {
                                            photonumber = Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 1000 && photo4 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 100 && photo4 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo4);
                                        }
                                        else if (photo4 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo4);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray4.jpg");

                                        if (photo5 > 999)
                                        {
                                            photonumber = Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 1000 && photo5 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 100 && photo5 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo5);
                                        }
                                        else if (photo5 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo5);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray5.jpg");

                                        if (photo6 > 999)
                                        {
                                            photonumber = Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 100 && photo6 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo6);
                                        }
                                        else if (photo6 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo6);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray6.jpg");

                                        if (photo7 > 999)
                                        {
                                            photonumber = Convert.ToString(photo7);
                                        }
                                        else if (photo6 < 1000 && photo6 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo7);
                                        }
                                        else if (photo7 < 100 && photo7 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo7);
                                        }
                                        else if (photo7 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo7);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray7.jpg");

                                        if (photo8 > 999)
                                        {
                                            photonumber = Convert.ToString(photo8);
                                        }
                                        else if (photo8 < 1000 && photo8 > 99)
                                        {
                                            photonumber = "0" + Convert.ToString(photo8);
                                        }
                                        else if (photo8 < 100 && photo8 > 9)
                                        {
                                            photonumber = "00" + Convert.ToString(photo8);
                                        }
                                        else if (photo8 < 10)
                                        {
                                            photonumber = "000" + Convert.ToString(photo8);
                                        }

                                        Pic(@"\RPhoto" + photonumber + ".JPG", @"\gray8.jpg");
                                        break;
                                }
                            }
                        }
                    }

                    if (MainWindow.checkkakao != "0" || MainWindow.qroption == 1 || MainWindow.checkvideo == "1")
                    {
                        ImgCompose.kakaonumbercheck.Clear();
                        ImgCompose.kakaonumbercheck.Append(DateTime.Now.ToString("MMddHHmmss"));
                        ImgCompose.SendKakaoImgName = "KakaoPhoto" + DateTime.Now.ToString(ImgCompose.kakaonumbercheck.ToString()) + ".PNG";

                        MainWindow.filename = GenerateFileName();

                        MainWindow.htmlUrl = "";
                        if (MainWindow.SorR == "S")
                        {
                            if (MainWindow.inifoldername == "animalhospital")
                            {
                                MainWindow.htmlUrl = $"https://onecutweb.azurewebsites.net/{MainWindow.inifoldername}/{MainWindow.filename}";
                            }
                            else if (MainWindow.inifoldername == "hhh")
                            {
                                MainWindow.htmlUrl = $"https://onecutweb.azurewebsites.net/{MainWindow.inifoldername}/{MainWindow.filename}";
                            }
                            else
                            {
                                MainWindow.htmlUrl = $"https://onecutweb.azurewebsites.net/{MainWindow.inifoldername}/{MainWindow.filename}";
                            }
                        }
                        else if (MainWindow.SorR == "R")
                        {
                            MainWindow.htmlUrl = $"https://onecutweb.azurewebsites.net/rental/{MainWindow.filename}";
                        }
                        GenerateQRCode(MainWindow.htmlUrl);
                    }

                    NextPage();
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private static string GenerateFileName()
        {
            DateTime now = DateTime.Now;
            string datePart = now.ToString("yyyyMMdd");
            string timePart = now.ToString("HHmmss");

            return $"PhotoView{datePart}{timePart}.php";
        }

        private static void GenerateQRCode(string htmlUrl)
        {
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(htmlUrl, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(10);

                // Set the QR code file name using the timestamp
                string qrCodeFileName = "QRCode" + ImgCompose.SendKakaoImgName;

                // 프로그램 실행 경로의 QR 폴더 경로
                string qrFolderPath = MainWindow.savekakaopath; // QR 폴더 경로
                string qrCodeFilePath = System.IO.Path.Combine(qrFolderPath, qrCodeFileName); // QR 코드 파일 경로

                // QR 폴더가 없는 경우 생성
                if (!Directory.Exists(qrFolderPath))
                {
                    Directory.CreateDirectory(qrFolderPath);
                }

                qrCodeImage.Save(qrCodeFilePath, System.Drawing.Imaging.ImageFormat.Png);

                Source.Log.log.Info("QR code image generated successfully and saved in the QR folder!");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }


        #region /// ai 필터 ///

        private async Task makefilter(string imgpath, string outputpath)
        {
            // API 엔드포인트 및 키 설정
            string apiUrl = "https://www.ailabapi.com/api/portrait/effects/portrait-animation";
            string apiKey = "7UYAtsNvGkWLm8O59ZFvEHJqr0arxRDdf1MljszixTPguVCzTwyB2E9mjVkXQONb";

            // 이미지 파일 정보
            var imageFileInfo = new FileInfo(imgpath);

            // 이미지 파일 읽기
            byte[] imageBytes = File.ReadAllBytes(imageFileInfo.FullName);

            // API 요청 헤더 설정
            var headers = new Dictionary<string, string>
                {
                    { "ailabapi-api-key", apiKey }
                };

            // API 요청 바디 데이터 설정
            var requestContent = new MultipartFormDataContent();
            foreach (var header in headers)
            {
                requestContent.Headers.Add(header.Key, header.Value);
            }
            var imageContent = new ByteArrayContent(imageBytes);
            imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            requestContent.Add(imageContent, "image", imageFileInfo.Name);

            switch (AISelect.aiselect)
            {
                case 1:
                    requestContent.Add(new StringContent("pixar_plus"), "type"); // 적절한 Cartoon effect type을 선택하세요.

                    outputfilepath = outputpath;
                    break;
                case 2:
                    requestContent.Add(new StringContent("jpcartoon"), "type"); // 적절한 Cartoon effect type을 선택하세요.

                    outputfilepath = outputpath;
                    break;
                case 3:
                    requestContent.Add(new StringContent("sketch"), "type"); // 적절한 Cartoon effect type을 선택하세요.

                    outputfilepath = outputpath;
                    break;
            }

            // API 호출 및 응답 받기
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(apiUrl, requestContent);

                // 응답 데이터 확인
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // 결과 이미지 URL 추출
                    var jsonObject = JObject.Parse(jsonResponse);
                    string imageUrl = jsonObject["data"]["image_url"].ToString();


                    // 결과 이미지 다운로드 및 바탕화면에 저장
                    await DownloadAndSaveImage(imageUrl, outputfilepath);

                    // 처리 완료된 시간 로그에 표시
                    Source.Log.log.Info($"변환 완료 시간: {DateTime.Now}");
                }
                else
                {
                    Source.Log.log.Info($"API 호출 실패. 응답 코드: {response.StatusCode}");
                    Source.Log.log.Info($"응답 내용: {await response.Content.ReadAsStringAsync()}");
                    Source.Log.log.Info("AI 필터 재시도");

                    // 이미지 파일 정보
                    imageFileInfo = new FileInfo(imgpath);

                    // 이미지 파일 읽기
                    imageBytes = File.ReadAllBytes(imageFileInfo.FullName);

                    // API 요청 헤더 설정
                    headers = new Dictionary<string, string>
                {
                    { "ailabapi-api-key", apiKey }
                };

                    // API 요청 바디 데이터 설정
                    requestContent = new MultipartFormDataContent();
                    foreach (var header in headers)
                    {
                        requestContent.Headers.Add(header.Key, header.Value);
                    }
                    imageContent = new ByteArrayContent(imageBytes);
                    imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    requestContent.Add(imageContent, "image", imageFileInfo.Name);

                    switch (AISelect.aiselect)
                    {
                        case 1:
                            requestContent.Add(new StringContent("pixar_plus"), "type"); // 적절한 Cartoon effect type을 선택하세요.

                            outputfilepath = outputpath;
                            break;
                        case 2:
                            requestContent.Add(new StringContent("jpcartoon"), "type"); // 적절한 Cartoon effect type을 선택하세요.

                            outputfilepath = outputpath;
                            break;
                        case 3:
                            requestContent.Add(new StringContent("sketch"), "type"); // 적절한 Cartoon effect type을 선택하세요.

                            outputfilepath = outputpath;
                            break;
                    }

                    // API 호출 및 응답 받기
                    using (var client2 = new HttpClient())
                    {
                        response = await client2.PostAsync(apiUrl, requestContent);

                        // 응답 데이터 확인
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();

                            // 결과 이미지 URL 추출
                            var jsonObject = JObject.Parse(jsonResponse);
                            string imageUrl = jsonObject["data"]["image_url"].ToString();


                            // 결과 이미지 다운로드 및 바탕화면에 저장
                            await DownloadAndSaveImage(imageUrl, outputfilepath);

                            // 처리 완료된 시간 로그에 표시
                            Source.Log.log.Info($"변환 완료 시간: {DateTime.Now}");
                        }
                        else
                        {
                            Source.Log.log.Info($"API 호출 실패. 응답 코드: {response.StatusCode}");
                            Source.Log.log.Info($"응답 내용: {await response.Content.ReadAsStringAsync()}");
                        }
                    }
                }
            }
        }

        public async Task DownloadAndSaveImage(string imageUrl, string savepath)
        {
            using (var client = new HttpClient())
            {
                var imageBytes = await client.GetByteArrayAsync(imageUrl);

                // 바탕화면에 저장할 경로 설정
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string savePath = System.IO.Path.Combine(savepath);

                // 이미지 저장
                using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    await fileStream.WriteAsync(imageBytes, 0, imageBytes.Length);
                }

                Source.Log.log.Info($"결과 이미지가 저장되었습니다. 경로: {savePath}");
            }
        }

        public async Task AIBeauty(string imgpath, string savepath)
        {
            // API 엔드포인트 및 키 설정
            string apiUrl = "https://www.ailabapi.com/api/portrait/effects/smart-skin";
            string apiKey = "7UYAtsNvGkWLm8O59ZFvEHJqr0arxRDdf1MljszixTPguVCzTwyB2E9mjVkXQONb";

            // 이미지 파일 정보
            var imageFileInfo = new FileInfo(imgpath);

            // 이미지 파일 읽기
            byte[] imageBytes = File.ReadAllBytes(imageFileInfo.FullName);

            // API 요청 헤더 설정
            var headers = new Dictionary<string, string>
                {
                    { "ailabapi-api-key", apiKey }
                };

            // API 요청 바디 데이터 설정
            var requestContent = new MultipartFormDataContent();
            foreach (var header in headers)
            {
                requestContent.Headers.Add(header.Key, header.Value);
            }
            var imageContent = new ByteArrayContent(imageBytes);
            imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            requestContent.Add(imageContent, "image", imageFileInfo.Name);

            requestContent.Add(new StringContent("1.0"), "retouch_degree");
            requestContent.Add(new StringContent("1.0"), "whitening_degree");

            outputfilepath = savepath;

            // API 호출 및 응답 받기
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(apiUrl, requestContent);

                // 응답 데이터 확인
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // 결과 이미지 URL 추출
                    var jsonObject = JObject.Parse(jsonResponse);
                    string imageUrl = jsonObject["data"]["image_url"].ToString();


                    // 결과 이미지 다운로드 및 바탕화면에 저장
                    await DownloadAndSaveImage(imageUrl, outputfilepath);

                    // 처리 완료된 시간 로그에 표시
                    Console.WriteLine($"변환 완료 시간: {DateTime.Now}");
                }
                else
                {
                    Console.WriteLine($"API 호출 실패. 응답 코드: {response.StatusCode}");
                    Console.WriteLine($"응답 내용: {await response.Content.ReadAsStringAsync()}");

                    // 이미지 파일 정보
                    imageFileInfo = new FileInfo(imgpath);

                    // 이미지 파일 읽기
                    imageBytes = File.ReadAllBytes(imageFileInfo.FullName);

                    // API 요청 헤더 설정
                    headers = new Dictionary<string, string>
                {
                    { "ailabapi-api-key", apiKey }
                };

                    // API 요청 바디 데이터 설정
                    requestContent = new MultipartFormDataContent();
                    foreach (var header in headers)
                    {
                        requestContent.Headers.Add(header.Key, header.Value);
                    }
                    imageContent = new ByteArrayContent(imageBytes);
                    imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    requestContent.Add(imageContent, "image", imageFileInfo.Name);

                    requestContent.Add(new StringContent("1.0"), "retouch_degree");
                    requestContent.Add(new StringContent("1.0"), "whitening_degree");

                    outputfilepath = savepath;

                    // API 호출 및 응답 받기
                    using (var client2 = new HttpClient())
                    {
                        response = await client2.PostAsync(apiUrl, requestContent);

                        // 응답 데이터 확인
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();

                            // 결과 이미지 URL 추출
                            var jsonObject = JObject.Parse(jsonResponse);
                            string imageUrl = jsonObject["data"]["image_url"].ToString();


                            // 결과 이미지 다운로드 및 바탕화면에 저장
                            await DownloadAndSaveImage(imageUrl, outputfilepath);

                            // 처리 완료된 시간 로그에 표시
                            Console.WriteLine($"변환 완료 시간: {DateTime.Now}");
                        }
                        else
                        {
                            Console.WriteLine($"API 호출 실패. 응답 코드: {response.StatusCode}");
                            Console.WriteLine($"응답 내용: {await response.Content.ReadAsStringAsync()}");
                        }
                    }
                }
            }
        }


        public async Task DownloadAndSaveImage2(string imageBase64, string savepath)
        {
            // Base64로 인코딩된 문자열을 이미지 바이트 배열로 디코딩
            byte[] imageBytes = Convert.FromBase64String(imageBase64);

            // 바탕화면에 저장할 경로 설정
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string savePath = System.IO.Path.Combine(savepath);

            // 이미지 저장
            File.WriteAllBytes(savePath, imageBytes);

            Source.Log.log.Info("뷰티 필터 결과 이미지가 저장되었습니다. 경로: " + savePath);
        }

        #endregion


        private void Imgshow()
        {
            try
            {
                int photonum = 0;
                photonum = TakePic.photonum;
                string photonumber1 = "";
                string photonumber2 = "";
                string photonumber3 = "";
                string photonumber4 = "";

                if (MainWindow.optiontempnum == 1)
                {
                    photo1 = photonum - 1;
                    photo2 = photonum;

                    if (photo1 > 999)
                    {
                        photonumber1 = Convert.ToString(photo1);
                    }
                    else if (photo1 < 1000 && photo1 > 99)
                    {
                        photonumber1 = "0" + Convert.ToString(photo1);
                    }
                    else if (photo1 < 100 && photo1 > 9)
                    {
                        photonumber1 = "00" + Convert.ToString(photo1);
                    }
                    else if (photo1 < 10)
                    {
                        photonumber1 = "000" + Convert.ToString(photo1);
                    }

                    if (photo2 > 999)
                    {
                        photonumber2 = Convert.ToString(photo2);
                    }
                    else if (photo2 < 1000 && photo2 > 99)
                    {
                        photonumber2 = "0" + Convert.ToString(photo2);
                    }
                    else if (photo2 < 100 && photo2 > 9)
                    {
                        photonumber2 = "00" + Convert.ToString(photo2);
                    }
                    else if (photo2 < 10)
                    {
                        photonumber2 = "000" + Convert.ToString(photo2);
                    }

                    switch (SelectPic.firstselect)
                    {
                        case 1:
                            gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                            break;
                        case 2:
                            gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                            break;
                    }
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            photo1 = photonum - 1;
                            photo2 = photonum;

                            if (photo1 > 999)
                            {
                                photonumber1 = Convert.ToString(photo1);
                            }
                            else if (photo1 < 1000 && photo1 > 99)
                            {
                                photonumber1 = "0" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 100 && photo1 > 9)
                            {
                                photonumber1 = "00" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 10)
                            {
                                photonumber1 = "000" + Convert.ToString(photo1);
                            }

                            if (photo2 > 999)
                            {
                                photonumber2 = Convert.ToString(photo2);
                            }
                            else if (photo2 < 1000 && photo2 > 99)
                            {
                                photonumber2 = "0" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 100 && photo2 > 9)
                            {
                                photonumber2 = "00" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 10)
                            {
                                photonumber2 = "000" + Convert.ToString(photo2);
                            }

                            switch (SelectPic.firstselect)
                            {
                                case 1:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                            }
                            break;
                        case 2:
                            photo1 = photonum - 3;
                            photo2 = photonum - 2;
                            photo3 = photonum - 1;
                            photo4 = photonum;

                            if (photo1 > 999)
                            {
                                photonumber1 = Convert.ToString(photo1);
                            }
                            else if (photo1 < 1000 && photo1 > 99)
                            {
                                photonumber1 = "0" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 100 && photo1 > 9)
                            {
                                photonumber1 = "00" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 10)
                            {
                                photonumber1 = "000" + Convert.ToString(photo1);
                            }

                            if (photo2 > 999)
                            {
                                photonumber2 = Convert.ToString(photo2);
                            }
                            else if (photo2 < 1000 && photo2 > 99)
                            {
                                photonumber2 = "0" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 100 && photo2 > 9)
                            {
                                photonumber2 = "00" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 10)
                            {
                                photonumber2 = "000" + Convert.ToString(photo2);
                            }

                            if (photo3 > 999)
                            {
                                photonumber3 = Convert.ToString(photo3);
                            }
                            else if (photo3 < 1000 && photo3 > 99)
                            {
                                photonumber3 = "0" + Convert.ToString(photo3);
                            }
                            else if (photo3 < 100 && photo3 > 9)
                            {
                                photonumber3 = "00" + Convert.ToString(photo3);
                            }
                            else if (photo3 < 10)
                            {
                                photonumber3 = "000" + Convert.ToString(photo3);
                            }

                            if (photo4 > 999)
                            {
                                photonumber4 = Convert.ToString(photo4);
                            }
                            else if (photo4 < 1000 && photo4 > 99)
                            {
                                photonumber4 = "0" + Convert.ToString(photo4);
                            }
                            else if (photo4 < 100 && photo4 > 9)
                            {
                                photonumber4 = "00" + Convert.ToString(photo4);
                            }
                            else if (photo4 < 10)
                            {
                                photonumber4 = "000" + Convert.ToString(photo4);
                            }

                            switch (SelectPic.firstselect)
                            {
                                case 1:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            switch (SelectPic.secondselect)
                            {
                                case 1:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            break;
                        case 3:
                            photo1 = photonum - 3;
                            photo2 = photonum - 2;
                            photo3 = photonum - 1;
                            photo4 = photonum;

                            if (photo1 > 999)
                            {
                                photonumber1 = Convert.ToString(photo1);
                            }
                            else if (photo1 < 1000 && photo1 > 99)
                            {
                                photonumber1 = "0" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 100 && photo1 > 9)
                            {
                                photonumber1 = "00" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 10)
                            {
                                photonumber1 = "000" + Convert.ToString(photo1);
                            }

                            if (photo2 > 999)
                            {
                                photonumber2 = Convert.ToString(photo2);
                            }
                            else if (photo2 < 1000 && photo2 > 99)
                            {
                                photonumber2 = "0" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 100 && photo2 > 9)
                            {
                                photonumber2 = "00" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 10)
                            {
                                photonumber2 = "000" + Convert.ToString(photo2);
                            }

                            if (photo3 > 999)
                            {
                                photonumber3 = Convert.ToString(photo3);
                            }
                            else if (photo3 < 1000 && photo3 > 99)
                            {
                                photonumber3 = "0" + Convert.ToString(photo3);
                            }
                            else if (photo3 < 100 && photo3 > 9)
                            {
                                photonumber3 = "00" + Convert.ToString(photo3);
                            }
                            else if (photo3 < 10)
                            {
                                photonumber3 = "000" + Convert.ToString(photo3);
                            }

                            if (photo4 > 999)
                            {
                                photonumber4 = Convert.ToString(photo4);
                            }
                            else if (photo4 < 1000 && photo4 > 99)
                            {
                                photonumber4 = "0" + Convert.ToString(photo4);
                            }
                            else if (photo4 < 100 && photo4 > 9)
                            {
                                photonumber4 = "00" + Convert.ToString(photo4);
                            }
                            else if (photo4 < 10)
                            {
                                photonumber4 = "000" + Convert.ToString(photo4);
                            }

                            switch (SelectPic.firstselect)
                            {
                                case 1:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            switch (SelectPic.secondselect)
                            {
                                case 1:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            switch (SelectPic.thirdselect)
                            {
                                case 1:
                                    gray3 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray3 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray3 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray3 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            switch (SelectPic.fourthselect)
                            {
                                case 1:
                                    gray4 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray4 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray4 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray4 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            break;
                    }
                }


                if (MainWindow.optiontempnum == 1)
                {
                    using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                    {
                        MainWindow.ga4.DrawImage(gray1, 228, 648, 902, 1353);
                        MainWindow.ga4.DrawImage(MainWindow.temp1_1Front, 0, 0, 2480, 3508);
                    }
                    MainWindow.ga4.Dispose();
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                            {
                                MainWindow.ga4.DrawImage(gray1, 228, 648, 902, 1353);
                                MainWindow.ga4.DrawImage(MainWindow.temp1_1Front, 0, 0, 2480, 3508);
                            }
                            MainWindow.ga4.Dispose();
                            break;
                        case 2:
                            using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                            {
                                MainWindow.ga4.DrawImage(gray1, 217, 640, 864, 1296);
                                MainWindow.ga4.DrawImage(gray2, 1377, 640, 864, 1296);
                                MainWindow.ga4.DrawImage(MainWindow.temp2_1Front, 0, 0, 2480, 3508);
                            }
                            MainWindow.ga4.Dispose();
                            break;
                        case 3:
                            using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                            {
                                MainWindow.ga4.DrawImage(gray1, 180, 590, 545, 818);
                                MainWindow.ga4.DrawImage(gray2, 180, 1524, 545, 818);
                                MainWindow.ga4.DrawImage(gray3, 954, 1524, 545, 818);
                                MainWindow.ga4.DrawImage(gray4, 1728, 1524, 545, 818);
                                MainWindow.ga4.DrawImage(MainWindow.temp3_1Front, 0, 0, 2480, 3508);
                            }
                            MainWindow.ga4.Dispose();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Imgshow2()
        {
            try
            {
                int photonum = 0;
                photonum = TakePic.photonum;
                string photonumber1 = "";
                string photonumber2 = "";
                string photonumber3 = "";
                string photonumber4 = "";

                if (MainWindow.optiontempnum == 1)
                {
                    photo1 = photonum - 1;
                    photo2 = photonum;

                    if (photo1 > 999)
                    {
                        photonumber1 = Convert.ToString(photo1);
                    }
                    else if (photo1 < 1000 && photo1 > 99)
                    {
                        photonumber1 = "0" + Convert.ToString(photo1);
                    }
                    else if (photo1 < 100 && photo1 > 9)
                    {
                        photonumber1 = "00" + Convert.ToString(photo1);
                    }
                    else if (photo1 < 10)
                    {
                        photonumber1 = "000" + Convert.ToString(photo1);
                    }

                    if (photo2 > 999)
                    {
                        photonumber2 = Convert.ToString(photo2);
                    }
                    else if (photo2 < 1000 && photo2 > 99)
                    {
                        photonumber2 = "0" + Convert.ToString(photo2);
                    }
                    else if (photo2 < 100 && photo2 > 9)
                    {
                        photonumber2 = "00" + Convert.ToString(photo2);
                    }
                    else if (photo2 < 10)
                    {
                        photonumber2 = "000" + Convert.ToString(photo2);
                    }

                    switch (SelectPic.firstselect)
                    {
                        case 1:
                            gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                            break;
                        case 2:
                            gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                            break;
                    }
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            photo1 = photonum - 1;
                            photo2 = photonum;

                            if (photo1 > 999)
                            {
                                photonumber1 = Convert.ToString(photo1);
                            }
                            else if (photo1 < 1000 && photo1 > 99)
                            {
                                photonumber1 = "0" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 100 && photo1 > 9)
                            {
                                photonumber1 = "00" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 10)
                            {
                                photonumber1 = "000" + Convert.ToString(photo1);
                            }

                            if (photo2 > 999)
                            {
                                photonumber2 = Convert.ToString(photo2);
                            }
                            else if (photo2 < 1000 && photo2 > 99)
                            {
                                photonumber2 = "0" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 100 && photo2 > 9)
                            {
                                photonumber2 = "00" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 10)
                            {
                                photonumber2 = "000" + Convert.ToString(photo2);
                            }

                            switch (SelectPic.firstselect)
                            {
                                case 1:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                            }
                            break;
                        case 2:
                            photo1 = photonum - 3;
                            photo2 = photonum - 2;
                            photo3 = photonum - 1;
                            photo4 = photonum;

                            if (photo1 > 999)
                            {
                                photonumber1 = Convert.ToString(photo1);
                            }
                            else if (photo1 < 1000 && photo1 > 99)
                            {
                                photonumber1 = "0" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 100 && photo1 > 9)
                            {
                                photonumber1 = "00" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 10)
                            {
                                photonumber1 = "000" + Convert.ToString(photo1);
                            }

                            if (photo2 > 999)
                            {
                                photonumber2 = Convert.ToString(photo2);
                            }
                            else if (photo2 < 1000 && photo2 > 99)
                            {
                                photonumber2 = "0" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 100 && photo2 > 9)
                            {
                                photonumber2 = "00" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 10)
                            {
                                photonumber2 = "000" + Convert.ToString(photo2);
                            }

                            if (photo3 > 999)
                            {
                                photonumber3 = Convert.ToString(photo3);
                            }
                            else if (photo3 < 1000 && photo3 > 99)
                            {
                                photonumber3 = "0" + Convert.ToString(photo3);
                            }
                            else if (photo3 < 100 && photo3 > 9)
                            {
                                photonumber3 = "00" + Convert.ToString(photo3);
                            }
                            else if (photo3 < 10)
                            {
                                photonumber3 = "000" + Convert.ToString(photo3);
                            }

                            if (photo4 > 999)
                            {
                                photonumber4 = Convert.ToString(photo4);
                            }
                            else if (photo4 < 1000 && photo4 > 99)
                            {
                                photonumber4 = "0" + Convert.ToString(photo4);
                            }
                            else if (photo4 < 100 && photo4 > 9)
                            {
                                photonumber4 = "00" + Convert.ToString(photo4);
                            }
                            else if (photo4 < 10)
                            {
                                photonumber4 = "000" + Convert.ToString(photo4);
                            }

                            switch (SelectPic.firstselect)
                            {
                                case 1:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            switch (SelectPic.secondselect)
                            {
                                case 1:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            break;
                        case 3:
                            photo1 = photonum - 3;
                            photo2 = photonum - 2;
                            photo3 = photonum - 1;
                            photo4 = photonum;

                            if (photo1 > 999)
                            {
                                photonumber1 = Convert.ToString(photo1);
                            }
                            else if (photo1 < 1000 && photo1 > 99)
                            {
                                photonumber1 = "0" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 100 && photo1 > 9)
                            {
                                photonumber1 = "00" + Convert.ToString(photo1);
                            }
                            else if (photo1 < 10)
                            {
                                photonumber1 = "000" + Convert.ToString(photo1);
                            }

                            if (photo2 > 999)
                            {
                                photonumber2 = Convert.ToString(photo2);
                            }
                            else if (photo2 < 1000 && photo2 > 99)
                            {
                                photonumber2 = "0" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 100 && photo2 > 9)
                            {
                                photonumber2 = "00" + Convert.ToString(photo2);
                            }
                            else if (photo2 < 10)
                            {
                                photonumber2 = "000" + Convert.ToString(photo2);
                            }

                            if (photo3 > 999)
                            {
                                photonumber3 = Convert.ToString(photo3);
                            }
                            else if (photo3 < 1000 && photo3 > 99)
                            {
                                photonumber3 = "0" + Convert.ToString(photo3);
                            }
                            else if (photo3 < 100 && photo3 > 9)
                            {
                                photonumber3 = "00" + Convert.ToString(photo3);
                            }
                            else if (photo3 < 10)
                            {
                                photonumber3 = "000" + Convert.ToString(photo3);
                            }

                            if (photo4 > 999)
                            {
                                photonumber4 = Convert.ToString(photo4);
                            }
                            else if (photo4 < 1000 && photo4 > 99)
                            {
                                photonumber4 = "0" + Convert.ToString(photo4);
                            }
                            else if (photo4 < 100 && photo4 > 9)
                            {
                                photonumber4 = "00" + Convert.ToString(photo4);
                            }
                            else if (photo4 < 10)
                            {
                                photonumber4 = "000" + Convert.ToString(photo4);
                            }

                            switch (SelectPic.firstselect)
                            {
                                case 1:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray1 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            switch (SelectPic.secondselect)
                            {
                                case 1:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray2 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            switch (SelectPic.thirdselect)
                            {
                                case 1:
                                    gray3 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray3 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray3 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray3 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            switch (SelectPic.fourthselect)
                            {
                                case 1:
                                    gray4 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray1.jpg");
                                    break;
                                case 2:
                                    gray4 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray2.jpg");
                                    break;
                                case 3:
                                    gray4 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray3.jpg");
                                    break;
                                case 4:
                                    gray4 = System.Drawing.Image.FromFile(MainWindow.GrayFilterPath + @"\gray4.jpg");
                                    break;
                            }
                            break;
                    }
                }

                MainWindow.hhhcanvas.Dispose();
                MainWindow.hhhcanvas = new Bitmap(2480, 3508);
                MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas);

                if (MainWindow.optiontempnum == 1)
                {
                    using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                    {
                        MainWindow.ga4.DrawImage(gray1, 228, 648, 902, 1353);
                        MainWindow.ga4.DrawImage(MainWindow.temp1_1Front, 0, 0, 2480, 3508);
                    }
                    MainWindow.ga4.Dispose();
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                            {
                                MainWindow.ga4.DrawImage(gray1, 228, 648, 902, 1353);
                                MainWindow.ga4.DrawImage(MainWindow.temp1_1Front, 0, 0, 2480, 3508);
                            }
                            MainWindow.ga4.Dispose();
                            break;
                        case 2:
                            using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                            {
                                MainWindow.ga4.DrawImage(gray1, 217, 640, 864, 1296);
                                MainWindow.ga4.DrawImage(gray2, 1377, 640, 864, 1296);
                                MainWindow.ga4.DrawImage(MainWindow.temp2_1Front, 0, 0, 2480, 3508);
                            }
                            MainWindow.ga4.Dispose();
                            break;
                        case 3:
                            using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                            {
                                MainWindow.ga4.DrawImage(gray1, 180, 590, 545, 818);
                                MainWindow.ga4.DrawImage(gray2, 180, 1524, 545, 818);
                                MainWindow.ga4.DrawImage(gray3, 954, 1524, 545, 818);
                                MainWindow.ga4.DrawImage(gray4, 1728, 1524, 545, 818);
                                MainWindow.ga4.DrawImage(MainWindow.temp3_1Front, 0, 0, 2480, 3508);
                            }
                            MainWindow.ga4.Dispose();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }


        private void PrintBitmap(Bitmap bitmap, int Copies = 1, bool isLandScape = false, int OffSetX = 0, int OffSetY = 0, int FactorX = 0, int FactorY = 0, bool isMessage = true)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 비트맵 인쇄");
            try
            {
                if (correctedPrintername != null)
                {
                    Source.Log.log.Debug("Print Copy : " + Copies);
                    PrintDocument printDocument = new PrintDocument();
                    printDocument.PrintController = new StandardPrintController();
                    printDocument.DefaultPageSettings.PrinterSettings.PrinterName = correctedPrintername;
                    if (bitmap.Width > bitmap.Height)
                    {
                        isLandScape = true;
                    }
                    else
                    {
                        isLandScape = false;
                    }
                    printDocument.DefaultPageSettings.Landscape = isLandScape;
                    //Margins margins = new Margins(8 + OffSetX, 0, 6 + OffSetY, 0);
                    Margins margins = new Margins(0,0,0,0);
                    if (isLandScape)
                    {
                        margins = new Margins(7 + OffSetY, 0, 2 + OffSetX, 0);
                    }
                    printDocument.DefaultPageSettings.Margins = margins;
                    printDocument.OriginAtMargins = true;
                    printDocument.DefaultPageSettings.PrinterSettings.Copies = (short)Copies;
                    PrinterResolution printerResolution = new PrinterResolution();
                    printerResolution.Kind = PrinterResolutionKind.Custom;
                    printerResolution.X = 600;
                    printerResolution.Y = 600;
                    printDocument.DefaultPageSettings.PrinterResolution = printerResolution;
                    printDocument.PrintPage += delegate (object sender, PrintPageEventArgs args)
                    {
                        System.Drawing.Image image = new Bitmap(bitmap);
                        args.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        if (!isLandScape)
                        {
                            args.Graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, 830, 1170), new System.Drawing.Rectangle(0, 0, 2480, 3508), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            args.Graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, 1169 + FactorY, 827 + FactorX), new System.Drawing.Rectangle(0, 0, 3508, 2480), GraphicsUnit.Pixel);
                        }
                        image.Dispose();
                    };
                    printDocument.Print();
                    bitmap.Dispose();
                    printDocument.Dispose();
                }
                else if (isMessage)
                {
                    System.Windows.MessageBox.Show("출력할 프린터가 없습니다.");
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

        private void print()
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 프린트 이미지 클릭");
            try
            {
                if (Source.OneClick.One_Click())
                {
                    MainWindow.checkupload = false;
                    MainWindow.checkretakenum = 0;

                    string day1 = DateTime.Today.ToString("dd");
                    if (MainWindow.printedpage.ToString() == "")
                    {
                        pagecount = 0;
                        pagecount = pagecount + PageSelect.PaperCount;
                        MainWindow.printedpage.Clear();
                        MainWindow.printedpage.Append(pagecount);
                        WritePrivateProfileString("PrintedPage", day1, Convert.ToString(MainWindow.printedpage), MainWindow.bankbookinipath);
                    }
                    else if (MainWindow.printedpage.ToString() == "0")
                    {
                        pagecount = 0;
                        pagecount = pagecount + PageSelect.PaperCount;
                        MainWindow.printedpage.Clear();
                        MainWindow.printedpage.Append(pagecount);
                        WritePrivateProfileString("PrintedPage", day1, Convert.ToString(MainWindow.printedpage), MainWindow.bankbookinipath);
                    }
                    else
                    {
                        if (MainWindow.day == day1)
                        {
                            pagecount = Convert.ToInt32(MainWindow.printedpage.ToString());
                            pagecount = pagecount + 1;
                            MainWindow.printedpage.Clear();
                            MainWindow.printedpage.Append(pagecount);
                            WritePrivateProfileString("PrintedPage", day1, Convert.ToString(MainWindow.printedpage), MainWindow.bankbookinipath);
                        }
                        else
                        {
                            MainWindow.printedpage.Clear();
                            pagecount = 1;
                            MainWindow.printedpage.Append(pagecount);
                            WritePrivateProfileString("PrintedPage", day1, Convert.ToString(MainWindow.printedpage), MainWindow.bankbookinipath);
                        }
                    }
                    int offSetX = Convert.ToInt32(MainWindow.offsetx);
                    int offSetY = Convert.ToInt32(MainWindow.offsety);
                    int factorX = Convert.ToInt32(MainWindow.factorx);
                    int factorY = Convert.ToInt32(MainWindow.factory);

                    if (MainWindow.Kakaothread == null)
                    { }
                    else
                    {
                        try
                        {
                            if (MainWindow.uploadthread.IsAlive)
                            {
                                Source.Log.log.Info("kakaothread살아있음");
                                MainWindow.uploadthread = null;
                            }
                        }
                        catch (ThreadAbortException threadex)
                        {
                            Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - " + threadex.Message);
                            Source.Log.log.Info("SendKakao thread 종료");
                        }
                    }

                    MainWindow.checkupload = false;

                    MainWindow.hhhcanvas.Save(MainWindow.Printpath + "\\Print" + ".PNG", ImageFormat.Png);
                    ImgCompose.kakaonumbercheck.Clear();
                    ImgCompose.kakaonumbercheck.Append(DateTime.Now.ToString("MMddHHmmss"));
                    ImgCompose.SendKakaoImgName = "KakaoPhoto" + DateTime.Now.ToString(ImgCompose.kakaonumbercheck.ToString()) + ".PNG";
                    MainWindow.hhhcanvas.Save(MainWindow.SnsPath + @"\KakaoPhoto" + DateTime.Now.ToString(ImgCompose.kakaonumbercheck.ToString()) + ".PNG", ImageFormat.Png);
                    MainWindow.hhhcanvas.Save(MainWindow.savekakaopath + @"\KakaoPhoto" + DateTime.Now.ToString(ImgCompose.kakaonumbercheck.ToString()) + ".PNG", ImageFormat.Png);


                    Imgshow2();
                    SetAutoPrinter();
                    PrintBitmap(MainWindow.hhhcanvas, PageSelect.PaperCount, isLandScape: true, offSetX, offSetY, factorX, factorY); //4컷 
                    papercount = Convert.ToInt32(MainWindow.papercount.ToString()) - PageSelect.PaperCount;
                    MainWindow.papercount.Clear();
                    MainWindow.papercount.Append(papercount);
                    WritePrivateProfileString("Setting", "PaperCount", papercount.ToString(), MainWindow.iniPath);
                    WritePrivateProfileString("RemainPaper", day1, papercount.ToString(), MainWindow.bankbookinipath);
                    if (MainWindow.checkvideo == "1")
                    {
                        Onecut.Source.MakingVideo.MakkingVideo();
                        MainWindow.uploadthread = new System.Threading.Thread(new System.Threading.ThreadStart(MainWindow.uploadimgthread));
                        MainWindow.uploadthread.Start();
                    }
                    Dispose();
                    MainWindow.hhhcanvas.Dispose();
                    MainWindow.hhhcanvas = new Bitmap(2480, 3508);
                    MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas);
                    if (MainWindow.inifoldername.Contains("hhh"))
                    {
                        NavigationService.Navigate(new Uri("View/Kakao.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        if (MainWindow.checkkakao == "1" && MainWindow.checkvideo == "0")
                        {
                            bool Isconnected = MainWindow.CheckInternetConnection();
                            if (Isconnected)
                            {
                                SendTotalData();
                            }
                            NavigationService.Navigate(new Uri("View/Final.xaml", UriKind.RelativeOrAbsolute));
                        }
                        else if ((MainWindow.checkkakao == "0" && MainWindow.checkvideo == "0") || (MainWindow.checkkakao == "1" && MainWindow.checkvideo == "1"))
                        {
                            NavigationService.Navigate(new Uri("View/Kakao.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }


        private void SetAutoPrinter()
        {
            PrinterSettings printerSettings = new PrinterSettings();
            correctedPrintername = printerSettings.PrinterName;
        }



        private void Dispose()
        {
            try
            {
                if (MainWindow.optiontempnum == 1)
                {
                    gray1.Dispose();
                    gray1 = null;
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            gray1.Dispose();
                            gray1 = null;
                            break;
                        case 2:
                            gray1.Dispose();
                            gray2.Dispose();
                            gray1 = null;
                            gray2 = null;
                            break;
                        case 3:
                            gray1.Dispose();
                            gray2.Dispose();
                            gray3.Dispose();
                            gray4.Dispose();
                            gray1 = null;
                            gray2 = null;
                            gray3 = null;
                            gray4 = null;
                            break;
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


        #region // DB전송 //

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

                if (MainWindow.checkfreeornot == "1")
                {
                    if (MainWindow.pagenum == "1")
                    {
                        switch (MainWindow.Way.ToString())
                        {
                            case "1":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                break;
                            case "2":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                break;
                            case "3":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + Convert.ToInt32(MainWindow.moneyset) + "&CARD_AMT=" + 0;
                                break;
                            case "4":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + 1 + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                break;
                        }
                    }
                    else
                    {
                        switch (MainWindow.Way.ToString())
                        {
                            case "1":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + PageSelect.PaperCount + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                break;
                            case "2":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + PageSelect.PaperCount + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                break;
                            case "3":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + PageSelect.PaperCount + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + Convert.ToInt32(MainWindow.moneyset) + "&CARD_AMT=" + 0;
                                break;
                            case "4":
                                sendData = "SALE_DT=" + day + "&MACHINE_CD=" + MainWindow.inimachinecode.ToString() + "&PRINT_CNT=" + PageSelect.PaperCount + "&PAYMENT_WAY=" + MainWindow.iniway + "&CASH_AMT=" + 0 + "&CARD_AMT=" + 0;
                                break;
                        }
                    }
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

                Source.Log.log.Info(responseMsg);

                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #endregion
    }
}