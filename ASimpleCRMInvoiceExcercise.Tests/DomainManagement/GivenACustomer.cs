using System;
using System.Collections;
using System.Dynamic;
using System.Linq;
using Bogus;
using Xunit;

namespace ASimpleCRMInvoiceExcercise.Tests
{
    public class GivenACustomer
    {
        public class ShouldDefineA
        {
            public ShouldDefineA()
            {
                _bogusPerson = new Bogus.Person();
                _customer = new Customer();
            }
            private Customer _customer;
            private Person _bogusPerson;

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
                var bogusPersonAdress = $"{_bogusPerson.Address.Street} , {_bogusPerson.Address.ZipCode} {_bogusPerson.Address.City}, {_bogusPerson.Address.State}";
                _customer.Address = bogusPersonAdress;
                Assert.Equal(bogusPersonAdress, _customer.Address);
            }

            [Fact]
            public void Contacts()
            {
                Assert.NotNull(_customer.Contacts);
            }
        }

        public class ShouldBeAbleTo
        {
            private Customer _customer;

            public ShouldBeAbleTo()
            {
                _customer = new Customer();
            }
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


