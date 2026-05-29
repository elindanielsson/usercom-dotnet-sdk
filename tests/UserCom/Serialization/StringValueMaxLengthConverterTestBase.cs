using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoFixture;
using Newtonsoft.Json;
using NUnit.Framework;
using UserCom;

namespace Tests.UserCom.Serialization;

public abstract class StringValueMaxLengthConverterTestBase<TType> where TType : class, new()
{
    protected abstract string GetJsonStr(string value);

    protected abstract int MaxLength { get; }

    protected abstract string GetValue(TType obj);

    protected abstract TType CreateObj(string value);

    private static string GenerateString(IFixture fixture, int length)
    {
        var sb = new StringBuilder();
        while (sb.Length < length)
        {
            sb.Append(fixture.Create<string>());
        }

        return sb.Length > length ? sb.ToString(0, length) : sb.ToString();
    }

    [Test, CustomAutoData]
    public void Empty_object_can_be_serialized(
        IFixture fixture)
    {
        var obj = new TType();

        var result = JsonConvert.SerializeObject(obj, UserComClient.SerializerSettings);

        Assert.That(result, Is.EqualTo("{}"));
    }

    [Test, CustomAutoData]
    public void Empty_object_can_be_deserialized(
        IFixture fixture)
    {
        const string objStr = "{}";

        var result = JsonConvert.DeserializeObject<TType>(objStr, UserComClient.SerializerSettings);

        Assert.That(GetValue(result), Is.Null);
    }

    [Test, CustomAutoData]
    public void Property_value_with_length_equal_to_MaxLength_can_be_serialized(
        IFixture fixture)
    {
        var obj = CreateObj(GenerateString(fixture, MaxLength));

        var result = JsonConvert.SerializeObject(obj, UserComClient.SerializerSettings);

        Assert.That(result, Is.EqualTo(GetJsonStr(GetValue(obj))));
    }

    [Test, CustomAutoData]
    public void Property_value_with_length_equal_to_MaxLength_can_be_deserialized(
        IFixture fixture)
    {
        var value = GenerateString(fixture, MaxLength);
        var objStr = GetJsonStr(value);

        var result = JsonConvert.DeserializeObject<TType>(objStr, UserComClient.SerializerSettings);

        Assert.That(GetValue(result), Is.EqualTo(value));
    }

    [Test, CustomAutoData]
    public void Property_value_with_length_less_than_MaxLength_can_be_serialized(
        IFixture fixture)
    {
        var value = GenerateString(fixture, RandomNumberGenerator.GetInt32(1, MaxLength));
        var obj = CreateObj(value);
        
        var result = JsonConvert.SerializeObject(obj, UserComClient.SerializerSettings);

        Assert.That(result, Is.EqualTo(GetJsonStr(value)));
    }

    [Test, CustomAutoData]
    public void Property_value_with_length_less_than_MaxLength_can_be_deserialized(
        IFixture fixture)
    {
        var value = GenerateString(fixture, RandomNumberGenerator.GetInt32(1, MaxLength));
        var objStr = GetJsonStr(value);

        var result = JsonConvert.DeserializeObject<TType>(objStr, UserComClient.SerializerSettings);

        Assert.That(GetValue(result), Is.EqualTo(value));
    }

    [Test, CustomAutoData]
    public void Property_value_with_length_more_than_MaxLength_can_be_serialized(
        IFixture fixture)
    {
        var value = GenerateString(fixture, RandomNumberGenerator.GetInt32(MaxLength + 1, fixture.Create<Generator<int>>().First(i => i > MaxLength + 1)));
        var obj = CreateObj(value);

        var result = JsonConvert.SerializeObject(obj, UserComClient.SerializerSettings);

        var expectedValue = $"{value.Substring(0, MaxLength - 3)}...";
        Assert.That(result, Is.EqualTo(GetJsonStr(expectedValue)));
    }

    [Test, CustomAutoData]
    public void Property_value_with_length_more_than_MaxLength_can_be_deserialized(
        IFixture fixture)
    {
        var value = GenerateString(fixture, RandomNumberGenerator.GetInt32(MaxLength + 1, fixture.Create<Generator<int>>().First(i => i > MaxLength + 1)));
        var objStr = GetJsonStr(value);

        var result = JsonConvert.DeserializeObject<TType>(objStr, UserComClient.SerializerSettings);

        Assert.That(GetValue(result), Is.EqualTo(value));
    }

}