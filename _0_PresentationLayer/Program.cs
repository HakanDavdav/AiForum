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
using System.Text;
using _1_BusinessLayer.Concrete.Services_Tools;
using _1_BusinessLayer.Concrete.Tools.Senders;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractFactories;
using _1_BusinessLayer.Concrete.Tools.BodyBuilders;

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


//autowiring tools
builder.Services.AddScoped<AbstractTokenFactory, TokenFactory>();
builder.Services.AddScoped<AbstractTokenSender, TokenSender>();
builder.Services.AddScoped<SmsBodyBuilder>();
builder.Services.AddScoped<EmailBodyBuilder>();
//autowiring of IdentityTools
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<UserManager<User>>();
//autowiring services
builder.Services.AddScoped<AbstractUserService, UserService>();





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
    options.SignIn.RequireConfirmedAccount = true;

})
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();



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

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();

    string[] roles = new[] { "Admin", "StandardUser", "Guest" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new UserRole { Name = role });
        }
    }
}


if (app.Environment.IsDevelopment())  // Yaln?zca geli?tirme ortam?nda aktif etmek isteyebilirsiniz
{
    app.UseSwagger(); // Swagger JSON dökümantasyonunu aktif eder
    app.UseSwaggerUI(); // Swagger UI'yi aktif eder
}

app.UseStaticFiles();
app.MapControllers();


app.Run();
