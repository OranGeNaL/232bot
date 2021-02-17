using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xOR
{
    class NeuralNetwork
    {
        //{ { 0.45, -0.12}, { 0.78, 0.13}, {1.5, -2.3 } };

        private double E = 0.000001;
        private double a = 0.3;
        

        public InputNeuron[] inputLayer;
        public MiddleNeuron[] hiddenLayer;
        public OutputNeuron outputLayer;

        public NeuralNetwork(bool initialize)
        {
            if(initialize)
            {

            }
            else
            {
                inputLayer = new InputNeuron[2];
                for (int i = 0; i < inputLayer.Length; i++)
                    inputLayer[i] = new InputNeuron(i);

                hiddenLayer = new MiddleNeuron[2];
                for (int i = 0; i < hiddenLayer.Length; i++)
                    hiddenLayer[i] = new MiddleNeuron(2, i);

                hiddenLayer[0].weights = new Array(new double[] { 0.45, -0.12});
                hiddenLayer[1].weights = new Array(new double[] { 0.78, 0.13 });

                outputLayer = new OutputNeuron(2);
                outputLayer.weights = new Array(new double[] { 1.5, -2.3});
            }
        }

        public void Calculate()
        {
            for (int i = 0; i < hiddenLayer.Length; i++)
            {
                double inputData = 0;
                for(int j = 0; j < hiddenLayer[i].weights.Length; j++)
                {
                    inputData += inputLayer[j].Value * hiddenLayer[i].weights[j];
                }
                hiddenLayer[i].Value = Sigmoid(inputData);
            }

            double inputOData = 0;

            for(int i = 0; i < outputLayer.weights.Length; i++)
            {
                inputOData += hiddenLayer[i].Value * outputLayer.weights[i];
            }

            outputLayer.Value = Sigmoid(inputOData);
        }

        public void GetResult(double[] values)
        {
            for (int i = 0; i < values.Length; i++)
                inputLayer[i].Value = values[i];
            Calculate();
            Console.WriteLine("Значение выходного нейрона: {0}", outputLayer.Value);
        }

        public void GetResultNError(double[] values, double ideal)
        {
            for (int i = 0; i < values.Length; i++)
                inputLayer[i].Value = values[i];
            Calculate();
            Console.WriteLine("Значение выходного нейрона: {0}\nЗначение ошибки: {1}", outputLayer.Value, (ideal - outputLayer.Value) * (ideal - outputLayer.Value) * 100);
        }

        public double GetError(double[] values, double ideal)
        {
            for (int i = 0; i < values.Length; i++)
                inputLayer[i].Value = values[i];
            Calculate();
            return (ideal - outputLayer.Value) * (ideal - outputLayer.Value) * 100;
        }

        public double[] UpdateWeights(double[] values, double ideal, double[] prevDeltas)
        {
            double[] deltas = new double[6];
            int m = 0;

            for (int i = 0; i < values.Length; i++)
                inputLayer[i].Value = values[i];
            Calculate();

            double delO1 = (ideal - outputLayer.Value) * ((1 - outputLayer.Value) * outputLayer.Value);

            double[] delH = new double[hiddenLayer.Length];

            for(int i = 0; i < hiddenLayer.Length; i++)
            {
                double delSin = ((1 - hiddenLayer[i].Value) * hiddenLayer[i].Value) * (outputLayer.weights[i] * delO1);

                delH[i] = delSin;

                double GRAD = hiddenLayer[i].Value * delO1;

                double delW = E * GRAD + prevDeltas[m] * a;
                deltas[m] = delW;
                m++;
                outputLayer.weights[i] += delW;
            }

            for(int i = 0; i < inputLayer.Length; i++)
            {
                for(int j = 0; j < hiddenLayer.Length; j++)
                {
                    double GRAD = inputLayer[i].Value * delH[i];
                    double delW = E * GRAD + prevDeltas[m] * a;
                    deltas[m] = delW;
                    m++;

                    hiddenLayer[j].weights[i] += delW;
                }
            }

            return deltas;
        }

        public void Learn(int epochs)
        {
            double[] deltas = { 0, 0, 0, 0, 0, 0 };
            double[] ideals = { 0, 1, 1, 0 };
            List<double[]> train = new List<double[]>();
            train.Add(new double[] { 0, 0});
            train.Add(new double[] { 0, 1 });
            train.Add(new double[] { 1, 0 });
            train.Add(new double[] { 1, 1 });

            List<double> errors = new List<double>();

            for (int i = 0; i < epochs; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    deltas = UpdateWeights(train[j], ideals[j], deltas);
                }
                //errors.Add(GetError(train[1], ideals[1]));
            }

            //WriteToFile(errors);
        }

        public void WriteToFile(List<double> list)
        {
            StreamWriter streamWriter = new StreamWriter("log.txt", true);
            string text = "";
            for (int i = 0; i < list.Count; i++)
            {
                text += list[i].ToString() + "\n";
            }
            streamWriter.WriteLine(text);
            streamWriter.Close();
        }

        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Pow(Math.E, -x));
        }

        public void Print()
        {
            Console.WriteLine("Входные синапсы {0}, {1}", inputLayer[0].Value, inputLayer[1].Value);
            Console.WriteLine("Скрытые синапсы {0}, {1}", hiddenLayer[0].Value, hiddenLayer[1].Value);
        }
    }
}
