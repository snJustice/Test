using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NPOI.Extend;
using NPOI.SS.UserModel;
using System.Threading.Tasks;
using System.Threading;
using 条烟纠错系统.Applications;
using 条烟纠错系统.Models;
using 条烟纠错系统.Logic;
using System.Collections;
using System.Diagnostics;

namespace 条烟纠错系统
{

    public delegate void Finished();


    public class TableHandle
    {
        public event Finished finishedLoading;
        #region 数据库
        public DataBaseProgress localDatabase = new DataBaseProgress();
        //PSContent database = new PSContent();
        DataProgress remoteDatabase = new DataProgress();


        #endregion

        public List<PackageShow> packageshows = new List<PackageShow>();
        private int currentPackageIndex = 0;
        private int lastPackageIndex = -1;
        private int nextPackageIndex = 1;

        #region 用来判断是否分割完成
        Semaphore s = new Semaphore(2, 2);
        object obj = new object();
        int finishTable = 0;
        int alltable = 0;
        #endregion


        #region 获得包
        public PackageShow GetPackageWithIndex(int _index)
        {
            return packageshows[_index];
        }

        public PackageShow GetLastCustomerPackageShow()
        {
            if (lastPackageIndex < 0)
            {
                return null;
            }
            return GetPackageWithIndex(lastPackageIndex);
        }
        public PackageShow GetCurrentCustomerPackageShow()
        {
            return GetPackageWithIndex(currentPackageIndex);
        }
        public PackageShow GetNextCustomerPackageShow()
        {
            if (nextPackageIndex >= packageshows.Count)
            {
                return null;
            }
            return GetPackageWithIndex(nextPackageIndex);
        }

        public void PackageShowMoveNext()
        {
            lastPackageIndex++;
            currentPackageIndex++;
            nextPackageIndex++;
        }
        #endregion


        public DataTable OrderTable = new DataTable();

        public  void LoadingData()
        {
            
             localDatabase.ClearAllPickData();
            localDatabase.dataBase.AbnormalProducts.ToList();
            Debug.WriteLine(localDatabase.dataBase.Packages.Count());
            Debug.WriteLine(localDatabase.dataBase.PackageDetails.Count());
            Debug.WriteLine(localDatabase.dataBase.PickListDetails.Count());
            Debug.WriteLine(localDatabase.dataBase.PickLists.Count());

            
            remoteDatabase.ConnectDataBase();
            OrderTable = remoteDatabase.ReadPickData();
            remoteDatabase.DisconnectDataBase();
            //GetOrder();
            //var kkkk = OrderTable.DefaultView.ToTable(false, new string[] { "pickDate" });

        }
        public async Task<bool> HandlerData()
        {


            int finishTable = 0;
            foreach(DataColumn temp in  OrderTable.Columns)
            {
                Debug.WriteLine(temp.DataType);
            }

            var pickdate = (from x in OrderTable.AsEnumerable() select x.Field<string>("pickDate")).ToList<string>();
            pickdate = pickdate.Distinct().ToList();
            var alltable = pickdate.Count;
            List<DataRow[]> dateTables = new List<DataRow[]>();
            
            foreach (string tempdate in pickdate)
            {
                var rowDateTable = OrderTable.Select("pickDate='" + tempdate + "'");

                localDatabase.dataBase.PickLists.Add(new PickList
                {
                    PickListID = alltable--,
                    PickDate = tempdate,
                });
                dateTables.Add(rowDateTable);



            }
            localDatabase.dataBase.SaveChanges();

            //插入分拣列表细节
            var customerIDs = (from x in OrderTable.AsEnumerable()
                               select x.Field<string>("customerID")).ToList<string>();
            customerIDs = customerIDs.Distinct().ToList();
            List<PickListDetail> temppl = new List<PickListDetail>();
            var pickList = localDatabase.dataBase.PickLists.ToList();
            Debug.WriteLine(pickList[pickList.Count()-1]);
            int customerCount = 1;
            foreach (var customerID in customerIDs)
            {
                var one = (from x in OrderTable.AsEnumerable()
                           where x["customerID"].ToString() == customerID
                           select x

                           ).FirstOrDefault();

                //var id = pickList.FirstOrDefault(c => c.PickDate.Trim() == one["pickDate"].ToString());
                var id = pickList.Where(c => c.PickDate.Trim() == one["pickDate"].ToString()).FirstOrDefault();
                temppl.Add(new PickListDetail
                {
                    CustomerID = customerID,
                    CustomerName = one["customerName"].ToString(),
                    LineID = one["lineID"].ToString(),
                    CustomerNumberID = customerCount++,
                    PickListID = id.PickListID//await localDatabase.GetIDByPickDate(one["pickDate"].ToString())

                });

            }
            localDatabase.dataBase.PickListDetails.AddRange(temppl);

            //插入包


            foreach (DataRow[] temp in dateTables)
            {
                var packIDs = (from x in temp
                               select x.Field<double>("packID")).ToList<double>();
                packIDs = packIDs.Distinct().ToList();

                List<PickPackage> pp = new List<PickPackage>();
                foreach (var packID in packIDs)
                {
                    var one = (from x in temp
                               where Convert.ToInt32(x["packID"].ToString()) == packID
                               select x

                               ).FirstOrDefault();

                    pp.Add(new PickPackage
                    {
                        PickPackageID = one["packID"].ToString().Trim() + one["customerID"].ToString().Trim(),
                        PackID = Convert.ToInt32(one["packID"].ToString()),
                        CustomerID = one["customerID"].ToString(),
                        Details = "",
                        CustomerName = one["customerName"].ToString(),
                        LineID = one["lineID"].ToString(),
                        PickDate = one["pickDate"].ToString()

                    });

                }
                localDatabase.dataBase.Packages.AddRange(pp);// InsertPickPackageRange(pp);

            }
            //插入包细节的数据
            int count = 1;
            List<PickPackageDetail> ppds = new List<PickPackageDetail>();

            foreach (DataRow row in OrderTable.Rows)
            {
                ppds.Add(new PickPackageDetail
                {

                    PickPackageDetailID = count++,
                    PickPackageID = row["packID"].ToString().Trim() + row["customerID"].ToString().Trim(),


                    ProductID = row["productID"].ToString(),
                    Quantity = Convert.ToInt32(row["quantity"].ToString()),
                    Readed = 0,
                    Details = ""

                });
            }

            localDatabase.dataBase.PackageDetails.AddRange(ppds); //InsertPickPackageDetailRange(ppds);
            localDatabase.dataBase.SaveChanges();
            finishedLoading?.Invoke();
            return true;

        }

        /*
        public async Task<bool> HandlerData()
        {


            finishTable = 0;


            var pickdate = (from x in OrderTable.AsEnumerable() select x.Field<string>("pickDate")).ToList<string>();
            pickdate = pickdate.Distinct().ToList();
            alltable = pickdate.Count;
            List<DataRow[]> dateTables = new List<DataRow[]>();

            foreach (string tempdate in pickdate)
            {
                var rowDateTable = OrderTable.Select("pickDate='" + tempdate + "'");

                await localDatabase.InsertPickList(new PickList {
                    PickListID = alltable--,
                    PickDate = tempdate,
                });
                dateTables.Add(rowDateTable);
            }

            //插入分拣列表细节
            var customerIDs = (from x in OrderTable.AsEnumerable()
                               select x.Field<string>("customerID")).ToList<string>();

            customerIDs = customerIDs.Distinct().ToList();

            List<PickListDetail> temppl = new List<PickListDetail>();
            foreach (var customerID in customerIDs)
            {
                var one = (from x in OrderTable.AsEnumerable()
                           where x["customerID"].ToString() == customerID
                           select x

                           ).FirstOrDefault();


                temppl.Add(new PickListDetail
                {
                    CustomerID = customerID,
                    CustomerName = one["customerName"].ToString(),
                    LineID = one["lineID"].ToString(),
                    PickListID = await localDatabase.GetIDByPickDate(one["pickDate"].ToString())
                });

            }
            await localDatabase.InsertPickListDetailRange(temppl);

            //插入包


            foreach (DataRow[] temp in dateTables)
            {
                var packIDs = (from x in temp
                               select x.Field<double>("packID")).ToList<double>();
                packIDs = packIDs.Distinct().ToList();

                List<PickPackage> pp = new List<PickPackage>();
                foreach (var packID in packIDs)
                {
                    var one = (from x in temp
                               where Convert.ToInt32(x["packID"].ToString()) == packID
                               select x

                               ).FirstOrDefault();

                    pp.Add(new PickPackage
                    {
                        PickPackageID = one["packID"].ToString().Trim() + one["customerID"].ToString().Trim(),
                        PackageID = Convert.ToInt32(one["packID"].ToString()),
                        CustomerID = one["customerID"].ToString(),
                        Details = "",
                        CustomerName = one["customerName"].ToString(),
                        LineID = one["lineID"].ToString(),
                        PickDate = one["pickDate"].ToString(),

                    });

                }
                await localDatabase.InsertPickPackageRange(pp);
            }
            //插入包细节的数据
            int count = 1;
            List<PickPackageDetail> ppds = new List<PickPackageDetail>();

            foreach (DataRow row in OrderTable.Rows)
            {
                ppds.Add(new PickPackageDetail {

                    PickPackageDetailID = count++,
                    PickPackageID = row["packID"].ToString().Trim() + row["customerID"].ToString().Trim(),


                    ProductID = row["productID"].ToString(),
                    Quantity = Convert.ToInt32(row["quantity"].ToString()),
                    Readed = 0

                });
            }

            await localDatabase.InsertPickPackageDetailRange(ppds);

            GetAllPackages();
            finishedLoading?.Invoke();
            return true;

        }
        */
        public void GetAllPackages()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            List<string> abnormalProductList = new List<string>();
            foreach (var temp in localDatabase.dataBase.AbnormalProducts)
            {
                abnormalProductList.Add(temp.ProductID);
            }


            var pds = localDatabase.dataBase.PackageDetails.Include("Product").ToList();
            var ps = localDatabase.dataBase.Packages.ToList();
            ps.Sort((left, right) => {
                int result = DateTime.Compare(Convert.ToDateTime(left.PickDate), Convert.ToDateTime(right.PickDate));
                if (result > 0)
                {
                    return -1;
                }
                else if (result == 0)
                {
                    return (left.PackID - right.PackID);
                }
                else
                    return 1;
            });//排序
            packageshows = new List<PackageShow>();
            
            foreach (PickPackage p in ps)
            {
                var pdstemp = pds.FindAll(n => n.PickPackageID == p.PickPackageID);
                List<PickPackageDetail> pdsTemp = new List<PickPackageDetail>();
                /*
                foreach(PickPackageDetail s in pdstemp)
                {
                    pdsTemp.Add(new PickPackageDetail {
                        Details = s.Details,
                        PickPackageDetailID = s.PickPackageDetailID,
                        PickPackageID = s.PickPackageID,
                        Product = new Product { ProductID = s.ProductID,ProductName = s.Product.ProductName},
                        ProductID = s.ProductID,
                        Quantity = s.Quantity,
                        Readed = s.Readed,
                        Result = 0
                        
                    });
                }*/
                packageshows.Add(new PackageShow
                {
                    AbnormalreadCount = 0,
                    HasAbmormal = pdstemp.FirstOrDefault(c => abnormalProductList.Contains(c.ProductID)) != null ? true : false,
                    CustomerID = p.CustomerID,
                    CustomerName = p.CustomerName,
                    ReadedCount = 0,
                    PackageID = p.PackID,
                    PackageDetails = pdstemp,
                    PickPackageID = p.PickPackageID
                });

            }
            time.Stop();
            Debug.WriteLine(time.Elapsed.TotalMilliseconds);
        }

        public TableHandle()
        {
           //GetAllPackages();
            

        }

        public void GetOrder()
        {
            //加载数据
            var workBook = NPOIHelper.LoadWorkbook(@"D:\Work\项目\香烟\香烟\资料文件参考\订单分拣数据（慧联系统）\订单分拣数据（慧联系统）\分拣订单数据 - 副本.xls");
            var sheet = workBook.GetSheetAt(0);//获得第一个sheet
            var rows = sheet.GetRowEnumerator();//获得所有行

            OrderTable = new DataTable();
            rows.MoveNext();//移动到第一行，
            var rowfirst = (IRow)rows.Current;//获得当前行，
            for (int i = 0; i < rowfirst.LastCellNum; i++)//遍历，把第一行数据，当做行加入表中
            {
                OrderTable.Columns.Add(rowfirst.GetCell(i).ToString());
            }

            while (rows.MoveNext())//然后循环遍历
            {
                var row = (IRow)rows.Current;
                DataRow dr = OrderTable.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);//取得单元格
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                OrderTable.Rows.Add(dr);
            }


        }

        public void SaveChange()
        {
            localDatabase.SaveChange();
        }

        public void LocatePackage(int index)
        {

            currentPackageIndex = index;
            lastPackageIndex = index-1;
            nextPackageIndex = index+1;
    }

    }

    public class PackageShowCompare : IEqualityComparer<PackageShow>
    {
        bool IEqualityComparer<PackageShow>.Equals(PackageShow x, PackageShow y)
        {
            return x.PickPackageID == y.PickPackageID;
        }

        int IEqualityComparer<PackageShow>.GetHashCode(PackageShow obj)
        {
            return obj.PickPackageID.GetHashCode();
        }
    }
    /*
    class TableHandle
    {
            
        public event Finished finishedLoading;


        

        DataTable OrderTable;
        
        public PickOrder PickOrder { private set; get; }
        List<string> AbnormalProduct;
        #region 用来判断是否分割完成
        Semaphore s = new Semaphore(2, 2);
        object obj = new object();
        int finishTable = 0;
        int alltable = 0;
        #endregion

        //可用的包
        public List<PackageShow> packageshows = new List<PackageShow>();
        private int currentPackageIndex = 0;
        private int lastPackageIndex = -1;
        private int nextPackageIndex = 1;

        #region 获得包
        public PackageShow GetPackageWithIndex(int _index)
        {
            return packageshows[_index];
        }

        public PackageShow GetLastCustomerPackageShow()
        {
            if(lastPackageIndex<0)
            {
                return null;
            }
            return GetPackageWithIndex(lastPackageIndex);
        }
        public PackageShow GetCurrentCustomerPackageShow()
        {
            return GetPackageWithIndex(currentPackageIndex);
        }
        public PackageShow GetNextCustomerPackageShow()
        {
            if(nextPackageIndex>= packageshows.Count)
            {
                return null;
            }
            return GetPackageWithIndex(nextPackageIndex);
        }

        public void PackageShowMoveNext()
        {
            lastPackageIndex++;
            currentPackageIndex++;
            nextPackageIndex++;
        }
        #endregion

        


        

        

        public TableHandle()
        {
            PickOrder = new PickOrder();
            PickOrder.PickOrders = new List<PickList>();
            AbnormalProduct = new List<string>();
            AbnormalProduct.Add("610001");
        }

        public void GetOrder()
        {
            //加载数据
            var workBook = NPOIHelper.LoadWorkbook(@"D:\Work\项目\香烟\香烟\资料文件参考\订单分拣数据（慧联系统）\订单分拣数据（慧联系统）\分拣订单数据 - 副本.xls");
            var sheet = workBook.GetSheetAt(0);//获得第一个sheet
            var rows = sheet.GetRowEnumerator();//获得所有行

            OrderTable = new DataTable();
            rows.MoveNext();//移动到第一行，
            var rowfirst = (IRow)rows.Current;//获得当前行，
            for (int i = 0; i < rowfirst.LastCellNum; i++)//遍历，把第一行数据，当做行加入表中
            {
                OrderTable.Columns.Add(rowfirst.GetCell(i).ToString());
            }

            while (rows.MoveNext())//然后循环遍历
            {
                var row = (IRow)rows.Current;
                DataRow dr = OrderTable.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);//取得单元格
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                OrderTable.Rows.Add(dr);
            }


        }



        #region 太慢
        DataTable totalOrder;
        List<string> childOrdersCustomerID; // 客户ID的列表
        List<DataTable> ChildOrsersList;//子订单
        List<DataTable> ChildOrderListShow;//展示的子订单
        List<int> ChildOrderCigaretteNum;//子包的条烟数量

        

        public void OrderTableToClass()
        {
            //多个日期
            List<string> tempDateString = new List<string>();
            Console.WriteLine( OrderTable.Columns[0].ToString());
            DataTable aa = OrderTable.DefaultView.ToTable(false, new string[] { "pickDate"});
            foreach(DataRow temp in aa.Rows)
            {
                tempDateString.Add(temp[0].ToString().Trim());
            }
            tempDateString = tempDateString.Distinct().ToList();

            
           

            
            foreach (string tempDate in tempDateString)
            {
                PickOrder.PickOrders.Add( GetChildOrderList(tempDate));

            }
            
            

        }

        



        private PickList  GetChildOrderList(string _date)
        {
            var datePick =  OrderTable.Select("pickDate='"+ _date + "'");
            List<string> tempCustomerString = new List<string>();
            foreach (DataRow temp in datePick)
            {
                tempCustomerString.Add(temp["customerID"].ToString().Trim());
            }
            tempCustomerString = tempCustomerString.Distinct().ToList();//获得顾客ID列表

            PickList picklist = new PickList { PickDate = _date };
            picklist.PickLists = new List<PickListDetail>();

            foreach(string temp in tempCustomerString)
            {
                var xxx = OrderTable.Select("pickDate='"+_date +"'and customerID='"+ temp+"'");//当前日期和顾客下的列表
                List<string> tempPickPackageString = new List<string>();
                foreach(DataRow temp1 in xxx)
                {
                    tempPickPackageString.Add(temp1["packID"].ToString().Trim());//当前顾客包
                }
                tempPickPackageString = tempPickPackageString.Distinct().ToList();

                PickListDetail picklistdt = new PickListDetail
                {
                    CustomerName = temp,
                    CustomerID = xxx[0]["customerID"].ToString()
                 };
                picklistdt.Packages = new List<Package>();

                foreach (string temp2 in tempPickPackageString)
                {
                    Package pack = new Package();
                    pack.PackageDetails = new List<PackageDetail>();

                    var packnumber = from _a in xxx where _a["packID"].ToString()==temp2 select _a;
                    foreach(var _packnumber in packnumber)
                    {
                        pack.PackageDetails.Add(new PackageDetail
                        {
                            ProductCount = Convert.ToInt32(_packnumber["quantity"].ToString()),
                            ProductID = _packnumber["productID"].ToString(),
                            ProductName = _packnumber["productName"].ToString()
                        });
                    }
                    picklistdt.Packages.Add(pack);


                }

                picklist.PickLists.Add(picklistdt);



            }

            return picklist;

        }

        private void GetAllTable()
        {
            //加载数据
            var workBook = NPOIHelper.LoadWorkbook(@"D:\Work\项目\香烟\香烟\资料文件参考\订单分拣数据（慧联系统）\订单分拣数据（慧联系统）\分拣订单数据 - 副本.xls");
            var sheet = workBook.GetSheetAt(0);//获得第一个sheet
            var rows = sheet.GetRowEnumerator();//获得所有行

            totalOrder = new DataTable();
            rows.MoveNext();//移动到第一行，
            var rowfirst = (IRow)rows.Current;//获得当前行，
            for (int i = 0; i < rowfirst.LastCellNum; i++)//遍历，把第一行数据，当做行加入表中
            {
                totalOrder.Columns.Add(rowfirst.GetCell(i).ToString());
            }

            while (rows.MoveNext())//然后循环遍历
            {
                var row = (IRow)rows.Current;
                DataRow dr = totalOrder.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);//取得单元格
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                totalOrder.Rows.Add(dr);
            }


        }

        private void GetChildOrders()
        {

            ChildOrsersList = new List<DataTable>();
            childOrdersCustomerID = new List<string>();

            foreach (DataRow temp in totalOrder.Rows)
            {
                childOrdersCustomerID.Add(temp["customerID"].ToString());
            }


            childOrdersCustomerID = childOrdersCustomerID.Distinct().ToList();//quchong



            foreach (string customerID in childOrdersCustomerID)
            {
                List<DataRow> tempRows = new List<DataRow>();
                List<string> package = new List<string>();


                var rowTempRows = totalOrder.Select("customerID=" + customerID);

                foreach (DataRow ss in rowTempRows)
                {
                    package.Add(ss["packID"].ToString());
                }
                package = package.Distinct().ToList();
                
                foreach (string number in package)
                {
                    DataTable a = totalOrder.Clone();
                    var x = from rr in rowTempRows where rr["packID"].ToString() == number select rr;
                    foreach (var tt in x)
                    {
                        a.ImportRow(tt);
                    }

                    ChildOrsersList.Add(a);
                }



            }

            Console.WriteLine(ChildOrsersList.Count);







        }

        private void GetChildOrdersShow()
        {
            ChildOrderListShow = new List<DataTable>();
            foreach (DataTable temp in ChildOrsersList)
            {
                DataTable aa = temp.DefaultView.ToTable(false, new string[] { "routeName", "customerID", "customerName", "productID", "productName", "quantity" });
                aa.Columns.Add("Readed");
                for (int i = 0; i < aa.Rows.Count; i++)
                {
                    aa.Rows[i][aa.Columns.Count - 1] = "0";
                }

                ChildOrderListShow.Add(aa);


            }

            



        }

        private void GetChildOrderAllCigaretteNum()
        {
            int sum = 0;
            ChildOrderCigaretteNum = new List<int>();
            foreach (DataTable temp in ChildOrderListShow)
            {
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(temp.Rows[i][temp.Columns.Count - 2]);
                }
                ChildOrderCigaretteNum.Add(sum);
                sum = 0;
            }
            Console.WriteLine(ChildOrderCigaretteNum.Count);
            Console.WriteLine(ChildOrderCigaretteNum[0]);
        }
        #endregion

    }



    */
}
