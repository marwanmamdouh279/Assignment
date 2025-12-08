using Assignment_DataAccesslayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_BussinesLogicLayer.Interfaces
{
    public interface IDepartmentReposatory :IGenericReposatory<Department>
    {
        List<Department> GetByName(string name);
        //IEnumerable<Department> GetAll();
        //Department? Get(int id);
        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);
    }
}
