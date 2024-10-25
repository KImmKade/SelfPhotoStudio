using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wpfTest;

namespace Onecut.Source
{
    internal class MakeGray
    {

        public static void MakingGrayPic(string inputfilepath, string outputfilepath)
        {
            Mat src = new Mat();
            Mat dst;

            src = Cv2.ImRead(inputfilepath, ImreadModes.Color);

            dst = new Mat(src.Width, src.Height, MatType.CV_8UC1);

            Cv2.CvtColor(src, dst, ColorConversionCodes.BGR2GRAY);
            Delay(100);
            dst.SaveImage(outputfilepath);


            src.Dispose();
            dst.Dispose();
            src = null;
            dst = null;
        }

        private static DateTime Delay(int MS)
        {
            wpfTest.Source.Log.log.Debug(MethodBase.GetCurrentMethod().Name + "() | 딜레이 시작 딜레이 시간(단위 : ms) : " + MS);

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
    }
}
