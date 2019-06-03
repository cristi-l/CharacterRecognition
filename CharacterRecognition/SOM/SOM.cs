using System;
using System.Collections.Generic;

namespace CharacterRecognition.SOM
{
    class Som
    {
        const int NetworkSize = 5;
        const int N = 100;
        const int T = 3 * N;
        public readonly Neuron[,] Neurons = new Neuron[NetworkSize, NetworkSize];

        public Som()
        {
            InitNeurons();
        }

        void InitNeurons()
        {
            int width = 20;
            int height = 32;
            int stepX = width / NetworkSize;
            int stepY = height / NetworkSize;
            for (int i = 0; i < NetworkSize; i++)
            {
                for (int j = 0; j < NetworkSize; j++)
                {
                    //neurons[i, j] = new Point(i * stepX, j * stepY);
                    Neurons[i, j] = new Neuron(-width / 2 + j * stepX, height / 2 - i * stepY);
                    //neurons[i, j] = new Point(50, 50, Color.Black);
                }
            }
        }
        public bool verifica()
        {
            for (int i = 0; i < NetworkSize; i++)
            {
                for (int j = 0; j < NetworkSize; j++)
                {
                    if (Neurons[i, j].X < 0 || Neurons[i, j].X > 20)
                        return false;
                    if (Neurons[i, j].Y < 0 || Neurons[i, j].Y > 32)
                        return false;
                }
            }
            return true;
        }

        public void TrainNetwork(List<ImagePixel> pixels)
        {
            bool finish = false;
            for (int t = 0; t < T || finish; t++)
            {
                double v = ComputeVicinity(t);
                double alf = ComputeAlpha(t);

                foreach (var pixel in pixels)
                {
                    double min = double.MaxValue;
                    int iWin = 0, jWin = 0;

                    for (int i = 0; i < NetworkSize; i++)
                    {
                        for (int j = 0; j < NetworkSize; j++)
                        {
                            double dist = ManhattanDistance(new Neuron(pixel.X, pixel.Y), Neurons[i, j]);
                            if (dist < min)
                            {
                                min = dist;
                                iWin = i;
                                jWin = j;
                            }
                        }
                    }


                    if (Math.Abs(alf) < 0.00001)
                    {
                        finish = true;
                        break;
                    }

                    for (int i = iWin - (int)v; i <= (int)v + iWin; i++)
                    {
                        for (int j = jWin - (int)v; j <= (int)v + jWin; j++)
                        {
                            if (i < 0 || j < 0 || i >= NetworkSize || j >= NetworkSize)
                            {
                                continue;
                            }

                            Neurons[i, j] = GetNeuron(new Neuron(pixel.X,pixel.Y), Neurons[i, j], alf);
                        }
                    }
                }
            }
        }

        public double Test(List<ImagePixel> pixels)
        {
            double sum = 0;
            foreach (var pixel in pixels)
            {
                double min = double.MaxValue;
                for (int i = 0; i < NetworkSize; i++)
                {
                    for (int j = 0; j < NetworkSize; j++)
                    {
                        double dist = ManhattanDistance(new Neuron(pixel.X,pixel.Y), Neurons[i, j]);
                        if (dist < min)
                            min = dist;
                    }
                }
                sum += min;
            }
            return sum;
        }

        public Neuron GetNeuron(Neuron currentPoint, Neuron neuron, double alpha)
        {
            Neuron ret = new Neuron(0.0, 0.0);
            ret.X = neuron.X + alpha * (currentPoint.X - neuron.X);
            ret.Y = neuron.Y + alpha * (currentPoint.Y - neuron.Y);

            return ret;
        }

        public double ComputeAlpha(double t)
        {
            return 0.7 * Math.Exp(-t / N);
        }

        public double ComputeVicinity(double t)
        {
            return 7 * Math.Exp(-t / N);
        }


        private double ManhattanDistance(Neuron a, Neuron b)
        {
            //return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
            return Math.Sqrt(Math.Pow(a.X - b.X,2) + Math.Pow(a.Y - b.Y,2));
        }
        public class Neuron
        {
            public double X;
            public double Y;

            public Neuron(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
