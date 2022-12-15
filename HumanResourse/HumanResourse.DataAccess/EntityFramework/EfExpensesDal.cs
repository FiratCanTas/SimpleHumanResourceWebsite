using HumanResourse.DataAccess.Abstract;
using HumanResourse.DataAccess.Context;
using HumanResourse.DataAccess.Repository;
using HumanResourse.Entitiy.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourse.DataAccess.EntityFramework
{
    public class EfExpensesDal : GenericRepository<Expenses>, IExpensesDal
    {
        private readonly HumanResoursesContext _humanResoursesContext;

        public EfExpensesDal(HumanResoursesContext context, HumanResoursesContext humanResoursesContext) : base(context)
        {
            _humanResoursesContext = humanResoursesContext;
        }

        public List<Expenses> GetListByUserName()
        {

            var values = _humanResoursesContext.Expenses.Include("AppUsers").ToList();
            return values;

        }
    }
}
