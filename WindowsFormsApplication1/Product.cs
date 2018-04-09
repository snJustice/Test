using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
namespace WindowsFormsApplication1
{
    [Table("Product")]
    class Product
    {
        public int ProductID { set; get; }
        public string ProductName { set; get; }
    }
}
