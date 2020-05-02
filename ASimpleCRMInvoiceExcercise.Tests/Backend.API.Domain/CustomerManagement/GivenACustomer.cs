using System.Linq;
using Backend.API.Domain.CommonManagement;
using Backend.API.Domain.CustomerManagement;
using Bogus;
using Xunit;

namespace Backend.API.Tests.Backend.API.Domain.CustomerManagement
{
    public class GivenACustomer
    {
        private Customer _customer;
        private Person _bogusPerson;

        public GivenACustomer()
        {
            _bogusPerson = new Bogus.Person();
            _customer = new Customer();
        }
        public class ShouldDefineA:GivenACustomer
        {

            [Fact]
            public void FirstName()
            {
                _customer.FirstName = _bogusPerson.FirstName;
                Assert.Equal(_bogusPerson.FirstName, _customer.FirstName);
            }

            [Fact]
            public void LastName()
            {
                _customer.LastName = _bogusPerson.LastName;
                Assert.Equal(_bogusPerson.LastName, _customer.LastName);
            }

            [Fact]
            public void Address()
            {

                _customer.Address = new Address();
                Assert.Equal(new Address(),_customer.Address);
            }

            [Fact]
            public void Contacts()
            {
                Assert.NotNull(_customer.Contacts);
            }
        }

        public class ShouldBeAbleTo:GivenACustomer
        {
            [Fact]
            public void HaveNoContacts()
            {
                _customer.ClearContacts();
                Assert.Equal(0, _customer.Contacts.Count());
            }

            [Theory]
            [InlineData(1)]
            [InlineData(10)]
            public void HaveMultipleContacts(int numberOfContactInfo)
            {
                var faker = new Bogus.Faker();
                
                for (int i = 0; i < numberOfContactInfo; i++)
                {
                    var contactInfo = new ContactInfo();
                    contactInfo.Type = faker.Random.AlphaNumeric(10);
                    contactInfo.Value = faker.Random.AlphaNumeric(10);

                    _customer.AddContact(contactInfo);

                }

                Assert.Equal(numberOfContactInfo, _customer.Contacts.Count());
            }

            [Fact]
            public void RemoveContactData()
            {
                var contactInfo = new ContactInfo();
                _customer.ClearContacts();
                _customer.AddContact(contactInfo);

                Assert.Equal(1, _customer.Contacts.Count());


                _customer.RemoveContact(contactInfo);

                Assert.Equal(0, _customer.Contacts.Count());


            }
        }

    }
}


