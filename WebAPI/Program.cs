using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using WebAPI.Entities.Data;
using WebAPI.Repository.Interface;
using WebAPI.Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SampleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));
//builder.Services.AddScoped<IEmailRepository, EmailRepository>();
//builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<Inewrepository, newrepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = " Standard Authorization using bearer Scheme  (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
//            //    _configuration.GetSection("AppSettings:Token").Value!));
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings: Token").Value)),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();

    //Run() Middleware

    //app.Run(async context => {
    //    await context.Response.WriteAsync("First Middleware!");
    //});
    //// this delegate didn’t invoke here because the first one terminated the pipeline.
    //app.Run(async context => {
    //    await context.Response.WriteAsync("Second Middleware!");
    //});

    //Use()
    //app.Use(async (context, next) =>
    //{
    //    await context.Response.WriteAsync("Before Request "); //1
    //    await next();
    //    await context.Response.WriteAsync("After Request "); //3
    //});
    //app.Run(async context =>
    //{
    //    await context.Response.WriteAsync("Hello!"); //2
    //});

    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.Map("/BranchOne", MapBranchOne);
//app.Map("/BranchTwo", MapBranchTwo);

//app.Run();
//static void MapBranchOne(IApplicationBuilder app)
//{
//    app.Run(async context =>
//    {
//        await context.Response.WriteAsync("You are on Branch One!");
//    });
//}
//static void MapBranchTwo(IApplicationBuilder app)
//{
//    app.Run(async context =>
//    {
//        await context.Response.WriteAsync("You are on Branch Two!");
//    });
//}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


