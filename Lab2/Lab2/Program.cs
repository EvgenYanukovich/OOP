using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace lab2 { 
    public partial class Set
    {
        public const string name = "Set";
        private static int instanceCount;
        private readonly int id;
        private List<int> elements;

        static Set()
        {
            instanceCount = 0;
        }

        private Set(List<int> elements, int? id)
        {
            this.elements = elements;
            this.id = id ?? this.GetHashCode();
            instanceCount++;
        }
        
        public Set() : this(new List<int>())
        {
            
        }

        public Set(IEnumerable<int> elements): this(new List<int>(elements), null)
        {

        }

        public Set(int element = 5) : this(new List<int> {element})
        {
            
        }
        
        public Set(Set set): this(new List<int>(set.elements), set.id)
        {

        }

        public static int InstanceCount
        {
            get { return instanceCount; }
        }

        public int ID
        {
            get { return id; }
        }

        public List<int> Elements
        {
            get { return elements; }
            set { elements = value; }
        }

        public void AddElement(int element)
        {
            if (!elements.Contains(element))
                elements.Add(element);
        }

        public void RemoveElement(int element)
        {
            elements.Remove(element);
        }

        public void BoolAdd(ref int element, out bool val)
        {
            if (!elements.Contains(element))
            {
                val = true;
                elements.Add(element);
            }
            else
            {
                val = false;
            }
        }
        public static string Information()
        {
            return $"Имя: {name}\nКол-во экземпляров: {instanceCount}" ;
        }

        public static Set Intersection(Set set1, Set set2)
        {
            return new Set(set1.elements.Intersect(set2.elements).ToList());
        }

        public static Set Difference(Set set1, Set set2)
        {
            return new Set(set1.elements.Except(set2.elements).ToList());
        }

        public override string ToString()
        {
            return "Set ID: " + id + ", Elements: " + string.Join(", ", elements);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, elements, ID, Elements);
        }

        public override bool Equals(object? obj)
        {
            return obj is Set set &&
                   id == set.id &&
                   EqualityComparer<List<int>>.Default.Equals(elements, set.elements) &&
                   ID == set.ID &&
                   EqualityComparer<List<int>>.Default.Equals(Elements, set.Elements);
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            Set set1 = new Set(new List<int> { 1, 2, 3, 4 });
            Set set2 = new Set(new List<int> { 3, 4, 5, 6 });
            Set set3 = new Set(new List<int> { -1, -2, -3, -4 });

            Console.WriteLine("Set1: " + set1.ToString());
            Console.WriteLine("Set2: " + set2.ToString());
            Console.WriteLine("Set3: " + set3.ToString());

            Set[] sets = new Set[] { set1, set2, set3 };

            foreach (var set in sets)
            {
                if (set.Elements.All(x => x % 2 == 0))
                    Console.WriteLine("Only even elements: " + set.ToString());

                if (set.Elements.All(x => x % 2 != 0))
                    Console.WriteLine("Only odd elements: " + set.ToString());

                if (set.Elements.Any(x => x < 0))
                    Console.WriteLine("Contains negative elements: " + set.ToString());
            }

            var anonymousType = new { ID = 1, Elements = new List<int> { 1, 2, 3 } };
            Console.WriteLine(anonymousType);
        }
    }
}