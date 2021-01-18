using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryMultiSet;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private MultiSet<char> multiSet;
        private MultiSet<StringBuilder> stringBuilderSet;

        private static StringBuilder a = new StringBuilder("aaa");
        private static StringBuilder b = new StringBuilder("bbb");
        private static StringBuilder c = new StringBuilder("ccc");

        IEnumerable<char> collection = new List<char>() { 'a', 'a', 'b', 'b', 'c' };


        IEnumerable<StringBuilder> stringBuilderCollection = new List<StringBuilder>()
        {
            a, a, a, b, b, c
        };

        IEqualityComparer<char> comparer = new MultiSetEqualityComparer<char>();
        IEqualityComparer<StringBuilder> stringBuilderComparer = new MultiSetEqualityComparer<StringBuilder>();

        [TestMethod]
        public void Constructor_1_Param()
        {
            multiSet = new MultiSet<char>(comparer);

            multiSet.Add('a');
            multiSet.Add('a');
            multiSet.Add('b');

            Assert.AreEqual(2, multiSet.Count);
            Assert.AreEqual(2, multiSet['a']);
            Assert.AreEqual(1, multiSet['b']);
        }

        [TestMethod]
        public void StringBuilder_Constructor_1_Param()
        {
            stringBuilderSet = new MultiSet<StringBuilder>(stringBuilderComparer);

            stringBuilderSet.Add(a);
            stringBuilderSet.Add(a);
            stringBuilderSet.Add(b);

            Assert.AreEqual(2, stringBuilderSet.Count);
            Assert.AreEqual(2, stringBuilderSet[a]);
            Assert.AreEqual(1, stringBuilderSet[b]);
        }

        [TestMethod]
        public void Constructor_1_Param_IEnumerable()
        {
            multiSet = new MultiSet<char>(collection);

            Assert.AreEqual(3, multiSet.Count);
            Assert.AreEqual(2, multiSet['a']);
            Assert.AreEqual(2, multiSet['b']);
            Assert.AreEqual(1, multiSet['c']);
        }

        [TestMethod]
        public void StringBuilder_Constructor_1_Param_IEnumerable()
        {
            stringBuilderSet = new MultiSet<StringBuilder>(stringBuilderCollection);

            Assert.AreEqual(3, stringBuilderSet.Count);
            Assert.AreEqual(3, stringBuilderSet[a]);
            Assert.AreEqual(2, stringBuilderSet[b]);
            Assert.AreEqual(1, stringBuilderSet[c]);
        }

        [TestMethod]
        public void Constructor_2_Params()
        {
            multiSet = new MultiSet<char>(collection, comparer);

            Assert.AreEqual(3, multiSet.Count);
            Assert.AreEqual(2, multiSet['a']);
            Assert.AreEqual(2, multiSet['b']);
            Assert.AreEqual(1, multiSet['c']);
        }

        [TestMethod]
        public void StringBuilder_Constructor_2_Params()
        {
            stringBuilderSet = new MultiSet<StringBuilder>(stringBuilderCollection, stringBuilderComparer);

            Assert.AreEqual(3, stringBuilderSet.Count);
            Assert.AreEqual(3, stringBuilderSet[a]);
            Assert.AreEqual(2, stringBuilderSet[b]);
            Assert.AreEqual(1, stringBuilderSet[c]);
        }

        [TestMethod]
        public void ToString_Method()
        {
            multiSet = new MultiSet<char>(collection);

            Assert.AreEqual("a: 2, b: 2, c: 1", multiSet.ToString());
        }

        [TestMethod]
        public void StringBuilder_ToString_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>(stringBuilderCollection);

            Assert.AreEqual("aaa: 3, bbb: 2, ccc: 1", stringBuilderSet.ToString());
        }

        [TestMethod]
        public void Operator_Plus()
        {
            multiSet = new MultiSet<char>(collection);
            MultiSet<char> test = new MultiSet<char>(collection);

            MultiSet<char> multiSets = multiSet + test;

            Assert.AreEqual("a: 4, b: 4, c: 2", multiSets.ToString());
        }

        [TestMethod]
        public void StringBuilder_Operator_Plus()
        {
            stringBuilderSet = new MultiSet<StringBuilder>(stringBuilderCollection);
            MultiSet<StringBuilder> test = new MultiSet<StringBuilder>(stringBuilderCollection);

            MultiSet<StringBuilder> multiSets = stringBuilderSet + test;

            Assert.AreEqual("aaa: 6, bbb: 4, ccc: 2", multiSets.ToString());
        }

        [TestMethod]
        public void Operator_Minus()
        {
            multiSet = new MultiSet<char>(collection);
            MultiSet<char> test = new MultiSet<char>(collection);
            test.Remove('c');

            MultiSet<char> multiSets = multiSet - test;

            Assert.AreEqual("c: 1", multiSets.ToString());
        }

        [TestMethod]
        public void StringBuilder_Operator_Minus()
        {
            stringBuilderSet = new MultiSet<StringBuilder>(stringBuilderCollection);
            MultiSet<StringBuilder> test = new MultiSet<StringBuilder>(stringBuilderCollection);
            test.Remove(c);

            MultiSet<StringBuilder> multiSets = stringBuilderSet - test;

            Assert.AreEqual("ccc: 1", multiSets.ToString());
        }

        [TestMethod]
        public void Operator_Multiply()
        {
            multiSet = new MultiSet<char>(collection);
            MultiSet<char> test = new MultiSet<char>();
            test.Add('a', 2);
            test.Add('b');

            MultiSet<char> multiSets = multiSet * test;

            Assert.AreEqual("a: 2, b: 1", multiSets.ToString());
        }

        [TestMethod]
        public void StringBuilder_Operator_Multiply()
        {
            stringBuilderSet = new MultiSet<StringBuilder>(stringBuilderCollection);
            MultiSet<StringBuilder> test = new MultiSet<StringBuilder>();
            test.Add(a, 2);
            test.Add(b);

            MultiSet<StringBuilder> multiSets = stringBuilderSet * test;

            Assert.AreEqual("aaa: 2, bbb: 1", multiSets.ToString());
        }

        [TestMethod]
        public void Add_T_int_Method()
        {
            multiSet = new MultiSet<char>();

            multiSet.Add('a', 5);
            multiSet.Add('b', 1);

            Assert.AreEqual(5, multiSet['a']);
            Assert.AreEqual(1, multiSet['b']);
            Assert.IsTrue(multiSet.Count == 2);
        }

        [TestMethod]
        public void StringBuilder_Add_T_int_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();

            stringBuilderSet.Add(a, 5);
            stringBuilderSet.Add(b, 1);

            Assert.AreEqual(5, stringBuilderSet[a]);
            Assert.AreEqual(1, stringBuilderSet[b]);
            Assert.IsTrue(stringBuilderSet.Count == 2);
        }

        [TestMethod]
        public void Add_T_Method()
        {
            multiSet = new MultiSet<char>();

            multiSet.Add('a');
            multiSet.Add('a');
            multiSet.Add('a');

            Assert.AreEqual(3, multiSet['a']);
        }

        [TestMethod]
        public void StringBuilder_Add_T_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a, 3);

            Assert.AreEqual(3, stringBuilderSet[a]);
        }

        [TestMethod]
        public void Contains_Method()
        {
            multiSet = new MultiSet<char>();

            multiSet.Add('a');

            Assert.IsTrue(multiSet.Contains('a'));
        }

        [TestMethod]
        public void StringBuilder_Contains_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();

            stringBuilderSet.Add(a);

            Assert.IsTrue(stringBuilderSet.Contains(a));
        }

        [TestMethod]
        public void Clear_Method()
        {
            multiSet = new MultiSet<char>();

            multiSet.Add('a');
            multiSet.Clear();

            Assert.AreEqual(0, multiSet.Count);
        }

        [TestMethod]
        public void StringBuilder_Clear_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();

            stringBuilderSet.Add(a);
            stringBuilderSet.Clear();

            Assert.AreEqual(0, stringBuilderSet.Count);
        }

        [TestMethod]
        public void Remove_T_Method()
        {
            multiSet = new MultiSet<char>();

            multiSet.Add('a');
            multiSet.Remove('a');

            Assert.IsTrue(multiSet.Count == 0);
        }

        [TestMethod]
        public void StringBuilder_Remove_T_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();

            stringBuilderSet.Add(a);
            stringBuilderSet.Remove(a);

            Assert.IsTrue(stringBuilderSet.Count == 0);
        }

        [TestMethod]
        public void Remove_T_int_Method()
        {
            multiSet = new MultiSet<char>();

            multiSet.Add('b', 5);

            multiSet.Remove('b', 10);

            Assert.IsTrue(multiSet.Count == 0);
        }

        [TestMethod]
        public void StringBuilder_Remove_T_int_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();

            stringBuilderSet.Add(b, 5);
            stringBuilderSet.Remove(b, 10);

            Assert.IsTrue(stringBuilderSet.Count == 0);
        }

        [TestMethod]
        public void StringBuilder_RemoveAll_T_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();

            stringBuilderSet.Add(a, 5);

            stringBuilderSet.RemoveAll(a);

            Assert.AreEqual(0, stringBuilderSet.Count);
        }

        [TestMethod]
        public void UnionWith_Method()
        {
            multiSet = new MultiSet<char>();
            multiSet.Add('a');
            multiSet.Add('c');

            MultiSet<char> multiSets = multiSet.UnionWith(collection);

            Assert.AreEqual(3, multiSets.Count);
            Assert.AreEqual(3, multiSets['a']);
            Assert.AreEqual(2, multiSets['b']);
            Assert.AreEqual(2, multiSets['c']);
        }

        [TestMethod]
        public void StringBuilder_UnionWith_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a);
            stringBuilderSet.Add(c);

            MultiSet<StringBuilder> multiSets = stringBuilderSet.UnionWith(stringBuilderCollection);

            Assert.AreEqual(3, multiSets.Count);
            Assert.AreEqual(4, multiSets[a]);
            Assert.AreEqual(2, multiSets[b]);
            Assert.AreEqual(2, multiSets[c]);
        }

        [TestMethod]
        public void IntersectWith_Method()
        {
            multiSet = new MultiSet<char>();
            multiSet.Add('a');

            MultiSet<char> multiSets = multiSet.IntersectWith(collection);

            Assert.AreEqual(1, multiSets.Count);
            Assert.AreEqual(1, multiSets['a']);
        }

        [TestMethod]
        public void StringBuilder_IntersectWith_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a);
            stringBuilderSet.Add(a);
            stringBuilderSet.Add(a);
            stringBuilderSet.Add(a);

            MultiSet<StringBuilder> multiSets = stringBuilderSet.IntersectWith(stringBuilderCollection);

            Assert.AreEqual(1, multiSets.Count);
            Assert.AreEqual(3, multiSets[a]);
        }

        [TestMethod]
        public void ExceptWith_Method()
        {
            multiSet = new MultiSet<char>();
            multiSet.Add('a');
            multiSet.Add('b');

            multiSet.Add('d');
            multiSet.Add('e');
            multiSet.Add('f');

            MultiSet<char> multiSets = multiSet.ExceptWith(collection);

            Assert.AreEqual(3, multiSets.Count);
        }

        [TestMethod]
        public void StringBuilder_ExceptWith_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a);
            stringBuilderSet.Add(b);

            stringBuilderSet.Add(c);

            MultiSet<StringBuilder> multiSets = stringBuilderSet.ExceptWith(stringBuilderCollection);

            Assert.AreEqual(0, multiSets.Count);
        }

        [TestMethod]
        public void IsSubsetOf_Method()
        {
            multiSet = new MultiSet<char>();
            multiSet.Add('a');
            multiSet.Add('b');

            bool isSubset = multiSet.IsSubsetOf(collection);

            Assert.IsTrue(isSubset);
        }

        [TestMethod]
        public void StringBuilder_IsSubsetOf_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a);
            stringBuilderSet.Add(b);

            bool isSubset = stringBuilderSet.IsSubsetOf(stringBuilderCollection);

            Assert.IsTrue(isSubset);
        }

        [TestMethod]
        public void Symmetric_Except_With_Method()
        {
            multiSet = new MultiSet<char>();
            multiSet.Add('a', 3);
            multiSet.Add('b', 2);
            multiSet.Add('e', 2);

            MultiSet<char> multiSets = multiSet.SymmetricExceptWith(collection);

            Assert.AreEqual("e: 2, c: 1", multiSets.ToString());
        }

        [TestMethod]
        public void StringBuilder_Symmetric_Except_With_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a, 3);
            stringBuilderSet.Add(b, 2);

            MultiSet<StringBuilder> multiSets = stringBuilderSet.SymmetricExceptWith(stringBuilderCollection);

            Assert.AreEqual("ccc: 1", multiSets.ToString());
        }

        [TestMethod]
        public void IsProperSubsetOf_Method()
        {
            multiSet = new MultiSet<char>();
            multiSet.Add('a');
            multiSet.Add('b');

            bool isProperSubset = multiSet.IsProperSubsetOf(collection);

            Assert.IsTrue(isProperSubset);
        }

        [TestMethod]
        public void StringBuilder_IsProperSubsetOf_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a);
            stringBuilderSet.Add(b);

            bool isProperSubset = stringBuilderSet.IsProperSubsetOf(stringBuilderCollection);

            Assert.IsTrue(isProperSubset);
        }

        [TestMethod]
        public void IsSupersetOf_Method()
        {
            multiSet = new MultiSet<char>();
            multiSet.Add('a', 3);
            multiSet.Add('b', 2);
            multiSet.Add('c');

            multiSet.Add('x', 5);

            bool isSupersetOf = multiSet.IsSupersetOf(collection);

            Assert.IsTrue(isSupersetOf);
        }

        [TestMethod]
        public void StringBuilder_IsSupersetOf_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a, 3);
            stringBuilderSet.Add(b, 2);
            stringBuilderSet.Add(c);

            stringBuilderSet.Add(c, 5);

            bool isSupersetOf = stringBuilderSet.IsSupersetOf(stringBuilderCollection);

            Assert.IsTrue(isSupersetOf);
        }

        [TestMethod]
        public void IsProperSupersetOf_Method()
        {
            multiSet = new MultiSet<char>();
            multiSet.Add('a', 3);
            multiSet.Add('b', 2);
            multiSet.Add('c');

            multiSet.Add('x', 5);

            bool isProperSupersetOf = multiSet.IsProperSupersetOf(collection);

            Assert.IsTrue(isProperSupersetOf);
        }

        [TestMethod]
        public void StringBuilder_IsProperSupersetOf_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a, 3);
            stringBuilderSet.Add(b, 2);
            stringBuilderSet.Add(c);

            stringBuilderSet.Add(c, 5);

            bool isProperSupersetOf = stringBuilderSet.IsProperSupersetOf(stringBuilderCollection);

            Assert.IsTrue(isProperSupersetOf);
        }

        [TestMethod]
        public void Overlaps_Method()
        {
            multiSet = new MultiSet<char>();
            multiSet.Add('a');

            multiSet.Add('x', 5);
            multiSet.Add('z', 10);

            bool isOverlaps = multiSet.Overlaps(collection);

            Assert.IsTrue(isOverlaps);
        }

        [TestMethod]
        public void StringBuilder_Overlaps_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a);

            stringBuilderSet.Add(c, 5);
            stringBuilderSet.Add(b, 10);

            bool isOverlaps = stringBuilderSet.Overlaps(stringBuilderCollection);

            Assert.IsTrue(isOverlaps);
        }

        [TestMethod]
        public void MultiSetEquals_Method()
        {
            multiSet = new MultiSet<char>();
            multiSet.Add('a', 2);
            multiSet.Add('b', 2);
            multiSet.Add('c');

            bool isEqual = multiSet.MultiSetEquals(collection);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void StringBuilder_MultiSetEquals_Method()
        {
            stringBuilderSet = new MultiSet<StringBuilder>();
            stringBuilderSet.Add(a, 3);
            stringBuilderSet.Add(b, 2);
            stringBuilderSet.Add(c);

            bool isEqual = stringBuilderSet.MultiSetEquals(stringBuilderCollection);

            Assert.IsTrue(isEqual);
        }

        
    }
}
