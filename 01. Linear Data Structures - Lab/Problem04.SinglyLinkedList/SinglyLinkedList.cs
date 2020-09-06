namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public SinglyLinkedList()
        {
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newElement = new Node<T>(item);

            newElement.Next = this._head;
            this._head = newElement;
            this.Count++;
        }

        public void AddLast(T item)
        {
            var newElement = new Node<T>(item);

            if (this._head == null)
            {
                this._head = newElement;
            }
            else
            {
                this.GetLastNode().Next = newElement;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.ValidateIfEmpty();

            return this._head.Value;
        }

        public T GetLast()
        {
            this.ValidateIfEmpty();

            return this.GetLastNode().Value;
        }

        public T RemoveFirst()
        {
            this.ValidateIfEmpty();

            var value = this._head.Value;
            this._head = this._head.Next;
            this.Count--;

            return value;
        }

        public T RemoveLast()
        {
            this.ValidateIfEmpty();

            T value;

            if (this.Count == 1)
            {
                value = this._head.Value;
                this._head = null;
            }
            else
            {
                var current = this._head;

                for (int i = 0; i < this.Count - 2; i++)
                {
                    current = current.Next;
                }

                var last = current.Next;

                current.Next = null;
                value = last.Value;
            }

            this.Count--;
            return value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;

            for (int i = 0; i < this.Count; i++)
            {
                yield return current.Value;
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

        private Node<T> GetLastNode()
        {
            var current = this._head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            return current;
        }
    }
}