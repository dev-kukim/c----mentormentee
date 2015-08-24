using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentorMentee_Project
{
    public partial class Join_form : Form
    {
        Data_func df = new Data_func();

        private string flags;

        public Join_form(string flag)
        {
            InitializeComponent();
            this.flags = flag;
        }

        private void Join_form_Load(object sender, EventArgs e)
        {
            
            if (flags.Equals("mentor"))
            {
                joinform_gb_flags.Text = "멘토 회원가입";
                this.Text = "회원가입 - 멘토";
                
            }

            else if (flags.Equals("mentee"))
            {
                joinform_gb_flags.Text = "멘티 회원가입";
                this.Text = "회원가입 - 멘티";
            }
        }


        // 가입버튼 눌렀을때..
        private void joinform_btn_ok_Click(object sender, EventArgs e)
        {
            if (joinform_txt_id.Text == "")
            {
                MessageBox.Show("아이디를 입력해 주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (joinform_txt_id.TextLength <= 5)
            {
                MessageBox.Show("아이디는 5자 이상 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (joinform_txt_pw.Text == "" || joinform_txt_pwcheck.Text == "")
            {
                MessageBox.Show("비밀번호를 입력해 주세요.\n(비밀번호 확인 포함)", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (joinform_txt_pw.TextLength <= 4)
            {
                MessageBox.Show("비밀번호는 4자 이상 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (joinform_txt_pw.Text != joinform_txt_pwcheck.Text)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (joinform_txt_id.Text == joinform_txt_pw.Text)
            {
                MessageBox.Show("아이디와 비밀번호가 동일합니다.\n다른 비밀번호를 사용해 주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (df.Join_truncate(joinform_txt_id.Text))
            {
                MessageBox.Show("중복된 아이디가 존재합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                if (flags.Equals("mentor"))
                {
                    Mentor_join mentorjoin = new Mentor_join(this, joinform_txt_id.Text.ToString());
                    mentorjoin.ShowDialog();
                }

                else if (flags.Equals("mentee"))
                {
                    Mentee_join menteejoin = new Mentee_join(this, joinform_txt_id.Text.ToString());
                    menteejoin.ShowDialog();
                }

            }

        }


        // 가입완료(계정부분) 스크립트
        public void join_complete(bool flag)
        {
            df.Join_complete(joinform_txt_id.Text, joinform_txt_pw.Text, flag);
        }


        // 취소버튼 눌렀을때
        private void joinform_btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

  
}
