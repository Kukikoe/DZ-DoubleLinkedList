using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCollections;

namespace UnitTestProject1
{
    [TestClass]
    public class DoubleLinkedListUnitTest
    {
        [TestMethod]
        public void Ctor1Test()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            Assert.AreNotEqual(null, list);
            Assert.AreEqual(0, list.Count);
            Assert.AreEqual(false, list.IsReadOnly);
        }

        [TestMethod]
        public void Ctor2Test()
        {
            int[] arr = { 1, 2, 3, 4, 5, 6 };
            DoubleLinkedList<int> list = new DoubleLinkedList<int>(arr);
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void AddTest()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            Assert.AreNotEqual(null, list);
            Assert.AreEqual(0, list.Count);
            list.Add(10);
            Assert.AreEqual(1, list.Count);
            for (int i = 0; i < 100; ++i)
            {
                list.Add(i);
            }
            Assert.AreEqual(101, list.Count);
        }

        [TestMethod]
        public void AddHeadTest()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            Assert.AreNotEqual(null, list);
            Assert.AreEqual(0, list.Count);
            list.AddHead(10);
            Assert.AreEqual(1, list.Count);
            for (int i = 0; i < 100; ++i)
            {
                list.Add(i);
            }
            Assert.AreEqual(101, list.Count);
        }

        [TestMethod]
        public void ClearTest()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            for (int i = 0; i < 100; ++i)
            {
                list.Add(i);
            }
            Assert.AreEqual(100, list.Count);
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void IndexTest()
        {
            int[] arr = { 1, 2, 5, 4, 3, 7, 9 };
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            foreach (int val in arr)
            {
                list.Add(val);
            }
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(arr[i], list[i]);
            }
        }

        [TestMethod]
        public void RemoveTest()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>() { 1, 2, 5, 4 };
            DoubleLinkedList<int> list1 = new DoubleLinkedList<int>() { 2, 5, 4 };
            bool value = list.Remove(1);
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void CopyToTest()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>() { 1, 2, 5, 4, 6, 4 };
            int[] arr = new int[6];
            list.CopyTo(arr, 0);

            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(arr[i], list[i]);
            }
        }

        [TestMethod]
        public void IndexOfTest()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>() { 1, 2, 5, 4 };
            var index = list.IndexOf(5);
            Assert.AreEqual(2, index);
        }

        [TestMethod]
        public void ContainsTest()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>() { 1, 2, 5, 4 };
            bool value = list.Contains(5);
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>() { 1, 2, 5, 4 };
            DoubleLinkedList<int> list1 = new DoubleLinkedList<int>() { 1, 5, 4 };
            list.RemoveAt(1);
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list1[i], list[i]);
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>() { 1, 2, 5, 4 };
            DoubleLinkedList<int> list1 = new DoubleLinkedList<int>() { 1, 5, 4 };
            list1.Insert(1, 2);
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(list[i], list1[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfRangeTest_1()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            list[-1] = 100;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfRangeTest_2()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            int x = list[-1];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfRangeTest_3()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            int x = list[1];
        }

        [TestMethod]
        public void IEnumerableTest()
        {
            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
            Assert.IsTrue(list.GetEnumerator() is IEnumerator<int>);
        }

        [TestMethod]
        public void IEnumerator_1Test()
        {
            int[] arr = { 1, 2, 5, 4, 3, 7, 9 };
            DoubleLinkedList<int> list = new DoubleLinkedList<int>(arr);
            int count = 0;
            using (IEnumerator<int> e = list.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    Assert.AreEqual(arr[count++], e.Current);
                }
            }

        }

        [TestMethod]
        public void IEnumerator_2Test()
        {
            int[] arr = { 1, 2, 5, 4, 3, 7, 9 };
            DoubleLinkedList<int> list = new DoubleLinkedList<int>(arr);
            int count = 0;
            foreach (int x in list)
            {
                Assert.AreEqual(arr[count++], x);
            }


        }
    }
}
