using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using 条烟纠错系统.Models;

namespace 条烟纠错系统
{
    public class PackageShow
    {
        public String CustomerName { set; get; }
        public string CustomerID { set; get; }
        public int PackageID { set; get; }
        public int CigaretteCount
        {
            get
            {
                return (from a in PackageDetails select a.Quantity).Sum();
            }
        }



        public int CigaretteOKCount
        {
            get
            {
                return (from a in PackageDetails select a.Readed).Sum();
            }
        }

        public int ReadedCount { set; get; }

        public int AbnormalreadCount { set; get; }

        public bool HasAbmormal { set; get; }

        public string PickPackageID { set; get; }

        public int Result { set; get; }
        public List<PickPackageDetail> PackageDetails { set; get; }
    }
}
