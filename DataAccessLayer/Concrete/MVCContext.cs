using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class MVCContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleMethod>().HasKey(x => new { x.RoleID, x.MethodNameID });
            modelBuilder.Entity<RoleMethod>().HasRequired(x => x.Role).WithMany(x => x.RoleMethods).HasForeignKey(x => x.RoleID);
            modelBuilder.Entity<RoleMethod>().HasRequired(x => x.MethodName).WithMany(x => x.RoleMethods).HasForeignKey(x => x.MethodNameID);

            modelBuilder.Entity<RoleControllerName>().HasKey(x => new { x.RoleID, x.ControllerNameID });
            modelBuilder.Entity<RoleControllerName>().HasRequired(x => x.Role).WithMany(x => x.RoleControllerNames).HasForeignKey(x => x.RoleID);
            modelBuilder.Entity<RoleControllerName>().HasRequired(x => x.ControllerName).WithMany(x => x.RoleControllerNames).HasForeignKey(x => x.ControllerNameID);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Heading> Headings { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<TestTable> TestTables { get; set; }
        public DbSet<ImageFile> ImageFiles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SkillInfo> SkillInfos { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MethodName> MethodNames { get; set; }
        public DbSet<RoleMethod> RoleMethods { get; set; }
        public DbSet<ControllerName> ControllerNames { get; set; }
        public DbSet<RoleControllerName> RoleControllerNames { get; set; }

    }
}
