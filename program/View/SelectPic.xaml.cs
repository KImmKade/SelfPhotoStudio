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
using System.Windows.Threading;
using System.IO;
using System.Windows.Forms;
using K4os.Compression.LZ4.Streams;
using System.Diagnostics;
using System.Drawing;
using System.Diagnostics.Eventing.Reader;
using OpenCvSharp.XPhoto;

namespace wpfTest.View
{
    /// <summary>
    /// SelectPic.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectPic : Page
    {

        int firstpicclickcheck = 0;
        int secondpicclickcheck = 0;
        int thirdpicclickcheck = 0;
        int fourthpicclickcheck = 0;
        int fifthpicclickcheck = 0;
        int sixthpicclickcheck = 0;
        int seventhpicclickcheck = 0;
        int eighthpicclickcheck = 0;
        int selectduration = Convert.ToInt32(MainWindow.count);

        public static int firstselect = 0;
        public static int secondselect = 0;
        public static int thirdselect = 0;
        public static int fourthselect = 0;
        public static int fifthselect = 0;
        public static int sixthselect = 0;
        public static int seventhselect = 0;
        public static int eighthselect = 0;

        System.Drawing.Image pic1;
        System.Drawing.Image pic2;
        System.Drawing.Image pic3;
        System.Drawing.Image pic4;
        System.Drawing.Image pic5;
        System.Drawing.Image pic6;
        System.Drawing.Image pic7;
        System.Drawing.Image pic8;

        BitmapImage Selectpicpage4 = new BitmapImage();


        DispatcherTimer timer = new DispatcherTimer();


        public SelectPic()
        {
            Source.Log.log.Info("SelectPic 구역 진입");
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                firstselect = 0;
                secondselect = 0;
                thirdselect = 0;
                fourthselect = 0;
                fifthselect = 0;
                sixthselect = 0;
                seventhselect = 0;
                eighthselect = 0;

                if (MainWindow.inifoldername == "dearpic1")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            backgroundimg.Source = MainWindow.Selectpicpage1;
                            Firstpic.Margin = new Thickness(1020, 337, 1920 - 1020 - 211, 1080 - 337 - 140);
                            Secondpic.Margin = new Thickness(1248, 337, 1920 - 1248 - 211, 1080 - 337 - 140);
                            Thirdpic.Margin = new Thickness(1020, 492, 1920 - 1020 - 211, 1080 - 492 - 140);
                            Fourthpic.Margin = new Thickness(1248, 492, 1920 - 1248 - 211, 1080 - 492 - 140);
                            Fifthpic.Margin = new Thickness(1020, 648, 1920 - 1020 - 211, 1080 - 648 - 140);
                            Sixthpic.Margin = new Thickness(1248, 648, 1920 - 1248 - 211, 1080 - 648 - 140);

                            firstpicborder.Margin = new Thickness(1020 - 5, 337 - 5, 1920 - 1020 - 211 - 5, 1080 - 337 - 140 - 5);
                            secondpicborder.Margin = new Thickness(1248 - 5, 337 - 5, 1920 - 1248 - 211 - 5, 1080 - 337 - 140 - 5);
                            thirdpicborder.Margin = new Thickness(1020 - 5, 492 - 5, 1920 - 1020 - 211 - 5, 1080 - 492 - 140 - 5);
                            fourthpicborder.Margin = new Thickness(1248 - 5, 492 - 5, 1920 - 1248 - 211 - 5, 1080 - 492 - 140 - 5);
                            fifthpicborder.Margin = new Thickness(1020 - 5, 648 - 5, 1920 - 1020 - 211 - 5, 1080 - 648 - 140 - 5);
                            sixthpicborder.Margin = new Thickness(1248 - 5, 648 - 5, 1920 - 1248 - 211 - 5, 1080 - 648 - 140 - 5);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;
                            Fifthpic.Source = MainWindow.fifthphoto;
                            Sixthpic.Source = MainWindow.sixthphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            pic5 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0005.JPG");
                            pic6 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0006.JPG");
                            break;
                        case 2:
                            backgroundimg.Source = MainWindow.Selectpicpage2;
                            Firstpic.Margin = new Thickness(1020, 337, 1920 - 1020 - 211, 1080 - 337 - 140);
                            Secondpic.Margin = new Thickness(1248, 337, 1920 - 1248 - 211, 1080 - 337 - 140);
                            Thirdpic.Margin = new Thickness(1020, 492, 1920 - 1020 - 211, 1080 - 492 - 140);
                            Fourthpic.Margin = new Thickness(1248, 492, 1920 - 1248 - 211, 1080 - 492 - 140);
                            Fifthpic.Margin = new Thickness(1020, 648, 1920 - 1020 - 211, 1080 - 648 - 140);
                            Sixthpic.Margin = new Thickness(1248, 648, 1920 - 1248 - 211, 1080 - 648 - 140);

                            firstpicborder.Margin = new Thickness(1020 - 5, 337 - 5, 1920 - 1020 - 211 - 5, 1080 - 337 - 140 - 5);
                            secondpicborder.Margin = new Thickness(1248 - 5, 337 - 5, 1920 - 1248 - 211 - 5, 1080 - 337 - 140 - 5);
                            thirdpicborder.Margin = new Thickness(1020 - 5, 492 - 5, 1920 - 1020 - 211 - 5, 1080 - 492 - 140 - 5);
                            fourthpicborder.Margin = new Thickness(1248 - 5, 492 - 5, 1920 - 1248 - 211 - 5, 1080 - 492 - 140 - 5);
                            fifthpicborder.Margin = new Thickness(1020 - 5, 648 - 5, 1920 - 1020 - 211 - 5, 1080 - 648 - 140 - 5);
                            sixthpicborder.Margin = new Thickness(1248 - 5, 648 - 5, 1920 - 1248 - 211 - 5, 1080 - 648 - 140 - 5);

                            priewviewimg.Width = 397;
                            priewviewimg.Height = 593;
                            priewviewimg.Margin = new Thickness(514, 347, 1920 - 397 - 514, 1080 - 593 - 347);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            break;
                        case 3:
                            backgroundimg.Source = MainWindow.Selectpicpage3;

                            priewviewimg.Width = 397;
                            priewviewimg.Height = 593;
                            priewviewimg.Margin = new Thickness(514, 347, 1920 - 397 - 514, 1080 - 593 - 347);

                            Firstpic.Width = 154;
                            Firstpic.Height = 154;
                            Secondpic.Width = 154;
                            Secondpic.Height = 154;
                            Thirdpic.Width = 154;
                            Thirdpic.Height = 154;
                            Fourthpic.Width = 154;
                            Fourthpic.Height = 154;
                            Fifthpic.Width = 154;
                            Fifthpic.Height = 154;
                            Sixthpic.Width = 154;
                            Sixthpic.Height = 154;
                            Seventhpic.Width = 154;
                            Seventhpic.Height = 154;
                            Eighthpic.Width = 154;
                            Eighthpic.Height = 154;

                            Firstpic.Margin = new Thickness(1077, 337, 1920 - 1077 - 154, 1080 - 337 - 154);
                            Secondpic.Margin = new Thickness(1247, 337, 1920 - 1247 - 154, 1080 - 337 - 154);
                            Thirdpic.Margin = new Thickness(1077, 508, 1920 - 1077 - 154, 1080 - 508 - 154);
                            Fourthpic.Margin = new Thickness(1247, 508, 1920 - 1247 - 154, 1080 - 508 - 154);
                            Fifthpic.Margin = new Thickness(1077, 679, 1920 - 1077 - 154, 1080 - 679 - 154);
                            Sixthpic.Margin = new Thickness(1247, 679, 1920 - 1247 - 154, 1080 - 679 - 154);
                            Seventhpic.Margin = new Thickness(1077, 850, 1920 - 1077 - 154, 1080 - 850 - 154);
                            Eighthpic.Margin = new Thickness(1247, 850, 1920 - 1247 - 154, 1080 - 850 - 154);

                            firstpicborder.Margin = new Thickness(1077 - 5, 337 - 5, 1920 - 1077 - 154 - 5, 1080 - 337 - 154 - 5);
                            secondpicborder.Margin = new Thickness(1247 - 5, 337 - 5, 1920 - 1247 - 154 - 5, 1080 - 337 - 154 - 5);
                            thirdpicborder.Margin = new Thickness(1077 - 5, 508 - 5, 1920 - 1077 - 154 - 5, 1080 - 508 - 154 - 5);
                            fourthpicborder.Margin = new Thickness(1247 - 5, 508 - 5, 1920 - 1247 - 154 - 5, 1080 - 508 - 154 - 5);
                            fifthpicborder.Margin = new Thickness(1077 - 5, 679 - 5, 1920 - 1077 - 154 - 5, 1080 - 679 - 154 - 5);
                            sixthpicborder.Margin = new Thickness(1247 - 5, 679 - 5, 1920 - 1247 - 154 - 5, 1080 - 679 - 154 - 5);
                            seventhpicborder.Margin = new Thickness(1077 - 5, 850 - 5, 1920 - 1077 - 154 - 5, 1080 - 850 - 154 - 5);
                            eighthpicborder.Margin = new Thickness(1247 - 5, 850 - 5, 1920 - 1247 - 154 - 5, 1080 - 850 - 154 - 5);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;
                            Fifthpic.Source = MainWindow.fifthphoto;
                            Sixthpic.Source = MainWindow.sixthphoto;
                            Seventhpic.Source = MainWindow.seventhphoto;
                            Eighthpic.Source = MainWindow.eighthphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            pic5 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0005.JPG");
                            pic6 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0006.JPG");
                            pic7 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0007.JPG");
                            pic8 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0008.JPG");
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            backgroundimg.Source = MainWindow.Selectpicpage1;

                            priewviewimg.Width = 397;
                            priewviewimg.Height = 593;
                            priewviewimg.Margin = new Thickness(514, 347, 1920 - 397 - 514, 1080 - 593 - 347);

                            Firstpic.Width = 140;
                            Firstpic.Height = 210;
                            Secondpic.Width = 140;
                            Secondpic.Height = 210;
                            Thirdpic.Width = 140;
                            Thirdpic.Height = 210;
                            Fourthpic.Width = 140;
                            Fourthpic.Height = 210;

                            Firstpic.Margin = new Thickness(1091, 338, 1920 - 1091 - 140, 1080 - 338 - 210);
                            Secondpic.Margin = new Thickness(1247, 338, 1920 - 1247 - 140, 1080 - 338 - 210);
                            Thirdpic.Margin = new Thickness(1091, 566, 1920 - 1091 - 140, 1080 - 566 - 210);
                            Fourthpic.Margin = new Thickness(1247, 566, 1920 - 1247 - 140, 1080 - 566 - 210);
                            Fifthpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Sixthpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Seventhpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Eighthpic.Margin = new Thickness(0, 0, 1920, 1080);

                            firstpicborder.Margin = new Thickness(1091 - 5, 338 - 5, 1920 - 1091 - 140 - 5, 1080 - 338 - 210 - 5);
                            secondpicborder.Margin = new Thickness(1247 - 5, 338 - 5, 1920 - 1247 - 140 - 5, 1080 - 338 - 210 - 5);
                            thirdpicborder.Margin = new Thickness(1091 - 5, 566 - 5, 1920 - 1091 - 140 - 5, 1080 - 566 - 210 - 5);
                            fourthpicborder.Margin = new Thickness(1247 - 5, 566 - 5, 1920 - 1247 - 140 - 5, 1080 - 566 - 210 - 5);
                            fifthpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            sixthpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            seventhpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            eighthpicborder.Margin = new Thickness(0, 0, 1920, 1080);


                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;

                            Firstpic.IsEnabled = true;
                            Secondpic.IsEnabled = true;
                            Thirdpic.IsEnabled = true;
                            Fourthpic.IsEnabled = true;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            break;
                        case 2:
                            backgroundimg.Source = MainWindow.Selectpicpage2;

                            priewviewimg.Width = 397;
                            priewviewimg.Height = 593;
                            priewviewimg.Margin = new Thickness(514, 347, 1920 - 397 - 514, 1080 - 593 - 347);

                            Firstpic.Width = 140;
                            Firstpic.Height = 210;
                            Secondpic.Width = 140;
                            Secondpic.Height = 210;
                            Thirdpic.Width = 140;
                            Thirdpic.Height = 210;
                            Fourthpic.Width = 140;
                            Fourthpic.Height = 210;
                            Fifthpic.Width = 140;
                            Fifthpic.Height = 210;
                            Sixthpic.Width = 140;
                            Sixthpic.Height = 210;

                            Firstpic.Margin = new Thickness(1013, 338, 1920 - 1013 - 140, 1080 - 338 - 210);
                            Secondpic.Margin = new Thickness(1168, 338, 1920 - 1168 - 140, 1080 - 338 - 210);
                            Thirdpic.Margin = new Thickness(1323, 338, 1920 - 1323 - 140, 1080 - 338 - 210);
                            Fourthpic.Margin = new Thickness(1013, 566, 1920 - 1013 - 140, 1080 - 566 - 210);
                            Fifthpic.Margin = new Thickness(1168, 566, 1920 - 1168 - 140, 1080 - 566 - 210);
                            Sixthpic.Margin = new Thickness(1323, 566, 1920 - 1323 - 140, 1080 - 566 - 210);
                            Seventhpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Eighthpic.Margin = new Thickness(0, 0, 1920, 1080);

                            firstpicborder.Margin = new Thickness(1013 - 5, 338 - 5, 1920 - 1013 - 140 - 5, 1080 - 338 - 210 - 5);
                            secondpicborder.Margin = new Thickness(1168 - 5, 338 - 5, 1920 - 1168 - 140 - 5, 1080 - 338 - 210 - 5);
                            thirdpicborder.Margin = new Thickness(1323 - 5, 338 - 5, 1920 - 1323 - 140 - 5, 1080 - 338 - 210 - 5);
                            fourthpicborder.Margin = new Thickness(1013 - 5, 566 - 5, 1920 - 1013 - 140 - 5, 1080 - 566 - 210 - 5);
                            fifthpicborder.Margin = new Thickness(1168 - 5, 566 - 5, 1920 - 1168 - 140 - 5, 1080 - 566 - 210 - 5);
                            sixthpicborder.Margin = new Thickness(1323 - 5, 566 - 5, 1920 - 1323 - 140 - 5, 1080 - 566 - 210 - 5);
                            seventhpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            eighthpicborder.Margin = new Thickness(0, 0, 1920, 1080);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;
                            Fifthpic.Source = MainWindow.fifthphoto;
                            Sixthpic.Source = MainWindow.sixthphoto;

                            Firstpic.IsEnabled = true;
                            Secondpic.IsEnabled = true;
                            Thirdpic.IsEnabled = true;
                            Fourthpic.IsEnabled = true;
                            Fifthpic.IsEnabled = true;
                            Sixthpic.IsEnabled = true;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            pic5 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0005.JPG");
                            pic6 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0006.JPG");
                            break;
                        case 3:
                            backgroundimg.Source = MainWindow.Selectpicpage3;

                            priewviewimg.Width = 397;
                            priewviewimg.Height = 593;
                            priewviewimg.Margin = new Thickness(514, 347, 1920 - 397 - 514, 1080 - 593 - 347);

                            Firstpic.Width = 140;
                            Firstpic.Height = 210;
                            Secondpic.Width = 140;
                            Secondpic.Height = 210;
                            Thirdpic.Width = 140;
                            Thirdpic.Height = 210;
                            Fourthpic.Width = 140;
                            Fourthpic.Height = 210;
                            Fifthpic.Width = 140;
                            Fifthpic.Height = 210;
                            Sixthpic.Width = 140;
                            Sixthpic.Height = 210;

                            Firstpic.Margin = new Thickness(1013, 338, 1920 - 1013 - 140, 1080 - 338 - 210);
                            Secondpic.Margin = new Thickness(1168, 338, 1920 - 1168 - 140, 1080 - 338 - 210);
                            Thirdpic.Margin = new Thickness(1323, 338, 1920 - 1323 - 140, 1080 - 338 - 210);
                            Fourthpic.Margin = new Thickness(1013, 566, 1920 - 1013 - 140, 1080 - 566 - 210);
                            Fifthpic.Margin = new Thickness(1168, 566, 1920 - 1168 - 140, 1080 - 566 - 210);
                            Sixthpic.Margin = new Thickness(1323, 566, 1920 - 1323 - 140, 1080 - 566 - 210);
                            Seventhpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Eighthpic.Margin = new Thickness(0, 0, 1920, 1080);

                            firstpicborder.Margin = new Thickness(1013 - 5, 338 - 5, 1920 - 1013 - 140 - 5, 1080 - 338 - 210 - 5);
                            secondpicborder.Margin = new Thickness(1168 - 5, 338 - 5, 1920 - 1168 - 140 - 5, 1080 - 338 - 210 - 5);
                            thirdpicborder.Margin = new Thickness(1323 - 5, 338 - 5, 1920 - 1323 - 140 - 5, 1080 - 338 - 210 - 5);
                            fourthpicborder.Margin = new Thickness(1013 - 5, 566 - 5, 1920 - 1013 - 140 - 5, 1080 - 566 - 210 - 5);
                            fifthpicborder.Margin = new Thickness(1168 - 5, 566 - 5, 1920 - 1168 - 140 - 5, 1080 - 566 - 210 - 5);
                            sixthpicborder.Margin = new Thickness(1323 - 5, 566 - 5, 1920 - 1323 - 140 - 5, 1080 - 566 - 210 - 5);
                            seventhpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            eighthpicborder.Margin = new Thickness(0, 0, 1920, 1080);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;
                            Fifthpic.Source = MainWindow.fifthphoto;
                            Sixthpic.Source = MainWindow.sixthphoto;

                            Firstpic.IsEnabled = true;
                            Secondpic.IsEnabled = true;
                            Thirdpic.IsEnabled = true;
                            Fourthpic.IsEnabled = true;
                            Fifthpic.IsEnabled = true;
                            Sixthpic.IsEnabled = true;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            pic5 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0005.JPG");
                            pic6 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0006.JPG");
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "dearpic2")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            backgroundimg.Source = MainWindow.Selectpicpage1;

                            priewviewimg.Width = 593;
                            priewviewimg.Height = 397;
                            priewviewimg.Margin = new Thickness(416, 382, 1920 - 593 - 416, 1080 - 397 - 382);

                            Firstpic.Width = 173;
                            Firstpic.Height = 173;
                            Secondpic.Width = 173;
                            Secondpic.Height = 173;
                            Thirdpic.Width = 173;
                            Thirdpic.Height = 173;

                            Firstpic.Margin = new Thickness(1114, 337, 1920 - 1114 - 173, 1080 - 337 - 173);
                            Secondpic.Margin = new Thickness(1304, 337, 1920 - 1304 - 173, 1080 - 337 - 173);
                            Thirdpic.Margin = new Thickness(1114, 532, 1920 - 1114 - 173, 1080 - 532 - 173);
                            Fourthpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Fifthpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Sixthpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Seventhpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Eighthpic.Margin = new Thickness(0, 0, 1920, 1080);

                            firstpicborder.Margin = new Thickness(1114 - 5, 337 - 5, 1920 - 1114 - 173 - 5, 1080 - 337 - 173 - 5);
                            secondpicborder.Margin = new Thickness(1304 - 5, 337 - 5, 1920 - 1304 - 173 - 5, 1080 - 337 - 173 - 5);
                            thirdpicborder.Margin = new Thickness(1114 - 5, 532 - 5, 1920 - 1114 - 173 - 5, 1080 - 532 - 173 - 5);
                            fourthpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            fifthpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            sixthpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            seventhpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            eighthpicborder.Margin = new Thickness(0, 0, 1920, 1080);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            break;
                        case 2:
                            backgroundimg.Source = MainWindow.Selectpicpage2;

                            priewviewimg.Width = 397;
                            priewviewimg.Height = 593;
                            priewviewimg.Margin = new Thickness(514, 347, 1920 - 397 - 514, 1080 - 593 - 347);

                            Firstpic.Width = 210;
                            Firstpic.Height = 140;
                            Secondpic.Width = 210;
                            Secondpic.Height = 140;
                            Thirdpic.Width = 210;
                            Thirdpic.Height = 140;
                            Fourthpic.Width = 210;
                            Fourthpic.Height = 140;

                            Firstpic.Margin = new Thickness(1020, 337, 1920 - 1020 - 210, 1080 - 337 - 140);
                            Secondpic.Margin = new Thickness(1248, 337, 1920 - 1248 - 210, 1080 - 337 - 140);
                            Thirdpic.Margin = new Thickness(1020, 493, 1920 - 1020 - 210, 1080 - 493 - 140);
                            Fourthpic.Margin = new Thickness(1248, 493, 1920 - 1248 - 210, 1080 - 493 - 140);
                            Fifthpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Sixthpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Seventhpic.Margin = new Thickness(0, 0, 1920, 1080);
                            Eighthpic.Margin = new Thickness(0, 0, 1920, 1080);

                            firstpicborder.Margin = new Thickness(1020 - 5, 337 - 5, 1920 - 1020 - 210 - 5, 1080 - 337 - 140 - 5);
                            secondpicborder.Margin = new Thickness(1248 - 5, 337 - 5, 1920 - 1248 - 210 - 5, 1080 - 337 - 140 - 5);
                            thirdpicborder.Margin = new Thickness(1020 - 5, 493 - 5, 1920 - 1020 - 210 - 5, 1080 - 493 - 140 - 5);
                            fourthpicborder.Margin = new Thickness(1248 - 5, 493 - 5, 1920 - 1248 - 210 - 5, 1080 - 493 - 140 - 5);
                            fifthpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            sixthpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            seventhpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                            eighthpicborder.Margin = new Thickness(0, 0, 1920, 1080);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "tech")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            backgroundimg.Source = MainWindow.Selectpicpage1;
                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;
                            Fifthpic.Source = MainWindow.fifthphoto;
                            Sixthpic.Source = MainWindow.sixthphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            pic5 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0005.JPG");
                            pic6 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0006.JPG");
                            break;
                        case 2:
                            backgroundimg.Source = MainWindow.Selectpicpage2;

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;
                            Fifthpic.Source = MainWindow.fifthphoto;
                            Sixthpic.Source = MainWindow.sixthphoto;

                            priewviewimg.Width = 428;
                            priewviewimg.Height = 488;
                            priewviewimg.Margin = new Thickness(499, 488, 1920 - 428 - 499, 1080 - 488 - 488);

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            pic5 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0005.JPG");
                            pic6 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0006.JPG");
                            break;
                        case 3:
                            backgroundimg.Source = MainWindow.Selectpicpage3;

                            priewviewimg.Width = 397;
                            priewviewimg.Height = 593;
                            priewviewimg.Margin = new Thickness(514, 347, 1920 - 397 - 514, 1080 - 593 - 347);

                            Firstpic.Width = 155;
                            Firstpic.Height = 155;
                            Secondpic.Width = 155;
                            Secondpic.Height = 155;
                            Thirdpic.Width = 155;
                            Thirdpic.Height = 155;
                            Fourthpic.Width = 155;
                            Fourthpic.Height = 155;
                            Fifthpic.Width = 155;
                            Fifthpic.Height = 155;
                            Sixthpic.Width = 155;
                            Sixthpic.Height = 155;
                            Seventhpic.Width = 155;
                            Seventhpic.Height = 155;
                            Eighthpic.Width = 155;
                            Eighthpic.Height = 155;

                            Firstpic.Margin = new Thickness(1067, 315, 1920 - 1067 - 155, 1080 - 315 - 155);
                            Secondpic.Margin = new Thickness(1237, 315, 1920 - 1237 - 155, 1080 - 315 - 155);
                            Thirdpic.Margin = new Thickness(1067, 486, 1920 - 1067 - 155, 1080 - 486 - 155);
                            Fourthpic.Margin = new Thickness(1237, 486, 1920 - 1237 - 155, 1080 - 486 - 155);
                            Fifthpic.Margin = new Thickness(1067, 657, 1920 - 1067 - 155, 1080 - 657 - 155);
                            Sixthpic.Margin = new Thickness(1237, 657, 1920 - 1237 - 155, 1080 - 657 - 155);
                            Seventhpic.Margin = new Thickness(1067, 828, 1920 - 1067 - 155, 1080 - 828 - 155);
                            Eighthpic.Margin = new Thickness(1237, 828, 1920 - 1237 - 155, 1080 - 828 - 155);

                            firstpicborder.Margin = new Thickness(1067 - 5, 315 - 5, 1920 - 1067 - 155 - 5, 1080 - 315 - 155 - 5);
                            secondpicborder.Margin = new Thickness(1237 - 5, 315 - 5, 1920 - 1237 - 155 - 5, 1080 - 315 - 155 - 5);
                            thirdpicborder.Margin = new Thickness(1067 - 5, 486 - 5, 1920 - 1067 - 155 - 5, 1080 - 486 - 155 - 5);
                            fourthpicborder.Margin = new Thickness(1237 - 5, 486 - 5, 1920 - 1237 - 155 - 5, 1080 - 486 - 155 - 5);
                            fifthpicborder.Margin = new Thickness(1067 - 5, 657 - 5, 1920 - 1067 - 155 - 5, 1080 - 657 - 155 - 5);
                            sixthpicborder.Margin = new Thickness(1237 - 5, 657 - 5, 1920 - 1237 - 155 - 5, 1080 - 657 - 155 - 5);
                            seventhpicborder.Margin = new Thickness(1067 - 5, 828 - 5, 1920 - 1067 - 155 - 5, 1080 - 828 - 155 - 5);
                            eighthpicborder.Margin = new Thickness(1237 - 5, 828 - 5, 1920 - 1237 - 155 - 5, 1080 - 828 - 155 - 5);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;
                            Fifthpic.Source = MainWindow.fifthphoto;
                            Sixthpic.Source = MainWindow.sixthphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            pic5 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0005.JPG");
                            pic6 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0006.JPG");
                            break;
                        case 4:
                            Selectpicpage4 = new BitmapImage();
                            Selectpicpage4.BeginInit();
                            Selectpicpage4.UriSource = new Uri(MainWindow.uipath + @"\selectpic4.png", UriKind.RelativeOrAbsolute);
                            Selectpicpage4.CacheOption = BitmapCacheOption.OnLoad;
                            Selectpicpage4.EndInit();
                            backgroundimg.Source = Selectpicpage4;

                            priewviewimg.Width = 397;
                            priewviewimg.Height = 593;
                            priewviewimg.Margin = new Thickness(514, 347, 1920 - 397 - 514, 1080 - 593 - 347);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;
                            Fifthpic.Source = MainWindow.fifthphoto;
                            Sixthpic.Source = MainWindow.sixthphoto;
                            Seventhpic.Source = MainWindow.seventhphoto;
                            Eighthpic.Source = MainWindow.eighthphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            pic5 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0005.JPG");
                            pic6 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0006.JPG");
                            pic7 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0007.JPG");
                            pic8 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0008.JPG");
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "hhh")
                {
                    Timer.Margin = new Thickness(1613, 130, 107, 850);
                    int redvalue = 245;
                    int greenvalue = 218;
                    int bluevalue = 165;
                    System.Windows.Media.Color bordercolor = System.Windows.Media.Color.FromRgb((byte)redvalue, (byte)greenvalue, (byte)bluevalue);

                    SolidColorBrush brush = new SolidColorBrush(bordercolor);
                    firstpicborder.BorderBrush = brush;
                    secondpicborder.BorderBrush = brush;
                    thirdpicborder.BorderBrush = brush;
                    fourthpicborder.BorderBrush = brush;

                    priewviewimg.Width = 400;
                    priewviewimg.Height = 565;
                    priewviewimg.Margin = new Thickness(513, 359, 1920 - 400 - 513, 1080 - 565 - 359);

                    Firstpic.Width = 143;
                    Firstpic.Height = 214;
                    Secondpic.Width = 143;
                    Secondpic.Height = 214;
                    Thirdpic.Width = 143;
                    Thirdpic.Height = 214;
                    Fourthpic.Width = 143;
                    Fourthpic.Height = 214;

                    Firstpic.Margin = new Thickness(1073, 363, 1920 - 143 - 1073, 1080 - 363 - 214);
                    Secondpic.Margin = new Thickness(1240, 363, 1920 - 143 - 1240, 1080 - 363 - 214);
                    Thirdpic.Margin = new Thickness(1073, 601, 1920 - 143 - 1073, 1080 - 601 - 214);
                    Fourthpic.Margin = new Thickness(1240, 601, 1920 - 143 - 1240, 1080 - 601 - 214);
                    Fifthpic.Margin = new Thickness(0, 0, 1920, 1080);
                    Sixthpic.Margin = new Thickness(0, 0, 1920, 1080);
                    Seventhpic.Margin = new Thickness(0, 0, 1920, 1080);
                    Eighthpic.Margin = new Thickness(0, 0, 1920, 1080);

                    firstpicborder.Margin = new Thickness(1073 - 5, 363 - 5, 1920 - 143 - 1073 - 5, 1080 - 363 - 214 - 5);
                    secondpicborder.Margin = new Thickness(1240 - 5, 363 - 5, 1920 - 143 - 1240 - 5, 1080 - 363 - 214 - 5);
                    thirdpicborder.Margin = new Thickness(1073 - 5, 601 - 5, 1920 - 143 - 1073 - 5, 1080 - 601 - 214 - 5);
                    fourthpicborder.Margin = new Thickness(1240 - 5, 601 - 5, 1920 - 143 - 1240 - 5, 1080 - 601 - 214 - 5);
                    fifthpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                    sixthpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                    seventhpicborder.Margin = new Thickness(0, 0, 1920, 1080);
                    eighthpicborder.Margin = new Thickness(0, 0, 1920, 1080);

                    BitmapImage opacity0img = new BitmapImage();
                    opacity0img.BeginInit();
                    opacity0img.UriSource = new Uri(MainWindow.TempPath + @"\Temp1_2Front.png");
                    opacity0img.CacheOption = BitmapCacheOption.OnLoad;
                    opacity0img.EndInit();

                    Selectfirstpic.Source = opacity0img;
                    Selectsecondpic.Source = opacity0img;
                    Selectthirdpic.Source = opacity0img;
                    Selectfourthpic.Source = opacity0img;

                    if (MainWindow.optiontempnum == 1)
                    {
                        Selectfirstpic.Width = 145;
                        Selectfirstpic.Height = 217;
                        Selectfirstpic.Margin = new Thickness(550, 464, 1920 - 550 - 145, 1080 - 464 - 217);
                        backgroundimg.Source = MainWindow.Selectpicpage1;
                        Firstpic.Source = MainWindow.firstphoto;
                        Secondpic.Source = MainWindow.secondphoto;

                        pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                        pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                Selectfirstpic.Width = 145;
                                Selectfirstpic.Height = 217;
                                Selectfirstpic.Margin = new Thickness(550, 464, 1920 - 550 - 145, 1080 - 464 - 217);
                                backgroundimg.Source = MainWindow.Selectpicpage1;
                                Firstpic.Source = MainWindow.firstphoto;
                                Secondpic.Source = MainWindow.secondphoto;

                                pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\RPhoto0001.JPG");
                                pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\RPhoto0002.JPG");
                                break;
                            case 2:
                                backgroundimg.Source = MainWindow.Selectpicpage2;

                                Selectfirstpic.Width = 139;
                                Selectfirstpic.Height = 209;
                                Selectsecondpic.Width = 139;
                                Selectsecondpic.Height = 209;

                                Selectfirstpic.Margin = new Thickness(548, 462, 1920 - 548 - 139, 1080 - 462 - 209);
                                Selectsecondpic.Margin = new Thickness(735, 462, 1920 - 735 - 139, 1080 - 462 - 209);

                                Firstpic.Source = MainWindow.firstphoto;
                                Secondpic.Source = MainWindow.secondphoto;
                                Thirdpic.Source = MainWindow.thirdphoto;
                                Fourthpic.Source = MainWindow.fourthphoto;

                                pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\RPhoto0001.JPG");
                                pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\RPhoto0002.JPG");
                                pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\RPhoto0003.JPG");
                                pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\RPhoto0004.JPG");
                                break;
                            case 3:
                                backgroundimg.Source = MainWindow.Selectpicpage3;

                                Selectfirstpic.Width = 88;
                                Selectfirstpic.Height = 132;
                                Selectsecondpic.Width = 88;
                                Selectsecondpic.Height = 132;
                                Selectthirdpic.Width = 88;
                                Selectthirdpic.Height = 132;
                                Selectfourthpic.Width = 88;
                                Selectfourthpic.Height = 132;

                                Selectfirstpic.Margin = new Thickness(542, 454, 1920 - 542 - 88, 1080 - 454 - 132);
                                Selectsecondpic.Margin = new Thickness(542, 605, 1920 - 542 - 88, 1080 - 605 - 132);
                                Selectthirdpic.Margin = new Thickness(667, 605, 1920 - 667 - 88, 1080 - 605 - 132);
                                Selectfourthpic.Margin = new Thickness(792, 605, 1920 - 792 - 88, 1080 - 605 - 132);

                                Firstpic.Source = MainWindow.firstphoto;
                                Secondpic.Source = MainWindow.secondphoto;
                                Thirdpic.Source = MainWindow.thirdphoto;
                                Fourthpic.Source = MainWindow.fourthphoto;

                                pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\RPhoto0001.JPG");
                                pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\RPhoto0002.JPG");
                                pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\RPhoto0003.JPG");
                                pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\RPhoto0004.JPG");
                                break;
                        }
                    }
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            backgroundimg.Source = MainWindow.Selectpicpage1;
                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;
                            Fifthpic.Source = MainWindow.fifthphoto;
                            Sixthpic.Source = MainWindow.sixthphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            pic5 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0005.JPG");
                            pic6 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0006.JPG");
                            break;
                        case 3:
                            backgroundimg.Source = MainWindow.Selectpicpage3;

                            priewviewimg.Width = 397;
                            priewviewimg.Height = 593;
                            priewviewimg.Margin = new Thickness(514, 347, 1920 - 397 - 514, 1080 - 593 - 347);

                            Firstpic.Width = 155;
                            Firstpic.Height = 155;
                            Secondpic.Width = 155;
                            Secondpic.Height = 155;
                            Thirdpic.Width = 155;
                            Thirdpic.Height = 155;
                            Fourthpic.Width = 155;
                            Fourthpic.Height = 155;
                            Fifthpic.Width = 155;
                            Fifthpic.Height = 155;
                            Sixthpic.Width = 155;
                            Sixthpic.Height = 155;
                            Seventhpic.Width = 155;
                            Seventhpic.Height = 155;
                            Eighthpic.Width = 155;
                            Eighthpic.Height = 155;

                            Firstpic.Margin = new Thickness(1067, 315, 1920 - 1067 - 155, 1080 - 315 - 155);
                            Secondpic.Margin = new Thickness(1237, 315, 1920 - 1237 - 155, 1080 - 315 - 155);
                            Thirdpic.Margin = new Thickness(1067, 486, 1920 - 1067 - 155, 1080 - 486 - 155);
                            Fourthpic.Margin = new Thickness(1237, 486, 1920 - 1237 - 155, 1080 - 486 - 155);
                            Fifthpic.Margin = new Thickness(1067, 657, 1920 - 1067 - 155, 1080 - 657 - 155);
                            Sixthpic.Margin = new Thickness(1237, 657, 1920 - 1237 - 155, 1080 - 657 - 155);
                            Seventhpic.Margin = new Thickness(1067, 828, 1920 - 1067 - 155, 1080 - 828 - 155);
                            Eighthpic.Margin = new Thickness(1237, 828, 1920 - 1237 - 155, 1080 - 828 - 155);

                            firstpicborder.Margin = new Thickness(1067 - 5, 315 - 5, 1920 - 1067 - 155 - 5, 1080 - 315 - 155 - 5);
                            secondpicborder.Margin = new Thickness(1237 - 5, 315 - 5, 1920 - 1237 - 155 - 5, 1080 - 315 - 155 - 5);
                            thirdpicborder.Margin = new Thickness(1067 - 5, 486 - 5, 1920 - 1067 - 155 - 5, 1080 - 486 - 155 - 5);
                            fourthpicborder.Margin = new Thickness(1237 - 5, 486 - 5, 1920 - 1237 - 155 - 5, 1080 - 486 - 155 - 5);
                            fifthpicborder.Margin = new Thickness(1067 - 5, 657 - 5, 1920 - 1067 - 155 - 5, 1080 - 657 - 155 - 5);
                            sixthpicborder.Margin = new Thickness(1237 - 5, 657 - 5, 1920 - 1237 - 155 - 5, 1080 - 657 - 155 - 5);
                            seventhpicborder.Margin = new Thickness(1067 - 5, 828 - 5, 1920 - 1067 - 155 - 5, 1080 - 828 - 155 - 5);
                            eighthpicborder.Margin = new Thickness(1237 - 5, 828 - 5, 1920 - 1237 - 155 - 5, 1080 - 828 - 155 - 5);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;
                            Fifthpic.Source = MainWindow.fifthphoto;
                            Sixthpic.Source = MainWindow.sixthphoto;
                            Seventhpic.Source = MainWindow.seventhphoto;
                            Eighthpic.Source = MainWindow.eighthphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            pic5 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0005.JPG");
                            pic6 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0006.JPG");
                            pic7 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0007.JPG");
                            pic8 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0008.JPG");
                            break;
                        case 2:
                            backgroundimg.Source = MainWindow.Selectpicpage2;

                            priewviewimg.Width = 397;
                            priewviewimg.Height = 593;
                            priewviewimg.Margin = new Thickness(514, 347, 1920 - 397 - 514, 1080 - 593 - 347);

                            Firstpic.Source = MainWindow.firstphoto;
                            Secondpic.Source = MainWindow.secondphoto;
                            Thirdpic.Source = MainWindow.thirdphoto;
                            Fourthpic.Source = MainWindow.fourthphoto;

                            pic1 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0001.JPG");
                            pic2 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0002.JPG");
                            pic3 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0003.JPG");
                            pic4 = System.Drawing.Image.FromFile(MainWindow.ResizePath + @"\Rphoto0004.JPG");
                            break;
                    }
                }

                PriewviewImgShow();

                if (MainWindow.inifoldername == "hhh")
                {
                    switch (MainWindow.checkcolor)
                    {
                        case "twice":
                            nextpage.Source = MainWindow.nextbtn;
                            break;
                        case "color":
                        case "none":
                            nextpage.Source = MainWindow.printbtn;
                            break;
                        case "black":
                            nextpage.Source = MainWindow.printbtn;
                            break;
                    }
                }
                else
                {
                    nextpage.Source = MainWindow.nextbtn;
                }

                Timer.Foreground = MainWindow.textbrush;
                
                if (MainWindow.timerlocation == "left")
                {
                    Timer.Margin = new Thickness(0, 130, 1720, 0);
                }

                if (MainWindow.Timer.ToString() == "Use")
                {
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += new EventHandler(Timer_Tik);
                    timer.Start();
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

        private void Firstpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (firstpicclickcheck == 0)
                {
                    firstpicclickcheck = 1;
                    firstpicborder.Opacity = 1;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    firstselect = 1;
                                }
                                else if (firstselect != 0)
                                {
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                }
                                break;
                            case 2:
                            case 3:
                                if (firstselect == 0)
                                {
                                    firstselect = 1;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 1;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    thirdselect = 1;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    fourthselect = 1;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "dearpic2")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    firstselect = 1;
                                }
                                else if (firstselect != 0)
                                {
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    firstselect = 1;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 1;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
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
                                if (firstselect == 0)
                                {
                                    firstselect = 1;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 1;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    thirdselect = 1;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    fourthselect = 1;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 4:
                                if (firstselect == 0)
                                {
                                    firstselect = 1;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 1;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    thirdselect = 1;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    fourthselect = 1;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    fifthselect = 1;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    sixthselect = 1;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "hhh")
                    {
                        if (MainWindow.optiontempnum == 1)
                        {
                            if (firstselect == 0)
                            {
                                firstselect = 1;
                            }
                            else if (firstselect == 2)
                            {
                                secondpicclickcheck = 0;
                                secondpicborder.Opacity = 0;
                                firstselect = 1;
                            }
                            else if (firstselect != 0)
                            {
                                firstpicclickcheck = 0;
                                firstpicborder.Opacity = 0;
                            }
                        }
                        else
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    if (firstselect == 0)
                                    {
                                        firstselect = 1;
                                    }
                                    else if (firstselect == 2)
                                    {
                                        secondpicclickcheck = 0;
                                        secondpicborder.Opacity = 0;
                                        firstselect = 1;
                                    }
                                    else if (firstselect != 0)
                                    {
                                        firstpicclickcheck = 0;
                                        firstpicborder.Opacity = 0;
                                    }
                                    break;
                                case 2:
                                    if (firstselect == 0)
                                    {
                                        firstselect = 1;
                                    }
                                    else if (firstselect != 0 && secondselect == 0)
                                    {
                                        secondselect = 1;
                                    }
                                    else if (firstselect != 0 && secondselect != 0)
                                    {
                                        firstpicclickcheck = 0;
                                        firstpicborder.Opacity = 0;
                                    }
                                    break;
                                case 3:
                                    if (firstselect == 0)
                                    {
                                        firstselect = 1;
                                    }
                                    else if (firstselect != 0 && secondselect == 0)
                                    {
                                        secondselect = 1;
                                    }
                                    else if (secondselect != 0 && thirdselect == 0)
                                    {
                                        thirdselect = 1;
                                    }
                                    else if (thirdselect != 0 && fourthselect == 0)
                                    {
                                        fourthselect = 1;
                                    }
                                    else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                    {
                                        firstpicclickcheck = 0;
                                        firstpicborder.Opacity = 0;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    firstselect = 1;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 1;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    thirdselect = 1;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    fourthselect = 1;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 3:
                                if (firstselect == 0)
                                {
                                    firstselect = 1;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 1;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    thirdselect = 1;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    fourthselect = 1;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    fifthselect = 1;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    sixthselect = 1;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    firstselect = 1;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 1;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    firstpicclickcheck = 0;
                    firstpicborder.Opacity = 0;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 1)
                                {
                                    firstselect = 0;
                                }
                                break;
                            case 2:
                            case 3:
                                if (firstselect == 1)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 1)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 1)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 1)
                                {
                                    fourthselect = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "dearpic2")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 1)
                                {
                                    firstselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 1)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 1)
                                {
                                    secondselect = 0;
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
                                if (firstselect == 1)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 1)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 1)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 1)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 4:
                                if (firstselect == 1)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 1)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 1)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 1)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 1)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 1)
                                {
                                    sixthselect = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "hhh")
                    {
                        if (MainWindow.optiontempnum == 1)
                        {
                            if (firstselect == 1)
                            {
                                firstselect = 0;
                            }
                        }
                        else
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    if (firstselect == 1)
                                    {
                                        firstselect = 0;
                                    }
                                    break;
                                case 2:
                                    if (firstselect == 1)
                                    {
                                        firstselect = 0;
                                    }
                                    else if (secondselect == 1)
                                    {
                                        secondselect = 0;
                                    }
                                    break;
                                case 3:
                                    if (firstselect == 1)
                                    {
                                        firstselect = 0;
                                    }
                                    else if (secondselect == 1)
                                    {
                                        secondselect = 0;
                                    }
                                    else if (thirdselect == 1)
                                    {
                                        thirdselect = 0;
                                    }
                                    else if (fourthselect == 1)
                                    {
                                        fourthselect = 0;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 1)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 1)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 1)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 1)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 1)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 1)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 1)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 1)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 1)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 1)
                                {
                                    sixthselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 1)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 1)
                                {
                                    secondselect = 0;
                                }
                                break;
                        }
                    }
                }

                PriewviewImgShow();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Secondpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (secondpicclickcheck == 0)
                {
                    secondpicclickcheck = 1;
                    secondpicborder.Opacity = 1;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 2;
                                }
                                else if (firstselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                }
                                break;
                            case 2:
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 2;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 2;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 2;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 2;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "dearpic2")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 2;
                                }
                                else if (firstselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 2;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 2;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
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
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 2;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 2;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 2;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 2;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 4:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 2;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 2;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 2;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 2;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 2;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 2;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "hhh")
                    {
                        if (MainWindow.optiontempnum == 1)
                        {
                            if (firstselect == 0)
                            {
                                firstselect = 2;
                            }
                            else if (firstselect == 1)
                            {
                                firstpicclickcheck = 0;
                                firstpicborder.Opacity = 0;
                                firstselect = 2;
                            }
                            else if (firstselect != 0)
                            {
                                secondpicclickcheck = 0;
                                secondpicborder.Opacity = 0;
                            }
                        }
                        else
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    if (firstselect == 0)
                                    {
                                        firstselect = 2;
                                    }
                                    else if (firstselect == 1)
                                    {
                                        firstpicclickcheck = 0;
                                        firstpicborder.Opacity = 0;
                                        firstselect = 2;
                                    }
                                    else if (firstselect != 0)
                                    {
                                        secondpicclickcheck = 0;
                                        secondpicborder.Opacity = 0;
                                    }
                                    break;
                                case 2:
                                    if (firstselect == 0)
                                    {

                                        firstselect = 2;
                                    }
                                    else if (firstselect != 0 && secondselect == 0)
                                    {
                                        secondselect = 2;
                                    }
                                    else if (firstselect != 0 && secondselect != 0)
                                    {
                                        secondpicclickcheck = 0;
                                        secondpicborder.Opacity = 0;
                                    }
                                    break;
                                case 3:
                                    if (firstselect == 0)
                                    {

                                        firstselect = 2;
                                    }
                                    else if (firstselect != 0 && secondselect == 0)
                                    {
                                        secondselect = 2;
                                    }
                                    else if (secondselect != 0 && thirdselect == 0)
                                    {
                                        thirdselect = 2;
                                    }
                                    else if (thirdselect != 0 && fourthselect == 0)
                                    {
                                        fourthselect = 2;
                                    }
                                    else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                    {
                                        secondpicclickcheck = 0;
                                        secondpicborder.Opacity = 0;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 2;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 2;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 2;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 2;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 2;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 2;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 2;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 2;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 2;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 2;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 2;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 2;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    secondpicclickcheck = 0;
                    secondpicborder.Opacity = 0;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 2)
                                {
                                    firstselect = 0;
                                }
                                break;
                            case 2:
                            case 3:
                                if (firstselect == 2)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 2)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 2)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 2)
                                {
                                    fourthselect = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "dearpic2")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 2)
                                {
                                    firstselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 2)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 2)
                                {
                                    secondselect = 0;
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
                                if (firstselect == 2)
                                {
                                    firstselect = 0;

                                }
                                else if (secondselect == 2)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 2)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 2)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 4:
                                if (firstselect == 2)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 2)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 2)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 2)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 2)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 2)
                                {
                                    sixthselect = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "hhh")
                    {
                        if (MainWindow.optiontempnum == 1)
                        {
                            if (firstselect == 2)
                            {
                                firstselect = 0;
                            }
                        }
                        else
                        {
                            switch (TempSelect.temp)
                            {
                                case 1:
                                    if (firstselect == 2)
                                    {
                                        firstselect = 0;
                                    }
                                    break;
                                case 2:
                                    if (firstselect == 2)
                                    {
                                        firstselect = 0;
                                    }
                                    else if (secondselect == 2)
                                    {
                                        secondselect = 0;
                                    }
                                    break;
                                case 3:
                                    if (firstselect == 2)
                                    {
                                        firstselect = 0;
                                    }
                                    else if (secondselect == 2)
                                    {
                                        secondselect = 0;
                                    }
                                    else if (thirdselect == 2)
                                    {
                                        thirdselect = 0;
                                    }
                                    else if (fourthselect == 2)
                                    {
                                        fourthselect = 0;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 2)
                                {
                                    firstselect = 0;

                                }
                                else if (secondselect == 2)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 2)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 2)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 2)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 2)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 2)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 2)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 2)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 2)
                                {
                                    sixthselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 2)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 2)
                                {
                                    secondselect = 0;
                                }
                                break;
                        }
                    }
                }

                PriewviewImgShow();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Thirdpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (thirdpicclickcheck == 0)
                {
                    thirdpicclickcheck = 1;
                    thirdpicborder.Opacity = 1;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 3;
                                }
                                else if (firstselect != 0)
                                {
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                }
                                break;
                            case 2:
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 3;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 3;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 3;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 3;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "dearpic2")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 3;
                                }
                                else if (firstselect != 0)
                                {
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 3;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 3;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
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
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 3;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 3;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 3;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 3;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 4:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 3;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 3;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 3;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 3;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 3;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 3;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "hhh")
                    {
                        switch (TempSelect.temp)
                        {
                            case 2:
                                if (firstselect == 0)
                                {
                                    firstselect = 3;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 3;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 0)
                                {
                                    firstselect = 3;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 3;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    thirdselect = 3;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    fourthselect = 3;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 3;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 3;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 3;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 3;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 3;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 3;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 3;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 3;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 3;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 3;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 3;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 3;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    thirdpicclickcheck = 0;
                    thirdpicborder.Opacity = 0;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                break;
                            case 2:
                            case 3:
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 3)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 3)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 3)
                                {
                                    fourthselect = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "dearpic2")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 3)
                                {
                                    secondselect = 0;
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
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 3)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 3)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 3)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 4:
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 3)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 3)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 3)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 3)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 3)
                                {
                                    sixthselect = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "hhh")
                    {
                        switch (TempSelect.temp)
                        {
                            case 2:
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 3)
                                {
                                    secondselect = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 3)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 3)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 3)
                                {
                                    fourthselect = 0;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 3)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 3)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 3)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 3)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 3)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 3)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 3)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 3)
                                {
                                    sixthselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 3)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 3)
                                {
                                    secondselect = 0;
                                }
                                break;
                        }
                    }
                }

                PriewviewImgShow();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Fourthpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (fourthpicclickcheck == 0)
                {
                    fourthpicclickcheck = 1;
                    fourthpicborder.Opacity = 1;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 4;
                                }
                                else if (firstselect != 0)
                                {
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                }
                                break;
                            case 2:
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 4;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 4;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 4;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 4;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "dearpic2")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 4;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 4;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
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
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 4;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 4;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 4;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 4;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 4:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 4;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 4;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 4;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 4;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 4;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 4;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "hhh")
                    {
                        switch(TempSelect.temp)
                        {
                            case 2:
                                if (firstselect == 0)
                                {
                                    firstselect = 4;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 4;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 0)
                                {
                                    firstselect = 4;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    secondselect = 4;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    thirdselect = 4;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    fourthselect = 4;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 4;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 4;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 4;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 4;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 4;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 4;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 4;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 4;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 4;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 4;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 4;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 4;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    fourthpicclickcheck = 0;
                    fourthpicborder.Opacity = 0;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 4)
                                {
                                    firstselect = 0;
                                }
                                break;
                            case 2:
                            case 3:
                                if (firstselect == 4)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 4)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 4)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 4)
                                {
                                    fourthselect = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "dearpic2")
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                break;
                            case 2:
                                if (firstselect == 4)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 4)
                                {
                                    secondselect = 0;
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
                                if (firstselect == 4)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 4)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 4)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 4)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 4:
                                if (firstselect == 4)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 4)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 4)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 4)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 4)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 4)
                                {
                                    sixthselect = 0;
                                }
                                break;
                        }
                    }
                    else if (MainWindow.inifoldername == "hhh")
                    {
                        switch (TempSelect.temp)
                        {
                            case 2:
                                if (firstselect == 4)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 4)
                                {
                                    secondselect = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 4)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 4)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 4)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 4)
                                {
                                    fourthselect = 0;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 4)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 4)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 4)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 4)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 4)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 4)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 4)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 4)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 4)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 4)
                                {
                                    sixthselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 4)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 4)
                                {
                                    secondselect = 0;
                                }
                                break;
                        }
                    }
                }

                PriewviewImgShow();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Fifthpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (fifthpicclickcheck == 0)
                {
                    fifthpicclickcheck = 1;
                    fifthpicborder.Opacity = 1;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 2:
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 5;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 5;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 5;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 5;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    fifthpicclickcheck = 0;
                                    fifthpicborder.Opacity = 0;
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
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 5;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 5;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 5;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 5;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    fifthpicclickcheck = 0;
                                    fifthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 4:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 5;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 5;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 5;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 5;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 5;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 5;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    fifthpicclickcheck = 0;
                                    fifthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 5;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 5;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 5;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 5;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    fifthpicclickcheck = 0;
                                    fifthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 5;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 5;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 5;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 5;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 5;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 5;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    fifthpicclickcheck = 0;
                                    fifthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 5;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 5;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    fifthpicclickcheck = 0;
                                    fifthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    fifthpicclickcheck = 0;
                    fifthpicborder.Opacity = 0;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 2:
                            case 3:
                                if (firstselect == 5)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 5)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 5)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 5)
                                {
                                    fourthselect = 0;
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
                                if (firstselect == 5)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 5)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 5)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 5)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 4:
                                if (firstselect == 5)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 5)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 5)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 5)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 5)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 5)
                                {
                                    sixthselect = 0;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 5)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 5)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 5)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 5)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 5)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 5)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 5)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 5)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 5)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 5)
                                {
                                    sixthselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 5)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 5)
                                {
                                    secondselect = 0;
                                }
                                break;
                        }
                    }
                }

                PriewviewImgShow();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Sixthpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (sixthpicclickcheck == 0)
                {
                    sixthpicborder.Opacity = 1;
                    sixthpicclickcheck = 1;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 2:
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 6;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 6;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 6;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 6;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    sixthpicclickcheck = 0;
                                    sixthpicborder.Opacity = 0;
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
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 6;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 6;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 6;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 6;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    sixthpicclickcheck = 0;
                                    sixthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 4:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 6;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 6;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 6;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 6;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 6;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 6;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    sixthpicclickcheck = 0;
                                    sixthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 6;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 6;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 6;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 6;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    sixthpicclickcheck = 0;
                                    sixthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 6;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 6;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 6;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 6;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 6;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 6;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    sixthpicclickcheck = 0;
                                    sixthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 6;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 6;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    sixthpicclickcheck = 0;
                                    sixthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    sixthpicclickcheck = 0;
                    sixthpicborder.Opacity = 0;
                    if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                    {
                        switch (TempSelect.temp)
                        {
                            case 2:
                            case 3:
                                if (firstselect == 6)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 6)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 6)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 6)
                                {
                                    fourthselect = 0;
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
                                if (firstselect == 6)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 6)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 6)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 6)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 4:
                                if (firstselect == 6)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 6)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 6)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 6)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 6)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 6)
                                {
                                    sixthselect = 0;
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 6)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 6)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 6)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 6)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 6)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 6)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 6)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 6)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 6)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 6)
                                {
                                    sixthselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 6)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 6)
                                {
                                    secondselect = 0;
                                }
                                break;
                        }
                    }
                }

                PriewviewImgShow();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Seventhpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (seventhpicclickcheck == 0)
                {
                    seventhpicborder.Opacity = 1;
                    seventhpicclickcheck = 1;
                    if (MainWindow.inifoldername == "tech")
                    {
                        if (TempSelect.temp == 4)
                        {
                            if (firstselect == 0)
                            {
                                
                                firstselect = 7;
                            }
                            else if (firstselect != 0 && secondselect == 0)
                            {
                                
                                secondselect = 7;
                            }
                            else if (secondselect != 0 && thirdselect == 0)
                            {
                                
                                thirdselect = 7;
                            }
                            else if (thirdselect != 0 && fourthselect == 0)
                            {
                                
                                fourthselect = 7;
                            }
                            else if (fourthselect != 0 && fifthselect == 0)
                            {
                                
                                fifthselect = 7;
                            }
                            else if (fifthselect != 0 && sixthselect == 0)
                            {
                                
                                sixthselect = 7;
                            }
                            else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                            {
                                seventhpicclickcheck = 0;
                                seventhpicborder.Opacity = 0;
                            }
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 7;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 7;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 7;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 7;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    seventhpicclickcheck = 0;
                                    seventhpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 7;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 7;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 7;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 7;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 7;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 7;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    seventhpicclickcheck = 0;
                                    seventhpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 7;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 7;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    seventhpicclickcheck = 0;
                                    seventhpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    seventhpicclickcheck = 0;
                    seventhpicborder.Opacity = 0;
                    if (MainWindow.inifoldername == "tech")
                    {
                        if (TempSelect.temp == 4)
                        {
                            if (firstselect == 7)
                            {
                                firstselect = 0;
                            }
                            else if (secondselect == 7)
                            {
                                secondselect = 0;
                            }
                            else if (thirdselect == 7)
                            {
                                thirdselect = 0;
                            }
                            else if (fourthselect == 7)
                            {
                                fourthselect = 0;
                            }
                            else if (fifthselect == 7)
                            {
                                fifthselect = 0;
                            }
                            else if (sixthselect == 7)
                            {
                                sixthselect = 0;
                            }
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 7)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 7)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 7)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 7)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 7)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 7)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 7)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 7)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 7)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 7)
                                {
                                    sixthselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 7)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 7)
                                {
                                    secondselect = 0;
                                }
                                break;
                        }
                    }
                }

                PriewviewImgShow();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Eighthpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                if (eighthpicclickcheck == 0)
                {
                    eighthpicborder.Opacity = 1;
                    eighthpicclickcheck = 1;
                    if (MainWindow.inifoldername == "tech")
                    {
                        if (TempSelect.temp == 4)
                        {
                            if (firstselect == 0)
                            {
                                
                                firstselect = 8;
                            }
                            else if (firstselect != 0 && secondselect == 0)
                            {
                                
                                secondselect = 8;
                            }
                            else if (secondselect != 0 && thirdselect == 0)
                            {
                                
                                thirdselect = 8;
                            }
                            else if (thirdselect != 0 && fourthselect == 0)
                            {
                                
                                fourthselect = 8;
                            }
                            else if (fourthselect != 0 && fifthselect == 0)
                            {
                                
                                fifthselect = 8;
                            }
                            else if (fifthselect != 0 && sixthselect == 0)
                            {
                                
                                sixthselect = 8;
                            }
                            else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                            {
                                eighthpicclickcheck = 0;
                                eighthpicborder.Opacity = 0;
                            }
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 8;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 8;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 8;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 8;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    eighthpicclickcheck = 0;
                                    eighthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 3:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 8;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 8;
                                }
                                else if (secondselect != 0 && thirdselect == 0)
                                {
                                    
                                    thirdselect = 8;
                                }
                                else if (thirdselect != 0 && fourthselect == 0)
                                {
                                    
                                    fourthselect = 8;
                                }
                                else if (fourthselect != 0 && fifthselect == 0)
                                {
                                    
                                    fifthselect = 8;
                                }
                                else if (fifthselect != 0 && sixthselect == 0)
                                {
                                    
                                    sixthselect = 8;
                                }
                                else if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                                {
                                    eighthpicclickcheck = 0;
                                    eighthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                            case 2:
                                if (firstselect == 0)
                                {
                                    
                                    firstselect = 8;
                                }
                                else if (firstselect != 0 && secondselect == 0)
                                {
                                    
                                    secondselect = 8;
                                }
                                else if (firstselect != 0 && secondselect != 0)
                                {
                                    eighthpicclickcheck = 0;
                                    eighthpicborder.Opacity = 0;
                                    break;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    eighthpicclickcheck = 0;
                    eighthpicborder.Opacity = 0;
                    if (MainWindow.inifoldername == "tech")
                    {
                        if (TempSelect.temp == 4)
                        {
                            if (firstselect == 8)
                            {
                                firstselect = 0;
                            }
                            else if (secondselect == 8)
                            {
                                secondselect = 0;
                            }
                            else if (thirdselect == 8)
                            {
                                thirdselect = 0;
                            }
                            else if (fourthselect == 8)
                            {
                                fourthselect = 0;
                            }
                            else if (fifthselect == 8)
                            {
                                fifthselect = 0;
                            }
                            else if (sixthselect == 8)
                            {
                                sixthselect = 0;
                            }
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 8)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 8)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 8)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 8)
                                {
                                    fourthselect = 0;
                                }
                                break;
                            case 3:
                                if (firstselect == 8)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 8)
                                {
                                    secondselect = 0;
                                }
                                else if (thirdselect == 8)
                                {
                                    thirdselect = 0;
                                }
                                else if (fourthselect == 8)
                                {
                                    fourthselect = 0;
                                }
                                else if (fifthselect == 8)
                                {
                                    fifthselect = 0;
                                }
                                else if (sixthselect == 8)
                                {
                                    sixthselect = 0;
                                }
                                break;
                            case 2:
                                if (firstselect == 8)
                                {
                                    firstselect = 0;
                                }
                                else if (secondselect == 8)
                                {
                                    secondselect = 0;
                                }
                                break;
                        }
                    }
                }

                PriewviewImgShow();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Timer_Tik(object sender, EventArgs e)
        {
            selectduration--;
            Timer.Text = selectduration.ToString();

            if (selectduration == 0)
            {
                nextpage.IsEnabled = false;
            }

            if (selectduration == -1)
            {
                timer.Stop();
                dispose();
                Random rand;
                int i;
                int zeroCount;
                List<int> availlableNumber;
                List<int> selects;
                List<int> selectedNumbers;
                if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            if (firstselect == 0)
                            {
                                rand = new Random();
                                int randdomnum = rand.Next(1, 4);
                                firstselect = randdomnum;
                            }
                            break;
                        case 2:
                        case 3:
                            rand = new Random();
                            availlableNumber = Enumerable.Range(1, 6).ToList();

                            if (firstselect != 0)
                            {
                                availlableNumber.Remove(firstselect);
                            }
                            if (secondselect != 0)
                            {
                                availlableNumber.Remove(secondselect);
                            }
                            if (thirdselect != 0)
                            {
                                availlableNumber.Remove(thirdselect);
                            }
                            if (fourthselect != 0)
                            {
                                availlableNumber.Remove(fourthselect);
                            }

                            // 0인 변수 개수 계산
                            zeroCount = new List<int> { firstselect, secondselect, thirdselect, fourthselect }.Count(x => x == 0);

                            // 0인 변수 개수만큼 랜덤 숫자 선택
                            selectedNumbers = availlableNumber.OrderBy(x => rand.Next()).Take(zeroCount).ToList();

                            // 랜덤한 값 할당
                            selects = new List<int> { firstselect, secondselect, thirdselect, fourthselect };
                            i = 0;
                            foreach (var number in selectedNumbers)
                            {
                                while (selects[i] != 0)
                                {
                                    i++;
                                }
                                selects[i] = number;
                                i++; // 다음 0인 위치로 이동
                            }

                            // 할당된 값으로 변수 업데이트
                            firstselect = selects[0];
                            secondselect = selects[1];
                            thirdselect = selects[2];
                            fourthselect = selects[3];
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "dearpic2")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            if (firstselect == 0)
                            {
                                rand = new Random();
                                int randdomnum = rand.Next(1, 3);
                                firstselect = randdomnum;
                            }
                            break;
                        case 2:
                            rand = new Random();
                            availlableNumber = Enumerable.Range(1, 4).ToList();

                            if (firstselect != 0)
                            {
                                availlableNumber.Remove(firstselect);
                            }
                            if (secondselect != 0)
                            {
                                availlableNumber.Remove(secondselect);
                            }

                            // 0인 변수 개수 계산
                            zeroCount = new List<int> { firstselect, secondselect }.Count(x => x == 0);

                            // 0인 변수 개수만큼 랜덤 숫자 선택
                            selectedNumbers = availlableNumber.OrderBy(x => rand.Next()).Take(zeroCount).ToList();

                            // 랜덤한 값 할당
                            selects = new List<int> { firstselect, secondselect };
                            i = 0;
                            foreach (var number in selectedNumbers)
                            {
                                while (selects[i] != 0)
                                {
                                    i++;
                                }
                                selects[i] = number;
                                i++; // 다음 0인 위치로 이동
                            }

                            // 할당된 값으로 변수 업데이트
                            firstselect = selects[0];
                            secondselect = selects[1];
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
                            rand = new Random();
                            availlableNumber = Enumerable.Range(1, 6).ToList();

                            if (firstselect != 0)
                            {
                                availlableNumber.Remove(firstselect);
                            }
                            if (secondselect != 0)
                            {
                                availlableNumber.Remove(secondselect);
                            }
                            if (thirdselect != 0)
                            {
                                availlableNumber.Remove(thirdselect);
                            }
                            if (fourthselect != 0)
                            {
                                availlableNumber.Remove(fourthselect);
                            }

                            // 0인 변수 개수 계산
                            zeroCount = new List<int> { firstselect, secondselect, thirdselect, fourthselect }.Count(x => x == 0);

                            // 0인 변수 개수만큼 랜덤 숫자 선택
                            selectedNumbers = availlableNumber.OrderBy(x => rand.Next()).Take(zeroCount).ToList();

                            // 랜덤한 값 할당
                            selects = new List<int> { firstselect, secondselect, thirdselect, fourthselect };
                            i = 0;
                            foreach (var number in selectedNumbers)
                            {
                                while (selects[i] != 0)
                                {
                                    i++;
                                }
                                selects[i] = number;
                                i++; // 다음 0인 위치로 이동
                            }

                            // 할당된 값으로 변수 업데이트
                            firstselect = selects[0];
                            secondselect = selects[1];
                            thirdselect = selects[2];
                            fourthselect = selects[3];
                            break;
                        case 4:
                            rand = new Random();
                            availlableNumber = Enumerable.Range(1, 8).ToList();

                            if (firstselect != 0)
                            {
                                availlableNumber.Remove(firstselect);
                            }
                            if (secondselect != 0)
                            {
                                availlableNumber.Remove(secondselect);
                            }
                            if (thirdselect != 0)
                            {
                                availlableNumber.Remove(thirdselect);
                            }
                            if (fourthselect != 0)
                            {
                                availlableNumber.Remove(fourthselect);
                            }
                            if (fifthselect != 0)
                            {
                                availlableNumber.Remove(fifthselect);
                            }
                            if (sixthselect != 0)
                            {
                                availlableNumber.Remove(sixthselect);
                            }

                            // 0인 변수 개수 계산
                            zeroCount = new List<int> { firstselect, secondselect, thirdselect, fourthselect, fifthselect, sixthselect }.Count(x => x == 0);

                            // 0인 변수 개수만큼 랜덤 숫자 선택
                            selectedNumbers = availlableNumber.OrderBy(x => rand.Next()).Take(zeroCount).ToList();

                            // 랜덤한 값 할당
                            selects = new List<int> { firstselect, secondselect, thirdselect, fourthselect, fifthselect, sixthselect };
                            i = 0;
                            foreach (var number in selectedNumbers)
                            {
                                while (selects[i] != 0)
                                {
                                    i++;
                                }
                                selects[i] = number;
                                i++; // 다음 0인 위치로 이동
                            }

                            // 할당된 값으로 변수 업데이트
                            firstselect = selects[0];
                            secondselect = selects[1];
                            thirdselect = selects[2];
                            fourthselect = selects[3];
                            fifthselect = selects[4];
                            sixthselect = selects[5];
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "hhh")
                {
                    if (MainWindow.optiontempnum == 1)
                    {
                        if (firstselect == 0)
                        {
                            rand = new Random();
                            int randdomnum = rand.Next(1, 2);
                            firstselect = randdomnum;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 0)
                                {
                                    rand = new Random();
                                    int randdomnum = rand.Next(1, 2);
                                    firstselect = randdomnum;
                                }
                                break;
                            case 2:
                                rand = new Random();
                                availlableNumber = Enumerable.Range(1, 4).ToList();

                                if (firstselect != 0)
                                {
                                    availlableNumber.Remove(firstselect);
                                }
                                if (secondselect != 0)
                                {
                                    availlableNumber.Remove(secondselect);
                                }

                                // 0인 변수 개수 계산
                                zeroCount = new List<int> { firstselect, secondselect }.Count(x => x == 0);

                                // 0인 변수 개수만큼 랜덤 숫자 선택
                                selectedNumbers = availlableNumber.OrderBy(x => rand.Next()).Take(zeroCount).ToList();

                                // 랜덤한 값 할당
                                selects = new List<int> { firstselect, secondselect };
                                i = 0;
                                foreach (var number in selectedNumbers)
                                {
                                    while (selects[i] != 0)
                                    {
                                        i++;
                                    }
                                    selects[i] = number;
                                    i++; // 다음 0인 위치로 이동
                                }

                                // 할당된 값으로 변수 업데이트
                                firstselect = selects[0];
                                secondselect = selects[1];
                                break;
                            case 3:
                                rand = new Random();
                                availlableNumber = Enumerable.Range(1, 4).ToList();

                                if (firstselect != 0)
                                {
                                    availlableNumber.Remove(firstselect);
                                }
                                if (secondselect != 0)
                                {
                                    availlableNumber.Remove(secondselect);
                                }
                                if (thirdselect != 0)
                                {
                                    availlableNumber.Remove(thirdselect);
                                }
                                if (fourthselect != 0)
                                {
                                    availlableNumber.Remove(fourthselect);
                                }

                                // 0인 변수 개수 계산
                                zeroCount = new List<int> { firstselect, secondselect, thirdselect, fourthselect }.Count(x => x == 0);

                                // 0인 변수 개수만큼 랜덤 숫자 선택
                                selectedNumbers = availlableNumber.OrderBy(x => rand.Next()).Take(zeroCount).ToList();

                                // 랜덤한 값 할당
                                selects = new List<int> { firstselect, secondselect, thirdselect, fourthselect };
                                i = 0;
                                foreach (var number in selectedNumbers)
                                {
                                    while (selects[i] != 0)
                                    {
                                        i++;
                                    }
                                    selects[i] = number;
                                    i++; // 다음 0인 위치로 이동
                                }

                                // 할당된 값으로 변수 업데이트
                                firstselect = selects[0];
                                secondselect = selects[1];
                                thirdselect = selects[2];
                                fourthselect = selects[3];
                                break;
                        }
                    }
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            rand = new Random();
                            availlableNumber = Enumerable.Range(1, 6).ToList();

                            if (firstselect != 0)
                            {
                                availlableNumber.Remove(firstselect);
                            }
                            if (secondselect != 0)
                            {
                                availlableNumber.Remove(secondselect);
                            }
                            if (thirdselect != 0)
                            {
                                availlableNumber.Remove(thirdselect);
                            }
                            if (fourthselect != 0)
                            {
                                availlableNumber.Remove(fourthselect);
                            }

                            // 0인 변수 개수 계산
                            zeroCount = new List<int> { firstselect, secondselect, thirdselect, fourthselect }.Count(x => x == 0);

                            // 0인 변수 개수만큼 랜덤 숫자 선택
                            selectedNumbers = availlableNumber.OrderBy(x => rand.Next()).Take(zeroCount).ToList();

                            // 랜덤한 값 할당
                            selects = new List<int> { firstselect, secondselect, thirdselect, fourthselect };
                            i = 0;
                            foreach (var number in selectedNumbers)
                            {
                                while (selects[i] != 0)
                                {
                                    i++;
                                }
                                selects[i] = number;
                                i++; // 다음 0인 위치로 이동
                            }

                            // 할당된 값으로 변수 업데이트
                            firstselect = selects[0];
                            secondselect = selects[1];
                            thirdselect = selects[2];
                            fourthselect = selects[3];
                            break;
                        case 2:
                            rand = new Random();
                            availlableNumber = Enumerable.Range(1, 4).ToList();

                            if (firstselect != 0)
                            {
                                availlableNumber.Remove(firstselect);
                            }
                            if (secondselect != 0)
                            {
                                availlableNumber.Remove(secondselect);
                            }

                            // 0인 변수 개수 계산
                            zeroCount = new List<int> { firstselect, secondselect }.Count(x => x == 0);

                            // 0인 변수 개수만큼 랜덤 숫자 선택
                            selectedNumbers = availlableNumber.OrderBy(x => rand.Next()).Take(zeroCount).ToList();

                            // 랜덤한 값 할당
                            selects = new List<int> { firstselect, secondselect };
                            i = 0;
                            foreach (var number in selectedNumbers)
                            {
                                while (selects[i] != 0)
                                {
                                    i++;
                                }
                                selects[i] = number;
                                i++; // 다음 0인 위치로 이동
                            }

                            // 할당된 값으로 변수 업데이트
                            firstselect = selects[0];
                            secondselect = selects[1];
                            break;
                        case 3:
                            rand = new Random();
                            availlableNumber = Enumerable.Range(1, 8).ToList();

                            if (firstselect != 0)
                            {
                                availlableNumber.Remove(firstselect);
                            }
                            if (secondselect != 0)
                            {
                                availlableNumber.Remove(secondselect);
                            }
                            if (thirdselect != 0)
                            {
                                availlableNumber.Remove(thirdselect);
                            }
                            if (fourthselect != 0)
                            {
                                availlableNumber.Remove(fourthselect);
                            }
                            if (fifthselect != 0)
                            {
                                availlableNumber.Remove(fifthselect);
                            }
                            if (sixthselect != 0)
                            {
                                availlableNumber.Remove(sixthselect);
                            }

                            // 0인 변수 개수 계산
                            zeroCount = new List<int> { firstselect, secondselect, thirdselect, fourthselect, fifthselect, sixthselect }.Count(x => x == 0);

                            // 0인 변수 개수만큼 랜덤 숫자 선택
                            selectedNumbers = availlableNumber.OrderBy(x => rand.Next()).Take(zeroCount).ToList();

                            // 랜덤한 값 할당
                            selects = new List<int> { firstselect, secondselect, thirdselect, fourthselect, fifthselect, sixthselect };
                            i = 0;
                            foreach (var number in selectedNumbers)
                            {
                                while (selects[i] != 0)
                                {
                                    i++;
                                }
                                selects[i] = number;
                                i++; // 다음 0인 위치로 이동
                            }

                            // 할당된 값으로 변수 업데이트
                            firstselect = selects[0];
                            secondselect = selects[1];
                            thirdselect = selects[2];
                            fourthselect = selects[3];
                            fifthselect = selects[4];
                            sixthselect = selects[5];
                            break;
                    }
                }
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                nextpage.IsEnabled = false;
                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        private void dispose()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");


                if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            backgroundimg.Source = MainWindow.Selectpicpage1;

                            Firstpic = null;
                            Secondpic = null;
                            Thirdpic = null;
                            Fourthpic = null;
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
                            break;
                        case 2:
                            backgroundimg.Source = MainWindow.Selectpicpage2;

                            Firstpic = null;
                            Secondpic = null;
                            Thirdpic = null;
                            Fourthpic = null;
                            Fifthpic = null;
                            Sixthpic = null;
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
                            MainWindow.fifthphoto = null;
                            MainWindow.sixthphoto = null;
                            break;
                        case 3:
                            backgroundimg.Source = MainWindow.Selectpicpage3;

                            Firstpic = null;
                            Secondpic = null;
                            Thirdpic = null;
                            Fourthpic = null;
                            Fifthpic = null;
                            Sixthpic = null;
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
                            MainWindow.fifthphoto = null;
                            MainWindow.sixthphoto = null;
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "dearpic2")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:

                            Firstpic = null;
                            Secondpic = null;
                            Thirdpic = null;
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            break;
                        case 2:

                            Firstpic = null;
                            Secondpic = null;
                            Thirdpic = null;
                            Fourthpic = null;
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
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

                            Firstpic = null;
                            Secondpic = null;
                            Thirdpic = null;
                            Fourthpic = null;
                            Fifthpic = null;
                            Sixthpic = null;
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
                            MainWindow.fifthphoto = null;
                            MainWindow.sixthphoto = null;
                            break;
                        case 4:

                            Firstpic = null;
                            Secondpic = null;
                            Thirdpic = null;
                            Fourthpic = null;
                            Fifthpic = null;
                            Sixthpic = null;
                            Seventhpic = null;
                            Eighthpic = null;
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
                            MainWindow.fifthphoto = null;
                            MainWindow.sixthphoto = null;
                            MainWindow.seventhphoto = null;
                            MainWindow.eighthphoto = null;
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "hhh")
                {
                    if (MainWindow.optiontempnum == 1)
                    {
                        Firstpic = null;
                        Secondpic = null;
                        MainWindow.firstphoto = null;
                        MainWindow.secondphoto = null;
                        pic1.Dispose();
                        pic2.Dispose();
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                Firstpic = null;
                                Secondpic = null;
                                MainWindow.firstphoto = null;
                                MainWindow.secondphoto = null;
                                pic1.Dispose();
                                pic2.Dispose();
                                break;
                            case 2:
                                Firstpic = null;
                                Secondpic = null;
                                Thirdpic = null;
                                Fourthpic = null;
                                MainWindow.firstphoto = null;
                                MainWindow.secondphoto = null;
                                MainWindow.thirdphoto = null;
                                MainWindow.fourthphoto = null;
                                pic1.Dispose();
                                pic2.Dispose();
                                pic3.Dispose();
                                pic4.Dispose();
                                break;
                            case 3:
                                Firstpic = null;
                                Secondpic = null;
                                Thirdpic = null;
                                Fourthpic = null;
                                MainWindow.firstphoto = null;
                                MainWindow.secondphoto = null;
                                MainWindow.thirdphoto = null;
                                MainWindow.fourthphoto = null;
                                pic1.Dispose();
                                pic2.Dispose();
                                pic3.Dispose();
                                pic4.Dispose();
                                break;
                        }
                    }
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:

                            Firstpic = null;
                            Secondpic = null;
                            Thirdpic = null;
                            Fourthpic = null;
                            Fifthpic = null;
                            Sixthpic = null;
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
                            MainWindow.fifthphoto = null;
                            MainWindow.sixthphoto = null;
                            break;
                        case 3:

                            Firstpic = null;
                            Secondpic = null;
                            Thirdpic = null;
                            Fourthpic = null;
                            Fifthpic = null;
                            Sixthpic = null;
                            Seventhpic = null;
                            Eighthpic = null;
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
                            MainWindow.fifthphoto = null;
                            MainWindow.sixthphoto = null;
                            MainWindow.seventhphoto = null;
                            MainWindow.eighthphoto = null;
                            break;
                        case 2:

                            Firstpic = null;
                            Secondpic = null;
                            Thirdpic = null;
                            Fourthpic = null;
                            MainWindow.firstphoto = null;
                            MainWindow.secondphoto = null;
                            MainWindow.thirdphoto = null;
                            MainWindow.fourthphoto = null;
                            break;
                    }
                }

                priewviewimg.Source = null;
                priewviewimg = null;

                timer.Tick -= new EventHandler(Timer_Tik);
                timer = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void nextpage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");
                nextpage.IsEnabled = false;
                if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            if (firstselect != 0)
                            {
                                timer.Stop();
                                dispose();
                                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                nextpage.IsEnabled = true;
                            }
                            break;
                        case 3:
                        case 2:
                            if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                            {
                                timer.Stop();
                                dispose();
                                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                nextpage.IsEnabled = true;
                            }
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "dearpic2")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            if (firstselect != 0)
                            {
                                timer.Stop();
                                dispose();
                                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                nextpage.IsEnabled = true;
                            }
                            break;
                        case 2:
                            if (firstselect != 0 && secondselect != 0)
                            {
                                timer.Stop();
                                dispose();
                                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                nextpage.IsEnabled = true;
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
                            if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                            {
                                timer.Stop();
                                dispose();
                                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                nextpage.IsEnabled = true;
                            }
                            break;
                        case 4:
                            if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                            {
                                timer.Stop();
                                dispose();
                                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                nextpage.IsEnabled = true;
                            }
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "hhh")
                {
                    if (MainWindow.optiontempnum == 1)
                    {
                        if (firstselect != 0)
                        {
                            timer.Stop();
                            dispose();
                            NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            nextpage.IsEnabled = true;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect != 0)
                                {
                                    timer.Stop();
                                    dispose();
                                    NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                                }
                                else
                                {
                                    nextpage.IsEnabled = true;
                                }
                                break;
                            case 2:
                                if (firstselect != 0 && secondselect != 0)
                                {
                                    timer.Stop();
                                    dispose();
                                    NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                                }
                                else
                                {
                                    nextpage.IsEnabled = true;
                                }
                                break;
                            case 3:
                                if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                                {
                                    timer.Stop();
                                    dispose();
                                    NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                                }
                                else
                                {
                                    nextpage.IsEnabled = true;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0)
                            {
                                timer.Stop();
                                dispose();
                                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                nextpage.IsEnabled = true;
                            }
                            break;
                        case 3:
                            if (firstselect != 0 && secondselect != 0 && thirdselect != 0 && fourthselect != 0 && fifthselect != 0 && sixthselect != 0)
                            {
                                timer.Stop();
                                dispose();
                                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                nextpage.IsEnabled = true;
                            }
                            break;
                        case 2:
                            if (firstselect != 0 && secondselect != 0)
                            {
                                timer.Stop();
                                dispose();
                                NavigationService.Navigate(new Uri("View/Preparing.xaml", UriKind.RelativeOrAbsolute));
                            }
                            else
                            {
                                nextpage.IsEnabled = true;
                            }
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

        private void PriewviewImgShow()
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (priewviewimg.Source != null)
                {
                    priewviewimg.Source = null;
                }

                if (MainWindow.inifoldername == "dearpic3" || MainWindow.inifoldername == "dearpic4")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            using (MainWindow.g23 = Graphics.FromImage(MainWindow.canvus2_3))
                            {
                                MainWindow.g23.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 64, 70, 1074, 1608);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 64, 70, 1074, 1608);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 64, 70, 1074, 1608);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 64, 70, 1074, 1608);
                                        break;
                                }
                                MainWindow.g23.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                                MainWindow.g23.Dispose();
                            }

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.canvus2_3.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 397;
                                bi.DecodePixelHeight = 593;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                        case 2:
                            using (MainWindow.g23 = Graphics.FromImage(MainWindow.canvus2_3))
                            {
                                MainWindow.g23.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 56, 127, 529, 794);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 56, 127, 529, 794);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 56, 127, 529, 794);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 56, 127, 529, 794);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 56, 127, 529, 794);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 56, 127, 529, 794);
                                        break;
                                }
                                switch (secondselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 615, 67, 529, 794);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 615, 67, 529, 794);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 615, 67, 529, 794);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 615, 67, 529, 794);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 615, 67, 529, 794);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 615, 67, 529, 794);
                                        break;
                                }
                                switch (thirdselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 56, 949, 529, 794);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 56, 949, 529, 794);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 56, 949, 529, 794);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 56, 949, 529, 794);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 56, 949, 529, 794);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 56, 949, 529, 794);
                                        break;
                                }
                                switch (fourthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 615, 888, 529, 794);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 615, 888, 529, 794);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 615, 888, 529, 794);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 615, 888, 529, 794);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 615, 888, 529, 794);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 615, 888, 529, 794);
                                        break;
                                }
                                MainWindow.g23.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                                MainWindow.g23.Dispose();
                            }

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.canvus2_3.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 397;
                                bi.DecodePixelHeight = 593;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                        case 3:
                            using (MainWindow.g23 = Graphics.FromImage(MainWindow.canvus2_3))
                            {
                                MainWindow.g23.DrawImage(MainWindow.temp1_1, 0, 0, 1200, 1800);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 61, 64, 529, 794);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 61, 64, 529, 794);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 61, 64, 529, 794);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 61, 64, 529, 794);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 61, 64, 529, 794);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 61, 64, 529, 794);
                                        break;
                                }
                                switch (secondselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 611, 64, 529, 794);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 611, 64, 529, 794);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 611, 64, 529, 794);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 611, 64, 529, 794);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 611, 64, 529, 794);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 611, 64, 529, 794);
                                        break;
                                }
                                switch (thirdselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 61, 880, 529, 794);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 61, 880, 529, 794);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 61, 880, 529, 794);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 61, 880, 529, 794);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 61, 880, 529, 794);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 61, 880, 529, 794);
                                        break;
                                }
                                switch (fourthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 611, 880, 529, 794);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 611, 880, 529, 794);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 611, 880, 529, 794);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 611, 880, 529, 794);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 611, 880, 529, 794);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 611, 880, 529, 794);
                                        break;
                                }
                                MainWindow.g23.DrawImage(MainWindow.temp1_1Front, 0, 0, 1200, 1800);

                                MainWindow.g23.Dispose();
                            }

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.canvus2_3.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 397;
                                bi.DecodePixelHeight = 593;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "dearpic2")
                {
                    switch(TempSelect.temp)
                    {
                        case 1:
                            using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                            {
                                MainWindow.r.DrawImage(MainWindow.temp1_1, 0, 0, 1800, 1200);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.r.DrawImage(pic1, 30, 30, 1140, 1140);
                                        break;
                                    case 2:
                                        MainWindow.r.DrawImage(pic2, 30, 30, 1140, 1140);
                                        break;
                                    case 3:
                                        MainWindow.r.DrawImage(pic3, 30, 30, 1140, 1140);
                                        break;
                                }
                                MainWindow.r.DrawImage(MainWindow.temp1_1Front, 0, 0, 1800, 1200);
                            }
                            MainWindow.r.Dispose();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.bigcanvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 593;
                                bi.DecodePixelHeight = 397;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                        case 2:
                            using (MainWindow.g23 = Graphics.FromImage(MainWindow.canvus2_3))
                            {
                                MainWindow.g23.DrawImage(MainWindow.temp2_1, 0, 0, 1200, 1800);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 30, 1140, 760);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 30, 1140, 760);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 30, 1140, 760);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 30, 1140, 760);
                                        break;
                                }
                                switch (secondselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 820, 1140, 760);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 820, 1140, 760);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 820, 1140, 760);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 820, 1140, 760);
                                        break;
                                }
                                MainWindow.g23.DrawImage(MainWindow.temp2_1Front, 0, 0, 1200, 1800);
                            }
                            MainWindow.g23.Dispose();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.canvus2_3.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 397;
                                bi.DecodePixelHeight = 593;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "tech")
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                            {
                                MainWindow.d.DrawImage(MainWindow.temp1_1, 0, 0, 600, 1800);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.d.DrawImage(pic1, 30, 30, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.d.DrawImage(pic2, 30, 30, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.d.DrawImage(pic3, 30, 30, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.d.DrawImage(pic4, 30, 30, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.d.DrawImage(pic5, 30, 30, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.d.DrawImage(pic6, 30, 30, 540, 360);
                                        break;
                                }
                                switch (secondselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.d.DrawImage(pic1, 30, 420, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.d.DrawImage(pic2, 30, 420, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.d.DrawImage(pic3, 30, 420, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.d.DrawImage(pic4, 30, 420, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.d.DrawImage(pic5, 30, 420, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.d.DrawImage(pic6, 30, 420, 540, 360);
                                        break;
                                }
                                switch (thirdselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.d.DrawImage(pic1, 30, 810, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.d.DrawImage(pic2, 30, 810, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.d.DrawImage(pic3, 30, 810, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.d.DrawImage(pic4, 30, 810, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.d.DrawImage(pic5, 30, 810, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.d.DrawImage(pic6, 30, 810, 540, 360);
                                        break;
                                }
                                switch (fourthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.d.DrawImage(pic1, 30, 1200, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.d.DrawImage(pic2, 30, 1200, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.d.DrawImage(pic3, 30, 1200, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.d.DrawImage(pic4, 30, 1200, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.d.DrawImage(pic5, 30, 1200, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.d.DrawImage(pic6, 30, 1200, 540, 360);
                                        break;
                                }
                                MainWindow.d.DrawImage(MainWindow.temp1_1Front, 0, 0, 600, 1800);
                            }
                            MainWindow.d.Dispose();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.canvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 206;
                                bi.DecodePixelHeight = 590;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                        case 2:
                            using (MainWindow.r = Graphics.FromImage(MainWindow.bigcanvus))
                            {
                                MainWindow.r.DrawImage(MainWindow.temp2_1, 0, 0, 1800, 1200);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.r.DrawImage(pic1, 57, 95, 714, 477);
                                        break;
                                    case 2:
                                        MainWindow.r.DrawImage(pic2, 57, 95, 714, 477);
                                        break;
                                    case 3:
                                        MainWindow.r.DrawImage(pic3, 57, 95, 714, 477);
                                        break;
                                    case 4:
                                        MainWindow.r.DrawImage(pic4, 57, 95, 714, 477);
                                        break;
                                    case 5:
                                        MainWindow.r.DrawImage(pic5, 57, 95, 714, 477);
                                        break;
                                    case 6:
                                        MainWindow.r.DrawImage(pic6, 57, 95, 714, 477);
                                        break;
                                }
                                switch (secondselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.r.DrawImage(pic1, 832, 95, 714, 477);
                                        break;
                                    case 2:
                                        MainWindow.r.DrawImage(pic2, 832, 95, 714, 477);
                                        break;
                                    case 3:
                                        MainWindow.r.DrawImage(pic3, 832, 95, 714, 477);
                                        break;
                                    case 4:
                                        MainWindow.r.DrawImage(pic4, 832, 95, 714, 477);
                                        break;
                                    case 5:
                                        MainWindow.r.DrawImage(pic5, 832, 95, 714, 477);
                                        break;
                                    case 6:
                                        MainWindow.r.DrawImage(pic6, 832, 95, 714, 477);
                                        break;
                                }
                                switch (thirdselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.r.DrawImage(pic1, 57, 629, 714, 477);
                                        break;
                                    case 2:
                                        MainWindow.r.DrawImage(pic2, 57, 629, 714, 477);
                                        break;
                                    case 3:
                                        MainWindow.r.DrawImage(pic3, 57, 629, 714, 477);
                                        break;
                                    case 4:
                                        MainWindow.r.DrawImage(pic4, 57, 629, 714, 477);
                                        break;
                                    case 5:
                                        MainWindow.r.DrawImage(pic5, 57, 629, 714, 477);
                                        break;
                                    case 6:
                                        MainWindow.r.DrawImage(pic6, 57, 629, 714, 477);
                                        break;
                                }
                                switch (fourthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.r.DrawImage(pic1, 832, 629, 714, 477);
                                        break;
                                    case 2:
                                        MainWindow.r.DrawImage(pic2, 832, 629, 714, 477);
                                        break;
                                    case 3:
                                        MainWindow.r.DrawImage(pic3, 832, 629, 714, 477);
                                        break;
                                    case 4:
                                        MainWindow.r.DrawImage(pic4, 832, 629, 714, 477);
                                        break;
                                    case 5:
                                        MainWindow.r.DrawImage(pic5, 832, 629, 714, 477);
                                        break;
                                    case 6:
                                        MainWindow.r.DrawImage(pic6, 832, 629, 714, 477);
                                        break;
                                }
                                MainWindow.r.DrawImage(MainWindow.temp2_1Front, 0, 0, 1800, 1200);
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
                        case 3:
                            using (MainWindow.g23 = Graphics.FromImage(MainWindow.canvus2_3))
                            {
                                MainWindow.g23.DrawImage(MainWindow.temp3_1, 0, 0, 1200, 1800);
                                switch(firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 30, 555, 555);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 30, 555, 555);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 30, 555, 555);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 30, 555, 555);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 30, 30, 555, 555);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 30, 30, 555, 555);
                                        break;
                                }
                                switch (secondselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 615, 30, 555, 555);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 615, 30, 555, 555);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 615, 30, 555, 555);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 615, 30, 555, 555);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 615, 30, 555, 555);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 615, 30, 555, 555);
                                        break;
                                }
                                switch (thirdselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 615, 555, 555);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 615, 555, 555);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 615, 555, 555);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 615, 555, 555);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 30, 615, 555, 555);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 30, 615, 555, 555);
                                        break;
                                }
                                switch (fourthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 615, 615, 555, 555);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 615, 615, 555, 555);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 615, 615, 555, 555);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 615, 615, 555, 555);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 615, 615, 555, 555);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 615, 615, 555, 555);
                                        break;
                                }
                                MainWindow.g23.DrawImage(MainWindow.temp3_1Front, 0, 0, 1200, 1800);
                            }
                            MainWindow.g23.Dispose();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.canvus2_3.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 428;
                                bi.DecodePixelHeight = 488;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                        case 4:
                            using (MainWindow.g23 = Graphics.FromImage(MainWindow.canvus2_3))
                            {
                                MainWindow.g23.DrawImage(MainWindow.temp4_1, 0, 0, 1200, 1800);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 30, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 30, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 30, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 30, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 30, 30, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 30, 30, 540, 360);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 30, 30, 540, 360);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 30, 30, 540, 360);
                                        break;
                                }
                                switch (secondselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 630, 30, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 630, 30, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 630, 30, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 630, 30, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 630, 30, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 630, 30, 540, 360);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 630, 30, 540, 360);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 630, 30, 540, 360);
                                        break;
                                }
                                switch (thirdselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 420, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 420, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 420, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 420, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 30, 420, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 30, 420, 540, 360);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 30, 420, 540, 360);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 30, 420, 540, 360);
                                        break;
                                }
                                switch (thirdselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 420, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 420, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 420, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 420, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 30, 420, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 30, 420, 540, 360);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 30, 420, 540, 360);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 30, 420, 540, 360);
                                        break;
                                }
                                switch (fourthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 630, 420, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 630, 420, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 630, 420, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 630, 420, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 630, 420, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 630, 420, 540, 360);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 630, 420, 540, 360);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 630, 420, 540, 360);
                                        break;
                                }
                                switch (fifthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 810, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 810, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 810, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 810, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 30, 810, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 30, 810, 540, 360);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 30, 810, 540, 360);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 30, 810, 540, 360);
                                        break;
                                }
                                switch (sixthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 630, 810, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 630, 810, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 630, 810, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 630, 810, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 630, 810, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 630, 810, 540, 360);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 630, 810, 540, 360);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 630, 810, 540, 360);
                                        break;
                                }
                                MainWindow.g23.DrawImage(MainWindow.temp4_1Front, 0, 0, 1200, 1800);
                            }
                            MainWindow.g23.Dispose();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.canvus2_3.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 397;
                                bi.DecodePixelHeight = 593;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                    }
                }
                else if (MainWindow.inifoldername == "hhh")
                {
                    if (MainWindow.optiontempnum == 1)
                    {
                        using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                        {
                            MainWindow.ga4.DrawImage(MainWindow.temp1_1, 0, 0, 2480, 3508);
                            switch (firstselect)
                            {
                                case 1:
                                    MainWindow.ga4.DrawImage(pic1, 228, 648, 902, 1353);
                                    break;
                                case 2:
                                    MainWindow.ga4.DrawImage(pic2, 228, 648, 902, 1353);
                                    break;
                            }
                            MainWindow.ga4.DrawImage(MainWindow.temp1_1Front, 0, 0, 2480, 3508);
                        }
                        MainWindow.ga4.Dispose();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            MainWindow.hhhcanvas.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = ms;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.DecodePixelWidth = 400;
                            bi.DecodePixelHeight = 565;
                            bi.EndInit();
                            priewviewimg.Source = bi;
                            ms.Dispose();
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                                {
                                    MainWindow.ga4.DrawImage(MainWindow.temp1_1, 0, 0, 2480, 3508);
                                    switch (firstselect)
                                    {
                                        case 1:
                                            MainWindow.ga4.DrawImage(pic1, 228, 648, 902, 1353);
                                            break;
                                        case 2:
                                            MainWindow.ga4.DrawImage(pic2, 228, 648, 902, 1353);
                                            break;
                                    }
                                    MainWindow.ga4.DrawImage(MainWindow.temp1_1Front, 0, 0, 2480, 3508);
                                }
                                MainWindow.ga4.Dispose();

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    MainWindow.hhhcanvas.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                    var bi = new BitmapImage();
                                    bi.BeginInit();
                                    bi.StreamSource = ms;
                                    bi.CacheOption = BitmapCacheOption.OnLoad;
                                    bi.DecodePixelWidth = 400;
                                    bi.DecodePixelHeight = 565;
                                    bi.EndInit();
                                    priewviewimg.Source = bi;
                                    ms.Dispose();
                                }
                                break;
                            case 2:
                                using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                                {
                                    MainWindow.ga4.DrawImage(MainWindow.temp2_1, 0, 0, 2480, 3508);
                                    switch (firstselect)
                                    {
                                        case 1:
                                            MainWindow.ga4.DrawImage(pic1, 217, 640, 864, 1296);
                                            break;
                                        case 2:
                                            MainWindow.ga4.DrawImage(pic2, 217, 640, 864, 1296);
                                            break;
                                        case 3:
                                            MainWindow.ga4.DrawImage(pic3, 217, 640, 864, 1296);
                                            break;
                                        case 4:
                                            MainWindow.ga4.DrawImage(pic4, 217, 640, 864, 1296);
                                            break;
                                    }
                                    switch (secondselect)
                                    {
                                        case 1:
                                            MainWindow.ga4.DrawImage(pic1, 1377, 640, 864, 1296);
                                            break;
                                        case 2:
                                            MainWindow.ga4.DrawImage(pic2, 1377, 640, 864, 1296);
                                            break;
                                        case 3:
                                            MainWindow.ga4.DrawImage(pic3, 1377, 640, 864, 1296);
                                            break;
                                        case 4:
                                            MainWindow.ga4.DrawImage(pic4, 1377, 640, 864, 1296);
                                            break;
                                    }
                                    MainWindow.ga4.DrawImage(MainWindow.temp2_1Front, 0, 0, 2480, 3508);
                                }
                                MainWindow.ga4.Dispose();

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    MainWindow.hhhcanvas.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                    var bi = new BitmapImage();
                                    bi.BeginInit();
                                    bi.StreamSource = ms;
                                    bi.CacheOption = BitmapCacheOption.OnLoad;
                                    bi.DecodePixelWidth = 400;
                                    bi.DecodePixelHeight = 565;
                                    bi.EndInit();
                                    priewviewimg.Source = bi;
                                    ms.Dispose();
                                }
                                break;
                            case 3:
                                using (MainWindow.ga4 = Graphics.FromImage(MainWindow.hhhcanvas))
                                {
                                    MainWindow.ga4.DrawImage(MainWindow.temp3_1, 0, 0, 2480, 3508);
                                    switch (firstselect)
                                    {
                                        case 1:
                                            MainWindow.ga4.DrawImage(pic1, 180, 590, 545, 818);
                                            break;
                                        case 2:
                                            MainWindow.ga4.DrawImage(pic2, 180, 590, 545, 818);
                                            break;
                                        case 3:
                                            MainWindow.ga4.DrawImage(pic3, 180, 590, 545, 818);
                                            break;
                                        case 4:
                                            MainWindow.ga4.DrawImage(pic4, 180, 590, 545, 818);
                                            break;
                                    }
                                    switch (secondselect)
                                    {
                                        case 1:
                                            MainWindow.ga4.DrawImage(pic1, 180, 1524, 545, 818);
                                            break;
                                        case 2:
                                            MainWindow.ga4.DrawImage(pic2, 180, 1524, 545, 818);
                                            break;
                                        case 3:
                                            MainWindow.ga4.DrawImage(pic3, 180, 1524, 545, 818);
                                            break;
                                        case 4:
                                            MainWindow.ga4.DrawImage(pic4, 180, 1524, 545, 818);
                                            break;
                                    }
                                    switch (thirdselect)
                                    {
                                        case 1:
                                            MainWindow.ga4.DrawImage(pic1, 954, 1524, 545, 818);
                                            break;
                                        case 2:
                                            MainWindow.ga4.DrawImage(pic2, 954, 1524, 545, 818);
                                            break;
                                        case 3:
                                            MainWindow.ga4.DrawImage(pic3, 954, 1524, 545, 818);
                                            break;
                                        case 4:
                                            MainWindow.ga4.DrawImage(pic4, 954, 1524, 545, 818);
                                            break;
                                    }
                                    switch (fourthselect)
                                    {
                                        case 1:
                                            MainWindow.ga4.DrawImage(pic1, 1728, 1524, 545, 815);
                                            break;
                                        case 2:
                                            MainWindow.ga4.DrawImage(pic2, 1728, 1524, 545, 815);
                                            break;
                                        case 3:
                                            MainWindow.ga4.DrawImage(pic3, 1728, 1524, 545, 815);
                                            break;
                                        case 4:
                                            MainWindow.ga4.DrawImage(pic4, 1728, 1524, 545, 815);
                                            break;
                                    }
                                    MainWindow.ga4.DrawImage(MainWindow.temp3_1Front, 0, 0, 2480, 3508);
                                }
                                MainWindow.ga4.Dispose();

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    MainWindow.hhhcanvas.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                    var bi = new BitmapImage();
                                    bi.BeginInit();
                                    bi.StreamSource = ms;
                                    bi.CacheOption = BitmapCacheOption.OnLoad;
                                    bi.DecodePixelWidth = 400;
                                    bi.DecodePixelHeight = 565;
                                    bi.EndInit();
                                    priewviewimg.Source = bi;
                                    ms.Dispose();
                                }
                                break;
                        }
                    }
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            using (MainWindow.d = Graphics.FromImage(MainWindow.canvus))
                            {
                                MainWindow.d.DrawImage(MainWindow.temp1_1, 0, 0, 600, 1800);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.d.DrawImage(pic1, 30, 30, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.d.DrawImage(pic2, 30, 30, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.d.DrawImage(pic3, 30, 30, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.d.DrawImage(pic4, 30, 30, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.d.DrawImage(pic5, 30, 30, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.d.DrawImage(pic6, 30, 30, 540, 360);
                                        break;
                                }
                                switch (secondselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.d.DrawImage(pic1, 30, 420, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.d.DrawImage(pic2, 30, 420, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.d.DrawImage(pic3, 30, 420, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.d.DrawImage(pic4, 30, 420, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.d.DrawImage(pic5, 30, 420, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.d.DrawImage(pic6, 30, 420, 540, 360);
                                        break;
                                }
                                switch (thirdselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.d.DrawImage(pic1, 30, 810, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.d.DrawImage(pic2, 30, 810, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.d.DrawImage(pic3, 30, 810, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.d.DrawImage(pic4, 30, 810, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.d.DrawImage(pic5, 30, 810, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.d.DrawImage(pic6, 30, 810, 540, 360);
                                        break;
                                }
                                switch (fourthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.d.DrawImage(pic1, 30, 1200, 540, 360);
                                        break;
                                    case 2:
                                        MainWindow.d.DrawImage(pic2, 30, 1200, 540, 360);
                                        break;
                                    case 3:
                                        MainWindow.d.DrawImage(pic3, 30, 1200, 540, 360);
                                        break;
                                    case 4:
                                        MainWindow.d.DrawImage(pic4, 30, 1200, 540, 360);
                                        break;
                                    case 5:
                                        MainWindow.d.DrawImage(pic5, 30, 1200, 540, 360);
                                        break;
                                    case 6:
                                        MainWindow.d.DrawImage(pic6, 30, 1200, 540, 360);
                                        break;
                                }
                                MainWindow.d.DrawImage(MainWindow.temp1_1Front, 0, 0, 600, 1800);
                            }
                            MainWindow.d.Dispose();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.canvus.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 206;
                                bi.DecodePixelHeight = 590;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                        case 2:
                            using (MainWindow.g23 = Graphics.FromImage(MainWindow.canvus2_3))
                            {
                                MainWindow.g23.DrawImage(MainWindow.temp2_1, 0, 0, 1200, 1800);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 30, 1140, 760);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 30, 1140, 760);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 30, 1140, 760);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 30, 1140, 760);
                                        break;
                                }
                                switch (secondselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 820, 1140, 760);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 820, 1140, 760);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 820, 1140, 760);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 820, 1140, 760);
                                        break;
                                }
                                MainWindow.g23.DrawImage(MainWindow.temp2_1Front, 0, 0, 1200, 1800);
                            }
                            MainWindow.g23.Dispose();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.canvus2_3.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 397;
                                bi.DecodePixelHeight = 593;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                        case 3:
                            using (MainWindow.g23 = Graphics.FromImage(MainWindow.canvus2_3))
                            {
                                MainWindow.g23.DrawImage(MainWindow.temp3_1, 0, 0, 1200, 1800);
                                switch (firstselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 30, 555, 555);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 30, 555, 555);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 30, 555, 555);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 30, 555, 555);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 30, 30, 555, 555);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 30, 30, 555, 555);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 30, 30, 555, 555);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 30, 30, 555, 555);
                                        break;
                                }
                                switch (secondselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 615, 30, 555, 555);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 615, 30, 555, 555);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 615, 30, 555, 555);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 615, 30, 555, 555);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 615, 30, 555, 555);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 615, 30, 555, 555);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 615, 30, 555, 555);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 615, 30, 555, 555);
                                        break;
                                }
                                switch (thirdselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 615, 555, 555);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 615, 555, 555);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 615, 555, 555);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 615, 555, 555);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 30, 615, 555, 555);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 30, 615, 555, 555);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 30, 615, 555, 555);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 30, 615, 555, 555);
                                        break;
                                }
                                switch (fourthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 615, 615, 555, 555);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 615, 615, 555, 555);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 615, 615, 555, 555);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 615, 615, 555, 555);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 615, 615, 555, 555);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 615, 615, 555, 555);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 615, 615, 555, 555);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 615, 615, 555, 555);
                                        break;
                                }
                                switch (fifthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 30, 1200, 555, 555);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 30, 1200, 555, 555);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 30, 1200, 555, 555);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 30, 1200, 555, 555);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 30, 1200, 555, 555);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 30, 1200, 555, 555);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 30, 1200, 555, 555);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 30, 1200, 555, 555);
                                        break;
                                }
                                switch (sixthselect)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        MainWindow.g23.DrawImage(pic1, 615, 1200, 555, 555);
                                        break;
                                    case 2:
                                        MainWindow.g23.DrawImage(pic2, 615, 1200, 555, 555);
                                        break;
                                    case 3:
                                        MainWindow.g23.DrawImage(pic3, 615, 1200, 555, 555);
                                        break;
                                    case 4:
                                        MainWindow.g23.DrawImage(pic4, 615, 1200, 555, 555);
                                        break;
                                    case 5:
                                        MainWindow.g23.DrawImage(pic5, 615, 1200, 555, 555);
                                        break;
                                    case 6:
                                        MainWindow.g23.DrawImage(pic6, 615, 1200, 555, 555);
                                        break;
                                    case 7:
                                        MainWindow.g23.DrawImage(pic7, 615, 1200, 555, 555);
                                        break;
                                    case 8:
                                        MainWindow.g23.DrawImage(pic8, 615, 1200, 555, 555);
                                        break;
                                }
                                MainWindow.g23.DrawImage(MainWindow.temp3_1Front, 0, 0, 1200, 1800);
                            }
                            MainWindow.g23.Dispose();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                MainWindow.canvus2_3.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                var bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = ms;
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.DecodePixelWidth = 397;
                                bi.DecodePixelHeight = 593;
                                bi.EndInit();
                                priewviewimg.Source = bi;
                                ms.Dispose();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Selectfirstpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername == "hhh")
                {
                    if (MainWindow.optiontempnum == 1)
                    {
                        if (firstselect == 1)
                        {
                            firstselect = 0;
                            firstpicclickcheck = 0;
                            firstpicborder.Opacity = 0;
                        }
                        else if (firstselect == 2)
                        {
                            firstselect = 0;
                            secondpicclickcheck = 0;
                            secondpicborder.Opacity = 0;
                        }
                    }
                    else
                    {
                        switch (TempSelect.temp)
                        {
                            case 1:
                                if (firstselect == 1)
                                {
                                    firstselect = 0;
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                }
                                else if (firstselect == 2)
                                {
                                    firstselect = 0;
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                }
                                break;
                            case 2:
                                switch (firstselect)
                                {
                                    case 1:
                                        firstselect = 0;
                                        firstpicclickcheck = 0;
                                        firstpicborder.Opacity = 0;
                                        break;
                                    case 2:
                                        firstselect = 0;
                                        secondpicclickcheck = 0;
                                        secondpicborder.Opacity = 0;
                                        break;
                                    case 3:
                                        firstselect = 0;
                                        thirdpicclickcheck = 0;
                                        thirdpicborder.Opacity = 0;
                                        break;
                                    case 4:
                                        firstselect = 0;
                                        fourthpicclickcheck = 0;
                                        fourthpicborder.Opacity = 0;
                                        break;
                                }
                                break;
                            case 3:
                                switch (firstselect)
                                {
                                    case 1:
                                        firstselect = 0;
                                        firstpicclickcheck = 0;
                                        firstpicborder.Opacity = 0;
                                        break;
                                    case 2:
                                        firstselect = 0;
                                        secondpicclickcheck = 0;
                                        secondpicborder.Opacity = 0;
                                        break;
                                    case 3:
                                        firstselect = 0;
                                        thirdpicclickcheck = 0;
                                        thirdpicborder.Opacity = 0;
                                        break;
                                    case 4:
                                        firstselect = 0;
                                        fourthpicclickcheck = 0;
                                        fourthpicborder.Opacity = 0;
                                        break;
                                }
                                break;
                        }
                    }
                }

                PriewviewImgShow();
            }
            catch (Exception ex)

            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Selectsecondpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername == "hhh")
                {
                    switch (TempSelect.temp)
                    {
                        case 2:
                            switch (secondselect)
                            {
                                case 1:
                                    secondselect = 0;
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                    break;
                                case 2:
                                    secondselect = 0;
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                    break;
                                case 3:
                                    secondselect = 0;
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                case 4:
                                    secondselect = 0;
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                            }
                            break;
                        case 3:
                            switch (secondselect)
                            {
                                case 1:
                                    secondselect = 0;
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                    break;
                                case 2:
                                    secondselect = 0;
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                    break;
                                case 3:
                                    secondselect = 0;
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                case 4:
                                    secondselect = 0;
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                            }
                            break;
                    }
                }

                PriewviewImgShow();
            }
            catch (Exception ex)

            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Selectthirdpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername == "hhh")
                {
                    switch (TempSelect.temp)
                    {
                        case 2:
                            switch (thirdselect)
                            {
                                case 1:
                                    thirdselect = 0;
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                    break;
                                case 2:
                                    thirdselect = 0;
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                    break;
                                case 3:
                                    thirdselect = 0;
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                case 4:
                                    thirdselect = 0;
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                            }
                            break;
                        case 3:
                            switch (thirdselect)
                            {
                                case 1:
                                    thirdselect = 0;
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                    break;
                                case 2:
                                    thirdselect = 0;
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                    break;
                                case 3:
                                    thirdselect = 0;
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                case 4:
                                    thirdselect = 0;
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                            }
                            break;
                    }
                }

                PriewviewImgShow();
            }
            catch (Exception ex)

            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void Selectfourthpic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Source.Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.inifoldername == "hhh")
                {
                    switch (TempSelect.temp)
                    {
                        case 2:
                            switch (fourthselect)
                            {
                                case 1:
                                    fourthselect = 0;
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                    break;
                                case 2:
                                    fourthselect = 0;
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                    break;
                                case 3:
                                    fourthselect = 0;
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                case 4:
                                    fourthselect = 0;
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                            }
                            break;
                        case 3:
                            switch (fourthselect)
                            {
                                case 1:
                                    fourthselect = 0;
                                    firstpicclickcheck = 0;
                                    firstpicborder.Opacity = 0;
                                    break;
                                case 2:
                                    fourthselect = 0;
                                    secondpicclickcheck = 0;
                                    secondpicborder.Opacity = 0;
                                    break;
                                case 3:
                                    fourthselect = 0;
                                    thirdpicclickcheck = 0;
                                    thirdpicborder.Opacity = 0;
                                    break;
                                case 4:
                                    fourthselect = 0;
                                    fourthpicclickcheck = 0;
                                    fourthpicborder.Opacity = 0;
                                    break;
                            }
                            break;
                    }
                }

                PriewviewImgShow();
            }
            catch (Exception ex)

            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
