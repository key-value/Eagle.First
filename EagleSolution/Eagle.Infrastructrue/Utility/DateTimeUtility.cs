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


        public static void GetWeekDays(int targetWeek, out DateTime beginTime, out DateTime endTime)
        {
            var year = targetWeek / 100;
            var weekNum = targetWeek % 100 - 1;

            DateTime mDatetime = new DateTime(year, 1, 1);//year为要求的那一年
            int firstweekfirstday = Convert.ToInt32(mDatetime.DayOfWeek);//一年中第一天是周几
            var days = (double)(7 - firstweekfirstday);
            DateTime secondweekfisrtday = mDatetime.AddDays(-days);
            beginTime = secondweekfisrtday.AddDays(weekNum * 7);//第N周第一天
            endTime = secondweekfisrtday.AddDays(weekNum * 7 + 6);//第N周最后一天
        }
    }
}