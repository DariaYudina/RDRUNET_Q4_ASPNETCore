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
    public class CityDao : ICityDao
    {
        private readonly string connectionString;

        public CityDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<City> GetCities()
        {
           var query = @"SELECT 
                [City_Id],
                [Area_Id],
                [Title]
                FROM Cities";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<City>(query);
            }
        }

        public City GetCityById(int id)
        {
            var query = @"SELECT 
                [City_Id],
                [Region_Id],
                [Title] 
                FROM Cities WHERE City_Id = @id";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<City>(query, new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<City> GetCityiesByAreaId(int id)
        {
             if(id != 0)
            {
                var query = @"SELECT 
                [City_Id],
                [Area_Id],
                [Title] FROM Cities WHERE Area_Id = @id";
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    return connection.Query<City>(query, new { id });
                }
            }

            return GetCities();
        }
    }
}
