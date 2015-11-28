using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Abstrakcyjne3.Objects;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestEmptyListCreation()
        {
            TripleList<int> tripleList = new TripleList<int>();
            Assert.AreEqual(0, tripleList.Count());

            Assert.IsNull(tripleList.PreviousElement);
            Assert.IsNull(tripleList.MiddleElement);
            Assert.IsNull(tripleList.NextElement);
        }

        [TestMethod]
        public void TestAddingSingleElement()
        {
            TripleList<int> tripleList = new TripleList<int>();
            const int value = 4;
            tripleList.Add(value);
            Assert.AreEqual(1, tripleList.Count());
            Assert.AreEqual(value, tripleList.Value);

            Assert.IsNull(tripleList.PreviousElement);
            Assert.IsNull(tripleList.MiddleElement);
            Assert.IsNull(tripleList.NextElement);
        }

        [TestMethod]
        public void TestAddingTwoElements()
        {
            TripleList<int> tripleList = new TripleList<int>();
            int value1 = 4;
            int value2 = -9;
            tripleList.Add(value1);
            tripleList.Add(value2);
            Assert.AreEqual(2, tripleList.Count());
            // checking values
            Assert.AreEqual(value1, tripleList.Value);
            Assert.AreEqual(value2, tripleList.MiddleElement.Value);
            Assert.AreEqual(tripleList.Value, tripleList.MiddleElement.MiddleElement.Value);
            // checking neighbour Nodes of first element
            Assert.IsNull(tripleList.PreviousElement);
            Assert.IsNotNull(tripleList.MiddleElement);
            Assert.IsNull(tripleList.NextElement);
            // checking neighbour Nodes of second element
            Assert.IsNull(tripleList.MiddleElement.PreviousElement);
            Assert.IsNull(tripleList.MiddleElement.NextElement);
        }

        [TestMethod]
        public void TestAddingTreeElements()
        {
            TripleList<int> tripleList = new TripleList<int>();
            int value1 = 4;
            int value2 = -9;
            int value3 = 47;
            tripleList.Add(value1);
            tripleList.Add(value2);
            tripleList.Add(value3);
            Assert.AreEqual(3, tripleList.Count());
            // checking values
            Assert.AreEqual(value1, tripleList.Value);
            Assert.AreEqual(value2, tripleList.MiddleElement.Value);
            Assert.AreEqual(value3, tripleList.NextElement.Value);
            // checking neighbour Nodes of first element
            Assert.IsNull(tripleList.PreviousElement);
            Assert.IsNotNull(tripleList.MiddleElement);
            Assert.IsNotNull(tripleList.NextElement);
            // checking neighbour Nodes of second element
            Assert.IsNull(tripleList.MiddleElement.PreviousElement);
            Assert.IsNotNull(tripleList.MiddleElement.MiddleElement);
            Assert.IsNull(tripleList.MiddleElement.NextElement);
            // checking neighbour Nodes of third/last element
            Assert.IsNotNull(tripleList.NextElement.PreviousElement);
            Assert.IsNull(tripleList.NextElement.MiddleElement);
            Assert.IsNull(tripleList.NextElement.NextElement);
            // checking values
            Assert.AreEqual(value1, tripleList.Value);
            Assert.AreEqual(value2, tripleList.MiddleElement.Value);
            Assert.AreEqual(value3, tripleList.NextElement.Value);
        }

        [TestMethod]
        public void TestAddingFiveElements()
        {
            TripleList<int> tripleList = new TripleList<int>();
            int value1 = 1;
            int value2 = 2;
            int value3 = 3;
            int value4 = 4;
            int value5 = 5;
            tripleList.Add(value1);
            tripleList.Add(value2);
            tripleList.Add(value3);
            tripleList.Add(value4);
            tripleList.Add(value5);
            Assert.AreEqual(5, tripleList.Count());
            // checking values
            Assert.AreEqual(value1, tripleList.Value);
            Assert.AreEqual(value2, tripleList.MiddleElement.Value);
            Assert.AreEqual(value3, tripleList.NextElement.Value);
            Assert.AreEqual(value4, tripleList.NextElement.MiddleElement.Value);
            Assert.AreEqual(value5, tripleList.NextElement.NextElement.Value);
        }

        [TestMethod]
        public void TestListsEnumerator()
        {
            double[] values = { 1.1, 3.14, 6.13, 9.99999, 99.001 };
            var tripleList = new TripleList<double>();
            int i;
            for (i = 0; i < values.Length; ++i)
            {
                tripleList.Add(values[i]);
            }
            i = 0;
            foreach (double d in tripleList)
            {
                Assert.AreEqual(values[i++], d);
            }
        }

        [TestMethod]
        public void TestListsEnumerator2()
        {
            double[] values = { 1.1, 3.14, 6.13, 9.99999, 99.001 };
            var tripleList = new TripleList<double>();
            int i;
            for (i = 0; i < values.Length; ++i)
            {
                tripleList.Add(values[i]);
            }
            i = 0;
            IEnumerator<double> it = tripleList.GetEnumerator();
            while (it.MoveNext())
            {
                Assert.AreEqual(values[i++], it.Current);
            }
        }

        [TestMethod]
        public void TestIfNoCycle()
        {
            /** Initialization of the TripleList **/
            const int NUMBER_OF_ELEMENTS = 100;
            TripleList<int> tripleList = new TripleList<int>();
            for (int i = 0; i < NUMBER_OF_ELEMENTS; ++i)
            {
                tripleList.Add(i);
            }
            /** Created 2 TripleLists, first jumps every single element,
            another every two elements, in out case every two elements means every NextElement**/
            TripleList<int> tripleListEverySingleNode = tripleList;
            TripleList<int> tripleListEveryTwoNodes = tripleList.NextElement;
            for (int i = 0; i < NUMBER_OF_ELEMENTS * NUMBER_OF_ELEMENTS; ++i)
            {
                Assert.AreNotSame(tripleListEverySingleNode, tripleListEveryTwoNodes);
                JumpToNextElement(ref tripleListEverySingleNode);
                if (null == tripleListEveryTwoNodes.NextElement)
                {
                    // if list has end means there are no cycles
                    break;
                }
                else
                {
                    tripleListEveryTwoNodes = tripleListEveryTwoNodes.NextElement;
                }
            }
        }

        [TestMethod]
        public void ArrayInitializers()
        {
            var tl1 = new TripleList<int>() { 5, 10, 15 };
            Assert.AreEqual(3, tl1.Count());
            var tl2 = new TripleList<int>() { 0, tl1, 20 };
            Assert.AreEqual(3, tl1.Count());
            Assert.AreEqual(5, tl2.Count());
            Assert.AreEqual(tl1.Value, tl2.MiddleElement.Value);
            var tl1Sorted = tl1.ToList();
            tl1Sorted.Sort();
            var tl2Sorted = tl2.ToList();
            tl2Sorted.Sort();
            Enumerable.SequenceEqual(tl1Sorted, tl2Sorted);
        }

        private void JumpToNextElement(ref TripleList<int> element)
        {
            if (IsNotLastElement(element))
            {
                if (IsMiddleElement(element))
                {
                    if (null != element.MiddleElement.NextElement)
                    {
                        element = element.MiddleElement.NextElement;
                    }
                }
                else
                {
                    if (null != element.NextElement)
                    {
                        element = element.NextElement;
                    }
                }
            }
        }

        private bool IsNotLastElement(TripleList<int> element)
        {
            return null != element.MiddleElement;
        }

        private bool IsMiddleElement(TripleList<int> element)
        {
            return null == element.NextElement && null == element.PreviousElement && null != element.MiddleElement;
        }
    }
}
