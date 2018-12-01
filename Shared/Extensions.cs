using Microsoft.AspNetCore.Http;
using System;

namespace Shared
{
    public static class Extensions
    {
        public static T Tap<T>(this T self, Action<T> action) { action(self); return self; }
        /// <summary>
        /// Not to be trusted ;)
        /// </summary>
        public static string UrlReferrer(this HttpRequest request)
        {
            return request.Headers["Referer"].ToString();
        }
        public static string Origin(this HttpRequest request)
        {
            return request.Headers["Origin"].ToString();
        }
    }
}
