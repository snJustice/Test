using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Data.OleDb;

namespace 条烟纠错系统.TableHandles
{
    public class SqlServerGetOrder : IGetOrder
    {
        string connectSql = "Data Source=JACK\\SQLEXPRESS;Initial Catalog=JC_JCXT2;User ID=sa;Password=123456@qq;Integrated Security=True;";
        string quarrySql = "select * from V_PackInterface";

        SqlConnection connection;

        public SqlServerGetOrder()
        {
            try
            {
                connection = new SqlConnection(connectSql);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void Connect()
        {
            if(connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public DataTable GetRemoteOrder()
        {
            //string comSearchDataL = "SELECT  [pickDate],[logistID],[packID],[productOrder],[routeID],[routeName],[customerID],[customerName],[customerAddress],[productID],[productName],[quantity],[productType],[length],[width],[height],[lineID],[pickNo],[pickOrderNo] FROM[JC_JCXT].[dbo].[V_PackInterface]";
            DataSet dat = new DataSet();
            DataTable x = new DataTable();
            try
            {
                //  SqlCommand comm = new SqlCommand(comSearchData, conn);
                SqlDataAdapter myda = new SqlDataAdapter(quarrySql, connection);
                myda.Fill(dat, "PackInterface");
                x = dat.Tables["PackInterface"];
                return x;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }
    }
}
