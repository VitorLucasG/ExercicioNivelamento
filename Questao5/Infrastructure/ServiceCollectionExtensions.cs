using System.Reflection;
using Questao5.Application.Handlers.interfaces;
using Questao5.Application.Handlers.Movimentacao;
using Questao5.Domain.RepositoryContracts;

namespace Questao5.Infrastructure
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddInfraServices(this IServiceCollection service)
        {

            var repositoryInterface = typeof(IBaseRepository<>);

            var repositoryTypes = Assembly.GetExecutingAssembly().GetTypes()
                                            .Where(t => t.GetInterfaces()
                                                          .Select(s => s.Name)
                                                          .ToList()
                                                          .Contains(repositoryInterface.Name)
                                                        && t.IsClass
                                                        && !t.IsNested
                                                        && !t.IsGenericType
                                            ).ToList();

            foreach (var serviceType in repositoryTypes)
            {
                var implementationType = serviceType.GetInterfaces().Where(w => w.Name.Contains(serviceType.Name)).FirstOrDefault();
                if (implementationType != null)
                {
                    service.AddTransient(implementationType, serviceType);
                }
            }

            return service;

        }

    }
}
