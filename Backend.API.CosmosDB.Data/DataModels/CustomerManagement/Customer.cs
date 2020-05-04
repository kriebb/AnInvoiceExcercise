using System.Collections.Generic;
using Backend.API.Domain.CommonManagement;
using Backend.API.Domain.CustomerManagement;
using Microsoft.Azure.Documents;
using Address = Backend.API.CosmosDB.Data.DataModels.CommonManagement.Address;

namespace Backend.API.CosmosDB.Data.DataModels.CustomerManagement
{
    internal class Customer:Document
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

        public bool AddContact(ContactInfo contactInfo)
        {
            return _contacts.Add(contactInfo);
        }

        public void RemoveContact(ContactInfo contactInfo)
        {
            _contacts.Remove(contactInfo);

        }
    }
}