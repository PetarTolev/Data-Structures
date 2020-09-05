namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;

        public Queue()
        {
            this._head = null;
            this.Count = 0;
        }

        public Queue(Node<T> element)
        {
            this._head = element;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            this.ValidateIfEmpty();

            foreach (var current in this)
            {
                if(current.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public T Dequeue()
        {
            this.ValidateIfEmpty();

            this.Count--;
            var value = this._head.Value;
            this._head = this._head.Next;
            return value;
        }

        public void Enqueue(T item)
        {
            var newElement = new Node<T>(item);

            if(this._head == null)
            {
                this._head = newElement;
            }
            else
            {
                var current = this._head;

                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newElement;
            }

            this.Count++;
        }

        public T Peek()
        {
            this.ValidateIfEmpty();

            return this._head.Value;
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
    }
}