using System;
using System.Collections.Generic;
using System.Text;

namespace xOR
{
    class InputNeuron
    {
        public double Value { get; set; }
        public int Number { get; set; }

        public InputNeuron() { }
        public InputNeuron(int num)
        {
            Number = num;
        }
    }
}
