using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using 条烟纠错系统.Models;

namespace 条烟纠错系统.Applications
{
    public class PSContent : DbContext
    {
        public PSContent()
        {

        }
        public DbSet<AbnormalProduct> AbnormalProducts { get; set; }

        public DbSet<PickPackageDetail> PackageDetails { get; set; }

        public DbSet<PickPackage> Packages { get; set; }

        public DbSet<PickListDetail> PickListDetails { get; set; }

        public DbSet<PickList> PickLists { get; set; }

        public  DbSet< Product> Products { get; set; }
        
        public  DbSet<User> Users { get; set; }

        public DbSet<PickException> PickExceptions { set; get; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
        }
    }

    /*
            PSContent xx = new PSContent();
            xx.Configuration.LazyLoadingEnabled = false;

            


            var rows = xx.AbnormalProducts.Include("Product").ToList();
            //var rowss = xx.AbnormalProducts.Include("Product").Select(c=>c);
           
            var dsa = xx.AbnormalProducts.Include("Product").FirstOrDefault();
            Console.WriteLine(rows.FirstOrDefault().Product.ProductName);
            foreach (var row in rows)
            {
                var temp = xx.Products.FirstOrDefault(c=>row.ProductID == c.ProductID);
            }*/


}
