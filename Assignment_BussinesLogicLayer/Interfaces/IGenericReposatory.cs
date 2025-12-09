using Assignment_DataAccesslayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_BussinesLogicLayer.Interfaces
{
    public interface IGenericReposatory<T> where T : BaseEntity
    {
       Task< IEnumerable<T>> GetAllAsync();
       Task< T?> GetAsync(int id);
        int Add(T model);
        int Update(T model);
        int Delete(T model);

        
    }
}
