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
    [Table("PickException")]
    public class PickException
    {
        [Key]
        public int ID { set; get; }

        public string ProductName { set; get; }
        public string CustomerID { set; get; }
        public string CustomerName { set; get; }

        public string LineID { set; get; }
        public int PackID { set; get; }
        public string Detail { set; get; }
        public string PickDate { set; get; }
        public string Time { set; get; }
    }
}
