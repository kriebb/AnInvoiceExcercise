using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using Address = Backend.API.CosmosDB.Data.DataModels.CommonManagement.Address;

namespace Backend.API.CosmosDB.Data.DataModels.CustomerManagement
{
    internal class Customer:Document
    {
        [JsonProperty]
        public string FirstName { get; set; }
        [JsonProperty]
        public string LastName { get; set; }
        [JsonProperty]
        public Address Address { get; set; }
        [JsonProperty]
        private readonly HashSet<ContactInfo> _contacts = new HashSet<ContactInfo>();
        [JsonProperty]
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