using Example.Models;
using System;

namespace Example.XSS
{
    public partial class ShowEncoded : System.Web.UI.Page
    {
        public Product Model;
        protected void Page_Load(object sender, EventArgs e)
        {
            Model = Product.GetXSSUserProduct();
        }
    }
}