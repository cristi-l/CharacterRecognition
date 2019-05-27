using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CharacterRecognition
{
    public partial class MainForm : Form
    {
        readonly List<Backpropagation> backpropagation = new List<Backpropagation>();
        readonly List<List<Image>> images = new List<List<Image>>();
        readonly List<SOM.Som> somMaps = new List<SOM.Som>();

        public MainForm()
        {
            InitializeComponent();
        }

        void ButtonInit_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.SelectedPath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
                var result = folderBrowser.ShowDialog();
                
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                {
                    var folders = Directory.GetDirectories(folderBrowser.SelectedPath);
                    foreach (var folder in folders)
                        images.Add(ImageLoader.LoadImages(folder));
                }
            }
        }

        void buttonBackpropagation_Click(object sender, EventArgs e)
        {
            foreach (var classImages in images)
            {
                var output = new double[classImages.Count][];
                var img = new double[classImages.Count][];
                for (var i = 0; i < classImages.Count; i++)
                {
                    img[i] = classImages[i].GetImageAsArray();
                    for (var j = 0; j < classImages.Count; j++)
                        output[i] = new double[classImages.Count];
                    for (var j = 0; j < classImages.Count; j++)
                        if (i == j)
                            output[i][j] = 1;
                        else
                            output[i][j] = 0;
                }

                var network = new Backpropagation(img[0].Length, 200, classImages.Count);
                network.Train(img, output);
                backpropagation.Add(network);
                network.SaveWeights($"perceptron{images.IndexOf(classImages)}.bp");
            }

            MessageBox.Show("Yeeeey");
        }

        void buttonTest_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "bmp (*.bmp)|*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var input = Utils.BmpToDoubleArray(openFileDialog.FileName);
                var classNumber = int.Parse(textBoxClass.Text);
                var output = backpropagation[classNumber].Run(input.GetImageAsArray());
                labelClass.Text = string.Join(" ", output);
                labelClass.Refresh();
            }
        }

        void TrainSOMButton_Click(object sender, EventArgs e)
        {
            foreach (var classImages in images)
            {
                var som = new SOM.Som();
                foreach (var classImage in classImages)
                {
                    som.TrainNetwork(classImage.Pixels.Where(x=>x.Value==1).ToList());
                }
                somMaps.Add(som);
            }

            MessageBox.Show("Done training SOMs!!!");
        }

        void TestSOMButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "bmp (*.bmp)|*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var input = Utils.BmpToDoubleArray(openFileDialog.FileName);
                var list = new List<double>();
                foreach (var somMap in somMaps)
                {
                    list.Add(somMap.Test(input.Pixels.Where(x => x.Value == 1).ToList()));
                }

                labelClass.Text = string.Join(" ", list);

                textBoxClass.Text = (list.IndexOf(list.Min())+1).ToString();
                labelClass.Refresh();
            }
        }

        void ButtonLoadWeights_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "bp (*.bp)|*.bp";
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileNames.Length == 1)
                {
                    var network = new Backpropagation();
                    network.LoadWeights(openFileDialog.FileName);
                    backpropagation.Add(network);
                }
                else
                {
                    foreach (var fileName in openFileDialog.FileNames)
                    {
                        var network = new Backpropagation();
                        network.LoadWeights(fileName);
                        backpropagation.Add(network);
                    }
                }
            }
        }
    }
}