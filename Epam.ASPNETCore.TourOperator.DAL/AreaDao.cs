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
    public class AreaDao : IAreaDao
    {
        private readonly string connectionString;

        public AreaDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Area GetAreaById(int id)
        {
            var query = @"SELECT 
                [Area_Id],
                [Region_Id],
                [Title] 
                FROM Areas WHERE Area_Id = @id";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Area>(query, new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<Area> GetAreas()
        {
            var query = @"SELECT 
                [Area_Id],
                [Region_Id],
                [Title]
                FROM Areas";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Area>(query);
            }
        }

        public IEnumerable<Area> GetAreasByRegionId(int id)
        {
            if(id != 0)
            {
                var query = @"SELECT 
                [Area_Id],
                [Region_Id],
                [Title] FROM Areas WHERE Region_Id = @id";
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    return connection.Query<Area>(query, new { id });
                }
            }

            return GetAreas();
        }
    }
}
