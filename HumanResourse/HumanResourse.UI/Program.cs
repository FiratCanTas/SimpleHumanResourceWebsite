using HumanResourse.DataAccess.Context;
using HumanResourse.Entitiy.Concrete;
using HumanResourse.Business.DIContainer;
using HumanResourse.UI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using FluentValidation.AspNetCore;
using HumanResourse.Business.ValidationRules;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceService();

// Add services to the container.


//builder.Services.AddControllers().AddFluentValidation(f =>
//{

//    f.RegisterValidatorsFromAssemblyContaining<SignInValidator>();
//});

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{ //identity options lar� verebilece�imiz bir overload yap�s� mevcut. Biz default ayalar� kullanmak istemeyip yap�land�rma yapmak istiyorsak belirtebiliriz. 

    opt.Password.RequireDigit = false;              //�ifremiz say� i�ermesine gerek yok
    opt.Password.RequiredLength = 1;               //gerekli uzunluk 1 e �ektik gibi d�zenlemeler yap�labilir.
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.SignIn.RequireConfirmedEmail = false;       //Default olarak false gelir. SignInResult "IsNotAllowed" �zelli�i dir . Mail onaylamas�.
    opt.Lockout.MaxFailedAccessAttempts = 10;       //4 hatal� denemeden sonra hesab� kitle
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //Hesab� 5 dakika boyunca kitle dedik. 
    opt.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCÇDEFGHIJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";


}).AddEntityFrameworkStores<HumanResoursesContext>().AddDefaultTokenProviders();

//builder.Services.AddMvc(config =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//                            .RequireAuthenticatedUser()
//                            .Build();
//    config.Filters.Add(new AuthorizeFilter(policy));
//});

builder.Services.AddDbContext<HumanResoursesContext>(opt =>
{
    opt.UseSqlServer("Server=tcp:bilgeadamhr.database.windows.net,1433;Initial Catalog=BilgeAdamHrDb;Persist Security Info=False;User ID=firatcantas;Password=f3Rat.123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
