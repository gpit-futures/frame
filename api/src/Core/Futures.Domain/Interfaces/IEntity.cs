using System;

namespace Futures.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}