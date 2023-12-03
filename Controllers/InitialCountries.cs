using Microsoft.Extensions.Configuration;

namespace ISERV_ETL.Controllers
{
    public static class InitialCountries
    {
        /// <summary>
        /// Задаёт список стран, по которым нужно получить данные.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string,string?>> Get()
        {
            var config = ConfigJSON.GetConfig();
            var countries = config.GetSection("Countries").AsEnumerable().Where(x => !string.IsNullOrEmpty(x.Value));

            return countries;
        }
    }
}
