using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;

namespace 条烟纠错系统
{
    class DataProgress
    {
        
        
        private string conServer = @"Server = JACK-PC;";
        private string conData = "Database=码管理系统;";//数据库名
        private string conWindow = "integrated security=SSPI";

        //身份验证
        string comTest1 = "select count(*) from 码管理系统.dbo.[用户] where 用户名='";
        string comTest2 = "' and 密码='";
        string comTest3 = "'";
        
        //插入码数据
        private string commStr1 = "insert into 码管理系统.dbo.[";
        private string commStr2 = "] values ('";
        private string commStr3 = "','";






        //查询Excel
        private string connectString = "Data Source=JACK\\SQLEXPRESS;Initial Catalog=JC_JCXT;User ID=sa;Password=123456@qq;Integrated Security=True;";

        private SqlConnection conn;

        //连接数据库
        public void ConnectDataBase()
        {
            string constr = connectString;
            conn = new SqlConnection(constr);
            try
            {
                conn.Open();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        //断开数据库
        public void DisconnectDataBase()
        {
            try
            {
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //写的不是很好,读取20条数据
        public DataTable ReadPickData()
        {
            string comSearchDataL = "SELECT  [pickDate],[logistID],[packID],[productOrder],[routeID],[routeName],[customerID],[customerName],[customerAddress],[productID],[productName],[quantity],[productType],[length],[width],[height],[lineID],[pickNo],[pickOrderNo] FROM[JC_JCXT].[dbo].[V_PackInterface]";
            DataSet dat = new DataSet();
            DataTable x = new DataTable();
            try
            {
              //  SqlCommand comm = new SqlCommand(comSearchData, conn);
                SqlDataAdapter myda = new SqlDataAdapter(comSearchDataL, conn);
                myda.Fill(dat, "Table_1");
                x =  dat.Tables["Table_1"];
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
