﻿namespace QUANLYCHUYENNGANH
{
    partial class frmKetnoiSQL
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
            this.btnketnoi = new System.Windows.Forms.Button();
            this.txtserver = new System.Windows.Forms.TextBox();
            this.txttencsdl = new System.Windows.Forms.TextBox();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnketnoi
            // 
            this.btnketnoi.Location = new System.Drawing.Point(232, 268);
            this.btnketnoi.Name = "btnketnoi";
            this.btnketnoi.Size = new System.Drawing.Size(75, 23);
            this.btnketnoi.TabIndex = 0;
            this.btnketnoi.Text = "button1";
            this.btnketnoi.UseVisualStyleBackColor = true;
            this.btnketnoi.Click += new System.EventHandler(this.btnketnoi_Click);
            // 
            // txtserver
            // 
            this.txtserver.Location = new System.Drawing.Point(193, 54);
            this.txtserver.Name = "txtserver";
            this.txtserver.Size = new System.Drawing.Size(189, 20);
            this.txtserver.TabIndex = 1;
            this.txtserver.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txttencsdl
            // 
            this.txttencsdl.Location = new System.Drawing.Point(193, 103);
            this.txttencsdl.Name = "txttencsdl";
            this.txttencsdl.Size = new System.Drawing.Size(189, 20);
            this.txttencsdl.TabIndex = 2;
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(193, 156);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(189, 20);
            this.txtuser.TabIndex = 3;
            this.txtuser.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtpass
            // 
            this.txtpass.Location = new System.Drawing.Point(193, 207);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(189, 20);
            this.txtpass.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "sever";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ten csdl";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "ten sql";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "mat khau";
            // 
            // frmKetnoiSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 316);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtpass);
            this.Controls.Add(this.txtuser);
            this.Controls.Add(this.txttencsdl);
            this.Controls.Add(this.txtserver);
            this.Controls.Add(this.btnketnoi);
            this.Name = "frmKetnoiSQL";
            this.Text = "KetnoiSQL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnketnoi;
        private System.Windows.Forms.TextBox txtserver;
        private System.Windows.Forms.TextBox txttencsdl;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}