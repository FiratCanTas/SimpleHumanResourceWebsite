using HumanResourse.Business.Abstract;
using HumanResourse.Business.Concrete;
using HumanResourse.DataAccess.Abstract;
using HumanResourse.DataAccess.EntityFramework;
using HumanResourse.DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using HumanResourse.Business.ValidationRules;

namespace HumanResourse.Business.DIContainer
{
    public static class ServiceRegistration
    {
     
        public  static void AddPersistenceService(this IServiceCollection services)
        {
            //services.AddDbContext<HumanResoursesContext>();

            services.AddDbContext<HumanResoursesContext>(options => options.UseSqlServer("Server=tcp:bilgeadamhr.database.windows.net,1433;Initial Catalog=BilgeAdamHrDb;Persist Security Info=False;User ID=firatcantas;Password=f3Rat.123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

            services.AddScoped<IPermitService, PermitManager>();
            services.AddScoped<IAdvancePaymentService, AdvancePaymentManager>();
            services.AddScoped<IExpensesService, ExpensesManager>();

            services.AddScoped<IExpensesDal, EfExpensesDal>();
            services.AddScoped<IPermitDal, EfPermitDal>();
            services.AddScoped<IAdvancePaymentDal, EFAdvancePaymentDal>();

         
        }

    }
}
