using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using elearning_platform.Configs;
using elearning_platform.Auth;
using elearning_platform.Data;
using Microsoft.EntityFrameworkCore;
using elearning_platform.Repo;
using elearning_platform.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var jwtConfig = new JWTConfig();
builder.Configuration.Bind("JWT", jwtConfig);

var smtpConfig = new SMTPConfig();
builder.Configuration.Bind("SMTP", smtpConfig);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var keyInBytes = Encoding.UTF8.GetBytes(jwtConfig.Key);
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(keyInBytes)
    };
});


//Authentication
builder.Services.AddAuthorization(options => PolicyManager.SetAuthorizationPolicies(options));

//App Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = "Server=127.0.0.1;Port=5432;Database=Elearning;User Id=postgres;Password=pgAdmin;";
    options.UseNpgsql(connectionString);
});

builder.Services.AddSingleton(jwtConfig);
builder.Services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddSingleton(smtpConfig);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMfaRepo, MfaRepo>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAdminRepo, AdminRepo>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
