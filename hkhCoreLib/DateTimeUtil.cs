using System;

namespace hkhCoreLib
{
    public static class DateTimeUtil
    {
        public static DateTime NowIfMinValue(this DateTime dt)
        {
            return DateTime.MinValue == dt ? DateTime.Now : dt;
        }

        public static DateTime TodayIfMinValue(this DateTime dt)
        {
            return DateTime.MinValue == dt ? DateTime.Today : dt;
        }

        public static DateTime Max(this DateTime dt, DateTime other)
        {
            return MaxOf(dt, other);
        }

        public static DateTime MaxOf(DateTime left, DateTime right)
        {
            return left > right ? left : right;
        }

        public static DateTime Min(this DateTime dt, DateTime other)
        {
            return MinOf(dt, other);
        }

        public static DateTime MinOf(DateTime left, DateTime right)
        {
            return left < right ? left : right;
        }
    }
}
