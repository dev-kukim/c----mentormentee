namespace MentorMentee_Project
{
    partial class Login
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.login_label_id_static = new System.Windows.Forms.Label();
            this.login_label_pw_static = new System.Windows.Forms.Label();
            this.login_txt_id = new System.Windows.Forms.TextBox();
            this.login_txt_pw = new System.Windows.Forms.TextBox();
            this.login_btn_ok = new System.Windows.Forms.Button();
            this.login_btn_mentorjoin = new System.Windows.Forms.Button();
            this.login_btn_menteejoin = new System.Windows.Forms.Button();
            this.login_btn_exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // login_label_id_static
            // 
            this.login_label_id_static.AutoSize = true;
            this.login_label_id_static.BackColor = System.Drawing.Color.Transparent;
            this.login_label_id_static.ForeColor = System.Drawing.Color.White;
            this.login_label_id_static.Location = new System.Drawing.Point(39, 446);
            this.login_label_id_static.Name = "login_label_id_static";
            this.login_label_id_static.Size = new System.Drawing.Size(41, 12);
            this.login_label_id_static.TabIndex = 0;
            this.login_label_id_static.Text = "아이디";
            // 
            // login_label_pw_static
            // 
            this.login_label_pw_static.AutoSize = true;
            this.login_label_pw_static.BackColor = System.Drawing.Color.Transparent;
            this.login_label_pw_static.ForeColor = System.Drawing.Color.White;
            this.login_label_pw_static.Location = new System.Drawing.Point(27, 476);
            this.login_label_pw_static.Name = "login_label_pw_static";
            this.login_label_pw_static.Size = new System.Drawing.Size(53, 12);
            this.login_label_pw_static.TabIndex = 1;
            this.login_label_pw_static.Text = "비밀번호";
            // 
            // login_txt_id
            // 
            this.login_txt_id.Location = new System.Drawing.Point(104, 437);
            this.login_txt_id.MaxLength = 20;
            this.login_txt_id.Name = "login_txt_id";
            this.login_txt_id.Size = new System.Drawing.Size(100, 21);
            this.login_txt_id.TabIndex = 2;
            this.login_txt_id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.login_txt_id_KeyDown);
            // 
            // login_txt_pw
            // 
            this.login_txt_pw.Location = new System.Drawing.Point(104, 473);
            this.login_txt_pw.MaxLength = 20;
            this.login_txt_pw.Name = "login_txt_pw";
            this.login_txt_pw.Size = new System.Drawing.Size(100, 21);
            this.login_txt_pw.TabIndex = 3;
            this.login_txt_pw.UseSystemPasswordChar = true;
            this.login_txt_pw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.login_txt_pw_KeyDown);
            // 
            // login_btn_ok
            // 
            this.login_btn_ok.BackColor = System.Drawing.Color.LightSeaGreen;
            this.login_btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.login_btn_ok.ForeColor = System.Drawing.Color.White;
            this.login_btn_ok.Location = new System.Drawing.Point(220, 442);
            this.login_btn_ok.Name = "login_btn_ok";
            this.login_btn_ok.Size = new System.Drawing.Size(89, 48);
            this.login_btn_ok.TabIndex = 4;
            this.login_btn_ok.Text = "로그인";
            this.login_btn_ok.UseVisualStyleBackColor = false;
            this.login_btn_ok.Click += new System.EventHandler(this.login_btn_ok_Click);
            // 
            // login_btn_mentorjoin
            // 
            this.login_btn_mentorjoin.BackColor = System.Drawing.Color.MediumAquamarine;
            this.login_btn_mentorjoin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.login_btn_mentorjoin.ForeColor = System.Drawing.Color.White;
            this.login_btn_mentorjoin.Location = new System.Drawing.Point(558, 413);
            this.login_btn_mentorjoin.Name = "login_btn_mentorjoin";
            this.login_btn_mentorjoin.Size = new System.Drawing.Size(115, 31);
            this.login_btn_mentorjoin.TabIndex = 5;
            this.login_btn_mentorjoin.Text = "멘토 회원가입";
            this.login_btn_mentorjoin.UseVisualStyleBackColor = false;
            this.login_btn_mentorjoin.Click += new System.EventHandler(this.login_btn_mentorjoin_Click);
            // 
            // login_btn_menteejoin
            // 
            this.login_btn_menteejoin.BackColor = System.Drawing.Color.MediumAquamarine;
            this.login_btn_menteejoin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.login_btn_menteejoin.ForeColor = System.Drawing.Color.White;
            this.login_btn_menteejoin.Location = new System.Drawing.Point(558, 450);
            this.login_btn_menteejoin.Name = "login_btn_menteejoin";
            this.login_btn_menteejoin.Size = new System.Drawing.Size(115, 32);
            this.login_btn_menteejoin.TabIndex = 6;
            this.login_btn_menteejoin.Text = "멘티 회원가입";
            this.login_btn_menteejoin.UseVisualStyleBackColor = false;
            this.login_btn_menteejoin.Click += new System.EventHandler(this.login_btn_menteejoin_Click);
            // 
            // login_btn_exit
            // 
            this.login_btn_exit.BackColor = System.Drawing.Color.MediumAquamarine;
            this.login_btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.login_btn_exit.ForeColor = System.Drawing.Color.White;
            this.login_btn_exit.Location = new System.Drawing.Point(558, 488);
            this.login_btn_exit.Name = "login_btn_exit";
            this.login_btn_exit.Size = new System.Drawing.Size(115, 32);
            this.login_btn_exit.TabIndex = 7;
            this.login_btn_exit.Text = "종료";
            this.login_btn_exit.UseVisualStyleBackColor = false;
            this.login_btn_exit.Click += new System.EventHandler(this.login_btn_exit_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(702, 543);
            this.Controls.Add(this.login_btn_exit);
            this.Controls.Add(this.login_btn_menteejoin);
            this.Controls.Add(this.login_btn_mentorjoin);
            this.Controls.Add(this.login_btn_ok);
            this.Controls.Add(this.login_txt_pw);
            this.Controls.Add(this.login_txt_id);
            this.Controls.Add(this.login_label_pw_static);
            this.Controls.Add(this.login_label_id_static);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "로그인";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Login_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label login_label_id_static;
        private System.Windows.Forms.Label login_label_pw_static;
        private System.Windows.Forms.TextBox login_txt_id;
        private System.Windows.Forms.TextBox login_txt_pw;
        private System.Windows.Forms.Button login_btn_ok;
        private System.Windows.Forms.Button login_btn_mentorjoin;
        private System.Windows.Forms.Button login_btn_menteejoin;
        private System.Windows.Forms.Button login_btn_exit;
    }
}

