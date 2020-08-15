using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System.ComponentModel.DataAnnotations;

namespace Xcc.Azure.Search.Extensions.Client
{
    public class DocumentModel
    {
        [Key]
        public string Key { get; set; }

        [IsSearchable]
        [IsFilterable]
        [Analyzer(AnalyzerName.AsString.Keyword)]
        public string CaseName { get; set; }

        [IsSearchable]
        [Analyzer(Constants.CustomAnalyzersByName.CustomImageTagsAnalyzer)]
        public string[] Tags { get; set; }

        [IsRetrievable(true)] 
        public string FilePath { get; set; }
    }
}