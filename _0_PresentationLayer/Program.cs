using _1_BusinessLayer.Abstractions;
using _1_BusinessLayer.Concrete.Services;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Extensions;
using _2_DataAccessLayer.Concrete.Repositories;
using _2_DataAccessLayer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.CreateOptions(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<AbstractUserRepository, UserRepository>();
builder.Services.AddScoped<AbstractUserService, UserService>();
builder.Services.AddControllers().AddJsonOptions(jsonOptions => 
jsonOptions.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddEndpointsApiExplorer(); // API'nin tüm endpointlerini kesfetmek için
builder.Services.AddSwaggerGen(); // Swagger UI ve dokümantasyonunu olusturmak için


var app = builder.Build();

if (app.Environment.IsDevelopment())  // Yalnýzca gelistirme ortam?nda aktif etmek isteyebilirsiniz
{
    app.UseSwagger(); // Swagger JSON dökümantasyonunu aktif eder
    app.UseSwaggerUI(); // Swagger UI'yi aktif eder
}


app.MapControllers();


app.Run();
