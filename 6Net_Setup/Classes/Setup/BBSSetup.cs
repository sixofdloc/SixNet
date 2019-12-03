using System;
using Net_Data;
using Net_Data.Models;

namespace Net_Setup.Classes.Setup
{
    public class BBSSetup
    {
        private readonly BBSDataCore _core;

        public BBSSetup(BBSDataCore core)
        {
            _core = core;
        }

        public void SetupBBS()
        {

            var bbsConfig = _core.GetBBSConfig();
            User sysOpUser = null;
            if (bbsConfig == null)
            {
                bbsConfig = new BBSConfig();
                sysOpUser = new User();
            }
            else
            {
                sysOpUser = _core.GetUserById(bbsConfig.SysOpUserId);
                if (sysOpUser == null)
                {
                    sysOpUser = new User();
                }
            }

            sysOpUser.Username = Utils.Input("Enter the SysOp's username", sysOpUser.Username);
            sysOpUser.HashedPassword = Utils.Input("Enter the SysOp's password", sysOpUser.HashedPassword);
            sysOpUser.LastConnection = DateTime.Now;
            sysOpUser.LastConnectionIP = "localhost";
            sysOpUser.LastDisconnection = DateTime.Now.AddSeconds(1);
            sysOpUser.RealName = Utils.Input("Enter the SysOp's real name", sysOpUser.RealName);
            sysOpUser.ComputerType = Utils.Input("Enter the SysOp's main computer type", sysOpUser.ComputerType);
            sysOpUser.Email = Utils.Input("Enter the SysOp's private email address", sysOpUser.Email);
            sysOpUser.WebPage = Utils.Input("Enter the SYsOp's webpage address", sysOpUser.WebPage);

            sysOpUser = _core.SaveUser(sysOpUser);
            if (sysOpUser != null)
            {
                bbsConfig.SysOpUserId = sysOpUser.Id;


                bbsConfig.BBSName = Utils.Input("Enter the BBS name", bbsConfig.BBSName);
                bbsConfig.BBSUrl = Utils.Input("Enter the BBS URL (no port)", bbsConfig.BBSUrl);

                var Port = "";
                bool portComplete = false;
                while (!portComplete)
                {
                    Port = Utils.Input("Enter the BBS port", bbsConfig.BBSPort.ToString());
                    if (int.TryParse(Port, out int intPort))
                    {
                        bbsConfig.BBSPort = intPort;
                        portComplete = true;
                    }
                }
                bbsConfig.SysOpPublicHandle = Utils.Input("Enter the SysOp's public handle", bbsConfig.SysOpPublicHandle);
                bbsConfig.SysOpEmail = Utils.Input("Enter the SysOp's public email address", bbsConfig.SysOpEmail);
                bbsConfig.SysOpMenuPassword = Utils.Input("Enter the password for the SysOp menu", bbsConfig.SysOpMenuPassword);
                _core.SaveBBSConfig(bbsConfig);
            }
        }

    }
}
