using System;
using System.Collections.Generic;
using System.Linq;
using SixNet_BBS;
using SixNet_BBS_Data;
using SixNet_StringUtils;
using SixNet_Logger;

namespace PFile_Dopewars
{
    public class Dopewars
    {

        #region Constants
        const int WEED = 0;
        const int COCAINE = 1;
        const int ACID = 2;
        const int PCP = 3;
        const int HASH = 4;
        const int EXTACY = 5;
        const int HEROIN = 6;
        const int LUDES = 7;
        const int OPIUM = 8;
        const int CRACK = 9;
        const int SPEED = 10;

        const int DRUG_HI = SPEED;

        const int BRONX = 0;
        const int GHETTO = 1;
        const int CENTRALPARK = 2;
        const int MANHATTAN = 3;
        const int CONEYISLAND = 4;
        const int BROOKLYN = 5;
        const int QUEENS = 6;
        const int STATENISLAND = 7;

        string[] Dope_Lady = { "Does your mother know you''re a dope dealer?",
                                            "You look like an aardvark!",
                                            "Get a haircut, hippie!",
                                            "FISH!",
                                            "I got a board with a nail in it.",
                                            "I used to be a man.",
                                            "I''d like to sell you an edible poodle.",
                                            "I am the walrus!",
                                            "You must be from California...",
                                            "You know, suicide is painless",
                                            "They shoot canoes, don''t they?",
                                            "I CAN FIT A SCREWDRIVER IN MY PEEHOLE!",
                                            "Your momma don't wear no socks",
                                            "You look like this dog I used to know.",
                                            "My neighbor's dog is the devil.  He tells me to kill people.",
                                            "Ballsack!",
                                            "My schnauzer has an unusually low testosterone level.",
                                            "I have a penis.  In a jar at home.",
                                            "My late husband, Walter used to sell drugs.",
                                            "Two in the pink, one in the stink!",
                                            "Pez!",
                                            "GHOTI!",
                                            "Crack kills!",
                                            "Peed Skills!",
                                            "I peed a little."
                             };
        string[] Dope_Hoods = { "The Bronx", "The Ghetto", "Central Park", "Manhattan", "Coney Island", "Brooklyn", "Queens", "Staten Island" };
        int[] Dope_Hood_Police_Encounter_Chance = { 30, 30, 20, 20, 10, 30, 20, 5 };
        int[] Dope_Hospitals = { MANHATTAN, QUEENS,STATENISLAND };
        int[] Dope_Gunstores = { BRONX, GHETTO, BROOKLYN };
        int[] Dope_Banks = { CENTRALPARK, STATENISLAND };
        int[] Dope_Pubs = { BRONX, GHETTO, BROOKLYN, QUEENS };
        
        string[] Dope_Drugs = { "Weed", "Cocaine", "Acid", "PCP", "Hashish", "Extacy", "Heroin", "Ludes", "Opium", "Crack", "Meth" };
        string[] Dope_Units = { "pounds", "kilos", "hits", "hits", "bricks", "tabs", "bags", "bottles", "bricks", "rocks", "hits" };

      //hood-localized price fluctuations
        int[,] Dope_Hood_Price_Adjust = {//   WEE,COK,ACD,PCP,HER,EXT,HA, L, O,CRK,MTH
                                             {  0, -5,  0,  0,-10,  0, 0, 0, 0, 10,-10}, //Bronx
                                             {-10,-10,  0, 10,  0,  0, 0, 0, 0,-20,  0}, //Ghetto
                                             { 10,  0,  0,  0,  0, 20, 0, 0, 0,  0,  0}, //Central Park
                                             {  0, 40,  0,-10,  0,  0, 0, 0, 0,  0, 10}, //Manhattan
                                             { 30,  0, 20,  0,  0, 10, 0, 0, 0,  0,  0}, //Coney Island
                                             {  0, -5,  0, 10, 20,  0, 0, 0, 0, 10, 20}, //Brooklyn
                                             { 10, 10,-10,  0, 10,  0, 0, 0, 0,  0, 10}, //Queens
                                             { 10,  0,  0,  0, 10,-20, 0, 0, 0,  0,  0}, //Staten Island
                                        };

    //base odds that any given drug will be for sale in any given hood
      int[,] Dope_Hood_ForSale_Chance= {   // WEE,COK,ACD,PCP,HER,EXT,HAS,LUD,OPM,CRK,MTH
                                             { 90, 60, 50, 50, 70, 50, 50, 50, 50, 30, 70}, //Bronx
                                             { 90, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50}, //Ghetto
                                             { 90, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50}, //Central Park
                                             { 90, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50}, //Manhattan
                                             { 90, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50}, //Coney Island
                                             { 90, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50}, //Brooklyn
                                             { 90, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50}, //Queens
                                             { 90, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50}, //Staten Island
                                        };
    //base odds that there will be any given drug for sale in any given hood
      int[,] Dope_Hood_Buyers_Chance =  {//   WEE,COK,ACD,PCP,HER,EXT,HA, L, O,CRK,MTH
                                             { 50, 40, 50, 50, 30, 50,50,50,50, 70, 30}, //Bronx
                                             { 70, 70, 10, 80, 50, 20,10,40,10, 80, 50}, //Ghetto
                                             { 10,  0,  0,  0,  0, 20, 0, 0, 0,  0,  0}, //Central Park
                                             {  0, 40,  0,-10,  0,  0, 0, 0, 0,  0, 10}, //Manhattan
                                             { 30,  0, 20,  0,  0, 10, 0, 0, 0,  0,  0}, //Coney Island
                                             {  0, -5,  0, 10, 20,  0, 0, 0, 0, 10, 20}, //Brooklyn
                                             { 10, 10,-10,  0, 10,  0, 0, 0, 0,  0, 10}, //Queens
                                             { 10,  0,  0,  0, 10,-20, 0, 0, 0,  0,  0}, //Staten Island
                                        };

        //Messages shown when a bull event occurs for a given drug
        string[] Dope_Bull = { "The dope famine is upon us!  Potheads are desperate for weed!",
                                            "Addicts are paying outrageous prices for coke!",
                                            "A hippie jam band is in town. Acid prices skyrocket!",
                                            "Sherm freaks are desperate for PCP!",
                                            "Hash has become immensely popular!",
                                            "A rave in the neighborhood boosts the price of extacy!",
                                            "Addicts are paying outrageous prices for smack!",
                                            "Rumors of super-strong ludes have driven the price sky-high!",
                                            "Stupid college kids are paying top dollar for opium!",
                                            "Addicts are paying outrageous prices for crack!",
                                            "Crazed rednecks are buying meth at outrageous prices!"
    };

        //Messages shown when a bear event occurs for a given drug
        string[] Dope_Bear =                {
                                                "Columbian freighters dust the coast guard - weed prices plummet!",
                                            "Coke prices are at an all-time low!",
                                            "The market is flooded with cheap, home-made acid!",
                                            "Someone''s selling super-cheap PCP!",
                                            "The Marakesh Express has arrived!",
                                            "The price of extacy has bottomed out!",
                                            "Smack is selling at an all-time low!",
                                            "Some kids knock off a pharmacy.  Cheap ludes abound!",
                                            "Some hippie is selling a bunch of cheap opium!",
                                            "Crack! Get your Crack!  Now cheaper than ever!",
                                            "A mobile meth lab has been in town, driving prices down."
                                            };

        int[] Dope_Low = { 300, 15000, 1000, 1500, 700, 60, 5000, 10, 50, 5, 70 };
        int[] Dope_High = { 900, 30000, 4500, 3500, 1500, 120, 14000, 60, 100, 20, 250 };
        #endregion

        #region vars
        DopeUser CurrentUser { get; set; }
        List<Dopewars_Location> Locations { get; set; }
        int CurrentLocation { get; set; }
        int Columns = 0;
        private readonly BBS _bbs;
        private readonly DataInterface _dataInterface;
        bool Dead = false;
        #endregion

        #region Main Loop

        public Dopewars()
        {
            //For compatability.
        }

        public Dopewars(BBS bbs, DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
        }

        public void Main()
        {
            Columns = _bbs.TerminalType.Columns();
            Init();
            MainMenu();
            SaveUser();
            _bbs.SendFileForTermType("Dopewars_Exit", true);
        }

        void Init()
        {
            _bbs.SendFileForTermType("Dopewars_intro", true);
            _bbs.AnyKey(true, true);

            Locations = new List<Dopewars_Location>();
            //Initialize (and save) locations
            for (int hood = 0; hood < Dope_Hoods.Count(); hood++)
            {
                Locations.Add(Location_Init(hood));
            }

            LoadUser();
        }

        void MainMenu()
        {
            bool Exit = false;
            while (!Exit)
            {
                if (!_bbs.Connected) Exit = true;
                _bbs.Write("~l2~c7P~c1lay, ~c7H~c1elp, ~c7N~c1ews, ~c7Q~c1uit~l1~c1Dopewars:~c7");
                char c = _bbs.GetChar();
                switch (c.ToString().ToUpper())
                {
                    case "P":
                        _bbs.WriteLine("Play~p1");
                        Play();
                        break;
                    case "H":
                        _bbs.WriteLine("Help~p1");
                        _bbs.SendFileForTermType("Dopewars_Help", true);
                        break;
                    case "N":
                        //Host_System.WriteLine("News~p1");

                        break;
                    case "Q":

                        _bbs.WriteLine("Quit~p1");
                        _bbs.WriteLine("~l1Exiting.~p1.~p1.~p1.");
                        Exit = true;
                        break;
                }
                if (Dead)
                {
                    _bbs.SendFileForTermType("Dopewars_Dead", true);
                    Exit = true;
                }
            }
        }
        #endregion

        #region Database Calls
        void SaveUser()
        {
            _dataInterface.SaveUserDefinedField(CurrentUser.UserId, "DOPEWARS", DopeUser.Serialize(_bbs, CurrentUser));
        }

        void DeleteUser()
        {
            //Stupid fuck died.
            _dataInterface.SaveUserDefinedField(CurrentUser.UserId, "DOPEWARS", "");
        }

        void LoadUser()
        {
            //Load User Record if there is one.
            string dustr = _dataInterface.GetUserDefinedField(_bbs.CurrentUser.UserId, "DOPEWARS");
            if (dustr == "")
            {
                // 012345678901234567890123456789012345678909 
                _bbs.Write("~c1~l1It looks like this is your first time.~l1Enter a nickname to use:~c7");
                string nick = _bbs.Input(true, false, false);
                _bbs.WriteLine("~l1~c1Setting up your account, ~c7" + nick + "~c1.~p1.~p1.");
                //New User
                CurrentUser = new DopeUser()
                {
                    UserId = _bbs.CurrentUser.UserId,
                    Turns = 24,
                    Cash = 1000,
                    Firepower = 1,
                    Carry = 100,
                    Health = 100,
                    Location = 0,
                    Posse = 0,
                    Debt = 2000,
                    LastTurnUsed = DateTime.Now,
                    Nick = nick,
                };
                Dopewars_Drug dd = new Dopewars_Drug()
                {
                    Drug_Id = WEED,
                    Units = 1,
                    Price = Dope_Low[WEED]
                };
                CurrentUser.Drugs.Add(dd);
                SaveUser();
            }
            else
            {
                CurrentUser = DopeUser.Deserialize(_bbs, dustr);
                TimeSpan since_last = DateTime.Now - CurrentUser.LastTurnUsed;
                CurrentUser.Turns += since_last.Hours;
                CurrentUser.LastTurnUsed = DateTime.Now;
                SaveUser();
            }

        }

        public List<DopeUser> AllUsers()
        {
            List<DopeUser> dopelist = new List<DopeUser>();
            List<IdAndKeys> idklist = _dataInterface.GetAllUserDefinedFieldsWithKey("DOPEWARS");
            foreach (IdAndKeys idk in idklist)
            {
                dopelist.Add(DopeUser.Deserialize(_bbs, idk.Keys["data"]));
            }
            return dopelist;
        }

        #endregion

        #region Main Play Loop
        void Play()
        {
            bool dopex = false;
            while (!dopex)
            {
                _bbs.Write("~s1~d2" + Utils.Center("Dopewars!" , Columns)+"~d0");
                _bbs.WriteLine("~c7Location: ~c1" + Dope_Hoods[CurrentUser.Location]);
    //Local prices
                _bbs.Write("~d2" + Utils.Center("Drugs Available", Columns) + "~d0");
                List_Drugs(Locations[CurrentLocation].Drugs,false,false);
                
                //Carrying
                _bbs.Write("~d2" + Utils.Center("Carrying", Columns) + "~d0");
                if (CurrentUser.Drugs.Count(p=>p.Units > 0)>0)
                {
                    List_Drugs(CurrentUser.Drugs, false,true);
                }
                else
                {
                    _bbs.WriteLine("~c4Nothing!");
                }
                _bbs.Write("~d2" + Utils.Center("Stats", Columns) + "~d0");
                
                _bbs.Write(Utils.Clip("~c7Firepower: ~c1" + CurrentUser.Firepower.ToString(), Columns / 2, true));
                _bbs.WriteLine(Utils.Clip("~c7Capacity: ~c1" + CurrentUser.Capacity().ToString(), (Columns / 2)-1, true));
                _bbs.Write(Utils.Clip("~c7Health: ~c1" + CurrentUser.Health.ToString() + "%", Columns / 2, true));
                _bbs.WriteLine(Utils.Clip("~c7Turns: ~c1" + CurrentUser.Turns.ToString(), (Columns / 2) - 1, true));
                _bbs.Write(Utils.Clip("~c7Cash: ~c1$" + CurrentUser.Cash.ToString() , Columns / 2, true));
                _bbs.WriteLine(Utils.Clip("~c7Debt: ~c1$" + CurrentUser.Debt.ToString(), (Columns / 2) - 1, true));
                _bbs.WriteLine(Utils.Clip("~c7Posse: ~c1" + CurrentUser.Posse.ToString(), Columns / 2, true));

                _bbs.Write("~d2" + Utils.SPC(Columns) + "~d0");

                //Buy, Sell, Travel, Quit
                _bbs.Write("~c1A~c7ttack, ~c1B~c7uy, ~c1S~c7ell, ~c1T~c7ravel, ~c1Q~c7uit~l1~c7Dopewars:~c1");
                char c = _bbs.GetChar();
                switch (c.ToString().ToUpper())
                {
                    case "A":
                        // Attack();
                        _bbs.WriteLine("Attack~p1");
                        break;
                    case "B":
                        _bbs.WriteLine("Buy~p1");
                        Buy();
                        break;
                    case "S":
                      //  Sell();
                        _bbs.WriteLine("Sell~p1");
                        Sell();
                        break;
                    case "T":
                        _bbs.WriteLine("Travel~p1");
                        Travel();
                        break;
                    case "Q":
                        _bbs.WriteLine("Quit");
                        dopex = true;
                        break;
                }
                if (Dead) dopex = true;
            }
        }

        public void Buy()
        {
            try
            {
                _bbs.Write("~s1~d2" + Utils.Center("Buy Drugs", Columns) + "~d0");
                //List drugs for sale in this hood
                List_Drugs(Locations[CurrentLocation].Drugs, true,false);
                _bbs.Write("~d2" + Utils.SPC(Columns) + "~d0");
                _bbs.WriteLine("~c4You Have ~c1$" + CurrentUser.Cash.ToString() + ".");
                _bbs.Write("~d2" + Utils.SPC(Columns) + "~d0");
                _bbs.Write("~c7Which drug:~c1");
                string dr = _bbs.Input(true, false, true);
                if (dr != "")
                {
                    if (int.TryParse(dr, out int drug))
                    {
                        //Is this drug actually available?
                        if (drug < Locations[CurrentLocation].Drugs.Count)
                        {
                            Dopewars_Drug dd = Locations[CurrentLocation].Drugs[drug];
                            _bbs.Write("~l1~c7How many " + Dope_Units[dd.Drug_Id] + "?~c1");
                            string un = _bbs.Input(true, false, true);
                            int units = 0;
                            if (un != "")
                            {
                                if (int.TryParse(un, out units))
                                {
                                    //Can they afford it?
                                    if ((units * dd.Price) <= CurrentUser.Cash)
                                    {
                                        //Can they carry it?
                                        if (units + CurrentUser.Carrying() <= CurrentUser.Capacity())
                                        {
                                            _bbs.WriteLine("~l1~c1Buying drugs.~p1.~p1.~p1.~p2");
                                            //Fucking buy it, then.
                                            CurrentUser.Cash -= units * dd.Price;
                                            CurrentUser.BuyDrugs(dd.Drug_Id, units, units * dd.Price);
                                            SaveUser();
                                        }
                                        else
                                        {
                                            _bbs.WriteLine("~l1~c2You can't carry that much.~p4");
                                        }
                                    }
                                    else
                                    {
                                        _bbs.WriteLine("~l1~c2You can't afford it, bitch!~p4");
                                    }
                                }
                                else
                                {
                                    goto notanumber;
                                }
                            }
                            else
                            {
                                goto notanumber;
                            }

                        }
                        else
                        {
                            _bbs.WriteLine("~l1~c2Pick something that's actually on the menu.?~p4");
                        }
                    }
                    else
                    {
                        goto notanumber;
                    }

                }
                else
                {
                    goto notanumber;
                }

            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in Dopewars.Buy(): " + e);
            }
            return;
            notanumber:
            _bbs.WriteLine("~l1~c2That's not a number, asshole!~p4");

        }

        public void Sell()
        {
            try
            {
                _bbs.Write("~s1~d2" + Utils.Center("Sell Drugs", Columns) + "~d0");
                //List drugs for sale in this hood
                List_Drugs(CurrentUser.Drugs, true, true);
                _bbs.Write("~d2" + Utils.Center("Local Prices", Columns) + "~d0");
                List_Drugs(Locations[CurrentLocation].Drugs, false, false);
                _bbs.Write("~d2" + Utils.SPC(Columns) + "~d0");
                _bbs.Write("~c7Which drug:~c1");
                string dr = _bbs.Input(true, false, true);
                if (dr != "")
                {
                    if (int.TryParse(dr, out int drug))
                    {
                        //Do they have it to sell
                        if (drug < CurrentUser.Drugs.Count)
                        {
                            Dopewars_Drug dd = CurrentUser.Drugs.Where(p => p.Units > 0).ToList()[drug];
                            Dopewars_Drug ld = Locations[CurrentLocation].Drugs.FirstOrDefault(p => p.Drug_Id.Equals(dd.Drug_Id));
                            if (ld != null)
                            {
                                _bbs.Write("~l1~c7How many " + Dope_Units[dd.Drug_Id] + "?~c1");
                                string un = _bbs.Input(true, false, true);
                                int units = 0;
                                if (un != "")
                                {
                                    if (int.TryParse(un, out units))
                                    {
                                        //DO they have that many to sell?
                                        if (units <= dd.Units)
                                        {
                                            //Confirm @ price
                                            _bbs.Write("~l1~c7Sell ~c1" + units.ToString() + " " + Dope_Units[dd.Drug_Id] + "~c7 of " + Dope_Drugs[dd.Drug_Id].ToLower() + " for ~c1$" + units * ld.Price + " ~c7");
                                            if (_bbs.YesNo(true, true))
                                            {

                                                _bbs.WriteLine("~c1Selling drugs.~p1.~p1.~p1.~p2");
                                                CurrentUser.SellDrugs(dd.Drug_Id, units, ld.Price);
                                                SaveUser();
                                            }
                                        }
                                        else
                                        {
                                            _bbs.WriteLine("~c2You don't have that many!~p4");
                                        }
                                    }
                                    else
                                    {
                                        goto notanumber;
                                    }
                                }
                                else
                                {
                                    goto notanumber;
                                }
                            }
                            else
                            {
                                _bbs.WriteLine("~l1~c2There are no buyers for that here!~p4");
                            }

                        }
                        else
                        {
                            _bbs.WriteLine("~l1~c2Pick something that's actually on the menu.?~p4");
                        }
                    }
                    else
                    {
                        goto notanumber;
                    }

                }
                else
                {
                    goto notanumber;
                }

            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in Dopewars.Sell: " + e);
            }
            return;
        notanumber:
            _bbs.WriteLine("~l1~c2That's not a number, asshole!~p4");

        }

        void Travel()
        {
            if (CurrentUser.Turns > 0)
            {
                //Show list of locations
                ListLocations();
                //Prompt for which location to travel to
                bool locpicked = false;
                while (!locpicked)
                {
                    _bbs.Write("~c7Where To?");
                    char l = _bbs.GetChar();
                    if ((l >= '0') && (l <= '9'))
                    {
                        CurrentLocation = int.Parse(l.ToString());
                        locpicked = true;
                    }
                    else
                    {
                        _bbs.WriteLine("~l1~c2Pick an actual fucking location.~p1");
                        ListLocations();
                    }
                }
                //Init that location
                _bbs.WriteLine("~c1" + Dope_Hoods[CurrentLocation] + "!");
                CurrentUser.Location = CurrentLocation;
                _bbs.WriteLine("~l1~c1Travelling to ~c4" + Dope_Hoods[CurrentLocation] + "~c1.~p1.~p1.~p1.");
                Locations[CurrentLocation] = Location_Init(CurrentLocation);
                Random r = new Random(DateTime.Now.Millisecond);
                Dead = false;
                //Random police encounter
                if (r.Next(0, 100) <= Dope_Hood_Police_Encounter_Chance[CurrentLocation])
                {
                    Police();
                }
                else
                {
                    _bbs.WriteLine("~c1You manage to evade the cops.~p1");
                }
                if (!Dead)
                {
                    if (CurrentUser.Debt > 0) CurrentUser.Debt = (int)(CurrentUser.Debt * 1.1f);
                    if (CurrentUser.Debt >= 100000)
                    {
                        //TODO
                        //Loan shark kicks your ass, kills 1 bitch each turn until all bitches are gone,
                        //Then kicks your ass for 10% health per turn until you die or pay up.
                        if (CurrentUser.Posse > 0)
                        {
                            _bbs.WriteLine("~l1~c2You owe the Loan Shark too much money.~l1To demonstrate his annoyance...~l1, he has one of your posse killed.");
                            CurrentUser.Posse--;
                        }
                        else
                        {
                            _bbs.WriteLine("~l1~c2You owe the Loan Shark too much money.~l1He sends some guys around...~l1They beat the living shit out of you.");
                            CurrentUser.Health -= 10;
                            if (CurrentUser.Health <= 0)
                            {
                                Dead = true; //dumbass.
                            }
                        }
                    }
                    if (!Dead)
                    {
                        //Random Lady on the Subay bullshit
                        _bbs.WriteLine("~l1~c1A lady on the subway says: ~c3'" + Dope_Lady[r.Next(0, Dope_Lady.Length - 1)] + "'~c1~p1");

                        //Random Bull Market Events
                        if (r.Next(0, 100) <= 30)  //20% chance, need to change this to per-neighborhood
                        {
                            int bulldrug = r.Next(0, DRUG_HI);
                            Dopewars_Drug dd = Locations[CurrentLocation].Drugs.FirstOrDefault(p => p.Drug_Id.Equals(bulldrug));
                            if (dd == null)
                            {
                                dd = new Dopewars_Drug()
                                {
                                    Drug_Id = bulldrug,
                                    Price = PriceForDrug(bulldrug, CurrentLocation)
                                };
                                Locations[CurrentLocation].Drugs.Add(dd);
                            }
                            _bbs.WriteLine("~c8" + Dope_Bull[bulldrug] + "~c1");
                            Locations[CurrentLocation].Drugs.First(p => p.Drug_Id.Equals(bulldrug)).Price *= 8;

                        }
                        //Random Bear Market Events
                        if (r.Next(0, 100) <= 30)  //20% chance, need to change this to per-neighborhood
                        {
                            int beardrug = r.Next(0, DRUG_HI);
                            Dopewars_Drug dd = Locations[CurrentLocation].Drugs.FirstOrDefault(p => p.Drug_Id.Equals(beardrug));
                            if (dd == null)
                            {
                                dd = new Dopewars_Drug()
                                {
                                    Drug_Id = beardrug,
                                    Price = PriceForDrug(beardrug, CurrentLocation)
                                };
                                Locations[CurrentLocation].Drugs.Add(dd);
                            }
                            _bbs.WriteLine("~c6" + Dope_Bear[beardrug] + "~c1");
                            Locations[CurrentLocation].Drugs.First(p => p.Drug_Id.Equals(beardrug)).Price /= 8;
                        }
                        //Robbed?
                        if (r.Next(0, 100) <= 20)  //20% chance, need to change this to per-neighborhood
                        {
                            int robamt = r.Next(1, CurrentUser.Cash / 2);
                            if (CurrentUser.Cash > 0)
                            {
                                if (CurrentUser.Cash >= robamt)
                                {
                                    CurrentUser.Cash -= robamt;
                                }
                                else
                                {
                                    robamt = CurrentUser.Cash;
                                    CurrentUser.Cash = 0;
                                }
                                _bbs.WriteLine("~l1~c2You got robbed on the subway!~l1They took ~c1$" + robamt.ToString() + "~c2!");
                            }
                        }

                        //Gift?
                        if (r.Next(0, 100) <= 20)  //20% chance, need to change this to per-neighborhood
                        {
                            int giftdrug = r.Next(0, DRUG_HI);
                            int giftamt = r.Next(1, 10);
                            _bbs.WriteLine("~l1~c7You run into a friend on the subway.~l1He gives you ~c1" + giftamt.ToString() + " " + Dope_Units[giftdrug] + " ~c7of ~c1" + Dope_Drugs[giftdrug]+"~c7.");
                            if (CurrentUser.Carrying() + giftamt <= CurrentUser.Capacity()){
                            CurrentUser.GiftDrugs(giftdrug, giftamt);
                            }
                            else
                            {
                                _bbs.WriteLine("~l1~c2Unfortunately, you can't carry that much.");
                            }
                        }

                        //Offered Gun?
                        if (r.Next(0, 100) <= 20)  //20% chance, need to change this to per-neighborhood
                        {
                            OfferGun();
                        }

                        //Offered Bitch?
                        if (r.Next(0, 100) <= 20)  //20% chance, need to change this to per-neighborhood
                        {
                            OfferBitch();
                        }

                        //Offered Drugs?
                        if (r.Next(0, 100) <= 20)  //20% chance, need to change this to per-neighborhood
                        {
                            int offerdrug = r.Next(0, DRUG_HI);
                            int offeramt = r.Next(10, 50);
                            int offerprice = (int)(Dope_Low[offerdrug]*.8);
                            _bbs.WriteLine("~c7A guy comes up to you on the subway and offers you ~c1" + offeramt.ToString() + " " + Dope_Units[offerdrug] + " ~c7of~c1 " + Dope_Drugs[offerdrug] + "~c7 for ~c1$" + offerprice.ToString() + "~c7.");
                            if (CurrentUser.Cash > offerprice)
                            {
                                if (CurrentUser.Carrying() + offeramt <= CurrentUser.Capacity())
                                {
                                    _bbs.Write("Buy it ");
                                    if (_bbs.YesNo(true, true))
                                    {
                                        CurrentUser.BuyDrugs(offerdrug, offeramt, offerprice);
                                    }
                                }
                                else
                                {
                                    _bbs.WriteLine("~l1~c2Unfortunately, you can't carry that much.");
                                }
                            }
                            else
                            {
                                _bbs.WriteLine("~l1~c2Unfortunately, you can't afford it.");
                            }
                        }

                        _bbs.WriteLine("~l1~c8You arrive at your destination. ~l1~k1");
                        CurrentUser.Turns--;
                        CurrentUser.LastTurnUsed = DateTime.Now;

                        //Entering Location, does it have a bank?
                        if (Locations[CurrentLocation].HasBank)
                        {
                            _bbs.Write("~l1~c7Visit the bank~c1");
                            if (_bbs.YesNo(true, true))
                            {
                                Bank();
                            }
                        }
                        //                   does it have the loan shark?
                        if (Locations[CurrentLocation].HasLoanShark)
                        {
                            _bbs.Write("~l1~c7Visit the loan shark~c1");
                            if (_bbs.YesNo(true, true))
                            {
                                Loan_Shark();
                            }
                        }
                        //                   does it have a pub?
                        if (Locations[CurrentLocation].HasPub)
                        {
                            _bbs.Write("~l1~c7Visit the pub~c1");
                            if (_bbs.YesNo(true, true))
                            {
                                Pub();
                            }

                        }
                        //                   does it have a gun store?
                        if (Locations[CurrentLocation].HasGunStore)
                        {
                            _bbs.Write("~l1~c7Visit the gun store~c1");
                            if (_bbs.YesNo(true, true))
                            {
                                GunStore();
                            }
                        }
                        //Here's where we drop back to the main loop
                    }
                    else
                    {
                        //You died.  Dumbass.
                    }
                }
                else
                {
                    //you died.  Dumbass.
                }
                if (Dead)
                {
                    _bbs.WriteLine("~l1~c2Congratulations, dumbass.  You're dead.~p4");
                    DeleteUser();
                    //Quit game completely.
                }
                SaveUser();
            }
            else
            {
                _bbs.WriteLine("~s1~c2Out of turns for today, no more travel.~p3");
            }
        }


        public void OfferGun()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            if (CurrentUser.Firepower < (CurrentUser.Posse + 1))
            {
                int gunamt = r.Next(500, 2500);
                if (CurrentUser.Cash > gunamt)
                {
                    _bbs.Write("~c7Want to buy a gun for ~c1$" + gunamt + "~c7? ~c1");
                    if (_bbs.YesNo(true, true))
                    {
                        CurrentUser.Firepower++;
                        CurrentUser.Cash -= gunamt;
                    }
                }
                else
                {
                        _bbs.WriteLine("~l1~c2A guy on the subway offers you a gun, but you can't afford it.");
                }
                
            }
        }

        public void OfferBitch()
        {
            Random r = new Random(DateTime.Now.Millisecond);
                int bitchamt = r.Next(5000, 25000);
                if (CurrentUser.Cash > bitchamt)
                {
                    _bbs.Write("~c7Want to hire someone for ~c1$" + bitchamt + "~c7? ~c1");
                    if (_bbs.YesNo(true, true))
                    {
                        CurrentUser.Posse++;
                        CurrentUser.Cash -= bitchamt;
                    }
                }
                else
                {
                    _bbs.WriteLine("~l1~c2A guy on the subway offers to work for you, but you can't afford it.");
                }

       
        }

#endregion

        #region Travel Subroutines
        public void Loan_Shark()
        {
            try
            {
                bool Loan_Shark_Done = false;
                while (!Loan_Shark_Done)
                {
                    _bbs.Write("~s1~d2" + Utils.Center("Loan Shark", Columns) + "~d0");
                    //List drugs for sale in this hood
                    _bbs.WriteLine("~c7Your Debt: ~c1$" + CurrentUser.Debt.ToString());
                    _bbs.Write("~d2" + Utils.SPC(Columns) + "~d0");
                    _bbs.WriteLine("~c7You Have ~c1$" + CurrentUser.Cash.ToString());
                    _bbs.Write("~d2" + Utils.SPC(Columns) + "~d0");
                    _bbs.Write("~c1B~c7orrow, ~c1P~c7ay, ~c1L~c7eave:~c1");
                    char dr = _bbs.GetChar();
                    switch (dr.ToString().ToUpper())
                    {
                        case "B":
                            //Borrow 
                            _bbs.Write("~l1~c7Borrow how much? ~c1");
                            string un = _bbs.Input(true, false, true);
                            int amount = 0;
                            if (un != "")
                            {
                                if (int.TryParse(un, out amount))
                                {
                                    if (amount + CurrentUser.Debt > 100000)
                                    {
                                        _bbs.WriteLine("~l1~c2No fucking way.  Not that much.~p4");
                                    }
                                    else
                                    {
                                        _bbs.WriteLine("~l1~c3Ok....~l1  But remember..~l1It's dangerous to owe me money.~p4");
                                        CurrentUser.Cash += amount;
                                        CurrentUser.Debt += amount;
                                        Loan_Shark_Done = true;
                                    }
                                }
                                else
                                {
                                    goto notanumber;
                                }
                            }
                            break;
                        case "P":
                            _bbs.Write("~l1~c7Borrow how much? ~c1");
                            un = _bbs.Input(true, false, true);
                            amount = 0;
                            if (un != "")
                            {
                                if (int.TryParse(un, out amount))
                                {
                                    bool paying = false;
                                    if (amount > CurrentUser.Debt)
                                    {
                                        _bbs.WriteLine("~l1~c2You don't owe that much.~p4");
                                    }
                                    else
                                    {
                                        if (amount > CurrentUser.Cash)
                                        {
                                            _bbs.WriteLine("~l1~c2You don't have that much cash.~p4");
                                        }
                                        else
                                        {
                                            paying = true;
                                        }
                                    }

                                    if (paying)
                                    {
                                        CurrentUser.Debt -= amount;
                                        CurrentUser.Cash -= amount;
                                        _bbs.WriteLine("~l1~c7Paying.~p1.~p1.~p1.~l1The Loan Shark says: ~c3'Nice doing business with you...'~p1");
                                        Loan_Shark_Done = true;
                                    }
                                }
                                else
                                {
                                    goto notanumber;
                                }
                            }
                            break;
                        case "L":
                            //Leave
                            Loan_Shark_Done = true;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in Dopewars.Loan_Shark: " + e);
            }
            return;
        notanumber:
            _bbs.WriteLine("~l1~c2That's not a number, asshole!~p4");
        }

        public void Police()
        {
            //Officer dick-lock and random number of deputies attack
            //Each combat round, roll for initiative.
            //Each side shoots, to-hit depends on Firepower
            //If hit, decimate one bitch/deputy or 10% health per hit.
            //Continue/Flee/Surrender
            //Surrender means full health back, but lose all money/drugs/firepower and 50% posse
        }

        public void Bank()
        {
            //Deposit, Withdrawl, Exit
        }

        public void Pub()
        {
            //Drink, Curse, Gamble, Exit
            //Drinking heals 1% health, cursing is just for fun., Gambling can lose/make money
        }

        public void GunStore()
        {
            //Buy, Sell, Exit
        }


        Dopewars_Location Location_Init(int hood)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            Dopewars_Location loc = new Dopewars_Location()
            {
                LocationId = hood,
                HasBank = (Dope_Banks.Contains(hood)),
                HasGunStore = (Dope_Gunstores.Contains(hood)),
                HasHospital = (Dope_Hospitals.Contains(hood)),
                HasLoanShark = (hood == BRONX),
                HasPub = (Dope_Pubs.Contains(hood)),
                Drugs = new List<Dopewars_Drug>()
            };
            //Figure out what drugs we have for sale or buyers here.
            for (int drug = WEED; drug <= SPEED; drug++)
            {
                //The odds of this being for sale here depend on the hood and drug
                bool Has = (r.Next(1, 100) <= Dope_Hood_ForSale_Chance[hood, drug]);
                if (Has)
                {
                    Dopewars_Drug dd = new Dopewars_Drug()
                    {
                        Drug_Id = drug,
                        Price = PriceForDrug(drug, hood)
                    };
                    loc.Drugs.Add(dd);
                }
            //The odds of there being buyers here depend on the hood and drug
                //Has = (r.Next(1, 100) < Dope_Hood_Buyers_Chance[hood, drug]);
                //if (Has)
                //{
                //    Dopewars_Drug dd = new Dopewars_Drug();
                //    dd.Drug_Id = drug;
                //    Dopewars_Drug ds = Locations[hood].Drugs_Selling.FirstOrDefault(p => p.Drug_Id.Equals(drug));
                //    if (ds == null)
                //    {
                //        dd.Price = PriceForDrug(drug, hood);
                //    }
                //    else
                //    {
                //        dd.Price = ds.Price;
                //    }
                //    Locations[hood].Drugs_Buying.Add(dd);
                //}
            }

            return loc;

        }

        #endregion

        #region Utils
        int PriceForDrug(int drug, int hood)
        {
            int Price = Dope_Low[drug];
            try
            {
                Random r = new Random(DateTime.Now.Millisecond);
                int Low = Dope_Low[drug];
                int High = Dope_High[drug];
                int PriceAdjust = Dope_Hood_Price_Adjust[hood, drug];
                Low = Low + ((PriceAdjust / 100) * Low);
                High = High + ((PriceAdjust / 100) * High);
                Price = r.Next(Low, High);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("DOPEWARS: Exception in Dopewars.PriceForDrug: " + e.ToString());
            }
            return Price;
        }

        void ListLocations()
        {
            _bbs.Write("~s1~d2" + Utils.Center("Dopewars!", Columns) + "~d0");
            _bbs.WriteLine("~c1Select your destination:");
            _bbs.Write("~c70~c1. "+Utils.Clip("The Bronx", (Columns / 2)-3, true));
            _bbs.WriteLine("~c71~c1. "+Utils.Clip("The Ghetto", (Columns / 2) - 4, true));
            _bbs.Write("~c72~c1. "+Utils.Clip("Central Park", (Columns / 2)-3, true));
            _bbs.WriteLine("~c73~c1. " + Utils.Clip("Manhattan", (Columns / 2) - 4, true));
            _bbs.Write("~c74~c1. " + Utils.Clip("Coney Island", (Columns / 2) - 3, true));
            _bbs.WriteLine("~c75~c1. " + Utils.Clip("Brooklyn", (Columns / 2) - 4, true));
            _bbs.Write("~c76~c1. " + Utils.Clip("Queens", (Columns / 2) - 3, true));
            _bbs.WriteLine("~c77~c1. "+Utils.Clip("Staten Island", (Columns / 2) - 4, true));
            _bbs.Write("~d2" + Utils.SPC(Columns) + "~d0");

        }


        void List_Drugs(List<Dopewars_Drug> druglist, bool numbered, bool showunits)
        {
            int drugno = 0;
            foreach (Dopewars_Drug dd in druglist)
            {
                int unitlen = 0;
                if (showunits) unitlen = dd.Units.ToString().Length;
                if ((showunits && dd.Units > 0) || (!showunits))
                {
                    if ((drugno % 2) == 0)
                    {
                        _bbs.Write("~c7" + Utils.Clip((numbered ? drugno.ToString() + ". " : "") + Dope_Drugs[dd.Drug_Id] + " ~c1" + (showunits ? dd.Units.ToString() + "@" : "") + " $" + dd.Price.ToString(), (Columns / 2) + (numbered ? 0 : 3) + (showunits ? unitlen * -1 : 0), true));
                    }
                    else
                    {
                        _bbs.WriteLine("~c7" + Utils.Clip((numbered ? drugno.ToString() + ". " : "") + Dope_Drugs[dd.Drug_Id] + " ~c1" + (showunits ? dd.Units.ToString() : "") + " $" + dd.Price.ToString(), (Columns / 2) + (numbered ? -1 : 2) + (showunits ? unitlen * -1 : 0), true));
                    }
                    drugno++;
                }
            }
            if ((drugno % 2) == 1) _bbs.WriteLine("");

        }

        #endregion

    }
}
