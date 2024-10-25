using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

namespace wpfTest.Source
{
    internal class SendMessage
    {
        private static int Port = 80;

        public static void Sendmessage(string number, string name, string url)
        {
            try
            {
                TcpClient client = new TcpClient("", Port);
                Source.Log.log.Info("서버에 연결되었습니다.");

                NetworkStream stream = client.GetStream();
                string message = number + "__" + name + "__" + url; // 보낼 신호
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Source.Log.log.Info($"서버로 데이터를 보냈습니다: {message}");

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error($"오류 발생: {ex}");
            }
        }
    }
}