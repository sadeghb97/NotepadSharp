namespace WindowsFormsApplication1
{
    partial class frmabout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmabout));
            this.picabout = new System.Windows.Forms.PictureBox();
            this.lblname = new System.Windows.Forms.Label();
            this.lblversion = new System.Windows.Forms.Label();
            this.lblcopyright = new System.Windows.Forms.Label();
            this.lbldeveloper = new System.Windows.Forms.Label();
            this.lnkdevelopermail = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picabout)).BeginInit();
            this.SuspendLayout();
            // 
            // picabout
            // 
            this.picabout.Image = ((System.Drawing.Image)(resources.GetObject("picabout.Image")));
            this.picabout.InitialImage = null;
            this.picabout.Location = new System.Drawing.Point(55, 11);
            this.picabout.Name = "picabout";
            this.picabout.Size = new System.Drawing.Size(46, 65);
            this.picabout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picabout.TabIndex = 0;
            this.picabout.TabStop = false;
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.Font = new System.Drawing.Font("Comic Sans MS", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblname.Location = new System.Drawing.Point(107, 14);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(137, 35);
            this.lblname.TabIndex = 1;
            this.lblname.Text = "Notepad#";
            // 
            // lblversion
            // 
            this.lblversion.AutoSize = true;
            this.lblversion.Location = new System.Drawing.Point(110, 52);
            this.lblversion.Name = "lblversion";
            this.lblversion.Size = new System.Drawing.Size(175, 17);
            this.lblversion.TabIndex = 2;
            this.lblversion.Text = "Version: 1.3 (Oct/04/2017)";
            // 
            // lblcopyright
            // 
            this.lblcopyright.AutoSize = true;
            this.lblcopyright.Location = new System.Drawing.Point(59, 92);
            this.lblcopyright.Name = "lblcopyright";
            this.lblcopyright.Size = new System.Drawing.Size(217, 17);
            this.lblcopyright.TabIndex = 3;
            this.lblcopyright.Text = "© 2016 SBPro. All rights reserved.";
            // 
            // lbldeveloper
            // 
            this.lbldeveloper.AutoSize = true;
            this.lbldeveloper.Location = new System.Drawing.Point(57, 113);
            this.lbldeveloper.Name = "lbldeveloper";
            this.lbldeveloper.Size = new System.Drawing.Size(75, 17);
            this.lbldeveloper.TabIndex = 4;
            this.lbldeveloper.Text = "Developer:";
            // 
            // lnkdevelopermail
            // 
            this.lnkdevelopermail.AutoSize = true;
            this.lnkdevelopermail.Location = new System.Drawing.Point(122, 112);
            this.lnkdevelopermail.Name = "lnkdevelopermail";
            this.lnkdevelopermail.Size = new System.Drawing.Size(151, 17);
            this.lnkdevelopermail.TabIndex = 5;
            this.lnkdevelopermail.TabStop = true;
            this.lnkdevelopermail.Text = "sadeghb97@gmail.com";
            this.lnkdevelopermail.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(10, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 2);
            this.label1.TabIndex = 6;
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(117, 141);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(85, 28);
            this.btnok.TabIndex = 0;
            this.btnok.Text = "OK";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // frmabout
            // 
            this.AcceptButton = this.btnok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 183);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkdevelopermail);
            this.Controls.Add(this.lbldeveloper);
            this.Controls.Add(this.lblcopyright);
            this.Controls.Add(this.lblversion);
            this.Controls.Add(this.lblname);
            this.Controls.Add(this.picabout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmabout";
            this.Text = "About Notepad#";
            this.Load += new System.EventHandler(this.frmabout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picabout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picabout;
        private System.Windows.Forms.Label lblname;
        private System.Windows.Forms.Label lblversion;
        private System.Windows.Forms.Label lblcopyright;
        private System.Windows.Forms.Label lbldeveloper;
        private System.Windows.Forms.LinkLabel lnkdevelopermail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnok;
    }
}