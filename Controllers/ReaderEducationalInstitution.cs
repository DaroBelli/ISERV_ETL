using ISERV_ETL.Model;

namespace ISERV_ETL.Controllers
{
    public class ReaderEducationalInstitution
    {
        readonly Semaphore semaphore;

        public List<EducationalInstitution> AllEducationalInstitution { get; set; } = [];

        readonly object locker = new();

        public ReaderEducationalInstitution(Semaphore sem, IEnumerable<KeyValuePair<string, string?>> countries)
        {
            semaphore = sem;

            GetAll(countries);
        }

        /// <summary>
        /// Получает по списку стран данные, по всем учебным заведениям и добавляет их в список.
        /// </summary>
        /// <param name="countries">Словарь стран.</param>
        public void GetAll(IEnumerable<KeyValuePair<string, string?>> countries) 
        {

            List<Thread> threads = [];

            foreach (var country in countries)
            {
                var countryValue = country.Value;

                Thread thread = new(Get)
                {
                    Name = $"{countryValue}"
                };

                threads.Add(thread);

                thread.Start(countryValue);

            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        /// <summary>
        /// Получает по стране данные, по всем учебным заведениям и добавляет их в список.
        /// </summary>
        /// <param name="country">Наименование страны.</param>
        public void Get(object? country)
        {
            semaphore.WaitOne();

            var client = Client.Get();

            var response = client.GetAsync($"?country={country}").Result;

            if (response.IsSuccessStatusCode)
            {
                var educationalInstitutions = response.Content.ReadAsAsync<IEnumerable<EducationalInstitution>>().Result;

                lock (locker)
                {
                    AllEducationalInstitution.AddRange(educationalInstitutions);
                }
            }
            else
            {
                Console.WriteLine($"{response.StatusCode} ({response.ReasonPhrase})");
            }

            client.Dispose();

            semaphore.Release();
        }
    }
}
