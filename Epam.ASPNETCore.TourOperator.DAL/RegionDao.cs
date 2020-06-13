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
    public class RegionDao : IRegionDao
    {
        private readonly string connectionString;

        public RegionDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Region GetRegionById(int id)
        {
            var query = @"SELECT 
                [Region_Id], 
                [Country_Id],
                [Title]
                FROM Regions WHERE Region_Id = @id";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Region>(query, new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<Region> GetRegions()
        {
            var query = "SELECT " +
                "[Region_Id]," +
                "[Country_Id]," +
                "[Title]"+
                "FROM Regions";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Region>(query);
            }
        }

        public IEnumerable<Region> GetRegionsByCountryId(int id)
        {
            var query = @"SELECT 
                [Region_Id], 
                [Country_Id],
                [Title]
                FROM Regions WHERE Country_Id = @id";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Region>(query, new { id });
            }
        }
    }
}
