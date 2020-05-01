using System.Collections.Generic;

namespace ASimpleCRMInvoiceExcercise.Tests
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        private readonly HashSet<ContactInfo> _contacts = new HashSet<ContactInfo>();

        public IEnumerable<ContactInfo> Contacts => _contacts;

        public void ClearContacts()
        {
            _contacts.Clear();
        }

        public void AddContact(ContactInfo contactInfo)
        {
            _contacts.Add(contactInfo);
        }

        public void RemoveContact(ContactInfo contactInfo)
        {
            _contacts.Remove(contactInfo);

        }
    }
}