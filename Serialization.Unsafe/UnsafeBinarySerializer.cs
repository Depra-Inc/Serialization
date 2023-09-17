using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Depra.Serialization.Unsafe
{
    public sealed class UnsafeBinarySerializer : IArraySerializer
    {
        public void Serialize<T>(Stream stream, T[] toSerialize)
        {
            var bytes = ChangeType(toSerialize, Array.Empty<byte>());
            try
            {
                using var writer = new BinaryWriter(stream);
                writer.Write(bytes.LongLength);
                writer.Write(bytes);
            }
            finally
            {
                ChangeType(bytes, Array.Empty<T>());
            }
        }

        public T[] Deserialize<T>(Stream stream)
        {
            using var reader = new BinaryReader(stream);
            var lenght = reader.ReadInt64();
            var bytes = reader.ReadBytes((int)lenght);
            return ChangeType(bytes, Array.Empty<T>());
        }

        private static unsafe byte[] ChangeType<T>(T[] array, byte[] v)
        {
            var size = Marshal.SizeOf<T>();
            var arrayPointer = (long*)AddressOf(array).ToPointer();
            var destinationPointer = (long*)AddressOf(v).ToPointer();
            var newLenght = size / sizeof(byte) * array.LongLength;
            *arrayPointer = *destinationPointer;
            *(arrayPointer + 1) = newLenght;

            var result = (object)array;
            var resultAsBytes = (byte[])result;

            return resultAsBytes;
        }
        
        private static unsafe T[] ChangeType<T>(byte[] byteArray, T[] sample)
        {
            var size = Marshal.SizeOf<T>();
            var arrayPointer = (long*)AddressOf(byteArray).ToPointer();
            var destinationPointer = (long*)AddressOf(sample).ToPointer();
            var newLength = byteArray.Length / size;
            *arrayPointer = *destinationPointer;
            *(arrayPointer + 1) = newLength;

            var result = (object)byteArray;
            var resultAsT = (T[])result;

            return resultAsT;
        }
        
        private static unsafe IntPtr AddressOf(object value)
        {
            var typedReference = __makeref(value);
            return **(IntPtr**)&typedReference;
        }
    }
}