using System;

namespace Depra.Serialization.Benchmarks.Types
{
    [Serializable]
    public record TestRecord
    {
        public TestRecord() { }

        public override string ToString() => "record";
    }
}