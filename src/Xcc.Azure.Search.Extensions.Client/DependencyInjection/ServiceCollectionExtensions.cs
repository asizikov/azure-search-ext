using Microsoft.Azure.Search;
using Microsoft.Extensions.DependencyInjection;

namespace Xcc.Azure.Search.Extensions.Client.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void UseSearchIndexClient(this IServiceCollection serviceCollection, string apiHost,
            string apiKey, ISearchIndexFactory searchIndexFactory = null)
        {
            serviceCollection.AddTransient(provider => searchIndexFactory ?? new SearchIndexFactory());
            serviceCollection.AddTransient<ISearchServiceClient>(provider =>
                new SearchServiceClient(apiHost, new SearchCredentials(apiKey)));
            serviceCollection.AddSingleton<ISearchApiClient, SearchApiClient>();
        }
    }
}