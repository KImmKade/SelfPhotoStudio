using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Ports;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EOSDigital.SDK;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using OpenCvSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Threading;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Windows.Interop;
using Azure.Storage.Blobs;
using Touchless.Vision.Camera;
using Azure.Storage.Blobs.Models;
using System.Runtime.InteropServices.ComTypes;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace wpfTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {

        #region///ini///

        public static StringBuilder pagecount = new StringBuilder();
        public static StringBuilder ComNum = new StringBuilder();
        public static StringBuilder checksns = new StringBuilder();
        public static StringBuilder checkfree = new StringBuilder();
        public static StringBuilder inisetmoney = new StringBuilder();
        public static StringBuilder phototime = new StringBuilder();
        public static StringBuilder retake = new StringBuilder();
        public static StringBuilder papercount = new StringBuilder();
        public static StringBuilder getOffSetx = new StringBuilder();
        public static StringBuilder getOffSety = new StringBuilder();
        public static StringBuilder getFactorx = new StringBuilder();
        public static StringBuilder getFactory = new StringBuilder();
        public static StringBuilder collectmoney = new StringBuilder();
        public static StringBuilder camnumber = new StringBuilder();
        public static StringBuilder Zoom = new StringBuilder();
        public static StringBuilder Xcount = new StringBuilder();
        public static StringBuilder Ycount = new StringBuilder();
        public static StringBuilder bright = new StringBuilder();
        public static StringBuilder ContrastRatio = new StringBuilder();
        public static StringBuilder Timer = new StringBuilder();
        public static StringBuilder tempselect = new StringBuilder();
        public static StringBuilder iniKakaoTimer = new StringBuilder();
        public static StringBuilder iniversioncheck = new StringBuilder();
        public static StringBuilder inikakaotextcolor = new StringBuilder();
        public static StringBuilder inicoupon = new StringBuilder();
        public static StringBuilder inicouponkind = new StringBuilder();

        public static StringBuilder initogether = new StringBuilder();

        public static VideoCapture video;

        public static CameraFrameSource WebCamFrameSource;

        public static Touchless.Vision.Camera.Camera Webcam;

        public static int CaptureWidth;

        public static int CaptureHeight;

        public static StringBuilder printedpage = new StringBuilder();
        public static StringBuilder chargedshot = new StringBuilder();
        public static StringBuilder freeshot = new StringBuilder();
        public static StringBuilder couponshot = new StringBuilder();
        public static StringBuilder checkflip = new StringBuilder();
        public static StringBuilder takenshot = new StringBuilder();
        public static StringBuilder tempnumber = new StringBuilder();
        public static StringBuilder ID = new StringBuilder();
        public static StringBuilder Way = new StringBuilder();
        public static StringBuilder foldername = new StringBuilder();
        public static StringBuilder paymentini = new StringBuilder();
        public static StringBuilder inicardnum = new StringBuilder();
        public static StringBuilder pagenumini = new StringBuilder();
        public static StringBuilder useqr = new StringBuilder();
        public static StringBuilder useselecttemp = new StringBuilder();
        public static StringBuilder inipic1startx = new StringBuilder();
        public static StringBuilder inipic1starty = new StringBuilder();
        public static StringBuilder inipic1width = new StringBuilder();
        public static StringBuilder inipic1height = new StringBuilder();
        public static StringBuilder inipic2startx = new StringBuilder();
        public static StringBuilder inipic2starty = new StringBuilder();
        public static StringBuilder inipic2width = new StringBuilder();
        public static StringBuilder inipic2height = new StringBuilder();
        public static StringBuilder inipic3startx = new StringBuilder();
        public static StringBuilder inipic3starty = new StringBuilder();
        public static StringBuilder inipic3width = new StringBuilder();
        public static StringBuilder inipic3height = new StringBuilder();
        public static StringBuilder inipic4startx = new StringBuilder();
        public static StringBuilder inipic4starty = new StringBuilder();
        public static StringBuilder inipic4width = new StringBuilder();
        public static StringBuilder inipic4height = new StringBuilder();
        public static StringBuilder inipic5startx = new StringBuilder();
        public static StringBuilder inipic5starty = new StringBuilder();
        public static StringBuilder inipic5width = new StringBuilder();
        public static StringBuilder inipic5height = new StringBuilder();
        public static StringBuilder inipic6startx = new StringBuilder();
        public static StringBuilder inipic6starty = new StringBuilder();
        public static StringBuilder inipic6width = new StringBuilder();
        public static StringBuilder inipic6height = new StringBuilder();

        public static StringBuilder iniqrstartx = new StringBuilder();
        public static StringBuilder iniqrstarty = new StringBuilder();
        public static StringBuilder iniqrwidth = new StringBuilder();
        public static StringBuilder iniqrheight = new StringBuilder();
        public static StringBuilder inipicratio = new StringBuilder();
        public static StringBuilder iniprinterratio = new StringBuilder();
        public static StringBuilder iniphotonum = new StringBuilder();
        public static StringBuilder initextcolor = new StringBuilder();
        public static StringBuilder iniqrpageuse = new StringBuilder();
        public static StringBuilder inidatetext = new StringBuilder();
        public static StringBuilder inidatetextcolor = new StringBuilder();
        public static StringBuilder iniautologin = new StringBuilder();
        public static StringBuilder iniuserid = new StringBuilder();
        public static StringBuilder iniuserpassword = new StringBuilder(255);
        public static StringBuilder iniSorR = new StringBuilder();
        public static StringBuilder initemp2fee = new StringBuilder();
        public static StringBuilder initemp3fee = new StringBuilder();
        public static StringBuilder initimerlocation = new StringBuilder();
        public static StringBuilder iniCheckVideo = new StringBuilder();
        public static StringBuilder inicheckcolor = new StringBuilder();

        // 납품 템플릿 설정

        public static StringBuilder initemp1printerratio = new StringBuilder();
        public static StringBuilder initemp2printerratio = new StringBuilder();
        public static StringBuilder initemp3printerratio = new StringBuilder();

        public static StringBuilder initemp1picratio = new StringBuilder();
        public static StringBuilder initemp2picratio = new StringBuilder();
        public static StringBuilder initemp3picratio = new StringBuilder();

        public static StringBuilder initemp1pic1startx = new StringBuilder();
        public static StringBuilder initemp1pic1starty = new StringBuilder();
        public static StringBuilder initemp1pic1width = new StringBuilder();
        public static StringBuilder initemp1pic1height = new StringBuilder();

        public static StringBuilder initemp1pic2startx = new StringBuilder();
        public static StringBuilder initemp1pic2starty = new StringBuilder();
        public static StringBuilder initemp1pic2width = new StringBuilder();
        public static StringBuilder initemp1pic2height = new StringBuilder();

        public static StringBuilder initemp1pic3startx = new StringBuilder();
        public static StringBuilder initemp1pic3starty = new StringBuilder();
        public static StringBuilder initemp1pic3width = new StringBuilder();
        public static StringBuilder initemp1pic3height = new StringBuilder();

        public static StringBuilder initemp1pic4startx = new StringBuilder();
        public static StringBuilder initemp1pic4starty = new StringBuilder();
        public static StringBuilder initemp1pic4width = new StringBuilder();
        public static StringBuilder initemp1pic4height = new StringBuilder();

        public static StringBuilder initemp2pic1startx = new StringBuilder();
        public static StringBuilder initemp2pic1starty = new StringBuilder();
        public static StringBuilder initemp2pic1width = new StringBuilder();
        public static StringBuilder initemp2pic1height = new StringBuilder();

        public static StringBuilder initemp2pic2startx = new StringBuilder();
        public static StringBuilder initemp2pic2starty = new StringBuilder();
        public static StringBuilder initemp2pic2width = new StringBuilder();
        public static StringBuilder initemp2pic2height = new StringBuilder();

        public static StringBuilder initemp3pic1startx = new StringBuilder();
        public static StringBuilder initemp3pic1starty = new StringBuilder();
        public static StringBuilder initemp3pic1width = new StringBuilder();
        public static StringBuilder initemp3pic1height = new StringBuilder();

        public static StringBuilder initemp3pic2startx = new StringBuilder();
        public static StringBuilder initemp3pic2starty = new StringBuilder();
        public static StringBuilder initemp3pic2width = new StringBuilder();
        public static StringBuilder initemp3pic2height = new StringBuilder();

        public static StringBuilder initemp3pic3startx = new StringBuilder();
        public static StringBuilder initemp3pic3starty = new StringBuilder();
        public static StringBuilder initemp3pic3width = new StringBuilder();
        public static StringBuilder initemp3pic3height = new StringBuilder();

        public static StringBuilder initemp3pic4startx = new StringBuilder();
        public static StringBuilder initemp3pic4starty = new StringBuilder();
        public static StringBuilder initemp3pic4width = new StringBuilder();
        public static StringBuilder initemp3pic4height = new StringBuilder();

        public static StringBuilder initemp3pic5startx = new StringBuilder();
        public static StringBuilder initemp3pic5starty = new StringBuilder();
        public static StringBuilder initemp3pic5width = new StringBuilder();
        public static StringBuilder initemp3pic5height = new StringBuilder();

        public static StringBuilder initemp3pic6startx = new StringBuilder();
        public static StringBuilder initemp3pic6starty = new StringBuilder();
        public static StringBuilder initemp3pic6width = new StringBuilder();
        public static StringBuilder initemp3pic6height = new StringBuilder();

        #endregion

        #region///전역변수///

        public static string Version;
        public static int cycle = 0;
        public static string count; // 페이지 카운트
        public static string comportnum; // 컴포트 넘버
        public static string savePath = AppDomain.CurrentDomain.BaseDirectory; // 현재 실행중인 디렉토리 경로 불러오기
        public static string iniPath;
        public static string cardinipath;
        public static string PhotoPath;
        public static string ResizePath;
        public static string TempPath;
        public static string GrayFilterPath;
        public static string Printpath;
        public static string SnsPath;
        public static string testprintpath;
        public static string bankbookinipath;
        public static string screenshotpath;
        public static string contrastpath;
        public static string soundpath;
        public static string jsonpath;
        public static string savekakaopath;
        public static string uipath;
        public static string checkkakao;// 카카오톡 확인
        public static string checkfreeornot; // 유료무료 확인
        public static string moneyset; // 돈 셋팅
        public static string checkretake; // 재촬영 확인
        public static string photocount; // 사진 카운트
        public static string offsetx; // offsetx
        public static string offsety; // offsety
        public static string factorx; // factorx
        public static string factory; // factory
        public static string checktempselect; // 템플릿 선택 확인
        public static string day;
        public static string month;
        public static string checktimeruse; // 타이머 사용여부
        public static string checkingflip; // 좌우반전 사용여부
        public static string logpath;
        public static string iniway;
        public static string inifoldername;
        public static string inimachinecode;
        public static string paymentway;
        public static string Videopath;
        public static string pagenum;
        public static int qroption;
        public static int tempoption;
        public static int optiontempnum;
        public static int picratio;
        public static int printerratio;
        public static int photonum;
        public static string textcolor;
        public static string datetextcolor;
        public static int qrpageuse;
        public static int datetext;
        public static int kakaotimer;
        public static string SorR;
        public static string temp2fee;
        public static string temp3fee;
        public static string versionpath;
        public static string kakaotextcolor;
        public static string autologin;
        public static string coupon;
        public static string couponkind;
        public static string htmlUrl;
        public static bool checknokakaobtn = false;

        public static string checkingtogether;

        public static bool checkupload = false;
        public static string checkcolor;

        public static string phpurl;

        public static string connectionString;
        public static string containerName;

        public static string timerlocation;

        public static string Updateprogrampath;

        // 납품 템플릿 셋팅

        public static int temp1printerratio;
        public static int temp2printerratio;
        public static int temp3printerratio;

        public static int temp1picratio;
        public static int temp2picratio;
        public static int temp3picratio;

        public static int temp1pic1startx;
        public static int temp1pic1starty;
        public static int temp1pic1width;
        public static int temp1pic1height;

        public static int temp1pic2startx;
        public static int temp1pic2starty;
        public static int temp1pic2width;
        public static int temp1pic2height;

        public static int temp1pic3startx;
        public static int temp1pic3starty;
        public static int temp1pic3width;
        public static int temp1pic3height;

        public static int temp1pic4startx;
        public static int temp1pic4starty;
        public static int temp1pic4width;
        public static int temp1pic4height;

        public static int temp2pic1startx;
        public static int temp2pic1starty;
        public static int temp2pic1width;
        public static int temp2pic1height;

        public static int temp2pic2startx;
        public static int temp2pic2starty;
        public static int temp2pic2width;
        public static int temp2pic2height;

        public static int temp3pic1startx;
        public static int temp3pic1starty;
        public static int temp3pic1width;
        public static int temp3pic1height;

        public static int temp3pic2startx;
        public static int temp3pic2starty;
        public static int temp3pic2width;
        public static int temp3pic2height;

        public static int temp3pic3startx;
        public static int temp3pic3starty;
        public static int temp3pic3width;
        public static int temp3pic3height;

        public static int temp3pic4startx;
        public static int temp3pic4starty;
        public static int temp3pic4width;
        public static int temp3pic4height;

        public static int temp3pic5startx;
        public static int temp3pic5starty;
        public static int temp3pic5width;
        public static int temp3pic5height;

        public static int temp3pic6startx;
        public static int temp3pic6starty;
        public static int temp3pic6width;
        public static int temp3pic6height;

        public static Bitmap canvus = new Bitmap(600, 1800);
        public static Graphics d = Graphics.FromImage(canvus);
        public static Bitmap bigcanvus = new Bitmap(1800, 1200);
        public static Bitmap bigcanvus2 = new Bitmap(1200, 1800);
        public static Graphics r = Graphics.FromImage(bigcanvus);
        public static Graphics g = Graphics.FromImage(bigcanvus2);
        public static Bitmap canvus2_3 = new Bitmap(1200, 1800);
        public static Graphics g23 = Graphics.FromImage(canvus2_3);
        public static Bitmap hhhcanvas = new Bitmap(2480, 3508);
        public static Graphics ga4 = Graphics.FromImage(hhhcanvas);
        public static int checkretakenum = 0;
        public static string phonenumber = "";
        public static int checkuploadlog = 0;
        public static string checkvideo;

        public static SoundPlayer fivetik = new SoundPlayer();
        public static SoundPlayer fourtik = new SoundPlayer();
        public static SoundPlayer threetik = new SoundPlayer();
        public static SoundPlayer twotik = new SoundPlayer();
        public static SoundPlayer onetik = new SoundPlayer();
        public static SoundPlayer shot = new SoundPlayer();
        public static int updowncount = 0; // 카메라 y축 움직임
        public static int leftrightcount = 0; // 카메라 x축 움직임
        public static double zoomratio = 0; // 카메라 줌

        public static BitmapImage firstphoto = new BitmapImage();
        public static BitmapImage secondphoto = new BitmapImage();
        public static BitmapImage thirdphoto = new BitmapImage();
        public static BitmapImage fourthphoto = new BitmapImage();
        public static BitmapImage fifthphoto = new BitmapImage();
        public static BitmapImage sixthphoto = new BitmapImage();
        public static BitmapImage seventhphoto = new BitmapImage();
        public static BitmapImage eighthphoto = new BitmapImage();

        public static string filename;

        // 페이지

        public static BitmapImage main;
        public static BitmapImage pageselect;
        public static BitmapImage payment;
        public static BitmapImage liveview;
        public static BitmapImage preparing;
        public static BitmapImage imgcompose;
        public static BitmapImage kakaopage;
        public static BitmapImage final;
        public static BitmapImage choicepayment;
        public static BitmapImage credit;
        public static BitmapImage tempselectpage;
        public static BitmapImage Selectpicpage1;
        public static BitmapImage Selectpicpage2;
        public static BitmapImage Selectpicpage3;
        public static BitmapImage couponpage;

        // 버튼

        public static BitmapImage Backimg;
        public static BitmapImage coloroff;
        public static BitmapImage coloron;
        public static BitmapImage grayoff;
        public static BitmapImage grayon;
        public static BitmapImage kakaobtn;
        public static BitmapImage nextbtn;
        public static BitmapImage nextcolor;
        public static BitmapImage backcolor;
        public static BitmapImage number0;
        public static BitmapImage number1;
        public static BitmapImage number2;
        public static BitmapImage number3;
        public static BitmapImage number4;
        public static BitmapImage number5;
        public static BitmapImage number6;
        public static BitmapImage number7;
        public static BitmapImage number8;
        public static BitmapImage number9;
        public static BitmapImage delbtn;
        public static BitmapImage printbtn;
        public static BitmapImage retakebtn;
        public static BitmapImage touchbtn;
        public static BitmapImage nokakaobtn;
        public static BitmapImage card_off;
        public static BitmapImage card_on;
        public static BitmapImage cash_off;
        public static BitmapImage cash_on;
        public static BitmapImage creditfail;
        public static BitmapImage credittimeout;
        public static BitmapImage crediterror;
        public static BitmapImage check;
        public static BitmapImage paymentcomplete;
        public static BitmapImage QRpage;
        public static BitmapImage composeimg;
        public static BitmapImage composeimg2;

        public static BitmapImage temp1_pick;
        public static BitmapImage temp2_pick;
        public static BitmapImage temp3_pick;
        public static BitmapImage temp1_nonpick;
        public static BitmapImage temp2_nonpick;
        public static BitmapImage temp3_nonpick;

        //serialcheck2 저역변수

        public static int brightvalue;
        public static int crvalue;

        // IMgCompose 변수//

        public static System.Drawing.Image temp1_1;
        public static System.Drawing.Image temp1_2;
        public static System.Drawing.Image temp1_3;
        public static System.Drawing.Image temp1_4;
        public static System.Drawing.Image temp1_5;
        public static System.Drawing.Image temp1_6;
        public static System.Drawing.Image temp1_7;
        public static System.Drawing.Image temp1_8;

        public static System.Drawing.Image temp1_1Front;
        public static System.Drawing.Image temp1_2Front;
        public static System.Drawing.Image temp1_3Front;
        public static System.Drawing.Image temp1_4Front;
        public static System.Drawing.Image temp1_5Front;
        public static System.Drawing.Image temp1_6Front;
        public static System.Drawing.Image temp1_7Front;
        public static System.Drawing.Image temp1_8Front;

        public static System.Drawing.Image temp2_1;
        public static System.Drawing.Image temp2_2;
        public static System.Drawing.Image temp2_3;
        public static System.Drawing.Image temp2_4;
        public static System.Drawing.Image temp2_5;
        public static System.Drawing.Image temp2_6;
        public static System.Drawing.Image temp2_7;
        public static System.Drawing.Image temp2_8;
        public static System.Drawing.Image temp2_1Front;
        public static System.Drawing.Image temp2_2Front;
        public static System.Drawing.Image temp2_3Front;
        public static System.Drawing.Image temp2_4Front;
        public static System.Drawing.Image temp2_5Front;
        public static System.Drawing.Image temp2_6Front;
        public static System.Drawing.Image temp2_7Front;
        public static System.Drawing.Image temp2_8Front;

        public static System.Drawing.Image temp3_1;
        public static System.Drawing.Image temp3_2;
        public static System.Drawing.Image temp3_3;
        public static System.Drawing.Image temp3_4;
        public static System.Drawing.Image temp3_5;
        public static System.Drawing.Image temp3_6;
        public static System.Drawing.Image temp3_7;
        public static System.Drawing.Image temp3_8;
        public static System.Drawing.Image temp3_1Front;
        public static System.Drawing.Image temp3_2Front;
        public static System.Drawing.Image temp3_3Front;
        public static System.Drawing.Image temp3_4Front;
        public static System.Drawing.Image temp3_5Front;
        public static System.Drawing.Image temp3_6Front;
        public static System.Drawing.Image temp3_7Front;
        public static System.Drawing.Image temp3_8Front;

        public static System.Drawing.Image temp4_1;
        public static System.Drawing.Image temp4_1Front;
        public static System.Drawing.Image temp4_2;
        public static System.Drawing.Image temp4_2Front;

        public static System.Drawing.Image temp5_1;
        public static System.Drawing.Image temp5_1Front;
        public static System.Drawing.Image temp5_2;
        public static System.Drawing.Image temp5_2Front;

        public static System.Drawing.Image temp6_1;
        public static System.Drawing.Image temp6_1Front;
        public static System.Drawing.Image temp6_2;
        public static System.Drawing.Image temp6_2Front;

        public static int pic1startx;
        public static int pic1starty;
        public static int pic1width;
        public static int pic1height;
        public static int pic2startx;
        public static int pic2starty;
        public static int pic2width;
        public static int pic2height;
        public static int pic3startx;
        public static int pic3starty;
        public static int pic3width;
        public static int pic3height;
        public static int pic4startx;
        public static int pic4starty;
        public static int pic4width;
        public static int pic4height;
        public static int pic5startx;
        public static int pic5starty;
        public static int pic5width;
        public static int pic5height;
        public static int pic6startx;
        public static int pic6starty;
        public static int pic6width;
        public static int pic6height;

        public static int qrstartx;
        public static int qrstarty;
        public static int qrwidth;
        public static int qrheight;

        public static string cardnum;

        public static int checkrepair = 0;

        TcpClient clientSocket = new TcpClient();
        public static NetworkStream stream = default(NetworkStream);
        string message = string.Empty;

        public static Thread Kakaothread;
        public static Thread uploadthread;
        public static Thread printimgthread;

        public static System.Windows.Media.Brush textbrush;
        public static Brush datetextcolorbrush;

        public static System.Windows.Media.Brush fontbrush;

        // Admin PassWord 변수//

        public static int Adminlogin = 0;

        // 로그 업로드 타이머

        System.Timers.Timer timer;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        #region///INI Import///

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.F4)
                {
                    if (MessageBox.Show("종료 하시겠습니까?", "경고", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | F4 입력(프로그램 종료 키)");
                        if (View.Main.MainCamera?.SessionOpen == true)
                        {
                            if (View.Main.MainCamera.IsLiveViewOn)
                            {
                                View.Main.MainCamera.StopLiveView();
                            }
                            View.Main.MainCamera.CloseSession();
                            Source.Log.log.Info("캐논 카메라 종료");
                        }
                        if (View.Main.port?.IsOpen == true)
                        {
                            View.Payment.DataSend('S', 'A', '\u000e');
                        }
                        ProgramClose();
                    }
                }
                else if (e.Key == Key.F9)
                {
                    Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() | F9 입력(수리 대기장 키)");
                    if (checkrepair == 0)
                    {
                        checkrepair = 1;
                    }
                    else
                    {
                        checkrepair = 0;
                    }
                }
                else if (e.Key == Key.F8) // 결제창 스킵
                {
                    Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | F8 입력(무료촬영 키)");
                    View.Payment.Pm_Global.Pm_Insert = View.Payment.Pm_Global.Pm_Total;
                    View.PageSelect.checkskip = 1;
                }
                else if (e.Key == Key.F12)
                {
                    Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | F12 입력(설정창 진입 키)");
                    View.Main.MCheckNum = "66776677";
                }
                else if (e.Key == Key.System && e.SystemKey == Key.F4)
                {
                    Source.Log.log.Info("Alt + F4 눌림");
                    e.Handled = true;
                }
            }
            catch(Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() 에러메세지 : " + ex.Message);
            }
        }

        public static void ProgramClose()
        {
            Source.Log.log.Info("프로그램 종료");
            Environment.Exit(0);
        }

        /// <summary>
        /// 프로그램 셋팅값 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() Start : 설정 값 로드 중");
            try
            {

                Process[] processes = Process.GetProcesses();

                StringBuilder checklogin = new StringBuilder();

                GetPrivateProfileString("Checklogin", "checklogin", "", checklogin, checklogin.Capacity, savePath + @"Data\Check");

                List<Process> processList = new List<Process>();

                foreach (Process process in processes)
                {
                    processList.Add(process);
                }

                bool updaterProcessExists = processList.Exists(process =>
                process.ProcessName.Equals("Onecut", StringComparison.OrdinalIgnoreCase));

                if (updaterProcessExists && checklogin.ToString() == "true")
                {
                    DeleteFile();

                    //셋팅값 주소 로드

                    iniPath = savePath + @"Data\Setting.ini";
                    versionpath = savePath + @"Data\Version.ini";
                    PhotoPath = savePath + @"IMG\Photo";
                    ResizePath = savePath + @"IMG\Resize";
                    TempPath = savePath + @"IMG\Temp";
                    GrayFilterPath = savePath + @"IMG\Filter\Gray";
                    Printpath = savePath + @"IMG\PrintImg";
                    SnsPath = savePath + @"IMG\Kakao";
                    testprintpath = savePath + @"Img\TestImg";
                    savekakaopath = savePath + @"IMG\savekakao";
                    string month = DateTime.Today.ToString("yyyy-MM");
                    bankbookinipath = savePath + @"Data\BankBookData\" + month + ".ini";
                    cardinipath = savePath + @"Data\BankBookData\cardrecord.ini";
                    screenshotpath = savePath + @"IMG\ScreenShot";
                    contrastpath = savePath + @"IMG\Filter\Contrast";
                    logpath = savePath + @"log";
                    soundpath = savePath + @"\Sound";
                    uipath = savePath + @"IMG\UI";
                    Videopath = savePath + @"\Video";

                    Updateprogrampath = savePath + @"\updater\Updater.exe";

                    //셋팅값 로드

                    d.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    r.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    day = DateTime.Today.ToString("dd");
                    month = DateTime.Today.ToString("yyyy-MM");
                    GetPrivateProfileString("Setting", "PageTimer", "", pagecount, pagecount.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "ComportNumber", "", ComNum, ComNum.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "Kakao", "", checksns, checksns.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "BillMachineUse", "", checkfree, checkfree.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "TempFee", "", inisetmoney, inisetmoney.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "ReTake", "", retake, retake.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "PhotoTimer", "", phototime, phototime.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "Papercount", "", papercount, papercount.Capacity, iniPath);
                    GetPrivateProfileString("PrinterSetting", "OffSetX", "", getOffSetx, getOffSetx.Capacity, iniPath);
                    GetPrivateProfileString("PrinterSetting", "OffSetY", "", getOffSety, getOffSety.Capacity, iniPath);
                    GetPrivateProfileString("PrinterSetting", "FactorX", "", getFactorx, getFactorx.Capacity, iniPath);
                    GetPrivateProfileString("PrinterSetting", "FactorY", "", getFactory, getFactory.Capacity, iniPath);
                    GetPrivateProfileString("Money", day, "", collectmoney, collectmoney.Capacity, bankbookinipath);
                    GetPrivateProfileString("FilterSetting", "Brightness", "", bright, bright.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "CamVersion", "", camnumber, camnumber.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "Zoom", "", Zoom, Zoom.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "leftrightcount", "", Xcount, Xcount.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "updowncount", "", Ycount, Ycount.Capacity, iniPath);
                    GetPrivateProfileString("FilterSetting", "Contrastratio", "", ContrastRatio, ContrastRatio.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "TimerUse", "", Timer, Timer.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "Temp", "", tempselect, tempselect.Capacity, iniPath);
                    GetPrivateProfileString("PrintedPage", day, "", printedpage, printedpage.Capacity, bankbookinipath);
                    GetPrivateProfileString("ChargedShot", day, "", chargedshot, chargedshot.Capacity, bankbookinipath);
                    GetPrivateProfileString("FreeShot", day, "", freeshot, freeshot.Capacity, bankbookinipath);
                    GetPrivateProfileString("CouponShot", day, "", couponshot, couponshot.Capacity, bankbookinipath);
                    GetPrivateProfileString("TakenShot", day, "", takenshot, takenshot.Capacity, bankbookinipath);
                    GetPrivateProfileString("Setting", "FlipUse", "", checkflip, checkflip.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "TempNumber", "", tempnumber, tempnumber.Capacity, iniPath);
                    GetPrivateProfileString("ApiTest", "MachineID", "", ID, ID.Capacity, iniPath);
                    GetPrivateProfileString("ApiTest", "PayWay", "", Way, Way.Capacity, iniPath);
                    GetPrivateProfileString("ApiTest", "foldername", "", foldername, foldername.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "paymentway", "", paymentini, paymentini.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "cardnum", "", inicardnum, inicardnum.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "pagenum", "", pagenumini, pagenumini.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "useqr", "", useqr, useqr.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "selecttemp", "", useselecttemp, useselecttemp.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic1startx", "", inipic1startx, inipic1startx.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic2startx", "", inipic2startx, inipic2startx.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic3startx", "", inipic3startx, inipic3startx.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic4startx", "", inipic4startx, inipic4startx.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic5startx", "", inipic5startx, inipic5startx.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic6startx", "", inipic6startx, inipic6startx.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic1starty", "", inipic1starty, inipic1starty.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic2starty", "", inipic2starty, inipic2starty.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic3starty", "", inipic3starty, inipic3starty.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic4starty", "", inipic4starty, inipic4starty.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic5starty", "", inipic5starty, inipic5starty.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic6starty", "", inipic6starty, inipic6starty.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic1width", "", inipic1width, inipic1width.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic2width", "", inipic2width, inipic2width.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic3width", "", inipic3width, inipic3width.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic4width", "", inipic4width, inipic4width.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic5width", "", inipic5width, inipic5width.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic6width", "", inipic6width, inipic6width.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic1height", "", inipic1height, inipic1height.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic2height", "", inipic2height, inipic2height.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic3height", "", inipic3height, inipic3height.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic4height", "", inipic4height, inipic4height.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic5height", "", inipic5height, inipic5height.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "pic6height", "", inipic6height, inipic6height.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "QRstartx", "", iniqrstartx, iniqrstartx.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "QRstarty", "", iniqrstarty, iniqrstarty.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "QRwidth", "", iniqrwidth, iniqrwidth.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "QRheight", "", iniqrheight, iniqrheight.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "picratio", "", inipicratio, inipicratio.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "printerratio", "", iniprinterratio, iniprinterratio.Capacity, iniPath);
                    GetPrivateProfileString("TempSetting", "picnum", "", iniphotonum, iniphotonum.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "textcolor", "", initextcolor, initextcolor.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "qrpageuse", "", iniqrpageuse, iniqrpageuse.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "DateText", "", inidatetext, inidatetext.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "datetextcolor", "", inidatetextcolor, inidatetextcolor.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "autologin", "", iniautologin, iniautologin.Capacity, iniPath);
                    GetPrivateProfileString("Login", "ID", "", iniuserid, iniuserid.Capacity, iniPath);
                    GetPrivateProfileString("Login", "PassWord", "", iniuserpassword, iniuserpassword.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "KakaoTimer", "", iniKakaoTimer, iniKakaoTimer.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "SorR", "", iniSorR, iniSorR.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "Temp2Fee", "", initemp2fee, initemp2fee.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "Temp3Fee", "", initemp3fee, initemp3fee.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "timerlocation", "", initimerlocation, initimerlocation.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "Video", "", iniCheckVideo, iniCheckVideo.Capacity, iniPath);
                    GetPrivateProfileString("Version", "Version", "", iniversioncheck, iniversioncheck.Capacity, versionpath);
                    GetPrivateProfileString("Setting", "kakaotextcolor", "", inikakaotextcolor, inikakaotextcolor.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "coupon", "", inicoupon, inicoupon.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "couponkind", "", inicouponkind, inicouponkind.Capacity, iniPath);
                    GetPrivateProfileString("Setting", "coloruse", "", inicheckcolor, inicheckcolor.Capacity, iniPath);

                    #region // 납품 템플릿 이미지 설정 불러오기 //

                    // 템플릿 1번

                    GetPrivateProfileString("SellTemp1", "picratio", "", initemp1picratio, initemp1picratio.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "temp1printerratio ", "", initemp1printerratio, initemp1printerratio.Capacity, iniPath);

                    GetPrivateProfileString("SellTemp1", "pic1startx", "", initemp1pic1startx, initemp1pic1startx.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic1starty", "", initemp1pic1starty, initemp1pic1starty.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic1width", "", initemp1pic1width, initemp1pic1width.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic1height", "", initemp1pic1height, initemp1pic1height.Capacity, iniPath);

                    GetPrivateProfileString("SellTemp1", "pic2startx", "", initemp1pic2startx, initemp1pic2startx.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic2starty", "", initemp1pic2starty, initemp1pic2starty.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic2width", "", initemp1pic2width, initemp1pic2width.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic2height", "", initemp1pic2height, initemp1pic2height.Capacity, iniPath);

                    GetPrivateProfileString("SellTemp1", "pic3startx", "", initemp1pic3startx, initemp1pic3startx.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic3starty", "", initemp1pic3starty, initemp1pic3starty.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic3width", "", initemp1pic3width, initemp1pic3width.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic3height", "", initemp1pic3height, initemp1pic3height.Capacity, iniPath);

                    GetPrivateProfileString("SellTemp1", "pic4startx", "", initemp1pic4startx, initemp1pic4startx.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic4starty", "", initemp1pic4starty, initemp1pic4starty.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic4width", "", initemp1pic4width, initemp1pic4width.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp1", "pic4height", "", initemp1pic4height, initemp1pic4height.Capacity, iniPath);

                    //템플릿 2번

                    GetPrivateProfileString("SellTemp2", "picratio", "", initemp2picratio, initemp2picratio.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp2", "temp2printerratio ", "", initemp2printerratio, initemp2printerratio.Capacity, iniPath);

                    GetPrivateProfileString("SellTemp2", "pic1startx", "", initemp2pic1startx, initemp2pic1startx.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp2", "pic1starty", "", initemp2pic1starty, initemp2pic1starty.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp2", "pic1width", "", initemp2pic1width, initemp2pic1width.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp2", "pic1height", "", initemp2pic1height, initemp2pic1height.Capacity, iniPath);

                    GetPrivateProfileString("SellTemp2", "pic2startx", "", initemp2pic2startx, initemp2pic2startx.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp2", "pic2starty", "", initemp2pic2starty, initemp2pic2starty.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp2", "pic2width", "", initemp2pic2width, initemp2pic2width.Capacity, iniPath);
                    GetPrivateProfileString("SellTemp2", "pic2height", "", initemp2pic2height, initemp2pic2height.Capacity, iniPath);

                    //템플릿 3번

                    GetPrivateProfileString("Selltemp3", "picratio", "", initemp3picratio, initemp3picratio.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "temp3printerratio ", "", initemp3printerratio, initemp3printerratio.Capacity, iniPath);

                    GetPrivateProfileString("Selltemp3", "pic1startx", "", initemp3pic1startx, initemp3pic1startx.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic1starty", "", initemp3pic1starty, initemp3pic1starty.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic1width", "", initemp3pic1width, initemp3pic1width.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic1height", "", initemp3pic1height, initemp3pic1height.Capacity, iniPath);

                    GetPrivateProfileString("Selltemp3", "pic2startx", "", initemp3pic2startx, initemp3pic2startx.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic2starty", "", initemp3pic2starty, initemp3pic2starty.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic2width", "", initemp3pic2width, initemp3pic2width.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic2height", "", initemp3pic2height, initemp3pic2height.Capacity, iniPath);

                    GetPrivateProfileString("Selltemp3", "pic3startx", "", initemp3pic3startx, initemp3pic3startx.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic3starty", "", initemp3pic3starty, initemp3pic3starty.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic3width", "", initemp3pic3width, initemp3pic3width.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic3height", "", initemp3pic3height, initemp3pic3height.Capacity, iniPath);

                    GetPrivateProfileString("Selltemp3", "pic4startx", "", initemp3pic4startx, initemp3pic4startx.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic4starty", "", initemp3pic4starty, initemp3pic4starty.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic4width", "", initemp3pic4width, initemp3pic4width.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic4height", "", initemp3pic4height, initemp3pic4height.Capacity, iniPath);

                    GetPrivateProfileString("Selltemp3", "pic5startx", "", initemp3pic5startx, initemp3pic5startx.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic5starty", "", initemp3pic5starty, initemp3pic5starty.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic5width", "", initemp3pic5width, initemp3pic5width.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic5height", "", initemp3pic5height, initemp3pic5height.Capacity, iniPath);

                    GetPrivateProfileString("Selltemp3", "pic6startx", "", initemp3pic6startx, initemp3pic6startx.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic6starty", "", initemp3pic6starty, initemp3pic6starty.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic6width", "", initemp3pic6width, initemp3pic6width.Capacity, iniPath);
                    GetPrivateProfileString("Selltemp3", "pic6height", "", initemp3pic6height, initemp3pic6height.Capacity, iniPath);

                    #endregion

                    if (printedpage.ToString() == "")
                    {
                        printedpage.Clear();
                        printedpage.Append("0");
                    }
                    if (chargedshot.ToString() == "")
                    {
                        chargedshot.Clear();
                        chargedshot.Append("0");
                    }
                    if (freeshot.ToString() == "")
                    {
                        freeshot.Clear();
                        freeshot.Append("0");
                    }
                    if (couponshot.ToString() == "")
                    {
                        couponshot.Clear();
                        couponshot.Append("0");
                    }
                    if (takenshot.ToString() == "")
                    {
                        takenshot.Clear();
                        takenshot.Append("0");
                    }
                    if (collectmoney.ToString() == "")
                    {
                        collectmoney.Clear();
                        collectmoney.Append(0);
                    }

                    string filepath = bankbookinipath;
                    FileInfo fileinfo = new FileInfo(filepath);
                    if (!fileinfo.Exists)
                    {
                        using (File.Create(filepath)) { }
                        for (int i = 0; i < 31; i++)
                        {
                            if (i < 9)
                            {
                                WritePrivateProfileString("Money", "0" + (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("PrintedPage", "0" + (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("RemainPaper", "0" + (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("TakenShot", "0" + (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("ChargedShot", "0" + (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("FreeShot", "0" + (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("CouponShot", "0" + (i + 1).ToString(), "0", filepath);
                            }
                            else
                            {
                                WritePrivateProfileString("Money", (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("PrintedPage", (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("RemainPaper", (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("TakenShot", (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("ChargedShot", (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("FreeShot", (i + 1).ToString(), "0", filepath);
                                WritePrivateProfileString("CouponShot", (i + 1).ToString(), "0", filepath);
                            }
                        }
                    }

                    DateTime nowdate = DateTime.Now;
                    int nowyear = nowdate.Year;
                    int nowmonth = nowdate.Month;
                    if (File.Exists(MainWindow.savePath + @"Data\BankBookData\cardrecord" + nowyear + "." + nowmonth + ".txt"))
                    {

                    }
                    else
                    {
                        File.WriteAllText(MainWindow.savePath + @"Data\BankBookData\cardrecord" + nowyear + "." + nowmonth + ".txt", string.Empty);
                    }


                    fivetik.SoundLocation = soundpath + @"\Counter_5.wav";
                    fourtik.SoundLocation = soundpath + @"\Counter_4.wav";
                    threetik.SoundLocation = soundpath + @"\Counter_3.wav";
                    twotik.SoundLocation = soundpath + @"\Counter_2.wav";
                    onetik.SoundLocation = soundpath + @"\Counter_1.wav";
                    shot.SoundLocation = soundpath + @"\Counter_0.wav";

                    // Serialcheck 2
                    brightvalue = Convert.ToInt32(bright.ToString());
                    crvalue = Convert.ToInt32(ContrastRatio.ToString());

                    //Serialcheck 1
                    checkretake = retake.ToString();
                    checkkakao = checksns.ToString();
                    checkfreeornot = checkfree.ToString();
                    checktimeruse = Timer.ToString();
                    count = pagecount.ToString();
                    photocount = phototime.ToString();
                    moneyset = inisetmoney.ToString();
                    offsetx = getOffSetx.ToString();
                    offsety = getOffSety.ToString();
                    factorx = getFactorx.ToString();
                    factory = getFactory.ToString();
                    checktempselect = tempselect.ToString();
                    comportnum = ComNum.ToString();
                    leftrightcount = Convert.ToInt32(Xcount.ToString());
                    updowncount = Convert.ToInt32(Ycount.ToString());
                    zoomratio = Convert.ToDouble(Zoom.ToString());
                    checkingflip = checkflip.ToString();
                    inifoldername = foldername.ToString();
                    iniway = Way.ToString();
                    inimachinecode = ID.ToString();
                    paymentway = paymentini.ToString();
                    pagenum = pagenumini.ToString();
                    qroption = Convert.ToInt32(useqr.ToString());
                    tempoption = Convert.ToInt32(useselecttemp.ToString());
                    optiontempnum = Convert.ToInt32(tempnumber.ToString());
                    picratio = Convert.ToInt32(inipicratio.ToString());
                    printerratio = Convert.ToInt32(iniprinterratio.ToString());
                    photonum = Convert.ToInt32(iniphotonum.ToString());
                    textcolor = initextcolor.ToString();
                    datetextcolor = inidatetextcolor.ToString();
                    kakaotimer = Convert.ToInt32(iniKakaoTimer.ToString());
                    cardnum = inicardnum.ToString();
                    SorR = iniSorR.ToString();
                    temp2fee = initemp2fee.ToString();
                    temp3fee = initemp3fee.ToString();
                    timerlocation = initimerlocation.ToString();
                    checkvideo = iniCheckVideo.ToString();
                    Version = iniversioncheck.ToString();
                    kakaotextcolor = inikakaotextcolor.ToString();
                    autologin = iniautologin.ToString();
                    coupon = inicoupon.ToString();
                    couponkind = inicouponkind.ToString();
                    checkcolor = inicheckcolor.ToString();

                    if (MainWindow.inifoldername.Contains("mediagram"))
                    {
                        GetPrivateProfileString("Setting", "checkingtogether", "", initogether, initogether.Capacity, iniPath);
                    }

                    if (initogether.ToString() == "" || initogether.ToString() == null)
                    {
                        checkingtogether = "No";
                    }
                    else
                    {
                        checkingtogether = initogether.ToString();
                    }


                    pic1startx = Convert.ToInt32(inipic1startx.ToString());
                    pic2startx = Convert.ToInt32(inipic2startx.ToString());
                    pic3startx = Convert.ToInt32(inipic3startx.ToString());
                    pic4startx = Convert.ToInt32(inipic4startx.ToString());
                    pic1starty = Convert.ToInt32(inipic1starty.ToString());
                    pic2starty = Convert.ToInt32(inipic2starty.ToString());
                    pic3starty = Convert.ToInt32(inipic3starty.ToString());
                    pic4starty = Convert.ToInt32(inipic4starty.ToString());
                    pic1width = Convert.ToInt32(inipic1width.ToString());
                    pic2width = Convert.ToInt32(inipic2width.ToString());
                    pic3width = Convert.ToInt32(inipic3width.ToString());
                    pic4width = Convert.ToInt32(inipic4width.ToString());
                    pic1height = Convert.ToInt32(inipic1height.ToString());
                    pic2height = Convert.ToInt32(inipic2height.ToString());
                    pic3height = Convert.ToInt32(inipic3height.ToString());
                    pic4height = Convert.ToInt32(inipic4height.ToString());

                    if (MainWindow.inipic5startx.ToString() == "" || MainWindow.inifoldername == null)
                    {
                        pic5startx = 0;
                        pic5starty = 0;
                        pic5width = 0;
                        pic5height = 0;
                        pic6startx = 0;
                        pic6width = 0;
                        pic6height = 0;
                        pic6starty = 0;
                    }
                    else
                    {
                        pic5startx = Convert.ToInt32(inipic5startx.ToString());
                        pic5starty = Convert.ToInt32(inipic5starty.ToString());
                        pic5width = Convert.ToInt32(inipic5width.ToString());
                        pic5height = Convert.ToInt32(inipic5height.ToString());
                        pic6startx = Convert.ToInt32(inipic6startx.ToString());
                        pic6width = Convert.ToInt32(inipic6width.ToString());
                        pic6height = Convert.ToInt32(inipic6height.ToString());
                        pic6starty = Convert.ToInt32(inipic6starty.ToString());
                    }

                    qrstartx = Convert.ToInt32(iniqrstartx.ToString());
                    qrstarty = Convert.ToInt32(iniqrstarty.ToString());
                    qrwidth = Convert.ToInt32(iniqrwidth.ToString());
                    qrheight = Convert.ToInt32(iniqrheight.ToString());
                    qrpageuse = Convert.ToInt32(iniqrpageuse.ToString());
                    datetext = Convert.ToInt32(inidatetext.ToString());

                    #region /// 납품 템플릿 설정 불러오기 ///

                    /// 템플릿 1번

                    temp1picratio = Convert.ToInt32(initemp1picratio.ToString());
                    temp1printerratio = Convert.ToInt32(initemp1printerratio.ToString());

                    temp1pic1startx = Convert.ToInt32(initemp1pic1startx.ToString());
                    temp1pic1starty = Convert.ToInt32(initemp1pic1starty.ToString());
                    temp1pic1width = Convert.ToInt32(initemp1pic1width.ToString());
                    temp1pic1height = Convert.ToInt32(initemp1pic1height.ToString());

                    temp1pic2startx = Convert.ToInt32(initemp1pic2startx.ToString());
                    temp1pic2starty = Convert.ToInt32(initemp1pic2starty.ToString());
                    temp1pic2width = Convert.ToInt32(initemp1pic2width.ToString());
                    temp1pic2height = Convert.ToInt32(initemp1pic2height.ToString());

                    temp1pic3startx = Convert.ToInt32(initemp1pic3startx.ToString());
                    temp1pic3starty = Convert.ToInt32(initemp1pic3starty.ToString());
                    temp1pic3width = Convert.ToInt32(initemp1pic3width.ToString());
                    temp1pic3height = Convert.ToInt32(initemp1pic3height.ToString());

                    temp1pic4startx = Convert.ToInt32(initemp1pic4startx.ToString());
                    temp1pic4starty = Convert.ToInt32(initemp1pic4starty.ToString());
                    temp1pic4width = Convert.ToInt32(initemp1pic4width.ToString());
                    temp1pic4height = Convert.ToInt32(initemp1pic4height.ToString());

                    /// 템플릿 2번

                    temp2picratio = Convert.ToInt32(initemp2picratio.ToString());
                    temp2printerratio = Convert.ToInt32(initemp2printerratio.ToString());

                    temp2pic1startx = Convert.ToInt32(initemp2pic1startx.ToString());
                    temp2pic1starty = Convert.ToInt32(initemp2pic1starty.ToString());
                    temp2pic1width = Convert.ToInt32(initemp2pic1width.ToString());
                    temp2pic1height = Convert.ToInt32(initemp2pic1height.ToString());

                    temp2pic2startx = Convert.ToInt32(initemp2pic2startx.ToString());
                    temp2pic2starty = Convert.ToInt32(initemp2pic2starty.ToString());
                    temp2pic2width = Convert.ToInt32(initemp2pic2width.ToString());
                    temp2pic2height = Convert.ToInt32(initemp2pic2height.ToString());

                    /// 템플릿 3번

                    temp3picratio = Convert.ToInt32(initemp3picratio.ToString());
                    temp3printerratio = Convert.ToInt32(initemp3printerratio.ToString());

                    temp3pic1startx = Convert.ToInt32(initemp3pic1startx.ToString());
                    temp3pic1starty = Convert.ToInt32(initemp3pic1starty.ToString());
                    temp3pic1width = Convert.ToInt32(initemp3pic1width.ToString());
                    temp3pic1height = Convert.ToInt32(initemp3pic1height.ToString());

                    temp3pic2startx = Convert.ToInt32(initemp3pic2startx.ToString());
                    temp3pic2starty = Convert.ToInt32(initemp3pic2starty.ToString());
                    temp3pic2width = Convert.ToInt32(initemp3pic2width.ToString());
                    temp3pic2height = Convert.ToInt32(initemp3pic2height.ToString());

                    temp3pic3startx = Convert.ToInt32(initemp3pic3startx.ToString());
                    temp3pic3starty = Convert.ToInt32(initemp3pic3starty.ToString());
                    temp3pic3width = Convert.ToInt32(initemp3pic3width.ToString());
                    temp3pic3height = Convert.ToInt32(initemp3pic3height.ToString());

                    temp3pic4startx = Convert.ToInt32(initemp3pic4startx.ToString());
                    temp3pic4starty = Convert.ToInt32(initemp3pic4starty.ToString());
                    temp3pic4width = Convert.ToInt32(initemp3pic4width.ToString());
                    temp3pic4height = Convert.ToInt32(initemp3pic4height.ToString());

                    temp3pic5startx = Convert.ToInt32(initemp3pic5startx.ToString());
                    temp3pic5starty = Convert.ToInt32(initemp3pic5starty.ToString());
                    temp3pic5width = Convert.ToInt32(initemp3pic5width.ToString());
                    temp3pic5height = Convert.ToInt32(initemp3pic5height.ToString());

                    temp3pic6startx = Convert.ToInt32(initemp3pic6startx.ToString());
                    temp3pic6starty = Convert.ToInt32(initemp3pic6starty.ToString());
                    temp3pic6width = Convert.ToInt32(initemp3pic6width.ToString());
                    temp3pic6height = Convert.ToInt32(initemp3pic6height.ToString());

                    #endregion

                    if (SorR == "R")
                    {
                        if (MainWindow.inifoldername.Contains("mediagram"))
                        {
                            temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                            temp1_2 = Bitmap.FromFile(TempPath + @"\Temp1_2.png");
                            temp1_3 = Bitmap.FromFile(TempPath + @"\Temp1_3.png");
                            temp1_4 = Bitmap.FromFile(TempPath + @"\Temp1_4.png");
                            temp1_5 = Bitmap.FromFile(TempPath + @"\Temp1_5.png");
                            temp1_6 = Bitmap.FromFile(TempPath + @"\Temp1_6.png");
                            temp1_7 = Bitmap.FromFile(TempPath + @"\Temp1_7.png");
                            temp1_8 = Bitmap.FromFile(TempPath + @"\Temp1_8.png");
                            temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                            temp1_2Front = Bitmap.FromFile(TempPath + @"\Temp1_2Front.png");
                            temp1_3Front = Bitmap.FromFile(TempPath + @"\Temp1_3Front.png");
                            temp1_4Front = Bitmap.FromFile(TempPath + @"\Temp1_4Front.png");
                            temp1_5Front = Bitmap.FromFile(TempPath + @"\Temp1_5Front.png");
                            temp1_6Front = Bitmap.FromFile(TempPath + @"\Temp1_6Front.png");
                            temp1_7Front = Bitmap.FromFile(TempPath + @"\Temp1_7Front.png");
                            temp1_8Front = Bitmap.FromFile(TempPath + @"\Temp1_8Front.png");
                        }
                        else
                        {
                            switch (tempnumber.ToString())
                            {
                                case "1":
                                    temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                    temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                                    break;
                                case "2":
                                    temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                    temp1_2 = Bitmap.FromFile(TempPath + @"\Temp1_2.png");
                                    temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                                    temp1_2Front = Bitmap.FromFile(TempPath + @"\Temp1_2Front.png");
                                    break;
                                case "3":
                                    temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                    temp1_2 = Bitmap.FromFile(TempPath + @"\Temp1_2.png");
                                    temp1_3 = Bitmap.FromFile(TempPath + @"\Temp1_3.png");
                                    temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                                    temp1_2Front = Bitmap.FromFile(TempPath + @"\Temp1_2Front.png");
                                    temp1_3Front = Bitmap.FromFile(TempPath + @"\Temp1_3Front.png");
                                    break;
                                case "4":
                                    temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                    temp1_2 = Bitmap.FromFile(TempPath + @"\Temp1_2.png");
                                    temp1_3 = Bitmap.FromFile(TempPath + @"\Temp1_3.png");
                                    temp1_4 = Bitmap.FromFile(TempPath + @"\Temp1_4.png");
                                    temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                                    temp1_2Front = Bitmap.FromFile(TempPath + @"\Temp1_2Front.png");
                                    temp1_3Front = Bitmap.FromFile(TempPath + @"\Temp1_3Front.png");
                                    temp1_4Front = Bitmap.FromFile(TempPath + @"\Temp1_4Front.png");
                                    break;
                                case "5":
                                    temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                    temp1_2 = Bitmap.FromFile(TempPath + @"\Temp1_2.png");
                                    temp1_3 = Bitmap.FromFile(TempPath + @"\Temp1_3.png");
                                    temp1_4 = Bitmap.FromFile(TempPath + @"\Temp1_4.png");
                                    temp1_5 = Bitmap.FromFile(TempPath + @"\Temp1_5.png");
                                    temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                                    temp1_2Front = Bitmap.FromFile(TempPath + @"\Temp1_2Front.png");
                                    temp1_3Front = Bitmap.FromFile(TempPath + @"\Temp1_3Front.png");
                                    temp1_4Front = Bitmap.FromFile(TempPath + @"\Temp1_4Front.png");
                                    temp1_5Front = Bitmap.FromFile(TempPath + @"\Temp1_5Front.png");
                                    break;
                                case "6":
                                    temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                    temp1_2 = Bitmap.FromFile(TempPath + @"\Temp1_2.png");
                                    temp1_3 = Bitmap.FromFile(TempPath + @"\Temp1_3.png");
                                    temp1_4 = Bitmap.FromFile(TempPath + @"\Temp1_4.png");
                                    temp1_5 = Bitmap.FromFile(TempPath + @"\Temp1_5.png");
                                    temp1_6 = Bitmap.FromFile(TempPath + @"\Temp1_6.png");
                                    temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                                    temp1_2Front = Bitmap.FromFile(TempPath + @"\Temp1_2Front.png");
                                    temp1_3Front = Bitmap.FromFile(TempPath + @"\Temp1_3Front.png");
                                    temp1_4Front = Bitmap.FromFile(TempPath + @"\Temp1_4Front.png");
                                    temp1_5Front = Bitmap.FromFile(TempPath + @"\Temp1_5Front.png");
                                    temp1_6Front = Bitmap.FromFile(TempPath + @"\Temp1_6Front.png");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (MainWindow.inifoldername.Contains("mediagram"))
                        {
                            temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                            temp1_2 = Bitmap.FromFile(TempPath + @"\Temp1_2.png");
                            temp1_3 = Bitmap.FromFile(TempPath + @"\Temp1_3.png");
                            temp1_4 = Bitmap.FromFile(TempPath + @"\Temp1_4.png");
                            temp1_5 = Bitmap.FromFile(TempPath + @"\Temp1_5.png");
                            temp1_6 = Bitmap.FromFile(TempPath + @"\Temp1_6.png");
                            temp1_7 = Bitmap.FromFile(TempPath + @"\Temp1_7.png");
                            temp1_8 = Bitmap.FromFile(TempPath + @"\Temp1_8.png");
                            temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                            temp1_2Front = Bitmap.FromFile(TempPath + @"\Temp1_2Front.png");
                            temp1_3Front = Bitmap.FromFile(TempPath + @"\Temp1_3Front.png");
                            temp1_4Front = Bitmap.FromFile(TempPath + @"\Temp1_4Front.png");
                            temp1_5Front = Bitmap.FromFile(TempPath + @"\Temp1_5Front.png");
                            temp1_6Front = Bitmap.FromFile(TempPath + @"\Temp1_6Front.png");
                            temp1_7Front = Bitmap.FromFile(TempPath + @"\Temp1_7Front.png");
                            temp1_8Front = Bitmap.FromFile(TempPath + @"\Temp1_8Front.png");

                            temp2_1 = Bitmap.FromFile(TempPath + @"\Temp2_1.png");
                            temp2_2 = Bitmap.FromFile(TempPath + @"\Temp2_2.png");
                            temp2_3 = Bitmap.FromFile(TempPath + @"\Temp2_3.png");
                            temp2_4 = Bitmap.FromFile(TempPath + @"\Temp2_4.png");
                            temp2_5 = Bitmap.FromFile(TempPath + @"\Temp2_5.png");
                            temp2_6 = Bitmap.FromFile(TempPath + @"\Temp2_6.png");
                            temp2_7 = Bitmap.FromFile(TempPath + @"\Temp2_7.png");
                            temp2_8 = Bitmap.FromFile(TempPath + @"\Temp2_8.png");
                            temp2_1Front = Bitmap.FromFile(TempPath + @"\Temp2_1Front.png");
                            temp2_2Front = Bitmap.FromFile(TempPath + @"\Temp2_2Front.png");
                            temp2_3Front = Bitmap.FromFile(TempPath + @"\Temp2_3Front.png");
                            temp2_4Front = Bitmap.FromFile(TempPath + @"\Temp2_4Front.png");
                            temp2_5Front = Bitmap.FromFile(TempPath + @"\Temp2_5Front.png");
                            temp2_6Front = Bitmap.FromFile(TempPath + @"\Temp2_6Front.png");
                            temp2_7Front = Bitmap.FromFile(TempPath + @"\Temp2_7Front.png");
                            temp2_8Front = Bitmap.FromFile(TempPath + @"\Temp2_8Front.png");

                            temp3_1 = Bitmap.FromFile(TempPath + @"\Temp3_1.png");
                            temp3_2 = Bitmap.FromFile(TempPath + @"\Temp3_2.png");
                            temp3_3 = Bitmap.FromFile(TempPath + @"\Temp3_3.png");
                            temp3_4 = Bitmap.FromFile(TempPath + @"\Temp3_4.png");
                            temp3_5 = Bitmap.FromFile(TempPath + @"\Temp3_5.png");
                            temp3_6 = Bitmap.FromFile(TempPath + @"\Temp3_6.png");
                            temp3_7 = Bitmap.FromFile(TempPath + @"\Temp3_7.png");
                            temp3_8 = Bitmap.FromFile(TempPath + @"\Temp3_8.png");
                            temp3_1Front = Bitmap.FromFile(TempPath + @"\Temp3_1Front.png");
                            temp3_2Front = Bitmap.FromFile(TempPath + @"\Temp3_2Front.png");
                            temp3_3Front = Bitmap.FromFile(TempPath + @"\Temp3_3Front.png");
                            temp3_4Front = Bitmap.FromFile(TempPath + @"\Temp3_4Front.png");
                            temp3_5Front = Bitmap.FromFile(TempPath + @"\Temp3_5Front.png");
                            temp3_6Front = Bitmap.FromFile(TempPath + @"\Temp3_6Front.png");
                            temp3_7Front = Bitmap.FromFile(TempPath + @"\Temp3_7Front.png");
                            temp3_8Front = Bitmap.FromFile(TempPath + @"\Temp3_8Front.png");
                        }
                        else
                        {
                            if (MainWindow.inifoldername == "tech")
                            {
                                temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                temp1_2 = Bitmap.FromFile(TempPath + @"\Temp1_2.png");
                                temp1_3 = Bitmap.FromFile(TempPath + @"\Temp1_3.png");
                                temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                                temp1_2Front = Bitmap.FromFile(TempPath + @"\Temp1_2Front.png");
                                temp1_3Front = Bitmap.FromFile(TempPath + @"\Temp1_3Front.png");

                                temp2_1 = Bitmap.FromFile(TempPath + @"\Temp2_1.png");
                                temp2_1Front = Bitmap.FromFile(TempPath + @"\Temp2_1Front.png");

                                temp3_1 = Bitmap.FromFile(TempPath + @"\Temp3_1.png");
                                temp3_1Front = Bitmap.FromFile(TempPath + @"\Temp3_1Front.png");

                                temp4_1 = Bitmap.FromFile(TempPath + @"\Temp4_1.png");
                                temp4_1Front = Bitmap.FromFile(TempPath + @"\Temp4_1Front.png");
                            }
                            if (MainWindow.inifoldername == "r80")
                            {
                                temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                temp1_2 = Bitmap.FromFile(TempPath + @"\Temp1_2.png");
                                temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                                temp1_2Front = Bitmap.FromFile(TempPath + @"\Temp1_2Front.png");
                                temp1_3 = Bitmap.FromFile(TempPath + @"\Temp1_3.png");
                                temp1_4 = Bitmap.FromFile(TempPath + @"\Temp1_4.png");
                                temp1_3Front = Bitmap.FromFile(TempPath + @"\Temp1_3Front.png");
                                temp1_4Front = Bitmap.FromFile(TempPath + @"\Temp1_4Front.png");

                                temp2_1 = Bitmap.FromFile(TempPath + @"\Temp2_1.png");
                                temp2_1Front = Bitmap.FromFile(TempPath + @"\Temp2_1Front.png");
                                temp2_2 = Bitmap.FromFile(TempPath + @"\Temp2_2.png");
                                temp2_2Front = Bitmap.FromFile(TempPath + @"\Temp2_2Front.png");
                                temp2_3 = Bitmap.FromFile(TempPath + @"\Temp2_3.png");
                                temp2_3Front = Bitmap.FromFile(TempPath + @"\Temp2_3Front.png");
                                temp2_4 = Bitmap.FromFile(TempPath + @"\Temp2_4.png");
                                temp2_4Front = Bitmap.FromFile(TempPath + @"\Temp2_4Front.png");
                            }
                            else if (MainWindow.inifoldername == "hhh")
                            {
                                if (MainWindow.optiontempnum == 1)
                                {
                                    temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                    temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                                }
                                else
                                {
                                    temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                    temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");

                                    temp2_1 = Bitmap.FromFile(TempPath + @"\Temp2_1.png");
                                    temp2_1Front = Bitmap.FromFile(TempPath + @"\Temp2_1Front.png");

                                    temp3_1 = Bitmap.FromFile(TempPath + @"\Temp3_1.png");
                                    temp3_1Front = Bitmap.FromFile(TempPath + @"\Temp3_1Front.png");
                                }
                            }
                            else
                            {
                                temp1_1 = Bitmap.FromFile(TempPath + @"\Temp1_1.png");
                                temp1_2 = Bitmap.FromFile(TempPath + @"\Temp1_2.png");
                                temp1_3 = Bitmap.FromFile(TempPath + @"\Temp1_3.png");
                                temp1_4 = Bitmap.FromFile(TempPath + @"\Temp1_4.png");
                                temp1_5 = Bitmap.FromFile(TempPath + @"\Temp1_5.png");
                                temp1_6 = Bitmap.FromFile(TempPath + @"\Temp1_6.png");
                                temp1_1Front = Bitmap.FromFile(TempPath + @"\Temp1_1Front.png");
                                temp1_2Front = Bitmap.FromFile(TempPath + @"\Temp1_2Front.png");
                                temp1_3Front = Bitmap.FromFile(TempPath + @"\Temp1_3Front.png");
                                temp1_4Front = Bitmap.FromFile(TempPath + @"\Temp1_4Front.png");
                                temp1_5Front = Bitmap.FromFile(TempPath + @"\Temp1_5Front.png");
                                temp1_6Front = Bitmap.FromFile(TempPath + @"\Temp1_6Front.png");

                                temp2_1 = Bitmap.FromFile(TempPath + @"\Temp2_1.png");
                                temp2_2 = Bitmap.FromFile(TempPath + @"\Temp2_2.png");
                                temp2_3 = Bitmap.FromFile(TempPath + @"\Temp2_3.png");
                                temp2_4 = Bitmap.FromFile(TempPath + @"\Temp2_4.png");
                                temp2_5 = Bitmap.FromFile(TempPath + @"\Temp2_5.png");
                                temp2_6 = Bitmap.FromFile(TempPath + @"\Temp2_6.png");
                                temp2_1Front = Bitmap.FromFile(TempPath + @"\Temp2_1Front.png");
                                temp2_2Front = Bitmap.FromFile(TempPath + @"\Temp2_2Front.png");
                                temp2_3Front = Bitmap.FromFile(TempPath + @"\Temp2_3Front.png");
                                temp2_4Front = Bitmap.FromFile(TempPath + @"\Temp2_4Front.png");
                                temp2_5Front = Bitmap.FromFile(TempPath + @"\Temp2_5Front.png");
                                temp2_6Front = Bitmap.FromFile(TempPath + @"\Temp2_6Front.png");

                                temp3_1 = Bitmap.FromFile(TempPath + @"\Temp3_1.png");
                                temp3_2 = Bitmap.FromFile(TempPath + @"\Temp3_2.png");
                                temp3_3 = Bitmap.FromFile(TempPath + @"\Temp3_3.png");
                                temp3_4 = Bitmap.FromFile(TempPath + @"\Temp3_4.png");
                                temp3_5 = Bitmap.FromFile(TempPath + @"\Temp3_5.png");
                                temp3_6 = Bitmap.FromFile(TempPath + @"\Temp3_6.png");
                                temp3_1Front = Bitmap.FromFile(TempPath + @"\Temp3_1Front.png");
                                temp3_2Front = Bitmap.FromFile(TempPath + @"\Temp3_2Front.png");
                                temp3_3Front = Bitmap.FromFile(TempPath + @"\Temp3_3Front.png");
                                temp3_4Front = Bitmap.FromFile(TempPath + @"\Temp3_4Front.png");
                                temp3_5Front = Bitmap.FromFile(TempPath + @"\Temp3_5Front.png");
                                temp3_6Front = Bitmap.FromFile(TempPath + @"\Temp3_6Front.png");
                            }
                        }
                    }

                    main = new BitmapImage();
                    main.BeginInit();
                    main.UriSource = new Uri(uipath + @"\MainImg.png", UriKind.RelativeOrAbsolute);
                    main.CacheOption = BitmapCacheOption.OnLoad;
                    main.EndInit();

                    pageselect = new BitmapImage();
                    pageselect.BeginInit();
                    pageselect.UriSource = new Uri(uipath + @"\PageSelectBackground.png", UriKind.RelativeOrAbsolute);
                    pageselect.CacheOption = BitmapCacheOption.OnLoad;
                    pageselect.EndInit();

                    payment = new BitmapImage();
                    payment.BeginInit();
                    payment.UriSource = new Uri(uipath + @"\Payment.png", UriKind.RelativeOrAbsolute);
                    payment.CacheOption = BitmapCacheOption.OnLoad;
                    payment.EndInit();

                    liveview = new BitmapImage();
                    liveview.BeginInit();
                    liveview.UriSource = new Uri(uipath + @"\liveview.png", UriKind.RelativeOrAbsolute);
                    liveview.CacheOption = BitmapCacheOption.OnLoad;
                    liveview.EndInit();

                    preparing = new BitmapImage();
                    preparing.BeginInit();
                    preparing.UriSource = new Uri(uipath + @"\Preparing.png", UriKind.RelativeOrAbsolute);
                    preparing.CacheOption = BitmapCacheOption.OnLoad;
                    preparing.EndInit();

                    imgcompose = new BitmapImage();
                    imgcompose.BeginInit();
                    imgcompose.UriSource = new Uri(uipath + @"\ImgCompose.png", UriKind.RelativeOrAbsolute);
                    imgcompose.CacheOption = BitmapCacheOption.OnLoad;
                    imgcompose.EndInit();

                    kakaopage = new BitmapImage();
                    kakaopage.BeginInit();
                    kakaopage.UriSource = new Uri(uipath + @"\KakaoPage.png", UriKind.RelativeOrAbsolute);
                    kakaopage.CacheOption = BitmapCacheOption.OnLoad;
                    kakaopage.EndInit();

                    couponpage = new BitmapImage();
                    couponpage.BeginInit();
                    couponpage.UriSource = new Uri(uipath + @"\couponpage.png", UriKind.RelativeOrAbsolute);
                    couponpage.CacheOption = BitmapCacheOption.OnLoad;
                    couponpage.EndInit();

                    final = new BitmapImage();
                    final.BeginInit();
                    final.UriSource = new Uri(uipath + @"\Final.png", UriKind.RelativeOrAbsolute);
                    final.CacheOption = BitmapCacheOption.OnLoad;
                    final.EndInit();

                    Backimg = new BitmapImage();
                    Backimg.BeginInit();
                    Backimg.UriSource = new Uri(uipath + @"\Button_Prev_off.png", UriKind.RelativeOrAbsolute);
                    Backimg.CacheOption = BitmapCacheOption.OnLoad;
                    Backimg.EndInit();

                    coloroff = new BitmapImage();
                    coloroff.BeginInit();
                    coloroff.UriSource = new Uri(uipath + @"\color_off.png", UriKind.RelativeOrAbsolute);
                    coloroff.CacheOption = BitmapCacheOption.OnLoad;
                    coloroff.EndInit();

                    coloron = new BitmapImage();
                    coloron.BeginInit();
                    coloron.UriSource = new Uri(uipath + @"\color_on.png", UriKind.RelativeOrAbsolute);
                    coloron.CacheOption = BitmapCacheOption.OnLoad;
                    coloron.EndInit();

                    grayoff = new BitmapImage();
                    grayoff.BeginInit();
                    grayoff.UriSource = new Uri(uipath + @"\gray_off.png", UriKind.RelativeOrAbsolute);
                    grayoff.CacheOption = BitmapCacheOption.OnLoad;
                    grayoff.EndInit();

                    grayon = new BitmapImage();
                    grayon.BeginInit();
                    grayon.UriSource = new Uri(uipath + @"\gray_on.png", UriKind.RelativeOrAbsolute);
                    grayon.CacheOption = BitmapCacheOption.OnLoad;
                    grayon.EndInit();

                    kakaobtn = new BitmapImage();
                    kakaobtn.BeginInit();
                    kakaobtn.UriSource = new Uri(uipath + @"\Kakaobtn.png", UriKind.RelativeOrAbsolute);
                    kakaobtn.CacheOption = BitmapCacheOption.OnLoad;
                    kakaobtn.EndInit();

                    nextbtn = new BitmapImage();
                    nextbtn.BeginInit();
                    nextbtn.UriSource = new Uri(uipath + @"\Next.png", UriKind.RelativeOrAbsolute);
                    nextbtn.CacheOption = BitmapCacheOption.OnLoad;
                    nextbtn.EndInit();

                    nextcolor = new BitmapImage();
                    nextcolor.BeginInit();
                    nextcolor.UriSource = new Uri(uipath + @"\Next_off.png", UriKind.RelativeOrAbsolute);
                    nextcolor.CacheOption = BitmapCacheOption.OnLoad;
                    nextcolor.EndInit();

                    backcolor = new BitmapImage();
                    backcolor.BeginInit();
                    backcolor.UriSource = new Uri(uipath + @"\Prev_off.png", UriKind.RelativeOrAbsolute);
                    backcolor.CacheOption = BitmapCacheOption.OnLoad;
                    backcolor.EndInit();

                    number0 = new BitmapImage();
                    number0.BeginInit();
                    number0.UriSource = new Uri(uipath + @"\number_0.png", UriKind.RelativeOrAbsolute);
                    number0.CacheOption = BitmapCacheOption.OnLoad;
                    number0.EndInit();

                    number1 = new BitmapImage();
                    number1.BeginInit();
                    number1.UriSource = new Uri(uipath + @"\number_1.png", UriKind.RelativeOrAbsolute);
                    number1.CacheOption = BitmapCacheOption.OnLoad;
                    number1.EndInit();

                    number2 = new BitmapImage();
                    number2.BeginInit();
                    number2.UriSource = new Uri(uipath + @"\number_2.png", UriKind.RelativeOrAbsolute);
                    number2.CacheOption = BitmapCacheOption.OnLoad;
                    number2.EndInit();

                    number3 = new BitmapImage();
                    number3.BeginInit();
                    number3.UriSource = new Uri(uipath + @"\number_3.png", UriKind.RelativeOrAbsolute);
                    number3.CacheOption = BitmapCacheOption.OnLoad;
                    number3.EndInit();

                    number4 = new BitmapImage();
                    number4.BeginInit();
                    number4.UriSource = new Uri(uipath + @"\number_4.png", UriKind.RelativeOrAbsolute);
                    number4.CacheOption = BitmapCacheOption.OnLoad;
                    number4.EndInit();

                    number5 = new BitmapImage();
                    number5.BeginInit();
                    number5.UriSource = new Uri(uipath + @"\number_5.png", UriKind.RelativeOrAbsolute);
                    number5.CacheOption = BitmapCacheOption.OnLoad;
                    number5.EndInit();

                    number6 = new BitmapImage();
                    number6.BeginInit();
                    number6.UriSource = new Uri(uipath + @"\number_6.png", UriKind.RelativeOrAbsolute);
                    number6.CacheOption = BitmapCacheOption.OnLoad;
                    number6.EndInit();

                    number7 = new BitmapImage();
                    number7.BeginInit();
                    number7.UriSource = new Uri(uipath + @"\number_7.png", UriKind.RelativeOrAbsolute);
                    number7.CacheOption = BitmapCacheOption.OnLoad;
                    number7.EndInit();

                    number8 = new BitmapImage();
                    number8.BeginInit();
                    number8.UriSource = new Uri(uipath + @"\number_8.png", UriKind.RelativeOrAbsolute);
                    number8.CacheOption = BitmapCacheOption.OnLoad;
                    number8.EndInit();

                    number9 = new BitmapImage();
                    number9.BeginInit();
                    number9.UriSource = new Uri(uipath + @"\number_9.png", UriKind.RelativeOrAbsolute);
                    number9.CacheOption = BitmapCacheOption.OnLoad;
                    number9.EndInit();

                    delbtn = new BitmapImage();
                    delbtn.BeginInit();
                    delbtn.UriSource = new Uri(uipath + @"\number_del.png", UriKind.RelativeOrAbsolute);
                    delbtn.CacheOption = BitmapCacheOption.OnLoad;
                    delbtn.EndInit();

                    printbtn = new BitmapImage();
                    printbtn.BeginInit();
                    printbtn.UriSource = new Uri(uipath + @"\print.png", UriKind.RelativeOrAbsolute);
                    printbtn.CacheOption = BitmapCacheOption.OnLoad;
                    printbtn.EndInit();

                    retakebtn = new BitmapImage();
                    retakebtn.BeginInit();
                    retakebtn.UriSource = new Uri(uipath + @"\Retakenbtn.png", UriKind.RelativeOrAbsolute);
                    retakebtn.CacheOption = BitmapCacheOption.OnLoad;
                    retakebtn.EndInit();

                    touchbtn = new BitmapImage();
                    touchbtn.BeginInit();
                    touchbtn.UriSource = new Uri(uipath + @"\Touch.png", UriKind.RelativeOrAbsolute);
                    touchbtn.CacheOption = BitmapCacheOption.OnLoad;
                    touchbtn.EndInit();

                    nokakaobtn = new BitmapImage();
                    nokakaobtn.BeginInit();
                    nokakaobtn.UriSource = new Uri(uipath + @"\Button_kakao2.png", UriKind.RelativeOrAbsolute);
                    nokakaobtn.CacheOption = BitmapCacheOption.OnLoad;
                    nokakaobtn.EndInit();

                    card_off = new BitmapImage();
                    card_off.BeginInit();
                    card_off.UriSource = new Uri(uipath + @"\card_off.png", UriKind.RelativeOrAbsolute);
                    card_off.CacheOption = BitmapCacheOption.OnLoad;
                    card_off.EndInit();

                    card_on = new BitmapImage();
                    card_on.BeginInit();
                    card_on.UriSource = new Uri(uipath + @"\card_on.png", UriKind.RelativeOrAbsolute);
                    card_on.CacheOption = BitmapCacheOption.OnLoad;
                    card_on.EndInit();

                    cash_off = new BitmapImage();
                    cash_off.BeginInit();
                    cash_off.UriSource = new Uri(uipath + @"\cash_off.png", UriKind.RelativeOrAbsolute);
                    cash_off.CacheOption = BitmapCacheOption.OnLoad;
                    cash_off.EndInit();

                    cash_on = new BitmapImage();
                    cash_on.BeginInit();
                    cash_on.UriSource = new Uri(uipath + @"\cash_on.png", UriKind.RelativeOrAbsolute);
                    cash_on.CacheOption = BitmapCacheOption.OnLoad;
                    cash_on.EndInit();

                    credit = new BitmapImage();
                    credit.BeginInit();
                    credit.UriSource = new Uri(uipath + @"\credit.png", UriKind.RelativeOrAbsolute);
                    credit.CacheOption = BitmapCacheOption.OnLoad;
                    credit.EndInit();

                    creditfail = new BitmapImage();
                    creditfail.BeginInit();
                    creditfail.UriSource = new Uri(uipath + @"\creditfail1.png", UriKind.RelativeOrAbsolute);
                    creditfail.CacheOption = BitmapCacheOption.OnLoad;
                    creditfail.EndInit();

                    if (File.Exists(MainWindow.uipath + @"\credittimeout.png"))
                    {
                        credittimeout = new BitmapImage();
                        credittimeout.BeginInit();
                        credittimeout.UriSource = new Uri(uipath + @"\credittimeout.png", UriKind.RelativeOrAbsolute);
                        credittimeout.CacheOption = BitmapCacheOption.OnLoad;
                        credittimeout.EndInit();
                    }

                    if (File.Exists(MainWindow.uipath + @"\crediterror.png"))
                    {
                        crediterror = new BitmapImage();
                        crediterror.BeginInit();
                        crediterror.UriSource = new Uri(uipath + @"\crediterror.png", UriKind.RelativeOrAbsolute);
                        crediterror.CacheOption = BitmapCacheOption.OnLoad;
                        crediterror.EndInit();
                    }

                    check = new BitmapImage();
                    check.BeginInit();
                    check.UriSource = new Uri(uipath + @"\check.png", UriKind.RelativeOrAbsolute);
                    check.CacheOption = BitmapCacheOption.OnLoad;
                    check.EndInit();

                    choicepayment = new BitmapImage();
                    choicepayment.BeginInit();
                    choicepayment.UriSource = new Uri(uipath + @"\ChoicePayment.png", UriKind.RelativeOrAbsolute);
                    choicepayment.CacheOption = BitmapCacheOption.OnLoad;
                    choicepayment.EndInit();

                    paymentcomplete = new BitmapImage();
                    paymentcomplete.BeginInit();
                    paymentcomplete.UriSource = new Uri(uipath + @"\PaymentComplete.png", UriKind.RelativeOrAbsolute);
                    paymentcomplete.CacheOption = BitmapCacheOption.OnLoad;
                    paymentcomplete.EndInit();

                    tempselectpage = new BitmapImage();
                    tempselectpage.BeginInit();
                    tempselectpage.UriSource = new Uri(uipath + @"\TempSelectImg.png", UriKind.RelativeOrAbsolute);
                    tempselectpage.CacheOption = BitmapCacheOption.OnLoad;
                    tempselectpage.EndInit();

                    QRpage = new BitmapImage();
                    QRpage.BeginInit();
                    QRpage.UriSource = new Uri(uipath + @"\QRKakaoPage.png", UriKind.RelativeOrAbsolute);
                    QRpage.CacheOption = BitmapCacheOption.OnLoad;
                    QRpage.EndInit();

                    temp1_pick = new BitmapImage();
                    temp1_pick.BeginInit();
                    temp1_pick.UriSource = new Uri(uipath + @"\Temp1_Pick.png", UriKind.RelativeOrAbsolute);
                    temp1_pick.CacheOption = BitmapCacheOption.OnLoad;
                    temp1_pick.EndInit();

                    temp2_pick = new BitmapImage();
                    temp2_pick.BeginInit();
                    temp2_pick.UriSource = new Uri(uipath + @"\Temp2_Pick.png", UriKind.RelativeOrAbsolute);
                    temp2_pick.CacheOption = BitmapCacheOption.OnLoad;
                    temp2_pick.EndInit();

                    temp3_pick = new BitmapImage();
                    temp3_pick.BeginInit();
                    temp3_pick.UriSource = new Uri(uipath + @"\Temp3_Pick.png", UriKind.RelativeOrAbsolute);
                    temp3_pick.CacheOption = BitmapCacheOption.OnLoad;
                    temp3_pick.EndInit();

                    temp1_nonpick = new BitmapImage();
                    temp1_nonpick.BeginInit();
                    temp1_nonpick.UriSource = new Uri(uipath + @"\Temp1.png", UriKind.RelativeOrAbsolute);
                    temp1_nonpick.CacheOption = BitmapCacheOption.OnLoad;
                    temp1_nonpick.EndInit();

                    temp2_nonpick = new BitmapImage();
                    temp2_nonpick.BeginInit();
                    temp2_nonpick.UriSource = new Uri(uipath + @"\Temp2.png", UriKind.RelativeOrAbsolute);
                    temp2_nonpick.CacheOption = BitmapCacheOption.OnLoad;
                    temp2_nonpick.EndInit();

                    temp3_nonpick = new BitmapImage();
                    temp3_nonpick.BeginInit();
                    temp3_nonpick.UriSource = new Uri(uipath + @"\Temp3.png", UriKind.RelativeOrAbsolute);
                    temp3_nonpick.CacheOption = BitmapCacheOption.OnLoad;
                    temp3_nonpick.EndInit();

                    if (MainWindow.inifoldername == "dearpic2" || MainWindow.inifoldername == "animalhospital")
                    {
                        Selectpicpage1 = new BitmapImage();
                        Selectpicpage1.BeginInit();
                        Selectpicpage1.UriSource = new Uri(uipath + @"\selectpic1.png", UriKind.RelativeOrAbsolute);
                        Selectpicpage1.CacheOption = BitmapCacheOption.OnLoad;
                        Selectpicpage1.EndInit();

                        Selectpicpage2 = new BitmapImage();
                        Selectpicpage2.BeginInit();
                        Selectpicpage2.UriSource = new Uri(uipath + @"\selectpic2.png", UriKind.RelativeOrAbsolute);
                        Selectpicpage2.CacheOption = BitmapCacheOption.OnLoad;
                        Selectpicpage2.EndInit();
                    }
                    else
                    {
                        Selectpicpage1 = new BitmapImage();
                        Selectpicpage1.BeginInit();
                        Selectpicpage1.UriSource = new Uri(uipath + @"\selectpic1.png", UriKind.RelativeOrAbsolute);
                        Selectpicpage1.CacheOption = BitmapCacheOption.OnLoad;
                        Selectpicpage1.EndInit();

                        Selectpicpage2 = new BitmapImage();
                        Selectpicpage2.BeginInit();
                        Selectpicpage2.UriSource = new Uri(uipath + @"\selectpic2.png", UriKind.RelativeOrAbsolute);
                        Selectpicpage2.CacheOption = BitmapCacheOption.OnLoad;
                        Selectpicpage2.EndInit();

                        Selectpicpage3 = new BitmapImage();
                        Selectpicpage3.BeginInit();
                        Selectpicpage3.UriSource = new Uri(uipath + @"\selectpic3.png", UriKind.RelativeOrAbsolute);
                        Selectpicpage3.CacheOption = BitmapCacheOption.OnLoad;
                        Selectpicpage3.EndInit();
                    }

                    composeimg = new BitmapImage();
                    composeimg.BeginInit();
                    composeimg.UriSource = new Uri(uipath + @"\compose1_1.png", UriKind.RelativeOrAbsolute);
                    composeimg.CacheOption = BitmapCacheOption.OnLoad;
                    composeimg.EndInit();

                    composeimg2 = new BitmapImage();
                    composeimg2.BeginInit();
                    composeimg2.UriSource = new Uri(uipath + @"\compose1_2.png", UriKind.RelativeOrAbsolute);
                    composeimg2.CacheOption = BitmapCacheOption.OnLoad;
                    composeimg2.EndInit();



                    // kakao 파일 안 이미지 삭제

                    try
                    {
                        DirectoryInfo sns = new DirectoryInfo(MainWindow.SnsPath);
                        foreach (FileInfo file in sns.EnumerateFiles())
                        {
                            file.Delete();
                            Source.Log.log.Debug("Kakao 폴더 안 이미지 삭제 완료");
                        }
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
                    catch (Exception delex)
                    {
                        Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + delex.Message);
                    }

                    if (camnumber.ToString() == "2")
                    {
                        try
                        {
                            video = new VideoCapture(0);
                            Webcam = CameraService.AvailableCameras[0];
                        }
                        catch (Exception webex)
                        {
                            MessageBox.Show("카메라 연결을 확인해주세요.");
                            Source.Log.log.Error(webex);
                            Source.Log.log.Error("카메라 연결을 확인해주세요.");
                        }
                        WebCamInitialize(CaptureWidth, CaptureHeight, 12, 0);
                    }
                    System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(textcolor);
                    textbrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));

                    System.Drawing.Color color2 = System.Drawing.ColorTranslator.FromHtml(kakaotextcolor);
                    fontbrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(color2.A, color2.R, color2.G, color2.B));

                    if (datetext == 1)
                    {
                        Color temptextcolor = ColorTranslator.FromHtml(datetextcolor);
                        datetextcolorbrush = new SolidBrush(System.Drawing.Color.FromArgb(temptextcolor.A, temptextcolor.R, temptextcolor.G, temptextcolor.B));
                    }

                    connectionString = "";
                    containerName = inifoldername;

                    timer = new System.Timers.Timer();
                    timer.Interval = 1000;
                    timer.Elapsed += Timer_TikTok;
                    timer.Start();

                    Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() 로드 완료");
                }
                else
                {
                    MessageBox.Show("런처를 실행시켜 주세요.");
                    ProgramClose();
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        internal void DeleteFile()
        {
            try
            {
                Source.Log.log.Info("이전 파일 삭제");

                DirectoryInfo kakaofolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"IMG\savekakao");

                int deleteday = 30;


                if (kakaofolder.Exists)
                {
                    FileInfo[] files = kakaofolder.GetFiles();
                    string date = DateTime.Today.AddDays(-(deleteday)).ToString("yyyy-MM-dd");

                    foreach (FileInfo file in files)
                    {
                        if (date.CompareTo(file.LastWriteTime.ToString("yyyy-MM-dd")) > 0)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(file.Name, ".PNG"))
                            {
                                File.Delete(kakaofolder + "\\" + file.Name);
                            }
                        }
                    }
                }

                Source.Log.log.Info("파일삭제 완료");

                kakaofolder = null;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                if (currentTime.Minute == 0 && currentTime.Second == 0)
                {
                    Source.Log.log.Info("로그 업로드 동작 시작");

                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        public static bool CheckInternetConnection()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send("www.google.com");
                    return (reply.Status == IPStatus.Success);
                }
            }
            catch
            {
                return false;
            }
        }

        private void Timer_TikTok(object sender, EventArgs e)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                if (currentTime.Hour != 0 && currentTime.Minute == 0 && currentTime.Second == 0)
                {
                    try
                    {
                        Source.Log.log.Info("정각 업로드 시작");
                        DateTime today = DateTime.Today;
                        string logFileName = $"Log_{today:yyyyMMdd}.log";
                        string logFilePath = System.IO.Path.Combine(logpath, logFileName);

                        if (File.Exists(logFilePath))
                        {
                            string destinationFolderPath = logpath + @"\uploadlog";
                            string destinationFilePath = System.IO.Path.Combine(destinationFolderPath, logFileName);

                            File.Copy(logFilePath, destinationFilePath, true);

                            UploadFileToBlobStorage(destinationFilePath, logFileName);

                            Source.Log.log.Info("Log file uplaoded successfully!");
                        }
                        else
                        {
                            Source.Log.log.Info("로그 파일이 없습니다.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void UploadFileToBlobStorage(string filePath, string fileName)
        {
            try
            {
                string connectionstring = connectionString; // Azure Blob Storage 연결 문자열 설정
                string containerName = inifoldername; // 컨테이너 이름 설정
                string blobDirectoryName = "Machine1/Log"; // 블롭 디렉토리 경로 설정

                CheckAndCreateContainer(connectionString, containerName);

                // BlobServiceClient 생성
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionstring);

                // 컨테이너 가져오기 (없을 경우 생성)
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                containerClient.CreateIfNotExists();

                // 블롭 경로 생성
                string blobFilePath = System.IO.Path.Combine(blobDirectoryName, fileName);

                // 블롭 클라이언트 생성
                BlobClient blobClient = containerClient.GetBlobClient(blobFilePath);

                // 파일 업로드
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    // 같은 이름의 블롭이 이미 존재하는지 확인
                    if (blobClient.Exists())
                    {
                        // 기존 블롭 삭제
                        blobClient.Delete();
                    }

                    // 파일 업로드
                    blobClient.Upload(fileStream, true);
                }

                Source.Log.log.Info("File uploaded successfully!");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        static void CheckAndCreateContainer(string storageConnectionString, string containerName)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);

                // 컨테이너 존재 여부 확인
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                if (containerClient.Exists())
                {
                    Source.Log.log.Info("컨테이너가 이미 존재합니다.");
                }
                else
                {
                    // 컨테이너 생성
                    containerClient.Create();
                    Source.Log.log.Info("컨테이너가 생성되었습니다.");
                    Source.Log.log.Info("컨테이너 이름: " + containerClient.Name);

                    ChangeAccessLevelToContainer(storageConnectionString, containerClient.Name, PublicAccessType.BlobContainer);
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        static void ChangeAccessLevelToContainer(string storageConnectionString, string containerName, PublicAccessType accessType)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);

                // 컨테이너 존재 여부 확인
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                if (containerClient.Exists())
                {
                    // 액세스 수준 변경
                    containerClient.SetAccessPolicy(accessType);

                    Source.Log.log.Info("액세스 수준이 컨테이너로 변경되었습니다.");
                    Source.Log.log.Info("컨테이너 이름: " + containerClient.Name);
                }
                else
                {
                    Source.Log.log.Info("컨테이너가 존재하지 않습니다.");
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(View.CreditPayment.WndProc);
        }

        #region /// QRcode, 업로드, 프린트 ///

        public static void uploadimgthread()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                Source.Log.log.Info("업로드 할 이미지 : " + View.ImgCompose.SendKakaoImgName + " php이름 : " + filename + " QR코드 이름 : " + "QRCode" + View.ImgCompose.SendKakaoImgName);

                uploadimg();

                Source.Log.log.Info("Azure 업로드 및 QR코드 생성 완료");
                uploadthread.Abort();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private static void uploadimg()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작 시작");
                string connectionString = "";
                string containerName = "";
                containerName = inifoldername;
                string VideoDirectory = MainWindow.Videopath;

                CheckAndCreateContainer(connectionString, containerName);

                string localDirectory = MainWindow.SnsPath;

                UploadImagesToBlobStorage(connectionString, containerName, localDirectory);

                if (checkvideo == "1")
                {
                    UploadVideoToBlobStorage(connectionString, containerName, VideoDirectory);
                }

                string containerUrl = $"";

                string[] imageUrls = GetImageUrls(containerUrl, containerName);

                if (checkvideo == "1")
                {
                    string[] videoUrls = GetVideoUrls(containerUrl, containerName);
                }

                // 프로그램 실행 경로의 HTML 폴더에 저장되도록 경로 수정
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory; // 현재 애플리케이션의 실행 경로
                string htmlFolderPath = System.IO.Path.Combine(appDirectory, "HTML"); // HTML 폴더 경로
                string filePath = System.IO.Path.Combine(htmlFolderPath, filename); // 파일 경로

                // HTML 폴더가 없는 경우 생성
                if (!Directory.Exists(htmlFolderPath))
                {
                    Directory.CreateDirectory(htmlFolderPath);
                }

                string html = GenerateHtmlImageGallery(containerUrl, imageUrls);

                File.WriteAllText(filePath, html);

                Delay(1000);

                // HTML 파일을 $web 컨테이너에 업로드합니다.
                //UploadHtmlToWebContainer(connectionString, containerName, filePath);
                if (filePath != null)
                {
                    string webAppFtpUrl = ""; // Azure 웹 앱의 FTP URL
                    string username = ""; // FTP 사용자 이름
                    string password = ""; // FTP 비밀번호

                    string ftpUri = "";
                    if (SorR == "S")
                    {
                        ftpUri = $"";
                    }
                    else
                    {
                        ftpUri = $"";
                    }

                    // FTP 연결 설정
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUri);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(username, password);

                    // 로컬 파일을 읽기 위한 스트림 열기
                    using (FileStream localFileStream = File.OpenRead(filePath))
                    {
                        // FTP 스트림 열기
                        using (Stream ftpStream = request.GetRequestStream())
                        {
                            byte[] buffer = new byte[10240];
                            int bytesRead;
                            while ((bytesRead = localFileStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ftpStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }

                    if (inifoldername == "animalhospital" || MainWindow.checkkakao == "0")
                    {
                        while (!checkupload)
                        {
                            Delay(500);
                            if (checknokakaobtn)
                            {
                                return;
                            }
                        }
                        Source.SendMessage.Sendmessage(phonenumber, MainWindow.inifoldername, MainWindow.htmlUrl);
                    }
                    else if (MainWindow.qroption == 1)
                    {
                        while (!checkupload)
                        {
                            Delay(500);

                            checkupload = true;
                        }
                    }

                    Source.Log.log.Info("파일 업로드 완료.");
                }

                Source.Log.log.Info("이미지 갤러리 HTML 파일이 성공적으로 생성되었고 데스크톱에 저장되었습니다!");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private static void UploadVideoToBlobStorage(string connectionString, string containerName, string localDirectory) //컨테이너 업로드 구문
        {
            try
            {
                // Azure Blob Service 연결
                BlobServiceClient blobServiceClient = new BlobServiceClient(MainWindow.connectionString);

                // 컨테이너 생성
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(MainWindow.containerName);
                containerClient.CreateIfNotExists();

                string[] imagePaths;

                if (camnumber.ToString() == "1")
                {
                    // 이미지 파일 경로
                    imagePaths = new string[]
                    {
                        MainWindow.Videopath + @"\" + "Video_" + View.TakePic.videoname + ".mp4"
                    };
                }
                else
                {

                    // 이미지 파일 경로
                    imagePaths = new string[]
                    {
                        MainWindow.Videopath + @"\" + "Video_" + View.WebCam.videoname + ".mp4"
                    };
                }

                Source.Log.log.Info("동영상 업로드 시작");

                // 이미지 업로드
                foreach (string imagePath in imagePaths)
                {
                    using (FileStream stream = File.OpenRead(imagePath))
                    {
                        string blobName = "Machine1/Video/" + System.IO.Path.GetFileName(imagePath);
                        BlobClient blobClient = containerClient.GetBlobClient(blobName);
                        blobClient.Upload(stream, overwrite: true);
                    }
                }
                Source.Log.log.Info("동영상 업로드 종료");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private static string[] GetVideoUrls(string containerUrl, string containerName)
        {
            string connectionString = $"";
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            var blobs = containerClient.GetBlobs(prefix: "Machine1/Video/");

            List<string> imageUrls = new List<string>();

            string fileName;

            if (camnumber.ToString() == "1")
            {
                fileName = "Video_" + View.TakePic.videoname + ".mp4";
            }
            else
            {
                fileName = "Video_" + View.WebCam.videoname + ".mp4";
            }

            foreach (var blobItem in blobs)
            {
                if (blobItem.Name.EndsWith($"/{fileName}", StringComparison.OrdinalIgnoreCase))
                {
                    string imageUrl = $"{containerUrl}/{blobItem.Name}";
                    imageUrls.Add(imageUrl);
                }
            }

            return imageUrls.ToArray();
        }

        private static string GenerateHtmlImageGallery(string containerUrl, string[] imageUrls)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            string imageName = View.ImgCompose.SendKakaoImgName;
            string videoname = "";

            if (checkvideo == "1")
            {
                if (camnumber.ToString() == "1")
                {
                    videoname = "Video_" + View.TakePic.videoname + ".mp4";
                }
                else
                {
                    videoname = "Video_" + View.WebCam.videoname + ".mp4";
                }
            }

            if (checkvideo == "1")
            {
                htmlBuilder.AppendLine("<?php");
                htmlBuilder.AppendLine("if (isset($_POST['download'])) {");
                htmlBuilder.AppendLine("\t// 파일 경로 설정");
                htmlBuilder.AppendLine("\t$file_url =" + "'" + containerUrl + "/Machine1/Video/" + videoname + "'" + ";");
                htmlBuilder.AppendLine("\t// 파일명 설정");
                htmlBuilder.AppendLine("\t$file_name = basename($file_url);");
                htmlBuilder.AppendLine("\t// 파일 크기 가져오기 (원격 파일 크기를 가져오기 위해 cURL 사용)");
                htmlBuilder.AppendLine("\t$file_size = filesize_remote($file_url);");
                htmlBuilder.AppendLine("\t// 파일 다운로드 헤더 설정");
                htmlBuilder.AppendLine("\theader('Content-Description: File Transfer');");
                htmlBuilder.AppendLine("\theader('Content-Type: application/octet-stream');");
                htmlBuilder.AppendLine("\theader('Content-Disposition: attachment; filename=' . $file_name);");
                htmlBuilder.AppendLine("\theader('Content-Transfer-Encoding: binary');");
                htmlBuilder.AppendLine("\theader('Expires: 0');");
                htmlBuilder.AppendLine("\theader('Cache-Control: must-revalidate');");
                htmlBuilder.AppendLine("\theader('Pragma: public');");
                htmlBuilder.AppendLine("\theader('Content-Length: ' . $file_size);");
                htmlBuilder.AppendLine("\t// 원격 파일 다운로드 함수");
                htmlBuilder.AppendLine("\t$file_content = remote_file_download($file_url);");
                htmlBuilder.AppendLine("\t// 파일 출력");
                htmlBuilder.AppendLine("\techo $file_content;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("// 파일 크기 가져오는 함수");
                htmlBuilder.AppendLine("function filesize_remote($url) {");
                htmlBuilder.AppendLine("\t$ch = curl_init($url);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_NOBODY, true);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_RETURNTRANSFER, true);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_HEADER, true);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_FOLLOWLOCATION, true); // 리다이렉션을 따라감");
                htmlBuilder.AppendLine("\tcurl_exec($ch);");
                htmlBuilder.AppendLine("\t$size = curl_getinfo($ch, CURLINFO_CONTENT_LENGTH_DOWNLOAD);");
                htmlBuilder.AppendLine("\tcurl_close($ch);");
                htmlBuilder.AppendLine("\treturn $size;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("// 원격 파일 다운로드 함수");
                htmlBuilder.AppendLine("function remote_file_download($url) {");
                htmlBuilder.AppendLine("\tset_time_limit(0);");
                htmlBuilder.AppendLine("\t$ch = curl_init();");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_URL, $url);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_CONNECTTIMEOUT, 0);");
                htmlBuilder.AppendLine("\t$file = curl_exec($ch);");
                htmlBuilder.AppendLine("\tcurl_close($ch);");
                htmlBuilder.AppendLine("\treturn $file;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("?>");
                htmlBuilder.AppendLine("<!DOCTYPE html>");
                htmlBuilder.AppendLine("<html lang=\"en\">");
                htmlBuilder.AppendLine("<head>");
                htmlBuilder.AppendLine("<meta charset=\"UTF-8\">");
                htmlBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
                htmlBuilder.AppendLine("<title>Photo View</title>");
                htmlBuilder.AppendLine("<link rel=\"stylesheet\" href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css\">");
                htmlBuilder.AppendLine("<style>");
                htmlBuilder.AppendLine("body {");
                htmlBuilder.AppendLine("    font-family: Arial, sans-serif;");
                htmlBuilder.AppendLine("    background-color: #f5f5f5;");
                htmlBuilder.AppendLine("    text-align: center;");
                htmlBuilder.AppendLine("    margin: 0;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine(".container {");
                htmlBuilder.AppendLine("    padding: 15px;");
                htmlBuilder.AppendLine("    border-radius: 10px;");
                htmlBuilder.AppendLine("    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("h1 {");
                htmlBuilder.AppendLine("    color: #333;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("p {");
                htmlBuilder.AppendLine("    color: #666;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine(".image-container {");
                htmlBuilder.AppendLine("    margin-top: 20px;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine(".image-container img {");
                htmlBuilder.AppendLine("    max-width: 100%;");
                htmlBuilder.AppendLine("    height: auto;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine(".video-container {");
                htmlBuilder.AppendLine("    max-width: 100%;");
                htmlBuilder.AppendLine("    margin-top: 1%;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine(".video-container video {");
                htmlBuilder.AppendLine("    max-width: 100%;");
                htmlBuilder.AppendLine("    height: 350px;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("</style>");
                htmlBuilder.AppendLine("</head>");
                htmlBuilder.AppendLine("<body>");
                htmlBuilder.AppendLine("<div class=\"container mt-1\">");
                htmlBuilder.AppendLine("<p><strong>다운로드를 누르고 기다려주세요!</strong><br><strong>Click download and wait!</strong></p>");
                htmlBuilder.AppendLine("<div class=\"row justify-content-center\">");
                htmlBuilder.AppendLine("<div class=\"col-6 col-md-4 mb-3\">");
                htmlBuilder.AppendLine("<form action=\"" + containerUrl + "/Machine1/Photo/" + imageName + "\" method=\"get\">");
                htmlBuilder.AppendLine("<input type=\"submit\" value=\"Download Image\" class=\"btn btn-primary btn-block\">");
                htmlBuilder.AppendLine("</form>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("<div class=\"col-6 col-md-4 mb-3\">");
                htmlBuilder.AppendLine("<form action=\"" + containerUrl + "/Machine1/Video/" + videoname + "\" method=\"get\">");
                htmlBuilder.AppendLine("<input type=\"submit\" name=\"download\" value=\"Download Video\" class=\"btn btn-warning btn-block\">");
                htmlBuilder.AppendLine("</form>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("<p style=\"color: red;\">아이폰 사용자는 아래 버튼을 눌러 다운받아주세요! <br>iPhone users<br>please click the button below to download!</p>");
                htmlBuilder.AppendLine("<div class=\"row justify-content-center\">");
                htmlBuilder.AppendLine("<div class=\"col-6 col-md-4 mb-3\">");
                htmlBuilder.AppendLine("<form action=\"" + containerUrl + "/Machine1/Photo/" + imageName + "\" method=\"get\">");
                htmlBuilder.AppendLine("<input type=\"submit\" value=\"Download Image\" class=\"btn btn-primary btn-block\">");
                htmlBuilder.AppendLine("</form>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("<div class=\"col-6 col-md-4 mb-3\">");
                htmlBuilder.AppendLine("<form action=\"" + "\" method=\"post\">");
                htmlBuilder.AppendLine("<input type=\"submit\" name=\"download\" value=\"Download Video\" class=\"btn btn-warning btn-block\">");
                htmlBuilder.AppendLine("</form>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");
                htmlBuilder.AppendLine("<div class=\"image-container\">");
                htmlBuilder.AppendLine("<img src=\"" + containerUrl + "/Machine1/Photo/" + imageName + "\" alt=\"Photo\" width=\"100%\" height=\"auto\">");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("<div class=\"video-container\">");
                htmlBuilder.AppendLine("<video controls>");
                htmlBuilder.AppendLine("<source src=\"" + containerUrl + "/Machine1/Video/" + videoname + "\" type=\"video/mp4\">");
                htmlBuilder.AppendLine("Your browser does not support the video tag.");
                htmlBuilder.AppendLine("</video>");
                htmlBuilder.AppendLine("</div>");
            }
            else
            {
                htmlBuilder.AppendLine("<?php");
                htmlBuilder.AppendLine("if (isset($_POST['download'])) {");
                htmlBuilder.AppendLine("\t// 파일 경로 설정");
                htmlBuilder.AppendLine("\t$file_name = basename($file_url);");
                htmlBuilder.AppendLine("\t// 파일 크기 가져오기 (원격 파일 크기를 가져오기 위해 cURL 사용)");
                htmlBuilder.AppendLine("\t$file_size = filesize_remote($file_url);");
                htmlBuilder.AppendLine("\t// 파일 다운로드 헤더 설정");
                htmlBuilder.AppendLine("\theader('Content-Description: File Transfer');");
                htmlBuilder.AppendLine("\theader('Content-Type: application/octet-stream');");
                htmlBuilder.AppendLine("\theader('Content-Disposition: attachment; filename=' . $file_name);");
                htmlBuilder.AppendLine("\theader('Content-Transfer-Encoding: binary');");
                htmlBuilder.AppendLine("\theader('Expires: 0');");
                htmlBuilder.AppendLine("\theader('Cache-Control: must-revalidate');");
                htmlBuilder.AppendLine("\theader('Pragma: public');");
                htmlBuilder.AppendLine("\theader('Content-Length: ' . $file_size);");
                htmlBuilder.AppendLine("\t// 원격 파일 다운로드 함수");
                htmlBuilder.AppendLine("\t$file_content = remote_file_download($file_url);");
                htmlBuilder.AppendLine("\t// 파일 출력");
                htmlBuilder.AppendLine("\techo $file_content;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("// 파일 크기 가져오는 함수");
                htmlBuilder.AppendLine("function filesize_remote($url) {");
                htmlBuilder.AppendLine("\t$ch = curl_init($url);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_NOBODY, true);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_RETURNTRANSFER, true);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_HEADER, true);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_FOLLOWLOCATION, true); // 리다이렉션을 따라감");
                htmlBuilder.AppendLine("\tcurl_exec($ch);");
                htmlBuilder.AppendLine("\t$size = curl_getinfo($ch, CURLINFO_CONTENT_LENGTH_DOWNLOAD);");
                htmlBuilder.AppendLine("\tcurl_close($ch);");
                htmlBuilder.AppendLine("\treturn $size;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("// 원격 파일 다운로드 함수");
                htmlBuilder.AppendLine("function remote_file_download($url) {");
                htmlBuilder.AppendLine("\tset_time_limit(0);");
                htmlBuilder.AppendLine("\t$ch = curl_init();");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_URL, $url);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);");
                htmlBuilder.AppendLine("\tcurl_setopt($ch, CURLOPT_CONNECTTIMEOUT, 0);");
                htmlBuilder.AppendLine("\t$file = curl_exec($ch);");
                htmlBuilder.AppendLine("\tcurl_close($ch);");
                htmlBuilder.AppendLine("\treturn $file;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("?>");
                htmlBuilder.AppendLine("<!DOCTYPE html>");
                htmlBuilder.AppendLine("<html lang=\"en\">");
                htmlBuilder.AppendLine("<head>");
                htmlBuilder.AppendLine("<meta charset=\"UTF-8\">");
                htmlBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
                htmlBuilder.AppendLine("<title>Photo View</title>");
                htmlBuilder.AppendLine("<link rel=\"stylesheet\" href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css\">");
                htmlBuilder.AppendLine("<style>");
                htmlBuilder.AppendLine("body {");
                htmlBuilder.AppendLine("    font-family: Arial, sans-serif;");
                htmlBuilder.AppendLine("    background-color: #f5f5f5;");
                htmlBuilder.AppendLine("    text-align: center;");
                htmlBuilder.AppendLine("    margin: 0;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine(".container {");
                htmlBuilder.AppendLine("    padding: 15px;");
                htmlBuilder.AppendLine("    border-radius: 10px;");
                htmlBuilder.AppendLine("    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("h1 {");
                htmlBuilder.AppendLine("    color: #333;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("p {");
                htmlBuilder.AppendLine("    color: #666;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine(".image-container {");
                htmlBuilder.AppendLine("    margin-top: 20px;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine(".image-container img {");
                htmlBuilder.AppendLine("    max-width: 100%;");
                htmlBuilder.AppendLine("    height: auto;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("</style>");
                htmlBuilder.AppendLine("</head>");
                htmlBuilder.AppendLine("<body>");
                htmlBuilder.AppendLine("<div class=\"container mt-1\">");
                htmlBuilder.AppendLine("<p><strong>다운로드를 누르고 기다려주세요!</strong><br><strong>Click download and wait!</strong></p>");
                htmlBuilder.AppendLine("<div class=\"row justify-content-center\">");
                htmlBuilder.AppendLine("<div class=\"col-6 col-md-4 mb-3\">");
                htmlBuilder.AppendLine("<form action=\"" + containerUrl + "/Machine1/Photo/" + imageName + "\" method=\"get\">");
                htmlBuilder.AppendLine("<input type=\"submit\" value=\"Download Image\" class=\"btn btn-primary btn-block\">");
                htmlBuilder.AppendLine("</form>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("<p style=\"color: red;\">아이폰은 사파리로 접속해주세요.<br>If you are using an iPhone, please use Safari.</p>");
                htmlBuilder.AppendLine("</div>");
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");
                htmlBuilder.AppendLine("<div class=\"image-container\">");
                htmlBuilder.AppendLine("<img src=\"" + containerUrl + "/Machine1/Photo/" + imageName + "\" alt=\"Photo\" width=\"100%\" height=\"auto\">");
                htmlBuilder.AppendLine("</div>");
            }

            string generatedHtml = htmlBuilder.ToString();

            return htmlBuilder.ToString();

        }

        private static string[] GetImageUrls(string containerUrl, string containerName)
        {
            string connectionString = $"";
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            var blobs = containerClient.GetBlobs(prefix: "Machine1/Photo/");

            List<string> imageUrls = new List<string>();

            string fileName = "KakaoPhoto" + View.ImgCompose.kakaonumbercheck.ToString() + ".png";

            foreach (var blobItem in blobs)
            {
                if (blobItem.Name.EndsWith($"/{fileName}", StringComparison.OrdinalIgnoreCase))
                {
                    string imageUrl = $"{containerUrl}/{blobItem.Name}";
                    imageUrls.Add(imageUrl);
                }
            }

            return imageUrls.ToArray();
        }

        private static void UploadImagesToBlobStorage(string connectionString, string containerName, string localDirectory) //컨테이너 업로드 구문
        {
            try
            {
                // Azure Blob Service 연결
                BlobServiceClient blobServiceClient = new BlobServiceClient(MainWindow.connectionString);

                // 컨테이너 생성
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(MainWindow.containerName);
                containerClient.CreateIfNotExists();

                // 이미지 파일 경로
                string[] imagePaths = new string[]
                {
                    MainWindow.SnsPath + @"\" + View.ImgCompose.SendKakaoImgName
                };

                Source.Log.log.Info("이미지 업로드 시작");

                // 이미지 업로드
                foreach (string imagePath in imagePaths)
                {
                    using (FileStream stream = File.OpenRead(imagePath))
                    {
                        string blobName = "Machine1/Photo/" + System.IO.Path.GetFileName(imagePath);
                        BlobClient blobClient = containerClient.GetBlobClient(blobName);
                        blobClient.Upload(stream, overwrite: true);
                    }
                }
                Source.Log.log.Info("이미지 업로드 종료");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
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

        #endregion

        #region /// 웹캠 설정 ///

        private bool WebCamInitialize(int width = 1920, int height = 1080, int frameRate = 12, int index = 0)
        {
            try
            {
                if (CameraService.AvailableCameras == null || CameraService.AvailableCameras.Count <= 0)
                {
                    return false;
                }
                Webcam = CameraService.AvailableCameras[index];
                setFrameSource(new CameraFrameSource(Webcam));
                WebCamFrameSource.Camera.CaptureWidth = 1920;
                WebCamFrameSource.Camera.CaptureHeight = 1080;
                WebCamFrameSource.Camera.Fps = frameRate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        private void setFrameSource(CameraFrameSource cameraFrameSource)
        {
            if (WebCamFrameSource != cameraFrameSource)
            {
                WebCamFrameSource = cameraFrameSource;
            }
        }

        #endregion
    }
}
