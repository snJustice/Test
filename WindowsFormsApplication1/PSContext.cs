﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace WindowsFormsApplication1
{
    class PSContext : DbContext
    {
        public DbSet<Product> product { set; get; }
    }
}
