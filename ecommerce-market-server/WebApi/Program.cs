using System.Text;
using BusinessLogic.Data;
using BusinessLogic.Logic;
using BusinessLogic.Logic.Repository;
using BusinessLogic.Logic.Service;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using WebApi.Middlewares;
using WebApi.Profiles;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigurePipeline(app);

await SeedDatabase(app);

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Registro de servicios
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();

    // Configuración de Identity
    var identityBuilder = services.AddIdentityCore<User>();

    identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);

    identityBuilder.AddRoles<IdentityRole>();
    identityBuilder.AddEntityFrameworkStores<SecurityDbContext>();
    identityBuilder.AddSignInManager<SignInManager<User>>();

    // Configuración de autenticación JWT
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]!)),
                ValidIssuer = configuration["Token:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = false
            };
        });

    // AutoMapper
    services.AddAutoMapper(typeof(MappingProfile));

    // Repositorios genéricos
    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    services.AddScoped(typeof(IGenericSecurityRepository<>), typeof(GenericSecurityRepository<>));

    // Configuración de DbContext
    services.AddDbContext<MarketDbContext>(opt =>
    {
        opt.UseSqlServer(configuration.GetConnectionString("SQLServerConnection"));
    });

    services.AddDbContext<SecurityDbContext>(x =>
    {
        x.UseSqlServer(configuration.GetConnectionString("AuthConnection"));
    });

    // Configuración de Redis
    services.AddSingleton<IConnectionMultiplexer>(c =>
    {
        var redisConfiguration = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection")!, true);
        return ConnectionMultiplexer.Connect(redisConfiguration);
    });

    // Servicios del sistema
    services.AddSingleton<TimeProvider>(TimeProvider.System);

    // Repositorios específicos
    services.AddTransient<IProductRepository, ProductRepository>();
    services.AddScoped<IBuyCartRepository, BuyCartRepository>();

    // Controladores
    services.AddControllers();

    // Configuración de CORS
    services.AddCors(opt =>
    {
        opt.AddPolicy("CorsRule", rule =>
        {
            rule.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("*");
        });
    });
}

static void ConfigurePipeline(WebApplication app)
{
    // Middleware de manejo de excepciones
    app.UseMiddleware<ExceptionMiddleware>();

    // Páginas de códigos de estado
    app.UseStatusCodePagesWithReExecute("/errors", "?code={0}");

    // Routing
    app.UseRouting();

    // CORS
    app.UseCors("CorsRule");

    // Autenticación y autorización
    app.UseAuthentication();
    app.UseAuthorization();

    // Mapeo de controladores
    app.MapControllers();
}

static async Task SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        // Migración de MarketDbContext
        var context = services.GetRequiredService<MarketDbContext>();

        await context.Database.MigrateAsync();
        await MarketDbContextData.LoadDataAsync(context, loggerFactory);

        // Migración de SeguridadDbContext y seed de usuarios
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var identityContext = services.GetRequiredService<SecurityDbContext>();

        await identityContext.Database.MigrateAsync();
        await SecurityDbContextData.SeedUserAsync(userManager, roleManager, loggerFactory);
    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();

        logger.LogError(e, "Errores en el proceso de migración");
    }
}
