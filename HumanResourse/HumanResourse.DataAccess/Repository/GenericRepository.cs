using HumanResourse.DataAccess.Abstract;
using HumanResourse.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourse.DataAccess.Repository
{
    public class GenericRepository<T> : IGenericDAL<T> where T : class
    {
        private readonly HumanResoursesContext _context;

        public GenericRepository(HumanResoursesContext context)
        {
            _context = context;
        }

        public void Add(T t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public void Delete(T t)
        {
            _context.SaveChanges();
        }

        public T GetByID(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            return _context.Set<T>().ToList();

        }

        public void Update(T t)
        {
            _context.Entry<T>(t).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
