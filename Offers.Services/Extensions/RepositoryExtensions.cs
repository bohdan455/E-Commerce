using Microsoft.Extensions.DependencyInjection;
using Offers.Services.Data.Repositories.Base;
using Offers.Services.Data.Repositories.Interfaces;

namespace Offers.Services.Extensions;

public static class RepositoryExtensions
{
    private static Dictionary<Type, Type> _repositories = new();
    
    public static void AddRepositories(this IServiceCollection collection)
    {
        var baseElasticRepository = typeof(IBaseElasticRepository<>);
        _repositories = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsInterface)
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseElasticRepository))
            .Select(k => new KeyValuePair<Type,Type>(k, AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t is { IsClass: true, IsAbstract: false })
                .First(k.IsAssignableFrom))).ToDictionary();

        foreach (var repository in _repositories)
        {
            collection.AddScoped(repository.Key, repository.Value);
        }
        
    }
    
    public static async Task InitElasticearchIndexes(this IServiceProvider provider)
    {
        await InitProductIndex(provider);
    }

    private static async Task InitProductIndex(IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        var repository = scope.ServiceProvider.GetService<IProductRepository>();
        await repository.CreateIndex(x => {});
    }
}