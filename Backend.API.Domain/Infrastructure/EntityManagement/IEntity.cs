using System;

namespace Backend.API.Domain.Infrastructure.EntityManagement
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}