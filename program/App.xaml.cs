using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Threading;
using NetFwTypeLib;
using System.IO;

namespace wpfTest
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {

        Mutex mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                string mutexName = "program";
                bool createNew;

                mutex = new Mutex(true, mutexName, out createNew);

                if (!createNew)
                {
                    Shutdown();
                }

                Source.Log.log.Info("==========  Started application  ==========");

                firewallRule();
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        private void connect()
        {
            try
            {
                string addr = string.Empty;
                string user = string.Empty;
                string pwd = string.Empty;
                string port = string.Empty;

                addr = "";
                user = "";
                pwd = "";
                port = "";

                Source.FTPManager manager = new Source.FTPManager();
                bool result = manager.ConnectToServer(addr, port, user, pwd);

                if (result == true)
                {
                    Source.Log.log.Info("FTP 접속 성공");
                }
                else
                {
                    Source.Log.log.Info("FTP 접속 실패");
                }
            }
            catch (Exception ex)
            {
                Source.Log.log.Error(MethodBase.GetCurrentMethod().Name + "() - " + ex.Message);
            }
        }

        public App()
        {
            this.InitializeComponent();
        }

        private void firewallRule()
        {
            INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            firewallRule.Description = "방화벽 규칙에 대한 설명을 입력합니다";
            firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
            firewallRule.ApplicationName = Environment.CurrentDirectory + @"\PhotoStudio.exe";
            firewallRule.InterfaceTypes = "All";
            firewallRule.Name = "Rule name"; // 방화벽 규칙을 구분하는 이름, 삭제시에도 사용됩니다
            firewallRule.Enabled = true;
            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallPolicy.Rules.Add(firewallRule);

            INetFwPolicy2 firewallPolicy2 = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
        }
    }
}
