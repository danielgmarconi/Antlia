using System.Reflection;
using Antlia.Application.Interfaces;
using Antlia.Application.Mappings;
using Antlia.Application.Services;
using Antlia.Domain.Interfaces;
using Antlia.Infra.Data.Repositories;
using DataAccessLayer.SqlServerAdapter;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antlia.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.Load("Antlia.Application"));
            services.AddScoped<ISQLServerAdapter>(_ => new SQLServerAdapter(configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            services.AddScoped<IMovimentoManualRepository, MovimentoManualRepository>();
            services.AddScoped<IMovimentoManualService, MovimentoManualService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IProdutoCosifRepository, ProdutoCosifRepository>();
            services.AddScoped<IProdutoCosifService, ProdutoCosifService>();

            return services;
        }
    }
}
