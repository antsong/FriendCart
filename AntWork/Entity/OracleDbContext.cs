using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class OracleDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //注意：这里 schema 全部大写
            modelBuilder.HasDefaultSchema("MES56X");
        }

        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<FileDocement> FileDocements { get; set; }
        public DbSet<HazardNotificationAttachment> HazardNotificationAttachments { get; set; }

    }
}
