using System;
using Net_BBS;
using Net_BBS.BBS_Core;
using Net_StringUtils;
using Newtonsoft.Json;

namespace PFile_Empire
{
    public class EmpireUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Land { get; set; }
        public int Grain { get; set; }
        public int Gold { get; set; }
        public int Tax { get; set; }
        public int Mills { get; set; }
        public int Markets { get; set; }
        public int Serfs { get; set; }
        public int Soldiers { get; set; }
        public int Nobles { get; set; }
        public int Castle { get; set; }
        public int Shipyards { get; set; }
        public int Foundries { get; set; }
        public int Turns { get; set; }
        public bool ArmyMobile { get; set; }
        public DateTime LastPlay { get; set; }

        public static void ApplyMinimums(ref EmpireUser empireUser)
        {
            if (empireUser.Land < 0) empireUser.Land = 0;
            if (empireUser.Grain < 0) empireUser.Grain = 0;
            if (empireUser.Gold < 0) empireUser.Gold = 0;
            if (empireUser.Mills < 0) empireUser.Mills = 0;
            if (empireUser.Markets < 0) empireUser.Markets = 0;
            if (empireUser.Serfs < 0) empireUser.Serfs = 0;
            if (empireUser.Soldiers < 0) empireUser.Soldiers = 0;
            if (empireUser.Nobles < 0) empireUser.Nobles = 0;
            if (empireUser.Castle < 0) empireUser.Castle = 0;
            if (empireUser.Shipyards < 0) empireUser.Shipyards = 0;
            if (empireUser.Foundries < 0) empireUser.Foundries = 0;
        }

        public static string Serialize( EmpireUser empireUser)
        {
            ApplyMinimums(ref empireUser);
            return JsonConvert.SerializeObject(empireUser);
        }

        public static EmpireUser Deserialize(string s)
        {
            EmpireUser empireUser =  JsonConvert.DeserializeObject<EmpireUser>(s);
            ApplyMinimums(ref empireUser);
            return empireUser;
        }


    }
}