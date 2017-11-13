using System;
using System.Collections.Generic;
using System.Linq;
using SixNet_BBS;
using SixNet_Logger;
using SixNet_BBS_Data;
using SixNet_StringUtils;

namespace PFile_Empire
{
    public class Empire
    {
        //********************************************************
        // CONSTANTS FROM ORIGINAL BASIC VERSION
        //********************************************************
        //Barbarian Prices
        private const int BARBARIAN_BUYGRAIN = 1;
        private const int BARBARIAN_SELLGRAIN = 2;
        private const int BARBARIAN_BUYLAND = 10; //Amount barbarians will buy land for.
        private const int BARBARIAN_SELLLAND = 25; //Amount barbarians will sell their land for.
        //Purchase costs at market
        private const int SOLDIER_COST = 10;
        private const int MARKET_COST = 1000;
        private const int MILL_COST = 2000;
        private const int CASTLE_COST = 10000;
        private const int FOUNDRY_COST = 7000;
        private const int SHIPYARD_COST = 8000;
        private const int NOBLE_COST = 25000;
        //Unit Consumption
        private const int PEOPLE_EAT = 5;
        private const int SOLDIERS_EAT = 8;
        //Unit Production
        private const float LANDPRODUCE = 2.7f;
        private const float SERFPRODUCE = 7.5f;
        private const float MILLPRODUCE = 99f;
        //Misc
        private const int EMPIRE_TURNSPERDAY = 5;
        private const float SERF_REVOLT = 0.20f;
        private const int SOLDIERSPERNOBLE = 40;
        private const int NOBLESPERCASTLE = 20;

        //Profitability calculations
        private const int MARKET_PROFIT_BASE = 25;
        private const int MARKET_PROFIT_PER_MAX = 75;
        private const int MILL_PROFIT_BASE = 50;
        private const int MILL_PROFIT_PER_MAX = 100;
        private const int FOUNDRY_PROFIT_BASE = 125;
        private const int FOUNDRY_PROFIT_PER_MAX = 175;
        private const int SHIPYARD_PROFIT_BASE = 150;
        private const int SHIPYARD_PROFIT_PER_MAX = 200;

        //Maximum Tax Rate
        private const int MAX_TAXRATE = 50;

        //Population Change Rates
        private const int NATURAL_DEATH_ADJUST = 40;
        private const int BABIES_BORN_ADJUST = 22;
        private const int IMMIGRATION_ADJUST = 25;

        //Grain Deficit Ransack Threshhold
        private const float RANSACK_THRESHHOLD = 0.8f;
        private const float PLAGUE_STRENGTH = 0.2f;
        private const float NOBLE_ATTACK_SOLDIERS = 0.3f;
        private const float PALACE_ATTACK_SOLDIERS = 0.3f;
        //********************************************************


        private readonly BBS _bbs;
        private readonly DataInterface _dataInterface;
        private int Columns = 40;
        private EmpireUser CurrentUser = null;

        public Empire(BBS bbs, DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
        }

        public void Main()
        {
            try
            {
                bool gamex = false;

                Columns = _bbs.TerminalType.Columns();

                _bbs.SendFileForTermType("Empire_Title", true);
                _bbs.AnyKey(true, true);

                LoadUser();
                while (!gamex)
                {
                    _bbs.Write("~l1~c7N~c1ews,~c7P~c1lay,~c7Q~c1uit,~c7S~c1ummary~l1Empire:~c7");
                    char c = _bbs.GetChar();
                    switch (c.ToString().ToUpper())
                    {
                        case "N":
                            _bbs.WriteLine("News~p1");
                            News();
                            break;
                        case "P":
                            _bbs.WriteLine("Play~p1");
                            Play();
                            break;
                        case "S":
                            _bbs.WriteLine("Summary~p1");
                            Summary();
                            break;
                        case "Q":
                            _bbs.WriteLine("Quit~p1");
                            gamex = true;
                            break;
                        default:
                            _bbs.Write("~g1");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in Empire: " + e.ToString());
            }
        }

        public void Play()
        {
            Taxes();
            Harvest();
            Barbarians();
            Feed();
            Market();
            Attack();
            Events();
            Stock();
            Reinforce();
            CurrentUser.Turns--;
            CurrentUser.LastPlay = DateTime.Now;
            SaveUser();
            Summary();
            _bbs.AnyKey(true, true);
        }



        //function Empire_Eligible (port:integer):boolean;
        //var b: boolean;
        //var i: integer;
        //begin
        //b := TRUE;
        ////Player is ineligible if he is on another port
        //FOR i := 0 to 10 do begin
        //  if i <> port THEN BEGIN
        //    IF Empire_Players[i].ID = PortToUnum(port) THEN BEGIN
        //      Exclaim('You are already playing on port '+InttoStr(i)+'!',port);
        //      b := FALSE;
        //    END;
        //  END;
        //end;
        //// or has 0 turns with a last play date of today.
        //IF IsToday(Empire_Players[port].LastPlay) AND (Empire_Players[port].Turns = 0) THEN b := FALSE;
        //Empire_Eligible := b;
        //end;

        public void Summary()
        {
            _bbs.WriteLine("~l1~c1Summary of your empire:~c7");
            _bbs.Divider();
            _bbs.Write("~c1     Land:~c2" + Utils.Clip(CurrentUser.Land.ToString() ,10,true));
            _bbs.Write("~c1     Cash:~c2" + Utils.Clip(CurrentUser.Gold.ToString(), 10, true));
            _bbs.Write("~c1 Tax Rate:~c2" + Utils.Clip(CurrentUser.Tax.ToString() + "%",10,true)       );
            _bbs.Write("~c1    Mills:~c2" + Utils.Clip(CurrentUser.Mills.ToString(), 10, true));
            _bbs.Write("~c1  Markets:~c2" + Utils.Clip(CurrentUser.Markets.ToString(), 10, true));
            _bbs.Write("~c1Foundries:~c2" + Utils.Clip(CurrentUser.Foundries.ToString(), 10, true));
            _bbs.Write("~c1Shipyards:~c2" + Utils.Clip(CurrentUser.Shipyards.ToString(), 10, true));
            _bbs.Write("~c1   Castle:~c2" + Utils.Clip(CurrentUser.Castle.ToString()+"%", 10, true));
            _bbs.Write("~c1   Nobles:~c2" + Utils.Clip(CurrentUser.Nobles.ToString(), 10, true));
            _bbs.Write("~c1 Soldiers:~c2" + Utils.Clip(CurrentUser.Soldiers.ToString(), 10, true));
            _bbs.Write("~c1    Serfs:~c2" + Utils.Clip(CurrentUser.Serfs.ToString(), 10, true));
            _bbs.Write("~c1    Turns:~c2" + Utils.Clip(CurrentUser.Turns.ToString(), 10, true));
            _bbs.WriteLine("~c1    Grain:~c2" + Utils.Clip(CurrentUser.Grain.ToString(), 10, true));
            _bbs.WriteLine("~c1Last Play:~c2" + Utils.Clip(CurrentUser.LastPlay.ToString("yyyy-MM-dd hh:mm"), 20, true));
            _bbs.Write("~c7");
            _bbs.Divider();
        }

        public void News()
        {
            _bbs.WriteLine("~c7Latest Empire News: ~c5");
            String s = _dataInterface.GetUserDefinedField(0, "EMPIRENEWS");
            string[] news = s.Split('|');
            foreach (string ss in news)
            {
                _bbs.WriteLine("~c4" + ss);
            }
            _bbs.Write("~c5");
            _bbs.Divider();
            _bbs.Write("~c1");
            _bbs.AnyKey(true,true);
        }

        public void AddNews(string s)
        {
            _dataInterface.AppendUserDefinedField(0, "EMPIRENEWS", "|" + s);
        }

        public void Barbarians()
        {
            //  //Purchase grain from barbarians?
            bool promptx = false;
            while (!promptx)
            {
                Summary();
                int afford = CurrentUser.Gold / BARBARIAN_SELLGRAIN;
                _bbs.WriteLine("~c1The barbarians will sell their grain~l1for ~c2"+BARBARIAN_SELLGRAIN.ToString() +"~c1 per bushel.");
                _bbs.Write("How many will you buy?(~c20~c1-~c2" + afford.ToString() + "~c1)");
                string s = _bbs.Input(true, false, true, false, 5);
                _bbs.Write("~l1");
                int.TryParse(s, out int si);
                if ((s != "") && (si != 0))
                {
                    if (si > afford)
                    {
                        _bbs.Exclaim("You can only afford " + afford.ToString() + "!");
                        _bbs.AnyKey(true, true);
                    }
                    else if (si < 0)
                    {
                        _bbs.Exclaim("You cannot purchase negative bushels!");
                        _bbs.AnyKey(true, true);
                    }
                    else
                    {
                        _bbs.Write("~l1~c1Purchase ~c2" + s + "~c1 bushels of grain?");
                        if (_bbs.YesNo(true, true))
                        {
                            CurrentUser.Grain += si;
                            CurrentUser.Gold -= (si * BARBARIAN_SELLGRAIN);
                            SaveUser();
                            promptx = true;
                        }
                    }
                }
                else
                {
                    promptx = true;
                }
            }

            //  //Sell grain to barbarians?
            promptx = false;
            while (!promptx)
            {
                Summary();
                _bbs.WriteLine("~c1The barbarians will buy your grain~l1for ~c2" + BARBARIAN_BUYGRAIN.ToString() + "~c1 per bushel.");
                _bbs.Write("How many will you sell?(~c20~c1-~c2" + CurrentUser.Grain.ToString() + "~c1)");
                string s = _bbs.Input(true, false, true, false, 5);
                int.TryParse(s, out int si);
                if ((s != "") && (si != 0))
                {
                    if (si > CurrentUser.Grain)
                    {
                        _bbs.Exclaim("You only have " + CurrentUser.Grain.ToString() + "!");
                        _bbs.AnyKey(true, true);
                    }
                    else if (si < 0)
                    {
                        _bbs.Exclaim("You cannot sell negative bushels!");
                        _bbs.AnyKey(true, true);
                    }
                    else
                    {
                        _bbs.Write("~l1~c1Sell ~c2" + s + "~c1 bushels of grain?");
                        if (_bbs.YesNo(true, true))
                        {
                            CurrentUser.Grain -= si;
                            CurrentUser.Gold += (si * BARBARIAN_BUYGRAIN);
                            SaveUser();
                            promptx = true;
                        }
                    }
                }
                else
                {
                    promptx = true;
                }
            }

            //  //Purchase land from barbarians?
            promptx = false;
            while (!promptx)
            {
                Summary();
                int afford = CurrentUser.Gold / BARBARIAN_SELLLAND;
                _bbs.WriteLine("~c1The barbarians will sell their land~l1for ~c2" + BARBARIAN_SELLLAND.ToString() + "~c1 per acre.");
                _bbs.Write("How many will you buy?(~c20~c1-~c2" + afford.ToString() + "~c1)");
                string s = _bbs.Input(true, false, true, false, 5);
                int.TryParse(s, out int si);
                if ((s != "") && (si != 0))
                {
                    if (si > afford)
                    {
                        _bbs.Exclaim("You can only afford " + afford.ToString() + "!");
                        _bbs.AnyKey(true, true);
                    }
                    else if (si < 0)
                    {
                        _bbs.Exclaim("You cannot purchase negative acres!");
                        _bbs.AnyKey(true, true);
                    }
                    else
                    {
                        _bbs.Write("~l1~c1Purchase ~c2" + s + "~c1 acres of land?");
                        if (_bbs.YesNo(true, true))
                        {
                            CurrentUser.Land += si;
                            CurrentUser.Gold -= (si * BARBARIAN_SELLLAND);
                            SaveUser();
                            promptx = true;
                        }
                    }
                }
                else
                {
                    promptx = true;
                }
            }

            //  //Sell land to barbarians?
            promptx = false;
            while (!promptx)
            {
                Summary();
                _bbs.WriteLine("~c1The barbarians will buy your land~l1for ~c2" + BARBARIAN_BUYLAND.ToString() + "~c1 per acre.");
                _bbs.Write("How many will you sell?(~c20~c1-~c2" + CurrentUser.Land.ToString() + "~c1)");
                string s = _bbs.Input(true, false, true, false, 5);
                int.TryParse(s, out int si);
                if ((s != "") && (si != 0))
                {
                    if (si > CurrentUser.Land)
                    {
                        _bbs.Exclaim("You only have " + CurrentUser.Land.ToString() + "!");
                        _bbs.AnyKey(true, true);
                    }
                    else if (si < 0)
                    {
                        _bbs.Exclaim("You cannot sell negative acres!");
                        _bbs.AnyKey(true, true);
                    }
                    else
                    {
                        _bbs.Write("~l1~c1Sell ~c2" + s + "~c1 acres of land?");
                        if (_bbs.YesNo(true, true))
                        {
                            CurrentUser.Land -= si;
                            CurrentUser.Gold += (si * BARBARIAN_BUYLAND);
                            SaveUser();
                            promptx = true;
                        }
                    }
                }
                else
                {
                    promptx = true;
                }
            }

        }

        public void Harvest()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            Summary();
            int harvest = (int)(
                ((CurrentUser.Land  *  LANDPRODUCE) * (r.NextDouble())) +
                ((CurrentUser.Serfs *  SERFPRODUCE) * (r.NextDouble())) +
                ((CurrentUser.Mills * MILLPRODUCE) * (r.NextDouble()))
                );
            //What's this limiting?  I don't get it.
            if (harvest > (CurrentUser.Land + CurrentUser.Serfs) * 4) harvest = (CurrentUser.Land + CurrentUser.Serfs) * 4;

            _bbs.WriteLine("~c1This year's harvest: ~c2" + harvest.ToString() + "~c1 bushels.");
            CurrentUser.Grain += harvest;
            SaveUser();
            _bbs.AnyKey(true, true);
        }

        public void Taxes()
        {
            bool promptx = false;
            while (!promptx)
            {
                Summary();
                _bbs.WriteLine("~c1Current Tax Rate is ~c2" + CurrentUser.Tax.ToString() + "~c1%");
                _bbs.Write("~c1New tax rate:~c7");
                string s = _bbs.Input(true, false, true,false,2);
                int.TryParse(s, out int i);
                if ((s !="") && (i != CurrentUser.Tax))
                {
                    if (i > MAX_TAXRATE)
                    {
                        _bbs.Exclaim("~l1Maximum tax rate is "+MAX_TAXRATE.ToString()+"%!");
                        _bbs.AnyKey(true, true);
                    }
                    else if (i < 0)
                    {
                        _bbs.Exclaim("~l1You cannot set a negative rate!");
                        _bbs.AnyKey(true, true);
                    }
                    else
                    {
                        _bbs.Write("~l1~c1Change tax rate to ~c2" + s + "~c1%?");
                        if (_bbs.YesNo(true, true))
                        {
                            CurrentUser.Tax = i;
                            promptx = true;
                        }
                    }
                }
                else
                {
                    _bbs.WriteLine("~l1~c1Keep current tax rate of ~c2" + CurrentUser.Tax.ToString() + "~c1%?");
                    if (_bbs.YesNo(true, true))
                    {
                        promptx = true;
                    }
                }
            }
        }

        public void Feed()
        {
            int GrainPeasants = 0;
            int GrainSoldiers = 0;
            int PeasantsRequire = CurrentUser.Serfs * PEOPLE_EAT;
            int SoldiersRequire = CurrentUser.Soldiers * SOLDIERS_EAT;
            
            //  //People
            bool promptx = false;
            while (!promptx)
            {
                Summary();
                _bbs.WriteLine("~c1Your people require ~c2" + PeasantsRequire.ToString() + "~c1 bushels ~l1of grain.");
                _bbs.Write("~c1How many will you give?(~c20~c1-~c2" + CurrentUser.Grain.ToString() + "~c1)");
                string s = _bbs.Input(true, false, true, false);
                int.TryParse(s, out int si);
                _bbs.WriteLine("");
                if ((s != "") && (si != 0))
                {
                    if (si > CurrentUser.Grain)
                    {
                        _bbs.Exclaim("You can only afford " + CurrentUser.Grain.ToString() + "!");
                        _bbs.AnyKey(true, true);
                    }
                    else if (si < 0)
                    {
                        _bbs.Exclaim("You cannot give negative bushels!");
                        _bbs.AnyKey(true, true);
                    }
                    else
                    {
                        _bbs.Write("~c1Give ~c2" + si.ToString() + "~c1 bushels?");
                        if (_bbs.YesNo(true, true))
                        {
                            CurrentUser.Grain -= si;
                            GrainPeasants = si;
                            SaveUser();
                            promptx = true;
                        }
                    }
                }
            }
            //  //Soldiers
            promptx = false;
            while (!promptx)
            {
                Summary();
                _bbs.WriteLine("~s1~c1Your soldiers require ~c2" + SoldiersRequire.ToString() + "~c1 bushels of grain.");
                _bbs.Write("~c1How many will you give?(~c20~c1-~c2" + CurrentUser.Grain.ToString() + "~c1)");
                string s = _bbs.Input(true, false, true, false);
                int.TryParse(s, out int si);
                _bbs.WriteLine("");
                if ((s != "") && (si != 0))
                {
                    if (si > CurrentUser.Grain)
                    {
                        _bbs.Exclaim("You can only afford " + CurrentUser.Grain.ToString() + "!");
                        _bbs.AnyKey(true, true);
                    }
                    else if (si < 0)
                    {
                        _bbs.Exclaim("You cannot give negative bushels!");
                        _bbs.AnyKey(true, true);
                    }
                    else
                    {
                        _bbs.Write("~c1Give ~c2" + si.ToString() + "~c1 bushels?");
                        if (_bbs.YesNo(true, true))
                        {
                            CurrentUser.Grain -= si;
                            GrainSoldiers = si;
                            SaveUser();
                            promptx = true;
                        }
                    }
                }
            }
            _bbs.WriteLine();
            Random r = new Random(DateTime.Now.Millisecond);
            double PeasantDeficit = GrainPeasants / PeasantsRequire;
            int PeasantsDied = 0;
            double SoldiersDeficit = GrainSoldiers / SoldiersRequire;
            if (PeasantDeficit < 1) PeasantsDied = (int)((int)((PeasantsRequire - GrainPeasants)/5) - (int)((r.NextDouble()*(PeasantsRequire-GrainPeasants))/5)+30);
            if ((PeasantDeficit < RANSACK_THRESHHOLD) || (PeasantDeficit < r.NextDouble()))
            {
                _bbs.Exclaim("Your empire was ransacked");
                _bbs.Exclaim("by a horde of starving serfs!");
                CurrentUser.Grain = 0;
                CurrentUser.Soldiers = 0;
                CurrentUser.Gold = 0;
                CurrentUser.Nobles = (int)(CurrentUser.Nobles / 2);
                CurrentUser.Land = (int)(CurrentUser.Land / 2);
                CurrentUser.Serfs = (int)(CurrentUser.Serfs / 2);
                CurrentUser.Markets = CurrentUser.Markets == 0 ? 0 : r.Next(CurrentUser.Markets);
                CurrentUser.Mills = CurrentUser.Mills == 0 ? 0 : r.Next(CurrentUser.Mills);
                CurrentUser.Foundries = CurrentUser.Foundries == 0 ? 0 : r.Next(CurrentUser.Foundries);
                CurrentUser.Shipyards = CurrentUser.Shipyards == 0 ? 0 : r.Next(CurrentUser.Shipyards);
            }
            SaveUser();

            AddNews(_bbs.CurrentUser.Username + " was too STINGY!");
            _bbs.AnyKey(true, true);

            //  //Do Results of Soldier Feeding
            CurrentUser.ArmyMobile = true;
            if (GrainSoldiers < SoldiersRequire)
            {
                CurrentUser.ArmyMobile = false;
                _bbs.Exclaim("Your army is immobilized this year!");
                _bbs.AnyKey(true, true);
            }
            Summary();

            //  //Do Population Growth/Decline
            int DiedNaturally = (int)(r.NextDouble() * (CurrentUser.Serfs / NATURAL_DEATH_ADJUST));
            int BabiesBorn = (int)(r.NextDouble() * (CurrentUser.Serfs / BABIES_BORN_ADJUST));
            int PeopleImmigrated = (int)(r.NextDouble() * (CurrentUser.Serfs / IMMIGRATION_ADJUST));
            int TotalPopulationChange = (BabiesBorn + PeopleImmigrated) - (PeasantsDied + DiedNaturally);
            CurrentUser.Serfs += TotalPopulationChange;
            SaveUser();

            //  //Do Tax Income
            int Taxes = (int)(((GrainPeasants - PeasantsRequire) * (CurrentUser.Tax / 100)) / 2);
            if (Taxes > 0)
            {
                _bbs.WriteLine("~l1~c1You received ~c2" + Taxes.ToString() + "~c1 pounds from taxes.");
                CurrentUser.Gold += Taxes;
                SaveUser();
            }

            //  //Show Population Changes
            _bbs.WriteLine("~c2~l1" + PeasantsDied + "~c1 people starved");
            _bbs.WriteLine("~c2~l1" + BabiesBorn + "~c1 babies were born");
            _bbs.WriteLine("~c2~l1" + DiedNaturally + "~c1 people died naturally");
            _bbs.WriteLine("~c2~l1" + PeopleImmigrated + "~c1 people immigrated");
            _bbs.WriteLine("~c1Your empire " + ((TotalPopulationChange > 0) ? "gained ~c2" : "lost ~c2") + TotalPopulationChange.ToString() + " ~c1citizens.");
            _bbs.WriteLine("The new population is ~c2" + CurrentUser.Serfs.ToString() + "~c1.");
            _bbs.AnyKey(true, true);
        }

        public void Market()
        {
            bool promptx = false;
            while (!promptx)
            {
                _bbs.WriteLine("~s1~c1Investments:~c5");
                _bbs.Divider();
                _bbs.WriteLine("~c21~c1. Mills            ~c22~c1. Markets");
                _bbs.WriteLine("~c23~c1. Shipyards        ~c24~c1. Foundries");
                _bbs.WriteLine("~c25~c1. Castle           ~c26~c1. Nobles");
                _bbs.WriteLine("~c27~c1. Soldiers~c5");
                _bbs.Divider();
                _bbs.WriteLine("~c1You have ~c2" + CurrentUser.Gold.ToString() + "~c1 pounds.");
                _bbs.Write("~c1Which investment? (~c20 ~c1to quit):~c7");
                string ss = _bbs.Input(true,false,true,false,1);
                if (ss == "") ss = "0";
                switch (ss)
                {
                    case "1":
                        BuyMills();
                        break;
                    case "2":
                        BuyMarkets();
                        break;
                    case "3":
                        BuyShipyards();
                        break;
                    case "4":
                        BuyFoundries();
                        break;
                    case "5":
                        BuyCastle();
                        break;
                    case "6":
                        BuyNobles();
                        break;
                    case "7":
                        BuySoldiers();
                        break;
                    default:
                        promptx = true;
                        break;
                }
            }
        }

        private int Buy_Base(string item, int unit_cost)
        {
            bool promptx = false;
            int afford = (int)(CurrentUser.Gold / unit_cost);
            int purchased = 0;
            if (afford > 1)
            {
                while (!promptx)
                {
                    _bbs.WriteLine("~l1~c1"+item+" cost ~c2"+unit_cost.ToString()+"~c1 each.");
                    _bbs.Write("Purchase how many ? (~c20~c1-~c2" + afford.ToString() + "~c1):");
                    string si = _bbs.Input(true, false, true, false);
                    _bbs.WriteLine();
                    if ((si.Length < 1)||(int.TryParse(si, out purchased)))
                    {
                        if (purchased <= afford)
                        {
                            _bbs.Write("~l1~c1Buy ~c2" + purchased.ToString() + "~c1 " + item.ToLower().ToString() + "?");
                            if (_bbs.YesNo(true, true))
                            {
                                //Remove Gold
                                CurrentUser.Gold -= (purchased * unit_cost);
                                promptx = true;
                            }
                            else
                            {
                                //declined confirmation
                                purchased = 0;
                                promptx = true;
                            }
                        }
                        else
                        {
                            //tried to buy more than they can afford
                            _bbs.Exclaim("You can only afford "+afford.ToString()+"!");
                            purchased = 0;
                            _bbs.AnyKey(true, true);
                        }
                    }
                    else
                    {
                        //bad input, bail.
                        purchased = 0;
                        promptx = true;
                    }
                }
            }
            else
            {
                _bbs.WriteLine("~l1~d2You can't afford any!~d0~c1~g1");
                _bbs.AnyKey(true, true);
            }
            return purchased;
        }

        private void BuyMills()
        {
            int MillsBought = Buy_Base("Mills", MILL_COST);
            if (MillsBought > 0)
            {
                CurrentUser.Mills += MillsBought;
                //Gold was already taken out.
                SaveUser();
            }
        }

        private void BuyMarkets()
        {
            int MarketsBought = Buy_Base("Markets", MARKET_COST);
            if (MarketsBought > 0)
            {
                CurrentUser.Markets += MarketsBought;
                //Gold was already taken out.
                SaveUser();
            }
        }
        
        private void BuyShipyards()
        {
            int ShipyardsBought = Buy_Base("Shipyards", SHIPYARD_COST);
            if (ShipyardsBought > 0)
            {
                CurrentUser.Shipyards += ShipyardsBought;
                //Gold was already taken out.
                SaveUser();
            }
        }

        private void BuyFoundries()
        {
            int FoundriesBought = Buy_Base("Foundries", FOUNDRY_COST);
            if (FoundriesBought > 0)
            {
                CurrentUser.Foundries += FoundriesBought;
                //Gold was already taken out.
                SaveUser();
            }
        }

        private void BuyCastle()
        {
            bool promptx = false;
            int afford = (int)(CurrentUser.Gold / CASTLE_COST);
            if (afford > 100 - CurrentUser.Castle) afford = 100 - CurrentUser.Castle;
            int purchased = 0;
            if (CurrentUser.Castle < 100)
            {
                if (afford > 1)
                {
                    while (!promptx)
                    {
                        _bbs.WriteLine("~l1~c1Castle building costs ~c2" + CASTLE_COST.ToString() + "~c1 each 10%.");
                        _bbs.WriteLine("You may only build up to 100%");
                        _bbs.WriteLine("You may only build increments of 10%");
                        _bbs.Write("Build how much? (~c20~c1-~c2" + afford.ToString() + "~c1):");
                        string si = _bbs.Input(true, false, true, false);
                        _bbs.WriteLine();
                        if (si.Contains('%')) si = si.Replace("%", "");
                        if ((si.Length < 1) || (int.TryParse(si, out purchased)))
                        {
                            if (purchased <= afford)
                            {
                                //                END ELSE IF sj mod 10 > 0 THEN BEGIN
                                if (purchased % 10 == 0)
                                {
                                    _bbs.Write("~l1~c1Build ~c2" + purchased.ToString() + "%~c1?");
                                    if (_bbs.YesNo(true, true))
                                    {
                                        //Add castle
                                        CurrentUser.Castle += purchased;
                                        //Remove Gold
                                        CurrentUser.Gold -= (purchased * CASTLE_COST);
                                        promptx = true;
                                    }
                                    else
                                    {
                                        //declined confirmation
                                        promptx = true;
                                    }
                                }
                                else
                                {
                                    //Tried something not an increment of 10
                                    _bbs.Exclaim("You can only build in increments of 10%!");
                                    _bbs.AnyKey(true, true);
                                }
                            }
                            else
                            {
                                //tried to buy more than they can afford
                                _bbs.Exclaim("You can only afford " + afford.ToString() + "!");
                                purchased = 0;
                                _bbs.AnyKey(true, true);
                            }
                        }
                        else
                        {
                            //bad input, bail.
                            promptx = true;
                        }
                    }
                }
                else
                {
                    _bbs.Exclaim("You can't afford any!");
                    _bbs.AnyKey(true, true);
                }
            }
            else
            {
                _bbs.Exclaim("Your castle is already at 100%!");
                _bbs.AnyKey(true, true);
            }

        }

        private void BuyNobles()
        {
            bool promptx = false;
            int afford = (int)(CurrentUser.Gold / NOBLE_COST);
            //IF afford > (Empire_Players[port].Castle div 10) * NOBLESPERCASTLE THEN afford := (Empire_Players[port].Castle div 10) * NOBLESPERCASTLE;
            if (afford > (CurrentUser.Castle / 10) * NOBLESPERCASTLE) afford = (int)((CurrentUser.Castle / 10) * NOBLESPERCASTLE);
            int purchased = 0;
            if (CurrentUser.Nobles < (CurrentUser.Castle/10) * NOBLESPERCASTLE)
            {
                if (afford > 1)
                {
                    while (!promptx)
                    {

                        _bbs.WriteLine("~l1~c1Nobles cost ~c2" + NOBLE_COST.ToString() + "~c1 each.");
                        _bbs.WriteLine("You may have up to ~c2"+ NOBLESPERCASTLE.ToString()+" ~c1 nobles per 10% of castle");
                        _bbs.Write("Buy how many? (~c20~c1-~c2" + afford.ToString() + "~c1):");
                        string si = _bbs.Input(true, false, true, false);
                        _bbs.WriteLine();
                        if ((si.Length < 1) || (int.TryParse(si, out purchased)))
                        {
                            if (purchased <= afford)
                            {
                                //                END ELSE IF sj mod 10 > 0 THEN BEGIN
                                    _bbs.Write("~l1~c1Buy ~c2" + purchased.ToString() + "~c1nobles?");
                                    if (_bbs.YesNo(true, true))
                                    {
                                        //Add nobles
                                        CurrentUser.Nobles += purchased;
                                        //Remove Gold
                                        CurrentUser.Gold -= (purchased * NOBLE_COST);
                                        promptx = true;
                                    }
                                    else
                                    {
                                        //declined confirmation
                                        promptx = true;
                                    }
                            }
                            else
                            {
                                //tried to buy more than they can afford
                                _bbs.Exclaim("You can only afford " + afford.ToString() + "!");
                                purchased = 0;
                                _bbs.AnyKey(true, true);
                            }
                        }
                        else
                        {
                            //bad input, bail.
                            promptx = true;
                        }
                    }
                }
                else
                {
                    _bbs.Exclaim("You can't afford any!");
                    _bbs.AnyKey(true, true);
                }
            }
            else
            {
                _bbs.Exclaim("Build more castle first!");
                _bbs.AnyKey(true, true);
            }


        }

        private void BuySoldiers()
        {
            bool promptx = false;
            int afford = (int)(CurrentUser.Gold / SOLDIER_COST);
            //IF afford > (Empire_Players[port].Castle div 10) * NOBLESPERCASTLE THEN afford := (Empire_Players[port].Castle div 10) * NOBLESPERCASTLE;
            if (afford > CurrentUser.Nobles * SOLDIERSPERNOBLE) afford = CurrentUser.Nobles * SOLDIERSPERNOBLE;
            int purchased = 0;
            if (CurrentUser.Soldiers < CurrentUser.Nobles * SOLDIERSPERNOBLE)
            {
                if (afford > 1)
                {
                    while (!promptx)
                    {

                        _bbs.WriteLine("~l1~c1Soldiers cost ~c2" + SOLDIER_COST.ToString() + "~c1 each.");
                        _bbs.WriteLine("You may have up to ~c2" + SOLDIERSPERNOBLE.ToString() + " ~c1 soldiers per noble");
                        _bbs.Write("Buy how many? (~c20~c1-~c2" + afford.ToString() + "~c1):");
                        string si = _bbs.Input(true, false, true, false);
                        _bbs.WriteLine();
                        if ((si.Length < 1) || (int.TryParse(si, out purchased)))
                        {
                            if (purchased <= afford)
                            {
                                //                END ELSE IF sj mod 10 > 0 THEN BEGIN
                                _bbs.Write("~l1~c1Buy ~c2" + purchased.ToString() + "~c1soldiers?");
                                if (_bbs.YesNo(true, true))
                                {
                                    //Add nobles
                                    CurrentUser.Soldiers += purchased;
                                    //Remove Gold
                                    CurrentUser.Gold -= (purchased * SOLDIER_COST);
                                    promptx = true;
                                }
                                else
                                {
                                    //declined confirmation
                                    promptx = true;
                                }
                            }
                            else
                            {
                                //tried to buy more than they can afford
                                _bbs.Exclaim("You can only afford " + afford.ToString() + "!");
                                purchased = 0;
                                _bbs.AnyKey(true, true);
                            }
                        }
                        else
                        {
                            //bad input, bail.
                            promptx = true;
                        }
                    }
                }
                else
                {
                    _bbs.Exclaim("You can't afford any!");
                    _bbs.AnyKey(true, true);
                }
            }
            else
            {
                _bbs.Exclaim("Buy more nobles first!");
                _bbs.AnyKey(true, true);
            }

        }

        private void Attack()
        {
            bool aquit = false;
            while (!aquit)
            {
                _bbs.WriteLine("~s1~c1Attack Options:~l1(~c2A~c1)ttack Emperor, (~c2L~c1)ist Emperors, (~c2N~c1)o Attack:");
                string s = _bbs.Input(true, false, false, true);
                if (s == "") s = "N";
                switch (s.ToUpper())
                {
                    case "A":
                        SubAttack();
                        break;
                    case "L":
                        List();
                        break;
                    default:
                        aquit = true;
                        break;
                }
            }
        }

        private bool SubAttack()
        {
            bool aquit = false;
            List<EmpireUser> Userlist = ListUsers();
            int VictimNumber = -1;
            while (VictimNumber == -1)
            {
                _bbs.Write("~l1~c1Enter the name or ID of an emperor,~l1QUIT to cancel.~l1Emperor to attack:");
                string s = _bbs.Input(true, false, false).ToUpper();
                if (s == "QUIT")
                {
                    aquit = true;
                    VictimNumber = -2;
                }
                else
                {
                    if (int.TryParse(s, out int tempnum))
                    {
                        if (Userlist.Count(p => p.UserId.Equals(tempnum)) > 0)
                        {
                            VictimNumber = tempnum;
                        }
                        else
                        {
                            _bbs.Exclaim("No emperor with that ID found!");
                            _bbs.AnyKey(true, true);
                        }
                    }
                    else
                    {
                        if (Userlist.Count(p => p.Username.ToUpper().Equals(s.ToUpper())) > 0)
                        {
                            VictimNumber = Userlist.FirstOrDefault(p => p.Username.ToUpper().Equals(s.ToUpper())).UserId;
                        }
                        else
                        {
                            _bbs.Exclaim("No emperor with that name found!");
                            _bbs.AnyKey(true, true);
                        }
                    }
                }
            }
            if (VictimNumber >= 0)
            {
                if (VictimNumber == CurrentUser.UserId)
                {
                    _bbs.WriteLine("~l1~d2~g1Attack yourself??...~l1~d2Ok, fucktard.~d0~c1~g1");
                    _bbs.WriteLine("~c1As ordered, your soldiers attack your");
                    _bbs.WriteLine("~c1own kingdom.  They drag you out into");
                    _bbs.WriteLine("~c1the street, kick the living shit out");
                    _bbs.WriteLine("~c1of you, then take turns ~c7pissing ~c1on");
                    _bbs.WriteLine("~c1your twitching carcass.");
                    CurrentUser.Land = (int)(CurrentUser.Land / 2);
                    SaveUser();
                    _bbs.AnyKey(true, true);
                    aquit = true;
                }
                else
                {
                    EmpireUser Victim = Userlist.FirstOrDefault(p => p.UserId.Equals(VictimNumber));
                    _bbs.WriteLine("~l1~c2A ~c1 - Attack Army");
                    _bbs.WriteLine("~l1~c2B ~c1 - Attack Palace");
                    _bbs.WriteLine("~l1~c2C ~c1 - Attack Nobles");
                    _bbs.WriteLine("~l1~c2D ~c1 - Done Attacking");
                    _bbs.Write("~c1Battle Command:~c2");
                    string s = _bbs.Input(true,false,false,true,1);
                    if (s == "") s = "D";
                    _bbs.WriteLine();
                    switch (s.ToUpper())
                    {
                        case "A":
                            Attack_Army(Victim);
                            aquit = true;
                            break;
                        case "B":
                            Attack_Palace(Victim);
                            aquit = true;
                            break;
                        case "C":
                            Attack_Nobles(Victim);
                            aquit = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            return aquit;
        }

        private void Attack_Nobles(EmpireUser Victim)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            if (Victim.Nobles >= 2)
            {
                if (CurrentUser.Nobles < ((int)(CurrentUser.Land / 1000)))
                {
                    _bbs.WriteLine("~l1~c1You sneak up on a noble...~p1");
                    if (r.Next(CurrentUser.Soldiers) < (Victim.Soldiers * 1.7))
                    {
                        _bbs.Exclaim("Guards appear and halt your attempts!");
                        int LostSoldiers = r.Next((int)(CurrentUser.Soldiers *NOBLE_ATTACK_SOLDIERS)) + 2;
                        if (LostSoldiers > CurrentUser.Soldiers) LostSoldiers = CurrentUser.Soldiers;
                        _bbs.WriteLine("~c2The guards kill " + LostSoldiers.ToString() + " of your soldiers.");
                        CurrentUser.Soldiers -= LostSoldiers;
                        SaveUser();
                    }
                    else
                    {
                        _bbs.WriteLine("~c1~l1You punch him out...~l1and drag his twitching carcass off!");
                        int LostSoldiers = r.Next((int)(CurrentUser.Soldiers * NOBLE_ATTACK_SOLDIERS)) + 1;
                        if (LostSoldiers > CurrentUser.Soldiers) LostSoldiers = CurrentUser.Soldiers;
                        _bbs.WriteLine("~c2You lost " + LostSoldiers.ToString() + " in the process.");
                        CurrentUser.Soldiers -= LostSoldiers;
                        CurrentUser.Nobles++;
                        SaveUser();
                        Victim.Nobles--;
                        SaveUser(Victim);
                        AddNews(CurrentUser.Username + "kidnapped one of " + Victim.Username + "'s nobles.");
                    }
                }
                else
                {
                    _bbs.Exclaim("Your other nobles threaten to revolt!");
                }
            }
            else
            {
                _bbs.Exclaim("You can't find any nobles to attack!");
            }
            _bbs.AnyKey(true, true);
        }

        private void Attack_Palace(EmpireUser Victim)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            _bbs.WriteLine("~l1You attack the enemy palace...");
            if (Victim.Castle > 0)
            {
                if (Victim.Soldiers == 0)
                {
                    _bbs.WriteLine("~c2Nobody's even guarding the place!~c1");
                }
                if ((Victim.Soldiers > 0) && (r.Next(CurrentUser.Soldiers) < r.Next(Victim.Soldiers * 2)))
                {
                    _bbs.WriteLine("~l1~d2~g1Guards appear and halt your attempts!~d0~c1~g1");
                    int LostSoldiers = r.Next((int)(CurrentUser.Soldiers * PALACE_ATTACK_SOLDIERS)) + 2;
                    if (LostSoldiers > CurrentUser.Soldiers) LostSoldiers = CurrentUser.Soldiers;
                    _bbs.WriteLine("~c2The guards kill " + LostSoldiers.ToString() + " of your soldiers.");
                    CurrentUser.Soldiers -= LostSoldiers;
                    SaveUser();
                    AddNews(CurrentUser.Username + " attacked " + Victim.Username + "'s castle, but was repelled.");
                }
                else
                {
                    //              Exclaim('You destroyed 10% of their palace!',port);
                    _bbs.WriteLine("~c2You destroyed 10% of their palace!~c1");
                    if (Victim.Soldiers > 0)
                    {
                        int LostSoldiers = r.Next((int)(CurrentUser.Soldiers * PALACE_ATTACK_SOLDIERS)) + 2;
                        if (LostSoldiers > CurrentUser.Soldiers) LostSoldiers = CurrentUser.Soldiers;
                        _bbs.WriteLine("~c2You lost " + LostSoldiers.ToString() + " soldiers in the process.");
                        CurrentUser.Soldiers -= LostSoldiers;
                        SaveUser();
                    }
                    Victim.Castle = Victim.Castle - 10;
                    if (Victim.Castle < 0) Victim.Castle = 0;
                    SaveUser(Victim);
                    AddNews(CurrentUser.Username + " attacked " + Victim.Username + "'s castle");
                }
            }
            else
            {
                _bbs.Exclaim("It's already destroyed!");
            }
            _bbs.AnyKey(true, true);
        }

        private void Attack_Army(EmpireUser Victim)
        {
            //          //Does this farknocker even HAVE an army?
            Random r = new Random(DateTime.Now.Millisecond);
            if (Victim.Soldiers > 0)
            {
                //            //Determine surprise or ambush
                int Initiative = (int)((CurrentUser.Soldiers + CurrentUser.Land) / (Victim.Soldiers + Victim.Land));
                if ((Initiative > 1) || Initiative > r.Next(1))
                {
                    //Ambushed
                    int LostSoldiers = r.Next(CurrentUser.Soldiers);
                    _bbs.WriteLine("~l1~d2~g1Your army got ambushed!~l1~d2"+LostSoldiers.ToString()+" of your soldiers died.~d0~c1");
                    CurrentUser.Soldiers -= LostSoldiers;
                    SaveUser();
                }
                else
                {
                    int KilledSoldiers = r.Next(Victim.Soldiers);
                    _bbs.WriteLine("~l1~c1You got the drop on them! ~l1" + KilledSoldiers.ToString() + " of their soldiers died.");
                    Victim.Soldiers -= KilledSoldiers;
                    SaveUser(Victim);
                }
                _bbs.WriteLine("~l1~c7(~c1+~c7)=Enemy Death (~c2-~c7)=Your Death~c5");
                _bbs.Divider();
                while ((CurrentUser.Soldiers > 0) && (Victim.Soldiers > 0))
                {
                    if (
                        ((r.Next(50) + (CurrentUser.Soldiers * 0.05) + (CurrentUser.Land * 0.002))) >
                        ((r.Next(50) + (Victim.Soldiers * 0.05) + (Victim.Land * 0.002)))
                        )
                    {
                        _bbs.Write("~c1+");
                        Victim.Soldiers--;
                    }
                    else
                    {
                        _bbs.Write("~c2-");
                        CurrentUser.Soldiers--;
                    }
                        
                }
                _bbs.WriteLine("~c5");
                _bbs.Divider();
                //          //Victory or Defeat
                int LandTaken = 0;
                if (Victim.Soldiers == 0)
                {
                    //Victory
                    LandTaken = (int)((CurrentUser.Soldiers * 0.7) + 200 + r.Next(300) + r.Next(200) + (Victim.Land * 0.01));
                    if (LandTaken > Victim.Land) LandTaken = Victim.Land;
                    _bbs.WriteLine("~l1~d5~g1Your forces were victorious!~d0~c1");
                    _bbs.WriteLine("~l1~c2Your " + CurrentUser.Soldiers.ToString() + " remaining soldiers managed to~l1 capture "+LandTaken.ToString()+" acres....");
                    AddNews(CurrentUser.Username + " defeated " + Victim.Username + " and seized " + LandTaken.ToString() + " acres.");
                }
                else
                {
                    //Defeat
                    LandTaken = CurrentUser.Soldiers - Victim.Soldiers;
                    if (LandTaken < 0) LandTaken = LandTaken * -1;
                    LandTaken = (r.Next(LandTaken * 4) + 100);
                    if (LandTaken > Victim.Land) LandTaken = Victim.Land;

                    _bbs.Exclaim("Your forces were defeated!");
                    _bbs.WriteLine("~l1~c2But they managed to~l1 seize " + LandTaken.ToString() + " acres....");

                    AddNews(Victim.Username + " defeated " + CurrentUser.Username + " but lost " + LandTaken.ToString() + " acres");
                }
                Victim.Land -= LandTaken;
                CurrentUser.Land += LandTaken;
                SaveUser();
                SaveUser(Victim);
                if (Victim.Land <= 0)
                {
                    //They dead.
                    _bbs.WriteLine("~l1~c1You also killed " + Victim.Username + "!");
                    DeleteUser(Victim.UserId);
                    AddNews(CurrentUser.Username + " KILLED " + Victim.Username + " and siezed " + LandTaken.ToString() + " acres.");
                }
                else
                {
                    _bbs.WriteLine("~l1~c1" + Victim.Username + " has " + Victim.Land.ToString() + " acres left.");
                }
            }
            else
            {
                _bbs.Exclaim("What army?");
            }
            _bbs.AnyKey(true, true);
        }

        private void Events()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int Event = r.Next(12);
            switch (Event)
            {
                case 1:
                //Worker Revolt
                _bbs.WriteLine("~l1~d2~g1Your workers revolt!");
                if ((CurrentUser.Foundries > 0) || (CurrentUser.Shipyards > 0))
                {
                    int FoundriesDestroyed = (CurrentUser.Foundries == 0) ? 0 : r.Next(CurrentUser.Foundries / 2);
                    int ShipyardsDestroyed = (CurrentUser.Shipyards == 0) ? 0 : r.Next(CurrentUser.Shipyards / 2);
                    if ((FoundriesDestroyed > 0) || (ShipyardsDestroyed > 0))
                    {
                        if (FoundriesDestroyed > 0)
                        {
                            _bbs.WriteLine("~d2~g1" + FoundriesDestroyed.ToString() + " foundries are destroyed!");
                            CurrentUser.Foundries -= FoundriesDestroyed;
                        }
                        if (ShipyardsDestroyed > 0)
                        {
                            _bbs.WriteLine("~d2~g1" + ShipyardsDestroyed.ToString() + " shipyards are destroyed!");
                            CurrentUser.Shipyards -= ShipyardsDestroyed;
                        }
                        _bbs.Write("~d0~c1");
                        SaveUser();
                    }
                    else
                    {
                        _bbs.WriteLine("~l1~d2~g1BUT...!~l1~d2~g1Your soldiers shut it down...~d0~c1");
                    }
                }
                else
                {
                    _bbs.WriteLine("~l1~d2~g1BUT...!~l1~d2~g1You don't have shit to destroy...~l1~d2~g1LOSER.~d0~c1");
                }
                    _bbs.AnyKey(true, true);
                    break;
                case 2:
                //Plague
                    _bbs.WriteLine("~l1~d2~g1Plague Strikes!~d0~c1");
                    if (CurrentUser.Serfs > 0)
                    {
                        int PlagueKills = r.Next((int)(CurrentUser.Serfs * PLAGUE_STRENGTH));
                        if (PlagueKills >= 1)
                        {
                            _bbs.WriteLine("~d2" + PlagueKills.ToString() + " of your people become soil~l1~d2for next year's growing season.~d0~c1");
                            CurrentUser.Serfs -= PlagueKills;
                            SaveUser();
                        }
                        else
                        {
                            _bbs.WriteLine("~l1~d2~g1BUT...!~l1~d2~g1Your people escape the black hand of death.~d0~c1");
                        }
                    }
                    else
                    {
                        _bbs.WriteLine("~l1~d2~g1BUT...!~l1~d2~g1All your serfs are dead already.~l1~d2~g1LOSER.~d0~c1");
                    }
                    _bbs.AnyKey(true, true);
                    break;
                case 3:
                    _bbs.WriteLine("~l1~d2~g1Fire breaks out!~d0~c1");
                    if (CurrentUser.Serfs > 0 || CurrentUser.Land > 0)
                    {
                        int SerfsBurned = r.Next((int)(CurrentUser.Serfs * 0.2));
                        int LandBurned = r.Next((int)(CurrentUser.Land / 3));
                        if ((SerfsBurned > 0) || (LandBurned > 0))
                        {
                            if (LandBurned > 0)
                            {
                                _bbs.WriteLine("~d2" + LandBurned.ToString() + " acres are scorched.~d0~c1");
                                CurrentUser.Land -= LandBurned;
                            }
                            if (SerfsBurned > 0)
                            {
                                _bbs.WriteLine("~d2" + SerfsBurned.ToString() + " people are melted.~d0~c1");
                                CurrentUser.Serfs -= SerfsBurned;
                            }
                            SaveUser();
                        }
                        else
                        {
                            _bbs.WriteLine("~l1~d2~g1BUT...!~l1~d2~g1Your soldiers put it out...~l1~d2~g1and nothing is damaged.~d0~c1");
                        }
                    }
                    else
                    {
                        _bbs.WriteLine("~l1~d2~g1BUT...!~l1~d2~g1Your kingdom is so shitty...~l1~d2~g1This is an improvement.~d0~c1");
                    }
                    _bbs.AnyKey(true, true);
                    break;
                case 4:
                //Thieves

                    _bbs.WriteLine("~l1~d2~g1Thieves storm your palace!~d0~c1");
                    if (CurrentUser.Gold > 0 )
                    {
                        int GoldStolen = r.Next((int)(CurrentUser.Gold * 0.6));
                        if ((GoldStolen > 0) )
                        {
                            _bbs.WriteLine("~d2" + GoldStolen.ToString() + " gold is stolen.~d0~c1");
                            CurrentUser.Gold -= GoldStolen;
                            SaveUser();
                        }
                        else
                        {
                            _bbs.WriteLine("~l1~d2~g1BUT...!~l1~d2~g1Your soldiers kill them...~l1~d2~g1and nothing is stolen.~d0~c1");
                        }
                    }
                    else
                    {
                        _bbs.WriteLine("~l1~d2~g1BUT...!~l1~d2~g1You don't have shit...~l1~d2~g1So they piss on your rug.~d0~c1");
                    }
                    _bbs.AnyKey(true, true);
                    break;
                case 5:
                //Land Gift
                    int LandGiven = 0;
                    if (CurrentUser.Land == 0)
                    {
                        LandGiven = r.Next(100);
                    }
                    else
                    {
                        LandGiven = r.Next(CurrentUser.Land * 3);
                    }
                    _bbs.WriteLine("~d7You receive " + LandGiven.ToString() + " acres as a gift.~d0~c1");
                    CurrentUser.Land += LandGiven;
                    SaveUser();
                    _bbs.AnyKey(true, true);
                    break;
                case 6:
                //Money Gift
                    int GoldGiven = 0;
                    if (CurrentUser.Gold == 0)
                    {
                        GoldGiven = r.Next(1000);
                    }
                    else
                    {
                        GoldGiven = r.Next(CurrentUser.Gold * 2);
                    }
                    _bbs.WriteLine("~d7You receive " + GoldGiven.ToString() + " pounds as a gift.~d0~c1");
                    CurrentUser.Gold += GoldGiven;
                    SaveUser();
                    _bbs.AnyKey(true, true);
                    break;
                default:
                    break;
            }
        }

        private void Reinforce()
        {
            Summary();
            _bbs.WriteLine("~l1~c1Would you like to buy reinforcements?");
            if (_bbs.YesNo(true, true))
            {
                int SoldiersAfford = (int)(CurrentUser.Gold / SOLDIER_COST);
                int MaxAllowedToBuy = (CurrentUser.Nobles * SOLDIERSPERNOBLE)-CurrentUser.Soldiers;

                if (SoldiersAfford >= 1)
                {
                    if (MaxAllowedToBuy > 0)
                    {
                        bool promptx = false;
                        while (!promptx)
                        {
                            _bbs.WriteLine("~c1Soldiers cost ~c2" + SOLDIER_COST.ToString() + "~c1 each.");
                            _bbs.WriteLine("~c1You may have up to ~c2" + SOLDIERSPERNOBLE.ToString() + "~c1 soldiers per noble.");
                            int RawAfford = ((SoldiersAfford < MaxAllowedToBuy) ? SoldiersAfford : MaxAllowedToBuy);
                            _bbs.Write("~c1Purchase how many?(~c20~c1-~c2" + RawAfford.ToString() + "~c1):");
                            string s = _bbs.Input(true, false, true);
                            if (int.TryParse(s, out int purchase))
                            {
                                if (purchase > 0)
                                {
                                    if (purchase <= RawAfford)
                                    {
                                        _bbs.WriteLine("~l1~c1Buy ~c2" + purchase.ToString() + " ~c1soldiers for ~c2" + (purchase * SOLDIER_COST).ToString() + "~c1?");
                                        if (_bbs.YesNo(true, true))
                                        {
                                            CurrentUser.Gold -= (purchase * SOLDIER_COST);
                                            CurrentUser.Soldiers += purchase;
                                            SaveUser();
                                            promptx = true;
                                            promptx = true;
                                        }
                                    }
                                    else
                                    {
                                        _bbs.WriteLine("~l1~d2~g1You can't afford that many.");
                                    }
                                }
                                else
                                {
                                    if (purchase < 0)
                                    {
                                        _bbs.WriteLine("~l1~d2~g1You can't purchase negative soldiers.");
                                    }
                                    else
                                    {
                                        promptx = true;
                                    }
                                }
                            }
                            else
                            {
                                _bbs.WriteLine("~l1~d2~g1That's not a number.");
                            }
                        }
                    }
                    else
                    {
                        _bbs.WriteLine("~l1~d2You need more nobles first!~g1~d0~c1");
                        _bbs.AnyKey(true, true);

                    }
                }
                else
                {
                    _bbs.WriteLine("~l1~d2You can't afford any!~g1~d0~c1");
                    _bbs.AnyKey(true, true);
                }
            }
        }

        private void List()
        {
            List<EmpireUser> ulist = ListUsers();
            _bbs.WriteLine("~s1~c1ID  Name                 Land      Gold~c5");
            _bbs.Divider();
            foreach (EmpireUser user in ulist)
            {
                _bbs.Write("~c2" + Utils.Clip(user.UserId.ToString(), 4, true));
                _bbs.Write("~c1" + Utils.Clip(user.Username, 16, true));
                _bbs.Write("~c7" + Utils.Clip(user.Land.ToString(), 10, true));
                _bbs.Write(Utils.Clip(user.Gold.ToString(), 10, true));
            }
            _bbs.Write("~c5");
            _bbs.Divider();
            _bbs.WriteLine("~c1");
            _bbs.AnyKey(true, true);
        }

        private void Stock()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            //Need to involve scale adjustments in this set of calcs
            int MarketProfit = (r.Next(MARKET_PROFIT_PER_MAX) + MARKET_PROFIT_BASE) * CurrentUser.Markets;
            int MillProfit = (r.Next(MILL_PROFIT_PER_MAX) + MILL_PROFIT_BASE) * CurrentUser.Mills;
            int FoundryProfit = (r.Next(FOUNDRY_PROFIT_PER_MAX) + FOUNDRY_PROFIT_BASE) * CurrentUser.Foundries;
            int ShipyardProfit = (r.Next(SHIPYARD_PROFIT_PER_MAX) + SHIPYARD_PROFIT_BASE) * CurrentUser.Shipyards;

            int TotalProfit = MarketProfit + MillProfit + FoundryProfit + ShipyardProfit;

            _bbs.WriteLine("~s1~c7Profit Report From The Stock Market~c5");
            _bbs.Divider();
            _bbs.WriteLine("~c1     Markets: ~c2" + MarketProfit.ToString());
            _bbs.WriteLine("~c1       Mills: ~c2" + MillProfit.ToString());
            _bbs.WriteLine("~c1   Foundries: ~c2" + FoundryProfit.ToString());
            _bbs.WriteLine("~c1   Shipyards: ~c2" + ShipyardProfit.ToString());
            _bbs.Write("~c5");
            _bbs.Divider();
            _bbs.WriteLine("!c1Total Profit: ~c2" + TotalProfit.ToString());
            CurrentUser.Gold += TotalProfit;
            SaveUser();
            _bbs.AnyKey(true, true);
        }

        private void SaveUser(EmpireUser user)
        {
            _dataInterface.SaveUserDefinedField(user.UserId, "EMPIRE", EmpireUser.Serialize(_bbs, user));
        }

        private void SaveUser()
        {
            _dataInterface.SaveUserDefinedField(CurrentUser.UserId, "EMPIRE", EmpireUser.Serialize(_bbs, CurrentUser));
        }

        private void DeleteUser(int UserId)
        {
            //Stupid fuck died.
            _dataInterface.SaveUserDefinedField(UserId, "EMPIRE", "");
        }

        private List<EmpireUser> ListUsers()
        {
            List<EmpireUser> ulist = new List<EmpireUser>();
            List<IdAndKeys> rawlist = _dataInterface.GetAllUserDefinedFieldsWithKey("EMPIRE");
            foreach (IdAndKeys s in rawlist)
            {
                ulist.Add(EmpireUser.Deserialize(_bbs, s.Keys["data"]));
            }
            return ulist;
        }

        private void LoadUser()
        {
            //Load User Record if there is one.
            string dustr = _dataInterface.GetUserDefinedField(_bbs.CurrentUser.UserId, "EMPIRE");
            if (dustr == "")
            {
                _bbs.Write("~c1~l1It looks like this is your first time.~l1~c1Setting up your account.~p1.~p1.");
                CurrentUser = new EmpireUser()
                {
                    UserId = _bbs.CurrentUser.UserId,
                    Username = _bbs.CurrentUser.Username,
                    ArmyMobile = true,
                    Land = 100,
                    Grain = 10000,
                    Gold = 1000,
                    Tax = 10,
                    Serfs = 1000,
                    Soldiers = 40,
                    Nobles = 1,
                    Castle = 0,
                    Mills = 0,
                    Shipyards = 0,
                    Foundries = 0,
                    Turns = 5,
                    LastPlay = DateTime.Now
                };
                SaveUser();
            }
            else
            {
                CurrentUser = EmpireUser.Deserialize(_bbs, dustr);
                TimeSpan since_last = DateTime.Now - CurrentUser.LastPlay;
                CurrentUser.Turns += since_last.Hours;
                CurrentUser.LastPlay = DateTime.Now;
                SaveUser();
            }

        }





    }
}
