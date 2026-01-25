
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Application.Common.Behaviors;
using OrderManagementSystem.Application.RepositoryContract;
using OrderManagementSystem.Application.UnitOfWorkContract;
using OrderManagementSystem.Domain.Models;
using OrderManagementSystem.Infrastructure.Context;
using OrderManagementSystem.Infrastructure.RepositoryImplementation;
using OrderManagementSystem.Infrastructure.UnitOfWorkImplementation;
using OrderManagementSystem.Web.Seed;
using Serilog;

namespace OrderManagementSystem.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ApplicationContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                if (builder.Environment.IsDevelopment())
                {
                    option.LogTo(
                        message => Console.WriteLine($"\n[EF] {message}\n"),
                        new[] { DbLoggerCategory.Database.Command.Name },
                        LogLevel.Information);
                }
            });

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddFluentValidation();
            builder.Services.AddValidatorsFromAssembly(OrderManagementSystem.Application.AssemblyReference.Assembly, includeInternalTypes: true);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                await RoleSeeder.SeedRoles(scopedServices);
            }

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
