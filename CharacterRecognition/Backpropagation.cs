using System;
using System.Diagnostics;

namespace CharacterRecognition
{
    public class Backpropagation
    {
        readonly double learningStep = 0.6; // Math.Pow(10, -1);
        readonly int hiddenNeuronsNumber;

        readonly double[] hiddenValue; //value of the neuron "h" from the hidden layer

        //number of neurons on each of the 3 layers
        readonly int inputNeuronsNumber;

        readonly double[] inputValue; //value of the neuron "i" from the input layer
        readonly int outputNeuronsNumber;
        readonly double[] outputValue; //value of the neuron "o" from the output layer

        readonly Random rand = new Random(42);
        readonly double[] thresholdHidden; //threshold value of the "h" neuron in the hidden layer
        readonly double[] thresholdOutput; //threshold value of the "o" neuron in the output layer

        //prag = threshold

        readonly double[][] w12; //the weight of the link between the neuron "h" from the hidden layer (2) with the neuron "i" from the input layer

        readonly double[][] w23; //the weight of the link between the neuron "o" in the output layer (3) with the neuron "h" from the hidden layer

        public Backpropagation(int inputNeuronsNumber, int hiddenNeuronsNumber, int outputNeuronsNumber)
        {
            this.inputNeuronsNumber = inputNeuronsNumber;
            this.hiddenNeuronsNumber = hiddenNeuronsNumber;
            this.outputNeuronsNumber = outputNeuronsNumber;

            inputValue = new double[inputNeuronsNumber];

            w12 = new double[hiddenNeuronsNumber][];
            for (var i = 0; i < hiddenNeuronsNumber; i++) w12[i] = new double[inputNeuronsNumber];

            thresholdHidden = new double[hiddenNeuronsNumber];
            hiddenValue = new double[hiddenNeuronsNumber];

            w23 = new double[outputNeuronsNumber][];
            for (var i = 0; i < outputNeuronsNumber; i++) w23[i] = new double[hiddenNeuronsNumber];

            thresholdOutput = new double[outputNeuronsNumber];
            outputValue = new double[outputNeuronsNumber];

            InitWeights();
        }

        //activation function
        double ActivationFunction(double x) => 1.0 / (1 + Math.Exp(-x));

        double ActivationFunctionDerived(double x) => (x*(1.0-x));

        void InitWeights()
        {
            for (var h = 0; h < hiddenNeuronsNumber; h++)
            {
                thresholdHidden[h] = rand.NextDouble();
                for (var i = 0; i < inputNeuronsNumber; i++) w12[h][i] = rand.NextDouble() - 0.5;
            }
            for (var o = 0; o < outputNeuronsNumber; o++)
            {
                thresholdOutput[o] = rand.NextDouble();
                for (var h = 0; h < hiddenNeuronsNumber; h++) w23[o][h] = rand.NextDouble() - 0.5;
            }
        }

        void Assert(double[][] examples, double[][] targets)
        {
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
            var E = double.MaxValue;
            var t = 0;
            while (t < 1000)
            {
                E = TrainEpoch(examples, targets);
                //Debug.WriteLine("E= " + E);
                t++;
            }
        }

        public double TrainEpoch(double[][] examples, double[][] targets)
        {
            double E = 0;
            for (var i = 0; i < examples.Length; i++)
            {
                //take each example and Run it through the network
                Forward(examples[i]);
                E += ComputeError(examples[i], targets[i]);
                Backward(targets[i]);
            }
            return E;
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
                s += thresholdHidden[h];
                hiddenValue[h] = ActivationFunction(s);
            }
        }

        void ComputeOutputLayerOutput()
        {
            for (var o = 0; o < outputNeuronsNumber; o++)
            {
                double s = 0;
                for (var h = 0; h < hiddenNeuronsNumber; h++) s += w23[o][h] * hiddenValue[h];
                s += thresholdOutput[o];
                outputValue[o] = ActivationFunction(s);
            }
        }

        double ComputeError(double[] example, double[] target)
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
                thresholdOutput[o] = thresholdOutput[o] - learningStep * dw;
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
                thresholdHidden[h] = thresholdHidden[h] - learningStep * dw;
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
    }
}