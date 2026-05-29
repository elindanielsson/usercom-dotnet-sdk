using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UserCom.Http;
using UserCom.Model;
using UserCom.Model.Segments;
using UserCom.Model.Users;
using UserCom.Model.Users.Requests;

#pragma warning disable S4039 // Explicit implementation required: multiple interfaces share identical method signatures with different semantics
namespace UserCom
{
    public partial class UserComClient : IUserComUsersClient
    {
        private static string USER_RESOURCE = "/api/public/users";

        async Task<int> IUserComUsersClient.AddListAsync(int userId, int listId)
        {
            var result = await SendAsync<dynamic, dynamic>(HttpMethod.Post, $"{USER_RESOURCE}/{userId}/add_to_list/", new { list = listId });

            return result.id;
        }

        async Task<AddTagResult> IUserComUsersClient.AddTagAsync(int userId, string tagName)
        {
            var response = await SendAsync<dynamic, dynamic>(HttpMethod.Post, $"{USER_RESOURCE}/{userId}/add_tag/", new { name = tagName });
            var result = new AddTagResult
            {
                Created = response.created,
                TagName = response.tag.name
            };

            return result;
        }

        async Task<User> IUserComUsersClient.CreateAsync(CreateUserRequest request)
        {
            var result = await SendAsync<CreateUserRequest, User>(HttpMethod.Post, $"{USER_RESOURCE}/", request);

            return result;
        }

        async Task IUserComUsersClient.DeleteAsync(int id)
        {
            await SendAsync(HttpMethod.Delete, $"{USER_RESOURCE}/{id}/");
        }

        async Task<PaginatedResult<User>> IUserComUsersClient.FilterAsync(IEnumerable<UserFilter> filters)
        {
            var builder = new HttpQueryStringBuilder();
            foreach (var filter in filters)
            {
                var param = filter.ToQueryParam();
                builder.Add(param.key, param.value);
            }

            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{USER_RESOURCE}/", builder.BuildQuery());
            var paginatedResult = CreatePaginatedResult<User>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<User>> IUserComUsersClient.FilterByCustomAttributesAsync(IEnumerable<CustomAttributeFilter> filters)
        {
            var builder = new HttpQueryStringBuilder();
            foreach (var filter in filters)
            {
                var param = filter.ToQueryParam();
                builder.Add(param.key, param.value);
            }

            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{USER_RESOURCE}/search/", builder.BuildQuery());
            var paginatedResult = CreatePaginatedResult<User>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<User>> IUserComUsersClient.FindByDateAsync(DateSearchType searchType, DateTime min, DateTime max)
        {
            var builder = new HttpQueryStringBuilder();
            builder.Add("search", searchType.ToString().ToSnakeCase());
            builder.Add("min", new DateTimeOffset(min.ToUniversalTime()).ToUnixTimeSeconds());
            builder.Add("max", new DateTimeOffset(max.ToUniversalTime()).ToUnixTimeSeconds());

            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{USER_RESOURCE}/search/", builder.BuildQuery());
            var paginatedResult = CreatePaginatedResult<User>(result);

            return paginatedResult;
        }

        async Task<IReadOnlyList<User>> IUserComUsersClient.FindByEmailAsync(string email)
        {
            var builder = new HttpQueryStringBuilder();
            builder.Add("email", email);
            builder.Add("many", true);

            var result = await SendAsync<User[]>(HttpMethod.Get, $"{USER_RESOURCE}/search/", builder.BuildQuery());

            return result;
        }

        async Task<User> IUserComUsersClient.FindByIdAsync(int id)
        {
            var result = await SendAsync<User>(HttpMethod.Get, $"{USER_RESOURCE}/{id}/");

            return result;
        }

        async Task<User> IUserComUsersClient.FindByKeyAsync(string key)
        {
            var builder = new HttpQueryStringBuilder();
            builder.Add("key", key);

            var result = await SendAsync<User>(HttpMethod.Get, $"{USER_RESOURCE}/search/", builder.BuildQuery());

            return result;
        }

        async Task<User> IUserComUsersClient.FindByPhoneNumberAsync(string phoneNumber)
        {
            var builder = new HttpQueryStringBuilder();
            builder.Add("phone_number", phoneNumber);

            var result = await SendAsync<User>(HttpMethod.Get, $"{USER_RESOURCE}/search/", builder.BuildQuery());

            return result;
        }

        async Task<PaginatedResult<UserEmail>> IUserComUsersClient.GetEmailsAsync(int userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{USER_RESOURCE}/{userId}/emails/");
            var paginatedResult = CreatePaginatedResult<UserEmail>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<UserEvent>> IUserComUsersClient.GetEventsAsync(int userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{USER_RESOURCE}/{userId}/events/");
            var paginatedResult = CreatePaginatedResult<UserEvent>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<UserPingHit>> IUserComUsersClient.GetPingHitsAsync(int userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{USER_RESOURCE}/{userId}/ping_hits/");
            var paginatedResult = CreatePaginatedResult<UserPingHit>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<UserProductEvent>> IUserComUsersClient.GetProductEventsAsync(int userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{USER_RESOURCE}/{userId}/product_events/");
            var paginatedResult = CreatePaginatedResult<UserProductEvent>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<Segment>> IUserComUsersClient.GetSegmentsAsync(int userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{USER_RESOURCE}/{userId}/segments/");
            var paginatedResult = CreatePaginatedResult<Segment>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<UserTimeline>> IUserComUsersClient.GetTimelineAsync(int userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{USER_RESOURCE}/{userId}/timeline/");
            var paginatedResult = CreatePaginatedResult<UserTimeline>(result);

            return paginatedResult;
        }

        async Task<string> IUserComUsersClient.MassUpdateAttributeAsync(IEnumerable<int> userIds, string attribute, string value)
        {
            var result = await SendAsync<dynamic, dynamic>(HttpMethod.Put, $"{USER_RESOURCE}/mass_update_standard_attributes/", new { ids = userIds, attribute, value });

            return result.status;
        }

        async Task IUserComUsersClient.MassUpdateCustomAttributeAsync(IEnumerable<int> userIds, string attribute, object value)
        {
            await SendAsync<dynamic>(HttpMethod.Put, $"{USER_RESOURCE}/mass_update_custom_attributes/", new { ids = userIds, attribute, value });
        }

        async Task IUserComUsersClient.RemoveAttributeAsync(int userId, string attributeName)
        {
            await SendAsync<dynamic>(HttpMethod.Delete, $"{USER_RESOURCE}/{userId}/remove_attribute/", new { attribute = attributeName });
        }

        async Task IUserComUsersClient.RemoveListAsync(int userId, int listId)
        {
            await SendAsync<dynamic>(HttpMethod.Delete, $"{USER_RESOURCE}/{userId}/remove_from_list/", new { list = listId });
        }

        async Task IUserComUsersClient.RemoveTagAsync(int userId, string tagName)
        {
            await SendAsync(HttpMethod.Delete, $"{USER_RESOURCE}/{userId}/remove_tag/", new { name = tagName });
        }

        async Task IUserComUsersClient.SetAttributeAsync(int userId, (string attribute, object value) attribute)
        {
            await SendAsync<dynamic>(HttpMethod.Post, $"{USER_RESOURCE}/{userId}/set_attribute/", attribute);
        }

        async Task IUserComUsersClient.SetMultipleAttributesAsync(int userId, Dictionary<string, object> attributes)
        {
            await SendAsync<Dictionary<string, object>>(HttpMethod.Post, $"{USER_RESOURCE}/{userId}/set_multiple_attributes/", attributes);
        }

        async Task<User> IUserComUsersClient.UpdateAsync(UpdateUserRequest request)
        {
            if (request.Id == default)
            {
                throw new ArgumentException($"{nameof(request.Id)} is missing or invalid", nameof(request));
            }

            var result = await SendAsync<UpdateUserRequest, User>(HttpMethod.Put, $"{USER_RESOURCE}/{request.Id}/", request);

            return result;
        }

        async Task<UpdateOrCreateUserResponse> IUserComUsersClient.UpdateOrCreateAsync(UpdateOrCreateUserRequest request)
        {
            var result = await SendAsync<UpdateOrCreateUserRequest, UpdateOrCreateUserResponse>(HttpMethod.Post, $"{USER_RESOURCE}/update_or_create/", request);

            return result;
        }
    }
}
#pragma warning restore S4039