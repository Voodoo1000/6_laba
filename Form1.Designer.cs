namespace _6_laba
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            picDisplay = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            tbDirection = new TrackBar();
            label1 = new Label();
            lblDirection = new Label();
            tbColor = new TrackBar();
            colorDialog1 = new ColorDialog();
            btnChooseColor = new Button();
            tbSpreading = new TrackBar();
            label2 = new Label();
            lblSpreading = new Label();
            chkColorChange = new CheckBox();
            label3 = new Label();
            lblRColor = new Label();
            chkEnableTeleport = new CheckBox();
            tbTeleportDirection = new TrackBar();
            label4 = new Label();
            lblTeleportDirection = new Label();
            chkEnableRadar = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)picDisplay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbDirection).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbSpreading).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbTeleportDirection).BeginInit();
            SuspendLayout();
            // 
            // picDisplay
            // 
            picDisplay.Location = new Point(-2, 1);
            picDisplay.Name = "picDisplay";
            picDisplay.Size = new Size(1094, 548);
            picDisplay.TabIndex = 0;
            picDisplay.TabStop = false;
            picDisplay.MouseClick += picDisplay_MouseClick;
            picDisplay.MouseMove += picDisplay_MouseMove;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 40;
            timer1.Tick += timer1_Tick;
            // 
            // tbDirection
            // 
            tbDirection.Location = new Point(12, 575);
            tbDirection.Maximum = 359;
            tbDirection.Name = "tbDirection";
            tbDirection.Size = new Size(193, 56);
            tbDirection.TabIndex = 1;
            tbDirection.Scroll += tbDirection_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 552);
            label1.Name = "label1";
            label1.Size = new Size(104, 20);
            label1.TabIndex = 2;
            label1.Text = "Направление";
            // 
            // lblDirection
            // 
            lblDirection.AutoSize = true;
            lblDirection.Location = new Point(211, 575);
            lblDirection.Name = "lblDirection";
            lblDirection.Size = new Size(23, 20);
            lblDirection.TabIndex = 3;
            lblDirection.Text = "0°";
            // 
            // tbColor
            // 
            tbColor.Enabled = false;
            tbColor.Location = new Point(331, 617);
            tbColor.Maximum = 100;
            tbColor.Minimum = 10;
            tbColor.Name = "tbColor";
            tbColor.Size = new Size(177, 56);
            tbColor.TabIndex = 4;
            tbColor.Value = 10;
            tbColor.Scroll += tbColor_Scroll;
            // 
            // btnChooseColor
            // 
            btnChooseColor.Enabled = false;
            btnChooseColor.Location = new Point(369, 679);
            btnChooseColor.Name = "btnChooseColor";
            btnChooseColor.Size = new Size(116, 29);
            btnChooseColor.TabIndex = 5;
            btnChooseColor.Text = "Выбрать цвет";
            btnChooseColor.UseVisualStyleBackColor = true;
            btnChooseColor.Click += btnChooseColor_Click;
            // 
            // tbSpreading
            // 
            tbSpreading.Location = new Point(12, 653);
            tbSpreading.Maximum = 200;
            tbSpreading.Minimum = 10;
            tbSpreading.Name = "tbSpreading";
            tbSpreading.Size = new Size(193, 56);
            tbSpreading.TabIndex = 6;
            tbSpreading.Value = 10;
            tbSpreading.Scroll += tbSpreading_Scroll;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 630);
            label2.Name = "label2";
            label2.Size = new Size(117, 20);
            label2.TabIndex = 7;
            label2.Text = "Разброс частиц";
            // 
            // lblSpreading
            // 
            lblSpreading.AutoSize = true;
            lblSpreading.Location = new Point(211, 653);
            lblSpreading.Name = "lblSpreading";
            lblSpreading.Size = new Size(23, 20);
            lblSpreading.TabIndex = 8;
            lblSpreading.Text = "0°";
            // 
            // chkColorChange
            // 
            chkColorChange.AutoSize = true;
            chkColorChange.Location = new Point(343, 555);
            chkColorChange.Name = "chkColorChange";
            chkColorChange.Size = new Size(151, 24);
            chkColorChange.TabIndex = 9;
            chkColorChange.Text = "Перекрашивалка";
            chkColorChange.UseVisualStyleBackColor = true;
            chkColorChange.CheckedChanged += chkColorChange_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(331, 594);
            label3.Name = "label3";
            label3.Size = new Size(56, 20);
            label3.TabIndex = 10;
            label3.Text = "Радиус";
            // 
            // lblRColor
            // 
            lblRColor.AutoSize = true;
            lblRColor.Location = new Point(514, 617);
            lblRColor.Name = "lblRColor";
            lblRColor.Size = new Size(25, 20);
            lblRColor.TabIndex = 11;
            lblRColor.Text = "50";
            // 
            // chkEnableTeleport
            // 
            chkEnableTeleport.AutoSize = true;
            chkEnableTeleport.Location = new Point(623, 555);
            chkEnableTeleport.Name = "chkEnableTeleport";
            chkEnableTeleport.Size = new Size(113, 24);
            chkEnableTeleport.TabIndex = 12;
            chkEnableTeleport.Text = "Телепортер";
            chkEnableTeleport.UseVisualStyleBackColor = true;
            chkEnableTeleport.CheckedChanged += chkEnableTeleport_CheckedChanged;
            // 
            // tbTeleportDirection
            // 
            tbTeleportDirection.Enabled = false;
            tbTeleportDirection.Location = new Point(598, 617);
            tbTeleportDirection.Maximum = 359;
            tbTeleportDirection.Name = "tbTeleportDirection";
            tbTeleportDirection.Size = new Size(168, 56);
            tbTeleportDirection.TabIndex = 13;
            tbTeleportDirection.Scroll += tbTeleportDirection_Scroll;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(598, 594);
            label4.Name = "label4";
            label4.Size = new Size(159, 20);
            label4.TabIndex = 14;
            label4.Text = "Направление выхода";
            // 
            // lblTeleportDirection
            // 
            lblTeleportDirection.AutoSize = true;
            lblTeleportDirection.Location = new Point(772, 617);
            lblTeleportDirection.Name = "lblTeleportDirection";
            lblTeleportDirection.Size = new Size(23, 20);
            lblTeleportDirection.TabIndex = 15;
            lblTeleportDirection.Text = "0°";
            // 
            // chkEnableRadar
            // 
            chkEnableRadar.AutoSize = true;
            chkEnableRadar.Location = new Point(908, 555);
            chkEnableRadar.Name = "chkEnableRadar";
            chkEnableRadar.Size = new Size(72, 24);
            chkEnableRadar.TabIndex = 16;
            chkEnableRadar.Text = "Радар";
            chkEnableRadar.UseVisualStyleBackColor = true;
            chkEnableRadar.CheckedChanged += chkEnableRadar_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1096, 721);
            Controls.Add(chkEnableRadar);
            Controls.Add(lblTeleportDirection);
            Controls.Add(label4);
            Controls.Add(tbTeleportDirection);
            Controls.Add(chkEnableTeleport);
            Controls.Add(lblRColor);
            Controls.Add(label3);
            Controls.Add(chkColorChange);
            Controls.Add(lblSpreading);
            Controls.Add(label2);
            Controls.Add(tbSpreading);
            Controls.Add(btnChooseColor);
            Controls.Add(tbColor);
            Controls.Add(lblDirection);
            Controls.Add(label1);
            Controls.Add(tbDirection);
            Controls.Add(picDisplay);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picDisplay).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbDirection).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbSpreading).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbTeleportDirection).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picDisplay;
        private System.Windows.Forms.Timer timer1;
        private TrackBar tbDirection;
        private Label label1;
        private Label lblDirection;
        private TrackBar tbColor;
        private ColorDialog colorDialog1;
        private Button btnChooseColor;
        private TrackBar tbSpreading;
        private Label label2;
        private Label lblSpreading;
        private CheckBox chkColorChange;
        private Label label3;
        private Label lblRColor;
        private CheckBox chkEnableTeleport;
        private TrackBar tbTeleportDirection;
        private Label label4;
        private Label lblTeleportDirection;
        private CheckBox chkEnableRadar;
    }
}
