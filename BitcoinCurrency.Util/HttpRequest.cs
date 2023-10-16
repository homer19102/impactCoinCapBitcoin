using System.Net;

namespace BitcoinCurrency.Util
{
    public static class HttpRequest
    {
        public static async Task<Tuple<string, HttpStatusCode>> HttpGet(string route, string extraParams)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();

                response = await client.GetAsync($"{route}{extraParams}").ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    return Tuple.Create($"{response.StatusCode} - {response.Content.ReadAsStringAsync().Result}", response.StatusCode);

                return Tuple.Create(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }
    }
}
