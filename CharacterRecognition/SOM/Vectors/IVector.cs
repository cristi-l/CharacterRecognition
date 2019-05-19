using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterRecognition.SOM.Vectors
{
    public interface IVector: IList<double>
    {
        double EuclidianDistance(IVector vector);
    }
}
