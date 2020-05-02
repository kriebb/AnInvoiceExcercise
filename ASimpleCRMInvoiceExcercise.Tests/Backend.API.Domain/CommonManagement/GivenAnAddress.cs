using Backend.API.Domain.CommonManagement;
using Bogus;
using Xunit;

namespace Backend.API.Tests.Backend.API.Domain.CommonManagement
{
    public class GivenAnAddress
    {
        private Address _address;
        private Bogus.DataSets.Address _bogusAddress;

        public GivenAnAddress()
        {
            _address = new Address();
            _bogusAddress = new Faker().Address;
        }

        public class ShouldDefine : GivenAnAddress
        {


            [Fact]

            public void Street()
            {
                var street = _bogusAddress.StreetAddress();
                _address.Street = street;

                Assert.Equal(street, _address.Street);
            }
            [Fact]

            public void StreetNumber()
            {
                var number = _bogusAddress.BuildingNumber();
                _address.StreetNumber = number;

                Assert.Equal(number, _address.StreetNumber);
            }
            [Fact]

            public void Extension()
            {
                var streetSuffix = _bogusAddress.StreetSuffix();
                _address.StreetNumber = streetSuffix;

                Assert.Equal(streetSuffix, _address.StreetNumber);
            }
            [Fact]

            public void PostalCode()
            {
                var postalCode = _bogusAddress.ZipCode();
                _address.PostalCode = postalCode;

                Assert.Equal(postalCode, _address.PostalCode);
            }
            [Fact]

            public void City()
            {
                var city = _bogusAddress.City();
                _address.City = city;

                Assert.Equal(city, _address.City);
            }
            [Fact]

            public void Country()
            {
                var country = _bogusAddress.Country();
                _address.Country = country;

                Assert.Equal(country, _address.Country);
            }
        }



    }
}