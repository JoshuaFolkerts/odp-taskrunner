using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODP.Services.Helpers
{
    public static class StringHelper
    {
        public static string GetUrlPath(string url)
        {
            try
            {
                var uri = new Uri(url);
                return uri.PathAndQuery;
            }
            catch (Exception)
            {
            }
            return url;
        }
    }
}