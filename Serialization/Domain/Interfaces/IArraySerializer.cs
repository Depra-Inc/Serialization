using System.IO;

namespace Depra.Serialization.Domain.Interfaces
{
    public interface IArraySerializer
    {
        void Serialize<TIn>(Stream outputStream, TIn[] toSerialize);

        TOut[] Deserialize<TOut>(Stream inputStream);
    }
}