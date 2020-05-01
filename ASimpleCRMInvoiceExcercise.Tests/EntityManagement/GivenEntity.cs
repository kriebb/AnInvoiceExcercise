using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using EntityManagement;
using Xunit;
using Xunit.Sdk;

namespace Backend.API.Tests.EntityManagement
{
    public class SomeEntity : Entity
    {
        public string SomeProperty { get; set; }
    }
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
            var someId = new Faker().Random.Number(1);
            _someEntity1.Id = someId;
            _someEntity2.Id = someId;

            Assert.Equal(_someEntity1, _someEntity2);
        }

        [Fact]
        public void WhenInstance1_Instance2_HaveBoth0_ShouldNotBeEqual()
        {
            var someId = 0;
            _someEntity1.Id = someId;
            _someEntity2.Id = someId;

            Assert.NotEqual(_someEntity1, _someEntity2);
        }

        [Fact]
        public void WhenInstance1_Instance2_HaveDifferentId_SameProperty_ShouldNotBeEqual()
        {
            var someId = new Faker().Random.Number();
            var alphanumeric = new Faker().Random.AlphaNumeric(10);
            _someEntity1.Id = someId;
            _someEntity1.SomeProperty = alphanumeric;
            _someEntity2.Id = --someId;
            _someEntity2.SomeProperty = alphanumeric;

            Assert.NotEqual(_someEntity1, _someEntity2);
        }

        [Fact]
        public void WhenInstance1_Instance2_HaveNotSameId_ShouldNotBeEqual()
        {
            var someId = new Faker().Random.Number();
            _someEntity1.Id = someId;
            _someEntity2.Id = --someId;

            Assert.NotEqual(_someEntity1, _someEntity2);
        }
    }
}
