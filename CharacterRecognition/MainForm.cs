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

namespace CharacterRecognition
{
    public partial class MainForm : Form
    {
        readonly List<List<double[]>> images = new List<List<double[]>>();
        readonly List<Backpropagation> backpropagation = new List<Backpropagation>();

        public MainForm()
        {
            InitializeComponent();
        }

        void ButtonInit_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.SelectedPath = @"C:\Users\Larisa\Source\Repos\CharacterRecognition\CharacterRecognition\Characters";
                DialogResult result = folderBrowser.ShowDialog();
                var imageLoader = new ImageLoader();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                {
                    string[] folders = Directory.GetDirectories(folderBrowser.SelectedPath);
                    foreach (var folder in folders)
                        images.Add(imageLoader.LoadImages(folder));
                }
            }
        }

        void buttonBackpropagation_Click(object sender, EventArgs e)
        {
            foreach (var example in images)
            {
                var output = new double[example.Count][];
                var img = new double[example.Count][];
                for (int i = 0; i < example.Count; i++)
                {
                    img[i] = example[i];
                    for (int j = 0; j < example.Count; j++)
                        output[i] = new double[example.Count];
                    for (int j = 0; j < example.Count; j++)
                        if (i == j)
                            output[i][j] = 1;
                        else
                            output[i][j] = 0;
                }
                var network = new Backpropagation(img[0].Length, 200, example.Count);
                network.Train(img, output);
                backpropagation.Add(network);
                network.SaveWeights($"perceptron{images.IndexOf(example)}.bp");
            }
            MessageBox.Show("Yeeeey");
        }

        void buttonTest_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "bmp (*.bmp)|*.bmp";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var input = Utils.BmpToDoubleArray(new Bitmap(openFileDialog.FileName));
                var classNumber = int.Parse(textBoxClass.Text);
                var output = backpropagation[classNumber].Run(input);
                labelClass.Text = String.Join(" ", output);
                labelClass.Refresh();

            }
        }

        private void ButtonLoadWeights_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "bp (*.bp)|*.bp";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var network = new Backpropagation();
                network.LoadWeights(openFileDialog.FileName);
                backpropagation.Add(network);

            }
        }
    }
}
