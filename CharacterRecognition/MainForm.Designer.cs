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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLetter = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelPredictedLetter = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInit
            // 
            this.buttonInit.Location = new System.Drawing.Point(158, 12);
            this.buttonInit.Name = "buttonInit";
            this.buttonInit.Size = new System.Drawing.Size(150, 23);
            this.buttonInit.TabIndex = 0;
            this.buttonInit.Text = "Initialize";
            this.buttonInit.UseVisualStyleBackColor = true;
            this.buttonInit.Click += new System.EventHandler(this.ButtonInit_Click);
            // 
            // buttonBackpropagation
            // 
            this.buttonBackpropagation.Location = new System.Drawing.Point(33, 59);
            this.buttonBackpropagation.Name = "buttonBackpropagation";
            this.buttonBackpropagation.Size = new System.Drawing.Size(150, 23);
            this.buttonBackpropagation.TabIndex = 1;
            this.buttonBackpropagation.Text = "Start Backpropagation";
            this.buttonBackpropagation.UseVisualStyleBackColor = true;
            this.buttonBackpropagation.Click += new System.EventHandler(this.buttonBackpropagation_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(33, 88);
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
            this.labelClass.Location = new System.Drawing.Point(9, 232);
            this.labelClass.Name = "labelClass";
            this.labelClass.Size = new System.Drawing.Size(32, 13);
            this.labelClass.TabIndex = 3;
            this.labelClass.Text = "Class";
            // 
            // textBoxClass
            // 
            this.textBoxClass.Location = new System.Drawing.Point(138, 164);
            this.textBoxClass.Name = "textBoxClass";
            this.textBoxClass.Size = new System.Drawing.Size(74, 20);
            this.textBoxClass.TabIndex = 4;
            this.textBoxClass.Text = "0";
            // 
            // trainSOMButton
            // 
            this.trainSOMButton.Location = new System.Drawing.Point(35, 76);
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
            this.testSOMButton.Location = new System.Drawing.Point(35, 105);
            this.testSOMButton.Name = "testSOMButton";
            this.testSOMButton.Size = new System.Drawing.Size(150, 23);
            this.testSOMButton.TabIndex = 6;
            this.testSOMButton.Text = "Test SOM";
            this.testSOMButton.UseVisualStyleBackColor = true;
            this.testSOMButton.Click += new System.EventHandler(this.TestSOMButton_Click);
            // 
            // buttonLoadWeights
            // 
            this.buttonLoadWeights.Location = new System.Drawing.Point(33, 30);
            this.buttonLoadWeights.Name = "buttonLoadWeights";
            this.buttonLoadWeights.Size = new System.Drawing.Size(150, 23);
            this.buttonLoadWeights.TabIndex = 5;
            this.buttonLoadWeights.Text = "Load Weights";
            this.buttonLoadWeights.UseVisualStyleBackColor = true;
            this.buttonLoadWeights.Click += new System.EventHandler(this.ButtonLoadWeights_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SOM";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonBackpropagation);
            this.groupBox2.Controls.Add(this.buttonTest);
            this.groupBox2.Controls.Add(this.buttonLoadWeights);
            this.groupBox2.Location = new System.Drawing.Point(254, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 134);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Backpropagation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Class predicted by SOM:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "SOM Outputs";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Backpropagation Outputs";
            // 
            // labelLetter
            // 
            this.labelLetter.AutoSize = true;
            this.labelLetter.Location = new System.Drawing.Point(251, 236);
            this.labelLetter.Name = "labelLetter";
            this.labelLetter.Size = new System.Drawing.Size(34, 13);
            this.labelLetter.TabIndex = 12;
            this.labelLetter.Text = "Letter";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Predicted letter";
            // 
            // labelPredictedLetter
            // 
            this.labelPredictedLetter.AutoSize = true;
            this.labelPredictedLetter.Location = new System.Drawing.Point(344, 193);
            this.labelPredictedLetter.Name = "labelPredictedLetter";
            this.labelPredictedLetter.Size = new System.Drawing.Size(10, 13);
            this.labelPredictedLetter.TabIndex = 14;
            this.labelPredictedLetter.Text = "-";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelPredictedLetter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelLetter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonInit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.testSOMButton);
            this.Controls.Add(this.trainSOMButton);
            this.Controls.Add(this.textBoxClass);
            this.Controls.Add(this.labelClass);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLetter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPredictedLetter;
    }
}

