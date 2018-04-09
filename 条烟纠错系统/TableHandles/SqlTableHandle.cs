using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using 条烟纠错系统.Models;

namespace 条烟纠错系统.TableHandles
{
    public class SqlTableHandle : TableHandleBase
    {
        //string sql = "Data Source=JACK\\SQLEXPRESS;Initial Catalog=JC_JCXT2;User ID=sa;Password=123456@qq;Integrated Security=True;";
        string sqllocal = "Data Source=JACK\\SQLEXPRESS;Initial Catalog=JC_JCXT4;User ID=sa;Password=123456@qq;Integrated Security=True;";

        IGetOrder iGetOrder;

        //IDbConnection connectionRemote;
        IDbConnection connectionLocal;



        public SqlTableHandle(IGetOrder _GetOrder)
        {
            iGetOrder = _GetOrder;
        }


        //订单的服务器连接，本地服务器连接
        public override void Connect()
        {
            iGetOrder.Connect();

            
            connectionLocal = new SqlConnection(sqllocal);
            try
            {
                connectionLocal.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        
        public async override Task<bool> HandleData()
        {
            string sql1 = "select distinct PickDate from V_PackInterface ";
            var data = connectionLocal.Query<PickList>(sql1).ToList();
            int count = 1;
            foreach(PickList x in data)
            {
                x.PickListID = count++;
            }
            string sql2 = "select distinct customerID,customerName,pickDate, lineID from V_PackInterface ";
            var data2 = connectionLocal.Query<PickListDetail>(sql2).ToList();
            count = 1;
            foreach(PickListDetail x in data2)
            {
                x.CustomerNumberID = count++;
            }

            string sql3 = "select distinct customerID,customerName,pickDate, packID,lineID from V_PackInterface ";
            var data3 = connectionLocal.Query<PickPackage>(sql3).ToList();
            foreach(PickPackage x in data3)
            {
                x.PickPackageID = x.CustomerID + x.PackID.ToString();
                x.Details = "";
            }

            string sql4 = "select distinct  packID,lineID,productID,quantity,customerID from V_PackInterface ";
            var data4 = connectionLocal.Query<PickPackageDetail>(sql4).ToList();
            count = 1;
            foreach (PickPackageDetail x in data4)
            {
                x.PickPackageID = x.CustomerID + x.PackID.ToString();
                x.PickPackageDetailID = count++;
                x.Details = "";
            }

            string insertSql1 = "insert into PickList (PickListID,PickDate) values(@PickListID,@PickDate)";
            var result = connectionLocal.Execute(insertSql1, data);

            string insertSql2 = @"insert into PickListDetail (CustomerID,CustomerName,LineID,PickListID,CustomerNumberID,PickDate) 
                values(@CustomerID,@CustomerName,@LineID,@PickListID,@CustomerNumberID,@PickDate)";
            var result2 = connectionLocal.Execute(insertSql2, data2);

            string insertSql3 = @"insert into PickPackage (PickPackageID,CustomerID,PackID,CustomerName,LineID,PickDate,Details) 
                values(@PickPackageID,@CustomerID,@PackID,@CustomerName,@LineID,@PickDate,@Details)";
            var result3 = connectionLocal.Execute(insertSql3, data3);

            string insertSql4 = @"insert into PickPackageDetail (PickPackageDetailID,Quantity,Readed,PickPackageID,ProductID,PackID,CustomerID,Details) 
                values(@PickPackageDetailID,@Quantity,@Readed,@PickPackageID,@ProductID,@PackID,@CustomerID,@Details)";
            var result4 = connectionLocal.Execute(insertSql4, data4);

            FinishLoading();
            return true;
        }


        //先获得远程的订单，然后把这个订单保存到本地数据库中
        public override bool GetOrder()
        {
            var rowOrder = iGetOrder.GetRemoteOrder();
            InsertOrder(rowOrder);

            return true;
            

        }
    }
}
