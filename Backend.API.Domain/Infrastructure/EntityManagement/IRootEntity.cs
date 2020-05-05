using System;

namespace Backend.API.Domain.Infrastructure.EntityManagement
{
    public interface IRootEntity
    {
        Guid Id { get; }
    }
}