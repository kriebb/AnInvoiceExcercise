using System;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Dtos;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Entities;
using Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Repositories;

namespace Backend.API.Infrastructure.Mappings.Tests.ExamplesManagement.Controllers
{
    public class ExampleController
    {
        private readonly IExampleRepository _exampleRepository;
        private readonly IMapper<ExampleEntity, ExampleDto> _exampleMapper;

        public ExampleController(IExampleRepository exampleRepository, IMapper<ExampleEntity, ExampleDto> exampleMapper)
        {
            _exampleRepository = exampleRepository;
            _exampleMapper = exampleMapper;
        }

        public ExampleDto Get(Guid newGuid)
        {
            var entity = _exampleRepository.Get(newGuid);
            var dto = _exampleMapper.Map(entity);

            return dto;
        }
    }
}