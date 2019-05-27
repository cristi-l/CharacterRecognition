using System;
using CharacterRecognition.SOM.Neurons;
using CharacterRecognition.SOM.Vectors;

namespace CharacterRecognition.SOM
{
    internal class Kohonen
    {
        internal int Height;
        internal double LearningRate;
        internal INeuron[,] Matrix;
        internal double MatrixRadius;
        internal double NumberOfIterations;
        internal double TimeConstant;
        internal int Width;

        public Kohonen(int width, int height, int inputDimension, int numberOfIterations, double learningRate)
        {
            Width = width;
            Height = height;
            Matrix = new INeuron[Width, Height];
            NumberOfIterations = numberOfIterations;
            LearningRate = learningRate;

            MatrixRadius = Math.Max(Width, Height) / 2;
            TimeConstant = NumberOfIterations / Math.Log(MatrixRadius);

            InitializeConnections(inputDimension);
        }

        public void Train(Vector[] input)
        {
            var iteration = 0;
            var learningRate = LearningRate;
            var averageDistance = 0;
            var totalDistance = 0;
            while (iteration < NumberOfIterations)
            {
                var currentRadius = CalculateNeighborhoodRadius(iteration);

                for (var i = 0; i < input.Length; i++)
                {
                    var currentInput = input[i];
                    var bmu = CalculateBmu(currentInput);

                    var (xStart, xEnd, yStart, yEnd) = GetRadiusIndexes(bmu, currentRadius);

                    for (var x = xStart; x < xEnd; x++)
                        for (var y = yStart; y < yEnd; y++)
                        {
                            var processingNeuron = GetNeuron(x, y);
                            var distance = bmu.Distance(processingNeuron);
                            if (distance <= Math.Pow(currentRadius, 2.0))
                            {
                                var distanceDrop = GetDistanceDrop(distance, currentRadius);
                                processingNeuron.UpdateWeights(currentInput, learningRate, distanceDrop);
                            }
                        }
                }

                iteration++;
                learningRate = LearningRate * Math.Exp(-(double)iteration / NumberOfIterations);
            }
        }
      
        (int xStart, int xEnd, int yStart, int yEnd) GetRadiusIndexes(INeuron bmu, double currentRadius)
        {
            var xStart = (int)(bmu.X - currentRadius - 1);
            xStart = xStart < 0 ? 0 : xStart;

            var xEnd = (int)(xStart + currentRadius * 2 + 1);
            if (xEnd > Width) xEnd = Width;

            var yStart = (int)(bmu.Y - currentRadius - 1);
            yStart = yStart < 0 ? 0 : yStart;

            var yEnd = (int)(yStart + currentRadius * 2 + 1);
            if (yEnd > Height) yEnd = Height;

            return (xStart, xEnd, yStart, yEnd);
        }

        INeuron GetNeuron(int indexX, int indexY)
        {
            if (indexX > Width || indexY > Height)
                throw new ArgumentException("Wrong index!");

            return Matrix[indexX, indexY];
        }

        double CalculateNeighborhoodRadius(double itteration)
        {
            return MatrixRadius * Math.Exp(-itteration / TimeConstant);
        }

        double GetDistanceDrop(double distance, double radius)
        {
            return Math.Exp(-(Math.Pow(distance, 2.0) / Math.Pow(radius, 2.0)));
        }

        INeuron CalculateBmu(IVector input)
        {
            var bmu = Matrix[0, 0];
            var bestDist = input.EuclidianDistance(bmu.Weights);

            for (var i = 0; i < Width; i++)
                for (var j = 0; j < Height; j++)
                {
                    var distance = input.EuclidianDistance(Matrix[i, j].Weights);
                    if (distance < bestDist)
                    {
                        bmu = Matrix[i, j];
                        bestDist = distance;
                    }
                }

            return bmu;
        }

        void InitializeConnections(int inputDimension)
        {
            for (var i = 0; i < Width; i++)
                for (var j = 0; j < Height; j++)
                    Matrix[i, j] = new Neuron(inputDimension) { X = i, Y = j };
        }
    }
}