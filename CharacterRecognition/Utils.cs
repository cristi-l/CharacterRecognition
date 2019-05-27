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
        public static Image BmpToDoubleArray(String file)
        {
            var bitmap = new Bitmap(file);
            var image = new Image
            {
                Label = file.Split('.')[0].Split('\\')[file.Split('.')[0].Split('\\').Length - 1]
            };
            for (var row = 0; row < bitmap.Height; row++)
                for (var column = 0; column < bitmap.Width; column++)
                {
                    var imagePixel = new ImagePixel();
                    var c = bitmap.GetPixel(column, row);
                    var gray = (c.R + c.G + c.B) / 3;
                    imagePixel.X = column;
                    imagePixel.Y = row;
                    if (gray < 20)
                    {
                        imagePixel.Value = 1;
                    }

                    else
                        imagePixel.Value = 0;
                    image.Pixels.Add(imagePixel);
                }

            return image;
        }
        public static List<Image> LoadImages(string path)
        {
            var returnValue = new List<Image>();
            foreach (var file in Directory.EnumerateFiles(path))
                if (file.EndsWith(".bmp"))
                    returnValue.Add(Utils.BmpToDoubleArray(file));

            return returnValue;
        }
    }
    public class Image
    {
        public String Label { get; set; }
        public List<ImagePixel> Pixels { get; set; }
        public Image()
        {
            Pixels = new List<ImagePixel>();
        }
        public double[] GetImageAsArray()
        {
            var imageAsArray = new List<double>();
            foreach (var pixel in Pixels)
            {
                imageAsArray.Add(pixel.Value);
            }
            return imageAsArray.ToArray();
        }
    }

    public class ImagePixel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Value { get; set; }
    }

    public class ImageLoader
    {
        
    }
}
