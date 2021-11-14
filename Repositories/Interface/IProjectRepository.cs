using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IB_Group.Models;
using IB_Group.Repositories.Interface;

namespace IB_Group_Demo.Repositories
{
    public interface IProjectRepository : IRepository<ProjectType>
    {
        //Task<List<ProjectType>> GetAllAsync();
        //Task<ProjectType> GetByIdAsync(int id);
        //Task<int> CreateAsync(ProjectType entity);
        //Task<int> UpdateAsync(ProjectType entity);
        //Task<int> DeleteAsync(ProjectType entity);
    }
}
