using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Countries
{
    public static class AdoNetHelper
    {
        public static List<Country> LoadCountries()
        {
            string dataProvider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

            List<Country> countries = new List<Country>();
            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null) return null;
                connection.ConnectionString = connectionString;
                connection.Open();

                DbCommand command = factory.CreateCommand();
                if (command == null) return null;
                
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Countries";
                
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string code = reader.GetString(reader.GetOrdinal("countryCode"));
                        string name = reader.GetString(reader.GetOrdinal("countryName"));
                        string currency = reader.GetString(reader.GetOrdinal("currencyCode"));
                        string capital = reader.GetString(reader.GetOrdinal("capital"));
                        string continent = reader.GetString(reader.GetOrdinal("continentName"));

                        List<string> languages = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("languages")))
                            languages = reader.GetString(reader.GetOrdinal("languages")).Split(',').ToList();
                        
                        countries.Add(new Country(id, code) {Name = name, Currency = currency, 
                            Capital = capital, Continent = continent, Languages = languages});
                    }
                }
            }
            return countries;
        }

        public static void AddCountry(Country country)
        {
            string cmdText = $"INSERT INTO Countries (countryCode, countryName, currencyCode, capital, continentName) " +
                        $" VALUES (@Code, @Name, @Currency, @Capital, @Continent)";
            ExecuteCommand(country, cmdText);
        }

        public static void UpdateCountry(Country country)
        {
            string cmdText = "UPDATE Countries SET countryName = @Name, currencyCode = @Currency, " +
                             $" capital = @Capital, continentName = @Continent WHERE countryCode = @Code" ;
            ExecuteCommand(country, cmdText);
        }

        private static void ExecuteCommand(Country country, string cmdText)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand {Connection = connection};

                SqlParameter code = new SqlParameter("@Code", SqlDbType.Char);
                SqlParameter name = new SqlParameter("@Name", SqlDbType.VarChar);
                SqlParameter currency = new SqlParameter("@Currency", SqlDbType.Char);
                SqlParameter capital = new SqlParameter("@Capital", SqlDbType.VarChar);
                SqlParameter continent = new SqlParameter("@Continent", SqlDbType.Char);

                cmd.Parameters.Add(code).Value = country.Code;
                cmd.Parameters.Add(name).Value = country.Name;
                cmd.Parameters.Add(currency).Value = (object) country.Currency ?? DBNull.Value;
                cmd.Parameters.Add(capital).Value = (object) country.Capital ?? DBNull.Value;
                cmd.Parameters.Add(continent).Value = country.Continent;
                
                cmd.CommandText = cmdText;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void DeleteCountry(string code)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand {Connection = connection};
                SqlParameter codeParam = new SqlParameter("@Code", SqlDbType.Char);
                cmd.Parameters.Add(codeParam).Value = code;
                cmd.CommandText = "DELETE FROM Countries WHERE countryCode = @Code";
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
