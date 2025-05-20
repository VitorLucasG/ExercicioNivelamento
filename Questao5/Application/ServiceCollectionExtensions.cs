using Questao5.Application.Handlers.interfaces;
using Questao5.Application.Handlers.Movimentacao;
using Questao5.Application.Handlers.Saldo;

namespace Questao5.Application
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddTransient<IMovimentacaoHandler, MovimentacaoHandler>();
            services.AddTransient<ISaldoHandler, SaldoHandler>();
            
            return services;
        }

    }
}
