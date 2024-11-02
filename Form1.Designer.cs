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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.background=new PictureBox();
            this.closeButton=new PictureBox();
            this.playButton=new PictureBox();
            this.websiteButton=new PictureBox();
            this.discordButton=new PictureBox();
            this.progressBar1=new ProgressBar();
            this.updateNoticeLabel=new Label();
            ((System.ComponentModel.ISupportInitialize)this.background).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.closeButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.playButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.websiteButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.discordButton).BeginInit();
            this.SuspendLayout();
            // 
            // background
            // 
            resources.ApplyResources(this.background, "background");
            this.background.Image=Properties.Resources.ImperialAgeLauncher;
            this.background.Name="background";
            this.background.TabStop=false;
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.BackColor=Color.Transparent;
            this.closeButton.Cursor=Cursors.Hand;
            this.closeButton.Image=Properties.Resources.exit1;
            this.closeButton.Name="closeButton";
            this.closeButton.TabStop=false;
            this.closeButton.Click+=this.CloseButton_Click;
            this.closeButton.MouseEnter+=this.CloseButton_MouseEnter;
            this.closeButton.MouseLeave+=this.CloseButton_MouseLeave;
            // 
            // playButton
            // 
            resources.ApplyResources(this.playButton, "playButton");
            this.playButton.BackColor=Color.Transparent;
            this.playButton.Cursor=Cursors.Hand;
            this.playButton.Image=Properties.Resources.jogar1;
            this.playButton.Name="playButton";
            this.playButton.TabStop=false;
            this.playButton.Click+=this.PlayButton_Click;
            this.playButton.MouseEnter+=this.PlayButton_MouseEnter;
            this.playButton.MouseLeave+=this.PlayButton_MouseLeave;
            // 
            // websiteButton
            // 
            resources.ApplyResources(this.websiteButton, "websiteButton");
            this.websiteButton.BackColor=Color.Transparent;
            this.websiteButton.Cursor=Cursors.Hand;
            this.websiteButton.Image=Properties.Resources.website1;
            this.websiteButton.Name="websiteButton";
            this.websiteButton.TabStop=false;
            this.websiteButton.Click+=this.WebsiteButton_Click;
            this.websiteButton.MouseEnter+=this.WebsiteButton_MouseEnter;
            this.websiteButton.MouseLeave+=this.WebsiteButton_MouseLeave;
            // 
            // discordButton
            // 
            resources.ApplyResources(this.discordButton, "discordButton");
            this.discordButton.BackColor=Color.Transparent;
            this.discordButton.Cursor=Cursors.Hand;
            this.discordButton.Image=Properties.Resources.discord1;
            this.discordButton.Name="discordButton";
            this.discordButton.TabStop=false;
            this.discordButton.Click+=this.DiscordButton_Click;
            this.discordButton.MouseEnter+=this.DiscordButton_MouseEnter;
            this.discordButton.MouseLeave+=this.DiscordButton_MouseLeave;
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name="progressBar1";
            // 
            // updateNoticeLabel
            // 
            resources.ApplyResources(this.updateNoticeLabel, "updateNoticeLabel");
            this.updateNoticeLabel.BackColor=Color.Transparent;
            this.updateNoticeLabel.ForeColor=SystemColors.ControlLightLight;
            this.updateNoticeLabel.Name="updateNoticeLabel";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode=AutoScaleMode.Font;
            this.ControlBox=false;
            this.Controls.Add(this.updateNoticeLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.discordButton);
            this.Controls.Add(this.websiteButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.background);
            this.Name="Form1";
            this.Load+=this.Form1_Load;
            ((System.ComponentModel.ISupportInitialize)this.background).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.closeButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.playButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.websiteButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.discordButton).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private PictureBox background;
        private PictureBox closeButton;
        private PictureBox playButton;
        private PictureBox websiteButton;
        private PictureBox discordButton;
        private ProgressBar progressBar1;
        private Label updateNoticeLabel;
    }
}
