using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wpfTest.Source
{
    class DataBase
    {

    }

    public class GetData
    {
        public string Year = string.Empty;
        public string Month = string.Empty;
        public string Day = string.Empty;
        public string User = string.Empty;
        public string Money = string.Empty;
        public string Totalpay = string.Empty;
    }

    /*private void ReadJson()
    {
        string jsonFilePath = MainWindow.jsonpath;
        string str = string.Empty;
        string users = string.Empty;

        // Json 파일 읽기
        using (StreamReader file = File.OpenText(jsonFilePath))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            JObject json = (JObject)JToken.ReadFrom(reader);

            GetData _db = new GetData();


        }
    }*/
}
