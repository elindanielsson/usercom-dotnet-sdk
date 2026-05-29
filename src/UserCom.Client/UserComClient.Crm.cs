using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UserCom.Model;
using UserCom.Model.CRM;
using UserCom.Model.CRM.Requests;

#pragma warning disable S4039 // Explicit implementation required: multiple interfaces share identical method signatures with different semantics
namespace UserCom
{
    public partial class UserComClient : IUserComCrmClient
    {
        private static string COMPANY_RESOURCE = "/api/public/companies";

        async Task<AddTagResult> IUserComCrmClient.AddCompanyTagAsync(int id, string name)
        {
            var response = await SendAsync<dynamic, dynamic>(HttpMethod.Post, $"{COMPANY_RESOURCE}/{id}/add_tag/", new { name });
            var result = new AddTagResult
            {
                Created = response.created,
                TagName = response.tag.name
            };

            return result;
        }

        async Task<Company> IUserComCrmClient.CreateCompanyAsync(CreateCompanyRequest request)
        {
            var result = await SendAsync<CreateCompanyRequest, Company>(HttpMethod.Post, $"{COMPANY_RESOURCE}/", request);

            return result;
        }

        async Task IUserComCrmClient.DeleteCompanyAsync(int id)
        {
            await SendAsync(HttpMethod.Delete, $"{COMPANY_RESOURCE}/{id}");
        }

        async Task<Company> IUserComCrmClient.FindCompanyByIdAsync(int id)
        {
            var result = await SendAsync<Company>(HttpMethod.Get, $"{COMPANY_RESOURCE}/{id}/");

            return result;
        }

        async Task<PaginatedResult<Company>> IUserComCrmClient.GetAllCompaniesAsync()
        {
            var result = await SendAsync<dynamic>(HttpMethod.Get, $"{COMPANY_RESOURCE}/");
            var paginatedResult = CreatePaginatedResult<Company>(result);

            return paginatedResult;
        }

        async Task IUserComCrmClient.RemoveCompanyTagAsync(int id, string name)
        {
            await SendAsync(HttpMethod.Delete, $"{COMPANY_RESOURCE}/{id}/remove_tag/", new { name });
        }

        async Task IUserComCrmClient.SetCompanyAttributeAsync(int id, (string attribute, object value) attribute)
        {
            await SendAsync<dynamic>(HttpMethod.Post, $"{COMPANY_RESOURCE}/{id}/set_attribute/", attribute);
        }

        async Task IUserComCrmClient.SetCompanyMultipleAttributesAsync(int id, Dictionary<string, object> attributes)
        {
            await SendAsync<Dictionary<string, object>>(HttpMethod.Post, $"{COMPANY_RESOURCE}/{id}/set_multiple_attributes/", attributes);
        }

        async Task<Company> IUserComCrmClient.UpdateCompanyAsync(UpdateCompanyRequest request)
        {
            if (request.Id == default)
            {
                throw new ArgumentException($"{nameof(request.Id)} is missing or invalid", nameof(request));
            }

            var result = await SendAsync<Company>(HttpMethod.Put, $"{COMPANY_RESOURCE}/{request.Id}/");

            return result;
        }
    }
}
#pragma warning restore S4039