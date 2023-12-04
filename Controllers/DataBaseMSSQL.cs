using ISERV_ETL.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ISERV_ETL.Controllers
{
    public static class DataBaseMSSQL
    {
        /// <summary>
        /// Добавляет полученные данные по учебным заведениям в БД.
        /// </summary>
        /// <param name="educationalInstitutions">Список учебных заведений.</param>
        public static void AddEducationalInstitutions(List<EducationalInstitution> educationalInstitutions)
        {
            using SqlConnection connection = new(GetConnectionString());

            connection.Open();

            string sqlQuery;

            foreach (var educationalInstitution in educationalInstitutions)
            {
                sqlQuery = $"INSERT INTO EducationalInstitutions (Name, Country, WebSites) " +
                    $"VALUES (@name, @country, @webSites)";

                SqlCommand command = new(sqlQuery, connection);
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = educationalInstitution.Name;
                command.Parameters.Add("@country", SqlDbType.VarChar).Value = educationalInstitution.Country;
                command.Parameters.Add("@webSites", SqlDbType.VarChar).Value = educationalInstitution.Web_Pages == null 
                    ? string.Empty 
                    : string.Join(';', educationalInstitution.Web_Pages);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Получает строку подключения к БД.
        /// </summary>
        /// <returns></returns>
        private static string? GetConnectionString()
        {
            var config = ConfigJSON.GetConfig();
            return config.GetConnectionString("DefaultConnection");
        }
    }
}
