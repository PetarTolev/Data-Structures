namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public DoublyLinkedList()
        {
            this.Count = 0;
        }

        public int Count { get; private set; }

        private void Add(T item)
        {
            var node = new Node<T>(item);

            if (this.Count == 0)
            {
                this.head = node;
                this.tail = node;
            }

            this.Count++;
        }

        public void AddFirst(T item)
        {
            var node = new Node<T>(item);

            if (this.Count == 0)
            {
                this.head = node;
                this.tail = node;
            }
            else
            {
                this.head.Previous = node;
                node.Next = this.head;
                this.head = node;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            var node = new Node<T>(item);

            if (this.Count == 0)
            {
                this.head = node;
                this.tail = node;
            }
            else
            {
                this.tail.Next = node;
                node.Previous = this.tail;
                this.tail = node;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.ValidateIfEmpty();

            return this.head.Item;
        }

        public T GetLast()
        {
            this.ValidateIfEmpty();

            return this.tail.Item;
        }

        public T RemoveFirst()
        {
            this.ValidateIfEmpty();

            var value = this.head.Item;
            this.head = this.head.Next;
            this.Count--;

            return value;
        }

        public T RemoveLast()
        {
            this.ValidateIfEmpty();

            var value = this.tail.Item;
            this.tail = this.tail.Previous;
            this.Count--;

            return value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ValidateIfEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}