using System;
using System.Linq;

namespace MarkLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var ring = new Ring();
            var ring2 = new Ring();
            foreach (var num in Enumerable.Range(1, 5))
            {
                ring.Add(num);
                ring2.Add(num);
            }
            
            ring2.Move(Ring.Direction.Backward);
            ring.Move(Ring.Direction.Forward);

            ShowRing(ring);
            Console.WriteLine("----------------");
            ShowRing(ring2);

            Console.WriteLine($"Is first weak equals second: { ring.CompareWeak(ring2) }");
            Console.WriteLine($"Is first strong equals second: { ring.CompareStrong(ring2) }");

            Console.WriteLine($"Pop: {ring.Pop()}");
            Console.WriteLine($"Pop: {ring.Pop()}");

            ShowRing(ring);

            Console.ReadLine();
        }

        private static void ShowRing(Ring ring)
        {
            foreach(var item in ring.Read(Ring.Direction.Forward).Take(ring.Count))
                Console.WriteLine(item);
        }
    }
}
