using System;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Entities;

namespace Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Repositories
{
    internal class FakeRepository : IExampleRepository
    {
        public ExampleEntity Get(Guid newGuid)
        {
            return new ExampleEntity() { Id = newGuid };
        }
    }
}