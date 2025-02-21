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
    options.Password.RequireLowercase = true; // En az bir k���k harf gerektirir
    options.Password.RequireUppercase = true; // En az bir b�y�k harf gerektirir
    options.Password.RequiredLength = 8; // Parolan�n minimum uzunlu�u
    options.Password.RequireNonAlphanumeric = false; // �zel karakter gereksinimi

    // Kullan�c� ad� gereksinimleri
    options.User.RequireUniqueEmail = true; // E-posta adresi benzersiz olmal�d�r

    // Kilitleme (Lockout) se�enekleri
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Kilitlenme s�resi
    options.Lockout.MaxFailedAccessAttempts = 5; // Maksimum ba�ar�s�z giri� say�s�
    options.Lockout.AllowedForNewUsers = true; // Yeni kullan�c�lar i�in kilitleme �zelli�i aktif mi?

    // Hesap do�rulama (Email ve Phone)
    options.SignIn.RequireConfirmedEmail = true; // E-posta do�rulamas� gereksinimi
    options.SignIn.RequireConfirmedPhoneNumber = false; // Telefon numaras� do�rulamas� gereksinimi
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
