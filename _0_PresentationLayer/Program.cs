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
using _1_BusinessLayer.Concrete.Services.MainServices;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Services.SideServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.CreateOptions(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>();


//autowiring repositories
builder.Services.AddScoped<AbstractEntryRepository, EntryRepository>();
builder.Services.AddScoped<AbstractFollowRepository, FollowRepository>();
builder.Services.AddScoped<AbstractLikeRepository, LikeRepository>();
builder.Services.AddScoped<AbstractPostRepository, PostRepository>();
builder.Services.AddScoped<AbstractUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<AbstractUserRepository, UserRepository>();


//autowiring side services
builder.Services.AddScoped<AbstractMailManager, MailManager>();
builder.Services.AddScoped<AbstractAuthenticationManager, AuthenticationManager>();
//autowiring main services
builder.Services.AddScoped<AbstractEntryService, EntryService>();
builder.Services.AddScoped<AbstractPostService, PostService>();
builder.Services.AddScoped<AbstractUserService, UserService>();
//autowiring of IdentityTools
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<UserManager<User>>();




//adding Identity on project
builder.Services.AddIdentity<User, UserRole>(options =>
{
    // Password policy
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    // Lockout policy
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User config
    options.User.RequireUniqueEmail = true;

   
})
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();



//adding Jwt Authentication on project
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
        ValidIssuers = builder.Configuration.GetSection("Jwt:ValidIssuers").Get<string[]>(),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

    jwtOptions.MapInboundClaims = false;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("StandardUser","Admin"));
    
    options.AddPolicy("GuestPolicy", policy => policy.RequireRole("Guest","StandardUser","Admin"));

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

app.UseStaticFiles();
app.MapControllers();


app.Run();
