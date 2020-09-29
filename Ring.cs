using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkLab1
{
    public class Ring
    {
        
        private RingNode _current;

        private RingNode Current => _current ?? throw new InvalidOperationException("Cannot perform an operation on empty ring");

        /// <summary>
        /// Current value
        /// </summary>
        public int Value => Current.Value;

        public int Count { get; private set; }

        public int Pop()
        {
            var value = Current.Value;

            if (Count == 1)
                _current = null;
            else
                _current = _current.Remove();

            Count--;
            return value;
        }

        public void Add(int item)
        {
            if (_current == null)
                _current = new RingNode(item);
            else
                _current.InsertAfterSelf(item);

            _current = _current.Next;
            Count++;
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Forward)
                _current = Current.Next;
            else
                _current = Current.Prev;
        }

        public IEnumerable<int> Read(Direction readOrder)
        {
            RingNode cursor = _current;
            while (true)
            {
                yield return cursor.Value;
                cursor = readOrder == Direction.Forward ? cursor.Next : cursor.Prev;
            }
        }

        public bool CompareWeak(Ring other)
        {
            if (Count != other.Count)
                return false;

            var firstItems = Read(Direction.Forward).Take(Count);
            var secondItems = other.Read(Direction.Forward).Take(Count);

            return !firstItems.Except(secondItems).Any();
        }

        public bool CompareStrong(Ring other)
        {
            if (Count != other.Count)
                return false;

            var firstItems = Read(Direction.Forward).Take(Count);
            var secondItems = other.Read(Direction.Forward).Take(Count);

            return Enumerable.SequenceEqual(firstItems, secondItems);
        }

        private class RingNode
        {
            /// <summary>
            /// Head node ctor. Creates a node with Next and Previous nodes set to self.
            /// </summary>
            /// <param name="value"></param>
            public RingNode(int value) => (Prev, Next, Value) = (this, this, value);

            /// <summary>
            /// Common node ctor. Specifies the Next and Previous node for new node.
            /// </summary>
            private RingNode(RingNode previous, RingNode next, int value) => (Prev, Next, Value) = (previous, next, value);

            public RingNode Next { get; private set; }

            public RingNode Prev { get; private set; }

            public int Value { get; set; }

            public void InsertAfterSelf(int value)
            {
                var newNode = new RingNode(this, Next, value);
                Next.Prev = newNode;
                Next = newNode;
            }

            public RingNode Remove()
            {
                Next.Prev = Prev;
                Prev.Next = Next;

                return Prev;
            }
        }

        public enum Direction
        {
            Forward,
            Backward
        }
    }
}
