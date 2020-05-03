using System;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Entities;

namespace Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Repositories
{
    public interface IExampleRepository
    {
        ExampleEntity Get(Guid newGuid);
    }
}