using System;

namespace xOR
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork nn = new NeuralNetwork(false);

            nn.GetResultNError(new double[] { 1, 0}, 1);

            nn.Learn(100000000);

            nn.GetResultNError(new double[] { 1, 0 }, 1);
            nn.GetResultNError(new double[] { 0, 0 }, 0);
            nn.GetResultNError(new double[] { 1, 1 }, 0);
        }
    }
}
