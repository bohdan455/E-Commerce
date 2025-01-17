using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;

namespace Offers.Services.Data.Repositories.Base;

public interface IBaseElasticRepository<T> where T : class
{
    Task CreateIndex(Action<CreateIndexRequestDescriptor<T>> action);
    Task<BulkResponse> AddOrUpdateBulk(IEnumerable<T> documents);
    Task<T> AddOrUpdate(T document);
    Task<BulkResponse> AddBulk(IList<T> documents);
    Task<GetResponse<T>> Get(string key);
    Task<List<T>?> GetAll();
    Task<SearchResponse<T>?> Query(SearchRequestDescriptor<T> sd);
    Task<bool> Remove(string key);
    Task<DeleteByQueryResponse> BulkRemove(DeleteByQueryRequestDescriptor<T> queryReq);
}