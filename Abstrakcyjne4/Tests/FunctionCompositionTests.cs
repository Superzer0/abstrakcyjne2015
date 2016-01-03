using System.Collections.Generic;
using System.Linq;
using FunctionComposition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class FunctionCompositionTests
    {
        [TestMethod]
        public void ImplementedInterfaces()
        {
            var comp = new Composition<double>();
            Assert.IsInstanceOfType(comp, typeof(IExecute<double>));
            Assert.IsInstanceOfType(comp, typeof(IEnumerable<IExecute<double>>));
        }

        [TestMethod]
        public void ComposeOneFunction()
        {
            IExecute<int> linearfunction = new SingleFunction<int>(x => 2 * x + 10);
            var comp = new Composition<int>(linearfunction);
            Assert.AreEqual(10, comp.Execute(0));
            Assert.AreEqual(14, comp.Execute(2));
            Assert.AreEqual(20, comp.Execute(5));
        }

        [TestMethod]
        public void ComposeTwoFunctions()
        {
            IExecute<int> linearfunction = new SingleFunction<int>(x => 2 * x + 5);
            IExecute<int> quadraticfunction = new SingleFunction<int>(x => x * x);

            var comp = new Composition<int>(linearfunction, quadraticfunction);
            Assert.AreEqual(25, comp.Execute(0));
            Assert.AreEqual(81, comp.Execute(2));
            Assert.AreEqual(225, comp.Execute(5));

            comp = new Composition<int>(quadraticfunction, linearfunction);
            Assert.AreEqual(5, comp.Execute(0));
            Assert.AreEqual(13, comp.Execute(2));
            Assert.AreEqual(55, comp.Execute(5));

            comp = new Composition<int>(linearfunction, linearfunction);
            Assert.AreEqual(15, comp.Execute(0));
            Assert.AreEqual(23, comp.Execute(2));
            Assert.AreEqual(35, comp.Execute(5));

            comp = new Composition<int>(quadraticfunction, quadraticfunction);
            Assert.AreEqual(0, comp.Execute(0));
            Assert.AreEqual(16, comp.Execute(2));
            Assert.AreEqual(625, comp.Execute(5));
        }

        [TestMethod]
        public void ComposeThreeFunctions()
        {
            IExecute<int> identity = new SingleFunction<int>(x => x);
            IExecute<int> linearFunction = new SingleFunction<int>(x => x + 2);
            IExecute<int> cubicfunction = new SingleFunction<int>(x => x * x * x - 1);

            var functionList = new[] { identity, linearFunction, cubicfunction };

            var comp = new Composition<int>(functionList);
            Assert.AreEqual(7, comp.Execute(0));
            Assert.AreEqual(63, comp.Execute(2));
            Assert.AreEqual(26, comp.Execute(1));
        }

        [TestMethod]
        public void AddingFunctionToComposition()
        {
            IExecute<int> identity = new SingleFunction<int>(x => x);

            var comp = new Composition<int>(identity);
            Assert.AreEqual(0, comp.Execute(0));
            Assert.AreEqual(2, comp.Execute(2));

            IExecute<int> linearFunction = new SingleFunction<int>(x => x + 2);

            comp.Add(linearFunction);
            Assert.AreEqual(2, comp.Execute(0));
            Assert.AreEqual(4, comp.Execute(2));

            IExecute<int> cubicfunction = new SingleFunction<int>(x => x * x * x);

            comp.Add(cubicfunction);
            Assert.AreEqual(8, comp.Execute(0));
            Assert.AreEqual(64, comp.Execute(2));
        }

        [TestMethod]
        public void CompositionOfCompositions()
        {
            IExecute<int> linearfunction = new SingleFunction<int>(x => x + 2);
            IExecute<int> quadraticfunction = new SingleFunction<int>(x => 2 * x * x);

            var comp1 = new Composition<int>(linearfunction, quadraticfunction);
            var comp2 = new Composition<int>(quadraticfunction, linearfunction);

            var compOfComps = new Composition<int>(comp1, comp2);

            Assert.AreEqual(130, compOfComps.Execute(0));
            Assert.AreEqual(650, compOfComps.Execute(1));
            Assert.AreEqual(2050, compOfComps.Execute(2));
        }

        [TestMethod]
        public void CheckIterator()
        {
            IExecute<int> identity = new SingleFunction<int>(x => x);
            IExecute<int> linearfunction = new SingleFunction<int>(x => 3 * x - 2);
            IExecute<int> quadraticfunction = new SingleFunction<int>(x => 2 * x * x - 5);
            IExecute<int> cubicfunction = new SingleFunction<int>(x => x * x * x + x * x + x + 1);

            var functionList = new[] { identity, linearfunction, quadraticfunction, cubicfunction };

            var comp = new Composition<int>(functionList);

            var tab0 = new[] { 0, -2, -5, 1 };
            var tab1 = new[] { 1, 1, -3, 4 };
            var tab2 = new[] { 2, 4, 3, 15 };
            var i = 0;

            foreach (var f in comp)
            {
                Assert.AreEqual(tab0[i], f.Execute(0));
                Assert.AreEqual(tab1[i], f.Execute(1));
                Assert.AreEqual(tab2[i], f.Execute(2));
                i++;
            }
        }

        [TestMethod]
        public void CheckIteratorCompositionOfCompositions()
        {
            IExecute<int> identity = new SingleFunction<int>(x => x);
            IExecute<int> linearfunction = new SingleFunction<int>(x => x + 2);
            IExecute<int> quadraticfunction = new SingleFunction<int>(x => 2 * x * x);

            var comp1 = new Composition<int>(linearfunction, quadraticfunction);
            var comp2 = new Composition<int>(quadraticfunction, linearfunction);

            var compOfComps = new Composition<int>(comp1, comp2);

            var functionList = new[] { identity, linearfunction, quadraticfunction, compOfComps };

            var comp = new Composition<int>(functionList);

            var tab0 = new[] { 0, 2, 0, 2, 0, 0, 2 };
            var tab1 = new[] { 1, 3, 2, 3, 2, 2, 3 };
            var tab2 = new[] { 2, 4, 8, 4, 8, 8, 4 };
            var i = 0;

            Assert.AreEqual(7, comp.Count());

            foreach (var f in comp)
            {
                Assert.AreEqual(tab0[i], f.Execute(0));
                Assert.AreEqual(tab1[i], f.Execute(1));
                Assert.AreEqual(tab2[i], f.Execute(2));
                i++;
            }
        }
    }
}
