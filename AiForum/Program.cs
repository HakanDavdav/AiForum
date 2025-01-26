using _1_BusinessLayer.Abstractions;
using _1_BusinessLayer.Concrete.Services;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Extensions;
using _2_DataAccessLayer.Concrete.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.CreateOptions(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // API'nin t�m endpointlerini ke?fetmek i�in
builder.Services.AddSwaggerGen(); // Swagger UI ve dok�mantasyonunu olu?turmak i�in

var app = builder.Build();

if (app.Environment.IsDevelopment())  // Yaln?zca geli?tirme ortam?nda aktif etmek isteyebilirsiniz
{
    app.UseSwagger(); // Swagger JSON d�k�mantasyonunu aktif eder
    app.UseSwaggerUI(); // Swagger UI'yi aktif eder
}


app.MapControllers();


app.Run();
