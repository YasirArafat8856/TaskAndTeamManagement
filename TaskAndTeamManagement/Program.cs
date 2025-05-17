using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskAndTeamManagement.Core.Interface.Repository;
using TaskAndTeamManagement.Core.Interface.Service;
using TaskAndTeamManagement.Core.Interface.UnitOFWork;
using TaskAndTeamManagement.Infrascture.Data;
using TaskAndTeamManagement.Infrascture.Implementation.Repo;
using TaskAndTeamManagement.Infrascture.Implementation.Service;
using TaskAndTeamManagement.Infrascture.Implementation.UnitOfWork;
using TaskAndTeamManagement.Infrascture.Mapper;
using TaskAndTeamManagement.Infrascture.Utility;
using static TaskAndTeamManagement.Core.Interface.Repository.IGenericRepository;

var builder = WebApplication.CreateBuilder(args);

#region Cors policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
#endregion

#region Auto Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = null; // Disable reference tracking
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

#endregion

#region Context

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagementDb")));

builder.Services.AddSingleton<DapperContext>();

#endregion

#region Register Repo
builder.Services.AddScoped<IDapperRepository, DapperRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
#endregion

#region Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
#endregion

#region Authorization
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

builder.Services.AddAuthorization();
#endregion

builder.Services.AddControllers();
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
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
