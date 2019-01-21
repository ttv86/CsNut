using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTTD.Imports;

namespace CsAi.UnitTests
{
    [TestClass]
    public class FibonacciHeapTests
    {
        [TestMethod]
        public void TestBasicFunctionality()
        {
            var heap = new FibonacciHeap<int>();
            heap.Insert(4, 4);
            heap.Insert(1, 1);
            heap.Insert(5, 5);
            heap.Insert(2, 2);
            heap.Insert(3, 3);
            int a = heap.Pop();
            int b = heap.Pop();
            int c = heap.Pop();
            int d = heap.Pop();
            int e = heap.Pop();
            Assert.AreEqual(1, a);
            Assert.AreEqual(2, b);
            Assert.AreEqual(3, c);
            Assert.AreEqual(4, d);
            Assert.AreEqual(5, e);
        }
    }
}
