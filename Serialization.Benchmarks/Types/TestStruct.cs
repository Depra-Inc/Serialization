using System;

namespace Depra.Serialization.Benchmarks.Types
{
    [Serializable]
    public struct TestStruct
    {
        public override string ToString() => "struct";
    }
}