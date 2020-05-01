using System;
using System.Collections.Generic;
using Bogus;
using Xunit;

namespace ASimpleCRMInvoiceExcercise.Tests
{
    public class GivenAnAddress
    {
        public class ShouldDefine
        {
            private readonly Address _address;
            private Bogus.DataSets.Address _bogusAddress;

            ShouldDefine()
            {
                _address = new Address();
                _bogusAddress = new Faker().Address;
            }
            [Fact]

            public void Street()
            {
                _address.Street = _bogusAddress.StreetAddress();

                Assert.Equal(_bogusAddress.StreetAddress(), _address.Street);
            }
            [Fact]

            public void StreetNumber()
            {
                _address.StreetNumber = _bogusAddress.BuildingNumber();

                Assert.Equal(_bogusAddress.BuildingNumber(), _address.StreetNumber);
            }
            [Fact]

            public void Extension()
            {

                _address.StreetNumber = _bogusAddress.StreetSuffix();

                Assert.Equal(_bogusAddress.StreetSuffix(), _bogusAddress.StreetSuffix());
            }
            [Fact]

            public void PostalCode()
            {
                
                _address.PostalCode = _bogusAddress.ZipCode();

                Assert.Equal(_address.PostalCode, _bogusAddress.ZipCode());
            }
            [Fact]

            public void City()
            {
                _address.City = _bogusAddress.City();

                Assert.Equal(_address.City, _bogusAddress.City());
            }
            [Fact]

            public void Country()
            {
                _address.Country = _bogusAddress.Country();

                Assert.Equal(_address.Country, _bogusAddress.Country());
            }
        }



    }
}