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
    public partial class Mentee_myrequest : Form
    {
        Data_func df = new Data_func();

        public string login_id = "";

        public static string requested_time = "";
        public static string groupname = "";

        public Mentee_myrequest()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Mentee_myrequest_Load(object sender, EventArgs e)
        {
            menteemyrequest_gv_list.DataSource = df.Myrequest(login_id);
            menteemyrequest_gv_list.ClearSelection();
            menteemyrequest_gv_list_Customizing();
            reset_label();
        }

        private void menteemyrequest_gv_list_Customizing()
        {
            menteemyrequest_gv_list.AutoGenerateColumns = false;

            menteemyrequest_gv_list.Columns["R_matching"].HeaderText = "상태";
            menteemyrequest_gv_list.Columns["R_mentee_groupname"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_location"].Visible = false;                 // 지역 부분 미출력
            menteemyrequest_gv_list.Columns["R_mentee_lowage"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_highage"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_male"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_female"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_job"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_company"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_tel"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_address"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_post1"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_post2"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_interestarea1"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_interestarea2"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_interestarea3"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_favorjob"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_name"].HeaderText = "멘토이름";
            menteemyrequest_gv_list.Columns["R_mentor_educational"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_soldierclass"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_major"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_jobs"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_company"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_depart"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_location"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_part1"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_part2"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_part3"].HeaderText = "분야";
            menteemyrequest_gv_list.Columns["R_mentor_tel"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_post1"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_post2"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_address"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_sex"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_age"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_email"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_domain"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentee_id"].Visible = false;
            menteemyrequest_gv_list.Columns["R_mentor_id"].Visible = false;
            menteemyrequest_gv_list.Columns["R_request_time"].HeaderText = "신청일";
            menteemyrequest_gv_list.Columns["R_cancel_time"].Visible = false;
            menteemyrequest_gv_list.Columns["R_start_time"].Visible = false;
            menteemyrequest_gv_list.Columns["R_finish_time"].Visible = false;
            menteemyrequest_gv_list.Columns["R_matching_time"].Visible = false;
            menteemyrequest_gv_list.Columns["R_complete_time"].Visible = false;
            menteemyrequest_gv_list.Columns["R_admin_agree_time"].Visible = false;
            menteemyrequest_gv_list.Columns["R_clock_time"].Visible = false;

            menteemyrequest_gv_list.Columns["R_matching"].DisplayIndex = 0;
            menteemyrequest_gv_list.Columns["R_mentor_name"].DisplayIndex = 1;
            menteemyrequest_gv_list.Columns["R_mentor_part3"].DisplayIndex = 2;
            menteemyrequest_gv_list.Columns["R_request_time"].DisplayIndex = 3;
        }

        private void reset_label()
        {
            menteemyrequest_lbl_name.Text = "";
            menteemyrequest_lbl_age.Text = "";
            menteemyrequest_lbl_sex.Text = "";
            menteemyrequest_lbl_major.Text = "";
            menteemyrequest_lbl_location.Text = "";
            menteemyrequest_lbl_jobs.Text = "";
            menteemyrequest_lbl_tel.Text = "";
            menteemyrequest_lbl_part.Text = "";
            menteemyrequest_lbl_company.Text = "";
            menteemyrequest_lbl_possibletime.Text = "";
            menteemyrequest_lbl_email.Text = "";
            menteemyrequest_lbl_term.Text = "";
            menteemyrequest_lbl_requestdate.Text = "";
            menteemyrequest_lbl_status.Text = "";
        }


        private void menteemyrequest_gv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                string a = menteemyrequest_gv_list.Rows[e.RowIndex].Cells["R_mentee_groupname"].Value.ToString();
                string b = menteemyrequest_gv_list.Rows[e.RowIndex].Cells["R_request_time"].Value.ToString();
                Dictionary<string, string> dir = df.Myrequest_info(a, b);

                // info label
                menteemyrequest_lbl_name.Text = dir["R_mentor_name"];
                menteemyrequest_lbl_age.Text = dir["R_mentor_age"];
                menteemyrequest_lbl_sex.Text = dir["R_mentor_sex"];
                menteemyrequest_lbl_major.Text = dir["R_mentor_major"];
                menteemyrequest_lbl_location.Text = dir["R_mentor_location"];
                menteemyrequest_lbl_jobs.Text = dir["R_mentor_jobs"];
                menteemyrequest_lbl_tel.Text = dir["R_mentor_tel"];
                menteemyrequest_lbl_part.Text = dir["R_mentor_part3"];
                menteemyrequest_lbl_company.Text = dir["R_mentor_company"];
                menteemyrequest_lbl_possibletime.Text = dir["R_clock_time"];
                menteemyrequest_lbl_email.Text = dir["R_mentor_email"];
                menteemyrequest_lbl_term.Text = dir["R_time"];
                menteemyrequest_lbl_requestdate.Text = dir["R_request_time"];

                // status label
                switch (dir["R_matching"])
                {
                    case "waiting":
                        menteemyrequest_lbl_status.Text = "승인대기(관리자)";
                        menteemyrequest_btn_cancel.Enabled = true;
                        menteemyrequest_btn_delete.Enabled = false;
                        menteemyrequest_btn_complete.Enabled = false;
                        break;
                    case "cancel":
                        menteemyrequest_lbl_status.Text = "취소 (" + dir["R_cancel_time"] + ")";
                        menteemyrequest_btn_cancel.Enabled = false;
                        menteemyrequest_btn_delete.Enabled = true;
                        menteemyrequest_btn_complete.Enabled = false;
                        break;
                    case "cancel(1)":
                        menteemyrequest_lbl_status.Text = "취소(멘토) (" + dir["R_cancel_time"] + ")";
                        menteemyrequest_btn_cancel.Enabled = false;
                        menteemyrequest_btn_delete.Enabled = true;
                        menteemyrequest_btn_complete.Enabled = false;
                        break;
                    case "cancel(2)":
                        menteemyrequest_lbl_status.Text = "취소(멘티) (" + dir["R_cancel_time"] + ")";
                        menteemyrequest_btn_cancel.Enabled = false;
                        menteemyrequest_btn_delete.Enabled = true;
                        menteemyrequest_btn_complete.Enabled = false;
                        break;
                    case "cancel(admin)":
                        menteemyrequest_lbl_status.Text = "취소(관리자) (" + dir["R_cancel_time"] + ")";
                        menteemyrequest_btn_cancel.Enabled = false;
                        menteemyrequest_btn_delete.Enabled = true;
                        menteemyrequest_btn_complete.Enabled = false;
                        break;
                    case "cancel(12)":
                        menteemyrequest_lbl_status.Text = "취소 (" + dir["R_cancel_time"] + ")";
                        menteemyrequest_btn_cancel.Enabled = false;
                        menteemyrequest_btn_delete.Enabled = true;
                        menteemyrequest_btn_complete.Enabled = false;
                        break;
                    case "matching":
                        menteemyrequest_lbl_status.Text = "최종승인";
                        menteemyrequest_btn_cancel.Enabled = false;
                        menteemyrequest_btn_delete.Enabled = false;
                        menteemyrequest_btn_complete.Enabled = true;
                        break;
                    case "agree(admin)":
                        menteemyrequest_lbl_status.Text = "승인(관리자)";
                        menteemyrequest_btn_cancel.Enabled = true;
                        menteemyrequest_btn_delete.Enabled = false;
                        menteemyrequest_btn_complete.Enabled = false;
                        break;
                    case "fin":
                        menteemyrequest_lbl_status.Text = "종료";
                        menteemyrequest_btn_cancel.Enabled = false;
                        menteemyrequest_btn_delete.Enabled = false;
                        menteemyrequest_btn_complete.Enabled = false;
                        break;
                    default:
                        break;
                }

                requested_time = dir["R_request_time"];
                groupname = dir["R_mentee_groupname"];
            }
        }

        private void menteemyrequest_gv_list_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                DataGridViewRow row = menteemyrequest_gv_list.Rows[index];

                int part3code_parse_result = 0;                             // 변환 성공여부 - 분야코드
                Int32.TryParse(row.Cells["R_mentor_part3"].Value.ToString(), out part3code_parse_result);                                     // 분야코드 숫자변환 가능여부 판별

                if (part3code_parse_result != 0)                            // 변환 성공시 (초기값이 아닐경우)
                {
                    row.Cells["R_mentor_part3"].Value = df.sortCode_GetName(row.Cells["R_mentor_part3"].Value.ToString());                 // 분야코드값 이름으로..
                }

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
                    case "cancel(12)":
                        row.Cells["R_matching"].Value = "취소";
                        break;
                    case "cancel(admin)":
                        row.Cells["R_matching"].Value = "취소(관리자)";
                        break;
                    case "matching":
                        row.Cells["R_matching"].Value = "최종승인";
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

            }
        }

        private void menteemyrequest_btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menteemyrequest_btn_cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("취소하시겠습니까?", "취소", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menteemyrequest_gv_list.DataSource = df.Myrequest_modifyStatus(groupname, requested_time, 0, login_id);
                menteemyrequest_gv_list.ClearSelection();
                reset_label();
                MessageBox.Show("취소하였습니다.", "취소완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void menteemyrequest_btn_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당 내역을 삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menteemyrequest_gv_list.DataSource = df.Myrequest_modifyStatus(groupname, requested_time, 1, login_id);
                menteemyrequest_gv_list.ClearSelection();
                reset_label();
                MessageBox.Show("해당 내역을 삭제하였습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void menteemyrequest_btn_complete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("멘토링을 완료하시겠습니까?", "완료", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menteemyrequest_gv_list.DataSource = df.Myrequest_modifyStatus(groupname, requested_time, 2, login_id);
                menteemyrequest_gv_list.ClearSelection();
                reset_label();
                MessageBox.Show("멘토링을 완료하였습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
