﻿using CharacterRecognition.SOM;
using CharacterRecognition.SOM.Vectors;
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
        readonly List<Kohonen> somMaps = new List<Kohonen>();

        public MainForm()
        {
            InitializeComponent();
        }

        void ButtonInit_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                
                folderBrowser.SelectedPath = @"C:\Users\Diana\Documents\CharacterRecognition\CharacterRecognition\Characters";
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
            foreach (var classImages in images)
            {
                var output = new double[classImages.Count][];
                var img = new double[classImages.Count][];
                for (int i = 0; i < classImages.Count; i++)
                {
                    img[i] = classImages[i];
                    for (int j = 0; j < classImages.Count; j++)
                        output[i] = new double[classImages.Count];
                    for (int j = 0; j < classImages.Count; j++)
                        if (i == j)
                            output[i][j] = 1;
                        else
                            output[i][j] = 0;
                }
                var network = new Backpropagation(img[0].Length, 200, classImages.Count);
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

        private void TrainSOMButton_Click(object sender, EventArgs e)
        {
            IVector inputVector = null;
           
            foreach (var classImages in images)
            {
                var input = new Vector[classImages.Count];
                int index = 0;

                foreach(var image in classImages)
                {
                    inputVector = new Vector();
                    foreach(var value in image)
                    {
                        inputVector.Add(value);
                    }
                    input[index++] = (Vector)inputVector;
                }

                var som = new Kohonen(5, 5, inputVector.Count, 100, 0.5);
                som.Train(input);
                somMaps.Add(som);
            }
            MessageBox.Show("Done training SOM");
        }

        private void TestSOMButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "bmp (*.bmp)|*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var input = Utils.BmpToDoubleArray(new Bitmap(openFileDialog.FileName));
               
                var inputVector = new Vector();
                foreach(var inputValue in input)
                {
                   inputVector.Add(inputValue);
                }

                var distances = new List<double>();
                foreach(var som in somMaps)
                {
                    distances.Add(som.Train(new Vector[] { inputVector }));
                }

                MessageBox.Show(String.Join(", ", distances));
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
