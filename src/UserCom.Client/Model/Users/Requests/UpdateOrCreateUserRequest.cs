#nullable enable

using System;
using Newtonsoft.Json;
using UserCom.Serialization;

namespace UserCom.Model.Users.Requests
{
    public class UpdateOrCreateUserRequest
    {
        public const int CityMaxLength = 64;
        public const int FirstNameMaxLength = 40;
        public const int LastNameMaxLength = 40;
        public const int PhoneNumberMaxLength = 64;

        [JsonProperty("assigned_to")]
        public int? AssignedTo { get; set; }

        [JsonProperty("city"), JsonConverter(typeof(StringValueMaxLengthConverter), CityMaxLength)]
        public string? City { get; set; }

        [JsonProperty("company_id")]
        public int? CompanyId { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("facebook_url")]
        public string? FacebookUrl { get; set; }

        [JsonProperty("first_name"), JsonConverter(typeof(StringValueMaxLengthConverter), FirstNameMaxLength)]
        public string? FirstName { get; set; }

        [JsonProperty("gender")]
        public UserGender? Gender { get; set; }

        [JsonProperty("google_url")]
        public string? GoogleUrl { get; set; }

        [JsonProperty("gravatar_url")]
        public string? GravatarUrl { get; set; }

        [JsonProperty("last_name"), JsonConverter(typeof(StringValueMaxLengthConverter), LastNameMaxLength)]
        public string? LastName { get; set; }

        [JsonProperty("linkedin_url")]
        public string? LinkedinUrl { get; set; }

        [JsonProperty("notifications")]
        public bool? Notifications { get; set; }

        [JsonProperty("phone_number"), JsonConverter(typeof(StringValueMaxLengthConverter), PhoneNumberMaxLength)]
        public string? PhoneNumber { get; set; }

        [JsonProperty("region")]
        public string? Region { get; set; }

        [JsonProperty("score")]
        public int? Score { get; set; }

        [JsonProperty("status")]
        public UserStatus? Status { get; set; }

        [JsonProperty("timezone")]
        public string? Timezone { get; set; }

        [JsonProperty("twitter_url")]
        public string? TwitterUrl { get; set; }

        [JsonProperty("unsubscribed")]
        public bool? Unsubscribed { get; set; }

        [JsonProperty("user_id")]
        public string? UserId { get; set; }

        [JsonProperty("birth_year")]
        public int? BirthYear { get; set; }

        [JsonProperty("job_title")]
        public string? JobTitle { get; set; }

        [JsonProperty("nationality")]
        public string? Nationality { get; set; }

        [JsonProperty("zip")]
        public string? Zip { get; set; }

        [JsonProperty("previous_education")]
        public string? PreviousEducation { get; set; }

        [JsonProperty("next_education_start_year")]
        public int? NextEducationStartYear { get; set; }

        [JsonProperty("Verified Member")]
        public string? VerifiedMember { get; set; }

        [JsonProperty("Latest Member Login")]
        public DateTime? LatestMemberLogin { get; set; }
    }
}