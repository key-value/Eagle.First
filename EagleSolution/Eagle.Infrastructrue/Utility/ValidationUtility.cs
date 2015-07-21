using System;

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
    }
}