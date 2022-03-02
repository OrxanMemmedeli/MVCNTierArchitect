using ShowcaseAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShowcaseAPI.Models.Context
{
    public class ShowcaseContext : DbContext
    {
        public DbSet<About> Abouts { get; set; }
        public DbSet<Development> Developments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SosialPage> SosialPages { get; set; }
        public DbSet<Tool> Tools { get; set; }

    }
}