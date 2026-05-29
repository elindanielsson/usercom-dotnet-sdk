using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UserCom.Model;
using UserCom.Model.Segments;
using UserCom.Model.Users;
using UserCom.Model.Users.Requests;

#pragma warning disable S4039 // Explicit implementation required: multiple interfaces share identical method signatures with different semantics
namespace UserCom
{
    public partial class UserComClient : IUserComCustomIdUsersClient
    {
        private static string CUSTOMIDUSER_RESOURCE = "/api/public/users-by-id";

        async Task<int> IUserComCustomIdUsersClient.AddListAsync(string userId, int listId)
        {
            var result = await SendAsync<dynamic, dynamic>(HttpMethod.Post, $"{CUSTOMIDUSER_RESOURCE}/{userId}/add_to_list/", new { list = listId });

            return result.id;
        }

        async Task<AddTagResult> IUserComCustomIdUsersClient.AddTagAsync(string userId, string tagName)
        {
            var response = await SendAsync<dynamic, dynamic>(HttpMethod.Post, $"{CUSTOMIDUSER_RESOURCE}/{userId}/add_tag/", new { name = tagName });
            var result = new AddTagResult
            {
                Created = response.created,
                TagName = response.tag.name
            };

            return result;
        }

        async Task IUserComCustomIdUsersClient.AddManyTagsAsync(string userId, IEnumerable<string> tagNames)
        {
            await SendAsync(HttpMethod.Post, $"{CUSTOMIDUSER_RESOURCE}/{userId}/add_many_tags/", new { tag_names = tagNames });
        }

        async Task IUserComCustomIdUsersClient.DeleteAsync(string userId)
        {
            await SendAsync(HttpMethod.Delete, $"{CUSTOMIDUSER_RESOURCE}/{userId}/");
        }

        async Task<User> IUserComCustomIdUsersClient.FindByCustomIdAsync(string userId)
        {
            var result = await SendAsync<User>(HttpMethod.Get, $"{CUSTOMIDUSER_RESOURCE}/{userId}/");

            return result;
        }

        async Task<PaginatedResult<UserEmail>> IUserComCustomIdUsersClient.GetEmailsAsync(string userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{CUSTOMIDUSER_RESOURCE}/{userId}/emails/");
            var paginatedResult = CreatePaginatedResult<UserEmail>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<UserEvent>> IUserComCustomIdUsersClient.GetEventsAsync(string userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{CUSTOMIDUSER_RESOURCE}/{userId}/events/");
            var paginatedResult = CreatePaginatedResult<UserEvent>(result);

            return paginatedResult;
        }

        async Task<AddEventResult> IUserComCustomIdUsersClient.AddEventAsync(string userId, AddEventRequest request)
        {
            var result = await SendAsync<AddEventRequest, AddEventResult>(HttpMethod.Post, $"{CUSTOMIDUSER_RESOURCE}/{userId}/events/", request);
            return result;
        }

        async Task<PaginatedResult<UserPingHit>> IUserComCustomIdUsersClient.GetPingHitsAsync(string userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{CUSTOMIDUSER_RESOURCE}/{userId}/ping_hits/");
            var paginatedResult = CreatePaginatedResult<UserPingHit>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<UserProductEvent>> IUserComCustomIdUsersClient.GetProductEventsAsync(string userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{CUSTOMIDUSER_RESOURCE}/{userId}/product_events/");
            var paginatedResult = CreatePaginatedResult<UserProductEvent>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<Segment>> IUserComCustomIdUsersClient.GetSegmentsAsync(string userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{CUSTOMIDUSER_RESOURCE}/{userId}/segments/");
            var paginatedResult = CreatePaginatedResult<Segment>(result);

            return paginatedResult;
        }

        async Task<PaginatedResult<UserTimeline>> IUserComCustomIdUsersClient.GetTimelineAsync(string userId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{CUSTOMIDUSER_RESOURCE}/{userId}/timeline/");
            var paginatedResult = CreatePaginatedResult<UserTimeline>(result);

            return paginatedResult;
        }

        async Task<string> IUserComCustomIdUsersClient.MassUpdateAttributeAsync(IEnumerable<string> userIds, string attribute, string value)
        {
            var result = await SendAsync<dynamic, dynamic>(HttpMethod.Put, $"{CUSTOMIDUSER_RESOURCE}/mass_update_standard_attributes/", new { ids = userIds, attribute, value });

            return result.status;
        }

        async Task IUserComCustomIdUsersClient.MassUpdateCustomAttributeAsync(IEnumerable<string> userIds, string attribute, object value)
        {
            await SendAsync<dynamic>(HttpMethod.Put, $"{CUSTOMIDUSER_RESOURCE}/mass_update_custom_attributes/", new { ids = userIds, attribute, value });
        }

        async Task IUserComCustomIdUsersClient.RemoveAttributeAsync(string userId, string attributeName)
        {
            await SendAsync<dynamic>(HttpMethod.Post, $"{CUSTOMIDUSER_RESOURCE}/{userId}/remove_attribute/", new { attribute = attributeName });
        }

        async Task IUserComCustomIdUsersClient.RemoveListAsync(string userId, int listId)
        {
            await SendAsync<dynamic>(HttpMethod.Post, $"{CUSTOMIDUSER_RESOURCE}/{userId}/remove_from_list/", new { list = listId });
        }

        async Task IUserComCustomIdUsersClient.RemoveTagAsync(string userId, string tagName)
        {
            await SendAsync(HttpMethod.Post, $"{CUSTOMIDUSER_RESOURCE}/{userId}/remove_tag/", new { name = tagName });
        }

        async Task IUserComCustomIdUsersClient.SetAttributeAsync(string userId, (string attribute, object value) attribute)
        {
            await SendAsync<dynamic>(HttpMethod.Post, $"{CUSTOMIDUSER_RESOURCE}/{userId}/set_attribute/", attribute);
        }

        async Task IUserComCustomIdUsersClient.SetMultipleAttributesAsync(string userId, Dictionary<string, object> attributes)
        {
            await SendAsync<Dictionary<string, object>>(HttpMethod.Post, $"{CUSTOMIDUSER_RESOURCE}/{userId}/set_multiple_attributes/", attributes);
        }

        async Task<User> IUserComCustomIdUsersClient.UpdateAsync(UpdateCustomIdUserRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new ArgumentException($"{nameof(request.UserId)} is missing or invalid", nameof(request));
            }

            var result = await SendAsync<UpdateCustomIdUserRequest, User>(HttpMethod.Put, $"{CUSTOMIDUSER_RESOURCE}/{request.UserId}/", request);

            return result;
        }
    }
}
#pragma warning restore S4039