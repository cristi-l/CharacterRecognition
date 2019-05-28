using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoiseGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string folder;
        Dictionary<string,Bitmap> images = new Dictionary<string, Bitmap>();
        private void Button1_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.SelectedPath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
                var result = folderBrowser.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                {
                    folder = folderBrowser.SelectedPath;
                    var folders = Directory.GetDirectories(folderBrowser.SelectedPath);
                    foreach (var folder in folders)
                    {
                        foreach (var file in Directory.EnumerateFiles(folder))
                            if(file.EndsWith(".bmp"))
                                images.Add(file,new Bitmap(file));
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.SelectedPath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
                var result = folderBrowser.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                {
                    var rand = new Random(42);
                    foreach (var img in images)
                    {
                        for (var i = 0; i < img.Value.Width; i++)
                        for (var j = 0; j < img.Value.Height; j++)
                            if (rand.NextDouble() < decimal.ToDouble(numericUpDown1.Value / 100))
                                img.Value.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                        img.Value.Save(folderBrowser.SelectedPath +"\\"+ numericUpDown1.Value+"noise" +
                                       img.Key.Split(new[]
                                               {
                                                   "\\"
                                               },
                                               StringSplitOptions.RemoveEmptyEntries)
                                           .Last());
                    }
                }
            }
            
        }
    }
}
