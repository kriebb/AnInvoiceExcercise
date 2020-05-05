using System;

namespace Backend.API.Dtos.CustomerManagement
{

    public class CustomerItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid Id { get; set; }
    }
}
