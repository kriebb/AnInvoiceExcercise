using Backend.API.Domain.CommonManagement;
using Backend.API.Domain.CustomerManagement;
using Backend.API.Domain.InvoiceManagement;
using Bogus;
using Bogus.Extensions;

namespace Backend.API.Data.Generator
{
    public class DomainGenerator
    {
        public static Faker<Invoice> InvoiceGenerator()
        {
            var fakeCustomerGenerator = CustomerGenerator();


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

        public static Faker<Customer> CustomerGenerator()
        {
            var fakeContactGenerator = ContactInfoGenerator();

            var fakeAddressGenerator = new Faker<Address>();

            var fakeCustomerGenerator = new Faker<Customer>();
            fakeCustomerGenerator
                .RuleFor(z => z.FirstName, f => f.Person.FirstName)
                .RuleFor(z => z.LastName, f => f.Person.LastName)
                .RuleFor(z => z.Id, f => f.Random.Guid())
                .RuleFor(z => z.Address, fakeAddressGenerator.Generate())
                .FinishWith(((faker, customer) =>
                {
                    foreach (var contactInfo in ExtensionsForFakerT.GenerateBetween<ContactInfo>(fakeContactGenerator, 0, 4))
                        customer.AddContact(contactInfo);
                }));
            return fakeCustomerGenerator;
        }

        public static Faker<ContactInfo> ContactInfoGenerator()
        {
            var fakeContactGenerator = new Faker<ContactInfo>();
            fakeContactGenerator.RuleFor(x => x.Type,
                    f => f.PickRandomParam(ContactInfo.CellNumber, ContactInfo.CellNumber))
                .RuleFor(x => x.Value, f => f.PickRandomParam(f.Phone.PhoneNumber(), f.Internet.Email()));

            return fakeContactGenerator;
        }
    }
}