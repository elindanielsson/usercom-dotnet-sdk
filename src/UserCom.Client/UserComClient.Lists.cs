using System.Net.Http;
using System.Threading.Tasks;
using UserCom.Model;
using UserCom.Model.Lists;
using UserCom.Model.Lists.Requests;

#pragma warning disable S4039 // Explicit implementation required: multiple interfaces share identical method signatures with different semantics
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
