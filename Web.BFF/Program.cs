using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using System.Security.Claims;
using Web.BFF.Interfaces;
using Web.BFF.Services;
using Web.BFF.Transforms;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


//builder.Services.AddApiVersioning(opts =>
//{
//    opts.ReportApiVersions = true;
//    opts.UseApiBehavior = true;
//});

// Add other services here.....
builder.Services.AddHttpClient("GreetingService", cfg => 
{
    cfg.BaseAddress = new Uri(builder.Configuration.GetSection("Services").GetValue<string>("Greeting"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IGreetingService, GreetingService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClaimsTransformation, ClaimsTransformation>();


//builder.Services.AddAuthorization(options =>
//{
//    var defaultAccessPolicy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .RequireRole("tenant")
//    .Build();

//    var zonalAccessPolicy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .RequireRole("zonal_admin")
//    .Build();

//    var partnerAccessPolicy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .RequireRole("customer_partner")
//    .Build();

//    options.DefaultPolicy = defaultAccessPolicy;
//    options.AddPolicy("CustomerPartnerPolicy", partnerAccessPolicy);
//    options.AddPolicy("ZonalAdminPolicy", zonalAccessPolicy);
//});

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

app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyHeader();
    x.AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
