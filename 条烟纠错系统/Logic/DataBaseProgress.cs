using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 条烟纠错系统.Models;
using 条烟纠错系统.Applications;
using System.Data.Entity;
using System.Diagnostics;

namespace 条烟纠错系统.Logic
{
    public class DataBaseProgress
    {
        public  PSContent dataBase;
        public DataBaseProgress()
        {
            dataBase = new PSContent();

            dataBase.Configuration.AutoDetectChangesEnabled = false;
        }

        public void SaveChange()
        {
            var temp = dataBase.PackageDetails.FirstOrDefault() ;

            temp.Details = "ssdad";
            //dataBase.en
            dataBase.Entry(temp).State = System.Data.Entity.EntityState.Modified;
            var result = dataBase.SaveChanges();
        }

        //清理分拣的数据
        public  void ClearAllPickData()
        {
            try
            {
                
                dataBase.PackageDetails.RemoveRange(dataBase.PackageDetails);
                dataBase.Packages.RemoveRange(dataBase.Packages);
                dataBase.PickLists.RemoveRange(dataBase.PickLists);
                
                dataBase.PickListDetails.RemoveRange(dataBase.PickListDetails);
                /*
                 * //这样快一点
                dataBase.Database.ExecuteSqlCommand("delete from PickList");
                 dataBase.Database.ExecuteSqlCommand("delete from PickListDetail");
                 dataBase.Database.ExecuteSqlCommand("delete from PickPackage");
                 dataBase.Database.ExecuteSqlCommand("delete from PickPackageDetail");*/
                dataBase.SaveChanges();



            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }

        public async Task<int> InsertPickList(PickList _temp)
        {
            try
            {
                dataBase.PickLists.Add(_temp);
                
                return await dataBase.SaveChangesAsync();
            }
            catch(Exception ex )
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

             
        }

        public async Task<int> InsertPickListDetail(PickListDetail _temp)
        {
            try
            {
                dataBase.PickListDetails.Add(new PickListDetail
                {
                    LineID = _temp.LineID,
                    PickListID = _temp.PickListID,
                    CustomerID = _temp.CustomerID,
                    CustomerName = _temp.CustomerName,
                    

                });
                Console.WriteLine("ok");
                return await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }


        }

        public async Task<int> InsertPickPackage(PickPackage _temp)
        {
            try
            {
                dataBase.Packages.Add(new PickPackage
                {
                    
                    CustomerID = _temp.CustomerID,
                    PickPackageID = _temp.PickPackageID,

                });
                Console.WriteLine("ok");
                return await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }


        }

        public async Task<int> InsertPickPackageDetail(PickPackageDetail _temp)
        {
            try
            {
                dataBase.PackageDetails.Add(new PickPackageDetail
                {

                   PickPackageID = _temp.PickPackageID,
                   PickPackageDetailID = _temp.PickPackageDetailID,
                   Quantity  = _temp.Quantity,
                   Readed = _temp.Readed,
                   ProductID = _temp.ProductID


                });
                Console.WriteLine("ok");
                return await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }


        }


        public async Task<int> InsertPickListRange(List<PickList> _temp)
        {
            try
            {
                dataBase.PickLists.AddRange(_temp);
                
                Console.WriteLine("ok");
                return await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }


        }

        public async Task<int> InsertPickListDetailRange(List<PickListDetail> _temp)
        {
            try
            {
                dataBase.PickListDetails.AddRange(_temp);
                
                return await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }


        }

        public async Task<int> InsertPickPackageRange(List<PickPackage> _temp)
        {
            try
            {
                dataBase.Packages.AddRange(_temp);
                
                return await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return 0;
            }


        }

        public async Task<int> InsertPickPackageDetailRange(List<PickPackageDetail> _temp)
        {
            try
            {
                dataBase.PackageDetails.AddRange(_temp);
                
                return await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }


        }

        public async Task<int> InsertProductRange(List<Product> _temp)
        {
            try
            {
                 dataBase.Products.AddRange(_temp);

                return await dataBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }


        }

        public async Task<List<PickList>> GetAllPickList()
        {
            return  await dataBase.PickLists.Include("PickListDetails.Packages.PackageDetails.Product").ToListAsync();
        }

        public async Task<List<Product>> GetProductInRemote()
        {
            
            return await  dataBase.Database.SqlQuery<Product>("SELECT DISTINCT TOP 1000 [productID],[productName] FROM[JC_JCXT].[dbo].[V_PackInterface] ").ToListAsync();
            
        }

        public async Task<List<Product>> GetProduct()
        {
            return await dataBase.Products.ToListAsync();
        }


        public async Task< int> GetIDByPickDate(string _date)
        {
            var model = await dataBase.PickLists.FirstOrDefaultAsync(c=>c.PickDate == _date);
            if (model == null) return -1;
            return model.PickListID;
        }
    }
}
