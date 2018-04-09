using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 条烟纠错系统.Logic;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using 条烟纠错系统.Models;

namespace 条烟纠错系统.TableHandles
{
    public delegate void Finished();

    public abstract class TableHandleBase //数据包处理的基类
    {
        //本地的数据库，用entityframework去映射拿数据，这个框架只用来从本地数据库中分割出包，
        //还要考虑，一包里面超过25条的情况。

        public event Finished finishedLoading;

        public DataBaseProgress localDatabase = new DataBaseProgress();

        //实际使用的包
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

        public void LocatePackage(int index)
        {

            currentPackageIndex = index;
            lastPackageIndex = index - 1;
            nextPackageIndex = index + 1;
        }
        #endregion

        //用entityframework 直接执行sql语句
        public bool ClearCurrentLocalDatabaseTable()
        {
            try
            {
                localDatabase.dataBase.Database.ExecuteSqlCommand("delete from PickList");
                localDatabase.dataBase.Database.ExecuteSqlCommand("delete from PickListDetail");
                localDatabase.dataBase.Database.ExecuteSqlCommand("delete from PickPackage");
                localDatabase.dataBase.Database.ExecuteSqlCommand("delete from PickPackageDetail");
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        //开始处理数据，先删掉本地数据中的数据，然后处理
        public async Task StartHandle()
        {
            var result = ClearCurrentLocalDatabaseTable();
            if (result != true)
            {
                return;
            }
            await HandleData();
        }

        public void InsertOrder(DataTable _order)
        {
            string sqllocal = "Data Source=JACK\\SQLEXPRESS;Initial Catalog=JC_JCXT4;User ID=sa;Password=123456@qq;Integrated Security=True;";
            SqlBulkCopy sqlbulkcopy;
            sqlbulkcopy = new SqlBulkCopy(sqllocal, SqlBulkCopyOptions.UseInternalTransaction);
            try
            {


                //sqlbulkcopy = new SqlBulkCopy(sqllocal, SqlBulkCopyOptions.UseInternalTransaction);
                sqlbulkcopy.DestinationTableName = "V_PackInterface";
                sqlbulkcopy.WriteToServer(_order);
                Debug.WriteLine("导入成功");
                //return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //return false;
            }
            finally
            {
                sqlbulkcopy.Close();
            }
        }

        public void FinishLoading()
        {
            finishedLoading?.Invoke();
        }

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

        public abstract  Task<bool> HandleData();//处理数据

        public abstract bool GetOrder();//根据接口去获得相应的数据

        public abstract void Connect();



    }
}
