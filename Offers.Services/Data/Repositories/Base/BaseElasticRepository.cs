﻿using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using Offers.Options;

namespace Offers.Services.Data.Repositories.Base;

public class BaseElasticRepository<T> : IBaseElasticRepository<T> where T : class
{
    private string IndexName { get; set; }
    private readonly ElasticsearchClient _client;

    public BaseElasticRepository(IOptions<ElasticSearchOptions> options)
    {
        var settings =
            new ElasticsearchClientSettings(new Uri(options.Value.Url)).Authentication(
                new BasicAuthentication(options.Value.Username, options.Value.Password));
        _client = new ElasticsearchClient(settings);
        IndexName = typeof(T).Name.ToLower() + "s";
    }
    public async Task<BulkResponse> AddOrUpdateBulk(IEnumerable<T> documents)
    {
        var indexResponse = await _client.BulkAsync(b => b
            .Index(IndexName)
            .UpdateMany(documents, (ud, d) => ud.Doc(d).DocAsUpsert())
        );
        return indexResponse;
    }

    public async Task<T> AddOrUpdate(T document)
    {
        var indexResponse =
            await _client.IndexAsync(document, idx => idx.Index(IndexName));
        if (!indexResponse.IsValidResponse)
        {
            throw new Exception(indexResponse.DebugInformation);
        }

        return document;
    }

    public async Task<BulkResponse> AddBulk(IList<T> documents)
    {
        var resp = await _client.BulkAsync(b => b
            .Index(IndexName)
            .IndexMany(documents)
        );
        return resp;
    }

    public async Task<GetResponse<T>> Get(string key)
    {
        return await _client.GetAsync<T>(key, g => g.Index(IndexName));
    }

    public async Task<List<T>?> GetAll()
    {
        var searchResponse = await _client.SearchAsync<T>(s => s.Index(IndexName).Query(q => q.MatchAll(new MatchAllQuery())));
        return searchResponse.IsValidResponse ? searchResponse.Documents.ToList() : default;
    }

    public async Task<SearchResponse<T>?> Query(SearchRequestDescriptor<T> sd)
    {
        var searchResponse = await _client.SearchAsync<T>(sd);
        return searchResponse;
    }

    public async Task<bool> Remove(string key)
    {
        var response = await _client.DeleteAsync<T>(key, d => d.Index(IndexName));
        return response.IsValidResponse;
    }

    public async Task<DeleteByQueryResponse> BulkRemove(DeleteByQueryRequestDescriptor<T> queryReq)
    {
        var response = await _client.DeleteByQueryAsync(queryReq);
        return response;
    }
}