namespace WiiTest
{
    partial class Form1
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
            this.pbIR = new System.Windows.Forms.PictureBox();
            this.pbIR2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelZ = new System.Windows.Forms.Label();
            this.status1 = new System.Windows.Forms.Label();
            this.status2 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.initBtn = new System.Windows.Forms.Button();
            this.resetBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbIR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIR2)).BeginInit();
            this.SuspendLayout();
            // 
            // pbIR
            // 
            this.pbIR.Location = new System.Drawing.Point(29, 40);
            this.pbIR.Name = "pbIR";
            this.pbIR.Size = new System.Drawing.Size(512, 384);
            this.pbIR.TabIndex = 0;
            this.pbIR.TabStop = false;
            // 
            // pbIR2
            // 
            this.pbIR2.Location = new System.Drawing.Point(560, 40);
            this.pbIR2.Name = "pbIR2";
            this.pbIR2.Size = new System.Drawing.Size(100, 384);
            this.pbIR2.TabIndex = 1;
            this.pbIR2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(666, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(666, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(666, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Z:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(694, 40);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(16, 17);
            this.labelX.TabIndex = 5;
            this.labelX.Text = "0";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(694, 69);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(16, 17);
            this.labelY.TabIndex = 6;
            this.labelY.Text = "0";
            // 
            // labelZ
            // 
            this.labelZ.AutoSize = true;
            this.labelZ.Location = new System.Drawing.Point(694, 99);
            this.labelZ.Name = "labelZ";
            this.labelZ.Size = new System.Drawing.Size(16, 17);
            this.labelZ.TabIndex = 7;
            this.labelZ.Text = "0";
            // 
            // status1
            // 
            this.status1.AutoSize = true;
            this.status1.Location = new System.Drawing.Point(29, 431);
            this.status1.Name = "status1";
            this.status1.Size = new System.Drawing.Size(45, 17);
            this.status1.TabIndex = 8;
            this.status1.Text = "LOST";
            this.status1.Click += new System.EventHandler(this.label4_Click);
            // 
            // status2
            // 
            this.status2.AutoSize = true;
            this.status2.Location = new System.Drawing.Point(560, 431);
            this.status2.Name = "status2";
            this.status2.Size = new System.Drawing.Size(45, 17);
            this.status2.TabIndex = 9;
            this.status2.Text = "LOST";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(669, 462);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(102, 34);
            this.saveBtn.TabIndex = 10;
            this.saveBtn.Text = "Save File";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // initBtn
            // 
            this.initBtn.Location = new System.Drawing.Point(32, 462);
            this.initBtn.Name = "initBtn";
            this.initBtn.Size = new System.Drawing.Size(137, 33);
            this.initBtn.TabIndex = 11;
            this.initBtn.Text = "Initialize Tracking";
            this.initBtn.UseVisualStyleBackColor = true;
            this.initBtn.Click += new System.EventHandler(this.initBtn_Click);
            // 
            // resetBtn
            // 
            this.resetBtn.Location = new System.Drawing.Point(515, 462);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(132, 34);
            this.resetBtn.TabIndex = 12;
            this.resetBtn.Text = "Reset Draw";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(515, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 29);
            this.button1.TabIndex = 13;
            this.button1.Text = "Switch<>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 521);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.initBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.status2);
            this.Controls.Add(this.status1);
            this.Controls.Add(this.labelZ);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbIR2);
            this.Controls.Add(this.pbIR);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbIR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIR2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbIR;
        private System.Windows.Forms.PictureBox pbIR2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label labelX;
        public System.Windows.Forms.Label labelY;
        public System.Windows.Forms.Label labelZ;
        private System.Windows.Forms.Label status1;
        private System.Windows.Forms.Label status2;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button initBtn;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.Button button1;

    }
}

