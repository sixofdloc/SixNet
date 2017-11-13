using System;
using SixNet_BBS;
using SixNet_StringUtils;

namespace PFile_Empire
{
    public class EmpireUser
    {
        //public int EmpireId { get; set; }
        public int      UserId     { get; set; }
        public string   Username   { get; set; }
        public int      Land       { get; set; }
        public int      Grain      { get; set; }
        public int      Gold       { get; set; }
        public int      Tax        { get; set; }
        public int      Mills      { get; set; }
        public int      Markets    { get; set; }
        public int      Serfs      { get; set; }
        public int      Soldiers   { get; set; }
        public int      Nobles     { get; set; }
        public int      Castle     { get; set; }
        public int      Shipyards  { get; set; }
        public int      Foundries  { get; set; }
        public int      Turns      { get; set; }
        public bool     ArmyMobile { get; set; }
        public DateTime LastPlay   { get; set; }

        public static string Serialize(BBS bbs, EmpireUser du)
        {
            return Utils.SerializeToXmlString<EmpireUser>(du);
        }

        public static EmpireUser Deserialize(BBS bbs, string s)
        {
            return Utils.DeserializeXmlString<EmpireUser>(s);
        }


    }
}
