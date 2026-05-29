using System.Net.Http;
using System.Threading.Tasks;
using UserCom.Model;
using UserCom.Model.Segments;
using UserCom.Model.Users;

// Explicit interface implementation is intentional: multiple interfaces on UserComClient share method names
// (e.g. DeleteAsync, GetAllAsync) with identical signatures but different semantics. Implicit implementation
// would cause compile-time ambiguity that cannot be resolved with public methods alone.
#pragma warning disable S4039
namespace UserCom
{
    public partial class UserComClient : IUserComUserSegmentClient
    {
        private static string SEGMENTS_RESOURCE = "/api/public/segments";

        async Task<PaginatedResult<User>> IUserComUserSegmentClient.GetAllUsersInUserSegmentAsync(string segmentId)
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{SEGMENTS_RESOURCE}/{segmentId}/users/");
            var paginatedResult = CreatePaginatedResult<User>(result);

            return paginatedResult;
        }
    }
}
#pragma warning restore S4039
