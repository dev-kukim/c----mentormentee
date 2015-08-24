using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MentorMentee_Project
{
    public partial class ChangePW : Form
    {
        public string login_id = "";
        Data_func df = new Data_func();

        public ChangePW()
        {
            InitializeComponent();
        }


        private void changePW_btn_ok_Click(object sender, EventArgs e)
        {
            if (changePW_txt_beforePW.Text == "")
            {
                MessageBox.Show("기존 비밀번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if(changePW_txt_newPW.Text == "")     
            {
                MessageBox.Show("새 비밀번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if(changePW_txt_newPWcheck.Text == "")
            {
                MessageBox.Show("새 비밀번호를 다시 한 번 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (changePW_txt_newPW.Text != changePW_txt_newPWcheck.Text)
            {
                MessageBox.Show("입력한 새 비밀번호랑 일치하지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (changePW_txt_newPW.TextLength <= 4)
            {
                MessageBox.Show("비밀번호는 4자 이상 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                if (df.ChangePW_isMember(login_id, changePW_txt_beforePW.Text))
                {
                    df.ChangePW_updatePW(login_id, changePW_txt_newPWcheck.Text);
                    MessageBox.Show("비밀번호 변경을 완료하였습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                else
                {
                    MessageBox.Show("기존 비밀번호가 일치하지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }

        private void changePW_btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
