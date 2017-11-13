using System;
using System.Collections.Generic;
using System.Linq;
using SixNet_BBS;
using SixNet_StringUtils;

namespace PFile_Dopewars
{
    public class DopeUser
    {
        public int UserId { get; set; }
        public string Nick { get; set; }
        public int Cash { get; set; }
        public int Debt { get; set; }
        public List<Dopewars_Drug> Drugs { get; set; }
        public int Health { get; set; }
        public int Firepower { get; set; }
        public int Posse { get; set; }
        public int Turns { get; set; }
        public int Carry { get; set; }
        public int Location { get; set; }
        public DateTime LastTurnUsed { get; set; }

        public DopeUser()
        {
            Drugs = new List<Dopewars_Drug>();
        }

        public static string Serialize(BBS bbs, DopeUser du)
        {
            return Utils.SerializeToXmlString<DopeUser>(du);
        }

        public static DopeUser Deserialize(BBS bbs, string s)
        {
            return Utils.DeserializeXmlString<DopeUser>(s);
        }

        public int Capacity()
        {
            return Carry + (20 * Posse);
        }

        public int Carrying()
        {
            int c = 0;
            foreach (Dopewars_Drug dd in Drugs)
            {
                c = c + dd.Units;
            }
            return c;
        }


        public void BuyDrugs(int drug_id, int units, int price)
        {
            //Do they have this one already?
            Dopewars_Drug dd = Drugs.FirstOrDefault(p => p.Drug_Id.Equals(drug_id));
            if (dd == null)
            {
                dd = new Dopewars_Drug() { Drug_Id = drug_id, Units = 0, Price = 0 };
                Drugs.Add(dd);
            }
            int currentpaid = dd.Price * dd.Units;
            int newpaid = (int)((currentpaid + price) / (dd.Units + units)); //New average price
            dd.Units += units;
            dd.Price = newpaid;
        }
        
        public void GiftDrugs(int drug_id, int units)
        {
            BuyDrugs(drug_id, units, 0);
        }


        public void SellDrugs(int drug_id, int units, int price)
        {
            Dopewars_Drug dd = Drugs.FirstOrDefault(p => p.Drug_Id.Equals(drug_id));
            if (dd != null)
            {
                dd.Units -= units;
                Cash += units * price;
            }
        }



    }

}
