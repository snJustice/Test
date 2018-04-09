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
    [Table("PickPackageDetail")]
    public class PickPackageDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PickPackageDetailID { set; get; }
        
        public int Quantity { get; set; }//产品数量
        
        public int Readed { set; get; }

        
        public string PickPackageID { set; get; }
        

        public string ProductID { get; set; }//产品ID

        public string Details { set; get; }
        public int Result { set; get; }

        public string CustomerID { set; get; }

        public int PackID { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }
        


    }
}
