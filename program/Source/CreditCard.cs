using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Interop;

namespace wpfTest.Source
{
    public class CreditCard
    {

        Thread KSCATmsg;

        [DllImport("ksnetcomm.dll")]
        public static extern int KSCATApproval(byte[] responseTelegram, string ip, int port, string requestTelegram, int RequestLen, int opeion);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string IpClassName, string IpWindowName);



        public string Request(int price, IntPtr Handle, string SerialNo = null)
        {
            string text4 = "";
            string request_mgs;
            string text2;
            byte[] recv_msg = new byte[4096];

            int num = 0;
            string msg_len_str = string.Empty;
            string tempStr = string.Empty;
            string STX = Convert.ToChar(2).ToString();
            string ETX = Convert.ToChar(3).ToString();
            string CR = Convert.ToChar(13).ToString();
            string FS = Convert.ToChar(28).ToString();

            System.Text.Encoding myEncode = System.Text.Encoding.GetEncoding("ks_c_5601-1987");
            ASCIIEncoding ascii = new ASCIIEncoding();

            string ip = "127.0.0.1";
            int port = 27015;

            request_mgs = "";
            request_mgs += STX;
            request_mgs += "IC";
            request_mgs += "01";
            request_mgs += "0200";
            request_mgs += "N";
            request_mgs = ((SerialNo != null && SerialNo.Length == 10) ? (request_mgs + SerialNo) : (request_mgs + "DPT0TEST03"));
            request_mgs += "    ";
            request_mgs += "000000000000";
            request_mgs += " ";
            request_mgs += "                    ";
            request_mgs += "                    ";
            request_mgs += "K";
            string text = Handle + "                ";
            request_mgs += text.Substring(0, 16);
            request_mgs += "################";
            request_mgs += "                                        ";
            request_mgs += "                                     ";
            request_mgs += FS;
            request_mgs += "00";
            if (price < 10000)
            {
                text2 = "00000000" + Convert.ToString(price);
            }
            else
            {
                text2 = "0000000" + Convert.ToString(price);
            }
            request_mgs += text2;
            request_mgs += "000000000000";
            request_mgs += "000000000000";
            request_mgs += "000000000000";
            request_mgs += "000000000000";
            request_mgs += "  ";
            request_mgs += "                ";
            request_mgs += "            ";
            request_mgs += "      ";
            request_mgs += tempStr.PadLeft(163, ' ');
            request_mgs += "X";
            request_mgs += ETX;
            request_mgs += CR;

            msg_len_str = string.Format("{0:0000}", request_mgs.Length);
            request_mgs = msg_len_str + request_mgs;

            Source.Log.log.Info("Card Message : " + request_mgs);
            num = KSCATApproval(recv_msg, ip, port, request_mgs, request_mgs.Length, 0);


            Source.Log.log.Info("Card Result : " + myEncode.GetString(recv_msg, 0, recv_msg.Length).Trim());

            if (num <= 0)
            {
                Source.Log.log.Info("오류메세지 : " + myEncode.GetString(recv_msg, 0, recv_msg.Length));
                text4 = "오류메세지 : " + myEncode.GetString(recv_msg, 0, recv_msg.Length);
            }

            if (myEncode.GetString(recv_msg).Contains("거래 취소됨"))
            {
                Log.log.Info("거래 취소됨");
                text4 = "Cancelled";
            }
            else if (myEncode.GetString(recv_msg).Contains("시간 초과"))
            {
                Log.log.Info("시간 초과");
                text4 = "TimeOut";
            }
            else if (myEncode.GetString(recv_msg).Contains("실패"))
            {
                Log.log.Info("실패");
                text4 = "Fail";
            }
            else if (myEncode.GetString(recv_msg).Contains("이전거래 미완료"))
            {
                Log.log.Info("이전거래 미완료");
                text4 = "Retry";
            }
            else if (myEncode.GetString(recv_msg).Contains("오류"))
            {
                Log.log.Info("오류");
                text4 = "Error";
            }
            else if (myEncode.GetString(recv_msg).Contains("OK"))
            {
                string text3 = myEncode.GetString(recv_msg, 0, recv_msg.Length);
                Log.log.Info("응답 전문 : " + text3);
                text4 = "OK!";
                text4 = text4 + "|" + myEncode.GetString(recv_msg, 49, 6);
                text4 = text4 + "|" + myEncode.GetString(recv_msg, 94, 12);
                text4 = text4 + "|" + myEncode.GetString(recv_msg, 106, 20);
                text4 = text4 + "|" + myEncode.GetString(recv_msg, 336, 6);
                Log.log.Info("응답 전문 처리 : " + text4);
            }
            return text4;
        }

        public string Cancel(int price, IntPtr Handle, string AuthorizeCode, string Date, string VANTR, string SerialNo = null)
        {
            string text6 = "";
            string request_msg;
            byte[] recv_msg = new byte[4096];

            int result = 0;
            string msg_len_str = string.Empty;
            string tempStr = string.Empty;
            string STX = Convert.ToChar(2).ToString();
            string ETX = Convert.ToChar(3).ToString();
            string CR = Convert.ToChar(13).ToString();
            string FS = Convert.ToChar(28).ToString();

            System.Text.Encoding myEncode = System.Text.Encoding.GetEncoding("ks_c_5601-1987");
            ASCIIEncoding ascii = new ASCIIEncoding();

            string ip = "127.0.0.1";
            int port = 27015;

            request_msg = "";
            request_msg += STX;
            request_msg += "IC";
            request_msg += "01";
            request_msg += "0420";
            request_msg += "N";
            request_msg = ((SerialNo != null && SerialNo.Length == 10) ? (request_msg + SerialNo) : (request_msg + "DPT0TEST03"));
            request_msg += "    ";
            request_msg += "000000000000";
            request_msg += " ";
            string text = VANTR + "                ";
            request_msg += text.Substring(0, 20);
            request_msg += "                    ";
            request_msg += " ";
            request_msg += (Handle + "                ").Substring(0, 16);
            request_msg += "################";
            request_msg += "                                        ";
            request_msg += "KSVTR                                ";
            request_msg += FS;
            request_msg += "00";
            string text3;
            if (price < 10000)
            {
                text3 = "00000000" + Convert.ToString(price);
            }
            else
            {
                text3 = "0000000" + Convert.ToString(price);
            }
            request_msg += text3;
            request_msg += "000000000000";
            request_msg += "000000000000";
            request_msg += "000000000000";
            request_msg += "000000000000";
            request_msg += "  ";
            request_msg += "                ";
            string text4 = AuthorizeCode + "            ";
            request_msg += text4.Substring(0, 12);
            request_msg += Date;
            request_msg += tempStr.PadLeft(163, ' ');
            request_msg += "X";
            request_msg += ETX;
            request_msg += CR;

            msg_len_str = string.Format("{0:0000}", request_msg.Length);
            request_msg = msg_len_str + request_msg;

            Log.log.Info("카드 취소 작동");
            Log.log.Info("Requested : " + request_msg);

            result = KSCATApproval(recv_msg, ip, port, request_msg, request_msg.Length, 0);

            Source.Log.log.Info("Card Result : " + myEncode.GetString(recv_msg, 0, recv_msg.Length).Trim());

            if (result <= 0)
            {
                Source.Log.log.Info("오류메세지 : " + myEncode.GetString(recv_msg, 0, recv_msg.Length));
                text6 = "오류메세지 : " + myEncode.GetString(recv_msg, 0, recv_msg.Length);
            }
            if (myEncode.GetString(recv_msg).Contains("거래 취소됨"))
            {
                Source.Log.log.Info("Cancelled");
                text6 = "Cancelled";
            }
            if (myEncode.GetString(recv_msg).Contains("시간 초과"))
            {
                Source.Log.log.Info("TimeOut");
                text6 = "TimeOut";
            }
            if (myEncode.GetString(recv_msg).Contains("취소된거래"))
            {
                Source.Log.log.Info("이미 취소됨");
                text6 = "Cancled";
            }
            if (myEncode.GetString(recv_msg).Contains("OK"))
            {
                string text5 = myEncode.GetString(recv_msg, 0, recv_msg.Length);
                Log.log.Info("응답 전문 : " + text5);
                text6 = "OK!";
                text6 = text6 + "|" + myEncode.GetString(recv_msg, 49, 6);
                text6 = text6 + "|" + myEncode.GetString(recv_msg, 94, 12);
                text6 = text6 + "|" + myEncode.GetString(recv_msg, 106, 20);
                Source.Log.log.Info("응답전문처리 : " + text6);
            }
            else
            {
                text6 = myEncode.GetString(recv_msg, 0, recv_msg.Length);
            }
            return text6;
        }
    }
}
