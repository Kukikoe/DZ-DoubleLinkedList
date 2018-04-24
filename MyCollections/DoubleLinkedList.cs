using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollections
{
    /// <summary>
    /// Demo class impliments DoubleLinkedList
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class DoubleLinkedList<T> : IList<T>, IEnumerator<T>
    {
        #region inner classes
        class ListItem
        {
            public T _value;
            public ListItem _prev;
            public ListItem _next;
            public ListItem(T value, ListItem prev = null, ListItem next = null)
            {
                _value = value;
                _prev = prev;
                _next = next;
            }
        }
        #endregion

        #region fields
        private ListItem _head = null;
        private ListItem _tail = null;
        private int _count = 0;
        private ListItem _currentItem;
        #endregion

        #region ctors
        /// <summary>
        /// Default constructor
        /// </summary>
        public DoubleLinkedList()
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="array">for initialization initial data</param>
        public DoubleLinkedList(IEnumerable<T> array)
        {
            using (IEnumerator<T> e = array.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    Add(e.Current);
                }
            }
        }
        #endregion

        #region properties
        public T this[int index]
        {
            get => GetItem(index)._value;
            set => GetItem(index)._value = value;
        }

        public int Count { get => _count; }

        public bool IsReadOnly { get => false; }

        public T Current { get => _currentItem._value; }

        object IEnumerator.Current { get => Current; }
        #endregion

        #region utilites
        /// <summary>
        /// Get value by index
        /// </summary>
        /// <param name="index">Index of the value</param>
        /// <returns></returns>
        private ListItem GetItem(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException();
            }
            ListItem item = _head;
            while (index-- > 0)
            {
                item = item._next;
            }
            return item;
        }
        #endregion

        #region methods
        /// <summary>
        /// Add to head
        /// </summary>
        /// <param name="item">Generic type, additional value</param>
        public void AddHead(T item)
        {
            if (_tail == null && _head == null)
            {
                _head = _tail = new ListItem(item);
                _count = 1;
            }
            else
            {
                ListItem val = new ListItem(item, _head);
                _head._prev = val;
                _head = val;
                ++_count;
            }
        }
        /// <summary>
        /// Add to tail
        /// </summary>
        /// <param name="item">Generic type, additional value</param>
        public void Add(T item)
        {
            if (_tail == null && _head == null)
            {
                _head = _tail = new ListItem(item);
                _count = 1;
            }
            else
            {
                ListItem val = new ListItem(item);
                _tail._next = val;
                _tail._prev = _tail;
                _tail = _tail._next;
                ++_count;         
            }
        }
        /// <summary>
        /// Removes all items
        /// </summary>
        public void Clear()
        {
            ListItem item = _head;
            while (item != null)
            {
                item = item._next;
                _head._next = null;
                _head._prev = null;
                _head = item;
            }
            _count = 0;
            _head = _tail = null;
        }
        /// <summary>
        /// Determines whether the DoubleLinkedList contains a specific value
        /// </summary>
        /// <param name="item">Generic type, additional value</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            using (IEnumerator<T> e = GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (e.Current.Equals(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Copies the elements of the DoubleLinkedList to an Array, starting at a particular Array index
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from DoubleLinkedList</param>
        /// <param name="arrayIndex">Index in array at which copying begins</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            using (IEnumerator<T> e = GetEnumerator())
            {
                while (e.MoveNext())
                {
                    array[arrayIndex++] = e.Current;
                }
            }
        }
        /// <summary>
        /// Determines the index of a specific item
        /// </summary>
        /// <param name="item">Generic type, additional value</param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            ListItem list = _head;
            for (int i = 0; list != null; ++i, list = list._next)
            {
                if (list._value.Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Insert an element at the specified position
        /// </summary>
        /// <param name="index">Index position of the insertion</param>
        /// <param name="item">Generic type, additional value</param>
        public void Insert(int index, T item)
        {
            if (index == 0)
            {
                AddHead(item);
                return;
            }
            if (index == _count)
            {
                Add(item);
                return;
            }
            ListItem list = new ListItem(item, GetItem(index - 1), GetItem(index));
            GetItem(index - 1)._next = GetItem(index)._prev = list;
            ++_count;
        }
        /// <summary>
        /// Removes the first occurrence of a specific object 
        /// </summary>
        /// <param name="item">Generic type, additional value</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            if (_head == null)
            {
                return false;
            }
            if (_head._value.Equals(item))
            {
                _head._next._prev = null;
                _head = _head._next;
                --_count;
                return true;
            }
            ListItem list = _head;
            while (list._next != null)
            {
                if (item.Equals(list._value))
                {
                    break;
                }
                list = list._next;
            }
            if (list == null) return false;
            list._next = list._next._next;
            list._prev = list._prev._prev;
            --_count;
            return true;
        }
        /// <summary>
        /// Removes the item at the specified index
        /// </summary>
        /// <param name="index"> Index of the item to remove</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (index == 0)
            {
                _head._next._prev = null;
                _head = _head._next;
                --_count;
            }
            ListItem list = _head;
            ListItem item = GetItem(index);
            while (list._next != null)
            {
                if (item.Equals(list._next))
                {
                    list._next = list._next._next;
                    list._prev = list._prev._prev;
                    --_count;
                }
                list = list._next;
            }
        }
        #endregion

        #region IEnumerator    
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Resert IEnumerator
        /// </summary>
        public void Reset()
        {
            _currentItem = null;
        }
        /// <summary>
        /// MoveNext IEnumerator
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (_currentItem == null)
            {
                _currentItem = _head;
            }
            else
            {
                _currentItem = _currentItem._next;
            }
            return _currentItem != null;
        }
        /// <summary>
        /// DisposeIEnumerator
        /// </summary>
        public void Dispose()
        {
            Reset();
        }
        #endregion
    }
}
