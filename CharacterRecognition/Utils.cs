using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterRecognition
{
    public static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static (double[] binarizedValues, int pointCount, List<(int row,int column)> points) BmpToTrainingExample(Bitmap bitmap)
        {
            int count = 0;
            List<(int,int)> points = new List<(int, int)>();
            var binarizedValues = new List<double>(bitmap.Width * bitmap.Height);
            for (var i = 0; i < bitmap.Width; i++)
            for (var j = 0; j < bitmap.Height; j++)
            {
                var c = bitmap.GetPixel(i, j);
                var gray = (c.R + c.G + c.B) / 3;
                if (gray < 20)
                {
                    binarizedValues.Add(1);
                    count++;
                    points.Add((i,j));
                }
                else
                    binarizedValues.Add(0);
            }

            return (binarizedValues.ToArray(), count,points);
        }

        public static List<double[]> LoadImages(string path)
        {
            var returnValue = new List<double[]>();
            var s = "Points: ";
            foreach (var file in Directory.EnumerateFiles(path))
                if (file.EndsWith(".bmp"))
                {
                    var a = Utils.BmpToTrainingExample(new Bitmap(file));
                    returnValue.Add(a.binarizedValues);
                    s += a.pointCount.ToString() + ", ";
                }
            Console.WriteLine(s);
            return returnValue;
        }
    }
}
