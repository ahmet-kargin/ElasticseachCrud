﻿
using Nest;
using System.Reflection.Metadata;

namespace ElasticseachCrudAPI.Services;

public class ElasticsearchService<T> : IElasticsearchService<T> where T : class
{
    private readonly ElasticClient _elasticClient;

    public ElasticsearchService(ElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }
    public async Task<string> CreateDocumentAsync(T document)
    {
       var response = await _elasticClient.IndexDocumentAsync(document);
        return response.IsValid ? "Document created succesfully" :"Failed to create document" ;
    }

    public async Task<string> DeleteDocumentAsync(int id)
    {
        var response = await _elasticClient.DeleteAsync(new DocumentPath<T>(id));
        return response.IsValid ? "Documetn created succesfully" : "Failed to create document";
    }

    public async Task<IEnumerable<T>> GetAllDocuments()
    {
        var searchResponse =await _elasticClient.SearchAsync<T>(s => s
                                                         .MatchAll()
                                                         .Size(10000));
        return searchResponse.Documents;
    }

    public async Task<T> GetDocumentAsync(int id)
    {
        var response = await _elasticClient.GetAsync(new DocumentPath<T>(id));
        return response.Source;
    }

    public async Task<string> UpdateDocumentAsync(T document)
    {
        var response = await _elasticClient.UpdateAsync(new DocumentPath<T>(document), u => u
                .Doc(document)
                .RetryOnConflict(3));

        return response.IsValid ? "Document updated succesfully" : "Failed to update document";

    }
}
