using System.IO;

namespace Depra.Serialization.Unsafe
{
    internal interface IArraySerializer
    {
        void Serialize<TIn>(Stream outputStream, TIn[] toSerialize);

        TOut[] Deserialize<TOut>(Stream inputStream);
    }
}