using Assignment_BussinesLogicLayer.Interfaces;
using Assignment_DataAccesslayer.Data.Contexts;
using Assignment_DataAccesslayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_BussinesLogicLayer.Reposatories
{
    public class GenericReposatory<T> : IGenericReposatory<T> where T:BaseEntity
    {
        //cant assign ne value from _context
        private readonly AssignmentContext _context;  //null

        //a clr to create object from AssignmentContext
        public GenericReposatory(AssignmentContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            if(typeof(T)==typeof(Employee))
            {
                return (IEnumerable<T>) _context.Employees.Include(E => E.Department).ToList();
            }
            return _context.Set<T>().ToList();
        }
        public T? Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return _context.Employees.Include(E => E.Department).FirstOrDefault(E=>E.Id==id) as T ;
            }
            return _context.Set<T>().Find(id);
        }
        public int Add(T model)
        {
            _context.Set<T>().Add(model);
            return _context.SaveChanges();
        }
        public int Update(T model)
        {
            _context.Set<T>().Update(model);
            return _context.SaveChanges();
        }
        public int Delete(T model)
        {
            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }

      
    }
}
