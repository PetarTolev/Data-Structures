namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> top;

        public Stack()
        {
            this.top = null;
            this.Count = 0;
        }

        public Stack(Node<T> element)
        {
            this.top = element;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            foreach (var current in this)
            {
                if (item.Equals(current))
                {
                    return true;
                }
            }

            return false;
        }

        public T Peek()
        {
            this.ValidationIfEmpty();

            return this.top.Value;
        }

        public T Pop()
        {
            this.ValidationIfEmpty();

            var value = this.top.Value;
            this.top = this.top.Next;
            this.Count--;
            return value;
        }

        public void Push(T item)
        {
            var newElement = new Node<T>(item);
            newElement.Next = this.top;
            this.top = newElement;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.top;
            
            for (int i = 0; i < this.Count; i++)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ValidationIfEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}