using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace 条烟纠错系统.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string PassWord { set; get; }
    }
}
