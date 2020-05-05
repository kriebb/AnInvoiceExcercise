using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Backend.API.CosmosDB.Data.Services.Infrastructure.Impl
{
    public class IncludeAllPropertiesContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            // Or other way to determine...
            foreach (var jsonProperty in properties)
            {
                // Include all properties.
                jsonProperty.Ignored = false;
            }
            return properties;
        }
    }
}