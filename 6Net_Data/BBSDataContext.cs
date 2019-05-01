using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MySql.Data.EntityFramework;
using Net_Data.Models;

namespace Net_Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BBSDataContext : DbContext
    {
        public DbSet<AccessGroup> AccessGroups { get; set; }
        public DbSet<BBSConfig> BBSConfigs { get; set; }
        public DbSet<CallLog> CallLogs { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<GFileArea> GFileAreas { get; set; }
        public DbSet<GFileDetail> GFileDetails { get; set; }
        public DbSet<GFileAreaAccessGroup> GFileAreaAccessGroups { get; set; }
        public DbSet<Graffiti> Graffiti { get; set; }
        public DbSet<MessageBaseArea> MessageBaseAreas { get; set; }
        public DbSet<MessageBase> MessageBases { get; set; }
        public DbSet<MessageBaseAccessGroup> MessageBaseAccessGroups { get; set; }
        public DbSet<MessageThread> MessageThreads { get; set; }
        public DbSet<MessageHeader> MessageHeaders { get; set; }
        public DbSet<MessageBody> MessageBodies { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<PFileArea> PFileAreas { get; set; }
        public DbSet<PFileDetail> PFileDetails { get; set; }
        public DbSet<PFileAreaAccessGroup> PFileAreaAccessGroups { get; set; }
        public DbSet<UDBaseArea> UDBaseAreas { get; set; }
        public DbSet<UDBase> UDBases { get; set; }
        public DbSet<UDFile> UDFiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccessGroup> UserAccessGroups { get; set; }



        public BBSDataContext() : base()
        {
        }

        public BBSDataContext(DbConnection dbConnection, bool contextOwnsConnection) : base (dbConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
