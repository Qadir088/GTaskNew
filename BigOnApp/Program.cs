using BigOnApp.DAL.context;
using BigOnApp.Helpers.Services.Implementations;
using BigOnApp.Helpers.Services.Interfaces;
using BigOnApp.Models.EmailOptions;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.Configure<EmailOption>(cfg =>
{
    builder.Configuration.GetSection("AccountConfirm").Bind(cfg);
});
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IDateTimeService, UtcDateTimaService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

var app = builder.Build();
app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);
app.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.UseStaticFiles();
app.Run();