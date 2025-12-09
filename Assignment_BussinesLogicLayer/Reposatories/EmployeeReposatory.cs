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
    public class EmployeeReposatory : GenericReposatory<Employee>, IEmployeeReposatory
    {
        private readonly AssignmentContext _context;

        public EmployeeReposatory(AssignmentContext context) :base(context)
        {
            _context = context;
        } //ask CLR to create Oj from AssignmentContext

        public async Task< List<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees.Include(D=>D.Department).Where(e=>e.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
