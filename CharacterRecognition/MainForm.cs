using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CharacterRecognition.SOM;
using CharacterRecognition.SOM.Vectors;

namespace CharacterRecognition
{
    public partial class MainForm : Form
    {
        readonly List<Backpropagation> backpropagation = new List<Backpropagation>();
        readonly List<List<double[]>> images = new List<List<double[]>>();
        readonly List<Kohonen> somMaps = new List<Kohonen>();

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
                var imageLoader = new ImageLoader();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                {
                    var folders = Directory.GetDirectories(folderBrowser.SelectedPath);
                    foreach (var folder in folders)
                        images.Add(imageLoader.LoadImages(folder));
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
                    img[i] = classImages[i];
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
                var input = Utils.BmpToDoubleArray(new Bitmap(openFileDialog.FileName));
                var classNumber = int.Parse(textBoxClass.Text);
                var output = backpropagation[classNumber].Run(input);
                labelClass.Text = string.Join(" ", output);
                labelClass.Refresh();
            }
        }

        void TrainSOMButton_Click(object sender, EventArgs e)
        {
            IVector inputVector = null;

            foreach (var classImages in images)
            {
                var input = new Vector[classImages.Count];
                var index = 0;

                foreach (var image in classImages)
                {
                    inputVector = new Vector();
                    foreach (var value in image) inputVector.Add(value);
                    input[index++] = (Vector) inputVector;
                }

                if (inputVector != null)
                {
                    var som = new Kohonen(5, 5, inputVector.Count, 100, 0.5);
                    som.Train(input);
                    somMaps.Add(som);
                }
            }

            MessageBox.Show("Done training SOM");
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
                var input = Utils.BmpToDoubleArray(new Bitmap(openFileDialog.FileName));

                var inputVector = new Vector();
                foreach (var inputValue in input) inputVector.Add(inputValue);

                var distances = new List<double>();
                foreach (var som in somMaps) distances.Add(som.Train(new[] {inputVector}));

                MessageBox.Show(string.Join(", ", distances));
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