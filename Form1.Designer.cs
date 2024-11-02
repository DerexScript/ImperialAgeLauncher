namespace ImperialAgeLauncher {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing&&(components!=null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            background = new PictureBox();
            closeButton = new PictureBox();
            playButton = new PictureBox();
            websiteButton = new PictureBox();
            discordButton = new PictureBox();
            progressBar1 = new ProgressBar();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)background).BeginInit();
            ((System.ComponentModel.ISupportInitialize)closeButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)websiteButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)discordButton).BeginInit();
            SuspendLayout();
            // 
            // background
            // 
            resources.ApplyResources(background, "background");
            background.Image = Properties.Resources.ImperialAgeLauncher;
            background.Name = "background";
            background.TabStop = false;
            // 
            // closeButton
            // 
            resources.ApplyResources(closeButton, "closeButton");
            closeButton.BackColor = Color.Transparent;
            closeButton.Cursor = Cursors.Hand;
            closeButton.Image = Properties.Resources.exit1;
            closeButton.Name = "closeButton";
            closeButton.TabStop = false;
            closeButton.Click += CloseButton_Click;
            closeButton.MouseEnter += CloseButton_MouseEnter;
            closeButton.MouseLeave += CloseButton_MouseLeave;
            // 
            // playButton
            // 
            resources.ApplyResources(playButton, "playButton");
            playButton.BackColor = Color.Transparent;
            playButton.Cursor = Cursors.Hand;
            playButton.Image = Properties.Resources.jogar1;
            playButton.Name = "playButton";
            playButton.TabStop = false;
            playButton.Click += PlayButton_Click;
            playButton.MouseEnter += PlayButton_MouseEnter;
            playButton.MouseLeave += PlayButton_MouseLeave;
            // 
            // websiteButton
            // 
            resources.ApplyResources(websiteButton, "websiteButton");
            websiteButton.BackColor = Color.Transparent;
            websiteButton.Cursor = Cursors.Hand;
            websiteButton.Image = Properties.Resources.website1;
            websiteButton.Name = "websiteButton";
            websiteButton.TabStop = false;
            websiteButton.Click += WebsiteButton_Click;
            websiteButton.MouseEnter += WebsiteButton_MouseEnter;
            websiteButton.MouseLeave += WebsiteButton_MouseLeave;
            // 
            // discordButton
            // 
            resources.ApplyResources(discordButton, "discordButton");
            discordButton.BackColor = Color.Transparent;
            discordButton.Cursor = Cursors.Hand;
            discordButton.Image = Properties.Resources.discord1;
            discordButton.Name = "discordButton";
            discordButton.TabStop = false;
            discordButton.Click += DiscordButton_Click;
            discordButton.MouseEnter += DiscordButton_MouseEnter;
            discordButton.MouseLeave += DiscordButton_MouseLeave;
            // 
            // progressBar1
            // 
            resources.ApplyResources(progressBar1, "progressBar1");
            progressBar1.Name = "progressBar1";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.BackColor = Color.Transparent;
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Name = "label1";
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            ControlBox = false;
            Controls.Add(label1);
            Controls.Add(progressBar1);
            Controls.Add(discordButton);
            Controls.Add(websiteButton);
            Controls.Add(playButton);
            Controls.Add(closeButton);
            Controls.Add(background);
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)background).EndInit();
            ((System.ComponentModel.ISupportInitialize)closeButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)playButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)websiteButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)discordButton).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox background;
        private PictureBox closeButton;
        private PictureBox playButton;
        private PictureBox websiteButton;
        private PictureBox discordButton;
        private ProgressBar progressBar1;
        private Label label1;
    }
}
