using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IB_Group.Models
{
    public class ProjectType: BaseEntity
    {
        public int? Id { get; set; }
        public string ProjectName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public int? DisplayOrder { get; set; }
    }

}
