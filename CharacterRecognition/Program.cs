using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CharacterRecognition
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
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
        public static (double[],int) BmpToDoubleArray(Bitmap bitmap)
        {
            int count = 0;
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
                }
                else
                    binarizedValues.Add(0);
            }

            return (binarizedValues.ToArray(),count);
        }
    }

    public class ImageLoader
    {
        public List<double[]> LoadImages(string path)
        {
            var returnValue = new List<double[]>();
            var s = "Points: ";
            foreach (var file in Directory.EnumerateFiles(path))
                if (file.EndsWith(".bmp"))
                {
                    var a = Utils.BmpToDoubleArray(new Bitmap(file));
                    returnValue.Add(a.Item1);
                    s += a.Item2.ToString() + ", ";
                }
            Console.WriteLine(s);
            return returnValue;
        }
    }
}