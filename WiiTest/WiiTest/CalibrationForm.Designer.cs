namespace WiiTest
{
    partial class CalibrationForm
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
            this.calPicBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.calPointLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.calPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // calPicBox
            // 
            this.calPicBox.Location = new System.Drawing.Point(71, 44);
            this.calPicBox.Name = "calPicBox";
            this.calPicBox.Size = new System.Drawing.Size(479, 376);
            this.calPicBox.TabIndex = 0;
            this.calPicBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(71, 445);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "Calibrate point";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // calPointLabel
            // 
            this.calPointLabel.AutoSize = true;
            this.calPointLabel.Location = new System.Drawing.Point(214, 454);
            this.calPointLabel.Name = "calPointLabel";
            this.calPointLabel.Size = new System.Drawing.Size(47, 17);
            this.calPointLabel.TabIndex = 2;
            this.calPointLabel.Text = "point: ";
            // 
            // CalibrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 511);
            this.Controls.Add(this.calPointLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.calPicBox);
            this.Name = "CalibrationForm";
            this.Text = "CalibrationForm";
            ((System.ComponentModel.ISupportInitialize)(this.calPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox calPicBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label calPointLabel;
    }
}