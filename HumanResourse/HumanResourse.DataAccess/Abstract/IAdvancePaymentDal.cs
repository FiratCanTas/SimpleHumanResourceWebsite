
using HumanResourse.DataAccess.Abstract;
using HumanResourse.Entitiy.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourse.DataAccess.Abstract
{

    public interface IAdvancePaymentDal:IGenericDAL<AdvancePayment>
    {
        List<AdvancePayment> GetListByUserName();
    }
}
