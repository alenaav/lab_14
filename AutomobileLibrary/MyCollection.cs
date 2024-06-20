using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutomobileLibrary;

namespace lab12_4
{
    public class MyCollection<T> : MyHashTable<T>, IEnumerable<T>, ICollection<T> where T : IInit, ICloneable, IComparable, new()
    {
        public MyCollection<T>.Point<T>? beg;

        public class Point<T> where T : IInit, ICloneable, IComparable, new()
        {
            public T Data { get; set; }
            public Point<T>? Next { get; set; }
            public Point<T>? Pred { get; set; }

            public Point(T data)
            {
                Data = data;
                Next = null;
                Pred = null;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public MyCollection() : base()
        {
            Count = 0;
        }

        public MyCollection(int length) : base(length)
        {
            Count = 0;
            for (int i = 0; i < length; i++)
            {
                T item = new T();
                item.RandomInit();
                AddPoint(item);
            }
        }

        public MyCollection(MyCollection<T> c)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            Point<T>? current = beg;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public new void AddPoint(T item)
        {
            base.AddPoint(item);
            Count++;
        }

        public bool Remove(T item)
        {
            return RemoveData(item);
        }

        public new bool RemoveData(T data)
        {
            bool result = base.RemoveData(data);
            if (result) Count--;
            return result;
        }

        public new bool Contains(T data)
        {
            return base.Contains(data);
        }

        public MyCollection<T> DeepCopy()
        {
            MyCollection<T> newCollection = new MyCollection<T>();
            foreach (var item in this)
            {
                newCollection.AddPoint((T)item.Clone());
            }
            return newCollection;
        }

        public void Add(T item)
        {
            AddPoint(item);
        }

        [ExcludeFromCodeCoverage]
        public new void PrintTable()
        {
            base.PrintTable();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }
            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Недостаточно места в массиве");
            }

            Point<T>? current = beg;
            while (current != null)
            {
                array[arrayIndex++] = current.Data;
                current = current.Next;
            }
        }

        public void Clear()
        {
            beg = null;
            Count = 0;
        }
    }
}