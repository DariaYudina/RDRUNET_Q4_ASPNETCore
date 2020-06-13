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
    public class TourDao : ITourDao
    {
        private readonly string connectionString;

        public TourDao(string connectionString)
        {
            this.connectionString = connectionString;
        }
        ///assas
        public IEnumerable<Tour> GetTours()
        {
            var query = @"SELECT
                t.[Tour_Id], 
                t.[Cost],
                t.[StartDate],
                t.[EndDate],
                t.[Image],
                c.[City_Id],
                c.[Region_Id],
                c.[Title],
                r.Region_Id,
                r.[Country_Id],
                r.[Title],
                co.[Country_Id],
                co.[Title]
                FROM Tours as t
                JOIN Cities AS c on t.City_Id = c.[City_Id]
                JOIN Regions AS r on c.Region_Id = r.Region_Id
                JOIN Countries AS co on r.Country_Id = co.Country_Id";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var result = connection.Query<Tour, City, Region, Country, Tour>(query,
                    (t, c, r, co) =>
                    {
                        t.City = c;
                        t.Region = r;
                        t.Country = co;
                        return t;
                    }, splitOn: "City_Id, Region_Id, Country_Id");

                return result;
            }
        }

        public Tour GetTourById(int id)
        {
            var query = "SELECT * FROM Tours WHERE Tour_Id = @id";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Tour>(query, new { id }).FirstOrDefault();
            }
        }
    }
}
