using AutoWrapper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using elearning_platform.Middlewares;
using elearning_platform.Configs;
using elearning_platform.Auth;
using elearning_platform.Data;
using Microsoft.EntityFrameworkCore;
using elearning_platform.Repo;
using elearning_platform.Services;
using System.Text.Json.Serialization;
using elearning_platform.Hubs;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions(){
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Staging,
    WebRootPath = "customwwwroot"
});

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
    // var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
    var connectionString = "Server=127.0.0.1;Port=5432;Database=Elearning;User Id=postgres;Password=pgAdmin;";

    if (connectionString == null)
    {
        throw new Exception("DATABASE_URL env var is not set");
    }
    options.UseNpgsql(connectionString);
});

builder.Services.AddSingleton(jwtConfig);
builder.Services.AddSingleton(smtpConfig);
builder.Services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();

builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IAdminRepo, AdminRepo>();
builder.Services.AddScoped<IMfaRepo, MfaRepo>();
builder.Services.AddScoped<IClaimRepo, ClaimRepo>();
builder.Services.AddScoped<IFileRepo, FileRepo>();
builder.Services.AddScoped<ITutorRepo, TutorRepo>();
builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();
builder.Services.AddScoped<ITaughtSubjectRepo, TaughtSubjectRepo>();
builder.Services.AddScoped<ITutorRequestRepo, TutorRequestRepo>();
builder.Services.AddScoped<IPaymentLinkRepo, PaymentLinkRepo>();
builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
builder.Services.AddScoped<ISessionRepo, SessionRepo>();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IOnboardingService, OnboardingService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ITaughtSubjectService, TaughtSubjectService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IStatService, StatService>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<ITutorRequestService, TutorRequestService>();



builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseStaticFiles(new StaticFileOptions(){
//     RequestPath = "/StaticFiles"
// });
app.UseAuthentication();
app.UseAuthorization();
app.UseCurrentUserService();
app.UseApiResponseAndExceptionWrapper();
app.MapControllers();
// app.MapHub<ChatHub>("/notify");

app.Run();
