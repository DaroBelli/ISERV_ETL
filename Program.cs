using ISERV_ETL.Controllers;

namespace ISERV_ETL
{
    public class Program
    {
        private static void Main(string[] args)
        {
            int count = CountFlow.Get();

            Semaphore semaphore = new(count, count);

            ReaderEducationalInstitution reader = new(semaphore, InitialCountries.Get());
             
            DataBaseMSSQL.AddEducationalInstitutions(reader.AllEducationalInstitution);
        }
    }
}