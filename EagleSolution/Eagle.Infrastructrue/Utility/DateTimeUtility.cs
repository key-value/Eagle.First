using System;
using System.Globalization;

namespace Eagle.Infrastructrue.Utility
{
    public class DateTimeUtility
    {
        /// <summary>
        /// 获取指定日期，在为一年中为第几周
        /// </summary>
        /// <param name="dt">指定时间</param>
        /// <reutrn>返回第几周</reutrn>
        public static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var weekNum = weekOfYear + dt.Year * 100;

            var dayOfWeek = DateTime.Now.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Sunday)
            {
                weekNum += 1;
            }
            return weekNum;
        }
    }
}