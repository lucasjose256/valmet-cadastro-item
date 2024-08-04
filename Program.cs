using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using valmet_cadastro_item.Helper;
using valmet_cadastro_item.Models;
using valmet_cadastro_item.Repositories;
using valmet_cadastro_item.smtp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataBase>(options => {

    var connetionString = builder.Configuration.GetConnectionString("DefaultConnetion");


    options.UseSqlServer(connetionString);
});
// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ISessionUser, SessionUser>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddSession(o => {
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
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

app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Method == "POST" &&
        context.Request.HasFormContentType &&
        context.Request.Form["_method"] == "PUT")
    {
        context.Request.Method = "PUT";
    }
    await next();
});

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
