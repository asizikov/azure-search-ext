using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace Xcc.Azure.Search.Extensions.Client
{
    internal class SearchIndexFactory : ISearchIndexFactory
    {
        public Index GetIndexDescription(string indexName)
        {
            var definition = new Index
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<DocumentModel>(),
                Analyzers = new Analyzer[]
                {
                    new CustomAnalyzer
                    {
                        Name = Constants.CustomAnalyzersByName.CustomImageTagsAnalyzer,
                        Tokenizer = TokenizerName.Lowercase,
                        TokenFilters = new[]
                        {
                            TokenFilterName.Lowercase,
                            TokenFilterName.AsciiFolding,
                            TokenFilterName.Phonetic
                        }
                    }
                }
            };
            return definition;
        }
    }
}