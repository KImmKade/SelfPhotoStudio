using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using EOSDigital.API;
using EOSDigital.SDK;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Eventing.Reader;
using System.Management;
using Touchless.Vision.Camera;

namespace wpfTest.View
{
    /// <summary>
    /// Main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Main : Page
    {

        #region///카메라 변수///

        public static CanonAPI APIHandler; // Canon api 사용
        public static EOSDigital.API.Camera MainCamera; // 현재 연결된 카메라
        public static List<EOSDigital.API.Camera> CamList; //연결된 카메라 리스트
        public static bool IsInit = false; //카메라 연결 여부
        public static ImageBrush bgbrush = new ImageBrush(); //색상지정
        public static Action<BitmapImage> SetImageAction;
        public static Action<TransformedBitmap> SetImageAction2;
        System.Windows.Forms.FolderBrowserDialog SaveFolderBrowser = new System.Windows.Forms.FolderBrowserDialog(); //폴더 지정

        #endregion

        #region///지폐기 변수///

        public static SerialPort port = Payment.port;

        #endregion

        #region///변수///
        DispatcherTimer timer = new DispatcherTimer();
        int countdown = 1;
        public static string MCheckNum = "";

        #endregion

        public Main()
        {
            Source.Log.log.Info("Main Level 진입");
            InitializeComponent();
        }

        #region///이미지 로드 및 기반 로드///

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | Main Level 설정 값 로드(자동연결)");

                MainWindow.checkrepair = 0;
                MCheckNum = "";

                Mainimg.Source = MainWindow.main;

                MainWindow.firstphoto = null;
                MainWindow.secondphoto = null;
                MainWindow.thirdphoto = null;
                MainWindow.fourthphoto = null;
                MainWindow.fifthphoto = null;
                MainWindow.sixthphoto = null;
                MainWindow.seventhphoto = null;
                MainWindow.eighthphoto = null;

                string day = DateTime.Now.ToString("dd");
                if(MainWindow.day != day)
                {
                    MainWindow.day = day;
                    MainWindow.takenshot.Clear();
                    MainWindow.takenshot.Append(0);
                    MainWindow.printedpage.Clear();
                    MainWindow.printedpage.Append(0);
                    MainWindow.couponshot.Clear();
                    MainWindow.couponshot.Append(0);
                    MainWindow.collectmoney.Clear();
                    MainWindow.collectmoney.Append(0);
                    MainWindow.chargedshot.Clear();
                    MainWindow.chargedshot.Append(0);
                    MainWindow.freeshot.Clear();
                    MainWindow.freeshot.Append(0);

                }
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_Tick);
                timer.Start();
                Source.Log.log.Info("타이머 시작");
                if (MainWindow.camnumber.ToString() == "1" && MainCamera == null)
                {
                    if (APIHandler == null)
                    {
                        APIHandler = new CanonAPI();
                    }
                    APIHandler.CameraAdded += APIHandler_CameraAdded;
                    ErrorHandler.SevereErrorHappened += ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
                    ErrorHandler.NonSevereErrorHappened += ErrorHandler_NonSevereErrorHappened; //에러 발생 시

                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        SetImageAction2 = (TransformedBitmap img) => { bgbrush.ImageSource = img; }; // Live View에 그려지는 이미지 Action 
                    }
                    else
                    {
                        SetImageAction = (BitmapImage img) => { bgbrush.ImageSource = img; }; // Live View에 그려지는 이미지 Action 
                    }
                    SaveFolderBrowser.Description = "Save Image To...";
                    RefreshCamera(); //연결된 카메라 정보 수집
                    IsInit = true;
                    if (MainCamera == null)
                    {
                        OpenSession();
                    }
                }
                if (MainWindow.checkfree.ToString() == "0")
                {
                    if (MainWindow.paymentini.ToString() == "cash" || MainWindow.paymentini.ToString() == "cardcash" || MainWindow.paymentini.ToString() == "cashcoupon")
                    {
                        BillMachineOpenSession();
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (DllNotFoundException)
            {
                Source.Log.log.Error("Canon DLL을 찾을 수 없습니다.");
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod() + "() - " + ex.Message);
            }
        }

        #endregion

        #region///지폐기///

        private void BillMachineOpenSession()
        {
            try
            {
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 지폐기 자동연결 시작");
                if (port?.IsOpen == false)
                {
                    if (MainWindow.comportnum == "")
                    {
                        Source.Log.log.Info("등록 된 컴포트가 없습니다.");
                        return;
                    }
                    else
                    {
                        port.PortName = MainWindow.comportnum;
                        port.BaudRate = 9600;
                        port.DataBits = 8;
                        port.StopBits = StopBits.One;
                        port.Parity = Parity.None;
                        port.Open();
                        Source.Log.log.Info("컴포트 : " + MainWindow.comportnum + "으로 연결되었습니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        #endregion

        #region///카메라 구동///

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
            CamList = APIHandler.GetCameraList(); // 연결된 카메라 리스트 불러오기

            foreach (EOSDigital.API.Camera cam in CamList) CamListBox.Items.Add(cam.DeviceName); //리스트 박스에 카메라 이름으로 추가


            if (MainCamera?.SessionOpen == true) CamListBox.SelectedIndex = CamList.FindIndex(t => t.ID == MainCamera.ID); // 세션이 열려있을 경우 현재 카메라 아이템 선택
            else if (CamList.Count > 0) CamListBox.SelectedIndex = 0; // 세션이 시작 전에는 첫번째 아이템 선택
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void ErrorHandler_SevereErrorHappened(object sender, Exception ex)
        {
            Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
        }

        private void ErrorHandler_NonSevereErrorHappened(object sender, ErrorCode ex)
        {
            Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + $"() - SDK Error code : {ex} ({((int)ex).ToString("X")})");

        }

        private void CloseSession()
        {
            try
            {
                MainCamera.LiveViewUpdated -= MainCamera_LiveViewUpdated;
                MainCamera.StateChanged -= MainCamera_StateChanged;
                MainCamera.DownloadReady -= MainCamera_DownloadReady;
                MainCamera.CloseSession();
                Source.Log.log.Info("Canon 카메라 세션 닫힘");
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (NullReferenceException e)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + e.Message);
                MessageBox.Show("Canon 카메라와 연결이 끊겨있습니다.", "경고", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
                Main.MainCamera.Dispose();
                Main.MainCamera = null;
                Main.APIHandler.Dispose();
                Main.APIHandler = null;
                Main.SetImageAction2 = null;
                Main.SetImageAction = null;
            }
        }

        private void OpenSession()
        {
            try
            {
                if (CamListBox.SelectedIndex >= 0) // 카메라 선택 했을 시 수행
                {
                    if (MainCamera == null || MainCamera.SessionOpen == false)
                    {
                        MainCamera = CamList[CamListBox.SelectedIndex]; //리스트 박스에서 선택 된 카메라를 메인 카메라로 지정
                        MainCamera.OpenSession();
                        MainCamera.LiveViewUpdated += MainCamera_LiveViewUpdated; //라이브뷰 업데이트 시 동작
                        MainCamera.StateChanged += MainCamera_StateChanged; //상태 변화 시 동작
                        MainCamera.DownloadReady += MainCamera_DownloadReady; //다운로드 준비 시 동작

                        MainCamera.SetSetting(PropertyID.SaveTo, (int)SaveTo.Host); //Host(PC)에 저장 
                        MainCamera.SetCapacity(4096, int.MaxValue); //저장 용량 지정, Take Picture Card NG Error 해결
                        Source.Log.log.Info("Canon 카메라 연결 완료");
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            catch(Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void MainCamera_StateChanged(object sender, StateEventID eventID, int parameter)
        {
            try
            {
                if (eventID == StateEventID.Shutdown && IsInit) // 연결 해제 시 Session닫기
                {
                    Dispatcher.Invoke((Action)delegate { CloseSession(); });
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void MainCamera_DownloadReady(EOSDigital.API.Camera sender, DownloadInfo info)
        {
            try
            {
                string dir = MainWindow.PhotoPath;
                sender.DownloadFile(info, dir); //다운파일에 저장 폴더 값 넘기기
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
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
                Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #endregion

        #region///타이머///

        private void Timer_Tick(object sender, EventArgs e)
        {
            countdown--;
            if(MCheckNum == "66776677")
            {
                CallSerial();
            }
            if (MainWindow.checkrepair == 1)
            {
                timer.Stop();
                Source.Log.log.Info("Main Level 타이머 종료");
                timer.Tick -= new EventHandler(Timer_Tick);
                if (MainWindow.camnumber.ToString() == "1")
                {
                    APIHandler.CameraAdded -= APIHandler_CameraAdded;
                    ErrorHandler.SevereErrorHappened -= ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
                    ErrorHandler.NonSevereErrorHappened -= ErrorHandler_NonSevereErrorHappened; //에러 발생 시
                    if (MainCamera != null)
                    {
                        CloseSession();
                        Main.MainCamera.Dispose();
                        Main.MainCamera = null;
                        Main.APIHandler.Dispose();
                        Main.APIHandler = null;
                        Main.SetImageAction = null;
                    }
                }
                Uri repair = new Uri("View/Repair.xaml", UriKind.RelativeOrAbsolute);
                if (repair != null)
                {
                    timer = null;
                    Mainimg.Source = null;
                    Mainimg = null;
                    NavigationService.Navigate(repair);
                    MCheckNum = "";
                    Source.Log.log.Info("옵션 메뉴 이동");
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        private void CallSerial()
        {
            try
            {
                timer.Stop();
                Source.Log.log.Info("Main Level 타이머 종료");
                timer.Tick -= new EventHandler(Timer_Tick);
                if (MainWindow.camnumber.ToString() == "1")
                {
                    APIHandler.CameraAdded -= APIHandler_CameraAdded;
                    ErrorHandler.SevereErrorHappened -= ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
                    ErrorHandler.NonSevereErrorHappened -= ErrorHandler_NonSevereErrorHappened; //에러 발생 시
                    if (MainCamera != null)
                    {
                        CloseSession();
                        Main.MainCamera.Dispose();
                        Main.MainCamera = null;
                        Main.APIHandler.Dispose();
                        Main.APIHandler = null;
                        Main.SetImageAction = null;
                    }
                }
                Uri serial = new Uri("View/SerialCheck.xaml", UriKind.RelativeOrAbsolute);
                if (serial != null)
                {
                    timer = null;
                    Mainimg.Source = null;
                    Mainimg = null;
                    NavigationService.Navigate(serial);
                    MCheckNum = "";
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);

                Uri serial = new Uri("View/SerialCheck.xaml", UriKind.RelativeOrAbsolute);
                if (serial != null)
                {
                    timer = null;
                    Mainimg.Source = null;
                    Mainimg = null;
                    NavigationService.Navigate(serial);
                    MCheckNum = "";
                    Source.Log.log.Info("옵션 메뉴 이동");
                }
            }
        }
        #endregion

        private void Mainimg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Source.OneClick.One_Click())
                {
                    Mainimg.IsEnabled = false;
                    if (MainWindow.camnumber.ToString() == "1")
                    {
                        APIHandler.CameraAdded -= APIHandler_CameraAdded;
                        ErrorHandler.SevereErrorHappened -= ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
                        ErrorHandler.NonSevereErrorHappened -= ErrorHandler_NonSevereErrorHappened; //에러 발생 시
                        if (MainCamera != null)
                        {
                            CloseSession();
                        }
                    }
                    Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 버튼 클릭");
                    timer.Stop();
                    Source.Log.log.Info("타이머 종료");
                    timer.Tick -= new EventHandler(Timer_Tick);
                    timer = null;



                    if (MainCamera != null || MainWindow.Webcam != null)
                    {
                        if (MainWindow.inifoldername.Contains("mediagram"))
                        {
                            if (MainWindow.checkingtogether == "Use")
                            {
                                NavigationService.Navigate(new Uri("View/SelectTakePic.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else if (MainWindow.checkingtogether != "Use")
                            {
                                Onecut.View.SelectTakePic.checkversion = 1;
                                NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
                            }
                        }
                        else
                        {
                            if (MainWindow.tempoption == 1)
                            {
                                NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
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
                                    NavigationService.Navigate(new Uri("View/PageSelect.xaml", UriKind.RelativeOrAbsolute));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MainWindow.camnumber.ToString() == "1")
                        {
                            Main.MainCamera.Dispose();
                            Main.MainCamera = null;
                            Main.APIHandler.Dispose();
                            Main.APIHandler = null;
                            Main.SetImageAction2 = null;
                            Main.SetImageAction = null;

                            if (APIHandler == null)
                            {
                                APIHandler = new CanonAPI();
                            }
                            APIHandler.CameraAdded += APIHandler_CameraAdded;
                            ErrorHandler.SevereErrorHappened += ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
                            ErrorHandler.NonSevereErrorHappened += ErrorHandler_NonSevereErrorHappened; //에러 발생 시

                            if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                            {
                                SetImageAction2 = (TransformedBitmap img) => { bgbrush.ImageSource = img; }; // Live View에 그려지는 이미지 Action 
                            }
                            else
                            {
                                SetImageAction = (BitmapImage img) => { bgbrush.ImageSource = img; }; // Live View에 그려지는 이미지 Action 
                            }
                            SaveFolderBrowser.Description = "Save Image To...";
                            RefreshCamera(); //연결된 카메라 정보 수집
                            IsInit = true;
                            if (MainCamera == null)
                            {
                                OpenSession();
                            }

                            APIHandler.CameraAdded -= APIHandler_CameraAdded;
                            ErrorHandler.SevereErrorHappened -= ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
                            ErrorHandler.NonSevereErrorHappened -= ErrorHandler_NonSevereErrorHappened; //에러 발생 시
                            if (MainCamera != null)
                            {
                                CloseSession();
                            }

                            if (MainCamera != null || MainWindow.video != null)
                            {
                                if (MainWindow.inifoldername.Contains("mediagram"))
                                {
                                    if (MainWindow.checkingtogether == "Use")
                                    {
                                        NavigationService.Navigate(new Uri("View/SelectTakePic.xaml", UriKind.RelativeOrAbsolute));
                                    }
                                    else if (MainWindow.checkingtogether != "Use")
                                    {
                                        Onecut.View.SelectTakePic.checkversion = 1;
                                        NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
                                    }
                                }
                                else
                                {
                                    if (MainWindow.tempoption == 1)
                                    {
                                        NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
                                    }
                                    else
                                    {
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
                                            NavigationService.Navigate(new Uri("View/PageSelect.xaml", UriKind.RelativeOrAbsolute));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("카메라를 확인해주세요.");
                                Mainimg.IsEnabled = true;

                                timer = new DispatcherTimer();
                                timer.Interval = TimeSpan.FromSeconds(1);
                                timer.Tick += new EventHandler(Timer_Tick);
                                timer.Start();
                            }
                        }
                        else
                        {
                            MainWindow.video.Dispose();
                            MainWindow.Webcam.Dispose();
                            MainWindow.video = new OpenCvSharp.VideoCapture(0);
                            MainWindow.Webcam = CameraService.AvailableCameras[0];

                            if (MainCamera != null || MainWindow.Webcam != null)
                            {

                                if (MainWindow.tempoption == 1)
                                {
                                    NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
                                }
                                else
                                {
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
                                        NavigationService.Navigate(new Uri("View/PageSelect.xaml", UriKind.RelativeOrAbsolute));
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("카메라를 확인해주세요.");
                                Mainimg.IsEnabled = true;

                                timer = new DispatcherTimer();
                                timer.Interval = TimeSpan.FromSeconds(1);
                                timer.Tick += new EventHandler(Timer_Tick);
                                timer.Start();
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
                if (MainWindow.camnumber.ToString() == "1")
                {
                    Mainimg.IsEnabled = true;

                    if (APIHandler == null)
                    {
                        APIHandler = new CanonAPI();
                    }
                    APIHandler.CameraAdded += APIHandler_CameraAdded;
                    ErrorHandler.SevereErrorHappened += ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
                    ErrorHandler.NonSevereErrorHappened += ErrorHandler_NonSevereErrorHappened; //에러 발생 시

                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        SetImageAction2 = (TransformedBitmap img) => { bgbrush.ImageSource = img; }; // Live View에 그려지는 이미지 Action 
                    }
                    else
                    {
                        SetImageAction = (BitmapImage img) => { bgbrush.ImageSource = img; }; // Live View에 그려지는 이미지 Action 
                    }
                    SaveFolderBrowser.Description = "Save Image To...";
                    RefreshCamera(); //연결된 카메라 정보 수집
                    IsInit = true;
                    if (MainCamera == null)
                    {
                        OpenSession();
                    }

                    APIHandler.CameraAdded -= APIHandler_CameraAdded;
                    ErrorHandler.SevereErrorHappened -= ErrorHandler_SevereErrorHappened; //치명적 에러 발생 시 
                    ErrorHandler.NonSevereErrorHappened -= ErrorHandler_NonSevereErrorHappened; //에러 발생 시
                    if (MainCamera != null)
                    {
                        CloseSession();
                    }

                    if (MainCamera != null || MainWindow.video != null)
                    {

                        if (MainWindow.tempoption == 1)
                        {
                            NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
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
                                NavigationService.Navigate(new Uri("View/PageSelect.xaml", UriKind.RelativeOrAbsolute));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("카메라를 확인해주세요.");
                        Mainimg.IsEnabled = true;

                        timer = new DispatcherTimer();
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += new EventHandler(Timer_Tick);
                        timer.Start();
                    }
                }
                else
                {
                    try
                    {
                        MainWindow.video.Dispose();
                        MainWindow.Webcam.Dispose();
                        MainWindow.video = new OpenCvSharp.VideoCapture(0);
                        MainWindow.Webcam = CameraService.AvailableCameras[0];

                        if (MainCamera != null || MainWindow.Webcam != null)
                        {

                            if (MainWindow.tempoption == 1)
                            {
                                NavigationService.Navigate(new Uri("View/TempSelect.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
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
                                    NavigationService.Navigate(new Uri("View/PageSelect.xaml", UriKind.RelativeOrAbsolute));
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("카메라를 확인해주세요.");
                            Mainimg.IsEnabled = true;

                            timer = new DispatcherTimer();
                            timer.Interval = TimeSpan.FromSeconds(1);
                            timer.Tick += new EventHandler(Timer_Tick);
                            timer.Start();
                        }
                    }
                    catch (Exception webcamex)
                    {
                        MessageBox.Show("카메라 연결을 확인해주세요.");
                        Mainimg.IsEnabled = true;

                        timer = new DispatcherTimer();
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += new EventHandler(Timer_Tick);
                        timer.Start();
                        return;
                    }
                }
            }
        }

        private void btn1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MCheckNum = MCheckNum + "1";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void btn3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MCheckNum = MCheckNum + "2";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void btn4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MCheckNum = MCheckNum + "3";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void btn2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MCheckNum == "12321")
                {
                    Source.Log.log.Info("관리자 페이지로 이동");

                    timer.Stop();
                    Source.Log.log.Info("Main Level 타이머 종료");
                    timer.Tick -= new EventHandler(Timer_Tick);
                    if (MainWindow.camnumber.ToString() == "1")
                    {
                        if (MainCamera != null)
                        {
                            CloseSession();
                        }
                    }
                    Uri serial = new Uri("View/SerialCheck.xaml", UriKind.RelativeOrAbsolute);
                    if (serial != null)
                    {
                        timer = null;
                        Mainimg.Source = null;
                        Mainimg = null;
                        NavigationService.Navigate(serial);
                        MCheckNum = "";
                        Source.Log.log.Info("옵션 메뉴 이동");
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                else
                {
                    MCheckNum = "";
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
                Uri serial = new Uri("View/SerialCheck.xaml", UriKind.RelativeOrAbsolute);
                if (serial != null)
                {
                    timer = null;
                    Mainimg.Source = null;
                    Mainimg = null;
                    NavigationService.Navigate(serial);
                    MCheckNum = "";
                    Source.Log.log.Info("옵션 메뉴 이동");
                }
            }
        }

        #region 장치 이름 가져오기

        private void Searchdevice()
        {
            string query = "SELECT * FROM Win32_PnPEntity WHERE Description = 'Canon EOS 200D II'";

            // WMI 쿼리 실행
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection collection = searcher.Get();


        }

        #endregion
    }
}
