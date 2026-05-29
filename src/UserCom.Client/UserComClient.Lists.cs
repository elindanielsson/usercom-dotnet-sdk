using System.Net.Http;
using System.Threading.Tasks;
using UserCom.Model;
using UserCom.Model.Lists;
using UserCom.Model.Lists.Requests;

// Explicit interface implementation is intentional: multiple interfaces on UserComClient share method names
// (e.g. DeleteAsync, GetAllAsync) with identical signatures but different semantics. Implicit implementation
// would cause compile-time ambiguity that cannot be resolved with public methods alone.
#pragma warning disable S4039
namespace UserCom
{
    public partial class UserComClient : IUserComListsClient
    {
        private static string LISTS_RESOURCE = "/api/public/lists";

        async Task<List> IUserComListsClient.CreateAsync(string name, string description)
        {
            var request = new UpdateOrCreateListRequest { Name = name, Description = description };
            var result = await SendAsync<UpdateOrCreateListRequest, List>(HttpMethod.Post, $"{LISTS_RESOURCE}/", request);

            return result;
        }

        async Task IUserComListsClient.DeleteAsync(int id)
        {
            await SendAsync(HttpMethod.Delete, $"{LISTS_RESOURCE}/{id}/");
        }

        async Task<PaginatedResult<List>> IUserComListsClient.GetAllAsync()
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{LISTS_RESOURCE}/");
            var paginatedResult = CreatePaginatedResult<List>(result);

            return paginatedResult;
        }

        async Task IUserComListsClient.UpdateAsync(int id, string? name, string? description)
        {
            var request = new UpdateOrCreateListRequest { Name = name, Description = description };
            await SendAsync<UpdateOrCreateListRequest>(HttpMethod.Put, $"{LISTS_RESOURCE}/{id}/", request);
        }
    }
}
#pragma warning restore S4039
