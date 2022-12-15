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
    public class AdvancePaymentManager : IAdvancePaymentService
    {
       private readonly IAdvancePaymentDal _advancePaymentDal;

        public AdvancePaymentManager(IAdvancePaymentDal advancePaymentDal)
        {
            _advancePaymentDal = advancePaymentDal;
        }

        public void TAdd(AdvancePayment t)
        {
            _advancePaymentDal.Add(t);
        }

        public void TDelete(AdvancePayment t)
        {
            _advancePaymentDal.Delete(t);
        }

        public AdvancePayment TGetByID(int id)
        {
            return _advancePaymentDal.GetByID(id);
        }

        public List<AdvancePayment> TGetList()
        {
            return _advancePaymentDal.GetList();
        }

        public List<AdvancePayment> TGetListByUserName()
        {
            return _advancePaymentDal.GetListByUserName(); 
        }

        public void TUpdate(AdvancePayment t)
        {
            _advancePaymentDal.Update(t);
        }
    }
}
