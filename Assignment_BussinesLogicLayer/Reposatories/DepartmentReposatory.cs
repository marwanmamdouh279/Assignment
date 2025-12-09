using Assignment_BussinesLogicLayer.Interfaces;
using Assignment_DataAccesslayer.Data.Contexts;
using Assignment_DataAccesslayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_BussinesLogicLayer.Reposatories
{
    public class DepartmentReposatory : GenericReposatory<Department>, IDepartmentReposatory
    {
        private readonly AssignmentContext _context;

        public DepartmentReposatory(AssignmentContext context):base(context)  //ask CLR to create Obj from AssignmentContext
        {
            _context = context;
        }

        public async Task< List<Department>> GetByNameAsync(string name)
        {
            return await _context.Departments.Where(e => e.Name.ToLower() .Contains( name.ToLower())).ToListAsync();

        }
    }
}
