using ControlAccesoApi.Repositorios;
using ControlAccesoApi.Servicios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControlAccesoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}