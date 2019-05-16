using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CharacterRecognition
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public static class Utils
    {
        public static double[] BmpToDoubleArray(Bitmap bitmap)
        {
            var binarizedValues = new List<double>(bitmap.Width * bitmap.Height);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    var c = bitmap.GetPixel(i, j);
                    var gray = (c.R + c.G + c.B) / 3;
                    if (gray < 20)
                        binarizedValues.Add(1);
                    else
                        binarizedValues.Add(0);
                }
            }
            return binarizedValues.ToArray();
        }
    }

    public class ImageLoader
    {
        public List<double[]> LoadImages(string path)
        {
            var returnValue = new List<double[]>();
            foreach (var file in Directory.EnumerateFiles(path))
            {
               returnValue.Add( Utils.BmpToDoubleArray(new Bitmap(file)));
            }

            return returnValue;
        }
    }

}
