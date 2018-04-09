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
    [Table("PickList")]
    public class PickList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PickListID { set; get; }

        public string PickDate { get; set; }//日期

        

        
        
    }
}
