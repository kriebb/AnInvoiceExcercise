using System;
using Bogus;
using Xunit;

namespace Backend.API.Tests.Backend.API.Domain.Infrastructure.EntityManagement
{
    public class GivenSomeEntity
    {
        private readonly SomeEntity _someEntity1;
        private readonly SomeEntity _someEntity2;

        public GivenSomeEntity()
        {
            _someEntity1 = new SomeEntity();

            _someEntity2 = new SomeEntity();
        }

        [Fact]
        public void SomeEntity1_And_2_ShouldBeDifferentInstance()
        {
            Assert.False(Object.ReferenceEquals(_someEntity1, _someEntity2));
        }

        [Fact]
        public void WhenInstance1_Instance2_HaveSameId_ShouldBeEqual()
        {
            var someId = new Faker().Random.Guid();
            _someEntity1.Id = someId;
            _someEntity2.Id = someId;

            Assert.Equal(_someEntity1, _someEntity2);
        }

        [Fact]
        public void WhenInstance1_Instance2_HaveBoth0_ShouldNotBeEqual()
        {
            var someId = Guid.Empty;
            _someEntity1.Id = someId;
            _someEntity2.Id = someId;

            Assert.NotEqual(_someEntity1, _someEntity2);
        }

        [Fact]
        public void WhenInstance1_Instance2_HaveDifferentId_SameProperty_ShouldNotBeEqual()
        {
            var someId = new Faker().Random.Guid();
            var alphanumeric = new Faker().Random.AlphaNumeric(10);
            _someEntity1.Id = someId;
            _someEntity1.SomeProperty = alphanumeric;
            _someEntity2.Id = new Faker().Random.Guid();
            _someEntity2.SomeProperty = alphanumeric;

            Assert.NotEqual(_someEntity1, _someEntity2);
        }

        [Fact]
        public void WhenInstance1_Instance2_HaveNotSameId_ShouldNotBeEqual()
        {
            var someId = new Faker().Random.Guid();
            _someEntity1.Id = someId;
            _someEntity2.Id =  new Faker().Random.Guid();

            Assert.NotEqual(_someEntity1, _someEntity2);
        }
    }
}