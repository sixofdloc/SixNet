//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using SixNet_BBS.Data.Objects;

//namespace SixNet_BBS.Data
//{
//    class DatabaseInitializer : CreateDatabaseIfNotExists<BBSDataContext>
//    {
//        protected override void Seed(BBSDataContext context)
//        {
//            AccessGroup ag = new AccessGroup() { CallsPerDay = 2, Description = "New Users who have not yet been validated", Title = "New Users", AccessGroupNumber = 0, Flag_Remote_Maintenance = false, Is_SysOp = false, MinutesPerCall = 30 };
//            context.AccessGroups.Add(ag);
//            AccessGroup ag2 = new AccessGroup() { CallsPerDay = 255, Description = "System Operators", Title = "SysOps", AccessGroupNumber = 255, Flag_Remote_Maintenance = true, Is_SysOp = true, MinutesPerCall = 512 };
//            context.AccessGroups.Add(ag2);
//            AccessGroup ag3 = new AccessGroup() { CallsPerDay = 255, Description = "Assistant System Operators", Title = "CoOps", AccessGroupNumber = 254, Flag_Remote_Maintenance = false, Is_SysOp = false, MinutesPerCall = 512 };
//            context.AccessGroups.Add(ag3);
//            AccessGroup ag4 = new AccessGroup() { CallsPerDay = 5, Description = "Normal Users", Title = "Users", AccessGroupNumber = 100, Flag_Remote_Maintenance = false, Is_SysOp = false, MinutesPerCall = 60 };
//            context.AccessGroups.Add(ag4);
//            AccessGroup ag5 = new AccessGroup() { CallsPerDay = 10, Description = "Elite Users", Title = "Elites", AccessGroupNumber = 200, Flag_Remote_Maintenance = false, Is_SysOp = false, MinutesPerCall = 60 };
//            context.AccessGroups.Add(ag4);

//            context.SaveChanges();


            
//            User u = new User() { Username = "Six", HashedPassword = "trojan", LastConnection = DateTime.Now, LastConnectionIP = "127.0.0.1", LastDisconnection = DateTime.Now, AccessLevel=255, ComputerType="Commodore 64", Email="six@darklordsofchaos.com", RealName="Oliver Clothesoff" };
//            context.Users.Add(u);
//            GFileArea g1 = new GFileArea() { Title = "Classics", LongDescription = "Classic 80s BBS Text Files", ParentAreaId = -1, AccessLevel=200 };
//            GFileArea g2 = new GFileArea() { Title = "Humor", LongDescription = "Jokes and Amusement Files", ParentAreaId = -1 , AccessLevel=100};
//            context.GFileAreas.Add(g1);
//            context.GFileAreas.Add(g2);
//            context.SaveChanges();

//            GFileArea g3 = new GFileArea() { Title = "Jokes", LongDescription = "Lists of Jokes", ParentAreaId = g2.GFileAreaId , AccessLevel=100};
//            GFileArea g4 = new GFileArea() { Title = "Hacking", LongDescription = "Hacking Vintage Systems", ParentAreaId = g1.GFileAreaId , AccessLevel=200};
//            GFileArea g5 = new GFileArea() { Title = "Phreaking", LongDescription = "Outdated Phone Tech Data", ParentAreaId = g1.GFileAreaId , AccessLevel=200};
//            GFileArea g6 = new GFileArea() { Title = "Anarchy", LongDescription = "Anarchy and Explosives", ParentAreaId = g1.GFileAreaId, AccessLevel = 200 };
//            GFileArea g7 = new GFileArea() { Title = "Boxes", LongDescription = "Old Color-Box Files", ParentAreaId = g1.GFileAreaId, AccessLevel = 200 };
//            context.GFileAreas.Add(g3);
//            context.GFileAreas.Add(g4);
//            context.GFileAreas.Add(g5);
//            context.GFileAreas.Add(g6);
//            context.GFileAreas.Add(g7);

//            context.SaveChanges();


//            MessageBaseArea mba = new MessageBaseArea() { Title = "Elite", ParentAreaId = -1, AccessLevel = 200, LongDescription = "Elite Members Only" };
//            MessageBaseArea mbb = new MessageBaseArea() { Title = "Programming", ParentAreaId = -1, AccessLevel = 10, LongDescription = "Retrocomputing Development" };
//            context.MessageBaseAreas.Add(mba);
//            context.MessageBaseAreas.Add(mbb);
//            context.SaveChanges();

//            MessageBase mb = new MessageBase() { Title = "Classic H/P/A", ParentArea = mba.MessageBaseAreaId, AccessLevel = 200, LongDescription = "Old-School Hacker chat" };
//            MessageBase mb2 = new MessageBase() { Title = "Warez", ParentArea = mba.MessageBaseAreaId, AccessLevel = 200, LongDescription = "Warez chat" };
//            MessageBase mb3 = new MessageBase() { Title = "General", ParentArea = -1, AccessLevel = 10, LongDescription = "General Discussions" };
//            MessageBase mb4 = new MessageBase() { Title = "BBSing", ParentArea = -1, AccessLevel = 10, LongDescription = "Modern BBSing" };
//            MessageBase mb5 = new MessageBase() { Title = "C64", ParentArea = -1, AccessLevel = 10, LongDescription = "C64 Discussions" };
//            MessageBase mb6 = new MessageBase() { Title = "Amiga", ParentArea = -1, AccessLevel = 10, LongDescription = "Amiga Discussions" };
//            MessageBase mb7 = new MessageBase() { Title = "DOS", ParentArea = -1, AccessLevel = 10, LongDescription = "DOS Discussions" };
//            MessageBase mb8 = new MessageBase() { Title = "CG Art", ParentArea = -1, AccessLevel = 10, LongDescription = "CBM C/G Artwork" };
//            MessageBase mb9 = new MessageBase() { Title = "ANSI Art", ParentArea = -1, AccessLevel = 10, LongDescription = "ANSI Artwork" };

//            MessageBase mb10 = new MessageBase() { Title = "DOS", ParentArea = mbb.MessageBaseAreaId, AccessLevel = 10, LongDescription = "DOS Programming" };
//            MessageBase mb11 = new MessageBase() { Title = "Amiga", ParentArea = mbb.MessageBaseAreaId, AccessLevel = 10, LongDescription = "Amiga Programming" };
//            MessageBase mb12 = new MessageBase() { Title = "C64", ParentArea = mbb.MessageBaseAreaId, AccessLevel = 10, LongDescription = "C64 Programming" };

//            MessageBase mb13 = new MessageBase() { Title = "Suggestion Box", ParentArea = -1, AccessLevel = 10, LongDescription = "Suggestions for the BBS" };

//            context.MessageBases.Add(mb);
//            context.MessageBases.Add(mb2);
//            context.MessageBases.Add(mb3);
//            context.MessageBases.Add(mb4);
//            context.MessageBases.Add(mb5);
//            context.MessageBases.Add(mb6);
//            context.MessageBases.Add(mb7);
//            context.MessageBases.Add(mb8);
//            context.MessageBases.Add(mb9);
//            context.MessageBases.Add(mb10);
//            context.MessageBases.Add(mb11);
//            context.MessageBases.Add(mb12);
//            context.MessageBases.Add(mb13);

//            context.SaveChanges();

//            PFileDetail pfd = new PFileDetail() { Added = DateTime.Now, Description = "Dopewars", Filename = "PFiles//PFile_Dopewars.dll", ParentAreaId = -1, PFileNumber = 1 };
//            context.PFileDetails.Add(pfd);
//            PFileDetail pfd2 = new PFileDetail() { Added = DateTime.Now, Description = "Empire", Filename = "PFiles//PFile_Empire.dll", ParentAreaId = -1, PFileNumber = 2 };
//            context.PFileDetails.Add(pfd2);
//            context.SaveChanges();

//            base.Seed(context);
//        }
//    }
//}
