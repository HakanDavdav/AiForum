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
builder.Services.AddScoped<AbstractMailService, MailService>();
builder.Services.AddScoped<AbstractAuthenticationService, AuthenticationService>();
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
    // Parola gereksinimleri
    options.Password.RequireDigit = true; // En az bir rakam gerektirir
    options.Password.RequireLowercase = true; // En az bir küçük harf gerektirir
    options.Password.RequireUppercase = true; // En az bir büyük harf gerektirir
    options.Password.RequiredLength = 8; // Parolanýn minimum uzunluðu
    options.Password.RequireNonAlphanumeric = false; // Özel karakter gereksinimi

    // Kullanýcý adý gereksinimleri
    options.User.RequireUniqueEmail = true; // E-posta adresi benzersiz olmalýdýr

    // Kilitleme (Lockout) seçenekleri
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Kilitlenme süresi
    options.Lockout.MaxFailedAccessAttempts = 5; // Maksimum baþarýsýz giriþ sayýsý
    options.Lockout.AllowedForNewUsers = true; // Yeni kullanýcýlar için kilitleme özelliði aktif mi?

    // Hesap doðrulama (Email ve Phone)
    options.SignIn.RequireConfirmedEmail = true; // E-posta doðrulamasý gereksinimi
    options.SignIn.RequireConfirmedPhoneNumber = false; // Telefon numarasý doðrulamasý gereksinimi
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
