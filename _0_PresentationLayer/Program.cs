using _1_BusinessLayer.Concrete.Services;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Extensions;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Identity;
using _2_DataAccessLayer.Concrete.Entities;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractSenders;
using _1_BusinessLayer.Concrete.Tools.AuthenticationManagers.Senders;
using _1_BusinessLayer.Concrete.Tools.AuthenticationManagers.BodyBuilders;
using _1_BusinessLayer.Concrete.Tools.AuthenticationManagers.Factories;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Concrete.Tools.BotManagers;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using Serilog.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.CreateOptions(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>();


//autowiring repositories
builder.Services.AddScoped<AbstractEntryRepository, EntryRepository>();
builder.Services.AddScoped<AbstractFollowRepository, FollowRepository>();
builder.Services.AddScoped<AbstractLikeRepository, LikeRepository>();
builder.Services.AddScoped<AbstractPostRepository, PostRepository>();
builder.Services.AddScoped<AbstractUserRepository, UserRepository>();
builder.Services.AddScoped<AbstractBotRepository, BotRepository>();
builder.Services.AddScoped<AbstractNotificationRepository, NotificationRepository>();
builder.Services.AddScoped<AbstractUserPreferenceRepository, UserPreferenceRepository>();
builder.Services.AddScoped<AbstractActivityRepository, BotActivityRepository>();
builder.Services.AddScoped<AbstractNewsRepository, NewsRepository>();

//autowiring of IdentityTools
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<UserManager<User>>();
//autowiring authIntegration Tools
builder.Services.AddScoped<AbstractTokenSender, TokenSender>();
builder.Services.AddScoped<TokenFactory>();
builder.Services.AddScoped<SmsBodyBuilder>();
builder.Services.AddScoped<EmailBodyBuilder>();
//autowiring bot Tools
builder.Services.AddScoped<BotApiCaller>();
builder.Services.AddScoped<BotDatabaseReader>();
builder.Services.AddScoped<BotDatabaseWriter>();
builder.Services.AddScoped<BotDeployManager>();
builder.Services.AddScoped<BotResponseParser>();
builder.Services.AddScoped<ProbabilitySet>();


//autowiring services
builder.Services.AddScoped<AbstractBotService, BotService>();
builder.Services.AddScoped<AbstractEntryService, EntryService>();
builder.Services.AddScoped<AbstractFollowService, FollowService>();
builder.Services.AddScoped<AbstractLikeService, LikeService>();
builder.Services.AddScoped<AbstractPostService, PostService>();
builder.Services.AddScoped<AbstractUserService, UserService>();
builder.Services.AddScoped<AbstractUserIdentityService, UserIdentityService>();




var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new SerilogLoggerProvider(logger));



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
    
    options.AddPolicy("TempUserPolicy", policy => policy.RequireRole("TempUser","StandardUser","Admin"));

});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // API'nin tüm endpointlerini ke?fetmek için
builder.Services.AddSwaggerGen(); // Swagger UI ve dokümantasyonunu olu?turmak için


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();

    string[] roles = new[] { "Admin", "StandardUser", "TempUser" };

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
