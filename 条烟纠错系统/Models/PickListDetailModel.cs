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
    [Table("PickListDetail")]
    public class PickListDetail
    {
        [Key]
        public string CustomerID { set; get; }

        public string CustomerName { set; get; }

        public string LineID { set; get; }

        public int PickListID { set; get; }

        public int CustomerNumberID { set; get; }

        public string PickDate { set; get; }

        

        
    }
}
