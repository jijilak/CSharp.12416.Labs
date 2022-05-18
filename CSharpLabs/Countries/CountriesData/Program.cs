using System;
using System.Configuration;
using System.Data.Common;

namespace CountriesData
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataProvider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    ShowError("Ошибка при соединении");
                    return;
                }
                connection.ConnectionString = connectionString;
                connection.Open();

                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    ShowError("Ошибка при создании команды");
                    return;
                }

                command.Connection = connection;
                command.CommandText = "SELECT * FROM Countries";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    for(int i = 0; i < reader.FieldCount; i++)
                    {
                        int pad = i > 2 ? 15 : i == 2 ? 30 : 4;
                        string column = reader.GetName(i);
                        if (column.Length > pad) column = column.Substring(0, pad - 1);
                        Console.Write(column.PadRight(pad));
                    }
                    Console.WriteLine();
                    Console.WriteLine("====================");
                    while(reader.Read()) {
                        for(int i = 0 ; i < reader.FieldCount; i++) {
                            int pad = i > 2 ? 15 : i == 2 ? 30 : 4;
                            string value = reader.GetValue(i).ToString();
                            if (value.Length > pad) value = value.Substring(0, pad-1);
                            Console.Write(value.PadRight(pad));
                        }
                        Console.WriteLine();
                    }
                    Console.ReadLine();
                }
            }
        }

        private static void ShowError(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
