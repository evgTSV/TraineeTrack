using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TraineeTrack.API.Exceptions;
using TraineeTrack.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.ConfigureOptions<JwtBearerOptionsConfiguration>();

builder.Services.AddDbContext<TraineeTrackContext>(o =>
    o.UseNpgsql(new NpgsqlConnectionStringBuilder
        {
            Username = builder.Configuration["POSTGRES_USER"],
            Password = builder.Configuration["POSTGRES_PASSWORD"],
            Host = builder.Configuration["POSTGRES_HOST"],
            Port = int.Parse(builder.Configuration["POSTGRES_PORT"] ?? "5432"),
            Database = builder.Configuration["POSTGRES_DB"]
        }.ConnectionString, 
        opts => opts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddControllers(o => o.Filters.Add<ExceptionFilter>())
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.WriteIndented = true;
        o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication().AddJwtBearer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetService<TraineeTrackContext>()?.Database.Migrate();

app.Run();