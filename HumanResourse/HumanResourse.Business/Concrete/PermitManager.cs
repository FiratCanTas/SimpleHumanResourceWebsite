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
    public class PermitManager : IPermitService
    {
        IPermitDal _permitDal;

        public PermitManager(IPermitDal permitDal)
        {
            _permitDal = permitDal;
        }

        public void TAdd(Permit t)
        {
            _permitDal.Add(t);
        }

        public void TDelete(Permit t)
        {
            _permitDal.Delete(t);
        }

        public Permit TGetByID(int id)
        {
            return _permitDal.GetByID(id);

        }

        public List<Permit> TGetList()
        {
            return _permitDal.GetList();
        }

        public List<Permit> TGetListByUserName()
        {
            return _permitDal.GetListByUserName();
        }

        public void TUpdate(Permit t)
        {
            _permitDal.Update(t);
        }
    }
}
