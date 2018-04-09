using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 条烟纠错系统.Logic;
using 条烟纠错系统.Models;

namespace 条烟纠错系统
{
    public partial class SearchForm : Form
    {
        DataBaseProgress localDatabase;
        DataTable packageDetailsShowTable;//显示包细节的表
        DataTable packageShowTable;//显示包的表

        public int currentPackageNumber;
        public int currentPackageCount;

        
        public SearchForm()
        {
            InitializeComponent();
            
            
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            localDatabase = new DataBaseProgress();
            packageDetailsShowTable = new DataTable();
            packageShowTable = new DataTable();
            InitNeededTables();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.PerformSafely(() => packageShowTable.Clear());
            if(string.IsNullOrEmpty(txtPickDate.Text))
            {
                MessageBox.Show("必须输入分拣日期");
                return;
            }
            if(string.IsNullOrEmpty( txtCustomerID.Text) && string.IsNullOrEmpty(txtPickPackage.Text))
            {
                MessageBox.Show("请输入订单号或者客户号");
                return;
            }
            //根据订单号
            if (string.IsNullOrEmpty(txtCustomerID.Text) && !string.IsNullOrEmpty(txtPickPackage.Text))
            {
                var package = localDatabase.dataBase.Database.SqlQuery<PickPackage>("select * from [PickPackage] where [PickDate]='" +
                txtPickDate.Text + "' and [PackageID]=" + txtPickPackage.Text
                ).FirstOrDefault();
                InsertPackageTable(package);
                

            }
            //根据客户号，客户号优先
            if (!string.IsNullOrEmpty(txtCustomerID.Text) && txtCustomerID.Text != "0")
            {
                var packages = localDatabase.dataBase.Database.SqlQuery<PickPackage>("select * from [PickPackage] where [PickDate]='" +
                txtPickDate.Text + "' and [CustomerID]=" + txtCustomerID.Text
                );
                InsertPackageTable(packages.ToList());

            }

            
        }

        private void InsertPackageTable(PickPackage _temp)
        {
            ShowPackageTable(_temp);
        }

        private void InsertPackageTable(List<PickPackage> _temp)
        {
            foreach(PickPackage tt in _temp)
            {
                ShowPackageTable(tt);
            }
        }
        //插入一条数据到package表中，并且显示
        private void ShowPackageTable(PickPackage _temp)
        {
            var row = packageShowTable.NewRow();
            row["包号"] = _temp.PackID;
            row["客户编码"] = _temp.CustomerID;
            row["客户名称"] = _temp.CustomerName;
            //row["结果"] = _temp.Details;
            row["日期"] = _temp.PickDate;
            if(_temp.Result == (int)ReadResult.OK)
            {
                row["结果信息"] = "正常";
            }
            else if(_temp.Result == 0)
            {
                row["结果信息"] = "还未读取";
            }
            else
            {
               
                row["结果信息"] = "异常";
            }
            this.PerformSafely(() => packageShowTable.Rows.Add(row));
        }

        //初始化表,显示包的表
        private void InitNeededTables()
        {
            packageShowTable.Columns.Add("包号", typeof(string));
            packageShowTable.Columns.Add("客户编码", typeof(string));
            packageShowTable.Columns.Add("客户名称", typeof(string));
            
            packageShowTable.Columns.Add("日期", typeof(string));
            packageShowTable.Columns.Add("结果信息", typeof(string));
            dgCurrentPackage.DataSource = packageShowTable;

            packageDetailsShowTable.Columns.Add("商品名称", typeof(string));
            packageDetailsShowTable.Columns.Add("商品ID", typeof(string));
            packageDetailsShowTable.Columns.Add("数量", typeof(string));
            packageDetailsShowTable.Columns.Add("已读数量", typeof(string));
            packageDetailsShowTable.Columns.Add("结果信息", typeof(string));
            packageDetailsShowTable.Columns["已读数量"].DefaultValue = "0";
            
            dgCurrentPackageDetails.DataSource = packageDetailsShowTable;
        }


        private void dgCurrentPackage_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine(e.RowIndex);
            packageDetailsShowTable.Clear();
            var cur = dgCurrentPackage.Rows[e.RowIndex];
            if ( cur ==null || cur.Cells[0].Value.ToString() == "")
            {
                return;
            }
            
            string command = "select * from[PickPackageDetail] where[PickPackageID] = '" +
                 cur.Cells["包号"].Value.ToString() + cur.Cells["客户编码"].Value.ToString() + "'";

            var packagesdetails = localDatabase.dataBase.Database.SqlQuery<PickPackageDetail>(command);
            var x = packagesdetails.ToList();
            if (packagesdetails !=null)
            {
                
                foreach (PickPackageDetail pds in packagesdetails)
                {
                    ShowPackageDetailTable(pds);
                }
            }
        }

        //插入一条数据到packageDetail表中，并且显示
        private void ShowPackageDetailTable(PickPackageDetail _temp)
        {
            
            var row = packageDetailsShowTable.NewRow();
            row["商品名称"] = localDatabase.dataBase.Database.SqlQuery<string>("select [ProductName] from [Product] where [ProductID]=" + _temp.ProductID).FirstOrDefault();
            row["商品ID"] = _temp.ProductID;
            row["数量"] = _temp.Quantity;
            row["已读数量"] = _temp.Readed;

            if (_temp.Result == (int)ReadResult.OK)
            {
                row["结果信息"] = "正常";
            }
            else if (_temp.Result == 0)
            {
                row["结果信息"] = "还未读取";
            }
            else
            {

                row["结果信息"] = "异常";
            }

            this.PerformSafely(() => packageDetailsShowTable.Rows.Add(row));
        }

        private void btnNextCustomer_Click(object sender, EventArgs e)
        {

        }

        private void btnLastCustomer_Click(object sender, EventArgs e)
        {

        }
    }
}
