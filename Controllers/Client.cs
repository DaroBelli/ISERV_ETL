using System.Net.Http.Headers;

namespace ISERV_ETL.Controllers
{
    public static class Client
    {
        /// <summary>
        /// Настройка подключения.
        /// </summary>
        /// <returns></returns>
        public static HttpClient Get()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("http://universities.hipolabs.com/search")
            };

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
