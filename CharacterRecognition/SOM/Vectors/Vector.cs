﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterRecognition.SOM.Vectors
{
    public class Vector : List<double>, IVector
    {
        public double EuclidianDistance(IVector vector)
        {
            if (vector.Count != Count)
                throw new ArgumentException("Not the same size");

            return this.Select(x => Math.Pow(x - vector[IndexOf(x)], 2)).Sum();
        }
    }
}