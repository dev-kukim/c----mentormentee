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
    public partial class Mentor_main : Form
    {
        Data_func df = new Data_func();
        
        public string login_id = "";            // 로그인된 아이디
        public static string requested_time = "";
        public static string groupname = "";


        public Mentor_main()
        {
            InitializeComponent();
            DoubleBuffered = true;      // 리스트뷰 속도 개선용..
        }

        private void Mentor_main_Load(object sender, EventArgs e)
        {
            // 시계 관련 클래스
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            // 시계 관련 클래스 끝

            mentor_gv_allrequestlist.DataSource = df.Mentor_request(login_id);
            mentor_gv_allrequestlist_Customizing();

            changelabel();
            reset_label();
            mentor_btn_approve.Enabled = false;
            mentor_btn_delete.Enabled = false;
            mentor_btn_exit.Enabled = false;
            mentor_btn_refuse.Enabled = false;

            mentor_gv_allrequestlist.ClearSelection();

        }

        // 진행현황 카운팅
        private void changelabel()
        {
            Dictionary<string, string> dic = df.Mentor_statusCount(login_id);
            mentor_lbl_processing.Text = dic["processing"];
            mentor_lbl_waiting.Text = dic["waiting"];
            mentor_lbl_canceled.Text = dic["canceled"];
            mentor_lbl_finished.Text = dic["finished"];
        }


        // 라벨 리셋
        private void reset_label()
        {
            mentor_lbl_groupname.Text = "";
            mentor_lbl_age.Text = "";
            mentor_lbl_sex.Text = "";
            mentor_lbl_location.Text = "";
            mentor_lbl_jobs.Text = "";
            mentor_lbl_tel.Text = "";
            mentor_lbl_part.Text = "";
            mentor_lbl_company.Text = "";
            mentor_lbl_address.Text = "";
            mentor_lbl_post.Text = "";
            mentor_lbl_term.Text = "";
            mentor_lbl_time.Text = "";
            mentor_lbl_requestdate.Text = "";
            mentor_lbl_status.Text = "";
        }

        // 시계 -_-
        void timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = login_id +"님 환영합니다.  현재시각 : " + DateTime.Now.ToString();
        }


        // 멘토링 요청 목록 커스터마이징
        private void mentor_gv_allrequestlist_Customizing()
        {
            mentor_gv_allrequestlist.AutoGenerateColumns = false;   // 이 코드를 안써주면 지 마음대로 그리드뷰가 생성되어서 순서가 안먹힘.

            mentor_gv_allrequestlist.Columns["R_matching"].HeaderText = "상태";
            mentor_gv_allrequestlist.Columns["R_mentee_groupname"].HeaderText = "그룹명";
            mentor_gv_allrequestlist.Columns["R_mentee_location"].Visible = false;                 // 지역 부분 미출력
            mentor_gv_allrequestlist.Columns["R_mentee_lowage"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_highage"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_male"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_female"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_job"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_company"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_tel"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_address"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_post1"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_post2"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_interestarea1"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_interestarea2"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_interestarea3"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_favorjob"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_name"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_educational"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_soldierclass"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_major"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_jobs"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_company"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_depart"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_location"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_part1"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_part2"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_part3"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_tel"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_post1"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_post2"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_address"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_sex"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_age"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_email"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_domain"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentee_id"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_mentor_id"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_request_time"].HeaderText = "신청일";
            mentor_gv_allrequestlist.Columns["R_cancel_time"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_start_time"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_finish_time"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_matching_time"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_complete_time"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_admin_agree_time"].Visible = false;
            mentor_gv_allrequestlist.Columns["R_clock_time"].Visible = false;

            mentor_gv_allrequestlist.Columns["R_matching"].DisplayIndex = 1;
            mentor_gv_allrequestlist.Columns["R_mentee_groupname"].DisplayIndex = 2;
            mentor_gv_allrequestlist.Columns["성별(남/여)"].DisplayIndex = 3;
            mentor_gv_allrequestlist.Columns["나이"].DisplayIndex = 4;
            mentor_gv_allrequestlist.Columns["R_mentee_company"].DisplayIndex = 5;

       
        }


        // 멘토링 요청 목록이 바인딩 될 때 - 코드값을 이름으로 바꾸고..등등
        private void mentor_gv_allrequestlist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int index = e.RowIndex;
            int index2 = e.RowCount;

            for (index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                DataGridViewRow row = mentor_gv_allrequestlist.Rows[index];

                switch (row.Cells["R_matching"].Value.ToString())
                {
                    case "waiting":
                        row.Cells["R_matching"].Value = "승인대기(관리자)";
                        break;
                    case "cancel":
                        row.Cells["R_matching"].Value = "취소";
                        break;
                    case "cancel(1)":
                        row.Cells["R_matching"].Value = "취소(멘토)";
                        break;
                    case "cancel(2)":
                        row.Cells["R_matching"].Value = "취소(멘티)";
                        break;
                    case "cancel(admin)":
                        row.Cells["R_matching"].Value = "취소(관리자)";
                        break;
                    case "matching":
                        row.Cells["R_matching"].Value = "최종승인(진행중)";
                        break;
                    case "agree(admin)":
                        row.Cells["R_matching"].Value = "승인(관리자)";
                        break;
                    case "fin":
                        row.Cells["R_matching"].Value = "종료";
                        break;
                    default:
                        break;
                }

                row.Cells["성별(남/여)"].Value = row.Cells["R_mentee_male"].Value + "명 / " + row.Cells["R_mentee_female"].Value+"명";
                row.Cells["나이"].Value = row.Cells["R_mentee_lowage"].Value + "세 ~ " + row.Cells["R_mentee_highage"].Value + "세";
            }

          //  MessageBox.Show(index2.ToString());
        }


        // 걍 껐을때..
        private void Mentor_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        // 멘토링 요청 목록을 클릭했을 때
        private void mentor_gv_allrequestlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                string status = mentor_gv_allrequestlist.Rows[e.RowIndex].Cells["R_matching"].Value.ToString();
                string a = mentor_gv_allrequestlist.Rows[e.RowIndex].Cells["R_mentee_groupname"].Value.ToString();
                string b = mentor_gv_allrequestlist.Rows[e.RowIndex].Cells["R_request_time"].Value.ToString();
                Dictionary<string, string> dir = df.drr(a, b);
                mentor_lbl_groupname.Text = dir["R_mentee_groupname"];
                mentor_lbl_age.Text = dir["R_mentee_age"];
                mentor_lbl_sex.Text = dir["R_mentee_sex"];
                mentor_lbl_location.Text = dir["R_mentee_location"];
                mentor_lbl_jobs.Text = dir["R_mentee_job"];
                mentor_lbl_tel.Text = dir["R_mentee_tel"];
                mentor_lbl_part.Text = dir["R_mentee_interestarea3"];
                mentor_lbl_company.Text = dir["R_mentee_company"];
                mentor_lbl_address.Text = dir["R_mentee_address"];
                mentor_lbl_post.Text = dir["R_mentee_post"];
                mentor_lbl_term.Text = dir["R_start_time"] + " ~ " + dir["R_finish_time"];
                mentor_lbl_time.Text = dir["R_clock_time"];
                mentor_lbl_requestdate.Text = dir["R_request_time"];
                mentor_lbl_status.Text = status;


                if (status.Equals("취소") || status.Equals("취소(관리자)") || status.Equals("취소(멘토)") || status.Equals("취소(멘티)") || status.Equals("종료"))
                {
                    mentor_btn_approve.Enabled = false;
                    mentor_btn_delete.Enabled = true;
                    mentor_btn_exit.Enabled = false;
                    mentor_btn_refuse.Enabled = false;
                }

                else if (status.Equals("승인(관리자)"))
                {
                    mentor_btn_approve.Enabled = true;
                    mentor_btn_delete.Enabled = false;
                    mentor_btn_exit.Enabled = false;
                    mentor_btn_refuse.Enabled = true;
                }

                else if (status.Equals("최종승인(진행중)"))
                {
                    mentor_btn_approve.Enabled = false;
                    mentor_btn_delete.Enabled = false;
                    mentor_btn_exit.Enabled = true;
                    mentor_btn_refuse.Enabled = false;
                }

                requested_time = dir["R_request_time"];
                groupname = dir["R_mentee_groupname"];

            }
             
        }


        // 메뉴 - 로그아웃
        private void mentor_menu_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Show();
        }


        // 메뉴 - 종료
        private void mentor_menu_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        // 최종승인 버튼
        private void mentor_btn_approve_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("멘토링을 진행하시겠습니까?", "진행", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mentor_gv_allrequestlist.DataSource = df.Mentor_changeStatus(groupname, requested_time, login_id, 0);
                mentor_gv_allrequestlist.ClearSelection();
                changelabel();
                reset_label();

                mentor_btn_approve.Enabled = false;
                mentor_btn_delete.Enabled = false;
                mentor_btn_exit.Enabled = false;
                mentor_btn_refuse.Enabled = false;

                MessageBox.Show("멘토링을 진행합니다.", "진행", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 거절 버튼
        private void mentor_btn_refuse_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("취소하시겠습니까?", "취소", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mentor_gv_allrequestlist.DataSource = df.Mentor_changeStatus(groupname, requested_time, login_id, 1);
                changelabel();

                reset_label();
                MessageBox.Show("취소하였습니다.", "취소완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // 멘토링 종료버튼을 눌렀을때, 현재는 편의상 프로그램 종료
        private void mentor_btn_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("멘토링을 종료하시겠습니까?", "종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mentor_gv_allrequestlist.DataSource = df.Mentor_changeStatus(groupname, requested_time, login_id, 2);
                mentor_gv_allrequestlist.ClearSelection();
                changelabel();
                reset_label();

                mentor_btn_approve.Enabled = false;
                mentor_btn_delete.Enabled = false;
                mentor_btn_exit.Enabled = false;
                mentor_btn_refuse.Enabled = false;

                MessageBox.Show("멘토링을 완료하였습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // 내역삭제 버튼
        private void mentor_btn_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당 내역을 삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mentor_gv_allrequestlist.DataSource = df.Mentor_changeStatus(groupname, requested_time, login_id, 3);
                mentor_gv_allrequestlist.ClearSelection();
                changelabel();
                reset_label();

                mentor_btn_approve.Enabled = false;
                mentor_btn_delete.Enabled = false;
                mentor_btn_exit.Enabled = false;
                mentor_btn_refuse.Enabled = false;

                MessageBox.Show("해당 내역을 삭제하였습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 메뉴 - 정보수정 클릭
        private void mentor_menu_edit_Click(object sender, EventArgs e)
        {
            if (df.Mentor_isMentoring(login_id))
            {
                MessageBox.Show("진행중 혹은 승인 대기중인 멘토링이 있는 관계로\n개인정보 수정이 불가능합니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Mentor_edit mentor_edit = new Mentor_edit();
                mentor_edit.id = login_id;
                mentor_edit.isAdmin = false;
                mentor_edit.Show();
            }
        }

        // 비밀번호 변경
        private void mentor_menu_changePW_Click(object sender, EventArgs e)
        {
            ChangePW pw = new ChangePW();
            pw.login_id = login_id;
            pw.ShowDialog();
        }
    }
}
