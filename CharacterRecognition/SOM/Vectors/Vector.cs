using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterRecognition.SOM.Vectors
{
    public class Vector : List<double>, IVector
    {
        public double EuclidianDistance(IVector vector)
        {
            if (vector.Count != Count)
                throw new ArgumentException("Not the same size");

            return this.Select(x => Math.Pow(x - vector[this.IndexOf(x)], 2)).Sum();
        }
    }
}
