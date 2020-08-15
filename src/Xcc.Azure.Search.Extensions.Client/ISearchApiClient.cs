using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Xcc.Azure.Search.Extensions.Client
{
    public interface ISearchApiClient
    {
        Task AddDocumentAsync(DocumentModel document,ILogger logger, CancellationToken token = default);
        Task RecreateIndexAsync(ILogger logger, CancellationToken token = default);
    }
}