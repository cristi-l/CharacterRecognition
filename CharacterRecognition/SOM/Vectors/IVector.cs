using System.Collections.Generic;

namespace CharacterRecognition.SOM.Vectors
{
    public interface IVector : IList<double>
    {
        double EuclidianDistance(IVector vector);
    }
}