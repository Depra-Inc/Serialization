using System;

namespace Depra.Serialization.Benchmarks.Types
{
    [Serializable]
    public class TestClass
    {
        public override string ToString() => nameof(TestClass);
    }
}