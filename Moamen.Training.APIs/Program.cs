using Microsoft.EntityFrameworkCore;
using Moamen.Training.APIs.Data;
using AutoMapper;
using Moamen.Training.APIs.Services;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(op =>
{
    op.ReturnHttpNotAcceptable = true;
    op.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
    op.InputFormatters.Add(new XmlDataContractSerializerInputFormatter(op));
    //op.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter());
}).AddNewtonsoftJson();

builder.Services.AddDbContext<DataContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async ctxt =>
        {
            ctxt.Response.StatusCode = 500;
            await ctxt.Response.WriteAsync("An unhandled error occured!");
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
