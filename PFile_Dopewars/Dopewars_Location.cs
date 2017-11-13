using System.Collections.Generic;
using SixNet_BBS;
using SixNet_StringUtils;

namespace PFile_Dopewars
{
    public class Dopewars_Location
    {
        public int LocationId { get; set; }
        public List<Dopewars_Drug> Drugs { get; set; } //Current price in this neighborhood
        public bool HasPub { get; set; }
        public bool HasGunStore { get; set; }
        public bool HasLoanShark { get; set; }
        public bool HasBank { get; set; }
        public bool HasHospital { get; set; }

        public Dopewars_Location()
        {
            Drugs = new List<Dopewars_Drug>();
        }

        public string Serialize(BBS bbs )
        {
            return Utils.SerializeToXmlString<Dopewars_Location>(this);
        }

        public void Deserialize(BBS bbs, string s)
        {
            Dopewars_Location temploc = Utils.DeserializeXmlString<Dopewars_Location>(s);
            LocationId = temploc.LocationId;
            HasPub = temploc.HasPub;
            HasGunStore = temploc.HasGunStore;
            HasLoanShark = temploc.HasLoanShark;
            HasBank = temploc.HasBank;
            HasHospital = temploc.HasHospital;
            Drugs = new List<Dopewars_Drug>();
            foreach (Dopewars_Drug drug in temploc.Drugs)
            {
                Drugs.Add(drug);
            }
        }

    }
}
