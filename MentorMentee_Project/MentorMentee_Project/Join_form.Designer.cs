namespace MentorMentee_Project
{
    partial class Join_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Join_form));
            this.label1 = new System.Windows.Forms.Label();
            this.joinform_gb_flags = new System.Windows.Forms.GroupBox();
            this.joinform_txt_pwcheck = new System.Windows.Forms.TextBox();
            this.joinform_txt_pw = new System.Windows.Forms.TextBox();
            this.joinform_txt_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.joinform_btn_ok = new System.Windows.Forms.Button();
            this.joinform_btn_cancel = new System.Windows.Forms.Button();
            this.joinform_gb_flags.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "아이디";
            // 
            // joinform_gb_flags
            // 
            this.joinform_gb_flags.Controls.Add(this.joinform_txt_pwcheck);
            this.joinform_gb_flags.Controls.Add(this.joinform_txt_pw);
            this.joinform_gb_flags.Controls.Add(this.joinform_txt_id);
            this.joinform_gb_flags.Controls.Add(this.label3);
            this.joinform_gb_flags.Controls.Add(this.label2);
            this.joinform_gb_flags.Controls.Add(this.label1);
            this.joinform_gb_flags.Location = new System.Drawing.Point(16, 12);
            this.joinform_gb_flags.Name = "joinform_gb_flags";
            this.joinform_gb_flags.Size = new System.Drawing.Size(289, 119);
            this.joinform_gb_flags.TabIndex = 1;
            this.joinform_gb_flags.TabStop = false;
            this.joinform_gb_flags.Text = "groupBox1";
            // 
            // joinform_txt_pwcheck
            // 
            this.joinform_txt_pwcheck.Location = new System.Drawing.Point(132, 75);
            this.joinform_txt_pwcheck.MaxLength = 20;
            this.joinform_txt_pwcheck.Name = "joinform_txt_pwcheck";
            this.joinform_txt_pwcheck.Size = new System.Drawing.Size(100, 21);
            this.joinform_txt_pwcheck.TabIndex = 5;
            this.joinform_txt_pwcheck.UseSystemPasswordChar = true;
            // 
            // joinform_txt_pw
            // 
            this.joinform_txt_pw.Location = new System.Drawing.Point(132, 49);
            this.joinform_txt_pw.MaxLength = 20;
            this.joinform_txt_pw.Name = "joinform_txt_pw";
            this.joinform_txt_pw.Size = new System.Drawing.Size(100, 21);
            this.joinform_txt_pw.TabIndex = 4;
            this.joinform_txt_pw.UseSystemPasswordChar = true;
            // 
            // joinform_txt_id
            // 
            this.joinform_txt_id.Location = new System.Drawing.Point(132, 24);
            this.joinform_txt_id.MaxLength = 20;
            this.joinform_txt_id.Name = "joinform_txt_id";
            this.joinform_txt_id.Size = new System.Drawing.Size(100, 21);
            this.joinform_txt_id.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "비밀번호 확인";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "비밀번호";
            // 
            // joinform_btn_ok
            // 
            this.joinform_btn_ok.Location = new System.Drawing.Point(148, 144);
            this.joinform_btn_ok.Name = "joinform_btn_ok";
            this.joinform_btn_ok.Size = new System.Drawing.Size(75, 23);
            this.joinform_btn_ok.TabIndex = 2;
            this.joinform_btn_ok.Text = "확인";
            this.joinform_btn_ok.UseVisualStyleBackColor = true;
            this.joinform_btn_ok.Click += new System.EventHandler(this.joinform_btn_ok_Click);
            // 
            // joinform_btn_cancel
            // 
            this.joinform_btn_cancel.Location = new System.Drawing.Point(230, 144);
            this.joinform_btn_cancel.Name = "joinform_btn_cancel";
            this.joinform_btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.joinform_btn_cancel.TabIndex = 3;
            this.joinform_btn_cancel.Text = "취소";
            this.joinform_btn_cancel.UseVisualStyleBackColor = true;
            this.joinform_btn_cancel.Click += new System.EventHandler(this.joinform_btn_cancel_Click);
            // 
            // Join_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 182);
            this.Controls.Add(this.joinform_btn_cancel);
            this.Controls.Add(this.joinform_btn_ok);
            this.Controls.Add(this.joinform_gb_flags);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Join_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Join_form";
            this.Load += new System.EventHandler(this.Join_form_Load);
            this.joinform_gb_flags.ResumeLayout(false);
            this.joinform_gb_flags.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox joinform_gb_flags;
        private System.Windows.Forms.TextBox joinform_txt_pwcheck;
        private System.Windows.Forms.TextBox joinform_txt_pw;
        private System.Windows.Forms.TextBox joinform_txt_id;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button joinform_btn_ok;
        private System.Windows.Forms.Button joinform_btn_cancel;
    }
}