using System;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace Core.Collections
{
    public class WeightedArray<T>
    {
        [Serializable]
        public struct Element {
            public T obj;
            public double weight;
            public Element(T obj, double weight = 0f) {
                this.obj = obj;
                this.weight = weight;
            }
        }

        private List<Element> _elements = new List<Element>();
        private double sumWeight;
        private Random rand = new Random();

        public T GetRandomItem() {
            double randWeight = rand.NextDouble() * sumWeight;
            return _elements.FirstOrDefault(x => x.weight >= randWeight).obj;
        }

        public void AddElement(T element, double weight = 0f) {
            sumWeight += weight;
            _elements.Add(new Element(element, sumWeight));   
        }
        public void Remove(T element) => _elements.Remove(_elements.FirstOrDefault(x => x.obj.Equals(element)));
        public void Clear() => _elements.Clear();
    }
}
