using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class CalculatorBase
    {

        public List<Memory> Memories { get; set; }
        
        public CalculatorBase()
        {
            Memories = new List<Memory>();
        }

        public void Store(double value)
        {
            if (Memories.Count < 10)
            {
                Memories.Insert(0, new Memory(value));
            }
            else
                Console.WriteLine("Out of memory");
        }

        public void AddToMemoryItem(int index, double value)
        {
            Memories.ElementAt(index).MAdd(value);
        }

        public void SubFromMemoryItem(int index, double value)
        {
            Memories.ElementAt(index).MSub(value);
        }

        public double RecallMemory()
        {
            return Memories.First().Value;
        }

        public void ClearMemoryItem(int index)
        {
            Memories.RemoveAt(index);
        }

        public void ClearAllMemory()
        {
            Memories.Clear();

        }



    }
}