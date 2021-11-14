using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using IB_Group.Models;
using IB_Group.Repositories.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
namespace IB_Group_Demo.Repositories
{
    
    public class ProjectRepository : DapperRequest, IProjectRepository
    {
        public ProjectRepository(IConfiguration configuration)
            : base(configuration)
        { }
         
        public async Task<List<ProjectType>> GetAllAsync()
        {
            try
            {
                var procedure = "spGetAllProjectType";
                using (var connection = CreateConnection())
                {
                    List<ProjectType> projectType = (List<ProjectType>)await connection.QueryAsync<ProjectType>(procedure);
                    return projectType.OrderBy(m => m.DisplayOrder).ToList();
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<ProjectType> GetByIdAsync(int id)
        {
            try
            {
                var query = "SELECT * FROM ProjectType WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<ProjectType>(query, parameters));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> CreateAsync(ProjectType entity)
        {
            try
            {
                var query = "INSERT INTO ProjectType (ProjectName, ImagePath,IsActive, DisplayOrder) VALUES (@ProjectName, @ImagePath,@IsActive, @DisplayOrder)";

                var parameters = new DynamicParameters();
                parameters.Add("ProjectName", entity.ProjectName, DbType.String);
                parameters.Add("ImagePath", entity.ImagePath, DbType.String);
                parameters.Add("IsActive", entity.IsActive, DbType.Boolean);
                parameters.Add("DisplayOrder", entity.DisplayOrder, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> UpdateAsync(ProjectType entity)
        {
            try
            {
                var query = "UPDATE ProjectType SET ProjectName = @ProjectName, ImagePath = @ImagePath, DisplayOrder = @DisplayOrder WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("ProjectName", entity.ProjectName, DbType.String);
                parameters.Add("ImagePath", entity.ImagePath, DbType.String);
                parameters.Add("DisplayOrder", entity.DisplayOrder, DbType.Int64);
                parameters.Add("Id", entity.Id, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> DeleteAsync(ProjectType entity)
        {
            try
            {
                var query = "DELETE FROM ProjectType WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", entity.Id, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
