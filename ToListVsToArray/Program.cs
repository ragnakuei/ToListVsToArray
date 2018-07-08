using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;

namespace ToListVsToArray
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestRunner>();
        }
    }

    [CoreJob] // 可以針對不同的 CLR 進行測試
    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    public class TestRunner
    {
        private readonly TestClass _test = new TestClass();

        public TestRunner()
        {
        }

        [Benchmark]
        public void ToList() => _test.ToList();

        [Benchmark]
        public void ToArray() => _test.ToArray();
    }

    public class TestClass
    {
        public void ToList()
        {
            Enumerable.Range(1, 100).ToList();
        }

        public void ToArray()
        {
            Enumerable.Range(1, 100).ToArray();
        }
    }

}
