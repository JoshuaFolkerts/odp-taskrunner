using System;

namespace ODP.Services.Helpers
{
    public static class UnixTimeHelper
    {
        /// <summary>
        /// Converts DateTime to Unix time.
        /// </summary>
        public static long ToUnixTime(this DateTime time) =>
            (long)time.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        /// <summary>
        /// Converts Unix time to DateTime.
        /// </summary>
        public static DateTime ToDateTime(long unixTime) =>
            new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(unixTime));
    }
}