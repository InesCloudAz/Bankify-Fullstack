using Bankify;
using Bankify.Interfaces;
using Bankify.Repository.Interfaces;
using Bankify.Repository.Repos;
using Bankify.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;




var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();





builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
     .AddJwtBearer(opt =>
     {
         opt.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = "http://localhost:5146",
             ValidAudience = "http://localhost:5146",
             IssuerSigningKey =
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("privatekey197354%¤%#098713)%¤?913864%%%%##"))
         };
     });





builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Skriv 'Bearer <din-token>' för att autentisera.",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(

        policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});








builder.Services.AddAuthorization();

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();

builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<IBankifyContext, BankifyContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CustomerService>();



var app = builder.Build();
app.UseCors(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
);
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowFrontend");



app.Run();