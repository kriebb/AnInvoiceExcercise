using System.Collections.Generic;

namespace Backend.API.Infrastructure.Mappings.Impl
{
    public class AutoMapperAdapter<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        private readonly AutoMapper.IMapper _mapper;

        public AutoMapperAdapter(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public IEnumerable<TDestination> MapMany(IEnumerable<TSource> sources)
        {
            return _mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(sources);
        }
    }
}
