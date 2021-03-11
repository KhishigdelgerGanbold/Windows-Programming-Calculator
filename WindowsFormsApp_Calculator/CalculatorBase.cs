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
        public double? Num1 { get; set; }

        public double? Num2 { get; set; }

        public string Operation { get; set; }

        public List<Memory> Memories { get; set; }
        
        public CalculatorBase()
        {
            Memories = new List<Memory>();
            Num2 = 0;
        }

        public void Store(double value)
        {
            if (Memories.Count < 10)
            {
                Memories.Insert(0, new Memory(value));
                Console.WriteLine("Action: {0} stored", value);
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

        public void AddNums()
        {
            Num1 += Num2;
        }

        public void SubNums()
        {
            Num1 -= Num2;
        }

        public void MultNums()
        {
            Num1 *= Num2;
        }

        public void DivNums()
        {
            Num1 /= Num2;
        }

        public double RecallMemory()
        {
            Num2 = Memories.First().Value;
            return Memories.First().Value;
        }

        public void ClearMemoryItem(int index)
        {
            Memories.RemoveAt(index);
            Console.WriteLine("Clear memory item at {0} index", index);
        }

        public void ClearAllMemory()
        {
            Memories.Clear();
            Console.WriteLine("Clear all memory");
        }

        public void ClearEntry()
        {
            Num2 = 0;
        }

        public void ClearAll()
        {
            Num1 = null;
            Num2 = 0;
        }
    }
}