using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;


namespace JsonRead.Json
{
    public class CustomerJsonRead<TClass> where TClass:new()
    {
        private TClass T;
        const string jsonDirectory = "Json";
        readonly string jsonFilePath;
        //构造函数
        public CustomerJsonRead()
        {

            this.T = new TClass();
            //创建Json文件保存的文件夹
            if (!Directory.Exists(jsonDirectory))
            {
                Directory.CreateDirectory(jsonDirectory);
            }

            //创建Json文件
            jsonFilePath = jsonDirectory + "/" + T.GetType().ToString() + ".json";
            if (!File.Exists(jsonFilePath))
            {
                File.Create(jsonFilePath).Close();
            }
        }

        public void SetJsonClass(TClass _t)
        {
            if(_t!=null)
                T = _t;
        }
        //获得Jsonstring
        private string GetJsonString()
        {
            return  JsonConvert.SerializeObject(T);

            
        }

        //保存jsonstring
        public void SaveJsonString()
        {
            var _jsonstring = GetJsonString();
            //System.Console.WriteLine(_jsonstring);
            if (!File.Exists(jsonFilePath))
            {
                var temp = File.Create(jsonFilePath);
                temp.Close();
            }
            FileStream jsonFile = null;
            try
            {
                jsonFile = File.Open(jsonFilePath, FileMode.Create, FileAccess.Write);
                byte[] bytes = new UTF8Encoding(true).GetBytes(_jsonstring);
                jsonFile.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                
                System.Environment.Exit(0);

            }
            finally { jsonFile.Close(); }
        }

        //获得Json类
        public TClass GetJsonClass()
        {
            FileStream jsonFile = null;
            try
            {
                while (IsFree(jsonFilePath)) ;
                jsonFile = File.Open(jsonFilePath, FileMode.Open);
                int length = (int)jsonFile.Length;
                byte[] heByte = new byte[length];
                int r = jsonFile.Read(heByte, 0, heByte.Length);
                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                Console.WriteLine(myStr);
                TClass t = (TClass)JsonConvert.DeserializeObject(myStr, typeof(TClass));
                jsonFile.Close();
                return t;
            }
            catch (Exception ex)
            {
                
                System.Environment.Exit(0);
                return default(TClass);
            }
        }

        private bool IsFree(string _filename)
        {
            bool inUse = true;
            FileStream fs = null;
            try
            {
                fs = new FileStream(_filename, FileMode.Open, FileAccess.Read,
                FileShare.None);
                inUse = false;
            }
            catch
            {
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return inUse;
        }

    }
}
