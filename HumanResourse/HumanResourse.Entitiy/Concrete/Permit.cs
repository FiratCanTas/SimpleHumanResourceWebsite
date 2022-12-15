using HumanResourse.Entitiy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourse.Entitiy.Concrete
{
    public class Permit
    {
        public int PermitID { get; set; }
        public PermitTypee permitType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? ReponseDate { get; set; }
        public int PermitDay { get; set; }
        public Position Statu { get; set; } = Position.Beklemede;

        public int AppUserID { get; set; }
        public AppUser AppUsers { get; set; }
    }
}
