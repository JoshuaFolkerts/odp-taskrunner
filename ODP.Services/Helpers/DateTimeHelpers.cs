using System;

namespace ODP.Services.Helpers
{
    public static class DateTimeHelpers
    {
        public static DateTime RandomDateTime(DateTime startDate, DateTime endDate)
        {
            var randomTest = new Random();

            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
            return startDate + newSpan;
        }
    }
}