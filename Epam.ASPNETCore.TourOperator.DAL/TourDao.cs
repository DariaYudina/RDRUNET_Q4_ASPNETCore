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
        ///assasss
        public IEnumerable<Tour> GetTours()
        {
            var query = @"SELECT
                t.[Tour_Id], 
                t.[Cost],
                t.[StartDate],
                t.[EndDate],
                t.[Image],

                c.[City_Id],
                c.[Area_Id],
                c.[Title],

                a.[Area_Id],
                a.[Region_Id],
                a.[Title],

                r.[Region_Id],
                r.[Country_Id],
                r.[Title],

                co.[Country_Id],
                co.[Title]

                FROM Tours as t
                JOIN Cities AS c on t.City_Id = c.[City_Id]
                JOIN Areas AS a on c.Area_Id = a.Area_Id
                JOIN Regions AS r on a.Region_Id = r.Region_Id
                JOIN Countries AS co on r.Country_Id = co.Country_Id";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var result = connection.Query<Tour, City, Area, Region, Country, Tour>(query,
                    (t, c, a, r, co) =>
                    {
                        t.City = c;
                        t.Area = a;
                        t.Region = r;
                        t.Country = co;
                        return t;
                    },
                    splitOn: "City_Id, Area_Id, Region_Id, Country_Id");

                return result;
            }
        }

        public Tour GetTourById(int id)
        {
            var query = @"SELECT
                t.[Tour_Id], 
                t.[Cost],
                t.[StartDate],
                t.[EndDate],
                t.[Image],

                c.[City_Id],
                c.[Area_Id],
                c.[Title],

                a.[Area_Id],
                a.[Region_Id],
                a.[Title],

                r.[Region_Id],
                r.[Country_Id],
                r.[Title],

                co.[Country_Id],
                co.[Title]

                FROM Tours as t
                JOIN Cities AS c on t.City_Id = c.[City_Id]
                JOIN Areas AS a on c.Area_Id = a.Area_Id
                JOIN Regions AS r on a.Region_Id = r.Region_Id
                JOIN Countries AS co on r.Country_Id = co.Country_Id
                WHERE t.[Tour_Id] = @id";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var result = connection.Query<Tour, City, Area, Region, Country, Tour>(query,
                    (t, c, a, r, co) =>
                    {
                        t.City = c;
                        t.Area = a;
                        t.Region = r;
                        t.Country = co;
                        return t;
                    },
                    new {id},
                    splitOn: "City_Id, Area_Id, Region_Id, Country_Id").FirstOrDefault();

                return result;
            }
        }

        public IEnumerable<Tour> GetToursByCountryId(int id)
        {
            var query = @"SELECT
                t.[Tour_Id], 
                t.[Cost],
                t.[StartDate],
                t.[EndDate],
                t.[Image],

                c.[City_Id],
                c.[Area_Id],
                c.[Title],

                a.[Area_Id],
                a.[Region_Id],
                a.[Title],

                r.[Region_Id],
                r.[Country_Id],
                r.[Title],

                co.[Country_Id],
                co.[Title]

                FROM Tours as t
                JOIN Cities AS c on t.City_Id = c.[City_Id]
                JOIN Areas AS a on c.Area_Id = a.Area_Id
                JOIN Regions AS r on a.Region_Id = r.Region_Id
                JOIN Countries AS co on r.Country_Id = co.Country_Id
                WHERE r.[Country_Id] = @id";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var result = connection.Query<Tour, City, Area, Region, Country, Tour>(query,
                    (t, c, a, r, co) =>
                    {
                        t.City = c;
                        t.Area = a;
                        t.Region = r;
                        t.Country = co;
                        return t;
                    },
                    new { id },
                    splitOn: "City_Id, Area_Id, Region_Id, Country_Id");

                return result;
            }
        }

        public IEnumerable<Tour> GetToursBySearchParametrs(int? countryId, int? regionId, int? areaId, int? cityId,
            decimal? startCost, decimal? endCost, DateTime startDate, int dateCount)
        {
            var query = @"SELECT
                t.[Tour_Id], 
                t.[Cost],
                t.[StartDate],
                t.[EndDate],
                t.[Image],

                c.[City_Id],
                c.[Area_Id],
                c.[Title],

                a.[Area_Id],
                a.[Region_Id],
                a.[Title],

                r.[Region_Id],
                r.[Country_Id],
                r.[Title],

                co.[Country_Id],
                co.[Title]

                FROM Tours as t
                JOIN Cities AS c on t.City_Id = c.[City_Id]
                JOIN Areas AS a on c.Area_Id = a.Area_Id
                JOIN Regions AS r on a.Region_Id = r.Region_Id
                JOIN Countries AS co on r.Country_Id = co.Country_Id

                WHERE co.[Country_Id] = ISNULL(@countryId, co.[Country_Id])
                AND r.[Region_Id] =  ISNULL(@regionId, r.[Region_Id])
                AND a.[Area_Id] = ISNULL(@areaId, a.[Area_Id])
                AND c.[City_Id] = ISNULL(@cityId, c.[City_Id])";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var result = connection.Query<Tour, City, Area, Region, Country, Tour>(query,
                    (t, c, a, r, co) =>
                    {
                        t.City = c;
                        t.Area = a;
                        t.Region = r;
                        t.Country = co;
                        return t;
                    },
                    new 
                    {
                        cityId,
                        areaId,
                        regionId,
                        countryId,
                    },
                    splitOn: "City_Id, Area_Id, Region_Id, Country_Id");

                return result;
            }
        }
    }
}
