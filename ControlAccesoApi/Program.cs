using ControlAccesoApi.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using ControlAccesoApi.Servicios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControlAccesoApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
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
            // Verifica que la clave JWT se esté leyendo correctamente
            var jwtKey = builder.Configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new Exception("JWT Key is not configured properly.");
            }
            Console.WriteLine($"JWT Key: {jwtKey}"); // Agrega esta línea para verificar el valor de la clave

            // Configura la autenticación JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "tu-dominio.com",
                        ValidAudience = "tu-dominio.com",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            builder.Services.AddAuthorization();

            // Register services as singletons
            builder.Services.AddSingleton<MongoDBService>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                return new MongoDBService(configuration);
            });
            builder.Services.AddSingleton<UsuarioRepositorio>();
            builder.Services.AddSingleton<AccesoRepositorio>();
            builder.Services.AddSingleton<RegistroRepositorio>();
            builder.Services.AddSingleton<PuertaRepositorio>();
            builder.Services.AddSingleton<PermisosRepositorio>();
            builder.Services.AddSingleton<CircuitoRepositorio>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Añade el middleware de autenticación y autorización
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();

            // Genera el token JWT (esto normalmente se haría en el controlador de autenticación)
            var token = await ObtenerTokenJwtAsync();

            // Realiza una solicitud GET con el token JWT
            await HacerSolicitudGetConTokenAsync(token);
        }

        private static async Task<string> ObtenerTokenJwtAsync()
        {
            using var client = new HttpClient();
            var loginData = new { Username = "admin", Password = "1234" };
            var response = await client.PostAsJsonAsync("https://localhost:44332/api/auth/login", loginData);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<dynamic>();
            return result.token;
        }

        private static async Task HacerSolicitudGetConTokenAsync(string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:44332/api/Acceso/Get");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}