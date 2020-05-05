using System.Collections.Generic;
using Backend.API.Domain.CommonManagement;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.InvoiceManagement;
using Backend.API.Dtos.CustomerManagement;
using Backend.API.Dtos.InvoiceManagement;
using Bogus;
using Bogus.Extensions;

namespace Backend.API.Data.Generator
{
    public class ApiDtoGenerator
    {
        public static Faker<InvoiceItem> InvoiceItemGenerator()
        {
            var fakeInvoiceLineGenerator = InvoiceLineItemGenerator();
            var fakeInvoiceGenerator = new Faker<InvoiceItem>();
            fakeInvoiceGenerator.RuleFor(z => z.CustomerId, f => f.Random.Guid())
                .RuleFor(z => z.Summary, f => f.Lorem.Paragraph())
                .RuleFor(z => z.TotalAmount, f => f.Random.Number())
                .RuleFor(z => z.Date, f => f.Date.Recent())
                .RuleFor(z => z.CustomerId, f => f.Random.Guid())
                .FinishWith(((faker, invoice) =>
                {
                    var lineItems = new List<InvoiceLineItem>();
                    foreach (var line in fakeInvoiceLineGenerator.GenerateBetween(0, 25))
                        lineItems.Add(line);
                    invoice.Lines = lineItems.ToArray();
                }));
            return fakeInvoiceGenerator;
        }

        private static Faker<InvoiceLineItem> InvoiceLineItemGenerator()
        {
            var faker = new Faker<InvoiceLineItem>();
            faker
                .RuleFor(x => x.Id, z => z.Random.Guid())
                .RuleFor(x => x.TotalPrice, z => z.Random.Decimal(1, 100))
                .RuleFor(x => x.Quantity, z => z.Random.Int(1, 100))
                .RuleFor(x => x.Price, z => z.Random.Decimal(1, 10));

            return faker;
        }

        public static Faker<ContactInfoItem> ContactInfoItemGenerator()
        {
            var fakeContactGenerator = new Faker<ContactInfoItem>();
            fakeContactGenerator.RuleFor(x => x.Type,
                    f => f.PickRandomParam(ContactInfo.CellNumber, ContactInfo.CellNumber))
                .RuleFor(x => x.Value, f => f.PickRandomParam(f.Phone.PhoneNumber(), f.Internet.Email()));

            return fakeContactGenerator;
        }

        public static Faker<CustomerItem> CustomerItemGenerator()
        {
            var fakeCustomerGenerator = new Faker<CustomerItem>();
            fakeCustomerGenerator
                .RuleFor(z => z.FirstName, f => f.Person.FirstName)
                .RuleFor(z => z.LastName, f => f.Person.LastName)
                .RuleFor(z => z.Id, f => f.Random.Guid());

            return fakeCustomerGenerator;
        }
    }
}