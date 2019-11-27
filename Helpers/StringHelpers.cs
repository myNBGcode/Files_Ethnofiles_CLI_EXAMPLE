using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FIleAPI_CLI
{
    public static class StringHelpers
    {
        /// <summary>
        /// Extension to trim sting
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Clear(this string value)
        {
            if (value == null)
                return null;
            if (value.Trim() == "")
                return null;
            return value.Trim();
        }

        public static string TryParseJSON(string json)
        {
            try
            {
                var response = JsonConvert.DeserializeObject(json).ToString();
                return response;
            }
            catch
            {                
                return json;
            }
        }
    }
}
