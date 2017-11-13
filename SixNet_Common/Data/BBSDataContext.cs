using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SixNet_BBS.Data.Objects;


namespace SixNet_BBS.Data
{
    public class BBSDataContext : DbContext
    {
        static BBSDataContext()
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            var type2 = typeof(System.Data.Entity.SqlServerCompact.SqlCeProviderServices);
            Database.SetInitializer(new DatabaseInitializer());
        }

         
        public static BBSDataContext GetContext()
        {
            BBSDataContext bbs = new BBSDataContext();
            //bbs.Database.Connection.ConnectionString = Properties.Settings.Default.BBSDataContext;
            return bbs;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<AccessGroup> AccessGroups { get; set; }
        public DbSet<SysConfig> SysConfigs { get; set; }
        public DbSet<BBSConfig> BBSConfigs { get; set; }

        public DbSet<GFileArea> GFileAreas { get; set; }
        public DbSet<GFileDetail> GFileDetails { get; set; }

        public DbSet<PFileArea> PFileAreas { get; set; }
        public DbSet<PFileDetail> PFileDetails { get; set; }

        public DbSet<MessageBaseArea> MessageBaseAreas { get; set; }
        public DbSet<MessageBase> MessageBases { get; set; }
        public DbSet<MessageThread> MessageThreads { get; set; }
        public DbSet<MessageHeader> MessageHeaders { get; set; }
        public DbSet<MessageBody> MessageBodies { get; set; }

        public DbSet<UserRead> UserReads { get; set; } //message read flags per user

        public DbSet<UserMessageBase> UserMessageBases { get; set; }

        public DbSet<UDBase> UDBases { get; set; }
        public DbSet<FileDetail> FileDetails { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<CallLog> CallLogs { get; set; }

        public DbSet<UserDefinedField> UserDefinedFields { get; set; }

        public DbSet<News_Item> News { get; set; }
        public DbSet<Graffiti> Graffitis { get; set; }


    }



}
