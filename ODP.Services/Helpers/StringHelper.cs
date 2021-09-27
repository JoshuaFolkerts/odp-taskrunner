using System;
using System.ComponentModel;

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

        public static bool HasValue(this string s) =>
            s != null && !string.IsNullOrWhiteSpace(s);

        public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                return null;
            }

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }
    }
}