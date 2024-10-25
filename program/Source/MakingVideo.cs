using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using wpfTest.Source;
using System.Reflection;
using wpfTest;
using System.Diagnostics.Eventing.Reader;
using wpfTest.View;

namespace Onecut.Source
{
    internal class MakingVideo
    {
        static string imgpath;
        static string frontimgpath;
        static string video1path;
        static string video2path;
        static string video3path;
        static string video4path;

        static VideoCapture cap1;
        static VideoCapture cap2;
        static VideoCapture cap3;
        static VideoCapture cap4;

        static Mat img1;
        static Mat img2;

        static Mat frame1;
        static Mat frame2;
        static Mat frame3;
        static Mat frame4;

        static Mat result;

        static VideoWriter writer1;

        private static void LoadVideo()
        {
            try
            {
                Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                if (MainWindow.optiontempnum == 1)
                {
                    switch (SelectPic.firstselect)
                    {
                        case 1:
                            video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_0.mp4";
                            break;
                        case 2:
                            video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_1.mp4";
                            break;
                    }
                    imgpath = MainWindow.TempPath + @"\Temp1_1.png";
                    frontimgpath = MainWindow.TempPath + @"\Temp1_1Front.png";
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            switch (SelectPic.firstselect)
                            {
                                case 1:
                                    video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_0.mp4";
                                    break;
                                case 2:
                                    video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_1.mp4";
                                    break;
                            }
                            imgpath = MainWindow.TempPath + @"\Temp1_1.png";
                            frontimgpath = MainWindow.TempPath + @"\Temp1_1Front.png";
                            break;
                        case 2:
                            switch (SelectPic.firstselect)
                            {
                                case 1:
                                    video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_0.mp4";
                                    break;
                                case 2:
                                    video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_1.mp4";
                                    break;
                                case 3:
                                    video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_2.mp4";
                                    break;
                                case 4:
                                    video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_3.mp4";
                                    break;
                            }
                            switch (SelectPic.secondselect)
                            {
                                case 1:
                                    video2path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_0.mp4";
                                    break;
                                case 2:
                                    video2path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_1.mp4";
                                    break;
                                case 3:
                                    video2path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_2.mp4";
                                    break;
                                case 4:
                                    video2path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_3.mp4";
                                    break;
                            }
                            imgpath = MainWindow.TempPath + @"\Temp2_1.png";
                            frontimgpath = MainWindow.TempPath + @"\Temp2_1Front.png";
                            break;
                        case 3:
                            switch (SelectPic.firstselect)
                            {
                                case 1:
                                    video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_0.mp4";
                                    break;
                                case 2:
                                    video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_1.mp4";
                                    break;
                                case 3:
                                    video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_2.mp4";
                                    break;
                                case 4:
                                    video1path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_3.mp4";
                                    break;
                            }
                            switch (SelectPic.secondselect)
                            {
                                case 1:
                                    video2path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_0.mp4";
                                    break;
                                case 2:
                                    video2path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_1.mp4";
                                    break;
                                case 3:
                                    video2path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_2.mp4";
                                    break;
                                case 4:
                                    video2path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_3.mp4";
                                    break;
                            }
                            switch (SelectPic.thirdselect)
                            {
                                case 1:
                                    video3path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_0.mp4";
                                    break;
                                case 2:
                                    video3path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_1.mp4";
                                    break;
                                case 3:
                                    video3path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_2.mp4";
                                    break;
                                case 4:
                                    video3path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_3.mp4";
                                    break;
                            }
                            switch (SelectPic.fourthselect)
                            {
                                case 1:
                                    video4path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_0.mp4";
                                    break;
                                case 2:
                                    video4path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_1.mp4";
                                    break;
                                case 3:
                                    video4path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_2.mp4";
                                    break;
                                case 4:
                                    video4path = MainWindow.Videopath + @"\Video_" + TakePic.videoname + "_3.mp4";
                                    break;
                            }
                            imgpath = MainWindow.TempPath + @"\Temp3_1.png";
                            frontimgpath = MainWindow.TempPath + @"\Temp3_1Front.png";
                            break;
                    }
                }
                Log.log.Info("동영상 및 이미지 로드 완료");
            }
            catch (Exception ex)
            {
                Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        public static void MakkingVideo()
        {
            try
            {
                Log.log.Info(MethodBase.GetCurrentMethod().Name + "() - 동작");

                LoadVideo();

                cap1 = new VideoCapture();
                cap2 = new VideoCapture();
                cap3 = new VideoCapture();
                cap4 = new VideoCapture();

                if (MainWindow.optiontempnum == 1)
                {
                    cap1 = new VideoCapture(video1path);
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            cap1 = new VideoCapture(video1path);
                            break;
                        case 2:
                            cap1 = new VideoCapture(video1path);
                            cap2 = new VideoCapture(video2path);
                            break;
                        case 3:
                            cap1 = new VideoCapture(video1path);
                            cap2 = new VideoCapture(video2path);
                            cap3 = new VideoCapture(video3path);
                            cap4 = new VideoCapture(video4path);
                            break;
                    }
                }

                img1 = Cv2.ImRead(imgpath, ImreadModes.Color);
                img2 = Cv2.ImRead(frontimgpath, ImreadModes.Unchanged);

                Cv2.Resize(img1, img1, new OpenCvSharp.Size(620, 877));
                Cv2.Resize(img2, img2, new OpenCvSharp.Size(620, 877));

                writer1 = new VideoWriter();

                writer1 = new VideoWriter(MainWindow.Videopath + @"\Video_" + TakePic.videoname + ".mp4", FourCC.X264, 15, new OpenCvSharp.Size(img1.Width, img1.Height), true);

                frame1 = new Mat();
                frame2 = new Mat();
                frame3 = new Mat();
                frame4 = new Mat();

                result = new Mat();

                if (MainWindow.optiontempnum == 1)
                {
                    while(true)
                    {
                        cap1.Read(frame1);

                        if (frame1.Empty())
                        {
                            break;
                        }

                        result = img1.Clone();
                        Cv2.Resize(frame1, frame1, new OpenCvSharp.Size(225, 338));
                        frame1.CopyTo(result[new OpenCvSharp.Rect(57, 162, frame1.Width, frame1.Height)]);

                        // 부분 불투명한 이미지2를 result 위에 덧붙입니다.
                        Parallel.For(0, result.Rows, y =>
                        {
                            for (int x = 0; x < result.Cols; x++)
                            {
                                Vec3b color1 = result.At<Vec3b>(y, x);
                                Vec4b color2 = img2.At<Vec4b>(y, x);
                                Vec3b color = new Vec3b();

                                float alpha2 = color2.Item3 / 255.0f;

                                for (int i = 0; i < 3; i++)
                                {
                                    color[i] = (byte)((color1[i] * (1 - alpha2)) + (color2[i] * alpha2));
                                }

                                result.Set(y, x, color);
                            }
                        });

                        writer1.Write(result);
                    }
                }
                else
                {
                    switch (TempSelect.temp)
                    {
                        case 1:
                            while (true)
                            {
                                cap1.Read(frame1);

                                if (frame1.Empty())
                                {
                                    break;
                                }

                                result = img1.Clone();
                                Cv2.Resize(frame1, frame1, new OpenCvSharp.Size(225, 338));
                                frame1.CopyTo(result[new OpenCvSharp.Rect(57, 162, frame1.Width, frame1.Height)]);

                                // 부분 불투명한 이미지2를 result 위에 덧붙입니다.
                                Parallel.For(0, result.Rows, y =>
                                {
                                    for (int x = 0; x < result.Cols; x++)
                                    {
                                        Vec3b color1 = result.At<Vec3b>(y, x);
                                        Vec4b color2 = img2.At<Vec4b>(y, x);
                                        Vec3b color = new Vec3b();

                                        float alpha2 = color2.Item3 / 255.0f;

                                        for (int i = 0; i < 3; i++)
                                        {
                                            color[i] = (byte)((color1[i] * (1 - alpha2)) + (color2[i] * alpha2));
                                        }

                                        result.Set(y, x, color);
                                    }
                                });

                                writer1.Write(result);
                            }
                            break;
                        case 2:
                            while (true)
                            {
                                cap1.Read(frame1);
                                cap2.Read(frame2);

                                if (frame1.Empty() || frame2.Empty())
                                {
                                    break;
                                }

                                result = img1.Clone();
                                Cv2.Resize(frame1, frame1, new OpenCvSharp.Size(216, 324));
                                Cv2.Resize(frame2, frame2, new OpenCvSharp.Size(216, 324));
                                frame1.CopyTo(result[new OpenCvSharp.Rect(54, 160, frame1.Width, frame1.Height)]);
                                frame2.CopyTo(result[new OpenCvSharp.Rect(344, 160, frame2.Width, frame2.Height)]);

                                // 부분 불투명한 이미지2를 result 위에 덧붙입니다.
                                Parallel.For(0, result.Rows, y =>
                                {
                                    for (int x = 0; x < result.Cols; x++)
                                    {
                                        Vec3b color1 = result.At<Vec3b>(y, x);
                                        Vec4b color2 = img2.At<Vec4b>(y, x);
                                        Vec3b color = new Vec3b();

                                        float alpha2 = color2.Item3 / 255.0f;

                                        for (int i = 0; i < 3; i++)
                                        {
                                            color[i] = (byte)((color1[i] * (1 - alpha2)) + (color2[i] * alpha2));
                                        }

                                        result.Set(y, x, color);
                                    }
                                });

                                writer1.Write(result);
                            }
                            break;
                        case 3:
                            while (true)
                            {
                                cap1.Read(frame1);
                                cap2.Read(frame2);
                                cap3.Read(frame3);
                                cap4.Read(frame4);

                                if (frame1.Empty() || frame2.Empty() || frame3.Empty() || frame4.Empty())
                                {
                                    break;
                                }

                                result = img1.Clone();
                                Cv2.Resize(frame1, frame1, new OpenCvSharp.Size(136, 204));
                                Cv2.Resize(frame2, frame2, new OpenCvSharp.Size(136, 204));
                                Cv2.Resize(frame3, frame3, new OpenCvSharp.Size(136, 204));
                                Cv2.Resize(frame4, frame4, new OpenCvSharp.Size(136, 204));
                                frame1.CopyTo(result[new OpenCvSharp.Rect(45, 147, frame1.Width, frame1.Height)]);
                                frame2.CopyTo(result[new OpenCvSharp.Rect(45, 381, frame2.Width, frame2.Height)]);
                                frame3.CopyTo(result[new OpenCvSharp.Rect(238, 381, frame3.Width, frame3.Height)]);
                                frame4.CopyTo(result[new OpenCvSharp.Rect(432, 381, frame4.Width, frame4.Height)]);

                                // 부분 불투명한 이미지2를 result 위에 덧붙입니다.
                                Parallel.For(0, result.Rows, y =>
                                {
                                    for (int x = 0; x < result.Cols; x++)
                                    {
                                        Vec3b color1 = result.At<Vec3b>(y, x);
                                        Vec4b color2 = img2.At<Vec4b>(y, x);
                                        Vec3b color = new Vec3b();

                                        float alpha2 = color2.Item3 / 255.0f;

                                        for (int i = 0; i < 3; i++)
                                        {
                                            color[i] = (byte)((color1[i] * (1 - alpha2)) + (color2[i] * alpha2));
                                        }

                                        result.Set(y, x, color);
                                    }
                                });

                                writer1.Write(result);
                            }
                            break;
                    }
                }

                cap1.Dispose();
                cap2.Dispose();
                cap3.Dispose();
                cap4.Dispose();
                img1.Dispose();
                img2.Dispose();
                frame1.Dispose();
                frame2.Dispose();
                frame3.Dispose();
                frame4.Dispose();
                result.Dispose();
                writer1.Dispose();
            }
            catch (Exception ex)
            {
                Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }
    }
}
