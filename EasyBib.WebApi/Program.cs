
using EasyBib.Domain.Contracts;
using EasyBib.Domain.Services;
using EasyBib.Infrastructure;
using EasyBib.Infrastructure.Persistance;

namespace EasyBib.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("Default")!;
            builder.Services.AddSqlServer<EasyBibDbContext>(connectionString);
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IMediaItemRepository, MediaItemRepository>();
            builder.Services.AddScoped<LoanService>();
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
