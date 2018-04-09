using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace 条烟纠错系统.Models

{
    [Table("Product")]
    public class Product
    {
        [Key]
        public string ProductID { set; get; }
        public string ProductName { set; get; }
        
    }
}
