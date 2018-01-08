namespace WindowsFormsApplication1
{
    partial class frmgoto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmgoto));
            this.lblgotoline = new System.Windows.Forms.Label();
            this.txtgotoline = new System.Windows.Forms.TextBox();
            this.btngoto = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblgotoline
            // 
            this.lblgotoline.AutoSize = true;
            this.lblgotoline.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblgotoline.Location = new System.Drawing.Point(10, 12);
            this.lblgotoline.Name = "lblgotoline";
            this.lblgotoline.Size = new System.Drawing.Size(89, 17);
            this.lblgotoline.TabIndex = 0;
            this.lblgotoline.Text = "Line Number";
            // 
            // txtgotoline
            // 
            this.txtgotoline.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgotoline.HideSelection = false;
            this.txtgotoline.Location = new System.Drawing.Point(12, 33);
            this.txtgotoline.Name = "txtgotoline";
            this.txtgotoline.Size = new System.Drawing.Size(249, 24);
            this.txtgotoline.TabIndex = 1;
            this.txtgotoline.TextChanged += new System.EventHandler(this.txtgotoline_TextChanged);
            this.txtgotoline.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtgotoline_KeyPress);
            // 
            // btngoto
            // 
            this.btngoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngoto.Location = new System.Drawing.Point(95, 64);
            this.btngoto.Name = "btngoto";
            this.btngoto.Size = new System.Drawing.Size(81, 23);
            this.btngoto.TabIndex = 2;
            this.btngoto.Text = "Go To";
            this.btngoto.UseVisualStyleBackColor = true;
            this.btngoto.Click += new System.EventHandler(this.btngoto_Click);
            // 
            // btncancel
            // 
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(181, 64);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(81, 23);
            this.btncancel.TabIndex = 3;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // frmgoto
            // 
            this.AcceptButton = this.btngoto;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 97);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btngoto);
            this.Controls.Add(this.txtgotoline);
            this.Controls.Add(this.lblgotoline);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmgoto";
            this.Text = "Go To Line";
            this.Load += new System.EventHandler(this.frmgoto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblgotoline;
        private System.Windows.Forms.TextBox txtgotoline;
        private System.Windows.Forms.Button btngoto;
        private System.Windows.Forms.Button btncancel;
    }
}