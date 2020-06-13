using Dapper;
using Epam.ASPNETCore.TourOperator.Entities;
using Epam.ASPNETCore.TourOperator.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Epam.ASPNETCore.TourOperator.DAL
{
    public class CountryDao : ICountryDao
    {
        private readonly string connectionString;

        public CountryDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<Country> GetCountries()
        {
            var query = "SELECT " +
                "[Country_Id]," +
                "[Title]"+
                "FROM Countries";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Country>(query);
            }
        }

        public Country GetCountryById(int id)
        {
            var query = "SELECT * FROM Countries WHERE Country_Id = @id";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Country>(query, new { id }).FirstOrDefault();
            }
        }
    }
}
