using System;
using System.Collections.Generic;
using System.Text;

namespace xOR
{
    class OutputNeuron
    {
        public double Value { get; set; }
        public Array weights;

        public OutputNeuron() { }
        public OutputNeuron(int syns)
        {
            weights = new Array(syns);
        }
    }
}
