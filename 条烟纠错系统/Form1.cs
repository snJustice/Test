using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 条烟纠错系统.Applications;
using 条烟纠错系统.Models;
using 条烟纠错系统.Logic;
using 条烟纠错系统.DSocket;
using System.Threading.Tasks;
using System.Diagnostics;
using 条烟纠错系统.TableHandles;

namespace 条烟纠错系统
{
    public partial class Form1 : Form
    {
        TCPCommunication tcp;
        object lockkk ;
        DataBaseProgress xx ;
        List<PackageShow> paskages ;     
        DataTable packageShowTable ;//显示包细节的表

        IGetOrder orderInterface;//订单接口
        TableHandleBase orderHandle;//订单处理



        public Form1()
        {
            InitializeComponent();

            lockkk = new object();
            xx = new DataBaseProgress();
            paskages = new List<PackageShow>();
            packageShowTable = new DataTable();

            orderInterface = new SqlServerGetOrder();
            orderHandle = new SqlTableHandle(orderInterface);
            orderHandle.finishedLoading += FinishingLoadData;


        }

        private async void btnLoadingOrder_Click(object sender, EventArgs e)
        {
            paskages = new List<PackageShow>();
            

            if (!splashScreenManager1.IsSplashFormVisible)
                splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormDescription("正在下载订单...");


            
            
            orderHandle.Connect();
            orderHandle.ClearCurrentLocalDatabaseTable();
            orderHandle.GetOrder();
            
            await orderHandle.HandleData();

        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            orderHandle.GetAllPackages();
            ShowPakageDetail();
            SetCustomersTxt();


        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            currenTime.Text = DateTime.Now.ToString();
            InitNeededTables();
            tcp = new TCPCommunication("127.0.0.1", PackageOk);
            await tcp.ConnectAsync();
            


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tcp != null && tcp.IsConnected)
            {
                tcp.Close();
            }
        }

        //下载完成后做的事情
        public void FinishingLoadData()
        {
            orderHandle.GetAllPackages();
            
            if (splashScreenManager1.IsSplashFormVisible)
            {
                splashScreenManager1.CloseWaitForm();
            }
        }

        //如果表中增加了产品，插入到数据表中
        public async void InsertNewProduct()
        {
            var remoteProductList = await xx.GetProductInRemote();

            var productListInDB = await xx.GetProduct();
            List<string> needProductID = new List<string>();

            foreach (Product move in productListInDB)
            {
                needProductID.Add(move.ProductID.Trim());
            }

            var needProduct = remoteProductList.FindAll(n => !needProductID.Contains(n.ProductID.Trim()));
            await xx.InsertProductRange(needProduct);
        }

        //分析扫到的条烟是否在包中
        private void PackageOk(DataPackageInfo _code)
        {
            var code = _code.CodeData;
            if (code == "")
                return;


            var package = orderHandle.GetCurrentCustomerPackageShow();
            lock (lockkk)
            {
                package.ReadedCount++;
            }
            int index = 0;


            foreach (PickPackageDetail pd in package.PackageDetails)
            {
                if (code == pd.ProductID.Trim())
                {
                    this.PerformSafely(() => {
                        packageShowTable.Rows[index]["readed"] = (Convert.ToInt32(packageShowTable.Rows[index]["readed"].ToString()) + 1).ToString();


                    });

                    pd.Readed++;
                    break;
                }


                if (package.HasAbmormal && package.AbnormalreadCount < 3)
                {
                    lock (lockkk)
                    {
                        package.AbnormalreadCount++;
                    }
                }

            }



            if (package.ReadedCount >= package.CigaretteCount)
            {
                this.PerformSafely(() => {
                    listBoxMessageShow.Items.Clear();
                });

                Task.Factory.StartNew(() => {
                    foreach (PickPackageDetail temp in package.PackageDetails)
                    {
                        int result = temp.Quantity - temp.Readed;
                        
                        ResultAnalysis(result, package.AbnormalreadCount, package.CigaretteCount, temp);

                    }
                    var pack = orderHandle.localDatabase.dataBase.Packages.FirstOrDefault(c => c.PickPackageID == package.PickPackageID);
                    foreach (PickPackageDetail temp in package.PackageDetails)
                    {
                       if(temp.Result == (int)ReadResult.LESS || temp.Result == (int)ReadResult.MORE)
                        {
                            
                            pack.Result = (int)ReadResult.LESS;
                            orderHandle.localDatabase.dataBase.Entry(pack).State = System.Data.Entity.EntityState.Modified;
                            orderHandle.localDatabase.dataBase.SaveChanges();
                            break;
                        }
                        pack.Result = (int)ReadResult.OK;
                        orderHandle.localDatabase.dataBase.Entry(pack).State = System.Data.Entity.EntityState.Modified;
                        orderHandle.localDatabase.dataBase.SaveChanges();
                    }

                    OkNGFlag(package.CigaretteCount - package.CigaretteOKCount, package.AbnormalreadCount,package);


                });

                PackageMoveNext();
            }
        }

        //分析一个包中条烟数量情况
        private void ResultAnalysis(int _result, int _abnormalCount, int _allCount, PickPackageDetail _pd )
        {
            var pdresult = orderHandle.localDatabase.dataBase.PackageDetails.FirstOrDefault(c => c.PickPackageDetailID == _pd.PickPackageDetailID);
            var _ps = orderHandle.localDatabase.dataBase.Packages.FirstOrDefault(c=>c.PickPackageID == _pd.PickPackageID);
            PickException exception = null;
            string message = "";
            if (_result == 0)
            {
                
                pdresult.Details = "正常";
                pdresult.Readed = _pd.Readed;
                pdresult.Result = (int)ReadResult.OK;
            }

            else if (_result > 0)
            {

                message = "缺少 " + _pd.Product.ProductName.Trim() + " , " + _pd.ProductID.Trim() + "," + _result.ToString() + "条";
                
                pdresult.Details = "缺少 " + _result.ToString() + "条";
                pdresult.Readed = _pd.Readed;
                pdresult.Result = (int)ReadResult.LESS;
                exception = new PickException();

                exception.CustomerID = _ps.CustomerID;
                exception.CustomerName = _ps.CustomerName;
                exception.Detail = "少烟";
                exception.PackID = _ps.PackID;
                exception.PickDate = _ps.PickDate;
                exception.ProductName = _pd.Product.ProductName;
                exception.LineID = _ps.LineID;
                exception.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


            }
            else
            {
                
                message = ("多 " + _pd.Product.ProductName.Trim() + " , " + _pd.ProductID.Trim() + "," + (-_result).ToString() + "条");
                
                pdresult.Details = "多 " + (-_result).ToString() + "条";
                pdresult.Readed = _pd.Readed;
                pdresult.Result =(int)ReadResult.MORE;

                exception = new PickException();

                exception.CustomerID = _ps.CustomerID;
                exception.CustomerName = _ps.CustomerName;
                exception.Detail = "多烟";
                exception.PackID = _ps.PackID;
                exception.PickDate = _ps.PickDate;
                exception.ProductName = _pd.Product.ProductName;
                exception.LineID = _ps.LineID;
                exception.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            try
            {
                orderHandle.localDatabase.dataBase.Entry(pdresult).State = System.Data.Entity.EntityState.Modified;
                if(exception!=null)
                {
                    orderHandle.localDatabase.dataBase.PickExceptions.Add(exception);
                }

                orderHandle.localDatabase.dataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
            }


            this.PerformSafely(() => {
                listBoxMessageShow.Items.Add(new CCWin.SkinControl.SkinListBoxItem { Text = message});
            });


        }
        //检测一个包是否合格
        private void OkNGFlag(int _result, int _abnormalCount,PackageShow _ps)
        {
            var pack = orderHandle.localDatabase.dataBase.Packages.FirstOrDefault(c => c.PickPackageID == _ps.PickPackageID);
            var packagedetails = orderHandle.localDatabase.dataBase.PackageDetails.ToList().FindAll(c => c.PickPackageID == _ps.PickPackageID);
            
            foreach (PickPackageDetail temp in packagedetails)
            {

            }
            if (_result == 0 || _abnormalCount - _result == 0)
            {
                Debug.WriteLine("ok");
                pack.Details = "正常";
            }
            else
            {
                Debug.WriteLine("bad");
                pack.Details = "异常";  
            }
            //保存数据
            try
            {
                orderHandle.localDatabase.dataBase.Entry(pack).State = System.Data.Entity.EntityState.Modified;
                orderHandle.localDatabase.dataBase.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
            }


        }

        

        //包向后移动
        private void PackageMoveNext()
        {
            orderHandle.PackageShowMoveNext();
            ShowPakageDetail();
            SetCustomersTxt();
        }
        //用index显示包数据
        private void ShowPakageDetail()
        {
            this.PerformSafely(() => packageShowTable.Clear());
            PackageShow temp = orderHandle.GetCurrentCustomerPackageShow();

            foreach (PickPackageDetail _temp in temp.PackageDetails)
            {
                InsertOnePackageDetail(_temp);
            }
        }

        //向显示表中插入一条烟
        private void InsertOnePackageDetail(PickPackageDetail _packagedetail)
        {
            DataRow tempp = packageShowTable.NewRow();
            tempp["productName"] = _packagedetail.Product.ProductName;
            tempp["productID"] = _packagedetail.ProductID;
            tempp["quantity"] = _packagedetail.Quantity;

            tempp["readed"] = "0";

            this.PerformSafely(() => packageShowTable.Rows.Add(tempp));
            //packageShowTable.Rows.Add(tempp);
        }
        //显示当前，上一个，下一个客户信息
        private void SetCustomersTxt()
        {
            this.PerformSafely(() => {
                var last = orderHandle.GetLastCustomerPackageShow();
                var current = orderHandle.GetCurrentCustomerPackageShow();
                var next = orderHandle.GetNextCustomerPackageShow();

                txtLastChildOrder.Text = last?.CustomerName;
                txtLastOrderID.Text = last?.CustomerID;
                txtLastOrderQuantity.Text = last?.CigaretteCount.ToString();

                txtCurrentChildOrder.Text = current.CustomerName;
                txtCurrentOrderID.Text = current.CustomerID;
                txtCurrentOrderQuantity.Text = current.CigaretteCount.ToString();

                txtNextChildOrder.Text = next?.CustomerName;
                txtNextOrderID.Text = next?.CustomerID;
                txtNextOrderQuantity.Text = next?.CigaretteCount.ToString();
            });

        }

        //初始化表,显示包的表
        private void InitNeededTables()
        {

            packageShowTable.Columns.Add("productName", typeof(string));
            packageShowTable.Columns.Add("productID", typeof(string));
            packageShowTable.Columns.Add("quantity", typeof(string));
            packageShowTable.Columns.Add("readed", typeof(string));
            packageShowTable.Columns["readed"].DefaultValue = "0";
            skinDataGridView2.DataSource = packageShowTable;
        }

        //显示查询界面
        private void btnMessageSearch_Click(object sender, EventArgs e)
        {
            var SearchForm = new SearchForm();
            SearchForm.Show();
        }

        //code查看
        private void skinButton3_Click(object sender, EventArgs e)
        {


            ExceptionForm exform = new ExceptionForm();
            exform.Show();

            

        }

        //定位包
        private void btnLocatePackage_Click(object sender, EventArgs e)
        {
            orderHandle.LocatePackage(Convert.ToInt32(txtPickPackage.Text));
            ShowPakageDetail();
            SetCustomersTxt();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currenTime.Text = DateTime.Now.ToString();
        }

        private async void btnSystemSetting_Click(object sender, EventArgs e)
        {
            

            
        }
    }
}
