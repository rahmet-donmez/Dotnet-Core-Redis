
using Microsoft.Extensions.Configuration;
using RedisExample.Services;
using StackExchange.Redis;

namespace RedisExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var redisConnection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
            builder.Services.AddSingleton<RedisService, RedisService>();

            
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
