using System;
using System.Globalization;
using Newtonsoft.Json;
using NUnit.Framework;
using UserCom;
using UserCom.Model.Users;

namespace Tests.UserCom.Serialization
{
    [TestFixture]
    public class UserSerializationTests
    {
        [TestFixture]
        public class UserAttributeTests
        {
            private const float FloatDecimalDivisor = 10f;
            [Test, CustomAutoData]
            public void String_attribute_value_can_be_deserialized(string value)
            {
                var userStr = $"{{\"attributes\":[{{\"value\":\"{value}\"}}]}}";

                var result = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.Attributes, Is.Not.Null);
                    Assert.That(result.Attributes.Count, Is.EqualTo(1));
                    Assert.That(result.Attributes[0].Value, Is.EqualTo(value));
                });
            }

            [Test, CustomAutoData]
            public void String_attribute_value_can_be_serialized(string value)
            {
                var userStr = $"{{\"attributes\":[{{\"value\":\"{value}\"}}]}}";

                var user = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                var result = JsonConvert.SerializeObject(user, UserComClient.SerializerSettings);

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result, Is.EqualTo(userStr));
                });
            }

            [Test, CustomAutoData]
            public void Boolean_attribute_value_can_be_deserialized(bool value)
            {
                var strValue = value.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();

                var userStr = $"{{\"attributes\":[{{\"value\":{strValue}}}]}}";

                var result = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.Attributes, Is.Not.Null);
                    Assert.That(result.Attributes.Count, Is.EqualTo(1));
                    Assert.That(result.Attributes[0].Value, Is.EqualTo(strValue));
                });
            }

            [Test, CustomAutoData]
            public void Boolean_attribute_value_can_be_serialized(bool value)
            {
                var strValue = value.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();

                var userStr = $"{{\"attributes\":[{{\"value\":{strValue}}}]}}";

                var user = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                var result = JsonConvert.SerializeObject(user, UserComClient.SerializerSettings);

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result, Is.EqualTo(userStr));
                });
            }

            [Test, CustomAutoData]
            public void Integer_attribute_value_can_be_deserialized(int value)
            {
                var userStr = $"{{\"attributes\":[{{\"value\":{value}}}]}}";

                var result = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.Attributes, Is.Not.Null);
                    Assert.That(result.Attributes.Count, Is.EqualTo(1));
                    Assert.That(result.Attributes[0].Value, Is.EqualTo(value.ToString(CultureInfo.InvariantCulture)));
                });
            }

            [Test, CustomAutoData]
            public void Integer_attribute_value_can_be_serialized(int value)
            {
                var userStr = $"{{\"attributes\":[{{\"value\":{value}}}]}}";

                var user = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                var result = JsonConvert.SerializeObject(user, UserComClient.SerializerSettings);

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result, Is.EqualTo(userStr));
                });
            }

            [Test, CustomAutoData]
            public void Float_attribute_value_can_be_deserialized(float value)
            {
                value /= FloatDecimalDivisor;
                var strValue = value.ToString(CultureInfo.InvariantCulture);

                var userStr = $"{{\"attributes\":[{{\"value\":{strValue}}}]}}";

                var result = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.Attributes, Is.Not.Null);
                    Assert.That(result.Attributes.Count, Is.EqualTo(1));
                    Assert.That(result.Attributes[0].Value, Is.EqualTo(strValue));
                });
            }

            [Test, CustomAutoData]
            public void Float_attribute_value_can_be_serialized(float value)
            {
                value /= FloatDecimalDivisor;
                var strValue = value.ToString(CultureInfo.InvariantCulture);

                var userStr = $"{{\"attributes\":[{{\"value\":{strValue}}}]}}";

                var user = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                var result = JsonConvert.SerializeObject(user, UserComClient.SerializerSettings);

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result, Is.EqualTo(userStr));
                });
            }

            [Test, CustomAutoData]
            public void DateTime_attribute_value_can_be_deserialized(DateTime value)
            {
                var strValue = value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

                var userStr = $"{{\"attributes\":[{{\"value\":\"{strValue}\"}}]}}";

                var result = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.Attributes, Is.Not.Null);
                    Assert.That(result.Attributes.Count, Is.EqualTo(1));
                    Assert.That(result.Attributes[0].Value, Is.EqualTo(strValue));
                });
            }

            [Test, CustomAutoData]
            public void DateTime_attribute_value_can_be_serialized(DateTime value)
            {
                var strValue = value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

                var userStr = $"{{\"attributes\":[{{\"value\":\"{strValue}\"}}]}}";

                var user = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                var result = JsonConvert.SerializeObject(user, UserComClient.SerializerSettings);

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result, Is.EqualTo(userStr));
                });
            }

            [Test, CustomAutoData]
            public void Date_attribute_value_can_be_deserialized(DateTime value)
            {
                var strValue = value.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                var userStr = $"{{\"attributes\":[{{\"value\":\"{strValue}\"}}]}}";

                var result = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.Attributes, Is.Not.Null);
                    Assert.That(result.Attributes.Count, Is.EqualTo(1));
                    Assert.That(result.Attributes[0].Value, Is.EqualTo(strValue));
                });
            }

            [Test, CustomAutoData]
            public void Date_attribute_value_can_be_serialized(DateTime value)
            {
                var strValue = value.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                var userStr = $"{{\"attributes\":[{{\"value\":\"{strValue}\"}}]}}";

                var user = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                var result = JsonConvert.SerializeObject(user, UserComClient.SerializerSettings);

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result, Is.EqualTo(userStr));
                });
            }

            [Test, CustomAutoData]
            public void Fixed_aka_array_attribute_value_can_be_deserialized(
                string value)
            {
                var userStr = $"{{\"attributes\":[{{\"value\":[\"{value}\"]}}]}}";

                var result = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.Attributes, Is.Not.Null);
                    Assert.That(result.Attributes.Count, Is.EqualTo(1));
                    Assert.That(result.Attributes[0].Value, Is.EqualTo($"[\"{value}\"]"));
                });
            }

            [Test, CustomAutoData]
            public void Fixed_aka_array_attribute_value_can_be_serialized(
                string value)
            {
                var userStr = $"{{\"attributes\":[{{\"value\":[\"{value}\"]}}]}}";

                var user = JsonConvert.DeserializeObject<User>(userStr, UserComClient.SerializerSettings);
                var result = JsonConvert.SerializeObject(user, UserComClient.SerializerSettings);

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result, Is.EqualTo(userStr));
                });
            }
        }
    }
}