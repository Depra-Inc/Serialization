using Depra.Serialization.Domain.Interfaces;

namespace Depra.Serialization.Application.Interfaces
{
    /// <summary>
    /// Facade for <see cref="IMemoryOptimalDeserializer"/>.
    /// Needed to associate the application layer with the infrastructure layer.
    /// </summary>
    internal interface IMemoryOptimalDeserializationProvider : IMemoryOptimalDeserializer { }
}