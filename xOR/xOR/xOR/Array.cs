using System;
using System.Collections.Generic;
using System.Text;

namespace xOR
{
    class Array
    {
        private double[] mass;
        public int Length { get; set; }

        public Array() { }
        public Array(int length)
        {
            mass = new double[length];
            Length = mass.Length;
        }
        public Array(double[] values)
        {
            mass = values;
            Length = mass.Length;
        }

        public double this[int index]
        {
            get
            {
                return mass[index];
            }

            set
            {
                mass[index] = value;
            }
        }
    }
}
