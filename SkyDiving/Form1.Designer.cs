namespace SkyDiving {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.display = new System.Windows.Forms.PictureBox();
            this.fpsLbl = new System.Windows.Forms.Label();
            this.startBtn = new System.Windows.Forms.Button();
            this.drawVectorsCheckBox = new System.Windows.Forms.CheckBox();
            this.nextFrame = new System.Windows.Forms.Button();
            this.frameRateTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.maxFrameRateLbl = new System.Windows.Forms.Label();
            this.distanceTravelledTitleLbl = new System.Windows.Forms.Label();
            this.distanceTravelledLbl = new System.Windows.Forms.Label();
            this.openParachuteBtn = new System.Windows.Forms.Button();
            this.simulationsComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.massLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameRateTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.display.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.display.Location = new System.Drawing.Point(0, 0);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(640, 442);
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            this.display.MouseClick += new System.Windows.Forms.MouseEventHandler(this.display_MouseClick);
            // 
            // fpsLbl
            // 
            this.fpsLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fpsLbl.AutoSize = true;
            this.fpsLbl.Location = new System.Drawing.Point(672, 419);
            this.fpsLbl.MinimumSize = new System.Drawing.Size(100, 0);
            this.fpsLbl.Name = "fpsLbl";
            this.fpsLbl.Size = new System.Drawing.Size(100, 13);
            this.fpsLbl.TabIndex = 1;
            this.fpsLbl.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(646, 39);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(55, 23);
            this.startBtn.TabIndex = 2;
            this.startBtn.Text = "Start";
            this.startBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // drawVectorsCheckBox
            // 
            this.drawVectorsCheckBox.AutoSize = true;
            this.drawVectorsCheckBox.Location = new System.Drawing.Point(646, 68);
            this.drawVectorsCheckBox.Name = "drawVectorsCheckBox";
            this.drawVectorsCheckBox.Size = new System.Drawing.Size(87, 17);
            this.drawVectorsCheckBox.TabIndex = 3;
            this.drawVectorsCheckBox.Text = "draw vectors";
            this.drawVectorsCheckBox.UseVisualStyleBackColor = true;
            this.drawVectorsCheckBox.CheckedChanged += new System.EventHandler(this.drawVectorsCheckBox_CheckedChanged);
            // 
            // nextFrame
            // 
            this.nextFrame.Location = new System.Drawing.Point(707, 39);
            this.nextFrame.Name = "nextFrame";
            this.nextFrame.Size = new System.Drawing.Size(74, 23);
            this.nextFrame.TabIndex = 4;
            this.nextFrame.Text = "Next frame";
            this.nextFrame.UseVisualStyleBackColor = true;
            this.nextFrame.Click += new System.EventHandler(this.nextFrame_Click);
            // 
            // frameRateTrackBar
            // 
            this.frameRateTrackBar.LargeChange = 20;
            this.frameRateTrackBar.Location = new System.Drawing.Point(646, 384);
            this.frameRateTrackBar.Maximum = 100;
            this.frameRateTrackBar.Minimum = 1;
            this.frameRateTrackBar.Name = "frameRateTrackBar";
            this.frameRateTrackBar.Size = new System.Drawing.Size(135, 45);
            this.frameRateTrackBar.TabIndex = 1;
            this.frameRateTrackBar.TickFrequency = 10;
            this.frameRateTrackBar.Value = 100;
            this.frameRateTrackBar.Scroll += new System.EventHandler(this.frameRateTrackBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(655, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Max. framerate: ";
            // 
            // maxFrameRateLbl
            // 
            this.maxFrameRateLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.maxFrameRateLbl.AutoSize = true;
            this.maxFrameRateLbl.Location = new System.Drawing.Point(744, 367);
            this.maxFrameRateLbl.Name = "maxFrameRateLbl";
            this.maxFrameRateLbl.Size = new System.Drawing.Size(25, 13);
            this.maxFrameRateLbl.TabIndex = 6;
            this.maxFrameRateLbl.Text = "100";
            this.maxFrameRateLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // distanceTravelledTitleLbl
            // 
            this.distanceTravelledTitleLbl.AutoSize = true;
            this.distanceTravelledTitleLbl.Location = new System.Drawing.Point(670, 177);
            this.distanceTravelledTitleLbl.Name = "distanceTravelledTitleLbl";
            this.distanceTravelledTitleLbl.Size = new System.Drawing.Size(95, 13);
            this.distanceTravelledTitleLbl.TabIndex = 7;
            this.distanceTravelledTitleLbl.Text = "Distance travelled:";
            // 
            // distanceTravelledLbl
            // 
            this.distanceTravelledLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.distanceTravelledLbl.AutoSize = true;
            this.distanceTravelledLbl.Location = new System.Drawing.Point(665, 190);
            this.distanceTravelledLbl.MinimumSize = new System.Drawing.Size(100, 0);
            this.distanceTravelledLbl.Name = "distanceTravelledLbl";
            this.distanceTravelledLbl.Size = new System.Drawing.Size(100, 13);
            this.distanceTravelledLbl.TabIndex = 8;
            this.distanceTravelledLbl.Text = "0 m";
            this.distanceTravelledLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // openParachuteBtn
            // 
            this.openParachuteBtn.Location = new System.Drawing.Point(650, 306);
            this.openParachuteBtn.Name = "openParachuteBtn";
            this.openParachuteBtn.Size = new System.Drawing.Size(122, 23);
            this.openParachuteBtn.TabIndex = 9;
            this.openParachuteBtn.Text = "Open parachute";
            this.openParachuteBtn.UseVisualStyleBackColor = true;
            this.openParachuteBtn.Click += new System.EventHandler(this.openParachuteBtn_Click);
            // 
            // simulationsComboBox
            // 
            this.simulationsComboBox.FormattingEnabled = true;
            this.simulationsComboBox.Location = new System.Drawing.Point(646, 12);
            this.simulationsComboBox.Name = "simulationsComboBox";
            this.simulationsComboBox.Size = new System.Drawing.Size(133, 21);
            this.simulationsComboBox.TabIndex = 10;
            this.simulationsComboBox.SelectedIndexChanged += new System.EventHandler(this.simulationsComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(729, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Mass:";
            // 
            // massLbl
            // 
            this.massLbl.AutoSize = true;
            this.massLbl.Location = new System.Drawing.Point(664, 232);
            this.massLbl.MinimumSize = new System.Drawing.Size(100, 0);
            this.massLbl.Name = "massLbl";
            this.massLbl.Size = new System.Drawing.Size(100, 13);
            this.massLbl.TabIndex = 12;
            this.massLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(672, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "(or press space)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.massLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.simulationsComboBox);
            this.Controls.Add(this.openParachuteBtn);
            this.Controls.Add(this.distanceTravelledLbl);
            this.Controls.Add(this.distanceTravelledTitleLbl);
            this.Controls.Add(this.maxFrameRateLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nextFrame);
            this.Controls.Add(this.drawVectorsCheckBox);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.fpsLbl);
            this.Controls.Add(this.display);
            this.Controls.Add(this.frameRateTrackBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(800, 480);
            this.MinimumSize = new System.Drawing.Size(800, 480);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sky Diving Simulation";
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameRateTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox display;
        private System.Windows.Forms.Label fpsLbl;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.CheckBox drawVectorsCheckBox;
        private System.Windows.Forms.Button nextFrame;
        private System.Windows.Forms.TrackBar frameRateTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label maxFrameRateLbl;
        private System.Windows.Forms.Label distanceTravelledTitleLbl;
        private System.Windows.Forms.Label distanceTravelledLbl;
        private System.Windows.Forms.Button openParachuteBtn;
        private System.Windows.Forms.ComboBox simulationsComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label massLbl;
        private System.Windows.Forms.Label label3;
    }
}

