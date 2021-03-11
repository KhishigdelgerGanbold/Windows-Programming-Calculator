using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Memory
    {
        public double Value { get; set; }

        public Memory(double value)
        {
            this.Value = value;
        }

        public void MAdd(double value)
        {
            this.Value += value;
        }

        public void MSub(double value)
        {
            this.Value -= value;
        }

        public void MClear()
        {
            this.Value = 0;
        }
    }
}