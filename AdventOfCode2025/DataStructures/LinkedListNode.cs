using System.Collections.Generic;

namespace DataStructures
{
    public class LinkedListNode<T>
    {
        public T Value { get; set; }

        public LinkedListNode<T>? Next { get; set; }
        public LinkedListNode<T>? Prev { get; set; }

        public LinkedListNode(T value, LinkedListNode<T>? prev = null, LinkedListNode<T>? next = null)
        {
            Value = value;
            Prev = prev;
            Next = next;
        }
    }
}
