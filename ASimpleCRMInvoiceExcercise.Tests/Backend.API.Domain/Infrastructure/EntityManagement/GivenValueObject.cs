using System;
using Bogus;
using Value;
using Xunit;

namespace Backend.API.Tests.Backend.API.Domain.Infrastructure.EntityManagement
{
    public class SomeValueObject : ValueObject
    {
        public string SomeProperty { get; set; }
    }
    public class GivenValueObject
    {
        private SomeValueObject _instance1;
        private SomeValueObject _instance2;

        public GivenValueObject()
        {
            _instance1 = new SomeValueObject();
            _instance2 = new SomeValueObject();
        }

        [Fact]
        public void Instance1_IsDifferentInstanceThen_Instance2()
        {
            Assert.False(Object.ReferenceEquals(_instance1, _instance2));
        }

        [Fact]
        public void WhenWeCompareAValueObjectWithOtherInstnaceWithSamePropertyValues_ShouldBeEqual()
        {
            var randomString = new Faker().Random.AlphaNumeric(10);
            ;
            _instance1.SomeProperty = randomString;
            _instance2.SomeProperty = randomString;

            Assert.Equal(_instance1, _instance2);
        }
        [Fact]
        public void WhenWeCompareAValueObjectWithOtherInstanceWithDifferentPropertyValues_ShouldNotEqual()
        {
            string randomString1 = null;
            string randomString2 = null;

            while (randomString1 == randomString2)
            {
                randomString1 = new Faker().Random.AlphaNumeric(10);

                randomString2 = new Faker().Random.AlphaNumeric(10);
            }

            ;
            _instance1.SomeProperty = randomString1;
            _instance2.SomeProperty = randomString2;

            Assert.NotEqual(_instance1, _instance2);
        }
    }
}
