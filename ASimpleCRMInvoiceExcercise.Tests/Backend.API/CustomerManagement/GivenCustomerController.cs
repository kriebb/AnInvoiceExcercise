using Xunit;

namespace Backend.API.Tests.Backend.API.CustomerManagement
{
    /*
     *  - Klanten aan te maken
        - Een contactgegeven (email/telefoon) toe te wijzen aan een klant
     */
    public class GivenCustomerController
    {
        [Fact]
        public void WhenAddingNullAsCustomer_ShouldReturnBadRequest()
        {

        }
        [Fact]
        public void WhenAddingACustomer_ShouldBeSaved()
        {

        }

        [Fact]
        public void WhenAddingNullAsContactDataToACustomer_ShouldReturnBadRequest()
        {

        }

        [Fact]
        public void WhenAddingContactDataToACustomer_ItShouldBeRetrieved()
        {

        }

        [Fact]
        public void WhenAddingDuplicateContactDataToACustomer_ItShouldNotBeAdded()
        {

        }


    }
}
