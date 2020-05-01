using Bogus;
using Xunit;

namespace ASimpleCRMInvoiceExcercise.Tests
{
    public class GivenContactData
    {
        private ContactInfo _contactInfo;

        public GivenContactData()
        {
            _contactInfo = new ContactInfo(); 
        }
        public class ShouldDefine:GivenContactData
        {

            public ShouldDefine()
            {


            }
            [Fact]
            public void AnEmail()
            {
                _contactInfo.Type = ContactInfo.Email;
                _contactInfo.Value = new Faker().Internet.Email();
                Assert.Equal(ContactInfo.Email, _contactInfo.Type);

            }
            [Fact]
            public void ACellNumber()
            {
                _contactInfo.Type = ContactInfo.CellNumber;
                _contactInfo.Value = new Faker().Person.Phone;

                Assert.Equal(ContactInfo.CellNumber, _contactInfo.Type);
            }
        }

    }
}