using System;
using System.Collections.Generic;
using System.Text;

namespace xOR
{
    class MiddleNeuron
    {
        public double Value { get; set; }
        public int Number { get; set; }

        public Array weights;

        public MiddleNeuron(int sins, int num)
        {
            weights = new Array(sins);
            Number = num;
        }
        ///
        public MiddleNeuron(int sins, int num, double[] _weights)
        {
            weights = new Array(_weights);
            Number = num;
        }
        ///
    }
}
