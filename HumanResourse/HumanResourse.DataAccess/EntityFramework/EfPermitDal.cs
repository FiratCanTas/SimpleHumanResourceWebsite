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
    public class EfPermitDal : GenericRepository<Permit>, IPermitDal
    {
        private readonly HumanResoursesContext _humanResoursesContext;
        public EfPermitDal(HumanResoursesContext context, HumanResoursesContext humanResoursesContext) : base(context)
        {
            _humanResoursesContext = humanResoursesContext;
        }

        public List<Permit> GetListByUserName()
        {
            var values = _humanResoursesContext.Permits.Include("AppUsers").ToList();
            return values;

        }
    }
}
