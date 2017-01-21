using Example.Models;
using Example.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Example.Controllers
{
    public class SQLInjectionController : Controller
    {
#if false
        private readonly string _connectionstring;
        public SQLInjectionController(IOptions<ConnectionStrings> connectionstring)
        {
            _connectionstring = connectionstring.Value.Default;
        }
#endif
        public IActionResult Index() { return View(); }
        public IActionResult ListDbTables()
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
        [AcceptVerbs("POST")]
        public IActionResult ListDbTables(string value)
        {
            return Search(value);
        }
        public IActionResult ListCompanies()
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
        [AcceptVerbs("POST")]
        public IActionResult ListCompanies(string value)
        {
            return Search(value);
        }
        public IActionResult InsertCompany()
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
        [AcceptVerbs("POST")]
        public IActionResult InsertCompany(string value)
        {
            return Search(value);
        }

        public IActionResult DeleteCompany()
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
        [AcceptVerbs("POST")]
        public IActionResult DeleteCompany(string value)
        {
            return Search(value);
        }

        public IActionResult HandleError()
        {
            //Default
            ViewData["value"] = @"xxxx' ;
!!!FAIL!!!!
--";
            ViewData.Model = new List<Product>();
            return View("Search");
        }
        [AcceptVerbs("POST")]
        public IActionResult HandleError(string value)
        {
            return Search(value);
        }

        [NonAction]
        private IActionResult Search(string value)
        {
            ViewData["value"] = value;
            var values = new List<Product>();
#if false
            using (var connection = new SqlConnection(_connectionstring))
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
#else
            throw new NotImplementedException();
#endif
        }
    }
}