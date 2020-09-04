namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] items;

        public List()
            : this(DEFAULT_CAPACITY) 
        {
            this.items = new T[DEFAULT_CAPACITY];
        }

        public List(int capacity)
        {
            this.items = new T[capacity];
            this.Count = 0;
        }

        public T this[int index]
        {
            get 
            {
                this.ValidateIndex(index);
                return this.items[index];
            }

            set 
            {
                this.ValidateIndex(index);
                this.items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.ResizeIfNecessary();
            this.items[this.Count++] = item;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) >= 0;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            this.ResizeIfNecessary();

            for (int i = this.Count; i > index; i--)
            {
                this.items[i] = this.items[i - 1];
            }

            this.items[index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            var index = this.IndexOf(item);
            if(index == -1)
            {
                return false;
            }

            this.RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            this.Count--;

            for (int i = index; i < this.Count; i++)
            {
                this.items[i] = this.items[i + 1];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ValidateIndex(int index)
        {
            if (index < 0 || index > this.Count - 1)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void ResizeIfNecessary()
        {
            if (this.items.Length == this.Count)
            {
                var newItems = new T[Count * 2];

                for (int i = 0; i < this.Count; i++)
                {
                    newItems[i] = this.items[i];
                }

                this.items = newItems;
            }
        }
    }
}