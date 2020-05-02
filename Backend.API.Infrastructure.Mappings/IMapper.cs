﻿using System.Collections.Generic;

namespace Backend.API.Infrastructure.Mappings
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
        TSource Map(TDestination source);

        IEnumerable<TDestination> MapMany(IEnumerable<TSource> sources);
    }
}