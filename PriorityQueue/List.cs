using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace two_way_list
{
    public class My_list<Type> where Type : IComparable<Type>
    {
        public Element Head { get; private set; }
        public Element Last { get; private set; }
        public Element Iteratorforward;
        public Element Iteratorbackward;

        public int ItemCounter { get; private set; }
        public My_list() { Head =Last= null; }
        public class Element : IComparable<Element>
        {
            public Element next, prev;
            public Type data { get; set; }

            public static Element operator ++(Element actual)
            {
                return actual.next;
            }
            public static Element operator --(Element actual)
            {
                return actual.prev;
            }
            public Element(Type data)
            {
                this.next = null;
                this.prev = null;
                this.data = data;
            }
            public int CompareTo(Element other)
            {
                if (other == null) return 1;

                return data.CompareTo(other.data);
            }
        }
        public void InsertFront(Type data)
        {
            Element newNode = new Element(data);
            if (Head == null)
            {
                Head = newNode;

                if (Last == null)
                {
                    Last = newNode;
                }
                ItemCounter++;
                return;
            }
            if (Head != null)
            {
                Element first_node = Head;
                Head = newNode;
                Head.next = first_node;
                first_node.prev = Head;
            }

            ItemCounter++;
        }
        public void InsertAfter(Element actual, Type data)
        {
            if (actual == null)
            {
                Console.WriteLine("Podełeś nieziainicjalizowany element");
                return;
            }
            Element newNode = new Element(data);
            newNode.next = actual.next;
            actual.next = newNode;
            newNode.prev = actual;
            if (newNode.next != null)
            {
                newNode.next.prev = newNode;
            }
            else { Last = newNode; }
            ItemCounter++;
        }
        public void InsertLast( Type data)
        {
            ItemCounter++;
            Element newNode = new Element(data);
            if (Head == null)
            {
                newNode.prev = null;
                Head = newNode;
                Last = newNode;
                return;
            }
            Element lastNode =Last;
            lastNode.next = newNode;
            newNode.prev = lastNode;
            Last = newNode;
        }
        public void DeleteNodebyKey( Type key)
        {
            Element actual = Head;
            if (actual != null && actual.data.Equals(key))
            {
                Head = actual.next;
                Head.prev = null;
                if (actual == Last)
                    Last = actual.prev;///null
                ItemCounter--;
                return;
            }

            while (actual != null && !actual.data.Equals(key))
            {
                actual = actual.next;
            }
            if (actual == null)
            {
                return;
            }
            if (actual.next != null)
            {
                actual.next.prev = actual.prev;
            }
            else////UWaga
            {
                Last=actual.prev;
            }
            if (actual.prev != null)
            {
                actual.prev.next = actual.next;
            }
            else
            {
                Head = actual.next;
            }

            ItemCounter--;
        }
        public void DeleteThisNode(Element actual)
        {
            if (actual == null)
                return;
            if (actual == Last)
                Last = actual.prev;
            if (actual == Head)
                Head = actual.next;

            if (actual.next != null)
            {
                actual.next.prev = actual.prev;
            }
            if (actual.prev != null)
            {
                actual.prev.next = actual.next;
            }
            ItemCounter--;
        }
        /// <summary>
        ///  TO KONIECZNIE TRZEBA PRZETESTOWAĆ
        /// </summary>
        public Type[] ToArray()
        {
            if (ItemCounter <= 0)
                return null;
            Element iterator = Head;
            Type[] tmpArray = new Type[ItemCounter];
            for (int i = 0; i < tmpArray.Length; i++)
            {
                tmpArray[i] = iterator.data;
                iterator = iterator.next;
            }
            return tmpArray;
        }

    }
}
