using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentorMentee_Project
{
    public partial class Login : Form
    {

        private Point mouse_point; 

        public Login()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void login_btn_ok_Click(object sender, EventArgs e)
        {
            Data_func df = new Data_func();
            string result = df.login_check(login_txt_id.Text, login_txt_pw.Text);

            if (result.Equals("mentor"))
            {
                this.Hide();
                Mentor_main mentor = new Mentor_main();
                mentor.login_id = login_txt_id.Text;
                mentor.Show();
                
            }

            else if (result.Equals("mentee"))
            {
                this.Hide();
                Mentee_main mentee = new Mentee_main();
                mentee.login_id = login_txt_id.Text;
                mentee.Show();
                
            }

            else if (result.Equals("admin"))
            {
                this.Hide();
                Admin_main admin = new Admin_main();
                admin.Show();
            }

            else
            {
                result = "";
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void login_btn_mentorjoin_Click(object sender, EventArgs e)
        {
            Join_form join = new Join_form("mentor");
            join.ShowDialog();
        }

        private void login_btn_menteejoin_Click(object sender, EventArgs e)
        {
            Join_form join = new Join_form("mentee");
            join.ShowDialog();
        }


        // 엔터키 로그인
        private void login_txt_id_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.Enter)
             {
                 login_btn_ok_Click(sender,e);
             }
        }

        // 엔터키 로그인
        private void login_txt_pw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login_btn_ok_Click(sender, e);
            }
        }


        // Form Border Style을 none로 했을 경우 마우스를 클릭한 상태에서 움직일 경우에는 별도의 이벤트 처리가 필요.
        // 마우스 클릭 위치값 받아오기
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_point = new Point(e.X, e.Y);
        }

        // 클릭 상태로 마우스를 이동 시 이동한 만큼에서 윈도우 위치값을 뺌 - 위에 작성한 함수값을 기반으로 작동
        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mouse_point.X - e.X),
                    this.Top - (mouse_point.Y - e.Y));
            }
        }

        private void login_btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
