using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using ProjectAccessibility.Context;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GebruikerContext>(options =>{
    options.UseNpgsql("User Id=postgres;Password=GaomsRWOhc9Vr38J;Server=db.tezmskemtfwvdzusobph.supabase.co;Port=5432;Database=postgres");
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();