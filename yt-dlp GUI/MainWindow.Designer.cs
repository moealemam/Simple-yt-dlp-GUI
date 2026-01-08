namespace yt_dlp_GUI
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label labelYtDlpDir;
        private System.Windows.Forms.TextBox textBoxYtDlpDir;
        private System.Windows.Forms.Button buttonBrowseYtDlpDir;
        private System.Windows.Forms.Label labelUrl;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label labelDownloadDir;
        private System.Windows.Forms.TextBox textBoxDownloadDir;
        private System.Windows.Forms.Button buttonBrowseDownloadDir;
        private System.Windows.Forms.Button buttonGetSRTs;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.ComboBox comboSubtitle;
        private System.Windows.Forms.Button buttonDownloadSubtitle;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RichTextBox richTextLog;

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
            this.components = new System.ComponentModel.Container();
            this.labelYtDlpDir = new System.Windows.Forms.Label();
            this.textBoxYtDlpDir = new System.Windows.Forms.TextBox();
            this.buttonBrowseYtDlpDir = new System.Windows.Forms.Button();
            this.labelUrl = new System.Windows.Forms.Label();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.labelDownloadDir = new System.Windows.Forms.Label();
            this.textBoxDownloadDir = new System.Windows.Forms.TextBox();
            this.buttonBrowseDownloadDir = new System.Windows.Forms.Button();
            this.buttonGetSRTs = new System.Windows.Forms.Button();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.comboSubtitle = new System.Windows.Forms.ComboBox();
            this.buttonDownloadSubtitle = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.richTextLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // labelYtDlpDir
            // 
            this.labelYtDlpDir.AutoSize = true;
            this.labelYtDlpDir.Location = new System.Drawing.Point(12, 15);
            this.labelYtDlpDir.Name = "labelYtDlpDir";
            this.labelYtDlpDir.Size = new System.Drawing.Size(148, 15);
            this.labelYtDlpDir.TabIndex = 0;
            this.labelYtDlpDir.Text = "yt-dlp/ffmpeg directory (contains exe):";
            // 
            // textBoxYtDlpDir
            // 
            this.textBoxYtDlpDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYtDlpDir.Location = new System.Drawing.Point(110, 33);
            this.textBoxYtDlpDir.Name = "textBoxYtDlpDir";
            this.textBoxYtDlpDir.PlaceholderText = "e.g. C:\\Tools\\yt-dlp";
            this.textBoxYtDlpDir.Size = new System.Drawing.Size(539, 23);
            this.textBoxYtDlpDir.TabIndex = 1;
            // 
            // buttonBrowseYtDlpDir
            // 
            this.buttonBrowseYtDlpDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseYtDlpDir.Location = new System.Drawing.Point(655, 32);
            this.buttonBrowseYtDlpDir.Name = "buttonBrowseYtDlpDir";
            this.buttonBrowseYtDlpDir.Size = new System.Drawing.Size(133, 27);
            this.buttonBrowseYtDlpDir.TabIndex = 2;
            this.buttonBrowseYtDlpDir.Text = "Browse...";
            this.buttonBrowseYtDlpDir.UseVisualStyleBackColor = true;
            this.buttonBrowseYtDlpDir.Click += new System.EventHandler(this.buttonBrowseYtDlpDir_Click);
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.Location = new System.Drawing.Point(12, 68);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(65, 15);
            this.labelUrl.TabIndex = 3;
            this.labelUrl.Text = "Video URL:";
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUrl.Location = new System.Drawing.Point(12, 86);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.PlaceholderText = "https://www.youtube.com/watch?v=...";
            this.textBoxUrl.Size = new System.Drawing.Size(776, 23);
            this.textBoxUrl.TabIndex = 4;
            // 
            // labelDownloadDir
            // 
            this.labelDownloadDir.AutoSize = true;
            this.labelDownloadDir.Location = new System.Drawing.Point(12, 126);
            this.labelDownloadDir.Name = "labelDownloadDir";
            this.labelDownloadDir.Size = new System.Drawing.Size(84, 15);
            this.labelDownloadDir.TabIndex = 5;
            this.labelDownloadDir.Text = "Save to folder:";
            // 
            // textBoxDownloadDir
            // 
            this.textBoxDownloadDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDownloadDir.Location = new System.Drawing.Point(110, 122);
            this.textBoxDownloadDir.Name = "textBoxDownloadDir";
            this.textBoxDownloadDir.PlaceholderText = "e.g. C:\\Users\\you\\Downloads";
            this.textBoxDownloadDir.Size = new System.Drawing.Size(539, 23);
            this.textBoxDownloadDir.TabIndex = 6;
            // 
            // buttonBrowseDownloadDir
            // 
            this.buttonBrowseDownloadDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseDownloadDir.Location = new System.Drawing.Point(655, 121);
            this.buttonBrowseDownloadDir.Name = "buttonBrowseDownloadDir";
            this.buttonBrowseDownloadDir.Size = new System.Drawing.Size(133, 27);
            this.buttonBrowseDownloadDir.TabIndex = 7;
            this.buttonBrowseDownloadDir.Text = "Browse...";
            this.buttonBrowseDownloadDir.UseVisualStyleBackColor = true;
            this.buttonBrowseDownloadDir.Click += new System.EventHandler(this.buttonBrowseDownloadDir_Click);
            // 
            // buttonGetSRTs
            // 
            this.buttonGetSRTs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetSRTs.Location = new System.Drawing.Point(655, 158);
            this.buttonGetSRTs.Name = "buttonGetSRTs";
            this.buttonGetSRTs.Size = new System.Drawing.Size(133, 27);
            this.buttonGetSRTs.TabIndex = 9;
            this.buttonGetSRTs.Text = "Get available SRTs";
            this.buttonGetSRTs.UseVisualStyleBackColor = true;
            this.buttonGetSRTs.Click += new System.EventHandler(this.buttonGetSRTs_Click);
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Location = new System.Drawing.Point(12, 163);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(92, 15);
            this.labelSubtitle.TabIndex = 8;
            this.labelSubtitle.Text = "Subtitle language:";
            // 
            // comboSubtitle
            // 
            this.comboSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSubtitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSubtitle.FormattingEnabled = true;
            this.comboSubtitle.Items.AddRange(new object[] {
            "None"});
            this.comboSubtitle.Location = new System.Drawing.Point(110, 159);
            this.comboSubtitle.Name = "comboSubtitle";
            this.comboSubtitle.Size = new System.Drawing.Size(539, 23);
            this.comboSubtitle.TabIndex = 10;
            // 
            // buttonDownloadSubtitle
            // 
            this.buttonDownloadSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDownloadSubtitle.Location = new System.Drawing.Point(516, 200);
            this.buttonDownloadSubtitle.Name = "buttonDownloadSubtitle";
            this.buttonDownloadSubtitle.Size = new System.Drawing.Size(133, 27);
            this.buttonDownloadSubtitle.TabIndex = 12;
            this.buttonDownloadSubtitle.Text = "Download subtitle";
            this.buttonDownloadSubtitle.UseVisualStyleBackColor = true;
            this.buttonDownloadSubtitle.Click += new System.EventHandler(this.buttonDownloadSubtitle_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDownload.Location = new System.Drawing.Point(655, 200);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(133, 27);
            this.buttonDownload.TabIndex = 13;
            this.buttonDownload.Text = "Download video";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 200);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(498, 27);
            this.progressBar.TabIndex = 11;
            // 
            // richTextLog
            // 
            this.richTextLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextLog.Location = new System.Drawing.Point(12, 240);
            this.richTextLog.Name = "richTextLog";
            this.richTextLog.ReadOnly = true;
            this.richTextLog.Size = new System.Drawing.Size(776, 198);
            this.richTextLog.TabIndex = 15;
            this.richTextLog.Text = "";
            this.richTextLog.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonBrowseYtDlpDir);
            this.Controls.Add(this.richTextLog);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.buttonDownloadSubtitle);
            this.Controls.Add(this.comboSubtitle);
            this.Controls.Add(this.labelSubtitle);
            this.Controls.Add(this.buttonGetSRTs);
            this.Controls.Add(this.buttonBrowseDownloadDir);
            this.Controls.Add(this.textBoxDownloadDir);
            this.Controls.Add(this.labelDownloadDir);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.labelUrl);
            this.Controls.Add(this.textBoxYtDlpDir);
            this.Controls.Add(this.labelYtDlpDir);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "yt-dlp GUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
