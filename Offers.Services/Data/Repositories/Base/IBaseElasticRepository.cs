using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Offers.Common.Models.Base;

namespace Offers.Services.Data.Repositories.Base;

public interface IBaseElasticRepository<T> where T : BaseModel
{
    Task CreateIndex(Action<CreateIndexRequestDescriptor<T>> action);
    Task<T> AddOrUpdate(T document);
    Task<GetResponse<T>> Get(string key);
    Task<SearchResponse<T>?> GetAll();
    Task<SearchResponse<T>?> Query(SearchRequestDescriptor<T> sd);
    Task<bool> Remove(string key);
}