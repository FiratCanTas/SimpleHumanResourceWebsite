
using HumanResourse.Business.Abstract;
using HumanResourse.Entitiy.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourse.Business.Abstract
{
    public interface IAdvancePaymentService:IGenericService<AdvancePayment>
    {
        List<AdvancePayment> TGetListByUserName();
    }
}
