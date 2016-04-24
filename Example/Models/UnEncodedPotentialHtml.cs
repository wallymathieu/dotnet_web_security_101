using System.Web.Mvc;

namespace Example.Models
{
    /// <summary>
    /// Normally we want the protection against user entered html. In this case we want to make sure that it's possible to render bbcode or markdown
    /// </summary>
    public class UnEncodedPotentialHtml
    {
        [AllowHtml]
        public string Value { get; set; }
    }
}