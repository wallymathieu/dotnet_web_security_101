using Example.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example.Controllers
{
    public class SQLInjectionController : Controller
    {
        public ActionResult Index() { return View(); }
        public ActionResult ListDbTables()
        {
            //Default
            ViewData["value"] = @"xxxx' 
UNION ALL 
select c.TABLE_NAME [ProductName], c.COLUMN_NAME [Description]
             from INFORMATION_SCHEMA.COLUMNS c  
            inner join INFORMATION_SCHEMA.TABLES t on c.TABLE_NAME = t.TABLE_NAME  
            where 
t.TABLE_TYPE = 'BASE TABLE'--";
            ViewData.Model = new List<Product>();
            return View("Search");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ListDbTables(string value)
        {
            return Search(value);
        }
        public ActionResult ListCompanies()
        {
            //Default
            ViewData["value"] = @"xxxx' 
UNION ALL 
select CONVERT(NVARCHAR(MAX), CompanyID) [ProductName], CompanyName [Description]
             from tblCompany c  
--";
            ViewData.Model = new List<Product>();
            return View("Search");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ListCompanies(string value)
        {
            return Search(value);
        }
        public ActionResult InsertCompany()
        {
            //Default
            ViewData["value"] = @"xxxx' ;
INSERT INTO [tblCompany]
           ([CompanyName])
     VALUES
           ('Test');
--";
            ViewData.Model = new List<Product>();
            return View("Search");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InsertCompany(string value)
        {
            return Search(value);
        }

        public ActionResult DeleteCompany()
        {
            //Default
            ViewData["value"] = @"xxxx' ;
    DELETE FROM [tblCompany]
      WHERE 
           [CompanyName]='Test';
--";
            ViewData.Model = new List<Product>();
            return View("Search");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteCompany(string value)
        {
            return Search(value);
        }

        public ActionResult HandleError()
        {
            //Default
            ViewData["value"] = @"xxxx' ;
!!!FAIL!!!!
--";
            ViewData.Model = new List<Product>();
            return View("Search");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HandleError(string value)
        {
            return Search(value);
        }

        [NonAction]
        private ActionResult Search(string value)
        {
            ViewData["value"] = value;
            var values = new List<Product>();
            using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                connection.Open();
                string cmdText = string.Format(@"SELECT
      [ProductName]
      ,[Description]
  FROM [dbo].[tblProduct] WHERE [ProductName] LIKE '{0}'", value);
                try
                {
                    using (var cmd = new SqlCommand(cmdText,
                          connection))

                    using (var reader = cmd.ExecuteReader())
                    {
                        var productNameX = reader.GetOrdinal("ProductName");
                        var descriptionX = reader.GetOrdinal("Description");
                        while (reader.Read())
                        {
                            values.Add(new Product
                            {
                                ProductName = reader.IsDBNull(productNameX) ? "" : reader.GetString(productNameX),
                                Description = reader.IsDBNull(descriptionX) ? "" : reader.GetString(descriptionX)
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("CMD: " + cmdText, e);
                }
                ViewData.Model = values;
                return View("Search");
            }
        }
    }
}