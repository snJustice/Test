using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

using 条烟纠错系统.Models;
using System.ComponentModel.DataAnnotations;

namespace 条烟纠错系统.Models
{
    [Table("PickPackage")]
    public class PickPackage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PickPackageID { set; get; }
        
        public string Details { set; get; }

        public string CustomerID { set; get; }

        public int PackID { set; get; }

        //新增
        public string LineID { get; set; }

        public string CustomerName { set; get; }

        public string PickDate { set; get; }
        public int Result { set; get; }

    }
}
