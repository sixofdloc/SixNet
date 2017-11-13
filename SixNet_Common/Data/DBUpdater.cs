using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Reflection;
using SixNet_Logger;

namespace SixNet_BBS.Data
{
    public class DBUpdater
    {
        private const int SOFTWARE_DATABASE_VERSION = 0;
        public string ErrorMessage;

        public bool Update()
        {
            bool b = false;
            Type thisType = this.GetType();
            BBSDataContext db = new BBSDataContext();
            SysConfig sc = db.SysConfigs.First(p => true);
            int oldDBversion = sc.DatabaseVersion;
            for (int ver = oldDBversion; ver < SOFTWARE_DATABASE_VERSION; ver++)
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        MethodInfo theMethod = thisType.GetMethod("UpdateFrom" + ver.ToString() + "To" + (ver + 1).ToString());
                        bool bb = (bool)(theMethod.Invoke(this, new object[] { db }));
                        if (bb)
                        {
                            db.Database.ExecuteSqlCommand("UPDATE SysConfigs SET DatabaseVersion=" + (ver + 1).ToString());
                            scope.Complete();
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        LoggingAPI.LogEntry("Exception in DBUpdater.Update: " + e.Message);
                        return false;
                    }
                }
                b = true;
            }
            return b;
        }

        public static bool InitializeDatabase()
        {
            bool b = true;
            BBSDataContext bbs = BBSDataContext.GetContext();
            SysConfig config = null;
            try
            {
                config = bbs.SysConfigs.First(p => true);
            }
            catch (Exception e)
            {
                //nada
                LoggingAPI.LogEntry("Exception in DBUpdater.InitializeDatabase (Initial config check): " + e.Message);
            }
            if (config == null)
            {
                //Create base config
                config = new SysConfig()
                {
                    DatabaseVersion = 0
                };
                bbs.SysConfigs.Add(config);

                bbs.SaveChanges();

                //Create Sysop User
                User u = new User()
                {
                    Username = "Six",
                    HashedPassword = "trojan",
                    LastConnection = DateTime.Now,
                    LastConnectionIP = "127.0.0.1",
                    LastDisconnection = DateTime.Now
                };
                bbs.Users.Add(u);
                bbs.SaveChanges();

                //Reload to get id in case it didn't autofill
                u = bbs.Users.FirstOrDefault(p=>p.Username.Equals("Six"));

                //Create base bbsconfig
                BBSConfig bconfig = new BBSConfig()
                {
                    SysOpUserId = u.UserId,
                    BBS_Name = "The Darkside BBS"
                };

                bbs.BBSConfigs.Add(bconfig);

                bbs.SaveChanges();
            }
            return b;
        }

        //bool UpdateFrom0To1(BBSDataContext db)
        //{
        //    bool b = false;
        //    try
        //    {
        //        string query = "ALTER TABLE Designs ADD PersonalizationLevel int not null default 0";
        //        db.Database.ExecuteSqlCommand(query);
        //        b = true;
        //    }
        //    catch (Exception e)
        //    {
        //        b = false;
        //        this.ErrorMessage = "UpdateFrom1To2 Failed: " + e.Message;
        //    }
        //    return b;
        //}

        //public bool UpdateFrom2To3(DataContext db)
        //{
        //    //Adds price to envelopes
        //    //Adds per-card amounts
        //    //Adds card fields for receipt and card back
        //    //Adds kiosk fields for receipt and card back
        //    bool b = false;
        //    try
        //    {
        //        string query = "ALTER TABLE Envelopes ADD Price decimal(18,2) not null default 4.99";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Designs ADD Amount1 int not null default 0";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Designs ADD Amount2 int not null default 0";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Designs ADD Amount3 int not null default 0";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Designs ADD Amount4 int not null default 0";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Designs ADD Amount5 int not null default 0";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Designs ADD Amount6 int not null default 0";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Designs ADD CardBack text not null default ''";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Designs ADD Receipt text not null default ''";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Kiosks ADD CardBack text not null default ''";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Kiosks ADD Receipt text not null default ''";
        //        db.Database.ExecuteSqlCommand(query);
        //        b = true;
        //    }
        //    catch (Exception e)
        //    {
        //        b = false;
        //        this.ErrorMessage = "UpdateFrom2To3 Failed: " + e.Message;
        //    }
        //    return b;
        //}

        //public bool UpdateFrom3To4(DataContext db)
        //{
        //    //Adds Tables for remote control messages
        //    bool b = false;
        //    try
        //    {
        //        string query = "CREATE TABLE MessageQueues ("
        //        + "MessageQueueId int IDENTITY(1,1) NOT NULL,"
        //        + "ResponseToMessageQueueId INT NOT NULL DEFAULT 0,"
        //        + "KioskId int NOT NULL DEFAULT 0,"
        //        + "MessageText NVARCHAR(MAX) NOT NULL DEFAULT '',"
        //        + "KioskIsRecipient bit NOT NULL DEFAULT 1,"
        //        + " CONSTRAINT PK_MessageQueue PRIMARY KEY CLUSTERED (MessageQueueId ASC)"
        //        + " WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
        //        db.Database.ExecuteSqlCommand(query);

        //        query = "CREATE TABLE UploadedFiles ("
        //        + "UploadedFileId int IDENTITY(1,1) NOT NULL,"
        //        + "Filename NVARCHAR(255) NOT NULL DEFAULT '',"
        //        + "KioskId int NOT NULL DEFAULT 0,"
        //        + "InResponseToMessageId int NOT NULL DEFAULT 0,"
        //        + "Complete BIT NOT NULL DEFAULT 0,"
        //        + " CONSTRAINT PK_UploadedFile PRIMARY KEY CLUSTERED (UploadedFileId ASC)"
        //        + " WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
        //        db.Database.ExecuteSqlCommand(query);


        //        query = "ALTER TABLE Kiosks ADD LastMessageFetched INT NOT NULL DEFAULT 0";
        //        db.Database.ExecuteSqlCommand(query);

        //        b = true;
        //    }
        //    catch (Exception e)
        //    {
        //        b = false;
        //        this.ErrorMessage = "UpdateFrom3To4 Failed: " + e.Message;
        //    }
        //    return b;
        //}

        //public bool UpdateFrom4To5(DataContext db)
        //{
        //    //Adds Terms & Conditions to Companies, Kiosks, Cards
        //    bool b = false;
        //    try
        //    {
        //        string query = "ALTER TABLE Companies ADD terms VARCHAR(MAX) NOT NULL DEFAULT ''";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Kiosks ADD terms VARCHAR(MAX) NOT NULL DEFAULT ''";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "ALTER TABLE Designs ADD terms VARCHAR(MAX) NOT NULL DEFAULT ''";
        //        db.Database.ExecuteSqlCommand(query);
        //        b = true;
        //    }
        //    catch (Exception e)
        //    {
        //        b = false;
        //        this.ErrorMessage = "UpdateFrom4To5 Failed: " + e.Message;
        //    }
        //    return b;
        //}

        //public bool UpdateFrom5To6(DataContext db)
        //{
        //    //Adds Signatures
        //    bool b = false;
        //    try
        //    {
        //        string query = "CREATE TABLE Signatures ("
        //        + "SignatureId int IDENTITY(1,1) NOT NULL,"
        //        + "TransactionId INT NOT NULL DEFAULT 0,"
        //        + "Points NVARCHAR(MAX) NOT NULL DEFAULT '',"
        //        + " CONSTRAINT PK_Signature PRIMARY KEY CLUSTERED (SignatureId ASC)"
        //        + " WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";

        //        db.Database.ExecuteSqlCommand(query);
        //        b = true;
        //    }
        //    catch (Exception e)
        //    {
        //        b = false;
        //        this.ErrorMessage = "UpdateFrom5To6 Failed: " + e.Message;
        //    }
        //    return b;
        //}

        //public bool UpdateFrom6To7(DataContext db)
        //{
        //    //Adds Terms & Conditions to Companies, Kiosks, Cards
        //    bool b = false;
        //    try
        //    {
        //        string query = "ALTER TABLE Envelopes ADD Message1 VARCHAR(256) NOT NULL DEFAULT ''," +
        //                                                    "Message2 VARCHAR(256) NOT NULL DEFAULT ''," +
        //                                                    "Message3 VARCHAR(256) NOT NULL DEFAULT ''," +
        //                                                    "Message4 VARCHAR(256) NOT NULL DEFAULT ''," +
        //                                                    "Message5 VARCHAR(256) NOT NULL DEFAULT ''";
        //        db.Database.ExecuteSqlCommand(query);
        //        b = true;
        //    }
        //    catch (Exception e)
        //    {
        //        b = false;
        //        this.ErrorMessage = "UpdateFrom6To7 Failed: " + e.Message;
        //    }
        //    return b;
        //}

        //public bool UpdateFrom7To8(DataContext db)
        //{
        //    //Adds Terms & Conditions to Companies, Kiosks, Cards
        //    bool b = false;
        //    try
        //    {
        //        string query = "ALTER TABLE Kiosks ADD LocationName VARCHAR(256) NOT NULL DEFAULT ''," +
        //                                                    "Address VARCHAR(256) NOT NULL DEFAULT ''," +
        //                                                    "City VARCHAR(256) NOT NULL DEFAULT ''," +
        //                                                    "State VARCHAR(256) NOT NULL DEFAULT ''," +
        //                                                    "Zip VARCHAR(256) NOT NULL DEFAULT ''";
        //        db.Database.ExecuteSqlCommand(query);
        //        b = true;
        //    }
        //    catch (Exception e)
        //    {
        //        b = false;
        //        this.ErrorMessage = "UpdateFrom7To8 Failed: " + e.Message;
        //    }
        //    return b;
        //}


        //public bool UpdateFrom8To9(DataContext db)
        //{
        //    //Adds Terms & Conditions to Companies, Kiosks, Cards
        //    bool b = false;
        //    try
        //    {
        //        string query = "DROP TABLE MercuryPay_Company";
        //        db.Database.ExecuteSqlCommand(query);
        //        query = "CREATE TABLE MercuryPay_Kiosk ("
        //            + "MercuryPay_KioskId int IDENTITY(1,1) NOT NULL,"
        //            + "CompanyId INT NOT NULL DEFAULT 0,"
        //            + "KioskId INT NOT NULL DEFAULT 0,"
        //            + "DBAName NVARCHAR(100) NOT NULL,"
        //            + "Address NVARCHAR(100) NOT NULL,"
        //            + "CSZ NVARCHAR(100) NOT NULL,"
        //            + "Phone NVARCHAR(50) NOT NULL,"
        //            + "MerchantNumber NVARCHAR(50) NOT NULL,"
        //            + "WebServicePassword NVARCHAR(50) NOT NULL,"
        //            + " CONSTRAINT PK_MercuryPay_Kiosk PRIMARY KEY CLUSTERED (MercuryPay_KioskId ASC)"
        //            + " WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
        //        db.Database.ExecuteSqlCommand(query);
        //        b = true;
        //    }
        //    catch (Exception e)
        //    {
        //        b = false;
        //        this.ErrorMessage = "UpdateFrom7To8 Failed: " + e.Message;
        //    }
        //    return b;
        //}

    }
}
