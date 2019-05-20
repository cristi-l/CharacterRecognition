using System;
using CharacterRecognition.SOM.Vectors;

namespace CharacterRecognition.SOM.Neurons
{
    internal class Neuron : INeuron
    {
        public Neuron(int numOfWeights)
        {
            var random = new Random();
            Weights = new Vector();

            for (var i = 0; i < numOfWeights; i++) Weights.Add(random.NextDouble());
        }

        public double X { get; set; }
        public double Y { get; set; }
        public IVector Weights { get; }

        public double Distance(INeuron neuron)
        {
            return Math.Pow(X - neuron.X, 2) + Math.Pow(Y - neuron.Y, 2);
        }

        public void SetWeight(int index, double value)
        {
            if (index >= Weights.Count)
                throw new ArgumentException("Wrong index!");

            Weights[index] = value;
        }

        public double GetWeight(int index)
        {
            if (index >= Weights.Count)
                throw new ArgumentException("Wrong index!");

            return Weights[index];
        }

        public void UpdateWeights(IVector input, double distanceDecay, double learningRate)
        {
            if (input.Count != Weights.Count)
                throw new ArgumentException("Wrong input!");

            for (var i = 0; i < Weights.Count; i++)
                Weights[i] += distanceDecay * learningRate * (input[i] - Weights[i]);
        }
    }
}