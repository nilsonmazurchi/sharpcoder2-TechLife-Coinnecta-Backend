using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text;
using sharpcoder2_TechLife_Coinnecta_Backend.Controller;
using Microsoft.IdentityModel.Tokens;
using sharpcoder2_TechLife_Coinnecta_Backend;
using sharpcoder2_TechLife_Coinnecta_Backend.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureSwagger();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.
    AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlite(defaultConnectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Substitua pelo endereço do seu cliente Angular
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// Configuração da autenticação JWT
// var jwtSettings = builder.Configuration.GetSection("JwtSettings");
// var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
//
// builder.Services.AddSingleton<JwtSettings>(new JwtSettings
// {
//     Secret = jwtSettings["Secret"]
// });
//
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = "JwtBearer";
//     options.DefaultChallengeScheme = "JwtBearer";
// }).AddJwtBearer("JwtBearer", options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuerSigningKey = true,
//         IssuerSigningKey = new SymmetricSecurityKey(key),
//         ValidateIssuer = false,
//         ValidateAudience = false,
//         ValidateLifetime = true
//     };
// });

builder.Services.AddHttpClient();


builder.Services.AddScoped<RendimentoServico>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

