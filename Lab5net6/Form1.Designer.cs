namespace Lab5net6
{
    partial class MainForm
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
            Logs = new RichTextBox();
            PictureBox = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            ScoreLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)PictureBox).BeginInit();
            SuspendLayout();
            // 
            // Logs
            // 
            Logs.Location = new Point(610, 12);
            Logs.Name = "Logs";
            Logs.Size = new Size(214, 426);
            Logs.TabIndex = 0;
            Logs.Text = "";
            // 
            // PictureBox
            // 
            PictureBox.Location = new Point(12, 33);
            PictureBox.Name = "PictureBox";
            PictureBox.Size = new Size(592, 405);
            PictureBox.TabIndex = 1;
            PictureBox.TabStop = false;
            PictureBox.Click += PictureBox_Click;
            PictureBox.Paint += PictureBox_Paint;
            PictureBox.MouseClick += PictureBox_MouseClick;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 30;
            timer1.Tick += timer1_Tick;
            // 
            // ScoreLabel
            // 
            ScoreLabel.Location = new Point(504, 12);
            ScoreLabel.Name = "ScoreLabel";
            ScoreLabel.Size = new Size(100, 18);
            ScoreLabel.TabIndex = 2;
            ScoreLabel.Text = "Очки: 0";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 450);
            Controls.Add(ScoreLabel);
            Controls.Add(PictureBox);
            Controls.Add(Logs);
            Name = "MainForm";
            Text = "Event Game";
            ((System.ComponentModel.ISupportInitialize)PictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox Logs;
        private PictureBox PictureBox;
        private System.Windows.Forms.Timer timer1;
        private Label ScoreLabel;
    }
}