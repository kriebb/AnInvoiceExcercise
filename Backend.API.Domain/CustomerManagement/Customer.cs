using System.Collections.Generic;
using Backend.API.Domain.CommonManagement;
using Backend.API.Domain.Infrastructure.EntityMangement;

namespace Backend.API.Domain.CustomerManagement
{
    public class Customer:Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

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