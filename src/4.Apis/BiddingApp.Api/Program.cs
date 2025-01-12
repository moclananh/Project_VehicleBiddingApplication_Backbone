using BiddingApp.Application.Hubs;
using BiddingApp.Application.Services.AuthenticateServices;
using BiddingApp.Application.Services.BiddingServices;
using BiddingApp.Application.Services.BiddingSessionServices;
using BiddingApp.Application.Services.UserServices;
using BiddingApp.Application.Services.VehicleSevices;
using BiddingApp.Application.SignalRServices;
using BiddingApp.BuildingBlock.Exceptions.Handler;
using BiddingApp.Domain.Models;
using BiddingApp.Domain.Models.EF;
using BiddingApp.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//config connection with db
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));

//register signalr
builder.Services.AddSignalR();

//declare DI
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IBiddingService, BiddingService>();
builder.Services.AddScoped<IBiddingSessionService, BiddingSessionService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
//register DI for signalr
builder.Services.AddScoped<IBiddingNotificationService, BiddingNotificationService>();

// Register logging services (ILogger is already available)
builder.Services.AddLogging();

//register infrastructure services
builder.Services.AddInfrastructureServices();

//register custom exception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//setting JWT
var secretKey = builder.Configuration["AppSettings:SecretKey"];
var SecretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            //tu cap token
            ValidateIssuer = false,
            ValidateAudience = false,

            //ky vao token
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(SecretKeyBytes),
            ClockSkew = TimeSpan.Zero
        };
    });
//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddRouting(options => { options.LowercaseUrls = true; });

//custom swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger ERS Solution", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

//use cors
app.UseCors("AllowSpecificOrigins");

//use custom exception
app.UseExceptionHandler(option => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<BiddingHub>("/bidding-hub");

app.Run();