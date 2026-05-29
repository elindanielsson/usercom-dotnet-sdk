using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UserCom.Http;
using UserCom.Model;
using UserCom.Model.Attributes;
using UserCom.Model.CRM;
using UserCom.Model.Lists;
using UserCom.Model.Segments;
using UserCom.Model.Tags;
using UserCom.Model.Users;

namespace UserCom
{
    public partial class UserComClient : HttpRestClient, IUserComClient
    {
        public UserComClient(UserComAuthenticator userComAuthenticator) : base(CreateClient(userComAuthenticator), SerializerSettings)
        {

        }

        private static HttpClient CreateClient(UserComAuthenticator authenticator)
        {
            return new HttpClient(authenticator) { BaseAddress = authenticator.ServiceUri };
        }

        private PaginatedResult<T> CreatePaginatedResult<T>(dynamic obj)
        {
            Lazy<PaginatedResult<T>>? CreateLazyRef(string url)
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return null;
                }

                return new Lazy<PaginatedResult<T>>(() =>
                {
                    try
                    {
                        var result = SendAsync<dynamic>(HttpMethod.Get, url).Result;

                        return CreatePaginatedResult<T>(result);
                    }
                    catch (UserComClientException clientException) when (clientException.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return new PaginatedResult<T> { Results = Array.Empty<T>() };
                    }
                    catch (AggregateException aggregateException) when (aggregateException.InnerException is UserComClientException { StatusCode: System.Net.HttpStatusCode.NotFound })
                    {
                        return new PaginatedResult<T> { Results = Array.Empty<T>() };
                    }
                });
            }

            return new PaginatedResult<T>
            {
                Count = obj.count,
                Results = obj.results.ToObject<List<T>>(),
                Next = CreateLazyRef(obj.next),
                Previous = CreateLazyRef(obj.previous)
            };
        }

        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public IUserComUsersClient Users => this;

        public IUserComUserSegmentClient UserSegments => this;

        public IUserComCustomIdUsersClient CustomIdUsers => this;

        public IUserComCrmClient Crm => this;

        public IUserComCustomIdCrmClient CustomIdCrm => this;

        public IUserComListsClient Lists => this;

        public IUserComTagsClient Tags => this;

        public IUserComAttributesClient Attributes => this;
    }
}
