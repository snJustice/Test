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
    [Table("AbnormalProduct")]
    public class AbnormalProduct

    {
        [Key]
        public int AbnormalProductID { set; get; }

        public string ProductID { set; get; }
        
        [ForeignKey("ProductID")]
        public Product Product { set; get; }
    }
}
