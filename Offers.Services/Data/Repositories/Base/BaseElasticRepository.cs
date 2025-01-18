using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using Offers.Common.Models.Base;
using Offers.Options;

namespace Offers.Services.Data.Repositories.Base;

public abstract class BaseElasticRepository<T> : IBaseElasticRepository<T> where T : BaseModel
{
    protected string IndexName { get; set; }
    protected readonly ElasticsearchClient _client;

    public BaseElasticRepository(IOptions<ElasticSearchOptions> options)
    {
        var settings =
            new ElasticsearchClientSettings(new Uri(options.Value.Url)).Authentication(
                new BasicAuthentication(options.Value.Username, options.Value.Password));
        _client = new ElasticsearchClient(settings);
        IndexName = typeof(T).Name.ToLower() + "s";
    }

    public virtual async Task CreateIndex(Action<CreateIndexRequestDescriptor<T>> action)
    {
        var indexExists = await _client.Indices.ExistsAsync<T>(IndexName);

        if (indexExists.Exists)
        {
            return;
        }

        var _ = await _client.Indices.CreateAsync<T>(IndexName, action);
    }

    public virtual async Task<T> AddOrUpdate(T document)
    {
        var indexResponse =
            await _client.IndexAsync(document, idx => idx.Index(IndexName).Id(document.Id));
        if (!indexResponse.IsValidResponse)
        {
            throw new Exception(indexResponse.DebugInformation);
        }

        return document;
    }

    public virtual async Task<GetResponse<T>> Get(string key)
    {
        return await _client.GetAsync<T>(key, g => g.Index(IndexName));
    }

    public virtual async Task<SearchResponse<T>?> GetAll()
    {
        return await _client.SearchAsync<T>(s => s.Index(IndexName).Query(q => q.MatchAll(new MatchAllQuery())));
    }

    public virtual async Task<SearchResponse<T>?> Query(SearchRequestDescriptor<T> sd)
    {
        var searchResponse = await _client.SearchAsync<T>(sd.Index(IndexName));
        return searchResponse;
    }

    public virtual async Task<bool> Remove(string key)
    {
        var response = await _client.DeleteAsync<T>(key, d => d.Index(IndexName));
        return response.IsValidResponse;
    }
}