using _1_BusinessLayer.Concrete.Services;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Extensions;
using _2_DataAccessLayer.Concrete.Repositories;
using _2_DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using _1_BusinessLayer.Abstractions.MainServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.CreateOptions(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>();

//autowire for repositories to use in services
builder.Services.AddScoped<AbstractEntryRepository,EntryRepository>();
builder.Services.AddScoped<AbstractFollowRepository, FollowRepository>();
builder.Services.AddScoped<AbstractLikeRepository, LikeRepository>();
builder.Services.AddScoped<AbstractPostRepository, PostRepository>();
builder.Services.AddScoped<AbstractUserRepository, UserRepository>();
builder.Services.AddScoped<AbstractUserRoleRepository, UserRoleRepository>();

//autowire for services to use in controller
builder.Services.AddScoped<AbstractPostService, UserManager>();
builder.Services.AddScoped<AbstractUserService, UserService>();

builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();


builder.Services.AddAuthentication()
.AddJwtBearer("main-scheme", jwtOptions =>
{
    jwtOptions.Authority = builder.Configuration["Jwt:Authority"];
    jwtOptions.Audience = builder.Configuration["Jwt:Audience"];
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidAudiences = builder.Configuration.GetSection("Jwt:ValidAudiences").Get<string[]>(),
        ValidIssuers = builder.Configuration.GetSection("Jwt:ValidIssuers").Get<string[]>()
    };

    jwtOptions.MapInboundClaims = false;
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // API'nin tüm endpointlerini ke?fetmek için
builder.Services.AddSwaggerGen(); // Swagger UI ve dokümantasyonunu olu?turmak için


var app = builder.Build();

if (app.Environment.IsDevelopment())  // Yaln?zca geli?tirme ortam?nda aktif etmek isteyebilirsiniz
{
    app.UseSwagger(); // Swagger JSON dökümantasyonunu aktif eder
    app.UseSwaggerUI(); // Swagger UI'yi aktif eder
}


app.MapControllers();


app.Run();
