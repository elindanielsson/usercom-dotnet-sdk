using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using UserCom;
using UserCom.Model.Users.Requests;

namespace Tests.UserCom.Serialization;

[TestFixture]
public class UpdateOrCreateUserRequestSerializationTests
{
    private const string VerifiedMemberPropertyName = "Verified Member";
    private const string LatestMemberLoginPropertyName = "Latest Member Login";

    [Test, CustomAutoData]
    public void VerifiedMember_serializes_using_expected_property_name(string expectedVerifiedMemberValue)
    {
        var request = new UpdateOrCreateUserRequest { VerifiedMember = expectedVerifiedMemberValue };

        var serialized = JsonConvert.SerializeObject(request, UserComClient.SerializerSettings);
        var json = JObject.Parse(serialized);

        Assert.That(json.ContainsKey(VerifiedMemberPropertyName), Is.True);
        Assert.That((string?)json[VerifiedMemberPropertyName], Is.EqualTo(expectedVerifiedMemberValue));
    }

    [Test, CustomAutoData]
    public void VerifiedMember_deserializes_using_expected_property_name(string expectedVerifiedMemberValue)
    {
        var json = $"{{\"{VerifiedMemberPropertyName}\":\"{expectedVerifiedMemberValue}\"}}";
        var deserialized = JsonConvert.DeserializeObject<UpdateOrCreateUserRequest>(json, UserComClient.SerializerSettings);
        Assert.That(deserialized?.VerifiedMember, Is.EqualTo(expectedVerifiedMemberValue));
    }

    [Test, CustomAutoData]
    public void LatestMemberLogin_serializes_using_expected_property_name(DateTime expectedLatestMemberLogin)
    {
        expectedLatestMemberLogin = expectedLatestMemberLogin.ToUniversalTime();
        var request = new UpdateOrCreateUserRequest { LatestMemberLogin = expectedLatestMemberLogin };

        var serialized = JsonConvert.SerializeObject(request, UserComClient.SerializerSettings);
        var json = JObject.Parse(serialized);

        Assert.That(json.ContainsKey(LatestMemberLoginPropertyName), Is.True);
        var serializedValue = json[LatestMemberLoginPropertyName]?.ToObject<DateTime>();
        Assert.That(serializedValue, Is.EqualTo(expectedLatestMemberLogin));
    }

    [Test, CustomAutoData]
    public void LatestMemberLogin_deserializes_using_expected_property_name(DateTime expectedLatestMemberLogin)
    {
        expectedLatestMemberLogin = expectedLatestMemberLogin.ToUniversalTime();
        var json = $"{{\"{LatestMemberLoginPropertyName}\":\"{expectedLatestMemberLogin:O}\"}}";
        var deserialized = JsonConvert.DeserializeObject<UpdateOrCreateUserRequest>(json, UserComClient.SerializerSettings);
        Assert.That(deserialized?.LatestMemberLogin, Is.EqualTo(expectedLatestMemberLogin));
    }

    [TestFixture]
    public class FirstNameSerializationTests : StringValueMaxLengthConverterTestBase<UpdateOrCreateUserRequest>
    {
        protected override string GetJsonStr(string value) => $"{{\"first_name\":\"{value}\"}}";

        protected override int MaxLength => UpdateOrCreateUserRequest.FirstNameMaxLength;

        protected override string GetValue(UpdateOrCreateUserRequest obj) => obj.FirstName;

        protected override UpdateOrCreateUserRequest CreateObj(string value) => new() { FirstName = value };
    }

    [TestFixture]
    public class LastNameSerializationTests : StringValueMaxLengthConverterTestBase<UpdateOrCreateUserRequest>
    {
        protected override string GetJsonStr(string value) => $"{{\"last_name\":\"{value}\"}}";

        protected override int MaxLength => UpdateOrCreateUserRequest.LastNameMaxLength;

        protected override string GetValue(UpdateOrCreateUserRequest obj) => obj.LastName;

        protected override UpdateOrCreateUserRequest CreateObj(string value) => new() { LastName = value };
    }

    [TestFixture]
    public class CitySerializationTests : StringValueMaxLengthConverterTestBase<UpdateOrCreateUserRequest>
    {
        protected override string GetJsonStr(string value) => $"{{\"city\":\"{value}\"}}";

        protected override int MaxLength => UpdateOrCreateUserRequest.CityMaxLength;

        protected override string GetValue(UpdateOrCreateUserRequest obj) => obj.City;

        protected override UpdateOrCreateUserRequest CreateObj(string value) => new() { City = value };
    }

    [TestFixture]
    public class PhoneNumberSerializationTests : StringValueMaxLengthConverterTestBase<UpdateOrCreateUserRequest>
    {
        protected override string GetJsonStr(string value) => $"{{\"phone_number\":\"{value}\"}}";

        protected override int MaxLength => UpdateOrCreateUserRequest.PhoneNumberMaxLength;

        protected override string GetValue(UpdateOrCreateUserRequest obj) => obj.PhoneNumber;

        protected override UpdateOrCreateUserRequest CreateObj(string value) => new() { PhoneNumber = value };
    }
}