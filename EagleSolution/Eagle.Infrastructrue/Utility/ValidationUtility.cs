using System;
using System.Data.SqlTypes;

namespace Eagle.Infrastructrue.Utility
{
    public static class ValidationUtility
    {
        public static bool Null(this object obj)
        {
            if (obj == null)
            {
                return true;
            }
            return false;
        }
        public static bool Null(this Guid obj)
        {
            if (obj == Guid.Empty)
            {
                return true;
            }
            return false;
        }
        public static bool Null(this DateTime obj)
        {
            if (obj < SqlDateTime.MinValue.Value)
            {
                return true;
            }
            return false;
        }

        public static DateTime DefaultValue(this DateTime obj)
        {
            return obj.DefaultValue(SqlDateTime.MinValue.Value);
        }

        public static DateTime DefaultValue(this DateTime obj, DateTime defaultValue)
        {
            if (obj < SqlDateTime.MinValue.Value)
            {
                return defaultValue;
            }
            return obj;
        }

    }
}