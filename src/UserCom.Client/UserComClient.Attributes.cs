using System.Net.Http;
using System.Threading.Tasks;
using UserCom.Model;
using UserCom.Model.Attributes;

// Explicit interface implementation is intentional: multiple interfaces on UserComClient share method names
// (e.g. DeleteAsync, GetAllAsync) with identical signatures but different semantics. Implicit implementation
// would cause compile-time ambiguity that cannot be resolved with public methods alone.
#pragma warning disable S4039
namespace UserCom
{
    public partial class UserComClient : IUserComAttributesClient
    {
        private static string ATTRIBUTE_RESOURCE = "/api/public/attributes";
        private static string ATTRIBUTETYPE_RESOURCE = "/api/public/attribute-type";

        async Task<Attribute> IUserComAttributesClient.CreateAsync(ValueType valueType, string name, CreationContentType contentType)
        {
            var result = await SendAsync<dynamic, Attribute>(HttpMethod.Post, $"{ATTRIBUTE_RESOURCE}/", new { valueType, name, contentType });

            return result;
        }

        async Task IUserComAttributesClient.DeleteAsync(int id)
        {
            await SendAsync(HttpMethod.Delete, $"{ATTRIBUTE_RESOURCE}/{id}/");
        }

        async Task<Attribute> IUserComAttributesClient.FindByIdAsync(int id)
        {
            var result = await SendAsync<Attribute>(HttpMethod.Get, $"{ATTRIBUTE_RESOURCE}/{id}/");

            return result;
        }

        async Task<PaginatedResult<Attribute>> IUserComAttributesClient.GetAllAsync()
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{ATTRIBUTE_RESOURCE}/");
            var paginatedResult = CreatePaginatedResult<Attribute>(result);

            return paginatedResult;
        }

        async Task<ValueType> IUserComAttributesClient.GetCompanyValueTypeAsync(string urlFriendlyName)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{ATTRIBUTETYPE_RESOURCE}/{urlFriendlyName}/companies/");

            return result.value_type.ToObject<ValueType>();
        }

        async Task<ValueType> IUserComAttributesClient.GetUserValueTypeAsync(string urlFriendlyName)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{ATTRIBUTETYPE_RESOURCE}/{urlFriendlyName}/users/");

            return result.value_type.ToObject<ValueType>();
        }
    }
}
#pragma warning restore S4039
