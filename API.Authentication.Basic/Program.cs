
using API.Authentication.Basic.Authentication;

using Microsoft.AspNetCore.Authentication;

namespace API.Authentication.Basic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string AUTH_SCHME_NAME = "BasicAuthentication";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Adding Authentication Schemes
            builder.Services.AddAuthentication(AUTH_SCHME_NAME)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>(AUTH_SCHME_NAME, null);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
