using Assignment_DataAccesslayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_BussinesLogicLayer.Interfaces
{
    public interface IEmployeeReposatory:IGenericReposatory<Employee>
    {
        List<Employee> GetByName(string name);
        // IEnumerable<Employee> GetAll();
        //Employee? Get(int id);
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);

    }
}
