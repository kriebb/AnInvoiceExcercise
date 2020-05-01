using Bogus;
using Xunit;

namespace ASimpleCRMInvoiceExcercise.Tests
{
    public class GivenAnInvoiceLine
    {
        private InvoiceLine _invoiceLine;
        private Faker _faker;

        public GivenAnInvoiceLine()
        {
            _invoiceLine = new InvoiceLine();
            _faker  = new Faker();
        }
        public class ShouldDefine:GivenAnInvoiceLine
        {
            [Fact]
            public void Quantity()
            {
                var quantity = _faker.Random.Number();
                _invoiceLine.Quantity = quantity;

                Assert.Equal(quantity,_invoiceLine.Quantity);
            }
            [Fact]

            public void PricePerQuantity()
            {
                var price = _faker.Random.Decimal();
                _invoiceLine.Price = price;

                Assert.Equal(price,_invoiceLine.Price);
            }
            [Fact]

            public void TotalPrice()
            {
                var totalPrice = _faker.Random.Decimal();
                _invoiceLine.TotalPrice = totalPrice;

                Assert.Equal(totalPrice,_invoiceLine.TotalPrice);
            }
        }
    }
}