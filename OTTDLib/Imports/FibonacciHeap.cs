using System.Collections.Generic;

namespace OpenTTD.Imports
{
    [ImportFrom("queue.fibonacci_heap", 2)]
    public class FibonacciHeap<T>
    {
        private readonly List<(T, double)> values = new List<(T, double)>();

        public void Insert(T item, double priority)
        {
            this.values.Add((item, priority));
            this.values.Sort((a, b) => b.Item2.CompareTo(a.Item2));
        }

        public T Pop()
        {
            var last = this.values[this.values.Count - 1];
            this.values.Remove(last);
            return last.Item1;
        }

        public T Peek()
        {
            var last = this.values[this.values.Count - 1];
            return last.Item1;
        }

        public int Count()
        {
            return this.values.Count;
        }

        public bool Exists(T item)
        {
            foreach (var value in this.values)
            {
                if (object.Equals(value.Item1, item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
