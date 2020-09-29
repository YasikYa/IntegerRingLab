using MarkLab1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RingTest
{
    [TestClass]
    public class RingTest
    {
        private Ring _ring;
        private Ring _compareRing;

        [TestInitialize]
        public void Init()
        {
            _ring = new Ring();
            _compareRing = new Ring();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _ring = null;
            _compareRing = null;
        }

        [TestMethod]
        public void EmptyRingShouldHaveZeroCount()
        {
            Assert.AreEqual(0, _ring.Count, "Empty ring count differs from zero");
        }

        [TestMethod]
        public void AddShouldIncrementCount()
        {
            _ring.Add(1);
            var firstAdd = _ring.Count;
            _ring.Add(2);
            var secondAdd = _ring.Count;

            Assert.AreEqual(1, firstAdd, "Add to ring didn`t increment the count for the fist insert");
            Assert.AreEqual(2, secondAdd, "Add to ring didn`t increment the count for the second insert");
        }

        [TestMethod]
        public void AddShouldInsertValuesAndMoveHead()
        {
            _ring.Add(1);
            var firstValue = _ring.Value;
            _ring.Add(2);
            var secondVAlue = _ring.Value;

            Assert.AreEqual(1, firstValue, "Ring value differs from added for the first insert");
            Assert.AreEqual(2, secondVAlue, "Ring value differs from added for the second insert");
        }

        [TestMethod]
        public void ValuePropertyShouldShowCurrentAndDoNotChangeCount()
        {
            _ring.Add(1);
            _ring.Add(2);
            var firstPeek = _ring.Value;
            var secondPeek = _ring.Value;

            Assert.AreEqual(2, _ring.Count, "Peeking a value changed the count");
            Assert.AreEqual(2, firstPeek, "Peeked value differs from last inserted");
            Assert.AreEqual(2, secondPeek, "Peeked value differs from last inserted for the second peek");
        }

        public void PopShouldDecrementCount()
        {
            _ring.Add(1);
            _ring.Add(2);

            _ring.Pop();
            var firstPopCount = _ring.Count;
            _ring.Pop();
            var secondPopCount = _ring.Count;

            Assert.AreEqual(1, firstPopCount, "Pop didn`t decrement count for the fist pop");
            Assert.AreEqual(0, secondPopCount, "Pop didn`t decrement count for the second pop");
        }

        [TestMethod]
        public void PopShouldReturnValueAndRemoveNode()
        {
            _ring.Add(1);
            _ring.Add(2);
            var firstPop = _ring.Pop();
            var secondPop = _ring.Pop();
            
            Assert.AreEqual(2, firstPop, "Pop value differs from last added for the first pop");
            Assert.AreEqual(1, secondPop, "Pop value didn`t remove the node");
        }

        [TestMethod]
        public void MoveHeadShouldWork()
        {
            foreach (var number in Enumerable.Range(1, 5))
                _ring.Add(number);

            _ring.Move(Ring.Direction.Forward);
            var forwardValue = _ring.Value;
            _ring.Move(Ring.Direction.Backward);
            var backwardValue = _ring.Value;

            Assert.AreEqual(1, forwardValue, "Move forward didn`t change the head to correct node");
            Assert.AreEqual(5, backwardValue, "Move bacward didn`t change the head to correct node");
        }

        [TestMethod]
        public void PopShouldRespectRingOrder()
        {
            foreach (var number in Enumerable.Range(1, 5))
                _ring.Add(number);

            _ring.Pop();
            var afterPopHead = _ring.Value;
            _ring.Move(Ring.Direction.Forward);
            var nextValue = _ring.Value;

            Assert.AreEqual(afterPopHead, 4, "Pop didn`t set the head to previous node");
            Assert.AreEqual(nextValue, 1, "The ring order is broken after pop item");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekOnEmptyRingShouldThrowAnException()
        {
            _ = _ring.Value;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopOnEmptyRingShouldThrowAnException()
        {
            _ = _ring.Pop();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MoveOnEmptyRingShouldThrowAnException()
        {
            _ring.Move(Ring.Direction.Forward);
        }

        [TestMethod]
        public void WeakCompareShouldReturnTrueOnSameRings()
        {
            foreach (var number in Enumerable.Range(1, 5))
            {
                _ring.Add(number);
                _compareRing.Add(number);
            }

            var isEquals = _ring.CompareWeak(_compareRing);

            Assert.IsTrue(isEquals, "Weak compare return false on the same rings");
        }

        [TestMethod]
        public void WeakCompareShouldReturnTrueForDifferentOrder()
        {
            foreach (var number in Enumerable.Range(1, 5))
            {
                _ring.Add(number);
                _compareRing.Add(number);
            }
            _compareRing.Move(Ring.Direction.Backward);

            var isEquals = _ring.CompareWeak(_compareRing);

            Assert.IsTrue(isEquals, "Weak compare return false on the same rings but with different head");
        }

        [TestMethod]
        public void WeakCompareShouldReturnFalseOnDifferentRings()
        {
            foreach (var number in Enumerable.Range(1, 5))
            {
                _ring.Add(number);
                _compareRing.Add(number);
            }
            _compareRing.Add(2);

            var isEquals = _ring.CompareWeak(_compareRing);

            Assert.IsFalse(isEquals, "Weak compare return true on different rings");
        }

        [TestMethod]
        public void StrongCompareShouldReturnTrueOnSameRings()
        {
            foreach (var number in Enumerable.Range(1, 5))
            {
                _ring.Add(number);
                _compareRing.Add(number);
            }

            var isEquals = _ring.CompareStrong(_compareRing);

            Assert.IsTrue(isEquals, "Strong compare return false on the same rings");
        }

        [TestMethod]
        public void StrongCompareShouldReturnFalseForDifferentOrder()
        {
            foreach (var number in Enumerable.Range(1, 5))
            {
                _ring.Add(number);
                _compareRing.Add(number);
            }
            _compareRing.Move(Ring.Direction.Backward);

            var isEquals = _ring.CompareStrong(_compareRing);

            Assert.IsFalse(isEquals, "Strong compare return true on the same rings but with different head");
        }

        [TestMethod]
        public void StrongCompareShouldReturnFalseOnDifferentRings()
        {
            foreach (var number in Enumerable.Range(1, 5))
            {
                _ring.Add(number);
                _compareRing.Add(number);
            }
            _compareRing.Add(2);

            var isEquals = _ring.CompareWeak(_compareRing);

            Assert.IsFalse(isEquals, "Strong compare return true on different rings");
        }

        [TestMethod]
        public void ReadOnRingShouldReturnTheCorrectStream()
        {
            foreach (var number in Enumerable.Range(1, 5))
                _ring.Add(number);

            _ring.Move(Ring.Direction.Forward);
            var ringStream = _ring.Read(Ring.Direction.Forward).Take(15).ToList();

            _ring.Move(Ring.Direction.Backward);
            var ringStreamBackward = _ring.Read(Ring.Direction.Backward).Take(15).ToList();

            var expectedOrder = Enumerable.Repeat(Enumerable.Range(1, 5), 3).SelectMany(i => i);
            var expectedBackwardOrder = Enumerable.Repeat(Enumerable.Range(1, 5).Reverse(), 3).SelectMany(i => i);

            Assert.IsTrue(Enumerable.SequenceEqual(expectedOrder, ringStream), "Ring stream is wrong in forward direction");
            Assert.IsTrue(Enumerable.SequenceEqual(expectedBackwardOrder, ringStreamBackward), "Ring stream is wrong in backward direction");
        }
    }
}
