using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyFin.v2.Models.database;
using MyFin.v2.Models.Entities.database;
using MyFin.v2.Models.Entities.repo;
using MyFin.v2.Models.Entities.repo.ifaces;
using MyFin.v2.Models.services;
using MyFin.v2.Models.services.ifaces;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
#region Identity&&Context
builder.Services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DockerIdentity")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(i => i.Password.RequiredUniqueChars = 0).AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddDbContext<FinContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DockerFinance")));
#endregion


#region Repository
builder.Services.AddTransient<ICreditRepo, CreditRepo>();
builder.Services.AddTransient<IDepositoryRepo, DepositoryRepo>();
builder.Services.AddTransient<IOperationRepo, OperationRepo>();
#endregion

#region Service
builder.Services.AddScoped<DbTransaction>();
builder.Services.AddScoped<ICreditService, CreditService>();
builder.Services.AddScoped<IDepositoryService, DepositoryService>();
builder.Services.AddScoped<IChartsService, ChartsService>();
builder.Services.AddScoped<IAccountService, AccountService>();
#endregion


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

builder.Services.AddControllersWithViews();


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
