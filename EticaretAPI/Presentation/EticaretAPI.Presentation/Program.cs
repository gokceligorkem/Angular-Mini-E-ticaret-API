using EticaretAPI.Application;
using EticaretAPI.Application.Validations.Product;
using EticaretAPI.Infrastructure;
using EticaretAPI.Infrastructure.Filters;
using EticaretAPI.Infrastructure.Services.StorageConcrete.Local;
using EticaretAPI.Persistence;
using EticaretAPI.Presentation.Configurations.ColumnWriters;
using EticaretAPI.Presentation.Exceptions;
using EticaretAPI.SignalR;
using EticaretAPI.SignalR.Hubs;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();//Client tan gelen request neticesinde olu�turulan Httpcontext nesnesine katmanlardaki classlar
//�zerinden (busineess logic ) eri�ebilmemizi sa�layan bir servistir.
builder.Services.AddPersistenceServices();//IoC container ileti�im
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationService();

builder.Services.AddSignalRServices();

builder.Services.AddStogare<LocalStogare>();
builder.Services.AddCors(options =>
  options.AddDefaultPolicy(policy=>policy.WithOrigins("http://localhost:4200","http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));//CQRS 


//Serilog configuration
Logger log = new LoggerConfiguration()
    .WriteTo.Console()//Consola
    .WriteTo.File("logs/log.txt")//File
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString(
        "PostgreSQL"),
        "Logs",
        needAutoCreateTable:true,
        columnOptions:new Dictionary<string, ColumnWriterBase>
        {
            {"message",new RenderedMessageColumnWriter() },
            {"message_template",new RenderedMessageColumnWriter() },
            {"level",new LevelColumnWriter() },
            {"time_stamp",new TimestampColumnWriter() },
            {"exception" ,new ExceptionColumnWriter() },
            {"log_event",new LogEventSerializedColumnWriter()},
            {"user_name",new UsernameColumnWriter() }
        })//veritab�na bu 3 noktaya loglama yap dedik 
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext()//Context beslenmesi gerekti�ini belirtiyoruz.
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log);
//-----
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});

builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValid>())
    .ConfigureApiBehaviorOptions(options=>options.SuppressModelStateInvalidFilter=true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options => {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,//Olu�uturulacak token de�erini kimlerin/hangi originlerin/sitelerin kullan�c� belirledi�imiz de�erdir.>www.random.com
            ValidateIssuer = true,//Olu�turulacak token de�erini kimin da��tt�n� ifade edece�imiz aland�r.www.myapi.com
            ValidateLifetime = true,//Olu�turulan token�n s�resini kontrol etti�imiz do�rulamad�r
            ValidateIssuerSigningKey = true,//�retilecek token de�erinin uygulamam�z� ait bir de�er  oldu�unu ifade eden suciry key    verisinin do�rulamas�d�r.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType=ClaimTypes.Name//Bu sayede name i �ekebiliyoruz.bunu yapt�ktan sonra jwt �retti�imiz yap�lanmaya gidip 
            
           
        };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExcepitonHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());//GLOBAL Exception middleware
app.UseStaticFiles();//wwwroot
app.UseSerilogRequestLogging();

app.UseHttpLogging();
app.UseCors();//Middleware olarak i�lemesini sa�lad�k.
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) => {
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name",username);
    await next();
});

app.MapControllers();
app.MapHubRegistration();
app.Run();
