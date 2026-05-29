using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UserCom.Model.CRM;
using UserCom.Model.CRM.Requests;

#pragma warning disable S4039 // Explicit implementation required: multiple interfaces share identical method signatures with different semantics
namespace UserCom
{
    public partial class UserComClient : IUserComCustomIdCrmClient
    {
        private static string CUSTOMIDCOMPANY_RESOURCE = "/api/public/companies-by-id";

        async Task<AddTagResult> IUserComCustomIdCrmClient.AddCompanyTagAsync(string companyId, string name)
        {
            var response = await SendAsync<dynamic, dynamic>(HttpMethod.Post, $"{CUSTOMIDCOMPANY_RESOURCE}/{companyId}/add_tag/", new { name });
            var result = new AddTagResult
            {
                Created = response.created,
                TagName = response.tag.name
            };

            return result;
        }

        async Task IUserComCustomIdCrmClient.DeleteCompanyAsync(string companyId)
        {
            await SendAsync(HttpMethod.Delete, $"{CUSTOMIDCOMPANY_RESOURCE}/{companyId}/");
        }

        async Task<Company> IUserComCustomIdCrmClient.FindCompanyByIdAsync(string companyId)
        {
            var result = await SendAsync<Company>(HttpMethod.Get, $"{CUSTOMIDCOMPANY_RESOURCE}/{companyId}/");

            return result;
        }

        async Task IUserComCustomIdCrmClient.RemoveCompanyTagAsync(string companyId, string name)
        {
            await SendAsync(HttpMethod.Delete, $"{CUSTOMIDCOMPANY_RESOURCE}/{companyId}/remove_tag/", new { name });
        }

        async Task IUserComCustomIdCrmClient.SetCompanyAttributeAsync(string companyId, (string attribute, object value) attribute)
        {
            await SendAsync<dynamic>(HttpMethod.Post, $"{CUSTOMIDCOMPANY_RESOURCE}/{companyId}/set_attribute/", attribute);
        }

        async Task IUserComCustomIdCrmClient.SetCompanyMultipleAttributesAsync(string companyId, Dictionary<string, object> attributes)
        {
            await SendAsync(HttpMethod.Post, $"{CUSTOMIDCOMPANY_RESOURCE}/{companyId}/set_multiple_attributes/", attributes);
        }

        async Task<Company> IUserComCustomIdCrmClient.UpdateCompanyAsync(UpdateCustomIdCompanyRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CompanyId))
            {
                throw new ArgumentException($"{nameof(request.CompanyId)} is missing or invalid", nameof(request));
            }

            var result = await SendAsync<Company>(HttpMethod.Put, $"{CUSTOMIDCOMPANY_RESOURCE}/{request.CompanyId}/");

            return result;
        }
    }
}
#pragma warning restore S4039
