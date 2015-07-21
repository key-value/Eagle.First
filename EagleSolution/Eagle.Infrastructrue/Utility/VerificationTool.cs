using System;
using System.Collections.Generic;
using System.Reflection;
using Eagle.Infrastructrue.Utility;

namespace Eagle.Infrastructrue.Utility
{
    public static class VerificationTool
    {
        public static bool VerificationInt(this Dictionary<string, string> dictionary, string paramName, ref int result)
        {
            if (!dictionary.ContainsKey(paramName) || dictionary[paramName] == null)
            {
                return false;
            }
            if (int.TryParse(dictionary[paramName], out result))
            {
                return true;
            }
            return false;
        }
        public static bool TryVerification<T>(this Dictionary<string, string> dictionary, string paramName, ref T result) where T : struct
        {
            if (!dictionary.ContainsKey(paramName) || dictionary[paramName] == null)
            {
                return false;
            }
            var type = typeof(T);
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }
            var tryParse = type.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder,
                                   new[] { typeof(string), type.MakeByRefType() },
                                   new[] { new ParameterModifier(2) });
            var parameters = new[] { dictionary[paramName], Activator.CreateInstance(type) };
            bool success = (bool)tryParse.Invoke(null, parameters);
            result = (T)parameters[1];
            return success;
        }
        public static bool Verification<T>(this Dictionary<string, string> dictionary, string paramName, ref T result) where T : class
        {
            if (!dictionary.ContainsKey(paramName) || dictionary[paramName] == null)
            {
                return false;
            }
            var resultStr = dictionary[paramName];

            result = resultStr.ToDeserialize<T>();

            return result != default(T);
        }

        public static bool VerificationString(this Dictionary<string, string> dictionary, string paramName, ref string result)
        {
            if (!dictionary.ContainsKey(paramName) || dictionary[paramName] == null)
            {
                return false;
            }
            result = dictionary[paramName];
            return true;
        }
        public static bool VerificationGuid(this Dictionary<string, string> dictionary, string paramName, ref Guid result)
        {
            if (!dictionary.ContainsKey(paramName) || dictionary[paramName] == null)
            {
                return false;
            }
            if (Guid.TryParse(dictionary[paramName], out result))
            {
                return true;
            }
            return false;
        }
    }
}
