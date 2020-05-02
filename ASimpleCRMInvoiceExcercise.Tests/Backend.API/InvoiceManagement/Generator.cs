using Backend.API.Domain.CommonManagement;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.InvoiceManagement;
using Backend.API.Dtos.InvoiceManagement;
using Bogus;
using Bogus.Extensions;

namespace Backend.API.Tests.Backend.API.InvoiceManagement
{
    public class Generator
    {
        public static Faker<InvoiceItem> InvoiceItemGenerator()
        {
            var fakeInvoiceGenerator = new Faker<InvoiceItem>();
            fakeInvoiceGenerator.RuleFor(z => z.CustomerId, f => f.Random.Number(1, int.MaxValue))
                .RuleFor(z => z.Summary, f => f.Lorem.Paragraph())
                .RuleFor(z => z.TotalAmount, f => f.Random.Number())
                .RuleFor(z => z.Date, f => f.Date.Recent());
            return fakeInvoiceGenerator;


        }

        public static Faker<Invoice> InvoiceGenerator()
        {
            var fakeContactGenerator = new Faker<ContactInfo>();
            fakeContactGenerator.RuleFor(x => x.Type,
                    f => f.PickRandomParam(ContactInfo.CellNumber, ContactInfo.CellNumber))
                .RuleFor(x => x.Value, f => f.PickRandomParam(f.Phone.PhoneNumber(), f.Internet.Email()));

            var fakeAddressGenerator = new Faker<Address>();

            var fakeCustomerGenerator = new Faker<Customer>();
            fakeCustomerGenerator
                .RuleFor(z => z.FirstName, f => f.Person.FirstName)
                .RuleFor(z => z.LastName, f => f.Person.LastName)
                .RuleFor(z => z.Id, f => f.IndexFaker++)
                .RuleFor(z => z.Address, fakeAddressGenerator.Generate())
                .FinishWith(((faker, customer) =>
                {
                    foreach (var contactInfo in fakeContactGenerator.GenerateBetween(0, 4))
                        customer.AddContact(contactInfo);
                }));


            var fakeInvoiceLineGenerator = new Faker<InvoiceLine>();
            fakeInvoiceLineGenerator.RuleFor(x => x.Price, f => f.Random.Decimal())
                .RuleFor(x => x.Quantity, f => f.Random.Number())
                .RuleFor(x => x.TotalPrice, f => f.Random.Decimal());


            var fakeInvoiceGenerator = new Faker<Invoice>();
            fakeInvoiceGenerator.RuleFor(z => z.Customer, fakeCustomerGenerator.Generate())
                .RuleFor(z => z.Summary, f => f.Lorem.Paragraph())
                .RuleFor(z => z.TotalAmount, f => f.Random.Number())
                .FinishWith(((faker, invoice) =>
                {
                    foreach (var line in fakeInvoiceLineGenerator.GenerateBetween(0, 25))
                        invoice.AddInvoiceLine(line);
                }));

            return fakeInvoiceGenerator;
        }
    }
}