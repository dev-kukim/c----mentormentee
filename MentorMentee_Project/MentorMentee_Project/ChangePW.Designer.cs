namespace MentorMentee_Project
{
    partial class ChangePW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePW));
            this.changePW_btn_cancel = new System.Windows.Forms.Button();
            this.changePW_btn_ok = new System.Windows.Forms.Button();
            this.changePW_gb = new System.Windows.Forms.GroupBox();
            this.changePW_txt_newPWcheck = new System.Windows.Forms.TextBox();
            this.changePW_txt_newPW = new System.Windows.Forms.TextBox();
            this.changePW_txt_beforePW = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.changePW_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // changePW_btn_cancel
            // 
            this.changePW_btn_cancel.Location = new System.Drawing.Point(240, 154);
            this.changePW_btn_cancel.Name = "changePW_btn_cancel";
            this.changePW_btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.changePW_btn_cancel.TabIndex = 5;
            this.changePW_btn_cancel.Text = "취소";
            this.changePW_btn_cancel.UseVisualStyleBackColor = true;
            this.changePW_btn_cancel.Click += new System.EventHandler(this.changePW_btn_cancel_Click);
            // 
            // changePW_btn_ok
            // 
            this.changePW_btn_ok.Location = new System.Drawing.Point(158, 154);
            this.changePW_btn_ok.Name = "changePW_btn_ok";
            this.changePW_btn_ok.Size = new System.Drawing.Size(75, 23);
            this.changePW_btn_ok.TabIndex = 4;
            this.changePW_btn_ok.Text = "확인";
            this.changePW_btn_ok.UseVisualStyleBackColor = true;
            this.changePW_btn_ok.Click += new System.EventHandler(this.changePW_btn_ok_Click);
            // 
            // changePW_gb
            // 
            this.changePW_gb.Controls.Add(this.changePW_txt_newPWcheck);
            this.changePW_gb.Controls.Add(this.changePW_txt_newPW);
            this.changePW_gb.Controls.Add(this.changePW_txt_beforePW);
            this.changePW_gb.Controls.Add(this.label3);
            this.changePW_gb.Controls.Add(this.label2);
            this.changePW_gb.Controls.Add(this.label1);
            this.changePW_gb.Location = new System.Drawing.Point(26, 22);
            this.changePW_gb.Name = "changePW_gb";
            this.changePW_gb.Size = new System.Drawing.Size(289, 119);
            this.changePW_gb.TabIndex = 4;
            this.changePW_gb.TabStop = false;
            this.changePW_gb.Text = "비밀번호 변경";
            // 
            // changePW_txt_newPWcheck
            // 
            this.changePW_txt_newPWcheck.Location = new System.Drawing.Point(159, 77);
            this.changePW_txt_newPWcheck.MaxLength = 20;
            this.changePW_txt_newPWcheck.Name = "changePW_txt_newPWcheck";
            this.changePW_txt_newPWcheck.Size = new System.Drawing.Size(100, 21);
            this.changePW_txt_newPWcheck.TabIndex = 3;
            this.changePW_txt_newPWcheck.UseSystemPasswordChar = true;
            // 
            // changePW_txt_newPW
            // 
            this.changePW_txt_newPW.Location = new System.Drawing.Point(159, 51);
            this.changePW_txt_newPW.MaxLength = 20;
            this.changePW_txt_newPW.Name = "changePW_txt_newPW";
            this.changePW_txt_newPW.Size = new System.Drawing.Size(100, 21);
            this.changePW_txt_newPW.TabIndex = 2;
            this.changePW_txt_newPW.UseSystemPasswordChar = true;
            // 
            // changePW_txt_beforePW
            // 
            this.changePW_txt_beforePW.Location = new System.Drawing.Point(159, 26);
            this.changePW_txt_beforePW.MaxLength = 20;
            this.changePW_txt_beforePW.Name = "changePW_txt_beforePW";
            this.changePW_txt_beforePW.Size = new System.Drawing.Size(100, 21);
            this.changePW_txt_beforePW.TabIndex = 1;
            this.changePW_txt_beforePW.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "새 비밀번호 확인";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "새 비밀번호";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "기존 비밀번호";
            // 
            // ChangePW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 191);
            this.Controls.Add(this.changePW_btn_cancel);
            this.Controls.Add(this.changePW_btn_ok);
            this.Controls.Add(this.changePW_gb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "비밀번호 변경";
            this.changePW_gb.ResumeLayout(false);
            this.changePW_gb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button changePW_btn_cancel;
        private System.Windows.Forms.Button changePW_btn_ok;
        private System.Windows.Forms.GroupBox changePW_gb;
        private System.Windows.Forms.TextBox changePW_txt_newPWcheck;
        private System.Windows.Forms.TextBox changePW_txt_newPW;
        private System.Windows.Forms.TextBox changePW_txt_beforePW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}