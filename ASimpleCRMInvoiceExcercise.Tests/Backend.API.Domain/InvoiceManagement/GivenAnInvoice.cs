using System.Linq;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.InvoiceManagement;
using Bogus;
using Xunit;

namespace Backend.API.Tests.Backend.API.Domain.InvoiceManagement
{
    public class GivenAnInvoice
    {
        public class ShouldDefineA
        {
            private Invoice _invoice;
            private Faker _faker;

            public ShouldDefineA()
            {
                _invoice = new Invoice();
                _faker = new Faker();
            }
            [Fact]
            public void Summary()
            {
                var summary = _faker.Lorem.Paragraph();
                _invoice.Summary = summary;
                Assert.Equal(summary, _invoice.Summary);
            }
            [Fact]
            public void Date()
            {
                var recentDate = _faker.Date.Recent();
                _invoice.Date = recentDate;
                Assert.Equal(recentDate, _invoice.Date);
            }

            [Fact]
            public void Customer()
            {
                var customer = new Customer();
                _invoice.Customer = customer;
                Assert.Equal(customer, _invoice.Customer);
            }

            [Fact]
            public void TotalAmount()
            {
                var random = _faker.Random.Number();
                _invoice.TotalAmount = random;

                Assert.Equal(random, _invoice.TotalAmount);
            }

            [Fact]
            public void InvoiceLines()
            {
                Assert.NotNull(_invoice.Lines);
            }
        }


        public class ShouldBeAbleTo
        {
            private readonly Invoice _invoice;

            public ShouldBeAbleTo()
            {
                _invoice = new Invoice();
            }
            [Fact]
            public void HaveOneInvoiceLines()
            {
                _invoice.AddInvoiceLine(new InvoiceLine());
            }

            [Theory]
            [InlineData(1)]
            [InlineData(10)]
            public void HaveMultipleInvoiceLines(int number)
            {
                for (int i = 0; i < number; i++)
                {
                    var line = new InvoiceLine();

                    _invoice.AddInvoiceLine(line);

                }

                Assert.Equal(number, _invoice.Lines.Count());
            }

            [Fact]
            public void RemoveAnInvoiceLIne()
            {
                var invoiceLine = new InvoiceLine();
                invoiceLine.Id = new Faker().Random.Guid();

                _invoice.ClearInvoiceLines();
                _invoice.AddInvoiceLine(invoiceLine);

                Assert.Single(_invoice.Lines);


                _invoice.RemoveInvoiceLines(invoiceLine);

                Assert.Empty(_invoice.Lines);
            }
        }



    }
}