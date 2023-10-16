using System.Text.Json;

namespace BitcoinCurrency.Util
{
    public static class Utils
    {

        public static JsonElement GetElementInStringJson(string jsonString, string property)
        {
            JsonDocument jsonDoc = JsonDocument.Parse(jsonString);

            JsonElement root = jsonDoc.RootElement;

            return root.GetProperty(property);
        }
    }
}
