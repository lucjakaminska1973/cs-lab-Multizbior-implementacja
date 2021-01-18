using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LibraryMultiSet
{
    public class MultiSet<T> : ICollection<T>, IMultiSet<T>
    {
        private Dictionary<T, int> mset = new Dictionary<T, int>();



        //konstruktory
        public MultiSet() { }

        public MultiSet(IEqualityComparer<T> comparer)
        {
            mset = new Dictionary<T, int>(comparer);
        }

        public MultiSet(IEnumerable<T> sequence, IEqualityComparer<T> comparer)
        {
            mset = new Dictionary<T, int>(comparer);

            foreach (var item in sequence)
                Add(item);

        }

        public static MultiSet<T> operator +(MultiSet<T> first, MultiSet<T> second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException();

            MultiSet<T> result = new MultiSet<T>();

            foreach (var item in first)
                result.Add(item);

            foreach (var item in second)
                result.Add(item);

            return result;
        }

        public static MultiSet<T> operator -(MultiSet<T> first, MultiSet<T> second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException();

            foreach (var item in second)
            {
                if (first.Contains(item))
                {
                    first.Remove(item);
                }

            }
           
            return first;
        }

        public static MultiSet<T> operator *(MultiSet<T> first, MultiSet<T> second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException();

            List<T> tmp = new List<T>();

            foreach (var x in first.AsDictionary())
                for (int i = 0; i < x.Value; i++)
                    tmp.Add(x.Key);

            IEnumerable<T> enumerable = tmp.AsEnumerable();

            return second.IntersectWith(enumerable);
        }

        public MultiSet(Dictionary<T, int> mset)
        {
            this.mset = mset;
        }

        public MultiSet(IEnumerable<T> data)
        {
            foreach (var element in data)
            {
                Add(element);

            }

        }

        

        // iterowanie
        public int this[T item] => mset[item];

        public int Count => mset.Count;

        public bool IsReadOnly => false;

        public bool IsEmpty => mset.Count == 0;

        public IEqualityComparer<T> Comparer => new MultiSetEqualityComparer<T>();

        public static MultiSet<T> Empty { get; } = null;

        public void Add(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (!mset.ContainsKey(item))
            {
                mset.Add(item, 1);
            }
            else mset[item]++;
        }

        public bool Remove(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (!mset.ContainsKey(item))
            {
                return false;
            }
            else
            {
                mset[item]--;
                if (mset[item] == 0) mset.Remove(item);
                return true;
            }
        }

        public void Clear()
        {
            if (IsReadOnly) throw new NotSupportedException();
            mset.Clear();
        }

        public bool Contains(T item) => mset.ContainsKey(item);


        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var x in mset)
            {
                for (int i = arrayIndex; i < mset.Count + arrayIndex - 1;)
                {
                    for (int j = 0; j < x.Value; j++)
                    {
                        array[i] = x.Key;
                        i++;
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach (var x in mset)//       od c#7:  foreach (var (item, multiplicity) in mset)
            {
                output.Append($"{x.Key}: {x.Value}, ");//    od c#7:   output.Append($"{item}: {multiplicity}, ");
            }
            return output.ToString(0, output.Length - 2);
        }

        public string ToStringExpanded()
        {
            StringBuilder output = new StringBuilder();
            foreach (var x in mset)//       od c#7:  foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < x.Value; i++)
                {
                    output.Append($"{x.Key}, ");//    od c#7:   output.Append($"{item}: {multiplicity}, ");
                }
            }
            return output.ToString(0, output.Length - 2);
        }


        public IEnumerator<T> GetEnumerator()
        {
            //return new  MultiSetEnumerator();
            foreach (var x in mset)//       od c#7:  foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < x.Value; i++)
                {
                    yield return x.Key;
                }
                // yield break;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public MultiSet<T> Add(T item, int numberOfItems = 1)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (Contains(item))
            {
                mset[item] += numberOfItems;
            }
            else
            {
                Add(item);
                mset[item] += numberOfItems - 1;
            }
            return this;
        }

        public MultiSet<T> Remove(T item, int numberOfItems = 1)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (numberOfItems > mset[item]) RemoveAll(item);

            if (Contains(item))
            {
                for (int i = 0; i < numberOfItems; i++)
                    mset.Remove(item);
            }
            return this;

        }

        public MultiSet<T> RemoveAll(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            foreach (var x in mset)
            {
                if (x.Key.Equals(item))
                    mset.Remove(x.Key);
            }
            return this;
        }



        public MultiSet<T> UnionWith(IEnumerable<T> other)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (other == null)
                throw new ArgumentNullException();
            foreach (var x in other)
            {
                this.Add(x);
            }
            return this;
        }

        public MultiSet<T> IntersectWith(IEnumerable<T> other)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (other == null)
                throw new ArgumentNullException();

            var temp = other.ToList<T>();

            foreach (var x in mset)
            {
                if (!temp.Contains(x.Key))
                {

                    RemoveAll(x.Key);
                }
                else 
                {
                    List<T> result = temp.FindAll(a => a.Equals(x.Key));
                    if (x.Value > result.Count)
                    {
                        Remove(x.Key, x.Value - result.Count);
                        result.Clear();
                    }


                }
            }

            return this;
        }

        public MultiSet<T> ExceptWith(IEnumerable<T> other)
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            if (other == null)
                throw new ArgumentNullException();

            foreach (var x in other)
            {
                if (Contains(x))
                    mset.Remove(x);
            }

            return this;
        }

        public MultiSet<T> SymmetricExceptWith(IEnumerable<T> other)
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            if (other == null)
                throw new ArgumentNullException();

            List<T> tmp = new List<T>();

            foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < multiplicity; i++)
                    tmp.Add(item);
            }

            List<T> otherList = other.ToList();

            T[] tmpArray = new T[tmp.Count];
            tmp.CopyTo(tmpArray);

            List<T> currentList = tmp;
            currentList.RemoveAll(x => otherList.Contains(x));

            List<T> otherCommonList = otherList;
            otherCommonList.RemoveAll(x => tmpArray.Contains(x));

            foreach (var item in otherCommonList)
                currentList.Add(item);

            mset = (Dictionary<T, int>)new MultiSet<T>(currentList).AsDictionary();
            return this;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException();

            MultiSet<T> tmp = new MultiSet<T>(other);

            int count = 0;
            foreach (var element in other)
            {
                if (tmp.Contains(element))
                {
                    continue;
                }
                if (Contains(element))
                {
                    count++;
                    tmp.Add(element);
                }
            }

            if (count == mset.Count)
                return true;

            return false;
        }



        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException();

            var temp = other.ToList<T>();

            foreach (var x in temp)
            {
                if (!mset.ContainsKey(x)) return false;
            }
            return true;
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException();

            int count = 0;
            foreach (var element in other)
            {
                if (Contains(element))
                    count++;
            }

            if (count == other.Count())
                return true;

            return false;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException();

            bool isSuperset = IsSupersetOf(other);

            if (isSuperset && mset.Count != other.Count())
                return true;

            return false;
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException();

            int count = 0;
            foreach (var element in other)
            {
                if (Contains(element))
                    count++;

                if (count == 1)
                    return true;
            }
            return false;
        }

        public bool MultiSetEquals(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException();

            int mainCount = 0;
            foreach (var x in mset)
            {
                int count = 0;
                foreach (var element in other)
                    if (element.Equals(x.Key))
                        count++;
                if (count == x.Value)
                    mainCount++;
            }

            if (mainCount == mset.Count)
                return true;

            return false;
        }

        public IReadOnlyDictionary<T, int> AsDictionary()
        {
            return mset;
        }

        public IReadOnlySet<T> AsSet()
        {
            ISet<T> result = new HashSet<T>();

            foreach (var x in mset)
            {
                if (!result.Contains(x.Key))
                    result.Add(x.Key);
            }

            return (IReadOnlySet<T>)result;
        }


        internal class MultiSetEnumerator : IEnumerator<T>
        {

            private MultiSet<T> currentObj = null;
            public T Current => throw new NotImplementedException();
            private T currentItem = default(T);
            private Queue<T> msAsQeque = null;
            object IEnumerator.Current => Current;

            public MultiSetEnumerator(MultiSet<T> ts)
            {
                this.currentObj = ts;
            }




            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }

    public class MultiSetEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(T obj) => obj.GetHashCode();
    }
}
