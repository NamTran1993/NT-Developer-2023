namespace WinFormsApp
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
            this.btnLog = new System.Windows.Forms.Button();
            this.btnGUID = new System.Windows.Forms.Button();
            this.btnTimersStart = new System.Windows.Forms.Button();
            this.btnTimerStop = new System.Windows.Forms.Button();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnThread = new System.Windows.Forms.Button();
            this.btnThreStop = new System.Windows.Forms.Button();
            this.btnGetHttp = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.btnSendGmail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(12, 12);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(75, 23);
            this.btnLog.TabIndex = 0;
            this.btnLog.Text = "Test Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnGUID
            // 
            this.btnGUID.Location = new System.Drawing.Point(93, 12);
            this.btnGUID.Name = "btnGUID";
            this.btnGUID.Size = new System.Drawing.Size(75, 23);
            this.btnGUID.TabIndex = 1;
            this.btnGUID.Text = "guid";
            this.btnGUID.UseVisualStyleBackColor = true;
            this.btnGUID.Click += new System.EventHandler(this.btnGUID_Click);
            // 
            // btnTimersStart
            // 
            this.btnTimersStart.Location = new System.Drawing.Point(174, 12);
            this.btnTimersStart.Name = "btnTimersStart";
            this.btnTimersStart.Size = new System.Drawing.Size(75, 23);
            this.btnTimersStart.TabIndex = 2;
            this.btnTimersStart.Text = "timers start";
            this.btnTimersStart.UseVisualStyleBackColor = true;
            this.btnTimersStart.Click += new System.EventHandler(this.btnTimersStart_Click);
            // 
            // btnTimerStop
            // 
            this.btnTimerStop.Location = new System.Drawing.Point(255, 12);
            this.btnTimerStop.Name = "btnTimerStop";
            this.btnTimerStop.Size = new System.Drawing.Size(75, 23);
            this.btnTimerStop.TabIndex = 3;
            this.btnTimerStop.Text = "timers stop";
            this.btnTimerStop.UseVisualStyleBackColor = true;
            this.btnTimerStop.Click += new System.EventHandler(this.btnTimerStop_Click);
            // 
            // btnRelease
            // 
            this.btnRelease.Location = new System.Drawing.Point(336, 12);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(121, 23);
            this.btnRelease.TabIndex = 4;
            this.btnRelease.Text = "timers release";
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnThread
            // 
            this.btnThread.Location = new System.Drawing.Point(463, 12);
            this.btnThread.Name = "btnThread";
            this.btnThread.Size = new System.Drawing.Size(117, 23);
            this.btnThread.TabIndex = 5;
            this.btnThread.Text = "thread mana start";
            this.btnThread.UseVisualStyleBackColor = true;
            this.btnThread.Click += new System.EventHandler(this.btnThread_Click);
            // 
            // btnThreStop
            // 
            this.btnThreStop.Location = new System.Drawing.Point(586, 12);
            this.btnThreStop.Name = "btnThreStop";
            this.btnThreStop.Size = new System.Drawing.Size(118, 23);
            this.btnThreStop.TabIndex = 6;
            this.btnThreStop.Text = "thread mana stop";
            this.btnThreStop.UseVisualStyleBackColor = true;
            this.btnThreStop.Click += new System.EventHandler(this.btnThreStop_Click);
            // 
            // btnGetHttp
            // 
            this.btnGetHttp.Location = new System.Drawing.Point(16, 45);
            this.btnGetHttp.Name = "btnGetHttp";
            this.btnGetHttp.Size = new System.Drawing.Size(103, 23);
            this.btnGetHttp.TabIndex = 7;
            this.btnGetHttp.Text = "Get HttpClient";
            this.btnGetHttp.UseVisualStyleBackColor = true;
            this.btnGetHttp.Click += new System.EventHandler(this.btnGetHttp_Click);
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(125, 45);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(103, 23);
            this.btnPost.TabIndex = 8;
            this.btnPost.Text = "Post HttpClient";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // btnSendGmail
            // 
            this.btnSendGmail.Location = new System.Drawing.Point(234, 45);
            this.btnSendGmail.Name = "btnSendGmail";
            this.btnSendGmail.Size = new System.Drawing.Size(75, 23);
            this.btnSendGmail.TabIndex = 9;
            this.btnSendGmail.Text = "send gmail";
            this.btnSendGmail.UseVisualStyleBackColor = true;
            this.btnSendGmail.Click += new System.EventHandler(this.btnSendGmail_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 514);
            this.Controls.Add(this.btnSendGmail);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.btnGetHttp);
            this.Controls.Add(this.btnThreStop);
            this.Controls.Add(this.btnThread);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.btnTimerStop);
            this.Controls.Add(this.btnTimersStart);
            this.Controls.Add(this.btnGUID);
            this.Controls.Add(this.btnLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnLog;
        private Button btnGUID;
        private Button btnTimersStart;
        private Button btnTimerStop;
        private Button btnRelease;
        private Button btnThread;
        private Button btnThreStop;
        private Button btnGetHttp;
        private Button btnPost;
        private Button btnSendGmail;
    }
}