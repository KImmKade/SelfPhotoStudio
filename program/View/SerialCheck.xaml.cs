using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.IO.Ports;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Drawing.Printing;
using System.Windows.Navigation;
using EOSDigital.API;
using EOSDigital.SDK;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Reflection;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Size = OpenCvSharp.Size;
using System.Net;
using Touchless.Vision.Camera;
using System.Drawing.Imaging;
using Npgsql;

namespace wpfTest.View
{
    /// <summary>
    /// SerialCheck.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SerialCheck : System.Windows.Controls.Page
    {
        #region///카메라 변수///

        System.Windows.Forms.FolderBrowserDialog SaveFolderBrowser = new System.Windows.Forms.FolderBrowserDialog(); //폴더 지정
        string savePath = string.Empty; //저장 폴더 경로 글로벌 선언
        int ErrCount; //Error 갯 수
        object ErrLock = new object(); //Error 발생 알림
        CameraValue[] AvList; //AV조절
        CameraValue[] TvList; //TV조절
        CameraValue[] ISOList; //ISO조절

        //--------------------------웹켐--------------------------//

        //VideoCapture video = new VideoCapture(0); // 웹켐
        Thread thread;
        Bitmap bitmap;
        Bitmap savebitmap;
        bool videothread = true;
        bool testphototime = false;
        bool Camonoff = false;
        Mat frame = new Mat();
        Mat flipframe = new Mat();

        int zoomcount = 0;
        Mat resizemat = new Mat();
        int zoomwidth, zoomheight, startx, starty;

        int picturececk = 0;

        #endregion

        #region///시리얼 변수///

        private SerialPort port = Payment.port;

        #endregion

        #region ///프린터 변수///

        public string correctedPrintername { get; private set; }
        string papercountdown;

        #endregion

        #region///INI Import///

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        #region///타이머 변수///

        #endregion

        public SerialCheck()
        {
            Source.Log.log.Info("옵션 페이지 진입");
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                if (MainWindow.SorR == "R")
                {
                    VersionTB.Text = "Rental " + MainWindow.Version;
                }
                else
                {
                    VersionTB.Text = "Sell " + MainWindow.Version;
                }

                //프린터
                SetAutoPrinter();
                printerLabel.Content = correctedPrintername;

                //시리얼 포트
                if (MainWindow.checkfreeornot == "0")
                {
                    if (port.IsOpen == false)
                    {
                        SerialButton.IsEnabled = true;
                        DisconnectButton.IsEnabled = false;
                        RefreshButton.IsEnabled = true;
                    }
                    else
                    {
                        SerialButton.IsEnabled = false;
                        DisconnectButton.IsEnabled = true;
                        RefreshButton.IsEnabled = false;
                        checkinsert.IsEnabled = true;
                    }

                    if (MainWindow.comportnum == "")
                    {
                        SerialButton.IsEnabled = false;
                    }
                }

                //카메라
                if (MainWindow.camnumber.ToString() == "1")
                {
                    Main.APIHandler = new CanonAPI();
                    Main.APIHandler.CameraAdded += APIHandler_CameraAdded;
                    ErrorHandler.SevereErrorHappened += ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
                    ErrorHandler.NonSevereErrorHappened += ErrorHandler_NonSevereErrorHappened; //에러 발생 시

                    SavePathTextBox.Text = MainWindow.PhotoPath; // 초기 저장폴더 지정

                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        Main.SetImageAction2 = (TransformedBitmap img) => { Main.bgbrush.ImageSource = img; }; // Live View에 그려지는 이미지 Action
                    }
                    else
                    {
                        Main.SetImageAction = (BitmapImage img) => { Main.bgbrush.ImageSource = img; }; // Live View에 그려지는 이미지 Action 
                    }
                    SaveFolderBrowser.Description = "Save Image To...";
                    RefreshCamera(); //연결된 카메라 정보 수집
                    Main.IsInit = true;
                    if (Main.MainCamera == null)
                    {
                        OpenSession();
                    }
                }
                else
                {
                    try
                    {
                        if (MainWindow.video == null)
                        {
                            MainWindow.video = new VideoCapture(0);
                        }
                        MainWindow.Webcam = CameraService.AvailableCameras[0];
                        CameraLabel.Content = MainWindow.Webcam.Name;
                    }
                    catch (Exception webex)
                    {
                        MessageBox.Show("카메라 연결을 확인해주세요.");
                        Source.Log.log.Error(webex);
                        Source.Log.log.Error("카메라 연결을 확인해주세요.");
                        CameraLabel.Content = "No Connection";
                    }
                    WebCamInitialize(MainWindow.CaptureWidth, MainWindow.CaptureHeight, 12, 0);
                }

                //남은 용지 매수
                Initialization.Text = Convert.ToString(MainWindow.papercount);

                LoadSetting();

                switch (MainWindow.zoomratio)
                {
                    case 0.9:
                        zoomcount = 1;
                        break;
                    case 0.8:
                        zoomcount = 2;
                        break;
                    case 0.7:
                        zoomcount = 3;
                        break;
                    case 0.65:
                        zoomcount = 4;
                        break;
                    case 0.6:
                        zoomcount = 5;
                        break;
                    case 0.5:
                        zoomcount = 6;
                        break;
                }
                if (MainWindow.inifoldername == "dearpic4" || MainWindow.inifoldername == "dearpic3")
                {
                    LVCanvus.Width = 245;
                    FlipCanvas.Width = 245;
                }

                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    checktogethertb.IsEnabled = true;
                    checktogethertb.Opacity = 1;
                    
                    if (MainWindow.checkingtogether == "Use")
                    {
                        togetheruse.IsChecked = true;
                    }
                    else if (MainWindow.checkingtogether != "Use")
                    {
                        solouse.IsChecked = true;
                    }
                }
                else if (!MainWindow.inifoldername.Contains("mediagram"))
                {
                    checktogethertb.IsEnabled = false;
                    checktogethertb.Opacity = 0;
                    solouse.IsChecked = true;
                }

                LVCanvus.Background = System.Windows.Media.Brushes.LightGray;
                FlipCanvas.Background = System.Windows.Media.Brushes.LightGray;

                Source.Log.log.Info("관리자 페이지 로드 완료");

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (DllNotFoundException)
            {
                Source.Log.log.Error("Canon DLLs not Found!");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #region///시리얼통신 함수///

        private void CheckSerial()
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 지폐기 자동연결 작동");
            try
            {
                if (MainWindow.comportnum != "")
                {
                    if (port.PortName == null)
                    {
                        port.PortName = MainWindow.comportnum;
                    }
                    port.BaudRate = 9600;
                    port.DataBits = 8;
                    port.StopBits = StopBits.One;
                    port.Parity = Parity.None;
                    if (!port.IsOpen)
                    {
                        port.Open();
                        Source.Log.log.Info("지폐기 연결됨");
                    }
                    SerialLabel.Content = port.PortName;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                else
                {
                    MessageBox.Show("등록된 지폐기가 없습니다.\n\n지폐기를 등록해주세요.");
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 컴포트 검색 버튼 클릭");
            try
            {
                if (port.IsOpen)
                {
                    port.Close();
                    port = null;
                }
                SerialListBox.Items.Clear();
                SerialListBox.ItemsSource = SerialPort.GetPortNames();
                SerialButton.IsEnabled = true;
                RefreshButton.IsEnabled = false;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void SerialButton_Click(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 컴포트 연결 버튼 클릭");
            try
            {
                if (!port.IsOpen)
                {
                    try
                    {
                        port.PortName = SerialListBox.SelectedItem.ToString();
                        port.BaudRate = 9600;
                        port.DataBits = 8;
                        port.StopBits = StopBits.One;
                        port.Parity = Parity.None;
                        port.Open();
                        Source.Log.log.Info("컴포트 " + SerialListBox.SelectedItem.ToString() + "으로 연결함");
                        string Recieve = port.ReadExisting();
                        SerialLabel.Content = "시리얼 포트가 열렸습니다.";
                        Source.Log.log.Info("시리얼 포트가 열림");
                        SerialButton.IsEnabled = false;
                        DisconnectButton.IsEnabled = true;
                        DataSend('e', 's', '\u0001');
                        DataSend('H', 'i', '?');
                        Thread.Sleep(100);
                        Recieve = port.ReadExisting();
                        if (Recieve.Length < 5 || Recieve[0] != '$')
                        {
                            Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - ERROR : " + Recieve);
                            return;
                        }
                        if (Recieve.Substring(0, 4) == "$me!")
                        {
                            MessageBox.Show("지폐기와 연결이 되었습니다.");
                            Source.Log.log.Info("지폐기 연결 성공");
                            SerialLabel.Content = port.PortName;
                            MainWindow.comportnum = port.PortName;
                            MainWindow.ComNum.Append(port.PortName);
                            //DisconnectButton.IsEnabled = false;
                            WritePrivateProfileString("Setting", "ComportNumber", port.PortName, MainWindow.iniPath);
                            MainWindow.comportnum = port.PortName;
                            checkinsert.IsEnabled = true;
                        }
                        if (Recieve.Substring(0, 4) == null)
                        {
                            port.Close();
                            Source.Log.log.Info("컴포트 닫힘(지폐기로부터 응답 신호 없음)");
                            SerialLabel.Content = "No Connect";
                            SerialButton.IsEnabled = true;
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("해당 시리얼포트가 없습니다.");
                        SerialLabel.Content = "해당 시리얼포트가 없습니다.";
                        Source.Log.log.Info("해당 시리얼 포트가 없음");
                    }
                }
                else
                {
                    MessageBox.Show("포트가 이미 열려 있습니다.");
                    SerialLabel.Content = "포트가 이미 열려 있습니다.";
                    Source.Log.log.Info("포트가 이미 열려 있음");
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void DataSend(char byte2, char byte3, char byte4) //지폐기로 데이터 보내기
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 지폐기로 데이터 보냄 보낸데이터 : " + byte2 + byte3 + byte4 + (byte)(byte2 + byte3 + byte4));
            try
            {
                byte[] array = new byte[5]
                {
                    36,
                    (byte)byte2,
                    (byte)byte3,
                    (byte)byte4,
                    (byte)(byte2 + byte3 + byte4)
                };
                port.Write(array, 0, array.Length);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 컴포트 연결 해제 버튼 클릭");
            try
            {
                DisconnectButton.IsEnabled = false;
                if (port.IsOpen)
                {
                    DataSend('S', 'A', '\u000e');
                    port.Close();

                    SerialLabel.Content = "포트가 닫혔습니다.";
                    Source.Log.log.Info("컴포트 닫힘");
                    if (SerialListBox.ItemsSource == null)
                    {
                        SerialButton.IsEnabled = false;
                        RefreshButton.IsEnabled = true;
                    }
                    else
                    {
                        SerialButton.IsEnabled = true;
                    }
                    checkinsert.IsEnabled = false;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                else
                {
                    SerialLabel.Content = "포트가 이미 닫혀 있습니다.";
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 저장버튼 클릭");
            try
            {
                MainWindow.Adminlogin = 0;

                SettingGroupBox.IsEnabled = false;
                InitGroupBox.IsEnabled = false;
                StoreName.IsEnabled = false;
                MachineNum.IsEnabled = false;
                checkpayment.IsEnabled = false;
                NextPageBtn.IsEnabled = false;

                // 재촬영 설정
                MainWindow.retake.Clear();
                MainWindow.retake.Append(MainWindow.checkretake);
                WritePrivateProfileString("Setting", "ReTake", MainWindow.checkretake, MainWindow.iniPath); // ini 파일에 저장(카테고리 Setting, 변수이름 ReTake)

                MainWindow.initimerlocation.Clear();
                MainWindow.initimerlocation.Append(MainWindow.timerlocation);
                WritePrivateProfileString("Setting", "timerlocation", MainWindow.timerlocation, MainWindow.iniPath);

                //카카오톡 여부
                MainWindow.checksns.Clear();
                MainWindow.checksns.Append(MainWindow.checkkakao);
                WritePrivateProfileString("Setting", "Kakao", MainWindow.checkkakao, MainWindow.iniPath); // ini 파일에 저장(카테고리 Setting 변수이름 Kakao)

                //지폐기 사용여부
                MainWindow.checkfree.Clear();
                MainWindow.checkfree.Append(MainWindow.checkfreeornot);
                WritePrivateProfileString("Setting", "BillMachineUse", MainWindow.checkfreeornot, MainWindow.iniPath);

                //카메라 셋팅
                WritePrivateProfileString("Setting", "CamVersion", MainWindow.camnumber.ToString(), MainWindow.iniPath);

                //타이머 사용여부
                MainWindow.Timer.Clear();
                MainWindow.Timer.Append(MainWindow.checktimeruse);
                WritePrivateProfileString("Setting", "TimerUse", MainWindow.checktimeruse, MainWindow.iniPath);

                MainWindow.inicoupon.Clear();
                MainWindow.inicoupon.Append(MainWindow.coupon);
                WritePrivateProfileString("Setting", "coupon", MainWindow.coupon, MainWindow.iniPath);

                //페이지 카운터
                MainWindow.pagecount.Clear();
                MainWindow.pagecount.Append(MainWindow.count);
                WritePrivateProfileString("Setting", "PageTimer", MainWindow.count, MainWindow.iniPath);

                //사진 카운터
                MainWindow.phototime.Clear();
                MainWindow.phototime.Append(MainWindow.photocount);
                WritePrivateProfileString("Setting", "PhotoTimer", MainWindow.photocount, MainWindow.iniPath);

                //돈 셋팅
                if (MainWindow.checkfreeornot == "0")
                {
                    MainWindow.inisetmoney.Clear();
                    MainWindow.inisetmoney.Append(MainWindow.moneyset);
                    WritePrivateProfileString("Setting", "TempFee", MainWindow.moneyset, MainWindow.iniPath);

                    MainWindow.initemp2fee.Clear();
                    MainWindow.initemp2fee.Append(MainWindow.temp2fee);
                    WritePrivateProfileString("Setting", "Temp2Fee", MainWindow.temp2fee, MainWindow.iniPath);

                    MainWindow.initemp3fee.Clear();
                    MainWindow.initemp3fee.Append(MainWindow.temp3fee);
                    WritePrivateProfileString("Setting", "Temp3Fee", MainWindow.temp3fee, MainWindow.iniPath);
                }
                else
                {
                    MainWindow.inisetmoney.Clear();
                    MainWindow.inisetmoney.Append("0");
                    WritePrivateProfileString("Setting", "TempFee", "0", MainWindow.iniPath);
                    MainWindow.moneyset = "0";

                    MainWindow.initemp2fee.Clear();
                    MainWindow.initemp2fee.Append(MainWindow.temp2fee);
                    WritePrivateProfileString("Setting", "Temp2Fee", MainWindow.temp2fee, MainWindow.iniPath);

                    MainWindow.initemp3fee.Clear();
                    MainWindow.initemp3fee.Append(MainWindow.temp3fee);
                    WritePrivateProfileString("Setting", "Temp3Fee", MainWindow.temp3fee, MainWindow.iniPath);
                }

                //Offset 세팅
                MainWindow.getOffSetx.Clear();
                MainWindow.getOffSetx.Append(MainWindow.offsetx);
                WritePrivateProfileString("PrinterSetting", "OffSetX", MainWindow.offsetx, MainWindow.iniPath);

                MainWindow.getOffSety.Clear();
                MainWindow.getOffSety.Append(MainWindow.offsety);
                WritePrivateProfileString("PrinterSetting", "OffSetY", MainWindow.offsety, MainWindow.iniPath);

                MainWindow.getFactorx.Clear();
                MainWindow.getFactorx.Append(MainWindow.factorx);
                WritePrivateProfileString("PrinterSetting", "FactorX", MainWindow.factorx, MainWindow.iniPath);

                MainWindow.getFactory.Clear();
                MainWindow.getFactory.Append(MainWindow.factory);
                WritePrivateProfileString("PrinterSetting", "FactorY", MainWindow.factory, MainWindow.iniPath);

                //템플릿 세팅
                MainWindow.tempselect.Clear();
                MainWindow.tempselect.Append(MainWindow.checktempselect);
                WritePrivateProfileString("TempSetting", "Temp", MainWindow.checktempselect, MainWindow.iniPath);

                //컴포트 세팅
                MainWindow.ComNum.Clear();
                MainWindow.ComNum.Append(MainWindow.comportnum);
                WritePrivateProfileString("Setting", "ComportNumber", MainWindow.comportnum, MainWindow.iniPath);

                //카메라 줌세팅
                MainWindow.Zoom.Clear();
                MainWindow.Zoom.Append(MainWindow.zoomratio);
                WritePrivateProfileString("Setting", "Zoom", MainWindow.zoomratio.ToString(), MainWindow.iniPath);

                //카메라 움직임
                MainWindow.Xcount.Clear();
                MainWindow.Xcount.Append(MainWindow.leftrightcount);
                WritePrivateProfileString("Setting", "leftrightcount", MainWindow.leftrightcount.ToString(), MainWindow.iniPath);
                MainWindow.Ycount.Clear();
                MainWindow.Ycount.Append(MainWindow.updowncount);
                WritePrivateProfileString("Setting", "updowncount", MainWindow.updowncount.ToString(), MainWindow.iniPath);

                //payway 저장
                MainWindow.paymentini.Clear();
                MainWindow.paymentini.Append(MainWindow.paymentway);
                WritePrivateProfileString("Setting", "paymentway", MainWindow.paymentway, MainWindow.iniPath);

                MainWindow.iniautologin.Clear();
                MainWindow.iniautologin.Append(MainWindow.autologin);
                WritePrivateProfileString("Setting", "autologin", MainWindow.autologin, MainWindow.iniPath);

                MainWindow.inicouponkind.Clear();
                MainWindow.inicouponkind.Append(MainWindow.couponkind);
                WritePrivateProfileString("Setting", "couponkind", MainWindow.couponkind, MainWindow.iniPath);

                MainWindow.inicheckcolor.Clear();
                MainWindow.inicheckcolor.Append(MainWindow.checkcolor);
                WritePrivateProfileString("Setting", "coloruse", MainWindow.checkcolor, MainWindow.iniPath);


                //Serialcheck 2 변수

                // 수량 페이지
                MainWindow.pagenumini.Clear();
                MainWindow.pagenumini.Append(MainWindow.pagenum);
                WritePrivateProfileString("Setting", "pagenum", MainWindow.pagenum, MainWindow.iniPath);



                //Admin Setting
                MainWindow.foldername.Clear();
                MainWindow.foldername.Append(MainWindow.inifoldername);
                WritePrivateProfileString("ApiTest", "foldername", MainWindow.foldername.ToString(), MainWindow.iniPath);
                MainWindow.ID.Clear();
                MainWindow.ID.Append(MainWindow.inimachinecode);
                WritePrivateProfileString("ApiTest", "MachineID", MainWindow.ID.ToString(), MainWindow.iniPath);

                //qr사용

                MainWindow.useqr.Clear();
                MainWindow.useqr.Append(MainWindow.qroption);
                WritePrivateProfileString("Setting", "useqr", MainWindow.useqr.ToString(), MainWindow.iniPath);

                // 템플릿 선택 사용

                MainWindow.useselecttemp.Clear();
                MainWindow.useselecttemp.Append(MainWindow.tempoption);
                WritePrivateProfileString("Setting", "selecttemp", MainWindow.tempoption.ToString(), MainWindow.iniPath);

                // 템플릿 수량

                MainWindow.tempnumber.Clear();
                MainWindow.tempnumber.Append(MainWindow.optiontempnum);
                WritePrivateProfileString("TempSetting", "TempNumber", MainWindow.optiontempnum.ToString(), MainWindow.iniPath);

                // 좌우반전

                MainWindow.checkflip.Clear();
                MainWindow.checkflip.Append(MainWindow.checkingflip);
                WritePrivateProfileString("Setting", "FlipUse", MainWindow.checkingflip, MainWindow.iniPath);

                // 사진 비율

                MainWindow.inipicratio.Clear();
                MainWindow.inipicratio.Append(MainWindow.picratio);
                WritePrivateProfileString("TempSetting", "picratio", MainWindow.picratio.ToString(), MainWindow.iniPath);

                // 용지 비율

                MainWindow.iniprinterratio.Clear();
                MainWindow.iniprinterratio.Append(MainWindow.printerratio);
                WritePrivateProfileString("TempSetting", "printerratio", MainWindow.printerratio.ToString(), MainWindow.iniPath);

                //사진 좌표

                // 1번사진

                MainWindow.inipic1startx.Clear();
                MainWindow.inipic1startx.Append(MainWindow.pic1startx);
                WritePrivateProfileString("TempSetting", "pic1startx", MainWindow.pic1startx.ToString(), MainWindow.iniPath);

                MainWindow.inipic1starty.Clear();
                MainWindow.inipic1starty.Append(MainWindow.pic1starty);
                WritePrivateProfileString("TempSetting", "pic1starty", MainWindow.pic1starty.ToString(), MainWindow.iniPath);

                MainWindow.inipic1width.Clear();
                MainWindow.inipic1width.Append(MainWindow.pic1width);
                WritePrivateProfileString("TempSetting", "pic1width", MainWindow.pic1width.ToString(), MainWindow.iniPath);

                MainWindow.inipic1height.Clear();
                MainWindow.inipic1height.Append(MainWindow.pic1height);
                WritePrivateProfileString("TempSetting", "pic1height", MainWindow.pic1height.ToString(), MainWindow.iniPath);

                // 2번 사진

                MainWindow.inipic2startx.Clear();
                MainWindow.inipic2startx.Append(MainWindow.pic2startx);
                WritePrivateProfileString("TempSetting", "pic2startx", MainWindow.pic2startx.ToString(), MainWindow.iniPath);

                MainWindow.inipic2starty.Clear();
                MainWindow.inipic2starty.Append(MainWindow.pic2starty);
                WritePrivateProfileString("TempSetting", "pic2starty", MainWindow.pic2starty.ToString(), MainWindow.iniPath);

                MainWindow.inipic2width.Clear();
                MainWindow.inipic2width.Append(MainWindow.pic2width);
                WritePrivateProfileString("TempSetting", "pic2width", MainWindow.pic2width.ToString(), MainWindow.iniPath);

                MainWindow.inipic2height.Clear();
                MainWindow.inipic2height.Append(MainWindow.pic1height);
                WritePrivateProfileString("TempSetting", "pic2height", MainWindow.pic2height.ToString(), MainWindow.iniPath);

                // 3번 사진

                MainWindow.inipic3startx.Clear();
                MainWindow.inipic3startx.Append(MainWindow.pic3startx);
                WritePrivateProfileString("TempSetting", "pic3startx", MainWindow.pic3startx.ToString(), MainWindow.iniPath);

                MainWindow.inipic3starty.Clear();
                MainWindow.inipic3starty.Append(MainWindow.pic3starty);
                WritePrivateProfileString("TempSetting", "pic3starty", MainWindow.pic3starty.ToString(), MainWindow.iniPath);

                MainWindow.inipic3width.Clear();
                MainWindow.inipic3width.Append(MainWindow.pic3width);
                WritePrivateProfileString("TempSetting", "pic3width", MainWindow.pic3width.ToString(), MainWindow.iniPath);

                MainWindow.inipic3height.Clear();
                MainWindow.inipic3height.Append(MainWindow.pic3height);
                WritePrivateProfileString("TempSetting", "pic3height", MainWindow.pic3height.ToString(), MainWindow.iniPath);

                // 4번 사진

                MainWindow.inipic4startx.Clear();
                MainWindow.inipic4startx.Append(MainWindow.pic4startx);
                WritePrivateProfileString("TempSetting", "pic4startx", MainWindow.pic4startx.ToString(), MainWindow.iniPath);

                MainWindow.inipic4starty.Clear();
                MainWindow.inipic4starty.Append(MainWindow.pic4starty);
                WritePrivateProfileString("TempSetting", "pic4starty", MainWindow.pic4starty.ToString(), MainWindow.iniPath);

                MainWindow.inipic4width.Clear();
                MainWindow.inipic4width.Append(MainWindow.pic4width);
                WritePrivateProfileString("TempSetting", "pic4width", MainWindow.pic4width.ToString(), MainWindow.iniPath);

                MainWindow.inipic4height.Clear();
                MainWindow.inipic4height.Append(MainWindow.pic4height);
                WritePrivateProfileString("TempSetting", "pic4height", MainWindow.pic4height.ToString(), MainWindow.iniPath);

                // 5번 사진

                MainWindow.inipic5startx.Clear();
                MainWindow.inipic5startx.Append(MainWindow.pic5startx);
                WritePrivateProfileString("TempSetting", "pic5startx", MainWindow.pic5startx.ToString(), MainWindow.iniPath);

                MainWindow.inipic5starty.Clear();
                MainWindow.inipic5starty.Append(MainWindow.pic5starty);
                WritePrivateProfileString("TempSetting", "pic5starty", MainWindow.pic5starty.ToString(), MainWindow.iniPath);

                MainWindow.inipic5width.Clear();
                MainWindow.inipic5width.Append(MainWindow.pic5width);
                WritePrivateProfileString("TempSetting", "pic5width", MainWindow.pic5width.ToString(), MainWindow.iniPath);

                MainWindow.inipic5height.Clear();
                MainWindow.inipic5height.Append(MainWindow.pic5height);
                WritePrivateProfileString("TempSetting", "pic5height", MainWindow.pic5height.ToString(), MainWindow.iniPath);

                // 6번 사진

                MainWindow.inipic6startx.Clear();
                MainWindow.inipic6startx.Append(MainWindow.pic6startx);
                WritePrivateProfileString("TempSetting", "pic6startx", MainWindow.pic6startx.ToString(), MainWindow.iniPath);

                MainWindow.inipic6starty.Clear();
                MainWindow.inipic6starty.Append(MainWindow.pic6starty);
                WritePrivateProfileString("TempSetting", "pic6starty", MainWindow.pic6starty.ToString(), MainWindow.iniPath);

                MainWindow.inipic6width.Clear();
                MainWindow.inipic6width.Append(MainWindow.pic6width);
                WritePrivateProfileString("TempSetting", "pic6width", MainWindow.pic6width.ToString(), MainWindow.iniPath);

                MainWindow.inipic6height.Clear();
                MainWindow.inipic6height.Append(MainWindow.pic6height);
                WritePrivateProfileString("TempSetting", "pic6height", MainWindow.pic6height.ToString(), MainWindow.iniPath);

                // QR코드

                MainWindow.iniqrstartx.Clear();
                MainWindow.iniqrstartx.Append(MainWindow.qrstartx);
                WritePrivateProfileString("TempSetting", "QRstartx", MainWindow.qrstartx.ToString(), MainWindow.iniPath);

                MainWindow.iniqrstarty.Clear();
                MainWindow.iniqrstarty.Append(MainWindow.qrstarty);
                WritePrivateProfileString("TempSetting", "QRstarty", MainWindow.qrstarty.ToString(), MainWindow.iniPath);

                MainWindow.iniqrwidth.Clear();
                MainWindow.iniqrwidth.Append(MainWindow.qrwidth);
                WritePrivateProfileString("TempSetting", "QRwidth", MainWindow.qrwidth.ToString(), MainWindow.iniPath);

                MainWindow.iniqrheight.Clear();
                MainWindow.iniqrheight.Append(MainWindow.qrheight);
                WritePrivateProfileString("TempSetting", "QRheight", MainWindow.qrheight.ToString(), MainWindow.iniPath);

                // 텍스트 컬러

                MainWindow.initextcolor.Clear();
                MainWindow.initextcolor.Append(MainWindow.textcolor);
                WritePrivateProfileString("Setting", "textcolor", MainWindow.textcolor.ToString(), MainWindow.iniPath);

                MainWindow.iniqrpageuse.Clear();
                MainWindow.iniqrpageuse.Append(MainWindow.qrpageuse);
                WritePrivateProfileString("Setting", "qrpageuse", MainWindow.qrpageuse.ToString(), MainWindow.iniPath);

                System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(MainWindow.textcolor);
                MainWindow.textbrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));

                MainWindow.iniphotonum.Clear();
                MainWindow.iniphotonum.Append(MainWindow.photonum);
                WritePrivateProfileString("TempSetting", "picnum", MainWindow.photonum.ToString(), MainWindow.iniPath);

                MainWindow.inidatetext.Clear();
                MainWindow.inidatetext.Append(MainWindow.datetext);
                WritePrivateProfileString("Setting", "DateText", MainWindow.datetext.ToString(), MainWindow.iniPath);

                MainWindow.inidatetextcolor.Clear();
                MainWindow.inidatetextcolor.Append(MainWindow.datetextcolor);
                WritePrivateProfileString("Setting", "datetextcolor", MainWindow.datetextcolor.ToString(), MainWindow.iniPath);

                MainWindow.iniKakaoTimer.Clear();
                MainWindow.iniKakaoTimer.Append(MainWindow.kakaotimer);
                WritePrivateProfileString("Setting", "KakaoTimer", MainWindow.kakaotimer.ToString(), MainWindow.iniPath);

                if (cardserialtextbox.Text == "기본값")
                {
                    MainWindow.inicardnum.Clear();
                    MainWindow.inicardnum.Append("AT0403177A");
                    WritePrivateProfileString("Setting", "cardnum", "AT0403177A", MainWindow.iniPath);
                    MainWindow.cardnum = "AT0403177A";
                }
                else if (cardserialtextbox.Text == "")
                {
                    MainWindow.inicardnum.Clear();
                    MainWindow.inicardnum.Append("AT0403177A");
                    WritePrivateProfileString("Setting", "cardnum", "AT0403177A", MainWindow.iniPath);
                    MainWindow.cardnum = "AT0403177A";
                    cardserialtextbox.Text = "기본값";
                }
                else
                {
                    MainWindow.inicardnum.Clear();
                    MainWindow.inicardnum.Append(MainWindow.cardnum);
                    WritePrivateProfileString("Setting", "cardnum", MainWindow.cardnum, MainWindow.iniPath);
                }

                MainWindow.iniSorR.Clear();
                MainWindow.iniSorR.Append(MainWindow.SorR);
                WritePrivateProfileString("Setting", "SorR", MainWindow.SorR, MainWindow.iniPath);

                MainWindow.iniCheckVideo.Clear();
                MainWindow.iniCheckVideo.Append(MainWindow.checkvideo);
                WritePrivateProfileString("Setting", "Video", MainWindow.checkvideo, MainWindow.iniPath);

                MainWindow.inikakaotextcolor.Clear();
                MainWindow.inikakaotextcolor.Append(MainWindow.kakaotextcolor);
                WritePrivateProfileString("Setting", "kakaotextcolor", MainWindow.kakaotextcolor, MainWindow.iniPath);


                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    MainWindow.initogether.Clear();
                    MainWindow.initogether.Append(MainWindow.checkingtogether);
                    WritePrivateProfileString("Setting", "checkingtogether", MainWindow.checkingtogether, MainWindow.iniPath);
                }
                else if (!MainWindow.inifoldername.Contains("mediagram"))
                {
                    MainWindow.initogether.Clear();
                    MainWindow.initogether.Append(MainWindow.checkingtogether);
                    WritePrivateProfileString("Setting", "checkingtogether", MainWindow.checkingtogether, MainWindow.iniPath);
                }

                LVCanvus.Background = System.Windows.Media.Brushes.LightGray;
                FlipCanvas.Background = System.Windows.Media.Brushes.LightGray;

                bool checkinternet = MainWindow.CheckInternetConnection();
                if (checkinternet)
                {
                    string connectionString = "Host=175.125.92.65;Port=5432;Database=onecutdb;User Id=onecut;Password=one6677";
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        // 쿼리문 생성
                        string query = $"UPDATE setting_tb SET temp1fee = @temp1fee, temp2fee = @temp2fee, temp3fee = @temp3fee, payway = @payway, sorr = @sorr WHERE id = @id";

                        // 쿼리 실행
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            // 매개변수 추가
                            command.Parameters.AddWithValue("@temp1fee", Convert.ToInt32(MainWindow.moneyset));
                            command.Parameters.AddWithValue("@temp2fee", Convert.ToInt32(MainWindow.temp2fee));
                            command.Parameters.AddWithValue("@temp3fee", Convert.ToInt32(MainWindow.temp3fee));
                            command.Parameters.AddWithValue("@payway", MainWindow.paymentway);
                            command.Parameters.AddWithValue("@sorr", MainWindow.SorR);
                            command.Parameters.AddWithValue("@id", MainWindow.inifoldername);

                            int rowsAffected = command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }
                }
                MainWindow.containerName = MainWindow.inifoldername;

                string dd = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
                label1.Content = dd + " 저장완료";

                Main.MCheckNum = "";
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 종표버튼 클릭");
            try
            {
                if (MessageBox.Show("종료 하시겠습니까?", "경고", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (MainWindow.paymentini.ToString() == "cardcash" || MainWindow.paymentini.ToString() == "cash")
                    {
                        DataSend('S', 'A', '\u000e');
                        port.Close();
                        Source.Log.log.Info("포트 닫힘");
                        port.Dispose();
                        port = null;
                    }
                    if (Main.MainCamera?.SessionOpen == true)
                    {
                        if (Main.MainCamera.IsLiveViewOn == true)
                        {
                            Main.MainCamera.StopLiveView();
                        }
                        CloseSession();
                    }
                    MainWindow.ProgramClose();
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



        private void EnableUI(bool enable) // UI 활성화
        {
            Source.Log.log.Info("Canon UI 활성화");
            try
            {
                if (!Dispatcher.CheckAccess()) Dispatcher.Invoke((Action)delegate { EnableUI(enable); });
                else
                {
                    SettingGroupBox.IsEnabled = enable;
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

        private void OpenSession()
        {
            Source.Log.log.Info("세션 열림");
            try
            {
                if (Main.MainCamera == null || Main.MainCamera.SessionOpen == false) // 세션이 닫혀있을때
                {
                    Main.MainCamera = Main.CamList[CamListBox.SelectedIndex]; //리스트 박스에서 선택 된 카메라를 메인 카메라로 지정
                    Main.MainCamera.OpenSession();
                    Main.MainCamera.LiveViewUpdated += MainCamera_LiveViewUpdated; //라이브뷰 업데이트 시 동작
                    Main.MainCamera.ProgressChanged += MainCamera_ProgressChanged; //진행 상황 변화 시 동작
                    Main.MainCamera.StateChanged += MainCamera_StateChanged; //상태 변화 시 동작
                    Main.MainCamera.DownloadReady += MainCamera_DownloadReady;

                    CameraLabel.Content = Main.MainCamera.DeviceName;

                    AvList = Main.MainCamera.GetSettingsList(PropertyID.Av);
                    TvList = Main.MainCamera.GetSettingsList(PropertyID.Tv);
                    ISOList = Main.MainCamera.GetSettingsList(PropertyID.ISO);

                    foreach (var Av in AvList) AVCoBox.Items.Add(Av.StringValue);
                    foreach (var Tv in TvList) TvCoBox.Items.Add(Tv.StringValue);
                    foreach (var ISO in ISOList) ISOCoBox.Items.Add(ISO.StringValue);

                    AVCoBox.SelectedIndex = AVCoBox.Items.IndexOf(AvValues.GetValue(Main.MainCamera.GetInt32Setting(PropertyID.Av)).StringValue);
                    TvCoBox.SelectedIndex = TvCoBox.Items.IndexOf(TvValues.GetValue(Main.MainCamera.GetInt32Setting(PropertyID.Tv)).StringValue);
                    ISOCoBox.SelectedIndex = ISOCoBox.Items.IndexOf(ISOValues.GetValue(Main.MainCamera.GetInt32Setting(PropertyID.ISO)).StringValue);

                    CameraSettingGroupBox.IsEnabled = true;
                    LiveVeiwGroupBox.IsEnabled = true;

                    Main.MainCamera.SetSetting(PropertyID.SaveTo, (int)SaveTo.Host); //Host(PC)에 저장 
                    Main.MainCamera.SetCapacity(4096, int.MaxValue); //저장 용량 지정, Take Picture Card NG Error 해결
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod() + "() - " + ex.Message);
            }
        }

        private void MainCamera_LiveViewUpdated(EOSDigital.API.Camera sender, Stream img)
        {
            //live view 실행 시 카메라에서 받아오는 img를 canvus에 뿌려주기
            try
            {
                if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                {
                    using (WrapStream s = new WrapStream(img))
                    {
                        img.Position = 0;
                        BitmapImage EvfImgae = new BitmapImage();
                        EvfImgae.BeginInit();
                        EvfImgae.StreamSource = s;
                        EvfImgae.CacheOption = BitmapCacheOption.OnLoad;
                        EvfImgae.DecodePixelWidth = 720;
                        EvfImgae.DecodePixelHeight = 1080;
                        EvfImgae.EndInit();
                        EvfImgae.Freeze();

                        TransformedBitmap transbitmap = new TransformedBitmap();
                        transbitmap.BeginInit();
                        transbitmap.Source = EvfImgae;
                        RotateTransform rotate = new RotateTransform(270.0);
                        transbitmap.Transform = rotate;
                        transbitmap.EndInit();
                        transbitmap.Freeze();

                        Application.Current.Dispatcher.BeginInvoke(Main.SetImageAction2, transbitmap);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
                }
                else
                {
                    using (WrapStream s = new WrapStream(img))
                    {
                        img.Position = 0;
                        BitmapImage EvfImgae = new BitmapImage();
                        EvfImgae.BeginInit();
                        EvfImgae.StreamSource = s;
                        EvfImgae.CacheOption = BitmapCacheOption.OnLoad;
                        EvfImgae.EndInit();
                        EvfImgae.Freeze();
                        Application.Current.Dispatcher.BeginInvoke(Main.SetImageAction, EvfImgae);
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod() + "() - " + ex.Message);
            }
        }

        private void MainCamera_DownloadReady(EOSDigital.API.Camera sender, DownloadInfo info)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod() + "() | Canon 찍힌 사진 다운로드");
            try
            {
                string dir = null;
                SavePathTextBox.Dispatcher.Invoke((Action)delegate { dir = SavePathTextBox.Text; }); // 사진 다운로드 경로 지정
                sender.DownloadFile(info, dir); //다운파일에 저장 폴더 값 넘기기
                MainProgressBar.Dispatcher.Invoke((Action)delegate { MainProgressBar.Value = 0; }); // 진행 상태 0으로 초기화
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void ReportError(string message, bool lockdown) // 에러 발생시 처리
        {
            int errc;
            lock (ErrLock) { errc = ++ErrCount; } //lock 다른 스레드로 부터 방해 받지 않음

            if (lockdown) EnableUI(false);

            if (errc < 4) MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (errc == 4) MessageBox.Show("Many errors happened!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            lock (ErrLock) { ErrCount--; }
        }

        private void ErrorHandler_NonSevereErrorHappened(object sender, EOSDigital.SDK.ErrorCode ex)
        {
            Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + $"() - SDK Error code : {ex} ({((int)ex).ToString("X")}");
        }

        private void ErrorHandler_SevereErrorHappened(object sender, Exception ex)
        {
            Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
        }

        private void CloseSession() // 연결 해제
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 세션 닫힘");
                Main.MainCamera.LiveViewUpdated -= MainCamera_LiveViewUpdated;
                Main.MainCamera.ProgressChanged -= MainCamera_ProgressChanged;
                Main.MainCamera.StateChanged -= MainCamera_StateChanged;
                Main.MainCamera.DownloadReady -= MainCamera_DownloadReady;
                Main.MainCamera.CloseSession();
                AVCoBox.Items.Clear();
                TvCoBox.Items.Clear();
                ISOCoBox.Items.Clear();
                CameraSettingGroupBox.IsEnabled = false;
                CameraLabel.Content = "No open session";
                tbStarLVButton.Text = "Start LV";
            }
            catch (NullReferenceException nullex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + nullex.Message);
                MessageBox.Show("Canon 카메라와 연결이 끊겨져있습니다.", "경고", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void MainCamera_ProgressChanged(object sender, int progress)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "()");
            try
            {
                MainProgressBar.Dispatcher.Invoke((Action)delegate { MainProgressBar.Value = progress; }); // 사진 저장 작업 시 진행상태 알림
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #region///카메라 셋팅///
        private void AVCoBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | AV박스 아이템 변경");
            try
            {
                if (AVCoBox.SelectedIndex < 0)
                {
                    return;
                }
                Main.MainCamera.SetSetting(PropertyID.Av, AvValues.GetValue((string)AVCoBox.SelectedItem).IntValue);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void MainCamera_StateChanged(object sender, StateEventID eventID, int parameter)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 카메라 상태 변경");
            try
            {
                if (eventID == StateEventID.Shutdown && Main.IsInit) // 연결 해제 시 Session닫기
                {
                    Dispatcher.Invoke((Action)delegate { CloseSession(); });
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void TvCoBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | TV 값 변경");
            try
            {
                if (TvCoBox.SelectedIndex < 0)
                {
                    return;
                }
                Main.MainCamera.SetSetting(PropertyID.Tv, TvValues.GetValue((string)TvCoBox.SelectedItem).IntValue);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void ISOCoBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | ISO값 변경");
            try
            {
                if (ISOCoBox.SelectedIndex < 0)
                {
                    return;
                }
                Main.MainCamera.SetSetting(PropertyID.ISO, ISOValues.GetValue((string)ISOCoBox.SelectedItem).IntValue);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void StartLVButton_Click(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 라이브뷰 시작");
            try
            {
                if (Canon.IsChecked == true)
                {
                    MainWindow.camnumber.Clear();
                    MainWindow.camnumber.Append("1");

                    if (!Main.MainCamera.IsLiveViewOn)
                    {
                        Main.MainCamera.StartLiveView();
                        Delay(1000);
                        
                        if (MainWindow.checkingflip == "Using")
                        {
                            LVCanvus.Opacity = 1;
                            FlipCanvas.Opacity = 0;
                            LVCanvus.Background = Main.bgbrush;
                        }
                        else
                        {
                            LVCanvus.Opacity = 0;
                            FlipCanvas.Opacity = 1;
                            FlipCanvas.Background = Main.bgbrush;
                        }
                        
                        Source.Log.log.Info("Canon 라이브뷰 시작");

                        tbStarLVButton.Text = "Stop LV";

                        checkcam.IsEnabled = true;
                    }
                    else
                    {
                        Main.MainCamera.StopLiveView();
                        Source.Log.log.Info("Canon 라이브뷰 중지");
                        tbStarLVButton.Text = "Start LV";
                        LVCanvus.Background = System.Windows.Media.Brushes.LightGray;
                        FlipCanvas.Background = System.Windows.Media.Brushes.LightGray;
                        checkcam.IsEnabled = false;
                    }
                }
                else if (WebCam.IsChecked == true)
                {
                    MainWindow.camnumber.Clear();
                    MainWindow.camnumber.Append("2");
                    if (Camonoff == false)
                    {
                        Camonoff = true;
                        thread = new Thread(new ThreadStart(VideoThread));
                        thread.Start();
                        Source.Log.log.Info("웹캠 라이브뷰 시작");
                        tbStarLVButton.Text = "Stop LV";

                        checkcam.IsEnabled = true;
                    }
                    else
                    {
                        thread.Abort();
                        Source.Log.log.Info("웹캠 라이브뷰 중지");
                        tbStarLVButton.Text = "Start LV";
                        Camonoff = false;
                        WebCamLiveView.Source = null;

                        checkcam.IsEnabled = false;
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

        private void InitializationBtn_Click(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 용지 초기화 버튼 클릭");
            try
            {
                string day = DateTime.Today.ToString("dd");
                PrinterSettings printerSettings = new PrinterSettings();
                string printername = printerSettings.PrinterName;
                if (printername.Contains("SINFONIA"))
                {
                    MainWindow.papercount.Clear();
                    MainWindow.papercount.Append("900");
                    if (MainWindow.CheckInternetConnection())
                    {
                        SendPaperApi();
                    }
                    WritePrivateProfileString("Setting", "PaperCount", "900", MainWindow.iniPath);
                    WritePrivateProfileString("RemainPaper", day, "900", MainWindow.bankbookinipath);
                }
                else
                {
                    MainWindow.papercount.Clear();
                    MainWindow.papercount.Append("700");
                    if (MainWindow.CheckInternetConnection())
                    {
                        SendPaperApi();
                    }
                    WritePrivateProfileString("Setting", "PaperCount", "700", MainWindow.iniPath);
                    WritePrivateProfileString("RemainPaper", day, "700", MainWindow.bankbookinipath);
                }
                Initialization.Text = Convert.ToString(MainWindow.papercount);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void BUse_Checked(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 지폐기 사용 클릭");
            try
            {
                
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void BNotUse_Checked(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 지폐기 사용안함 클릭");
            try
            {
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void PhotoTimeSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 포토 타임 변경");
            try
            {
                if (PhotoTimeSetCoBox.SelectedIndex < 0)
                {
                    return;
                }
                switch (PhotoTimeSetCoBox.SelectedIndex)
                {
                    case 0:
                        MainWindow.photocount = "5";
                        break;
                    case 1:
                        MainWindow.photocount = "6";
                        break;
                    case 2:
                        MainWindow.photocount = "7";
                        break;
                    case 3:
                        MainWindow.photocount = "8";
                        break;
                    case 4:
                        MainWindow.photocount = "9";
                        break;
                    case 5:
                        MainWindow.photocount = "10";
                        break;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void PageTimeSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() + | 페이지 타이머 변경");
            try
            {
                if (PageTimeSetCoBox.SelectedIndex < 0)
                {
                    return;
                }
                switch (PageTimeSetCoBox.SelectedIndex)
                {
                    case 0:
                        MainWindow.count = "60";
                        break;
                    case 1:
                        MainWindow.count = "120";
                        break;
                    case 2:
                        MainWindow.count = "150";
                        break;
                    case 3:
                        MainWindow.count = "180";
                        break;
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

        private void MoneySettingCoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 템플릿 1번 돈 셋팅 변경");
            try
            {
                if (MoneySettingCoBox.SelectedIndex < 0)
                {
                    return;
                }
                switch (MoneySettingCoBox.SelectedIndex)
                {
                    case 0:
                        MainWindow.moneyset = "0";
                        break;
                    case 1:
                        MainWindow.moneyset = "1000";
                        break;
                    case 2:
                        MainWindow.moneyset = "2000";
                        break;
                    case 3:
                        MainWindow.moneyset = "3000";
                        break;
                    case 4:
                        MainWindow.moneyset = "4000";
                        break;
                    case 5:
                        MainWindow.moneyset = "5000";
                        break;
                    case 6:
                        MainWindow.moneyset = "6000";
                        break;
                    case 7:
                        MainWindow.moneyset = "7000";
                        break;
                    case 8:
                        MainWindow.moneyset = "8000";
                        break;
                    case 9:
                        MainWindow.moneyset = "9000";
                        break;
                    case 10:
                        MainWindow.moneyset = "10000";
                        break;
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

        private void OffSetXCoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | offsetx 변경");
            try
            {
                MainWindow.offsetx = "0";
                if (OffSetXCoBox.SelectedIndex < 0)
                {
                    return;
                }
                switch (OffSetXCoBox.SelectedIndex)
                {
                    case 0:
                        MainWindow.offsetx = "-8";
                        break;
                    case 1:
                        MainWindow.offsetx = "-7";
                        break;
                    case 2:
                        MainWindow.offsetx = "-6";
                        break;
                    case 3:
                        MainWindow.offsetx = "-5";
                        break;
                    case 4:
                        MainWindow.offsetx = "-4";
                        break;
                    case 5:
                        MainWindow.offsetx = "-3";
                        break;
                    case 6:
                        MainWindow.offsetx = "-2";
                        break;
                    case 7:
                        MainWindow.offsetx = "-1";
                        break;
                    case 8:
                        MainWindow.offsetx = "0";
                        break;
                    case 9:
                        MainWindow.offsetx = "1";
                        break;
                    case 10:
                        MainWindow.offsetx = "2";
                        break;
                    case 11:
                        MainWindow.offsetx = "3";
                        break;
                    case 12:
                        MainWindow.offsetx = "4";
                        break;
                    case 13:
                        MainWindow.offsetx = "5";
                        break;
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

        private void OffSetYCoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | offsety 변경");
            try
            {
                if (OffSetYCoBox.SelectedIndex < 0)
                {
                    return;
                }
                switch (OffSetYCoBox.SelectedIndex)
                {
                    case 0:
                        MainWindow.offsety = "-7";
                        break;
                    case 1:
                        MainWindow.offsety = "-6";
                        break;
                    case 2:
                        MainWindow.offsety = "-5";
                        break;
                    case 3:
                        MainWindow.offsety = "-4";
                        break;
                    case 4:
                        MainWindow.offsety = "-3";
                        break;
                    case 5:
                        MainWindow.offsety = "-2";
                        break;
                    case 6:
                        MainWindow.offsety = "-1";
                        break;
                    case 7:
                        MainWindow.offsety = "0";
                        break;
                    case 8:
                        MainWindow.offsety = "1";
                        break;
                    case 9:
                        MainWindow.offsety = "2";
                        break;
                    case 10:
                        MainWindow.offsety = "3";
                        break;
                    case 11:
                        MainWindow.offsety = "4";
                        break;
                    case 12:
                        MainWindow.offsety = "5";
                        break;
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

        private void FactorXCoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | factorX 변경");
            try
            {
                if (FactorXCoBox.SelectedIndex < 0)
                {
                    return;
                }
                switch (FactorXCoBox.SelectedIndex)
                {
                    case 0:
                        MainWindow.factorx = "-5";
                        break;
                    case 1:
                        MainWindow.factorx = "-4";
                        break;
                    case 2:
                        MainWindow.factorx = "-3";
                        break;
                    case 3:
                        MainWindow.factorx = "-2";
                        break;
                    case 4:
                        MainWindow.factorx = "-1";
                        break;
                    case 5:
                        MainWindow.factorx = "0";
                        break;
                    case 6:
                        MainWindow.factorx = "1";
                        break;
                    case 7:
                        MainWindow.factorx = "2";
                        break;
                    case 8:
                        MainWindow.factorx = "3";
                        break;
                    case 9:
                        MainWindow.factorx = "4";
                        break;
                    case 10:
                        MainWindow.factorx = "5";
                        break;
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

        private void FactorYCoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | FactorY 변경");
            try
            {
                if (FactorYCoBox.SelectedIndex < 0)
                {
                    return;
                }
                switch (FactorYCoBox.SelectedIndex)
                {
                    case 0:
                        MainWindow.factory = "-5";
                        break;
                    case 1:
                        MainWindow.factory = "-4";
                        break;
                    case 2:
                        MainWindow.factory = "-3";
                        break;
                    case 3:
                        MainWindow.factory = "-2";
                        break;
                    case 4:
                        MainWindow.factory = "-1";
                        break;
                    case 5:
                        MainWindow.factory = "0";
                        break;
                    case 6:
                        MainWindow.factory = "1";
                        break;
                    case 7:
                        MainWindow.factory = "2";
                        break;
                    case 8:
                        MainWindow.factory = "3";
                        break;
                    case 9:
                        MainWindow.factory = "4";
                        break;
                    case 10:
                        MainWindow.factory = "5";
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #region///프린터///

        private void LoadSetting()
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 로드된 값 세팅");
            try
            {

                if (MainWindow.cardnum == "AT0403177A")
                {
                    cardserialtextbox.Text = "기본값";
                }
                else if (MainWindow.cardnum == "")
                {
                    cardserialtextbox.Text = "기본값";
                }
                else
                {
                    cardserialtextbox.Text = MainWindow.cardnum;
                }

                switch (MainWindow.paymentway.ToString())
                {
                    case "free":
                        PayWayCB.SelectedItem = PayWayCB.Items[0];
                        break;
                    case "cash":
                        PayWayCB.SelectedItem = PayWayCB.Items[1];
                        break;
                    case "card":
                        PayWayCB.SelectedItem = PayWayCB.Items[2];
                        break;
                    case "cardcash":
                        PayWayCB.SelectedItem = PayWayCB.Items[3];
                        break;
                    case "cashcoupon":
                        PayWayCB.SelectedItem = PayWayCB.Items[4];
                        break;
                    case "cardcoupon":
                        PayWayCB.SelectedItem = PayWayCB.Items[5];
                        break;
                    case "coupon":
                        PayWayCB.SelectedItem = PayWayCB.Items[6];
                        break;
                }

                switch (MainWindow.couponkind)
                {
                    case "no":
                        couponkind.SelectedItem = couponkind.Items[0];
                        break;
                    case "num":
                        couponkind.SelectedItem = couponkind.Items[1];
                        break;
                    case "numchar":
                        couponkind.SelectedItem = couponkind.Items[2];
                        break;
                }

                // 재촬영 확인
                if (MainWindow.checkretake == "0")
                {
                    Retakebtn.IsChecked = true;
                }
                else
                {
                    NoTakebtn.IsChecked = true;
                }

                //카메라 셋팅

                if (MainWindow.camnumber.ToString() == "1")
                {
                    Canon.IsChecked = true;
                }
                else if (MainWindow.camnumber.ToString() == "2")
                {
                    WebCam.IsChecked = true;
                }

                //타이머
                if (MainWindow.checktimeruse == "Use")
                {
                    timeruse.IsChecked = true;
                    if (MainWindow.count == MainWindow.pagecount.ToString())
                    {
                        if (MainWindow.count == "")
                        {
                            PageTimeSetCoBox.SelectedItem = PageTimeSetCoBox.Items[0];
                        }
                        else
                        {
                            switch (Convert.ToInt32(MainWindow.count))
                            {
                                case 60:
                                    PageTimeSetCoBox.SelectedItem = PageTimeSetCoBox.Items[0];
                                    break;
                                case 120:
                                    PageTimeSetCoBox.SelectedItem = PageTimeSetCoBox.Items[1];
                                    break;
                                case 150:
                                    PageTimeSetCoBox.SelectedItem = PageTimeSetCoBox.Items[2];
                                    break;
                                case 180:
                                    PageTimeSetCoBox.SelectedItem = PageTimeSetCoBox.Items[3];
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (Convert.ToInt32(MainWindow.count))
                        {
                            case 120:
                                PageTimeSetCoBox.SelectedItem = PageTimeSetCoBox.Items[0];
                                break;
                            case 150:
                                PageTimeSetCoBox.SelectedItem = PageTimeSetCoBox.Items[1];
                                break;
                            case 180:
                                PageTimeSetCoBox.SelectedItem = PageTimeSetCoBox.Items[2];
                                break;
                        }
                    }
                }
                else
                {
                    timernotuse.IsChecked = true;
                    PageTimeSetCoBox.IsEnabled = false;
                }


                //포토 타이머 셋팅

                if (MainWindow.photocount == "")
                {
                    PhotoTimeSetCoBox.SelectedItem = PhotoTimeSetCoBox.Items[5];
                }
                else
                {
                    switch (MainWindow.photocount)
                    {
                        case "5":
                            PhotoTimeSetCoBox.SelectedItem = PhotoTimeSetCoBox.Items[0];
                            break;
                        case "6":
                            PhotoTimeSetCoBox.SelectedItem = PhotoTimeSetCoBox.Items[1];
                            break;
                        case "7":
                            PhotoTimeSetCoBox.SelectedItem = PhotoTimeSetCoBox.Items[2];
                            break;
                        case "8":
                            PhotoTimeSetCoBox.SelectedItem = PhotoTimeSetCoBox.Items[3];
                            break;
                        case "9":
                            PhotoTimeSetCoBox.SelectedItem = PhotoTimeSetCoBox.Items[4];
                            break;
                        case "10":
                            PhotoTimeSetCoBox.SelectedItem = PhotoTimeSetCoBox.Items[5];
                            break;
                    }
                }

                //OffSEt

                if (MainWindow.offsetx == "" && MainWindow.offsety == "" && MainWindow.factorx == "" && MainWindow.factory == "")
                {
                    OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[5];
                    OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[5];
                    FactorXCoBox.SelectedItem = FactorXCoBox.Items[5];
                    FactorYCoBox.SelectedItem = FactorYCoBox.Items[5];
                }
                else
                {
                    switch (Convert.ToInt32(MainWindow.offsetx))
                    {
                        case -8:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[0];
                            break;
                        case -7:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[1];
                            break;
                        case -6:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[2];
                            break;
                        case -5:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[3];
                            break;
                        case -4:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[4];
                            break;
                        case -3:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[5];
                            break;
                        case -2:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[6];
                            break;
                        case -1:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[7];
                            break;
                        case 0:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[8];
                            break;
                        case 1:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[9];
                            break;
                        case 2:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[10];
                            break;
                        case 3:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[11];
                            break;
                        case 4:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[12];
                            break;
                        case 5:
                            OffSetXCoBox.SelectedItem = OffSetXCoBox.Items[13];
                            break;
                    }
                    switch (Convert.ToInt32(MainWindow.offsety))
                    {
                        case -7:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[0];
                            break;
                        case -6:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[1];
                            break;
                        case -5:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[2];
                            break;
                        case -4:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[3];
                            break;
                        case -3:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[4];
                            break;
                        case -2:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[5];
                            break;
                        case -1:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[6];
                            break;
                        case 0:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[7];
                            break;
                        case 1:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[8];
                            break;
                        case 2:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[9];
                            break;
                        case 3:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[10];
                            break;
                        case 4:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[11];
                            break;
                        case 5:
                            OffSetYCoBox.SelectedItem = OffSetYCoBox.Items[12];
                            break;
                    }
                    switch (Convert.ToInt32(MainWindow.factorx))
                    {
                        case -5:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[0];
                            break;
                        case -4:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[1];
                            break;
                        case -3:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[2];
                            break;
                        case -2:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[3];
                            break;
                        case -1:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[4];
                            break;
                        case 0:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[5];
                            break;
                        case 1:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[6];
                            break;
                        case 2:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[7];
                            break;
                        case 3:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[8];
                            break;
                        case 4:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[9];
                            break;
                        case 5:
                            FactorXCoBox.SelectedItem = FactorXCoBox.Items[10];
                            break;
                    }
                    switch (Convert.ToInt32(MainWindow.factory))
                    {
                        case -5:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[0];
                            break;
                        case -4:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[1];
                            break;
                        case -3:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[2];
                            break;
                        case -2:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[3];
                            break;
                        case -1:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[4];
                            break;
                        case 0:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[5];
                            break;
                        case 1:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[6];
                            break;
                        case 2:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[7];
                            break;
                        case 3:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[8];
                            break;
                        case 4:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[9];
                            break;
                        case 5:
                            FactorYCoBox.SelectedItem = FactorYCoBox.Items[10];
                            break;
                    }
                }

                if ((MainWindow.checkkakao == "0") || (MainWindow.checkkakao != "0" && MainWindow.qrpageuse == 1))
                {
                    switch (MainWindow.kakaotimer)
                    {
                        case 45:
                            KakaoPageTimeSetCoBox.SelectedItem = KakaoPageTimeSetCoBox.Items[0];
                            break;
                        case 60:
                            KakaoPageTimeSetCoBox.SelectedItem = KakaoPageTimeSetCoBox.Items[1];
                            break;
                        case 75:
                            KakaoPageTimeSetCoBox.SelectedItem = KakaoPageTimeSetCoBox.Items[2];
                            break;
                        case 90:
                            KakaoPageTimeSetCoBox.SelectedItem = KakaoPageTimeSetCoBox.Items[3];
                            break;
                    }
                }
                else
                {
                    KakaoPageTimeSetCoBox.IsEnabled = false;
                }

                StoreName.Text = MainWindow.foldername.ToString();
                MachineNum.Text = MainWindow.ID.ToString();

                if (MainWindow.timerlocation == "right")
                {
                    timerlocationright.IsChecked = true;
                }
                else
                {
                    timerlocationleft.IsChecked = true;
                }

                // 좌우반전

                if (MainWindow.checkingflip == "Using")
                {
                    FlipUse.IsChecked = true;
                }
                else
                {
                    FlipNotUse.IsChecked = true;
                }

                if (MainWindow.Adminlogin == 1)
                {
                    SettingGroupBox.IsEnabled = true;
                    InitGroupBox.IsEnabled = true;
                    StoreName.IsEnabled = true;
                    MachineNum.IsEnabled = true;
                    checkpayment.IsEnabled = true;
                    NextPageBtn.IsEnabled = true;
                }

                if (MainWindow.autologin == "0")
                {
                    autonotuse.IsChecked = true;
                }
                else
                {
                    autouse.IsChecked = true;
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

        private void SetAutoPrinter()
        {
            PrinterSettings printerSettings = new PrinterSettings();
            correctedPrintername = printerSettings.PrinterName;
        }

        private void PrintTestBtn_Click(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 프린트 버튼 클릭");
            try
            {
                string day = DateTime.Today.ToString("dd");
                int offSetX = Convert.ToInt32(MainWindow.offsetx);
                int offSetY = Convert.ToInt32(MainWindow.offsety);
                int factorX = Convert.ToInt32(MainWindow.factorx);
                int factorY = Convert.ToInt32(MainWindow.factory);
                if (MainWindow.inifoldername == "hhh")
                {
                    Bitmap canvasa4;
                    canvasa4 = new Bitmap(2480, 3508);
                    Graphics ga4 = Graphics.FromImage(canvasa4);

                    System.Drawing.Image img4_2 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "4-2.png");
                    ga4.DrawImage(img4_2, 0, 0, 2480, 3508);

                    canvasa4.RotateFlip(RotateFlipType.Rotate90FlipNone);

                    PrintBitmap(canvasa4, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                    papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                    MainWindow.papercount.Clear();
                    MainWindow.papercount.Append(papercountdown);
                    WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                    WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                    Initialization.Text = papercountdown;
                    ga4.Dispose();
                    img4_2.Dispose();
                    canvasa4.Dispose();
                }
                else
                {
                    switch (MainWindow.checktempselect.ToString())
                    {
                        case "2-1":
                            Bitmap canvas2_1;
                            canvas2_1 = new Bitmap(1200, 1800);

                            Graphics g2_1 = Graphics.FromImage(canvas2_1);
                            System.Drawing.Image img2_1 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "2-1.png");
                            g2_1.DrawImage(img2_1, 0, 0, 1200, 1800);
                            PrintBitmap(canvas2_1, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g2_1.Dispose();
                            img2_1.Dispose();
                            canvas2_1.Dispose();
                            break;
                        case "2-2":
                            Bitmap canvas2_2;
                            canvas2_2 = new Bitmap(1800, 1200);

                            Graphics g2_2 = Graphics.FromImage(canvas2_2);
                            System.Drawing.Image img2_2 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "2-2.png");
                            g2_2.DrawImage(img2_2, 0, 0, 1800, 1200);
                            PrintBitmap(canvas2_2, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g2_2.Dispose();
                            img2_2.Dispose();
                            canvas2_2.Dispose();
                            break;
                        case "2-3":
                            Bitmap canvas2_3;
                            canvas2_3 = new Bitmap(1800, 1200);

                            Graphics g2_3 = Graphics.FromImage(canvas2_3);
                            System.Drawing.Image img2_3 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "2-3.png");
                            g2_3.DrawImage(img2_3, 0, 0, 1800, 1200);
                            PrintBitmap(canvas2_3, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g2_3.Dispose();
                            img2_3.Dispose();
                            canvas2_3.Dispose();
                            break;
                        case "3-1":
                            Bitmap canvas3_1;
                            canvas3_1 = new Bitmap(1200, 1800);

                            Graphics g3_1 = Graphics.FromImage(canvas3_1);
                            System.Drawing.Image img3_1 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "3-1.png");
                            g3_1.DrawImage(img3_1, 0, 0, 1200, 1800);
                            PrintBitmap(canvas3_1, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g3_1.Dispose();
                            img3_1.Dispose();
                            canvas3_1.Dispose();
                            break;
                        case "3-2":
                            Bitmap canvas3_2;
                            canvas3_2 = new Bitmap(1200, 1800);

                            Graphics g3_2 = Graphics.FromImage(canvas3_2);
                            System.Drawing.Image img3_2 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "3-2.png");
                            g3_2.DrawImage(img3_2, 0, 0, 1200, 1800);
                            PrintBitmap(canvas3_2, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g3_2.Dispose();
                            img3_2.Dispose();
                            canvas3_2.Dispose();
                            break;
                        case "3-3":
                            Bitmap canvas3_3;
                            canvas3_3 = new Bitmap(1800, 1200);

                            Graphics g3_3 = Graphics.FromImage(canvas3_3);
                            System.Drawing.Image img3_3 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "3-3.png");
                            g3_3.DrawImage(img3_3, 0, 0, 1800, 1200);
                            PrintBitmap(canvas3_3, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g3_3.Dispose();
                            img3_3.Dispose();
                            canvas3_3.Dispose();
                            break;
                        case "3-4":
                            Bitmap canvas3_4;
                            canvas3_4 = new Bitmap(1800, 1200);

                            Graphics g3_4 = Graphics.FromImage(canvas3_4);
                            System.Drawing.Image img3_4 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "3-4.png");
                            g3_4.DrawImage(img3_4, 0, 0, 1800, 1200);
                            PrintBitmap(canvas3_4, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g3_4.Dispose();
                            img3_4.Dispose();
                            canvas3_4.Dispose();
                            break;
                        case "4-1":
                            Bitmap canvas4_1;
                            canvas4_1 = new Bitmap(1800, 1200);

                            Graphics g4_1 = Graphics.FromImage(canvas4_1);
                            System.Drawing.Image img4_1 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "4-1.png");
                            g4_1.DrawImage(img4_1, 0, 0, 1800, 1200);
                            PrintBitmap(canvas4_1, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g4_1.Dispose();
                            img4_1.Dispose();
                            canvas4_1.Dispose();
                            break;
                        case "4-2":
                            Bitmap canvas4_2;
                            canvas4_2 = new Bitmap(1200, 1800);
                            Graphics g4_2 = Graphics.FromImage(canvas4_2);

                            System.Drawing.Image img4_2 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "4-2.png");
                            g4_2.DrawImage(img4_2, 0, 0, 600, 1800);
                            g4_2.DrawImage(img4_2, 600, 0, 600, 1800);

                            PrintBitmap(canvas4_2, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g4_2.Dispose();
                            img4_2.Dispose();
                            canvas4_2.Dispose();
                            break;
                        case "4-3":
                            Bitmap canvas4_3;
                            canvas4_3 = new Bitmap(1200, 1800);

                            Graphics g4_3 = Graphics.FromImage(canvas4_3);
                            System.Drawing.Image img4_3 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "4-3.png");
                            g4_3.DrawImage(img4_3, 0, 0, 1200, 1800);
                            PrintBitmap(canvas4_3, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g4_3.Dispose();
                            img4_3.Dispose();
                            canvas4_3.Dispose();
                            break;
                        case "4-4":
                            Bitmap canvas4_4;
                            canvas4_4 = new Bitmap(1200, 1800);

                            Graphics g4_4 = Graphics.FromImage(canvas4_4);
                            System.Drawing.Image img4_4 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "4-4.png");
                            g4_4.DrawImage(img4_4, 0, 0, 1200, 1800);
                            PrintBitmap(canvas4_4, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g4_4.Dispose();
                            img4_4.Dispose();
                            canvas4_4.Dispose();
                            break;
                        case "4-5":
                            Bitmap canvas4_5;
                            canvas4_5 = new Bitmap(1200, 1800);

                            Graphics g4_5 = Graphics.FromImage(canvas4_5);
                            System.Drawing.Image img4_5 = Bitmap.FromFile(MainWindow.testprintpath + "\\" + "4-5.png");
                            g4_5.DrawImage(img4_5, 0, 0, 1200, 1800);
                            PrintBitmap(canvas4_5, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                            papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                            MainWindow.papercount.Clear();
                            MainWindow.papercount.Append(papercountdown);
                            WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                            WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                            Initialization.Text = papercountdown;
                            g4_5.Dispose();
                            img4_5.Dispose();
                            canvas4_5.Dispose();
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
                    Margins margins = new Margins(8 + OffSetX, 0, 7 + OffSetY, 0);
                    if (isLandScape)
                    {
                        margins = new Margins(7 + OffSetY, 0, 2 + OffSetX, 0);
                    }
                    if (MainWindow.inifoldername == "hhh")
                    {
                        margins = new Margins(0,0,0,0);
                    }
                    printDocument.DefaultPageSettings.Margins = margins;
                    printDocument.OriginAtMargins = true;
                    printDocument.DefaultPageSettings.PrinterSettings.Copies = (short)Copies;
                    PrinterResolution printerResolution = new PrinterResolution();
                    printerResolution.Kind = PrinterResolutionKind.Custom;
                    printerResolution.X = 600;
                    printerResolution.Y = 600;
                    printDocument.DefaultPageSettings.PrinterResolution = printerResolution;
                    if (MainWindow.inifoldername == "hhh")
                    {
                        printDocument.PrintPage += delegate (object sender, PrintPageEventArgs args)
                        {
                            System.Drawing.Image image = new Bitmap(bitmap);
                            args.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            if (!isLandScape)
                            {
                                args.Graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, 827 + FactorX, 1169 + FactorY), new System.Drawing.Rectangle(0, 0, 2480, 3508), GraphicsUnit.Pixel);
                            }
                            else
                            {
                                args.Graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, 1169 + FactorY, 827 + FactorX), new System.Drawing.Rectangle(0, 0, 3508, 2480), GraphicsUnit.Pixel);
                            }
                            image.Dispose();
                        };
                    }
                    else
                    {
                        printDocument.PrintPage += delegate (object sender, PrintPageEventArgs args)
                        {
                            System.Drawing.Image image = new Bitmap(bitmap);
                            args.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            if (!isLandScape)
                            {
                                args.Graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, 400 + FactorX, 600 + FactorY), new System.Drawing.Rectangle(0, 0, 1200, 1800), GraphicsUnit.Pixel);
                            }
                            else
                            {
                                args.Graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, 603 + FactorY, 401 + FactorX), new System.Drawing.Rectangle(0, 0, 1800, 1200), GraphicsUnit.Pixel);
                            }
                            image.Dispose();
                        };
                    }
                    printDocument.Print();
                    bitmap.Dispose();
                    printDocument.Dispose();
                }
                else if (isMessage)
                {
                    MessageBox.Show("출력할 프린터가 없습니다.");
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

        private void NextPageBtn_Click(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 다음페이지 클릭");
            try
            {
                if (MainWindow.paymentini.ToString() == "cardcash" || MainWindow.paymentini.ToString() == "cash")
                {
                    DataSend('S', 'A', '\u000e');
                }
                if (MainWindow.camnumber.ToString() == "1")
                {
                    if (Main.MainCamera?.SessionOpen == true)
                    {
                        if (Main.MainCamera?.IsLiveViewOn == true)
                        {
                            Main.MainCamera.StopLiveView();
                            Source.Log.log.Info("Canon 카메라 라이브뷰 종료");
                        }
                        cameradispose();
                        Delay(500);
                        CloseSession();
                    }
                }
                else
                {
                    if (Camonoff)
                    {
                        thread.Abort();
                        Source.Log.log.Info("웹캠 라이브뷰 종료");
                        if (zoomcount != 0)
                        {
                            resizemat.Dispose();
                        }
                        frame.Dispose();
                        flipframe.Dispose();
                        Camonoff = false;
                        WebCamLiveView.Source = null;
                    }
                }
                LVCanvus.Background = System.Windows.Media.Brushes.LightGray;
                NavigationService.Navigate(new Uri("View/SerialCheck2.xaml", UriKind.RelativeOrAbsolute));
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Canon_Checked(object sender, RoutedEventArgs e) // 카메라 Canon으로 설정
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 캐논 카메라 체크");
            try
            {
                if (MainWindow.camnumber.ToString() == "1")
                { }
                else
                {
                    if (MainWindow.video != null)
                    {
                        MainWindow.video.Dispose();
                        if (MainWindow.Webcam != null)
                        {
                            MainWindow.Webcam.Dispose();
                        }
                    }
                    MainWindow.camnumber.Clear();
                    MainWindow.camnumber.Append("1");
                    compose1.Opacity = 0;
                    compose2.Opacity = 0;

                    if (Main.MainCamera == null)
                    {
                        Main.APIHandler = new CanonAPI();
                        Main.APIHandler.CameraAdded += APIHandler_CameraAdded;
                        ErrorHandler.SevereErrorHappened += ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
                        ErrorHandler.NonSevereErrorHappened += ErrorHandler_NonSevereErrorHappened; //에러 발생 시

                        SavePathTextBox.Text = MainWindow.PhotoPath; // 초기 저장폴더 지정

                        if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                        {
                            Main.SetImageAction2 = (TransformedBitmap img) => { Main.bgbrush.ImageSource = img; }; // Live View에 그려지는 이미지 Action 
                        }
                        else
                        {
                            Main.SetImageAction = (BitmapImage img) => { Main.bgbrush.ImageSource = img; }; // Live View에 그려지는 이미지 Action 
                        }
                        SaveFolderBrowser.Description = "Save Image To...";
                        RefreshCamera(); //연결된 카메라 정보 수집
                        Main.IsInit = true;
                        if (Main.MainCamera == null)
                        {
                            OpenSession();
                            if (Main.MainCamera != null)
                            {
                                CameraLabel.Content = Main.MainCamera.DeviceName;
                            }
                        }
                    }
                }

                if (LVCanvus != null)
                {
                    LVCanvus.IsEnabled = true;
                    LVCanvus.Visibility = Visibility.Visible;
                }
                if (WebCamLiveView != null)
                {
                    WebCamLiveView.Source = null;
                    WebCamLiveView.IsEnabled = false;
                    WebCamLiveView.Visibility = Visibility.Hidden;
                }
                if (Camonoff)
                {
                    Camonoff = false;
                    thread.Abort();
                    Source.Log.log.Info("웹캠 카메라 종료");
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void WebCam_Checked(object sender, RoutedEventArgs e) // 카메라 웹캠으로 설정
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 웹캠 체크");
            try
            {
                if (MainWindow.camnumber.ToString() == "2")
                { }
                else
                {
                    MainWindow.camnumber.Clear();
                    MainWindow.camnumber.Append("2");

                    if (Main.MainCamera?.SessionOpen == true)
                    {
                        if (Main.MainCamera?.IsLiveViewOn == true)
                        {
                            Main.MainCamera.StopLiveView();
                            Source.Log.log.Info("Canon 카메라 종료");
                        }
                        cameradispose();
                        Delay(500);
                        CloseSession();
                        Delay(500);
                        cameranull();
                    }

                    try
                    {
                        MainWindow.video = new VideoCapture(0);
                        MainWindow.Webcam = CameraService.AvailableCameras[0];
                    }
                    catch (Exception webex)
                    {
                        MessageBox.Show("카메라 연결을 확인해주세요.");
                        Source.Log.log.Error(webex);
                        Source.Log.log.Error("카메라 연결을 확인해주세요.");
                    }
                    WebCamInitialize(MainWindow.CaptureWidth, MainWindow.CaptureHeight, 12, 0);
                }
                WebCamGB.IsEnabled = true;
                LVCanvus.Background = null;
                LVCanvus.IsEnabled = false;
                LVCanvus.Visibility = Visibility.Hidden;
                WebCamLiveView.IsEnabled = true;
                WebCamLiveView.Visibility = Visibility.Visible;
                compose1.Opacity = 1;
                compose2.Opacity = 2;

                if (MainWindow.Webcam != null)
                {
                    CameraLabel.Content = MainWindow.Webcam.Name;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }


        #region ///웹켐 구동///

        private void VideoThread()
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 웹캠 구동");
            try
            {
                MainWindow.video.FrameWidth = 1920;
                MainWindow.video.FrameHeight = 1080;

                while (videothread)
                {
                    while (MainWindow.video.PosFrames != 60)
                    {
                        frame = new Mat();
                        flipframe = new Mat();
                        if (testphototime)
                        {

                        }
                        else
                        {
                            if (MainWindow.checkingflip == "Using")
                            {
                                if (MainWindow.video.Read(frame))
                                {
                                    bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                                    {
                                        using (MemoryStream ms = new MemoryStream())
                                        {
                                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                            var bi = new BitmapImage();
                                            bi.BeginInit();
                                            bi.StreamSource = ms;
                                            bi.CacheOption = BitmapCacheOption.OnLoad;
                                            bi.DecodePixelWidth = 1920;
                                            bi.DecodePixelHeight = 1080;
                                            bi.EndInit();
                                            WebCamLiveView.Source = bi;
                                            ms.Dispose();
                                        }
                                    }));
                                }
                            }
                            else
                            {
                                if (MainWindow.video.Read(frame))
                                {
                                    MainWindow.video.Read(frame);
                                    Cv2.Flip(frame, flipframe, FlipMode.Y);
                                    bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(flipframe);
                                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                                    {
                                        using (MemoryStream ms = new MemoryStream())
                                        {
                                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                            var bi = new BitmapImage();
                                            bi.BeginInit();
                                            bi.StreamSource = ms;
                                            bi.CacheOption = BitmapCacheOption.OnLoad;
                                            bi.DecodePixelWidth = 1920;
                                            bi.DecodePixelHeight = 1080;
                                            bi.EndInit();
                                            WebCamLiveView.Source = bi;
                                            ms.Dispose();
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
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
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
                MainWindow.Webcam = CameraService.AvailableCameras[index];
                setFrameSource(new CameraFrameSource(MainWindow.Webcam));
                MainWindow.WebCamFrameSource.Camera.CaptureWidth = 1920;
                MainWindow.WebCamFrameSource.Camera.CaptureHeight = 1080;
                MainWindow.WebCamFrameSource.Camera.Fps = frameRate;
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
            if (MainWindow.WebCamFrameSource != cameraFrameSource)
            {
                MainWindow.WebCamFrameSource = cameraFrameSource;
            }
        }

        #endregion

        #region ///웹캠 라이브뷰 이동///

        private void SalesStatementBT_Click(object sender, RoutedEventArgs e)
        {
            bankbook bankbook = new bankbook();
            bankbook.Left = 0;
            bankbook.Top = 0;
            bankbook.ShowDialog();
        }

        private void ToMain_Click(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 메인 버튼 클릭");
            try
            {
                if (MainWindow.paymentini.ToString() == "cardcash" || MainWindow.paymentini.ToString() == "cash")
                {
                    DataSend('S', 'A', '\u000e');
                }
                if (MainWindow.camnumber.ToString() == "1")
                {
                    try
                    {
                        if (Main.MainCamera?.SessionOpen == true)
                        {
                            if (Main.MainCamera?.IsLiveViewOn == true)
                            {
                                Main.MainCamera.StopLiveView();
                                Source.Log.log.Info("Canon 카메라 종료");
                            }
                            cameradispose();
                            Delay(500);
                            CloseSession();
                            Delay(500);
                            cameranull();
                        }
                    }
                    catch (Exception camex)
                    {
                        Source.Log.log.Error(camex);
                    }
                }
                else
                {
                    if (Camonoff)
                    {
                        thread.Abort();
                        Source.Log.log.Info("웹캠 라이브뷰 중지");
                        tbStarLVButton.Text = "Start LV";
                        Camonoff = false;
                        WebCamLiveView.Source = null;

                        checkcam.IsEnabled = false;
                    }
                }
                // Serialcheck 1 변수 초기화
                if (MainWindow.checkretake != MainWindow.retake.ToString() || MainWindow.checkkakao != MainWindow.checksns.ToString() || MainWindow.checkfreeornot != MainWindow.checkfree.ToString() || MainWindow.checktimeruse != MainWindow.Timer.ToString() ||
                    MainWindow.count != MainWindow.pagecount.ToString() || MainWindow.photocount != MainWindow.phototime.ToString() || MainWindow.moneyset != MainWindow.inisetmoney.ToString() || MainWindow.offsetx != MainWindow.getOffSetx.ToString() ||
                    MainWindow.offsety != MainWindow.getOffSety.ToString() || MainWindow.factorx != MainWindow.getFactorx.ToString() || MainWindow.factory != MainWindow.getFactory.ToString() || MainWindow.checktempselect != MainWindow.tempselect.ToString() ||
                    MainWindow.comportnum != MainWindow.ComNum.ToString() || MainWindow.leftrightcount != Convert.ToInt32(MainWindow.Xcount.ToString()) || MainWindow.updowncount != Convert.ToInt32(MainWindow.Ycount.ToString()) || MainWindow.zoomratio != Convert.ToDouble(MainWindow.zoomratio.ToString()) ||
                    MainWindow.brightvalue.ToString() != MainWindow.bright.ToString() || MainWindow.crvalue.ToString() != MainWindow.ContrastRatio.ToString() || MainWindow.checkingflip != MainWindow.checkflip.ToString() ||
                    MainWindow.ID.ToString() != MainWindow.inimachinecode.ToString() || MainWindow.inifoldername != MainWindow.foldername.ToString() || MainWindow.paymentway != MainWindow.paymentini.ToString() || MainWindow.pagenum != MainWindow.pagenumini.ToString() ||
                    MainWindow.useqr.ToString() != MainWindow.qroption.ToString() || MainWindow.useselecttemp.ToString() != MainWindow.tempoption.ToString() || MainWindow.tempnumber.ToString() != MainWindow.optiontempnum.ToString() || MainWindow.picratio.ToString() != MainWindow.inipicratio.ToString() ||
                    MainWindow.printerratio.ToString() != MainWindow.iniprinterratio.ToString() || MainWindow.inipic1startx.ToString() != MainWindow.pic1startx.ToString() || MainWindow.inipic1starty.ToString() != MainWindow.pic1starty.ToString() ||
                    MainWindow.inipic1width.ToString() != MainWindow.pic1width.ToString() || MainWindow.pic1height.ToString() != MainWindow.inipic1height.ToString() || MainWindow.inipic2startx.ToString() != MainWindow.pic2startx.ToString() ||
                    MainWindow.inipic2starty.ToString() != MainWindow.pic2starty.ToString() || MainWindow.inipic2width.ToString() != MainWindow.pic2width.ToString() || MainWindow.pic2height.ToString() != MainWindow.inipic2height.ToString() ||
                    MainWindow.inipic3startx.ToString() != MainWindow.pic3startx.ToString() || MainWindow.inipic3starty.ToString() != MainWindow.pic3starty.ToString() || MainWindow.inipic3width.ToString() != MainWindow.pic3width.ToString() || MainWindow.inipic3height.ToString() != MainWindow.pic3height.ToString() ||
                    MainWindow.inipic4startx.ToString() != MainWindow.pic4startx.ToString() || MainWindow.inipic4starty.ToString() != MainWindow.pic4starty.ToString() || MainWindow.inipic4width.ToString() != MainWindow.pic4width.ToString() || MainWindow.inipic4height.ToString() != MainWindow.pic4height.ToString() ||
                    MainWindow.iniqrstartx.ToString() != MainWindow.qrstartx.ToString() || MainWindow.iniqrstarty.ToString() != MainWindow.qrstarty.ToString() || MainWindow.iniqrwidth.ToString() != MainWindow.qrwidth.ToString() || MainWindow.iniqrheight.ToString() != MainWindow.qrheight.ToString() ||
                    MainWindow.photonum.ToString() != MainWindow.iniphotonum.ToString() || MainWindow.textcolor != MainWindow.initextcolor.ToString() || MainWindow.iniqrpageuse.ToString() != MainWindow.qrpageuse.ToString() || MainWindow.inidatetext.ToString() != MainWindow.datetext.ToString() ||
                    MainWindow.datetextcolor.ToString() != MainWindow.inidatetextcolor.ToString() || Convert.ToInt32(MainWindow.iniKakaoTimer.ToString()) != MainWindow.kakaotimer || MainWindow.cardnum != MainWindow.inicardnum.ToString() || MainWindow.SorR != MainWindow.iniSorR.ToString() || MainWindow.initimerlocation.ToString() != MainWindow.timerlocation ||
                    MainWindow.iniCheckVideo.ToString() != MainWindow.checkvideo || MainWindow.inikakaotextcolor.ToString() != MainWindow.kakaotextcolor || MainWindow.iniautologin.ToString() != MainWindow.autologin || MainWindow.temp2fee != MainWindow.initemp2fee.ToString() || MainWindow.temp3fee != MainWindow.initemp3fee.ToString() ||
                    MainWindow.inicoupon.ToString() != MainWindow.coupon || MainWindow.couponkind != MainWindow.inicouponkind.ToString() || MainWindow.checkcolor != MainWindow.inicheckcolor.ToString() ||
                    MainWindow.inipic5startx.ToString() != MainWindow.pic5startx.ToString() || MainWindow.inipic5starty.ToString() != MainWindow.pic5starty.ToString() || MainWindow.inipic5width.ToString() != MainWindow.pic5width.ToString() || MainWindow.inipic5height.ToString() != MainWindow.pic5height.ToString() ||
                    MainWindow.inipic6startx.ToString() != MainWindow.pic6startx.ToString() || MainWindow.inipic6starty.ToString() != MainWindow.pic6starty.ToString() || MainWindow.inipic6width.ToString() != MainWindow.pic6width.ToString() || MainWindow.inipic6height.ToString() != MainWindow.pic6height.ToString() ||
                    MainWindow.initogether.ToString() != MainWindow.checkingtogether)
                {
                    if (MessageBox.Show("값을 저장하지 않았습니다.\n\n저장하지 않고 넘어가시겟습니까?", "경고", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        // Serialcheck 2
                        MainWindow.brightvalue = Convert.ToInt32(MainWindow.bright.ToString());
                        MainWindow.crvalue = Convert.ToInt32(MainWindow.ContrastRatio.ToString());

                        //Serialcheck 1
                        MainWindow.SorR = MainWindow.iniSorR.ToString();
                        MainWindow.checkretake = MainWindow.retake.ToString();
                        MainWindow.checkkakao = MainWindow.checksns.ToString();
                        MainWindow.checkfreeornot = MainWindow.checkfree.ToString();
                        MainWindow.checktimeruse = MainWindow.Timer.ToString();
                        MainWindow.count = MainWindow.pagecount.ToString();
                        MainWindow.photocount = MainWindow.phototime.ToString();
                        MainWindow.moneyset = MainWindow.inisetmoney.ToString();
                        MainWindow.offsetx = MainWindow.getOffSetx.ToString();
                        MainWindow.offsety = MainWindow.getOffSety.ToString();
                        MainWindow.factorx = MainWindow.getFactorx.ToString();
                        MainWindow.factory = MainWindow.getFactory.ToString();
                        MainWindow.checktempselect = MainWindow.tempselect.ToString();
                        MainWindow.comportnum = MainWindow.ComNum.ToString();
                        MainWindow.leftrightcount = Convert.ToInt32(MainWindow.Xcount.ToString());
                        MainWindow.updowncount = Convert.ToInt32(MainWindow.Ycount.ToString());
                        MainWindow.zoomratio = Convert.ToDouble(MainWindow.Zoom.ToString());
                        MainWindow.inifoldername = MainWindow.foldername.ToString();
                        MainWindow.inimachinecode = MainWindow.ID.ToString();
                        MainWindow.paymentway = MainWindow.paymentini.ToString();
                        MainWindow.pagenum = MainWindow.pagenumini.ToString();
                        MainWindow.qroption = Convert.ToInt32(MainWindow.useqr.ToString());
                        MainWindow.tempoption = Convert.ToInt32(MainWindow.useselecttemp.ToString());
                        MainWindow.optiontempnum = Convert.ToInt32(MainWindow.tempnumber.ToString());
                        MainWindow.picratio = Convert.ToInt32(MainWindow.inipicratio.ToString());
                        MainWindow.printerratio = Convert.ToInt32(MainWindow.iniprinterratio.ToString());
                        MainWindow.pic1startx = Convert.ToInt32(MainWindow.inipic1startx.ToString());
                        MainWindow.pic1starty = Convert.ToInt32(MainWindow.inipic1starty.ToString());
                        MainWindow.pic1width = Convert.ToInt32(MainWindow.inipic1width.ToString());
                        MainWindow.pic1height = Convert.ToInt32(MainWindow.inipic1height.ToString());
                        MainWindow.pic2startx = Convert.ToInt32(MainWindow.inipic2startx.ToString());
                        MainWindow.pic2starty = Convert.ToInt32(MainWindow.inipic2starty.ToString());
                        MainWindow.pic2width = Convert.ToInt32(MainWindow.inipic2width.ToString());
                        MainWindow.pic2height = Convert.ToInt32(MainWindow.inipic2height.ToString());
                        MainWindow.pic3startx = Convert.ToInt32(MainWindow.inipic3startx.ToString());
                        MainWindow.pic3starty = Convert.ToInt32(MainWindow.inipic3starty.ToString());
                        MainWindow.pic3width = Convert.ToInt32(MainWindow.inipic3width.ToString());
                        MainWindow.pic3height = Convert.ToInt32(MainWindow.inipic3height.ToString());
                        MainWindow.pic4startx = Convert.ToInt32(MainWindow.inipic4startx.ToString());
                        MainWindow.pic4starty = Convert.ToInt32(MainWindow.inipic4starty.ToString());
                        MainWindow.pic4width = Convert.ToInt32(MainWindow.inipic4width.ToString());
                        MainWindow.pic4height = Convert.ToInt32(MainWindow.inipic4height.ToString());
                        MainWindow.pic5startx = Convert.ToInt32(MainWindow.inipic5startx.ToString());
                        MainWindow.pic5starty = Convert.ToInt32(MainWindow.inipic5starty.ToString());
                        MainWindow.pic5width = Convert.ToInt32(MainWindow.inipic5width.ToString());
                        MainWindow.pic5height = Convert.ToInt32(MainWindow.inipic5height.ToString());
                        MainWindow.pic6startx = Convert.ToInt32(MainWindow.inipic6startx.ToString());
                        MainWindow.pic6starty = Convert.ToInt32(MainWindow.inipic6starty.ToString());
                        MainWindow.pic6width = Convert.ToInt32(MainWindow.inipic6width.ToString());
                        MainWindow.pic6height = Convert.ToInt32(MainWindow.inipic6height.ToString());
                        MainWindow.photonum = Convert.ToInt32(MainWindow.iniphotonum.ToString());
                        MainWindow.qrstartx = Convert.ToInt32(MainWindow.iniqrstartx.ToString());
                        MainWindow.qrstarty = Convert.ToInt32(MainWindow.iniqrstarty.ToString());
                        MainWindow.qrwidth = Convert.ToInt32(MainWindow.iniqrwidth.ToString());
                        MainWindow.qrheight = Convert.ToInt32(MainWindow.iniqrheight.ToString());
                        MainWindow.textcolor = MainWindow.initextcolor.ToString();
                        MainWindow.qrpageuse = Convert.ToInt32(MainWindow.iniqrpageuse.ToString());
                        MainWindow.datetext = Convert.ToInt32(MainWindow.inidatetext.ToString());
                        MainWindow.datetextcolor = MainWindow.inidatetextcolor.ToString();
                        MainWindow.kakaotimer = Convert.ToInt32(MainWindow.iniKakaoTimer.ToString());
                        MainWindow.cardnum = MainWindow.inicardnum.ToString();
                        MainWindow.timerlocation = MainWindow.initimerlocation.ToString();
                        MainWindow.checkvideo = MainWindow.iniCheckVideo.ToString();
                        MainWindow.kakaotextcolor = MainWindow.inikakaotextcolor.ToString();
                        MainWindow.autologin = MainWindow.iniautologin.ToString();
                        MainWindow.coupon = MainWindow.inicoupon.ToString();
                        MainWindow.couponkind = MainWindow.inicouponkind.ToString();
                        MainWindow.checkcolor = MainWindow.inicheckcolor.ToString();
                        MainWindow.checkingtogether = MainWindow.initogether.ToString();
                        

                        System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(MainWindow.textcolor);
                        MainWindow.textbrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));

                        System.Drawing.Color datecolor = System.Drawing.ColorTranslator.FromHtml(MainWindow.datetextcolor);
                        MainWindow.datetextcolorbrush = new SolidBrush(System.Drawing.Color.FromArgb(datecolor.A, datecolor.R, datecolor.G, datecolor.B));

                        System.Drawing.Color kakaocolor = System.Drawing.ColorTranslator.FromHtml(MainWindow.kakaotextcolor);
                        MainWindow.fontbrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(kakaocolor.A, kakaocolor.R, kakaocolor.G, kakaocolor.B));

                        switch (MainWindow.tempnumber.ToString())
                        {
                            case "1":
                                MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                                MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                                break;
                            case "2":
                                MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                                MainWindow.temp1_2 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_2.png");
                                MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                                MainWindow.temp1_2Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_2Front.png");
                                break;
                            case "3":
                                MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                                MainWindow.temp1_2 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_2.png");
                                MainWindow.temp1_3 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_3.png");
                                MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                                MainWindow.temp1_2Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_2Front.png");
                                MainWindow.temp1_3Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_3Front.png");
                                break;
                            case "4":
                                MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                                MainWindow.temp1_2 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_2.png");
                                MainWindow.temp1_3 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_3.png");
                                MainWindow.temp1_4 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_4.png");
                                MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                                MainWindow.temp1_2Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_2Front.png");
                                MainWindow.temp1_3Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_3Front.png");
                                MainWindow.temp1_4Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_4Front.png");
                                break;
                            case "5":
                                MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                                MainWindow.temp1_2 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_2.png");
                                MainWindow.temp1_3 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_3.png");
                                MainWindow.temp1_4 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_4.png");
                                MainWindow.temp1_5 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_5.png");
                                MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                                MainWindow.temp1_2Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_2Front.png");
                                MainWindow.temp1_3Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_3Front.png");
                                MainWindow.temp1_4Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_4Front.png");
                                MainWindow.temp1_5Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_5Front.png");
                                break;
                            case "6":
                                MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                                MainWindow.temp1_2 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_2.png");
                                MainWindow.temp1_3 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_3.png");
                                MainWindow.temp1_4 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_4.png");
                                MainWindow.temp1_5 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_5.png");
                                MainWindow.temp1_6 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_6.png");
                                MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                                MainWindow.temp1_2Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_2Front.png");
                                MainWindow.temp1_3Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_3Front.png");
                                MainWindow.temp1_4Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_4Front.png");
                                MainWindow.temp1_5Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_5Front.png");
                                MainWindow.temp1_6Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_6Front.png");
                                break;
                        }

                        NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    switch (MainWindow.tempnumber.ToString())
                    {
                        case "1":
                            MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                            MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                            break;
                        case "2":
                            MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                            MainWindow.temp1_2 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_2.png");
                            MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                            MainWindow.temp1_2Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_2Front.png");
                            break;
                        case "3":
                            MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                            MainWindow.temp1_2 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_2.png");
                            MainWindow.temp1_3 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_3.png");
                            MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                            MainWindow.temp1_2Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_2Front.png");
                            MainWindow.temp1_3Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_3Front.png");
                            break;
                        case "4":
                            MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                            MainWindow.temp1_2 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_2.png");
                            MainWindow.temp1_3 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_3.png");
                            MainWindow.temp1_4 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_4.png");
                            MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                            MainWindow.temp1_2Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_2Front.png");
                            MainWindow.temp1_3Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_3Front.png");
                            MainWindow.temp1_4Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_4Front.png");
                            break;
                        case "5":
                            MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                            MainWindow.temp1_2 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_2.png");
                            MainWindow.temp1_3 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_3.png");
                            MainWindow.temp1_4 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_4.png");
                            MainWindow.temp1_5 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_5.png");
                            MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                            MainWindow.temp1_2Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_2Front.png");
                            MainWindow.temp1_3Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_3Front.png");
                            MainWindow.temp1_4Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_4Front.png");
                            MainWindow.temp1_5Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_5Front.png");
                            break;
                        case "6":
                            MainWindow.temp1_1 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_1.png");
                            MainWindow.temp1_2 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_2.png");
                            MainWindow.temp1_3 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_3.png");
                            MainWindow.temp1_4 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_4.png");
                            MainWindow.temp1_5 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_5.png");
                            MainWindow.temp1_6 = Bitmap.FromFile(MainWindow.TempPath + @"\Temp1_6.png");
                            MainWindow.temp1_1Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_1Front.png");
                            MainWindow.temp1_2Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_2Front.png");
                            MainWindow.temp1_3Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_3Front.png");
                            MainWindow.temp1_4Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_4Front.png");
                            MainWindow.temp1_5Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_5Front.png");
                            MainWindow.temp1_6Front = Bitmap.FromFile(MainWindow.TempPath + @"\temp1_6Front.png");
                            break;
                    }

                    System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(MainWindow.textcolor);
                    MainWindow.textbrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));

                    System.Drawing.Color datecolor = System.Drawing.ColorTranslator.FromHtml(MainWindow.datetextcolor);
                    MainWindow.datetextcolorbrush = new SolidBrush(System.Drawing.Color.FromArgb(datecolor.A, datecolor.R, datecolor.G, datecolor.B));

                    System.Drawing.Color kakaocolor = System.Drawing.ColorTranslator.FromHtml(MainWindow.kakaotextcolor);
                    MainWindow.fontbrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(kakaocolor.A, kakaocolor.R, kakaocolor.G, kakaocolor.B));

                    NavigationService.Navigate(new Uri("View/Main.xaml", UriKind.RelativeOrAbsolute));
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Retakebtn_Checked(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 재촬영 버튼 체크");
            try
            {
                MainWindow.checkretake = "0";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void NoTakebtn_Checked(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 재촬영 금지 버튼 체크");
            try
            {
                MainWindow.checkretake = "1";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void FlipUse_Checked(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 좌우반전 사용");
            try
            {
                FlipUse.IsChecked = true;
                MainWindow.checkingflip = "Using";

                if (MainWindow.checkingflip == "Using")
                {
                    LVCanvus.Opacity = 1;
                    FlipCanvas.Opacity = 0;
                    LVCanvus.Background = Main.bgbrush;
                }
                else
                {
                    LVCanvus.Opacity = 0;
                    FlipCanvas.Opacity = 1;
                    FlipCanvas.Background = Main.bgbrush;
                }
                if (MainWindow.camnumber.ToString() == "1")
                {
                    if (!Main.MainCamera.IsLiveViewOn)
                    {
                        LVCanvus.Background = System.Windows.Media.Brushes.LightGray;
                        FlipCanvas.Background = System.Windows.Media.Brushes.LightGray;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void FlipNotUse_Checked(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + " () | 좌우반전 사용안함");
            try
            {
                FlipNotUse.IsChecked = true;
                MainWindow.checkingflip = "NoUsing";

                if (MainWindow.checkingflip == "Using")
                {
                    LVCanvus.Opacity = 1;
                    FlipCanvas.Opacity = 0;
                    LVCanvus.Background = Main.bgbrush;
                }
                else
                {
                    LVCanvus.Opacity = 0;
                    FlipCanvas.Opacity = 1;
                    FlipCanvas.Background = Main.bgbrush;
                }

                if (MainWindow.camnumber.ToString() == "1")
                {
                    if (!Main.MainCamera.IsLiveViewOn)
                    {
                        LVCanvus.Background = System.Windows.Media.Brushes.LightGray;
                        FlipCanvas.Background = System.Windows.Media.Brushes.LightGray;
                    }
                }
            }
            catch(Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void checkinsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 지폐기 투입 가능 버튼 클릭");
                checkinsert.IsEnabled = false;
                DataSend('S', 'A', '\u000d');
                checkinsertstop.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void checkinsertstop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 지폐기 투입 불가 버튼 클릭");
                checkinsertstop.IsEnabled = false;
                DataSend('S', 'A', '\u000e');
                checkinsert.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void dispose()
        {
            Main.MainCamera.Dispose();
        }

        private void cameradispose()
        {
            ErrorHandler.SevereErrorHappened -= ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
            ErrorHandler.NonSevereErrorHappened -= ErrorHandler_NonSevereErrorHappened; //에러 발생 시
        }

        private void cameranull()
        {
            Main.MainCamera.Dispose();
            Main.MainCamera = null;
            Main.APIHandler.Dispose();
            Main.APIHandler = null;
            Main.SetImageAction = null;
            Main.SetImageAction2 = null;
        }

        #endregion

        #region///타이머 옵션 및 템플릿선택///

        private void timeruse_Checked(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + " () | 타이머 사용 체크");
            try
            {
                MainWindow.checktimeruse = "Use";
                PageTimeSetCoBox.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.Adminlogin != 1)
            {
                Password pw = new Password();
                pw.Owner = Application.Current.MainWindow;
                pw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                Source.Log.log.Info("관리자 로그인페이지 접속");
                pw.ShowDialog();

                if (MainWindow.Adminlogin == 1)
                {
                    SettingGroupBox.IsEnabled = true;
                    InitGroupBox.IsEnabled = true;
                    StoreName.IsEnabled = true;
                    MachineNum.IsEnabled = true;
                    checkpayment.IsEnabled = true;
                }
            }
            else
            {
                return;
            }
        }

        private void StoreSetting_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.Adminlogin != 1)
            {
                Password pw = new Password();
                pw.Owner = Application.Current.MainWindow;
                pw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                Source.Log.log.Info("관리자 로그인페이지 접속");
                pw.ShowDialog();

                if (MainWindow.Adminlogin == 1)
                {
                    SettingGroupBox.IsEnabled = true;
                    InitGroupBox.IsEnabled = true;
                    StoreName.IsEnabled = true;
                    MachineNum.IsEnabled = true;
                    checkpayment.IsEnabled = true;
                    NextPageBtn.IsEnabled = true;
                }
            }
            else
            {
                return;
            }
        }

        private void timernotuse_Checked(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 타이머 미사용 체크");
            try
            {
                MainWindow.checktimeruse = "Not";
                PageTimeSetCoBox.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                creditbook credit = new creditbook();
                credit.Left = 0;
                credit.Top = 0;
                credit.ShowDialog();
            }
            catch (Exception ex)
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void StoreName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                MainWindow.inifoldername = StoreName.Text;

                if (MainWindow.inifoldername.Contains("mediagram"))
                {
                    checktogethertb.IsEnabled = true;
                    checktogethertb.Opacity = 1;
                }
                else if (!MainWindow.inifoldername.Contains("mediagram"))
                {
                    checktogethertb.IsEnabled = false;
                    checktogethertb.Opacity = 0;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void MachineNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.inimachinecode = MachineNum.Text;
        }

        private void WebCamOption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.Webcam.ShowPropertiesDialog();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void checkcam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.camnumber.ToString() == "1")
                {
                    picturececk++;

                    Main.MainCamera.SendCommand(CameraCommand.PressShutterButton, (int)ShutterButton.Halfway); // 반셔터
                    Main.MainCamera.SendCommand(CameraCommand.PressShutterButton, (int)ShutterButton.OFF);
                    Delay(1000);
                    Main.MainCamera.SendCommand(CameraCommand.PressShutterButton, (int)ShutterButton.Completely_NonAF); // Non 포커스 촬영
                    Main.MainCamera.SendCommand(CameraCommand.PressShutterButton, (int)ShutterButton.OFF);

                    Delay(1000);
                    
                    if (picturececk < 10)
                    {
                        canoncutimg(MainWindow.PhotoPath + @"\IMG_000" + picturececk.ToString() + ".jpg");
                    }
                    else if (picturececk > 10 && picturececk < 100)
                    {
                        canoncutimg(MainWindow.PhotoPath + @"\IMG_00" + picturececk.ToString() + ".jpg");
                    }

                    string day = DateTime.Today.ToString("dd");
                    int offSetX = Convert.ToInt32(MainWindow.offsetx);
                    int offSetY = Convert.ToInt32(MainWindow.offsety);
                    int factorX = Convert.ToInt32(MainWindow.factorx);
                    int factorY = Convert.ToInt32(MainWindow.factory);

                    Bitmap canvas4_2;
                    canvas4_2 = new Bitmap(1200, 1800);
                    Graphics g4_2 = Graphics.FromImage(canvas4_2);

                    System.Drawing.Image img4_2 = Bitmap.FromFile(MainWindow.TempPath + "\\" + "Temp1_1.png");
                    System.Drawing.Image testimg = null;
                    if (picturececk < 10)
                    {
                        testimg = Bitmap.FromFile(MainWindow.ResizePath + @"\RPhoto_000" + picturececk.ToString() + ".JPG");
                    }
                    else if (picturececk > 10 && picturececk < 100)
                    {
                        testimg = Bitmap.FromFile(MainWindow.ResizePath + @"\RPhoto_00" + picturececk.ToString() + ".JPG");
                    }
                    g4_2.DrawImage(img4_2, 0, 0, 600, 1800);
                    g4_2.DrawImage(img4_2, 600, 0, 600, 1800);
                    g4_2.DrawImage(testimg, 30, 30, 540, 360);
                    g4_2.DrawImage(testimg, 30, 420, 540, 360);
                    g4_2.DrawImage(testimg, 30, 810, 540, 360);
                    g4_2.DrawImage(testimg, 30, 1200, 540, 360);
                    g4_2.DrawImage(testimg, 630, 30, 540, 360);
                    g4_2.DrawImage(testimg, 630, 420, 540, 360);
                    g4_2.DrawImage(testimg, 630, 810, 540, 360);
                    g4_2.DrawImage(testimg, 630, 1200, 540, 360);
                    PrintBitmap(canvas4_2, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                    papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                    MainWindow.papercount.Clear();
                    MainWindow.papercount.Append(papercountdown);
                    WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                    WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                    Initialization.Text = papercountdown;
                    testimg.Dispose();
                    g4_2.Dispose();
                    img4_2.Dispose();
                    canvas4_2.Dispose();
                }
                else
                {
                    testphototime = true;

                    //Delay(2000);

                    //savebitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(flipframe);
                    savebitmap = bitmap;
                    savebitmap.SetResolution(72, 72);

                    ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L); // JPEG 품질 조절

                    savebitmap.Save(MainWindow.PhotoPath + @"\Test.JPG", jpegCodec, encoderParams);

                    //Delay(1);

                    CutImg(MainWindow.PhotoPath + @"\Test.JPG");

                    string day = DateTime.Today.ToString("dd");
                    int offSetX = Convert.ToInt32(MainWindow.offsetx);
                    int offSetY = Convert.ToInt32(MainWindow.offsety);
                    int factorX = Convert.ToInt32(MainWindow.factorx);
                    int factorY = Convert.ToInt32(MainWindow.factory);

                    Bitmap canvas4_2;
                    canvas4_2 = new Bitmap(1200, 1800);
                    Graphics g4_2 = Graphics.FromImage(canvas4_2);

                    System.Drawing.Image img4_2 = Bitmap.FromFile(MainWindow.TempPath + "\\" + "Temp1_1.png");
                    System.Drawing.Image testimg = Bitmap.FromFile(MainWindow.ResizePath + @"\RPhoto_Test.JPG");
                    g4_2.DrawImage(img4_2, 0, 0, 600, 1800);
                    g4_2.DrawImage(img4_2, 600, 0, 600, 1800);
                    g4_2.DrawImage(testimg, 30, 30, 540, 360);
                    g4_2.DrawImage(testimg, 30, 420, 540, 360);
                    g4_2.DrawImage(testimg, 30, 810, 540, 360);
                    g4_2.DrawImage(testimg, 30, 1200, 540, 360);
                    g4_2.DrawImage(testimg, 630, 30, 540, 360);
                    g4_2.DrawImage(testimg, 630, 420, 540, 360);
                    g4_2.DrawImage(testimg, 630, 810, 540, 360);
                    g4_2.DrawImage(testimg, 630, 1200, 540, 360);
                    PrintBitmap(canvas4_2, 1, isLandScape: true, offSetX, offSetY, factorX, factorY);
                    papercountdown = Convert.ToString(Convert.ToInt32(MainWindow.papercount.ToString()) - 1);
                    MainWindow.papercount.Clear();
                    MainWindow.papercount.Append(papercountdown);
                    WritePrivateProfileString("Setting", "PaperCount", papercountdown, MainWindow.iniPath);
                    WritePrivateProfileString("RemainPaper", day, papercountdown, MainWindow.bankbookinipath);
                    Initialization.Text = papercountdown;
                    g4_2.Dispose();
                    img4_2.Dispose();
                    canvas4_2.Dispose();

                    testphototime = false;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void canoncutimg(string path)
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 구동 | 이미지위치 : " + path);
                Mat src = new Mat(path);
                Mat dst = new Mat();
                Mat flip = new Mat();

                if (MainWindow.checkingflip.ToString() == "NoUsing")
                {
                    dst = src.SubMat(new OpenCvSharp.Rect(0, 0, src.Width, src.Height));
                    Cv2.Flip(dst, flip, FlipMode.Y);
                }
                else
                {
                    dst = src.SubMat(new OpenCvSharp.Rect(0, 0, src.Width, src.Height));
                }

                Cv2.ImWrite(MainWindow.ResizePath + @"\RPhoto_0001.JPG", dst);

                dst.Dispose();
                src.Dispose();
                flip.Dispose();
                dst = null;
                src = null;
                flip = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void CutImg(string path)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작 | 이미지 위치 : " + path);
                Mat src = new Mat(path, ImreadModes.Color);
                Mat dst = new Mat();

                dst = src.SubMat(new OpenCvSharp.Rect((src.Width - (src.Height / 2 * 3)) / 2, 0, src.Height / 2 * 3, src.Height));

                Cv2.ImWrite(MainWindow.ResizePath + @"\RPhoto_Test.JPG", dst);

                dst.Dispose();
                src.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

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

        private void KakaoPageTimeSetCoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (KakaoPageTimeSetCoBox.SelectedIndex < 0)
                {
                    return;
                }
                switch (KakaoPageTimeSetCoBox.SelectedIndex)
                {
                    case 0:
                        MainWindow.kakaotimer = 45;
                        break;
                    case 1:
                        MainWindow.kakaotimer = 60;
                        break;
                    case 2:
                        MainWindow.kakaotimer = 75;
                        break;
                    case 3:
                        MainWindow.kakaotimer = 90;
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void PayWayCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (PayWayCB.SelectedIndex < 0)
                {
                    return;
                }

                switch (PayWayCB.SelectedIndex)
                {
                    case 0:
                        MainWindow.checkfreeornot = "1";
                        MainWindow.paymentway = "free";

                        PayWayCB.SelectedItem = PayWayCB.Items[0];
                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[0];
                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
                        couponkind.SelectedItem = couponkind.Items[0];
                        couponkind.IsEnabled = false;

                        SettingFeeGroupBox.IsEnabled = false;

                        if (InitGroupBox != null)
                        {
                            InitGroupBox.IsEnabled = false;
                            if (port?.IsOpen == true)
                            {
                                port.Close();
                                port.Dispose();
                                SerialLabel.Content = "DisConnect";
                            }
                        }

                        MainWindow.paymentway = "free";
                        InitGroupBox.IsEnabled = false;

                        search.IsEnabled = false;
                        if (MainWindow.cardnum.ToString() == "AT0403177A")
                        {
                            cardserialtextbox.Text = "기본값";
                        }
                        else if (MainWindow.cardnum.ToString() == "")
                        {
                            cardserialtextbox.Text = "기본값";
                        }
                        else
                        {
                            cardserialtextbox.Text = MainWindow.cardnum;
                        }
                        cardserialtextbox.IsEnabled = false;
                        break;
                    case 1:
                        MainWindow.paymentway = "cash";
                        InitGroupBox.IsEnabled = true;

                        search.IsEnabled = false;
                        if (MainWindow.cardnum.ToString() == "AT0403177A")
                        {
                            cardserialtextbox.Text = "기본값";
                        }
                        else if (MainWindow.cardnum.ToString() == "")
                        {
                            cardserialtextbox.Text = "기본값";
                        }
                        else
                        {
                            cardserialtextbox.Text = MainWindow.cardnum;
                        }

                        if (InitGroupBox != null)
                        {
                            InitGroupBox.IsEnabled = true;
                            cardserialtextbox.IsEnabled = false;
                            CheckSerial();
                        }
                        MainWindow.checkfreeornot = "0";

                        if (MainWindow.Adminlogin == 1)
                        {
                            checkpayment.IsEnabled = true;
                        }

                        SettingFeeGroupBox.IsEnabled = true;

                        if (MainWindow.SorR == "R")
                        {
                            if (SettingFeeGroupBox != null)
                            {
                                if (MainWindow.moneyset == "0")
                                {
                                    MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.moneyset)
                                    {
                                        case "0":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                                temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                                temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
                            }
                        }
                        else
                        {
                            if (SettingFeeGroupBox != null)
                            {
                                if (MainWindow.moneyset == "0")
                                {
                                    MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.moneyset)
                                    {
                                        case "0":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                                if (MainWindow.temp2fee == "0")
                                {
                                    temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.temp2fee)
                                    {
                                        case "0":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                                if (MainWindow.temp3fee == "0")
                                {
                                    temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.temp3fee)
                                    {
                                        case "0":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                            }
                        }
                        couponkind.SelectedItem = couponkind.Items[0];
                        couponkind.IsEnabled = false;
                        break;
                    case 2:
                        MainWindow.paymentway = "card";
                        InitGroupBox.IsEnabled = false;

                        search.IsEnabled = true;

                        cardserialtextbox.IsEnabled = true;
                        if (MainWindow.cardnum.ToString() == "AT0403177A")
                        {
                            cardserialtextbox.Text = "기본값";
                        }
                        else if (MainWindow.cardnum.ToString() == "")
                        {
                            cardserialtextbox.Text = "기본값";
                        }
                        else
                        {
                            cardserialtextbox.Text = MainWindow.cardnum;
                        }
                        MainWindow.checkfreeornot = "0";

                        if (MainWindow.Adminlogin == 1)
                        {
                            checkpayment.IsEnabled = true;
                        }

                        SettingFeeGroupBox.IsEnabled = true;

                        if (SettingFeeGroupBox != null)
                        {
                            if (MainWindow.moneyset == "0")
                            {
                                MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                            }
                            else
                            {
                                switch (MainWindow.moneyset)
                                {
                                    case "0":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[0];
                                        break;
                                    case "1000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[1];
                                        break;
                                    case "2000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[2];
                                        break;
                                    case "3000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[3];
                                        break;
                                    case "4000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[4];
                                        break;
                                    case "5000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                        break;
                                    case "6000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[6];
                                        break;
                                    case "7000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[7];
                                        break;
                                    case "8000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[8];
                                        break;
                                    case "9000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[9];
                                        break;
                                    case "10000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[10];
                                        break;
                                }
                            }
                            if (MainWindow.temp2fee == "0")
                            {
                                temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[5];
                            }
                            else
                            {
                                switch (MainWindow.temp2fee)
                                {
                                    case "0":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                                        break;
                                    case "1000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[1];
                                        break;
                                    case "2000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[2];
                                        break;
                                    case "3000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[3];
                                        break;
                                    case "4000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[4];
                                        break;
                                    case "5000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[5];
                                        break;
                                    case "6000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[6];
                                        break;
                                    case "7000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[7];
                                        break;
                                    case "8000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[8];
                                        break;
                                    case "9000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[9];
                                        break;
                                    case "10000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[10];
                                        break;
                                }
                            }
                            if (MainWindow.temp3fee == "0")
                            {
                                temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[5];
                            }
                            else
                            {
                                switch (MainWindow.temp3fee)
                                {
                                    case "0":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
                                        break;
                                    case "1000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[1];
                                        break;
                                    case "2000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[2];
                                        break;
                                    case "3000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[3];
                                        break;
                                    case "4000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[4];
                                        break;
                                    case "5000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[5];
                                        break;
                                    case "6000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[6];
                                        break;
                                    case "7000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[7];
                                        break;
                                    case "8000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[8];
                                        break;
                                    case "9000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[9];
                                        break;
                                    case "10000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[10];
                                        break;
                                }
                            }
                        }
                        couponkind.SelectedItem = couponkind.Items[0];
                        couponkind.IsEnabled = false;
                        break;
                    case 3:
                        MainWindow.paymentway = "cardcash";
                        InitGroupBox.IsEnabled = true;

                        search.IsEnabled = true;

                        if (InitGroupBox != null)
                        {
                            InitGroupBox.IsEnabled = true;
                            if (MainWindow.inicardnum.ToString() == "AT0403177A")
                            {
                                cardserialtextbox.Text = "기본값";
                            }
                            else if (MainWindow.inicardnum.ToString() == "")
                            {
                                cardserialtextbox.Text = "기본값";
                            }
                            else
                            {
                                cardserialtextbox.Text = MainWindow.inicardnum.ToString();
                            }
                            CheckSerial();
                        }
                        MainWindow.checkfreeornot = "0";

                        if (MainWindow.Adminlogin == 1)
                        {
                            checkpayment.IsEnabled = true;
                        }

                        if (InitGroupBox != null)
                        {
                            InitGroupBox.IsEnabled = true;
                            CheckSerial();
                        }

                        SettingFeeGroupBox.IsEnabled = true;

                        if (MainWindow.SorR == "R")
                        {
                            if (SettingFeeGroupBox != null)
                            {
                                if (MainWindow.moneyset == "0")
                                {
                                    MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.moneyset)
                                    {
                                        case "0":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                                temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                                temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
                            }
                        }
                        else
                        {
                            if (SettingFeeGroupBox != null)
                            {
                                if (MainWindow.moneyset == "0")
                                {
                                    MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.moneyset)
                                    {
                                        case "0":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                                if (MainWindow.temp2fee == "0")
                                {
                                    temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.temp2fee)
                                    {
                                        case "0":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                                if (MainWindow.temp3fee == "0")
                                {
                                    temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.temp3fee)
                                    {
                                        case "0":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                            }
                        }
                        couponkind.SelectedItem = couponkind.Items[0];
                        couponkind.IsEnabled = false;
                        break;
                    case 4:
                        MainWindow.paymentway = "cashcoupon";
                        InitGroupBox.IsEnabled = true;

                        search.IsEnabled = false;

                        if (InitGroupBox != null)
                        {
                            InitGroupBox.IsEnabled = true;
                            if (MainWindow.inicardnum.ToString() == "AT0403177A")
                            {
                                cardserialtextbox.Text = "기본값";
                            }
                            else if (MainWindow.inicardnum.ToString() == "")
                            {
                                cardserialtextbox.Text = "기본값";
                            }
                            else
                            {
                                cardserialtextbox.Text = MainWindow.inicardnum.ToString();
                            }
                            CheckSerial();
                        }
                        MainWindow.checkfreeornot = "0";

                        if (MainWindow.Adminlogin == 1)
                        {
                            checkpayment.IsEnabled = true;
                        }

                        if (InitGroupBox != null)
                        {
                            InitGroupBox.IsEnabled = true;
                            CheckSerial();
                        }

                        SettingFeeGroupBox.IsEnabled = true;

                        if (MainWindow.SorR == "R")
                        {
                            if (SettingFeeGroupBox != null)
                            {
                                if (MainWindow.moneyset == "0")
                                {
                                    MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.moneyset)
                                    {
                                        case "0":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                                temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                                temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
                            }
                        }
                        else
                        {
                            if (SettingFeeGroupBox != null)
                            {
                                if (MainWindow.moneyset == "0")
                                {
                                    MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.moneyset)
                                    {
                                        case "0":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                                if (MainWindow.temp2fee == "0")
                                {
                                    temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.temp2fee)
                                    {
                                        case "0":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                                if (MainWindow.temp3fee == "0")
                                {
                                    temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[5];
                                }
                                else
                                {
                                    switch (MainWindow.temp3fee)
                                    {
                                        case "0":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
                                            break;
                                        case "1000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[1];
                                            break;
                                        case "2000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[2];
                                            break;
                                        case "3000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[3];
                                            break;
                                        case "4000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[4];
                                            break;
                                        case "5000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[5];
                                            break;
                                        case "6000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[6];
                                            break;
                                        case "7000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[7];
                                            break;
                                        case "8000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[8];
                                            break;
                                        case "9000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[9];
                                            break;
                                        case "10000":
                                            temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[10];
                                            break;
                                    }
                                }
                            }
                        }
                        
                        couponkind.IsEnabled = true;
                        switch (MainWindow.couponkind)
                        {
                            case "":
                                couponkind.SelectedItem = couponkind.Items[2];
                                break;
                            case "no":
                                couponkind.SelectedItem = couponkind.Items[0];
                                break;
                            case "num":
                                couponkind.SelectedItem = couponkind.Items[1];
                                break;
                            case "numchar":
                                couponkind.SelectedItem = couponkind.Items[2];
                                break;
                        }
                        break;
                    case 5:
                        MainWindow.paymentway = "cardcoupon";
                        InitGroupBox.IsEnabled = false;

                        search.IsEnabled = true;

                        if (InitGroupBox != null)
                        {
                            InitGroupBox.IsEnabled = true;
                            if (MainWindow.inicardnum.ToString() == "AT0403177A")
                            {
                                cardserialtextbox.Text = "기본값";
                            }
                            else if (MainWindow.inicardnum.ToString() == "")
                            {
                                cardserialtextbox.Text = "기본값";
                            }
                            else
                            {
                                cardserialtextbox.Text = MainWindow.inicardnum.ToString();
                            }
                            CheckSerial();
                        }
                        MainWindow.checkfreeornot = "0";

                        if (MainWindow.Adminlogin == 1)
                        {
                            checkpayment.IsEnabled = true;
                        }

                        if (InitGroupBox != null)
                        {
                            InitGroupBox.IsEnabled = true;
                            CheckSerial();
                        }

                        SettingFeeGroupBox.IsEnabled = true;

                        if (SettingFeeGroupBox != null)
                        {
                            if (MainWindow.moneyset == "0")
                            {
                                MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                            }
                            else
                            {
                                switch (MainWindow.moneyset)
                                {
                                    case "0":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[0];
                                        break;
                                    case "1000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[1];
                                        break;
                                    case "2000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[2];
                                        break;
                                    case "3000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[3];
                                        break;
                                    case "4000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[4];
                                        break;
                                    case "5000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[5];
                                        break;
                                    case "6000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[6];
                                        break;
                                    case "7000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[7];
                                        break;
                                    case "8000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[8];
                                        break;
                                    case "9000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[9];
                                        break;
                                    case "10000":
                                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[10];
                                        break;
                                }
                            }
                            if (MainWindow.temp2fee == "0")
                            {
                                temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[5];
                            }
                            else
                            {
                                switch (MainWindow.temp2fee)
                                {
                                    case "0":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                                        break;
                                    case "1000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[1];
                                        break;
                                    case "2000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[2];
                                        break;
                                    case "3000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[3];
                                        break;
                                    case "4000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[4];
                                        break;
                                    case "5000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[5];
                                        break;
                                    case "6000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[6];
                                        break;
                                    case "7000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[7];
                                        break;
                                    case "8000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[8];
                                        break;
                                    case "9000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[9];
                                        break;
                                    case "10000":
                                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[10];
                                        break;
                                }
                            }
                            if (MainWindow.temp3fee == "0")
                            {
                                temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[5];
                            }
                            else
                            {
                                switch (MainWindow.temp3fee)
                                {
                                    case "0":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
                                        break;
                                    case "1000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[1];
                                        break;
                                    case "2000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[2];
                                        break;
                                    case "3000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[3];
                                        break;
                                    case "4000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[4];
                                        break;
                                    case "5000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[5];
                                        break;
                                    case "6000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[6];
                                        break;
                                    case "7000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[7];
                                        break;
                                    case "8000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[8];
                                        break;
                                    case "9000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[9];
                                        break;
                                    case "10000":
                                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[10];
                                        break;
                                }
                            }
                        }
                        couponkind.IsEnabled = true;
                        switch (MainWindow.couponkind)
                        {
                            case "":
                                couponkind.SelectedItem = couponkind.Items[2];
                                break;
                            case "no":
                                couponkind.SelectedItem = couponkind.Items[0];
                                break;
                            case "num":
                                couponkind.SelectedItem = couponkind.Items[1];
                                break;
                            case "numchar":
                                couponkind.SelectedItem = couponkind.Items[2];
                                break;
                        }
                        break;
                    case 6:
                        MainWindow.paymentway = "coupon";
                        InitGroupBox.IsEnabled = false;

                        search.IsEnabled = false;

                        cardserialtextbox.IsEnabled = false;
                        if (MainWindow.cardnum.ToString() == "AT0403177A")
                        {
                            cardserialtextbox.Text = "기본값";
                        }
                        else if (MainWindow.cardnum.ToString() == "")
                        {
                            cardserialtextbox.Text = "기본값";
                        }
                        else
                        {
                            cardserialtextbox.Text = MainWindow.cardnum;
                        }
                        MainWindow.checkfreeornot = "0";

                        if (MainWindow.Adminlogin == 1)
                        {
                            checkpayment.IsEnabled = true;
                        }

                        SettingFeeGroupBox.IsEnabled = false;

                        MoneySettingCoBox.SelectedItem = MoneySettingCoBox.Items[0];
                        temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                        temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
                        couponkind.IsEnabled = true;
                        switch (MainWindow.couponkind)
                        {
                            case "":
                                couponkind.SelectedItem = couponkind.Items[2];
                                break;
                            case "no":
                                couponkind.SelectedItem = couponkind.Items[0];
                                break;
                            case "num":
                                couponkind.SelectedItem = couponkind.Items[1];
                                break;
                            case "numchar":
                                couponkind.SelectedItem = couponkind.Items[2];
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

        private void RentalBtn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.SorR = "R";

                temp2MoneySettingCoBox.IsEnabled = false;
                temp3MoneySettingCoBox.IsEnabled = false;
                temp2MoneySettingCoBox.SelectedItem = temp2MoneySettingCoBox.Items[0];
                temp3MoneySettingCoBox.SelectedItem = temp3MoneySettingCoBox.Items[0];
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void SellBtn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.SorR = "S";

                temp2MoneySettingCoBox.IsEnabled = true;
                temp3MoneySettingCoBox.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void temp2MoneySettingCoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 템플릿 2번 돈 셋팅 변경");
            try
            {
                if (temp2MoneySettingCoBox.SelectedIndex < 0)
                {
                    return;
                }
                switch (temp2MoneySettingCoBox.SelectedIndex)
                {
                    case 0:
                        MainWindow.temp2fee = "0";
                        break;
                    case 1:
                        MainWindow.temp2fee = "1000";
                        break;
                    case 2:
                        MainWindow.temp2fee = "2000";
                        break;
                    case 3:
                        MainWindow.temp2fee = "3000";
                        break;
                    case 4:
                        MainWindow.temp2fee = "4000";
                        break;
                    case 5:
                        MainWindow.temp2fee = "5000";
                        break;
                    case 6:
                        MainWindow.temp2fee = "6000";
                        break;
                    case 7:
                        MainWindow.temp2fee = "7000";
                        break;
                    case 8:
                        MainWindow.temp2fee = "8000";
                        break;
                    case 9:
                        MainWindow.temp2fee = "9000";
                        break;
                    case 10:
                        MainWindow.temp2fee = "10000";
                        break;
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

        private void temp3MoneySettingCoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 템플릿 3번 돈 셋팅 변경");
            try
            {
                if (temp3MoneySettingCoBox.SelectedIndex < 0)
                {
                    return;
                }
                switch (temp3MoneySettingCoBox.SelectedIndex)
                {
                    case 0:
                        MainWindow.temp3fee = "0";
                        break;
                    case 1:
                        MainWindow.temp3fee = "1000";
                        break;
                    case 2:
                        MainWindow.temp3fee = "2000";
                        break;
                    case 3:
                        MainWindow.temp3fee = "3000";
                        break;
                    case 4:
                        MainWindow.temp3fee = "4000";
                        break;
                    case 5:
                        MainWindow.temp3fee = "5000";
                        break;
                    case 6:
                        MainWindow.temp3fee = "6000";
                        break;
                    case 7:
                        MainWindow.temp3fee = "7000";
                        break;
                    case 8:
                        MainWindow.temp3fee = "8000";
                        break;
                    case 9:
                        MainWindow.temp3fee = "9000";
                        break;
                    case 10:
                        MainWindow.temp3fee = "10000";
                        break;
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

        private void cardserialtextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 로드");

                MainWindow.cardnum = cardserialtextbox.Text;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void timerlocationleft_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.timerlocation = "left";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void timerlocationright_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.timerlocation = "right";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void autouse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.autologin = "1";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void autonotuse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.autologin = "0";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void couponkind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (couponkind.SelectedIndex < 0)
                {
                    return;
                }
                switch (couponkind.SelectedIndex)
                {
                    case 0:
                        MainWindow.couponkind = "no";
                        break;
                    case 1:
                        MainWindow.couponkind = "num";
                        break;
                    case 2:
                        MainWindow.couponkind = "numchar";
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void togetheruse_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.checkingtogether = "Use";
        }

        private void solouse_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.checkingtogether = "No";
        }

        #endregion

        #region /// Send Api ///

        private void SendPaperApi()
        {
            string url = "";

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            string sendData = "MACHINE_CD=" + MainWindow.ID.ToString();

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

        #endregion

        private void APIHandler_CameraAdded(CanonAPI sender)
        {
            try { Dispatcher.Invoke((Action)delegate { RefreshCamera(); }); }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void RefreshCamera()
        {
            CamListBox.Items.Clear(); // 리스트박스 초기화
            Main.CamList = Main.APIHandler.GetCameraList(); // 연결된 카메라 리스트 불러오기

            foreach (EOSDigital.API.Camera cam in Main.CamList) CamListBox.Items.Add(cam.DeviceName); //리스트 박스에 카메라 이름으로 추가


            if (Main.MainCamera?.SessionOpen == true) CamListBox.SelectedIndex = Main.CamList.FindIndex(t => t.ID == Main.MainCamera.ID); // 세션이 열려있을 경우 현재 카메라 아이템 선택
            else if (Main.CamList.Count > 0) CamListBox.SelectedIndex = 0; // 세션이 시작 전에는 첫번째 아이템 선택
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}