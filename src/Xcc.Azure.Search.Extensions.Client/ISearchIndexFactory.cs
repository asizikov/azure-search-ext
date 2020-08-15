using Microsoft.Azure.Search.Models;

namespace Xcc.Azure.Search.Extensions.Client
{
    public interface ISearchIndexFactory
    {
        Index GetIndexDescription(string indexName);
    }
}