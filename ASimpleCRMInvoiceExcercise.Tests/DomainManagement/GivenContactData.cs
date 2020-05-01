using Bogus;
using Xunit;

namespace ASimpleCRMInvoiceExcercise.Tests
{
    public class GivenContactData
    {
        public class ShouldDefine
        {
            private ContactInfo _contactInfo;

            public ShouldDefine()
            {
                _contactInfo = new ContactInfo();

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