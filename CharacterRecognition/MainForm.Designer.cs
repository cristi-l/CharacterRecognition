namespace CharacterRecognition
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonInit = new System.Windows.Forms.Button();
            this.buttonBackpropagation = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.labelClass = new System.Windows.Forms.Label();
            this.textBoxClass = new System.Windows.Forms.TextBox();
            this.trainSOMButton = new System.Windows.Forms.Button();
            this.testSOMButton = new System.Windows.Forms.Button();
            this.buttonLoadWeights = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonInit
            // 
            this.buttonInit.Location = new System.Drawing.Point(12, 12);
            this.buttonInit.Name = "buttonInit";
            this.buttonInit.Size = new System.Drawing.Size(150, 23);
            this.buttonInit.TabIndex = 0;
            this.buttonInit.Text = "Initialize";
            this.buttonInit.UseVisualStyleBackColor = true;
            this.buttonInit.Click += new System.EventHandler(this.ButtonInit_Click);
            // 
            // buttonBackpropagation
            // 
            this.buttonBackpropagation.Location = new System.Drawing.Point(12, 70);
            this.buttonBackpropagation.Name = "buttonBackpropagation";
            this.buttonBackpropagation.Size = new System.Drawing.Size(150, 23);
            this.buttonBackpropagation.TabIndex = 1;
            this.buttonBackpropagation.Text = "Start Backpropagation";
            this.buttonBackpropagation.UseVisualStyleBackColor = true;
            this.buttonBackpropagation.Click += new System.EventHandler(this.buttonBackpropagation_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(12, 99);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(150, 23);
            this.buttonTest.TabIndex = 2;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // labelClass
            // 
            this.labelClass.AutoSize = true;
            this.labelClass.Location = new System.Drawing.Point(180, 75);
            this.labelClass.Name = "labelClass";
            this.labelClass.Size = new System.Drawing.Size(32, 13);
            this.labelClass.TabIndex = 3;
            this.labelClass.Text = "Class";
            // 
            // textBoxClass
            // 
            this.textBoxClass.Location = new System.Drawing.Point(183, 104);
            this.textBoxClass.Name = "textBoxClass";
            this.textBoxClass.Size = new System.Drawing.Size(100, 20);
            this.textBoxClass.TabIndex = 4;
            this.textBoxClass.Text = "0";
            // 
            // trainSOMButton
            // 
            this.trainSOMButton.Location = new System.Drawing.Point(12, 115);
            this.trainSOMButton.Name = "trainSOMButton";
            this.trainSOMButton.Size = new System.Drawing.Size(150, 23);
            this.trainSOMButton.TabIndex = 5;
            this.trainSOMButton.Text = "Train SOM";
            this.trainSOMButton.UseVisualStyleBackColor = true;
            this.trainSOMButton.Click += new System.EventHandler(this.TrainSOMButton_Click);
            // 
            // testSOMButton
            // 
            this.testSOMButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.testSOMButton.Location = new System.Drawing.Point(12, 153);
            this.testSOMButton.Name = "testSOMButton";
            this.testSOMButton.Size = new System.Drawing.Size(150, 23);
            this.testSOMButton.TabIndex = 6;
            this.testSOMButton.Text = "Test SOM";
            this.testSOMButton.UseVisualStyleBackColor = true;
            this.testSOMButton.Click += new System.EventHandler(this.TestSOMButton_Click);
            // 
            // buttonLoadWeights
            // 
            this.buttonLoadWeights.Location = new System.Drawing.Point(12, 41);
            this.buttonLoadWeights.Name = "buttonLoadWeights";
            this.buttonLoadWeights.Size = new System.Drawing.Size(150, 23);
            this.buttonLoadWeights.TabIndex = 5;
            this.buttonLoadWeights.Text = "Load Weights";
            this.buttonLoadWeights.UseVisualStyleBackColor = true;
            this.buttonLoadWeights.Click += new System.EventHandler(this.ButtonLoadWeights_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.testSOMButton);
            this.Controls.Add(this.trainSOMButton);
            this.Controls.Add(this.buttonLoadWeights);
            this.Controls.Add(this.textBoxClass);
            this.Controls.Add(this.labelClass);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.buttonBackpropagation);
            this.Controls.Add(this.buttonInit);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInit;
        private System.Windows.Forms.Button buttonBackpropagation;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Label labelClass;
        private System.Windows.Forms.TextBox textBoxClass;
        private System.Windows.Forms.Button trainSOMButton;
        private System.Windows.Forms.Button testSOMButton;
        private System.Windows.Forms.Button buttonLoadWeights;
    }
}

