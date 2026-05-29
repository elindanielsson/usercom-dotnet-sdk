using System.Net.Http;
using System.Threading.Tasks;
using UserCom.Model;
using UserCom.Model.CRM;
using UserCom.Model.Tags;
using UserCom.Model.Users;

#pragma warning disable S4039 // Explicit implementation required: multiple interfaces share identical method signatures with different semantics
namespace UserCom
{
    public partial class UserComClient : IUserComTagsClient
    {
        private static string TAGS_RESOURCE = "/api/public/tags";

        async Task<Tag> IUserComTagsClient.CreateAsync(string name)
        {
            var result = await SendAsync<dynamic, Tag>(HttpMethod.Post, $"{TAGS_RESOURCE}/", new { name });

            return result;
        }

        async Task IUserComTagsClient.DeleteAsync(int id)
        {
            await SendAsync(HttpMethod.Delete, $"{TAGS_RESOURCE}/{id}/");
        }

        async Task<PaginatedResult<Company>> IUserComTagsClient.FindCompaniesAsync(int id)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{TAGS_RESOURCE}/{id}/companies/");
            var paginatedResult = CreatePaginatedResult<Company>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<User>> IUserComTagsClient.FindUsersAsync(int id)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{TAGS_RESOURCE}/{id}/users/");
            var paginatedResult = CreatePaginatedResult<User>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<Tag>> IUserComTagsClient.GetAllAsync()
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{TAGS_RESOURCE}/");
            var paginatedResult = CreatePaginatedResult<Tag>(result);

            return paginatedResult;
        }

        async Task IUserComTagsClient.UpdateAsync(int id, string name)
        {
            await SendAsync<dynamic>(HttpMethod.Put, $"{TAGS_RESOURCE}/{id}/", new { name });
        }
    }
}
#pragma warning restore S4039
