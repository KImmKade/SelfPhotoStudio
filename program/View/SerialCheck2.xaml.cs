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
using Npgsql;

namespace wpfTest.View
{
    /// <summary>
    /// SerialCheck2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SerialCheck2 : Page
    {

        System.Drawing.Image sample;
        System.Drawing.Image qrsample;

        int SellTempFocus;

        #region///시리얼 변수///

        private SerialPort port = Payment.port;

        #endregion

        public SerialCheck2()
        {
            Source.Log.log.Info("설정페이지 2번 진입");
            InitializeComponent();
        }

        #region///INI Import///

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                sample = System.Drawing.Image.FromFile(MainWindow.TempPath + @"\Sample.jpg");
                qrsample = System.Drawing.Image.FromFile(MainWindow.TempPath + @"\QRSample.PNG");

                if (MainWindow.SorR == "R")
                {
                    RentalBtn.IsChecked = true;
                }
                else
                {
                    SellBtn.IsChecked = true;
                }

                if (MainWindow.SorR == "R")
                {
                    // pic1 좌표

                    pic1startxlocation.Text = MainWindow.pic1startx.ToString();
                    pic1startylocation.Text = MainWindow.pic1starty.ToString();
                    pic1widthlengh.Text = MainWindow.pic1width.ToString();
                    pic1heightlengh.Text = MainWindow.pic1height.ToString();

                    // pic2 좌표

                    pic2startxlocation.Text = MainWindow.pic2startx.ToString();
                    pic2startylocation.Text = MainWindow.pic2starty.ToString();
                    pic2widthlengh.Text = MainWindow.pic2width.ToString();
                    pic2heightlengh.Text = MainWindow.pic2height.ToString();

                    // pic3 좌표

                    pic3startxlocation.Text = MainWindow.pic3startx.ToString();
                    pic3startylocation.Text = MainWindow.pic3starty.ToString();
                    pic3widthlengh.Text = MainWindow.pic3width.ToString();
                    pic3heightlengh.Text = MainWindow.pic3height.ToString();

                    // pic4 좌표

                    pic4startxlocation.Text = MainWindow.pic4startx.ToString();
                    pic4startylocation.Text = MainWindow.pic4starty.ToString();
                    pic4widthlengh.Text = MainWindow.pic4width.ToString();
                    pic4heightlengh.Text = MainWindow.pic4height.ToString();

                    // pic5 좌표

                    pic5startxlocation.Text = MainWindow.pic5startx.ToString();
                    pic5startylocation.Text = MainWindow.pic5starty.ToString();
                    pic5widthlengh.Text = MainWindow.pic5width.ToString();
                    pic5heightlengh.Text = MainWindow.pic5height.ToString();

                    // pic6 좌표

                    pic6startxlocation.Text = MainWindow.pic6startx.ToString();
                    pic6startylocation.Text = MainWindow.pic6starty.ToString();
                    pic6widthlengh.Text = MainWindow.pic6width.ToString();
                    pic6heightlengh.Text = MainWindow.pic6height.ToString();

                    qrstartxlocation.Text = MainWindow.qrstartx.ToString();
                    qrstartylocation.Text = MainWindow.qrstarty.ToString();
                    qrwidthlengh.Text = MainWindow.qrwidth.ToString();
                    qrheightlengh.Text = MainWindow.qrheight.ToString();

                }
                else
                {
                    pic1startxlocation.Text = MainWindow.temp1pic1startx.ToString();
                    pic1startylocation.Text = MainWindow.temp1pic1starty.ToString();
                    pic1widthlengh.Text = MainWindow.temp1pic1width.ToString();
                    pic1heightlengh.Text = MainWindow.temp1pic1height.ToString();

                    pic2startxlocation.Text = MainWindow.temp1pic2startx.ToString();
                    pic2startylocation.Text = MainWindow.temp1pic2starty.ToString();
                    pic2widthlengh.Text = MainWindow.temp1pic2width.ToString();
                    pic2heightlengh.Text = MainWindow.temp1pic2height.ToString();

                    pic3startxlocation.Text = MainWindow.temp1pic3startx.ToString();
                    pic3startylocation.Text = MainWindow.temp1pic3starty.ToString();
                    pic3widthlengh.Text = MainWindow.temp1pic3width.ToString();
                    pic3heightlengh.Text = MainWindow.temp1pic3height.ToString();

                    pic4startxlocation.Text = MainWindow.temp1pic4startx.ToString();
                    pic4startylocation.Text = MainWindow.temp1pic4starty.ToString();
                    pic4widthlengh.Text = MainWindow.temp1pic4width.ToString();
                    pic4heightlengh.Text = MainWindow.temp1pic4height.ToString();

                    // pic5 좌표

                    pic5startxlocation.Text = MainWindow.pic5startx.ToString();
                    pic5startylocation.Text = MainWindow.pic5starty.ToString();
                    pic5widthlengh.Text = MainWindow.pic5width.ToString();
                    pic5heightlengh.Text = MainWindow.pic5height.ToString();

                    // pic6 좌표

                    pic6startxlocation.Text = MainWindow.pic6startx.ToString();
                    pic6startylocation.Text = MainWindow.pic6starty.ToString();
                    pic6widthlengh.Text = MainWindow.pic6width.ToString();
                    pic6heightlengh.Text = MainWindow.pic6height.ToString();
                }

                switch (MainWindow.checkvideo)
                {
                    case "0":
                        Novideo.IsChecked = true;
                        break;
                    case "1":
                        UseVideo.IsChecked = true;
                        break;
                }

                switch (MainWindow.coupon)
                {
                    case "0":
                        Nocoupon.IsChecked = true;
                        break;
                    case "1":
                        Usecoupon.IsChecked = true;
                        break;
                }

                switch (MainWindow.picratio)
                {
                    case 1:
                        pic32.IsChecked = true;
                        break;
                    case 2:
                        pic11.IsChecked = true;
                        break;
                    case 3:
                        pic23.IsChecked = true;
                        break;
                }

                switch (MainWindow.printerratio)
                {
                    case 1:
                        ptinrationame.SelectedItem = ptinrationame.Items[0];
                        break;
                    case 2:
                        ptinrationame.SelectedItem = ptinrationame.Items[1];
                        break;
                    case 3:
                        ptinrationame.SelectedItem = ptinrationame.Items[2];
                        break;
                }

                switch (MainWindow.pagenum.ToString())
                {
                    case "1":
                        pagecobox.SelectedItem = pagecobox.Items[0];
                        break;
                    case "2":
                        pagecobox.SelectedItem = pagecobox.Items[1];
                        break;
                    case "3":
                        pagecobox.SelectedItem = pagecobox.Items[2];
                        break;
                    case "4":
                        pagecobox.SelectedItem = pagecobox.Items[3];
                        break;
                    case "5":
                        pagecobox.SelectedItem = pagecobox.Items[4];
                        break;
                    case "6":
                        pagecobox.SelectedItem = pagecobox.Items[5];
                        break;
                    case "7":
                        pagecobox.SelectedItem = pagecobox.Items[6];
                        break;
                    case "8":
                        pagecobox.SelectedItem = pagecobox.Items[7];
                        break;
                    case "9":
                        pagecobox.SelectedItem = pagecobox.Items[8];
                        break;
                    case "10":
                        pagecobox.SelectedItem = pagecobox.Items[9];
                        break;
                }

                switch (Convert.ToInt32(MainWindow.tempnumber.ToString()))
                {
                    case 1:
                        tempcountcb.SelectedItem = tempcountcb.Items[0];
                        break;
                    case 2:
                        tempcountcb.SelectedItem = tempcountcb.Items[1];
                        break;
                    case 3:
                        tempcountcb.SelectedItem = tempcountcb.Items[2];
                        break;
                    case 4:
                        tempcountcb.SelectedItem = tempcountcb.Items[3];
                        break;
                    case 5:
                        tempcountcb.SelectedItem = tempcountcb.Items[4];
                        break;
                    case 6:
                        tempcountcb.SelectedItem = tempcountcb.Items[5];
                        break;
                }

                //카카오톡 여부

                if (MainWindow.checkkakao == "0")
                {
                    Use.IsChecked = true;
                }
                else
                {
                    NotUse.IsChecked = true;
                }
                if (MainWindow.useqr.ToString() == "0")
                {
                    QRNotUse.IsChecked = true;
                }
                else
                {
                    QRUse.IsChecked = true;
                }

                // 템플릿 선택 사용 유무

                if (MainWindow.tempoption == 1)
                {
                    tempUse.IsChecked = true;
                }
                else
                {
                    tempNotUse.IsChecked = true;
                }

                switch(MainWindow.photonum)
                {
                    case 1:
                        selectcut.SelectedItem = selectcut.Items[0];
                        break;
                    case 2:
                        selectcut.SelectedItem = selectcut.Items[1];
                        break;
                    case 3:
                        selectcut.SelectedItem = selectcut.Items[2];
                        break;
                    case 4:
                        selectcut.SelectedItem = selectcut.Items[3];
                        break;
                    case 5:
                        selectcut.SelectedItem = selectcut.Items[4];
                        break;
                    case 6:
                        selectcut.SelectedItem = selectcut.Items[5];
                        break;
                }

                fontcolortb.Text = MainWindow.textcolor;
                fontcolorcanvas.Background = MainWindow.textbrush;
                kakaofontcolor.Text = MainWindow.kakaotextcolor;

                switch (MainWindow.qrpageuse)
                {
                    case 0:
                        QRpageNotUse.IsChecked = true;
                        break;
                    case 1:
                        QRpageUse.IsChecked = true;
                        break;
                }

                switch (MainWindow.checkcolor)
                {
                    case "twice":
                        coloruse.SelectedItem = coloruse.Items[0];
                        break;
                    case "color":
                        coloruse.SelectedItem = coloruse.Items[1];
                        break;
                    case "black":
                        coloruse.SelectedItem = coloruse.Items[2];
                        break;
                    case "none":
                        coloruse.SelectedItem = coloruse.Items[3];
                        break;
                }

                // 날짜 선택

                if (MainWindow.datetext == 1)
                {
                    datefontcolortb.Text = MainWindow.datetextcolor;
                    DateonTempon.IsChecked = true;
                }
                else
                {
                    datefontcolortb.Text = MainWindow.datetextcolor;
                    DateonTempoff.IsChecked = true;
                }

            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pagecobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (pagecobox.SelectedIndex < 0)
                {
                    return;
                }
                switch (pagecobox.SelectedIndex)
                {
                    case 0:
                        MainWindow.pagenum = "1";
                        break;
                    case 1:
                        MainWindow.pagenum = "2";
                        break;
                    case 2:
                        MainWindow.pagenum = "3";
                        break;
                    case 3:
                        MainWindow.pagenum = "4";
                        break;
                    case 4:
                        MainWindow.pagenum = "5";
                        break;
                    case 5:
                        MainWindow.pagenum = "6";
                        break;
                    case 6:
                        MainWindow.pagenum = "7";
                        break;
                    case 7:
                        MainWindow.pagenum = "8";
                        break;
                    case 8:
                        MainWindow.pagenum = "9";
                        break;
                    case 9:
                        MainWindow.pagenum = "10";
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Use_Checked(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 카카오톡 사용  버튼 체크");
            try
            {
                MainWindow.checkkakao = "0";

                MainWindow.qroption = 0;
                QRNotUse.IsChecked = true;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void NotUse_Checked(object sender, RoutedEventArgs e)
        {
            Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 카카오톡 미사용 버튼 체크");
            try
            {
                MainWindow.checkkakao = "1";

                qronoff.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.Adminlogin = 0;

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

                MainWindow.inicoupon.Clear();
                MainWindow.inicoupon.Append(MainWindow.coupon);
                WritePrivateProfileString("Setting", "coupon", MainWindow.coupon, MainWindow.iniPath);

                //카메라 셋팅
                WritePrivateProfileString("Setting", "CamVersion", MainWindow.camnumber.ToString(), MainWindow.iniPath);

                //타이머 사용여부
                MainWindow.Timer.Clear();
                MainWindow.Timer.Append(MainWindow.checktimeruse);
                WritePrivateProfileString("Setting", "TimerUse", MainWindow.checktimeruse, MainWindow.iniPath);

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

                if (MainWindow.cardnum == "기본값")
                {
                    MainWindow.inicardnum.Clear();
                    MainWindow.inicardnum.Append("8C89990196");
                    WritePrivateProfileString("Setting", "cardnum", "8C89990196", MainWindow.iniPath);
                    MainWindow.cardnum = "8C89990196";
                }
                else if (MainWindow.cardnum ==  "")
                {
                    MainWindow.inicardnum.Clear();
                    MainWindow.inicardnum.Append("8C89990196");
                    WritePrivateProfileString("Setting", "cardnum", "8C89990196", MainWindow.iniPath);
                    MainWindow.cardnum = "8C89990196";
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

                if (MainWindow.CheckInternetConnection())
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

        private void ToMain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 메인으로 동작");

                if (Main.MainCamera?.SessionOpen == true)
                {
                    if (Main.MainCamera?.IsLiveViewOn == true)
                    {
                        Main.MainCamera.StopLiveView();
                        Source.Log.log.Info("Canon 카메라 종료");
                    }
                    cameranull();
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
                    MainWindow.iniCheckVideo.ToString() != MainWindow.checkvideo || MainWindow.kakaotextcolor != MainWindow.inikakaotextcolor.ToString() || MainWindow.autologin != MainWindow.iniautologin.ToString() || MainWindow.initemp2fee.ToString() != MainWindow.temp2fee || MainWindow.temp3fee != MainWindow.initemp3fee.ToString() ||
                    MainWindow.coupon != MainWindow.inicoupon.ToString() || MainWindow.couponkind != MainWindow.inicouponkind.ToString() || MainWindow.checkcolor != MainWindow.inicheckcolor.ToString() ||
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

        private void BackPageBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 이전 버튼 동작");

                NavigationService.Navigate(new Uri("View/SerialCheck.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        #region /// camera dispose ///

        private void cameranull()
        {
            Main.MainCamera.Dispose();
            Main.MainCamera = null;
            Main.APIHandler.Dispose();
            Main.APIHandler = null;
            Main.SetImageAction = null;
        }

        #endregion

        private void QRUse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - qr사용 클릭");

                MainWindow.qroption = 1;

                QRgroupbox.IsEnabled = true;
                qrpageonoff.IsEnabled = true;

                videoGB.IsEnabled = true;

                switch (MainWindow.printerratio)
                {
                    case 1:
                        using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                        {
                            MainWindow.d.DrawImage(MainWindow.temp1_1, 0, 0, 600, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.d.DrawImage(MainWindow.temp1_1Front, 0, 0, 600, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.d.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.d.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.canvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 200;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 2:
                        using (MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2))
                        {
                            MainWindow.g.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.g.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.g.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.g.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus2.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 400;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 3:
                        using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                        {
                            MainWindow.r.DrawImage(MainWindow.temp1_1, 0, 0, 1800, 1200);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.r.DrawImage(MainWindow.temp1_1Front, 0, 0, 1800, 1200);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.r.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.r.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 600;
                            bi.DecodePixelHeight = 400;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void QRNotUse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - qr미사용 클릭");

                MainWindow.qroption = 0;

                QRgroupbox.IsEnabled = false;
                QRpageNotUse.IsChecked = true;
                qrpageonoff.IsEnabled = false;

                switch (MainWindow.printerratio)
                {
                    case 1:
                        using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                        {
                            MainWindow.d.DrawImage(MainWindow.temp1_1, 0, 0, 600, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.d.DrawImage(MainWindow.temp1_1Front, 0, 0, 600, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.d.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.d.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.canvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 200;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 2:
                        using (MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2))
                        {
                            MainWindow.g.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.g.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.g.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.g.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus2.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 400;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 3:
                        using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                        {
                            MainWindow.r.DrawImage(MainWindow.temp1_1, 0, 0, 1800, 1200);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.r.DrawImage(MainWindow.temp1_1Front, 0, 0, 1800, 1200);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.r.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.r.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 600;
                            bi.DecodePixelHeight = 400;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void tempUse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 템플릿 선택 사용 클릭");

                MainWindow.tempoption = 1;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void tempNotUse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 템플릿 선택 미사용 클릭");

                MainWindow.tempoption = 0;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void tempcountcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                switch (tempcountcb.SelectedIndex)
                {
                    case 0:
                        MainWindow.optiontempnum = 1;
                        break;
                    case 1:
                        MainWindow.optiontempnum = 2;
                        break;
                    case 2:
                        MainWindow.optiontempnum = 3;
                        break;
                    case 3:
                        MainWindow.optiontempnum = 4;
                        break;
                    case 4:
                        MainWindow.optiontempnum = 5;
                        break;
                    case 5:
                        MainWindow.optiontempnum = 6;
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic32_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.picratio = 1;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic11_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.picratio = 2;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic23_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.picratio = 3;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void priewviewbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                switch (MainWindow.printerratio)
                {
                    case 1:
                        using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                        {
                            MainWindow.d.DrawImage(MainWindow.temp1_1, 0, 0, 600, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                                case 5:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic5startx, MainWindow.pic5starty, MainWindow.pic5width, MainWindow.pic5height);
                                    break;
                                case 6:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic5startx, MainWindow.pic5starty, MainWindow.pic5width, MainWindow.pic5height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic6startx, MainWindow.pic6starty, MainWindow.pic6width, MainWindow.pic6height);
                                    break;
                            }
                            MainWindow.d.DrawImage(MainWindow.temp1_1Front, 0, 0, 600, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.d.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }

                            if (MainWindow.datetext == 1)
                            {
                                string text = DateTime.Today.ToString("yyyy.MM.dd");
                                Font font = new Font("Arial", 30);
                                string colorcode = MainWindow.datetextcolor;
                                System.Drawing.Color color = ColorTranslator.FromHtml(colorcode);
                                System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));
                                MainWindow.d.DrawString(text, font, brush, 200, 1750);
                            }
                        }
                        MainWindow.d.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.canvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 200;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 2:
                        using (MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2))
                        {
                            MainWindow.g.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                                case 5:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic5startx, MainWindow.pic5starty, MainWindow.pic5width, MainWindow.pic5height);
                                    break;
                                case 6:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic5startx, MainWindow.pic5starty, MainWindow.pic5width, MainWindow.pic5height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic6startx, MainWindow.pic6starty, MainWindow.pic6width, MainWindow.pic6height);
                                    break;
                            }
                            MainWindow.g.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.g.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }

                            if (MainWindow.datetext == 1)
                            {
                                string text = DateTime.Today.ToString("yyyy.MM.dd");
                                Font font = new Font("Arial", 30);
                                string colorcode = MainWindow.datetextcolor;
                                System.Drawing.Color color = ColorTranslator.FromHtml(colorcode);
                                System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));
                                MainWindow.g.DrawString(text, font, brush, 500, 1750);
                            }
                        }
                        MainWindow.g.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus2.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 400;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 3:
                        using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                        {
                            MainWindow.r.DrawImage(MainWindow.temp1_1, 0, 0, 1800, 1200);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                                case 5:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic5startx, MainWindow.pic5starty, MainWindow.pic5width, MainWindow.pic5height);
                                    break;
                                case 6:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic5startx, MainWindow.pic5starty, MainWindow.pic5width, MainWindow.pic5height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic6startx, MainWindow.pic6starty, MainWindow.pic6width, MainWindow.pic6height);
                                    break;
                            }
                            MainWindow.r.DrawImage(MainWindow.temp1_1Front, 0, 0, 1800, 1200);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.r.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }

                            if (MainWindow.datetext == 1)
                            {
                                string text = DateTime.Today.ToString("yyyy.MM.dd");
                                Font font = new Font("Arial", 30);
                                string colorcode = MainWindow.datetextcolor;
                                System.Drawing.Color color = ColorTranslator.FromHtml(colorcode);
                                System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));
                                MainWindow.r.DrawString(text, font, brush, 800, 1150);
                            }
                        }
                        MainWindow.r.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 600;
                            bi.DecodePixelHeight = 400;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic1startxlocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic1startx = Convert.ToInt32(pic1startxlocation.Text);
                }
                else
                {
                    switch(SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic1startx = Convert.ToInt32(pic1startxlocation.Text);
                            break;
                        case 2:
                            MainWindow.temp2pic1startx = Convert.ToInt32(pic1startxlocation.Text);
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic1startylocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic1starty = Convert.ToInt32(pic1startylocation.Text);
                }
                else
                {
                    switch(SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic1starty = Convert.ToInt32(pic1startylocation.Text);
                            break;
                        case 2:
                            MainWindow.temp2pic1starty = Convert.ToInt32(pic1startylocation.Text);
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic1widthlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic1width = Convert.ToInt32(pic1widthlengh.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic1width = Convert.ToInt32(pic1widthlengh.Text);
                            break;
                        case 2:
                            MainWindow.temp2pic1width = Convert.ToInt32(pic1widthlengh.Text);
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic1heightlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic1height = Convert.ToInt32(pic1heightlengh.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic1height = Convert.ToInt32(pic1heightlengh.Text);
                            break;
                        case 2:
                            MainWindow.temp2pic1height = Convert.ToInt32(pic1heightlengh.Text);
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic2startxlocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic2startx = Convert.ToInt32(pic2startxlocation.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic2startx = Convert.ToInt32(pic2startxlocation.Text);
                            break;
                        case 2:
                            MainWindow.temp2pic2startx = Convert.ToInt32(pic2startxlocation.Text);
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic2startylocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic2starty = Convert.ToInt32(pic2startylocation.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic2starty = Convert.ToInt32(pic2startylocation.Text);
                            break;
                        case 2:
                            MainWindow.temp2pic2starty = Convert.ToInt32(pic2startylocation.Text);
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic2widthlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic2width = Convert.ToInt32(pic2widthlengh.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic2width = Convert.ToInt32(pic2widthlengh.Text);
                            break;
                        case 2:
                            MainWindow.temp2pic2width = Convert.ToInt32(pic2widthlengh.Text);
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic2heightlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic2height = Convert.ToInt32(pic2heightlengh.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic2height = Convert.ToInt32(pic2heightlengh.Text);
                            break;
                        case 2:
                            MainWindow.temp2pic2height = Convert.ToInt32(pic2heightlengh.Text);
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic3startxlocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic3startx = Convert.ToInt32(pic3startxlocation.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic3startx = Convert.ToInt32(pic3startxlocation.Text);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic3startylocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic3starty = Convert.ToInt32(pic3startylocation.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic3starty = Convert.ToInt32(pic3startylocation.Text);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic3widthlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic3width = Convert.ToInt32(pic3widthlengh.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic3width = Convert.ToInt32(pic3widthlengh.Text);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic3heightlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic3height = Convert.ToInt32(pic3heightlengh.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic3height = Convert.ToInt32(pic3heightlengh.Text);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic4startxlocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic4startx = Convert.ToInt32(pic4startxlocation.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic4startx = Convert.ToInt32(pic4startxlocation.Text);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic4startylocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic4starty = Convert.ToInt32(pic4startylocation.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic4starty = Convert.ToInt32(pic4startylocation.Text);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic4widthlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic4width = Convert.ToInt32(pic4widthlengh.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic4width = Convert.ToInt32(pic4widthlengh.Text);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic4heightlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.SorR == "R")
                {
                    MainWindow.pic4height = Convert.ToInt32(pic4heightlengh.Text);
                }
                else
                {
                    switch (SellTempFocus)
                    {
                        case 1:
                            MainWindow.temp1pic4height = Convert.ToInt32(pic4heightlengh.Text);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                pic1startxlocation.Text = MainWindow.inipic1startx.ToString();
                pic1startylocation.Text = MainWindow.inipic1starty.ToString();
                pic1widthlengh.Text = MainWindow.inipic1width.ToString();
                pic1heightlengh.Text = MainWindow.inipic1height.ToString();

                pic2startxlocation.Text = MainWindow.inipic2startx.ToString();
                pic2startylocation.Text = MainWindow.inipic2starty.ToString();
                pic2widthlengh.Text = MainWindow.inipic2width.ToString();
                pic2heightlengh.Text = MainWindow.inipic2height.ToString();

                pic3startxlocation.Text = MainWindow.inipic3startx.ToString();
                pic3startylocation.Text = MainWindow.inipic3starty.ToString();
                pic3widthlengh.Text = MainWindow.inipic3width.ToString();
                pic3heightlengh.Text = MainWindow.inipic3height.ToString();

                pic4startxlocation.Text = MainWindow.inipic4startx.ToString();
                pic4startylocation.Text = MainWindow.inipic4starty.ToString();
                pic4widthlengh.Text = MainWindow.inipic4width.ToString();
                pic4heightlengh.Text = MainWindow.inipic4height.ToString();

                // pic5 좌표

                pic5startxlocation.Text = MainWindow.pic5startx.ToString();
                pic5startylocation.Text = MainWindow.pic5starty.ToString();
                pic5widthlengh.Text = MainWindow.pic5width.ToString();
                pic5heightlengh.Text = MainWindow.pic5height.ToString();

                // pic6 좌표

                pic6startxlocation.Text = MainWindow.pic6startx.ToString();
                pic6startylocation.Text = MainWindow.pic6starty.ToString();
                pic6widthlengh.Text = MainWindow.pic6width.ToString();
                pic6heightlengh.Text = MainWindow.pic6height.ToString();

                qrstartxlocation.Text = MainWindow.iniqrstartx.ToString();
                qrstartylocation.Text = MainWindow.iniqrstarty.ToString();
                qrwidthlengh.Text = MainWindow.iniqrwidth.ToString();
                qrheightlengh.Text = MainWindow.iniqrheight.ToString();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void num1_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.photonum = 1;
                switch (MainWindow.printerratio)
                {
                    case 1:
                        using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                        {
                            MainWindow.d.DrawImage(MainWindow.temp1_1, 0, 0, 600, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.d.DrawImage(MainWindow.temp1_1Front, 0, 0, 600, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.d.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.d.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.canvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 200;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 2:
                        using (MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2))
                        {
                            MainWindow.g.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.g.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.g.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.g.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus2.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 400;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 3:
                        using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                        {
                            MainWindow.r.DrawImage(MainWindow.temp1_1, 0, 0, 1800, 1200);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.r.DrawImage(MainWindow.temp1_1Front, 0, 0, 1800, 1200);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.r.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.r.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 600;
                            bi.DecodePixelHeight = 400;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void num2_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.photonum = 2;
                
                switch (MainWindow.printerratio)
                {
                    case 1:
                        using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                        {
                            MainWindow.d.DrawImage(MainWindow.temp1_1, 0, 0, 600, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.d.DrawImage(MainWindow.temp1_1Front, 0, 0, 600, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.d.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.d.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.canvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 200;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 2:
                        using (MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2))
                        {
                            MainWindow.g.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.g.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.g.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.g.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus2.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 400;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 3:
                        using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                        {
                            MainWindow.r.DrawImage(MainWindow.temp1_1, 0, 0, 1800, 1200);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.r.DrawImage(MainWindow.temp1_1Front, 0, 0, 1800, 1200);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.r.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.r.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 600;
                            bi.DecodePixelHeight = 400;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void num3_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.photonum = 3;
                switch (MainWindow.printerratio)
                {
                    case 1:
                        using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                        {
                            MainWindow.d.DrawImage(MainWindow.temp1_1, 0, 0, 600, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.d.DrawImage(MainWindow.temp1_1Front, 0, 0, 600, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.d.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.d.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.canvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 200;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 2:
                        using (MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2))
                        {
                            MainWindow.g.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.g.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.g.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.g.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus2.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 400;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 3:
                        using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                        {
                            MainWindow.r.DrawImage(MainWindow.temp1_1, 0, 0, 1800, 1200);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.r.DrawImage(MainWindow.temp1_1Front, 0, 0, 1800, 1200);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.r.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.r.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 600;
                            bi.DecodePixelHeight = 400;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void num4_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.photonum = 4;

                switch (MainWindow.printerratio)
                {
                    case 1:
                        using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                        {
                            MainWindow.d.DrawImage(MainWindow.temp1_1, 0, 0, 600, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.d.DrawImage(MainWindow.temp1_1Front, 0, 0, 600, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.d.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.d.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.canvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 200;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 2:
                        using (MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2))
                        {
                            MainWindow.g.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.g.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.g.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.g.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus2.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 400;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 3:
                        using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                        {
                            MainWindow.r.DrawImage(MainWindow.temp1_1, 0, 0, 1800, 1200);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.r.DrawImage(MainWindow.temp1_1Front, 0, 0, 1800, 1200);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.r.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.r.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 600;
                            bi.DecodePixelHeight = 400;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void qrstartxlocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.qrstartx = Convert.ToInt32(qrstartxlocation.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void qrstartylocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.qrstarty = Convert.ToInt32(qrstartylocation.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void qrwidthlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.qrwidth = Convert.ToInt32(qrwidthlengh.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void qrheightlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.qrheight = Convert.ToInt32(qrheightlengh.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void fontcolortb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.textcolor = fontcolortb.Text;

                string colorcode = fontcolortb.Text;
                System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(colorcode);
                System.Windows.Media.Brush brush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));

                fontcolorcanvas.Background = brush;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void QRpageUse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.qrpageuse = 1;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void QRpageNotUse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.qrpageuse = 0;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void DateonTempon_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.datetext = 1;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void DateonTempoff_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.datetext = 0;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void datefontcolortb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.datetextcolor = datefontcolortb.Text;

                string colorcode = datefontcolortb.Text;
                System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(colorcode);
                System.Windows.Media.Brush brush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));

                datefontcolorcanvas.Background = brush;
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

        private void RentalBtn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.SorR = "R";

                Temp1Btn.IsChecked = false;
                Temp2Btn.IsChecked = false;
                Temp3Btn.IsChecked = false;

                SellTempSettingGB.IsEnabled = false;
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

                Temp1Btn.IsChecked = true;

                SellTempSettingGB.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Temp1Btn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                SellTempFocus = 1;

                ptinrationame.SelectedItem = ptinrationame.Items[0];

                pic1startxlocation.Text = MainWindow.temp1pic1startx.ToString();
                pic1startylocation.Text = MainWindow.temp1pic1starty.ToString();
                pic1widthlengh.Text = MainWindow.temp1pic1width.ToString();
                pic1heightlengh.Text = MainWindow.temp1pic1height.ToString();

                pic2startxlocation.Text = MainWindow.temp1pic2startx.ToString();
                pic2startylocation.Text = MainWindow.temp1pic2starty.ToString();
                pic2widthlengh.Text = MainWindow.temp1pic2width.ToString();
                pic2heightlengh.Text = MainWindow.temp1pic2height.ToString();

                pic3startxlocation.Text = MainWindow.temp1pic3startx.ToString();
                pic3startylocation.Text = MainWindow.temp1pic3starty.ToString();
                pic3widthlengh.Text = MainWindow.temp1pic3width.ToString();
                pic3heightlengh.Text = MainWindow.temp1pic3height.ToString();

                pic4startxlocation.Text = MainWindow.temp1pic4startx.ToString();
                pic4startylocation.Text = MainWindow.temp1pic4starty.ToString();
                pic4widthlengh.Text = MainWindow.temp1pic4width.ToString();
                pic4heightlengh.Text = MainWindow.temp1pic4height.ToString();

                pic1settingGB.IsEnabled = true;
                pic2settingGB.IsEnabled = true;
                pic3settingGB.IsEnabled = true;
                pic4settingGB.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Temp2Btn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                SellTempFocus = 2;

                ptinrationame.SelectedItem = ptinrationame.Items[1];

                pic1startxlocation.Text = MainWindow.temp2pic1startx.ToString();
                pic1startylocation.Text = MainWindow.temp2pic1starty.ToString();
                pic1widthlengh.Text = MainWindow.temp2pic1width.ToString();
                pic1heightlengh.Text = MainWindow.temp2pic1height.ToString();

                pic2startxlocation.Text = MainWindow.temp2pic2startx.ToString();
                pic2startylocation.Text = MainWindow.temp2pic2starty.ToString();
                pic2widthlengh.Text = MainWindow.temp2pic2width.ToString();
                pic2heightlengh.Text = MainWindow.temp2pic2height.ToString();

                pic1settingGB.IsEnabled = true;
                pic2settingGB.IsEnabled = true;
                pic3settingGB.IsEnabled = false;
                pic4settingGB.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Temp3Btn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                SellTempFocus = 3;

                ptinrationame.SelectedItem = ptinrationame.Items[1];
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void ptinrationame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (ptinrationame.SelectedIndex < 0)
                {
                    return;
                }
                switch (ptinrationame.SelectedIndex)
                {
                    case 0:

                        if (MainWindow.SorR == "R")
                        {
                            MainWindow.printerratio = 1;
                        }

                        priewviewimg.Width = 200;
                        priewviewimg.Height = 600;

                        if (priewviewimg.Source != null)
                        {
                            priewviewimg.Source = null;
                        }

                        using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                        {
                            MainWindow.d.DrawImage(MainWindow.temp1_1, 0, 0, 600, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.d.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.d.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.d.DrawImage(MainWindow.temp1_1Front, 0, 0, 600, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.d.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.d.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.canvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 200;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 1:
                        if (MainWindow.SorR == "R")
                        {
                            MainWindow.printerratio = 2;
                        }
                        priewviewimg.Width = 400;
                        priewviewimg.Height = 600;

                        if (priewviewimg.Source != null)
                        {
                            priewviewimg.Source = null;
                        }

                        using (MainWindow.g = Graphics.FromImage(MainWindow.bigcanvus2))
                        {
                            MainWindow.g.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.g.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.g.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.g.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.g.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.g.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus2.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 400;
                            bi.DecodePixelHeight = 600;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                    case 2:
                        if (MainWindow.SorR == "R")
                        {
                            MainWindow.printerratio = 3;
                        }
                        priewviewimg.Width = 600;
                        priewviewimg.Height = 400;

                        if (priewviewimg.Source != null)
                        {
                            priewviewimg.Source = null;
                        }

                        using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                        {
                            MainWindow.r.DrawImage(MainWindow.temp1_1, 0, 0, 1800, 1200);
                            switch (MainWindow.photonum)
                            {
                                case 1:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    break;
                                case 2:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    break;
                                case 3:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    break;
                                case 4:
                                    MainWindow.r.DrawImage(sample, MainWindow.pic1startx, MainWindow.pic1starty, MainWindow.pic1width, MainWindow.pic1height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic2startx, MainWindow.pic2starty, MainWindow.pic2width, MainWindow.pic2height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic3startx, MainWindow.pic3starty, MainWindow.pic3width, MainWindow.pic3height);
                                    MainWindow.r.DrawImage(sample, MainWindow.pic4startx, MainWindow.pic4starty, MainWindow.pic4width, MainWindow.pic4height);
                                    break;
                            }
                            MainWindow.r.DrawImage(MainWindow.temp1_1Front, 0, 0, 1800, 1200);

                            if (MainWindow.qroption == 1)
                            {
                                MainWindow.r.DrawImage(qrsample, MainWindow.qrstartx, MainWindow.qrstarty, MainWindow.qrwidth, MainWindow.qrheight);
                            }
                        }
                        MainWindow.r.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.bigcanvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 600;
                            bi.DecodePixelHeight = 400;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Novideo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.checkvideo = "0";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void UseVideo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.checkvideo = "1";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void kakaofontcolor_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.kakaotextcolor = kakaofontcolor.Text;

                string colorcode = kakaofontcolor.Text;
                System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(colorcode);
                System.Windows.Media.Brush brush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));

                kakaofontcolorcanvas.Background = brush;
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Nocoupon_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.coupon = "0";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Usecoupon_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.coupon = "1";
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void coloruse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (coloruse.SelectedIndex)
                {
                    case 0:
                        MainWindow.checkcolor = "twice";
                        break;
                    case 1:
                        MainWindow.checkcolor = "color";
                        break;
                    case 2:
                        MainWindow.checkcolor = "black";
                        break;
                    case 3:
                        MainWindow.checkcolor = "none";
                        break;
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void selectcut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectcut.SelectedIndex < 0)
            {
                return;
            }
            switch (selectcut.SelectedIndex)
            {
                case 0:
                    MainWindow.photonum = 1;
                    break;
                case 1:
                    MainWindow.photonum = 2;
                    break;
                case 2:
                    MainWindow.photonum = 3;
                    break;
                case 3:
                    MainWindow.photonum = 4;
                    break;
                case 4:
                    MainWindow.photonum = 5;
                    break;
                case 5:
                    MainWindow.photonum = 6;
                    break;
            }
        }

        private void pic5startxlocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.pic5startx = Convert.ToInt32(pic5startxlocation.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic5widthlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.pic5width = Convert.ToInt32(pic5widthlengh.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic5startylocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.pic5starty = Convert.ToInt32(pic5startylocation.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic5heightlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.pic5height = Convert.ToInt32(pic5heightlengh.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic6startxlocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.pic6startx = Convert.ToInt32(pic6startxlocation.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic6widthlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.pic6width = Convert.ToInt32(pic6widthlengh.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic6startylocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.pic6starty = Convert.ToInt32(pic6startylocation.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void pic6heightlengh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                MainWindow.pic6height = Convert.ToInt32(pic6heightlengh.Text);
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
