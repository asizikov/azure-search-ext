using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Logging;

namespace Xcc.Azure.Search.Extensions.Client
{
    internal class SearchApiClient : ISearchApiClient
    {
        private readonly ISearchServiceClient _searchServiceClient;
        private readonly ISearchIndexFactory _searchIndexFactory;

        public SearchApiClient(ISearchServiceClient searchServiceClient, ISearchIndexFactory searchIndexFactory)
        {
            _searchServiceClient = searchServiceClient;
            _searchIndexFactory = searchIndexFactory;
        }

        public async Task AddDocumentAsync(DocumentModel document, ILogger logger, CancellationToken token = default)
        {
            await EnsureIndexCreatedAsync(logger, token);
            var actions = new[]
            {
                IndexAction.MergeOrUpload(document)
            };
            var searchIndexClient = _searchServiceClient.Indexes.GetClient(Constants.IndexName);
            await searchIndexClient.Documents.IndexAsync(IndexBatch.New(actions), cancellationToken: token);
        }

        private async Task EnsureIndexCreatedAsync(ILogger logger, CancellationToken token)
        {
            if (!await _searchServiceClient.Indexes.ExistsAsync(Constants.IndexName, cancellationToken: token))
            {
                logger.LogWarning("No index found, a new index will be crated.");
                await CreateIndexAsync(Constants.IndexName, token);
            }
        }

        private async Task CreateIndexAsync(string indexName, CancellationToken token)
        {
            var definition = _searchIndexFactory.GetIndexDescription(indexName);
            await _searchServiceClient.Indexes.CreateAsync(definition, cancellationToken: token);
        }

        public async Task RecreateIndexAsync(ILogger logger, CancellationToken token = default)
        {
            logger.LogWarning("Going to drop and recreated search index");
            await _searchServiceClient.Indexes.DeleteWithHttpMessagesAsync(Constants.IndexName, cancellationToken: token);
        }
    }
}