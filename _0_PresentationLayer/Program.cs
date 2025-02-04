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
builder.Services.AddEndpointsApiExplorer(); // API'nin t�m endpointlerini kesfetmek i�in
builder.Services.AddSwaggerGen(); // Swagger UI ve dok�mantasyonunu olusturmak i�in


var app = builder.Build();

if (app.Environment.IsDevelopment())  // Yaln�zca gelistirme ortam?nda aktif etmek isteyebilirsiniz
{
    app.UseSwagger(); // Swagger JSON d�k�mantasyonunu aktif eder
    app.UseSwaggerUI(); // Swagger UI'yi aktif eder
}


app.MapControllers();


app.Run();
