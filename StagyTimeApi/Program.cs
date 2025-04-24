using System.Data.Common;
using MySqlConnector;        
using PetaPoco;
using StagyTimeApi.Repositories;
using StagyTimeApi.Repositories.Interfaces;
using StagyTimeApi.Services;
using StagyTimeApi.Services.Interfaces;

namespace StagyTimeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Recupera a connection string do appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // ? Registra o factory para MySql.Data no .NET Core
            DbProviderFactories.RegisterFactory(
                "MySqlConnector",
                 MySqlConnectorFactory.Instance
            );

            // ? Configura o PetaPoco.Database usando o mesmo providerName
            builder.Services.AddSingleton<Database>(sp =>
                new Database(connectionString, "MySqlConnector")
            );

            // ? Registra injeções de dependência de repositório e serviço
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // ? Controllers + Swagger/OpenAPI
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ? Pipeline HTTP
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
