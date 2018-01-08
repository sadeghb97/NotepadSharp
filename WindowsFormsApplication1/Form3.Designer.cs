namespace WindowsFormsApplication1
{
    partial class frmreplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmreplace));
            this.lblfindwhat = new System.Windows.Forms.Label();
            this.btnfindnext = new System.Windows.Forms.Button();
            this.chbmatchcase = new System.Windows.Forms.CheckBox();
            this.txtfindwhat = new System.Windows.Forms.TextBox();
            this.lblreplacewith = new System.Windows.Forms.Label();
            this.txtreplacewith = new System.Windows.Forms.TextBox();
            this.btnreplace = new System.Windows.Forms.Button();
            this.btnreplaceAll = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblfindwhat
            // 
            this.lblfindwhat.AutoSize = true;
            this.lblfindwhat.Location = new System.Drawing.Point(8, 18);
            this.lblfindwhat.Name = "lblfindwhat";
            this.lblfindwhat.Size = new System.Drawing.Size(71, 17);
            this.lblfindwhat.TabIndex = 0;
            this.lblfindwhat.Text = "Find What";
            // 
            // btnfindnext
            // 
            this.btnfindnext.Location = new System.Drawing.Point(290, 7);
            this.btnfindnext.Name = "btnfindnext";
            this.btnfindnext.Size = new System.Drawing.Size(94, 29);
            this.btnfindnext.TabIndex = 2;
            this.btnfindnext.Text = "Find Next";
            this.btnfindnext.UseVisualStyleBackColor = true;
            this.btnfindnext.Click += new System.EventHandler(this.btnfindnext_Click);
            // 
            // chbmatchcase
            // 
            this.chbmatchcase.AutoSize = true;
            this.chbmatchcase.Location = new System.Drawing.Point(10, 121);
            this.chbmatchcase.Name = "chbmatchcase";
            this.chbmatchcase.Size = new System.Drawing.Size(98, 21);
            this.chbmatchcase.TabIndex = 5;
            this.chbmatchcase.Text = "Match case";
            this.chbmatchcase.UseVisualStyleBackColor = true;
            // 
            // txtfindwhat
            // 
            this.txtfindwhat.Location = new System.Drawing.Point(103, 15);
            this.txtfindwhat.Name = "txtfindwhat";
            this.txtfindwhat.Size = new System.Drawing.Size(172, 24);
            this.txtfindwhat.TabIndex = 0;
            this.txtfindwhat.TextChanged += new System.EventHandler(this.txtfindwhat_TextChanged);
            // 
            // lblreplacewith
            // 
            this.lblreplacewith.AutoSize = true;
            this.lblreplacewith.Location = new System.Drawing.Point(8, 49);
            this.lblreplacewith.Name = "lblreplacewith";
            this.lblreplacewith.Size = new System.Drawing.Size(93, 17);
            this.lblreplacewith.TabIndex = 5;
            this.lblreplacewith.Text = "Replace With:";
            // 
            // txtreplacewith
            // 
            this.txtreplacewith.Location = new System.Drawing.Point(103, 47);
            this.txtreplacewith.Name = "txtreplacewith";
            this.txtreplacewith.Size = new System.Drawing.Size(172, 24);
            this.txtreplacewith.TabIndex = 1;
            // 
            // btnreplace
            // 
            this.btnreplace.Location = new System.Drawing.Point(290, 42);
            this.btnreplace.Name = "btnreplace";
            this.btnreplace.Size = new System.Drawing.Size(94, 29);
            this.btnreplace.TabIndex = 3;
            this.btnreplace.Text = "Replace";
            this.btnreplace.UseVisualStyleBackColor = true;
            this.btnreplace.Click += new System.EventHandler(this.btnreplace_Click);
            // 
            // btnreplaceAll
            // 
            this.btnreplaceAll.Location = new System.Drawing.Point(290, 77);
            this.btnreplaceAll.Name = "btnreplaceAll";
            this.btnreplaceAll.Size = new System.Drawing.Size(94, 29);
            this.btnreplaceAll.TabIndex = 4;
            this.btnreplaceAll.Text = "Replace All";
            this.btnreplaceAll.UseVisualStyleBackColor = true;
            this.btnreplaceAll.Click += new System.EventHandler(this.btnreplaceAll_Click);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(290, 112);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(94, 29);
            this.btncancel.TabIndex = 6;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // frmreplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 174);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnreplaceAll);
            this.Controls.Add(this.btnreplace);
            this.Controls.Add(this.txtreplacewith);
            this.Controls.Add(this.lblreplacewith);
            this.Controls.Add(this.txtfindwhat);
            this.Controls.Add(this.chbmatchcase);
            this.Controls.Add(this.btnfindnext);
            this.Controls.Add(this.lblfindwhat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmreplace";
            this.Text = "Replace";
            this.Load += new System.EventHandler(this.frmreplace_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblfindwhat;
        private System.Windows.Forms.Button btnfindnext;
        private System.Windows.Forms.CheckBox chbmatchcase;
        private System.Windows.Forms.TextBox txtfindwhat;
        private System.Windows.Forms.Label lblreplacewith;
        private System.Windows.Forms.TextBox txtreplacewith;
        private System.Windows.Forms.Button btnreplace;
        private System.Windows.Forms.Button btnreplaceAll;
        private System.Windows.Forms.Button btncancel;
    }
}