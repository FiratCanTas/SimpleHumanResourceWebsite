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
    public class EFAdvancePaymentDal : GenericRepository<AdvancePayment>, IAdvancePaymentDal
    {
        private readonly HumanResoursesContext _humanResoursesContext;
        public EFAdvancePaymentDal(HumanResoursesContext context, HumanResoursesContext humanResoursesContext) : base(context)
        {
            _humanResoursesContext = humanResoursesContext;
        }

        public List<AdvancePayment> GetListByUserName()
        {
            var values = _humanResoursesContext.AdvancePayments.Include("AppUsers").ToList();
            return values;
        }
    }
}
