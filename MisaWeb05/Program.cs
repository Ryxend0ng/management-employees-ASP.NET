
using Microsoft.Azure.Management.AppService.Fluent.Models;
using Microsoft.OpenApi.Models;
using MisaWeb05.Core.Interface.Repository;
using MisaWeb05.Core.Interface.Services;
using MisaWeb05.Core.Services.Impl;
using MisaWeb05.Infastructure.Repositories.Impl;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentSevice>();
builder.Services.AddScoped<IEmployeesService, EmployeeService>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped(typeof(IGenaralRepository<>), typeof( BaseRepository<>));
builder.Services.AddScoped(typeof(IGeneralService<>), typeof(BaseService<>));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
 {

     var filePath = Path.Combine(AppContext.BaseDirectory, "MisaWeb05.xml");
     c.IncludeXmlComments(filePath);
 });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(MyAllowSpecificOrigins);

}
app.UseCors(builder =>
  builder
    .WithExposedHeaders("Content-Disposition"));
app.UseAuthorization();

app.MapControllers();

app.Run();
