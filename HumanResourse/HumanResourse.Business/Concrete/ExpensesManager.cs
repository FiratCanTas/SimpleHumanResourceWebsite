using HumanResourse.Business.Abstract;
using HumanResourse.DataAccess.Abstract;
using HumanResourse.Entitiy.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourse.Business.Concrete
{
    public class ExpensesManager : IExpensesService
    {
       private readonly IExpensesDal _expensesDal;

        public ExpensesManager(IExpensesDal expensesDal)
        {
            _expensesDal = expensesDal;
        }

        public void TAdd(Expenses t)
        {
            _expensesDal.Add(t);
        }

        public void TDelete(Expenses t)
        {
           _expensesDal.Delete(t);
        }

        public Expenses TGetByID(int id)
        {
            return _expensesDal.GetByID(id);
        }

        public List<Expenses> TGetList()
        {
           return _expensesDal.GetList();
        }

        public List<Expenses> TGetListByUserName()
        {
            return _expensesDal.GetListByUserName();
        }

        public void TUpdate(Expenses t)
        {
           _expensesDal.Update(t);
        }
    }
}
