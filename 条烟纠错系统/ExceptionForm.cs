using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 条烟纠错系统.Applications;
using 条烟纠错系统.Applicatoins;
using 条烟纠错系统.Models;
namespace 条烟纠错系统
{
    public partial class ExceptionForm : Form
    {
        PSContent dataBase;
        DataTable pickExceptionTable;
        public ExceptionForm()
        {
            InitializeComponent();
            dataBase = new PSContent();
            pickExceptionTable = new DataTable();
            InitExceptionTable();
            dgExceptionTable.DataSource = pickExceptionTable;
        }

        


        private void btnSearch_Click(object sender, EventArgs e)
        {
            pickExceptionTable.Clear();
            var pickExceptions = dataBase.PickExceptions.ToList().FindAll(c=>c.PickDate == txtPickDate.Text);
            foreach(PickException temp in pickExceptions)
            {
                ShowPackageDetailTable(temp);
            }

        }

        private void InitExceptionTable()
        {
            pickExceptionTable.Columns.Add("发生时间", typeof(string));
            pickExceptionTable.Columns.Add("日期", typeof(string));
            pickExceptionTable.Columns.Add("线路名称", typeof(string));
            pickExceptionTable.Columns.Add("客户编码", typeof(string));
            pickExceptionTable.Columns.Add("客户名称", typeof(string));

            pickExceptionTable.Columns.Add("包号", typeof(string));
            pickExceptionTable.Columns.Add("商品名称", typeof(string));
            pickExceptionTable.Columns.Add("异常原因", typeof(string));
        }

        //插入一条数据到packageDetail表中，并且显示
        private void ShowPackageDetailTable(PickException _temp)
        {
           // dgExceptionTable.Rows.Add();
            
            
            var row = pickExceptionTable.NewRow();
            row["发生时间"] = _temp.Time.ToString();
            row["日期"] = _temp.PickDate;
            row["线路名称"] = _temp.LineID.ToString();
            row["客户编码"] = _temp.CustomerID;
            row["客户名称"] = _temp.CustomerName;

            row["包号"] = _temp.PackID.ToString();
            row["商品名称"] = _temp.ProductName;
            row["异常原因"] = _temp.Detail;
           

            

            this.PerformSafely(() => pickExceptionTable.Rows.Add(row));
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            dataBase.PickExceptions.RemoveRange(dataBase.PickExceptions);
            dataBase.SaveChanges();
        }
    }
}
