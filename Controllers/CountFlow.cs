namespace ISERV_ETL.Controllers
{
    public static class CountFlow
    {
        /// <summary>
        /// Запрашивает у пользователя количество потоков, в которых будут подгружаться данные с API.
        /// </summary>
        /// <returns></returns>
        public static int Get()
        {
            int count = 0;

            while (count <= 0)
            {
                Console.WriteLine("Введите количество потоков: ");
                if (int.TryParse(Console.ReadLine(), out count))
                {
                    if (count > 0)
                    {
                        return count;
                    }

                    Console.WriteLine("Введите положительное число!");
                }
                else
                {
                    Console.WriteLine("Введите целое число!");
                    Console.WriteLine();
                }
            }

            return count;
        }
    }
}
