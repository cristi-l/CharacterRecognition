using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CharacterRecognition
{
    public class Backpropagation
    {
        readonly double learningStep = 0.6; // Math.Pow(10, -1);

        readonly Random rand = new Random(42);
        double[] biasHidden; //bias value of the "h" neuron in the hidden layer
        double[] biasOutput; //bias value of the "o" neuron in the output layer
        int hiddenNeuronsNumber;

        double[] hiddenValue; //value of the neuron "h" from the hidden layer

        //number of neurons on each of the 3 layers
        int inputNeuronsNumber;

        double[] inputValue; //value of the neuron "i" from the input layer
        int outputNeuronsNumber;
        double[] outputValue; //value of the neuron "o" from the output layer

        //prag = threshold

        double[][]
            w12; //the weight of the link between the neuron "h" from the hidden layer (2) with the neuron "i" from the input layer

        double[][]
            w23; //the weight of the link between the neuron "o" in the output layer (3) with the neuron "h" from the hidden layer

        public Backpropagation(int inputNeuronsNumber, int hiddenNeuronsNumber, int outputNeuronsNumber)
        {
            this.inputNeuronsNumber = inputNeuronsNumber;
            this.hiddenNeuronsNumber = hiddenNeuronsNumber;
            this.outputNeuronsNumber = outputNeuronsNumber;

            inputValue = new double[inputNeuronsNumber];

            w12 = new double[hiddenNeuronsNumber][];
            for (var i = 0; i < hiddenNeuronsNumber; i++) w12[i] = new double[inputNeuronsNumber];

            biasHidden = new double[hiddenNeuronsNumber];
            hiddenValue = new double[hiddenNeuronsNumber];

            w23 = new double[outputNeuronsNumber][];
            for (var i = 0; i < outputNeuronsNumber; i++) w23[i] = new double[hiddenNeuronsNumber];

            biasOutput = new double[outputNeuronsNumber];
            outputValue = new double[outputNeuronsNumber];

            InitWeights();
        }

        public Backpropagation()
        {
        }

        //activation function
        double ActivationFunction(double x)
        {
            return 1.0 / (1 + Math.Exp(-x));
        }

        double ActivationFunctionDerived(double x)
        {
            return x * (1.0 - x);
        }

        void InitWeights()
        {
            for (var h = 0; h < hiddenNeuronsNumber; h++)
            {
                biasHidden[h] = rand.NextDouble();
                for (var i = 0; i < inputNeuronsNumber; i++) w12[h][i] = rand.NextDouble() - 0.5;
            }

            for (var o = 0; o < outputNeuronsNumber; o++)
            {
                biasOutput[o] = rand.NextDouble();
                for (var h = 0; h < hiddenNeuronsNumber; h++) w23[o][h] = rand.NextDouble() - 0.5;
            }
        }

        void Assert(double[][] examples, double[][] targets)
        {
            if (examples == null) throw new ArgumentNullException(nameof(examples));
            if (targets == null) throw new ArgumentNullException(nameof(targets));
            //the size of input examples must match the number of input neurons
            Debug.Assert(examples[0].Length == inputNeuronsNumber);
            //the size of targets (results) must match the number of output neurons
            Debug.Assert(targets[0].Length == outputNeuronsNumber);
            //the size of input example must match the number of results
            Debug.Assert(examples.Length == targets.Length);
        }

        public double[] Run(double[] example)
        {
            Debug.Assert(example.Length == inputNeuronsNumber);
            Forward(example);
            return outputValue;
        }

        public void Train(double[][] examples, double[][] targets)
        {
            Assert(examples, targets);
            var t = 0;
            while (t < 2000)
            {
                TrainEpoch(examples, targets);
                //Debug.WriteLine("E= " + E);
                t++;
            }
        }

        public double TrainEpoch(double[][] examples, double[][] targets)
        {
            double e = 0;
            for (var i = 0; i < examples.Length; i++)
            {
                //take each example and Run it through the network
                Forward(examples[i]);
                e += ComputeError(targets[i]);
                Backward(targets[i]);
            }

            return e;
        }

        void Forward(double[] example)
        {
            ComputeInputLayerOutput(example);
            ComputeHiddenLayer();
            ComputeOutputLayerOutput();
        }

        void ComputeInputLayerOutput(double[] example)
        {
            for (var i = 0; i < example.Length; i++)
                //compute inputValue, which is equal to the input
                inputValue[i] = example[i];
        }

        void ComputeHiddenLayer()
        {
            //compute the hidden layer outputs
            for (var h = 0; h < hiddenNeuronsNumber; h++)
            {
                double s = 0;
                for (var i = 0; i < inputNeuronsNumber; i++)
                    s += w12[h][i] * inputValue[i];
                s += biasHidden[h];
                hiddenValue[h] = ActivationFunction(s);
            }
        }

        void ComputeOutputLayerOutput()
        {
            for (var o = 0; o < outputNeuronsNumber; o++)
            {
                double s = 0;
                for (var h = 0; h < hiddenNeuronsNumber; h++) s += w23[o][h] * hiddenValue[h];
                s += biasOutput[o];
                outputValue[o] = ActivationFunction(s);
            }
        }

        double ComputeError(double[] target)
        {
            double error = 0;
            for (var o = 0; o < outputNeuronsNumber; o++) error += Math.Pow(outputValue[o] - target[o], 2);
            return error;
        }

        void Backward(double[] target)
        {
            UpdateWeightsOutputLayer(target);
            UpdateWeightsHiddenLayer(target);
            UpdateWeightsInputHiddenLayer(target);
            UpdateWeightsHiddenOutputLayer(target);
        }

        void UpdateWeightsOutputLayer(double[] target)
        {
            for (var o = 0; o < outputNeuronsNumber; o++)
            {
                var dw = 2 * (outputValue[o] - target[o]) * ActivationFunctionDerived(outputValue[o]);
                biasOutput[o] = biasOutput[o] - learningStep * dw;
            }
        }

        void UpdateWeightsHiddenLayer(double[] target)
        {
            for (var h = 0; h < hiddenNeuronsNumber; h++)
            {
                double sum = 0;
                for (var o = 0; o < outputNeuronsNumber; o++)
                    sum += (outputValue[o] - target[o]) * ActivationFunctionDerived(outputValue[o]) * w23[o][h];
                var dw = 2 * sum * ActivationFunctionDerived(hiddenValue[h]);
                biasHidden[h] = biasHidden[h] - learningStep * dw;
            }
        }

        void UpdateWeightsInputHiddenLayer(double[] target)
        {
            for (var h = 0; h < hiddenNeuronsNumber; h++)
            for (var i = 0; i < inputNeuronsNumber; i++)
            {
                double sum = 0;
                for (var o = 0; o < outputNeuronsNumber; o++)
                    sum += (outputValue[o] - target[o]) * ActivationFunctionDerived(outputValue[o]) * w23[o][h];
                var dw = 2 * sum * ActivationFunctionDerived(hiddenValue[h]) * inputValue[i];
                w12[h][i] = w12[h][i] - learningStep * dw;
            }
        }

        void UpdateWeightsHiddenOutputLayer(double[] target)
        {
            for (var o = 0; o < outputNeuronsNumber; o++)
            for (var h = 0; h < hiddenNeuronsNumber; h++)
            {
                var dw = 2 * (outputValue[o] - target[o]) * ActivationFunctionDerived(outputValue[o]) * hiddenValue[h];
                w23[o][h] = w23[o][h] - learningStep * dw;
            }
        }

        public string SaveWeights(string path)
        {
            var s = new StringBuilder();
            s.AppendLine($"{inputNeuronsNumber} {hiddenNeuronsNumber} {outputNeuronsNumber}");
            s.AppendLine($"w12 {w12.Length} {w12[0].Length}");
            for (var i = 0; i < w12.Length; i++)
                s.AppendLine(string.Join(",", w12[i]));

            s.AppendLine($"w23 {w23.Length} {w23[0].Length}");
            for (var i = 0; i < w23.Length; i++)
                s.AppendLine(string.Join(",", w23[i]));
            s.AppendLine($"biasHidden {biasHidden.Length}");
            s.AppendLine(string.Join(",", biasHidden));
            s.AppendLine($"biasOutput {biasOutput.Length}");
            s.AppendLine(string.Join(",", biasOutput));
            using (var sw = new StreamWriter(path))
            {
                sw.Write(s.ToString());
            }

            return s.ToString();
        }

        public void LoadWeights(string path)
        {
            using (var sr = new StreamReader(path))
            {
                var line = sr.ReadLine();
                var items = line?.Split(' ');
                if (items != null)
                {
                    inputNeuronsNumber = int.Parse(items[0]);
                    hiddenNeuronsNumber = int.Parse(items[1]);
                    outputNeuronsNumber = int.Parse(items[2]);
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        items = line?.Split(' ');
                        switch (items?[0])
                        {
                            case "w12":
                            {
                                var dim1 = int.Parse(items[1]);
                                var dim2 = int.Parse(items[2]);
                                inputNeuronsNumber = dim2;
                                hiddenNeuronsNumber = dim1;
                                inputValue = new double[inputNeuronsNumber];
                                biasHidden = new double[hiddenNeuronsNumber];
                                hiddenValue = new double[hiddenNeuronsNumber];
                                w12 = new double[dim1][];
                                for (var i = 0; i < dim1; i++)
                                    w12[i] = new double[dim2];
                                for (var i = 0; i < dim1; i++)
                                {
                                    var l = sr.ReadLine();
                                    var values = l?.Split(',');
                                    if (values != null && values.Length == dim2)
                                        for (var j = 0; j < values.Length; j++)
                                            w12[i][j] = double.Parse(values[j]);
                                }
                            }
                                break;
                            case "w23":
                            {
                                var dim1 = int.Parse(items[1]);
                                outputNeuronsNumber = dim1;
                                biasOutput = new double[outputNeuronsNumber];
                                outputValue = new double[outputNeuronsNumber];
                                var dim2 = int.Parse(items[2]);
                                w23 = new double[dim1][];
                                for (var i = 0; i < dim1; i++)
                                    w23[i] = new double[dim2];
                                for (var i = 0; i < dim1; i++)
                                {
                                    var l = sr.ReadLine();
                                    var values = l?.Split(',');
                                    if (values != null && values.Length == dim2)
                                        for (var j = 0; j < values.Length; j++)
                                            w23[i][j] = double.Parse(values[j]);
                                }
                            }
                                break;
                            case "biasHidden":
                            {
                                var dim1 = int.Parse(items[1]);
                                biasHidden = new double[dim1];
                                var values = sr.ReadLine()?.Split(',');
                                for (var i = 0; i < dim1; i++)
                                    if (values != null && values.Length == dim1)
                                        biasHidden[i] = double.Parse(values[i]);
                            }
                                break;
                            case "biasOutput":
                            {
                                var dim1 = int.Parse(items[1]);
                                biasOutput = new double[dim1];
                                var values = sr.ReadLine()?.Split(',');
                                for (var i = 0; i < dim1; i++)
                                    if (values != null && values.Length == dim1)
                                        biasOutput[i] = double.Parse(values[i]);
                            }
                                break;
                        }
                    }
                }
            }
        }
    }
}