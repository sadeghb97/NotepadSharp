namespace WindowsFormsApplication1
{
    partial class Frmfind
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmfind));
            this.lblfindwhat = new System.Windows.Forms.Label();
            this.txtfind = new System.Windows.Forms.TextBox();
            this.chbmatchcase = new System.Windows.Forms.CheckBox();
            this.btnfind = new System.Windows.Forms.Button();
            this.grbdirection = new System.Windows.Forms.GroupBox();
            this.rdodown = new System.Windows.Forms.RadioButton();
            this.rdoup = new System.Windows.Forms.RadioButton();
            this.btnfindnext = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.grbdirection.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblfindwhat
            // 
            this.lblfindwhat.AutoSize = true;
            this.lblfindwhat.Location = new System.Drawing.Point(8, 19);
            this.lblfindwhat.Name = "lblfindwhat";
            this.lblfindwhat.Size = new System.Drawing.Size(71, 17);
            this.lblfindwhat.TabIndex = 0;
            this.lblfindwhat.Text = "Find What";
            // 
            // txtfind
            // 
            this.txtfind.Location = new System.Drawing.Point(89, 17);
            this.txtfind.Name = "txtfind";
            this.txtfind.Size = new System.Drawing.Size(207, 24);
            this.txtfind.TabIndex = 0;
            this.txtfind.TextChanged += new System.EventHandler(this.txtfind_TextChanged);
            // 
            // chbmatchcase
            // 
            this.chbmatchcase.AutoSize = true;
            this.chbmatchcase.Location = new System.Drawing.Point(10, 91);
            this.chbmatchcase.Name = "chbmatchcase";
            this.chbmatchcase.Size = new System.Drawing.Size(100, 21);
            this.chbmatchcase.TabIndex = 3;
            this.chbmatchcase.Text = "Match Case";
            this.chbmatchcase.UseVisualStyleBackColor = true;
            // 
            // btnfind
            // 
            this.btnfind.Location = new System.Drawing.Point(308, 8);
            this.btnfind.Name = "btnfind";
            this.btnfind.Size = new System.Drawing.Size(97, 32);
            this.btnfind.TabIndex = 1;
            this.btnfind.Text = "Find";
            this.btnfind.UseVisualStyleBackColor = true;
            this.btnfind.Click += new System.EventHandler(this.btnfind_Click);
            // 
            // grbdirection
            // 
            this.grbdirection.Controls.Add(this.rdodown);
            this.grbdirection.Controls.Add(this.rdoup);
            this.grbdirection.Location = new System.Drawing.Point(179, 51);
            this.grbdirection.Name = "grbdirection";
            this.grbdirection.Size = new System.Drawing.Size(115, 61);
            this.grbdirection.TabIndex = 4;
            this.grbdirection.TabStop = false;
            this.grbdirection.Text = "Direction";
            // 
            // rdodown
            // 
            this.rdodown.AutoSize = true;
            this.rdodown.Checked = true;
            this.rdodown.Location = new System.Drawing.Point(58, 28);
            this.rdodown.Name = "rdodown";
            this.rdodown.Size = new System.Drawing.Size(65, 21);
            this.rdodown.TabIndex = 1;
            this.rdodown.TabStop = true;
            this.rdodown.Text = "Down";
            this.rdodown.UseVisualStyleBackColor = true;
            // 
            // rdoup
            // 
            this.rdoup.AutoSize = true;
            this.rdoup.Location = new System.Drawing.Point(6, 29);
            this.rdoup.Name = "rdoup";
            this.rdoup.Size = new System.Drawing.Size(46, 21);
            this.rdoup.TabIndex = 0;
            this.rdoup.Text = "Up";
            this.rdoup.UseVisualStyleBackColor = true;
            // 
            // btnfindnext
            // 
            this.btnfindnext.Location = new System.Drawing.Point(308, 47);
            this.btnfindnext.Name = "btnfindnext";
            this.btnfindnext.Size = new System.Drawing.Size(97, 32);
            this.btnfindnext.TabIndex = 2;
            this.btnfindnext.Text = "Find Next";
            this.btnfindnext.UseVisualStyleBackColor = true;
            this.btnfindnext.Click += new System.EventHandler(this.btnfindnext_Click);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(308, 86);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(97, 32);
            this.btncancel.TabIndex = 4;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // Frmfind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 135);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnfindnext);
            this.Controls.Add(this.grbdirection);
            this.Controls.Add(this.btnfind);
            this.Controls.Add(this.chbmatchcase);
            this.Controls.Add(this.txtfind);
            this.Controls.Add(this.lblfindwhat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frmfind";
            this.Text = "Find";
            this.Load += new System.EventHandler(this.Frmfind_Load);
            this.grbdirection.ResumeLayout(false);
            this.grbdirection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblfindwhat;
        private System.Windows.Forms.TextBox txtfind;
        private System.Windows.Forms.CheckBox chbmatchcase;
        private System.Windows.Forms.Button btnfind;
        private System.Windows.Forms.GroupBox grbdirection;
        private System.Windows.Forms.RadioButton rdodown;
        private System.Windows.Forms.RadioButton rdoup;
        private System.Windows.Forms.Button btnfindnext;
        private System.Windows.Forms.Button btncancel;
    }
}