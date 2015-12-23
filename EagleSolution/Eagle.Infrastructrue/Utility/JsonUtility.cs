using Newtonsoft.Json;

namespace Eagle.Infrastructrue.Utility
{
    public static class JsonUtility
    {
        public static string ToJson(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(obj);
        }

        public static T ToDeserialize<T>(this string str)
        {
            if (str == null)
            {
                return default(T);
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch(JsonException ex)
            {
                return default(T);
            }
        }
    }
}
