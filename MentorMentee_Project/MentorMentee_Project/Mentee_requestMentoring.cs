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
    public partial class Mentee_requestMentoring : Form
    {
        Data_func df = new Data_func();

        public string mentor_id = "";
        public string mentee_id = "";

        // 멘토정보 변수 영역
        public static string mentorinfo_name = "";
        public static string mentorinfo_educational = "";
        public static string mentorinfo_soldierclass = "";
        public static string mentorinfo_major = "";
        public static string mentorinfo_jobs = "";
        public static string mentorinfo_company = "";
        public static string mentorinfo_depart = "";
        public static string mentorinfo_location = "";
        public static string mentorinfo_part1 = "";
        public static string mentorinfo_part2 = "";
        public static string mentorinfo_part3 = "";
        public static string mentorinfo_tel = "";
        public static string mentorinfo_post1 = "";
        public static string mentorinfo_post2 = "";
        public static string mentorinfo_address = "";
        public static string mentorinfo_sex = "";
        public static string mentorinfo_age = "";
        public static string mentorinfo_email = "";
        public static string mentorinfo_domain = "";

        // 멘티정보 변수 영역
        public static string menteeinfo_groupname = "";
        public static string menteeinfo_location = "";
        public static string menteeinfo_lowage = "";
        public static string menteeinfo_highage = "";
        public static string menteeinfo_male = "";
        public static string menteeinfo_female = "";
        public static string menteeinfo_job = "";
        public static string menteeinfo_company = "";
        public static string menteeinfo_tel = "";
        public static string menteeinfo_address = "";
        public static string menteeinfo_post1 = "";
        public static string menteeinfo_post2 = "";
        public static string menteeinfo_interestarea1 = "";
        public static string menteeinfo_interestarea2 = "";
        public static string menteeinfo_intereatarea3 = "";
        public static string menteeinfo_favorjob = "";
          
        public Mentee_requestMentoring()
        {
            InitializeComponent();
        }

        private void Mentee_requestMentoring_Load(object sender, EventArgs e)
        {
            menteerequest_cb_startapm.SelectedIndex = 0;
            menteerequest_cb_finishapm.SelectedIndex = 0;
            menteerequest_cb_starttime.SelectedIndex = 0;
            menteerequest_cb_finishtime.SelectedIndex = 0;

            menteerequest_dt_startdate.Format = DateTimePickerFormat.Short;
            menteerequest_dt_startdate.Value = DateTime.Today;
            menteerequest_dt_finishdate.Format = DateTimePickerFormat.Short;
            menteerequest_dt_finishdate.Value = DateTime.Today;

            Dictionary<string, string> dir = df.Request_info(mentor_id);

            menteerequest_lab_mentorname.Text = dir["mentor_name"];
            menteerequest_lab_mentorsex.Text = dir["mentor_sex"];
            menteerequest_lab_mentorage.Text = dir["mentor_age"];
            menteerequest_lab_mentormajor.Text = dir["mentor_major"];
            menteerequest_lab_mentorjobs.Text = dir["mentor_jobs"];
            menteerequest_lab_mentorlocation.Text = dir["mentor_location"];
            menteerequest_lab_mentorcompany.Text = dir["mentor_company"];
            menteerequest_lab_mentorpart.Text = dir["mentor_part3"];
            menteerequest_lab_mentoremail.Text = dir["mentor_email"];
            menteerequest_lab_mentortel.Text = dir["mentor_tel"];
        }

        private void menteerequest_btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menteerequest_btn_ok_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("시작날짜: " + menteerequest_dt_startdate.Value + "\n현재날짜 : " + DateTime.Now.Date);

            if (menteerequest_dt_startdate.Value > menteerequest_dt_finishdate.Value)
            {
                MessageBox.Show("종료일이 시작일보다 이전값입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (menteerequest_dt_startdate.Value < DateTime.Now.Date)
            {
                MessageBox.Show("시작일이 오늘보다 이전값입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                if (MessageBox.Show("신청하시겠습니까?", "신청확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    menteerequest_getMenteeInfo();
                    menteerequest_getMentorInfo();

                    if (df.Request_truncate(mentor_id, mentee_id))
                    {
                        MessageBox.Show("이미 멘토링 신청을 완료하셨습니다.\n자세한 내용은 나의 신청내역에서 확인할 수 있습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);            
                        return;
                    }

                    Dictionary<string, string> dir = new Dictionary<string, string>();
                    dir["R_mentee_groupname"] = menteeinfo_groupname;
                    dir["R_mentee_location"] = menteeinfo_location;
                    dir["R_mentee_lowage"] = menteeinfo_lowage;
                    dir["R_mentee_highage"] = menteeinfo_highage;
                    dir["R_mentee_male"] = menteeinfo_male;
                    dir["R_mentee_female"] = menteeinfo_female;
                    dir["R_mentee_job"] = menteeinfo_job;
                    dir["R_mentee_company"] = menteeinfo_company;
                    dir["R_mentee_tel"] = menteeinfo_tel;
                    dir["R_mentee_address"] = menteeinfo_address;
                    dir["R_mentee_post1"] = menteeinfo_post1;
                    dir["R_mentee_post2"] = menteeinfo_post2;
                    dir["R_mentee_interestarea1"] = menteeinfo_interestarea1;
                    dir["R_mentee_interestarea2"] = menteeinfo_interestarea2;
                    dir["R_mentee_interestarea3"] = menteeinfo_intereatarea3;
                    dir["R_mentee_favorjob"] = menteeinfo_favorjob;
                    dir["R_mentor_name"] = mentorinfo_name;
                    dir["R_mentor_educational"] = mentorinfo_educational;
                    dir["R_mentor_soldierclass"] = mentorinfo_soldierclass;
                    dir["R_mentor_major"] = mentorinfo_major;
                    dir["R_mentor_jobs"] = mentorinfo_jobs;
                    dir["R_mentor_company"] = mentorinfo_company;
                    dir["R_mentor_depart"] = mentorinfo_depart;
                    dir["R_mentor_location"] = mentorinfo_location;
                    dir["R_mentor_part1"] = mentorinfo_part1;
                    dir["R_mentor_part2"] = mentorinfo_part2;
                    dir["R_mentor_part3"] = mentorinfo_part3;
                    dir["R_mentor_tel"] = mentorinfo_tel;
                    dir["R_mentor_post1"] = mentorinfo_post1;
                    dir["R_mentor_post2"] = mentorinfo_post2;
                    dir["R_mentor_address"] = mentorinfo_address;
                    dir["R_mentor_sex"] = mentorinfo_sex;
                    dir["R_mentor_age"] = mentorinfo_age;
                    dir["R_mentor_email"] = mentorinfo_email;
                    dir["R_mentor_domain"] = mentorinfo_domain;
                    dir["R_mentee_id"] = mentee_id;
                    dir["R_mentor_id"] = mentor_id;
                    dir["R_request_time"] = DateTime.Now.ToString();                         // 요청시간
                    dir["R_cancel_time"] = "";                                               // 취소시간
                    dir["R_start_time"] = menteerequest_dt_startdate.Text;                  // 시작일
                    dir["R_finish_time"] = menteerequest_dt_startdate.Text;                 // 종료일
                    dir["R_matching_time"] = "";                                             // 최종승인 - 진행시간
                    dir["R_complete_time"] = "";                                             // 완료시간
                    dir["R_admin_agree_time"] = "";                                          // 관리자 승인 시간
                    dir["R_clock_time"] = menteerequest_cb_startapm.Text + menteerequest_cb_starttime.Text + "시 ~ " + menteerequest_cb_finishapm.Text + menteerequest_cb_finishtime.Text + "시";                           // 가능 시간대 
                    df.Request_complete(dir);
                    MessageBox.Show("멘토링 신청을 완료하였습니다.\n자세한 내용은 나의 신청내역에서 확인할 수 있습니다.", "신청완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
            }
        }


        // 멘토 정보 가져오는 영역
        private void menteerequest_getMentorInfo()
        {
            Dictionary<string, string> dir = df.Request_getMentorInfo(mentor_id);

            mentorinfo_name = dir["mentor_name"];
            mentorinfo_educational = dir["mentor_educational"];
            mentorinfo_soldierclass = dir["mentor_soldierclass"];
            mentorinfo_major = dir["mentor_major"];
            mentorinfo_jobs = dir["mentor_jobs"];
            mentorinfo_company = dir["mentor_company"];
            mentorinfo_depart = dir["mentor_depart"];
            mentorinfo_location = dir["mentor_location"];
            mentorinfo_part1 = dir["mentor_part1"];
            mentorinfo_part2 = dir["mentor_part2"];
            mentorinfo_part3 = dir["mentor_part3"];
            mentorinfo_tel = dir["mentor_tel"];
            mentorinfo_post1 = dir["mentor_post1"];
            mentorinfo_post2 = dir["mentor_post2"];
            mentorinfo_address = dir["mentor_address"];
            mentorinfo_sex = dir["mentor_sex"];
            mentorinfo_age = dir["mentor_age"];
            mentorinfo_email = dir["mentor_email"];
            mentorinfo_domain = dir["mentor_domain"];
        }


        // 멘티 정보 가져오는 영역
        private void menteerequest_getMenteeInfo()
        {
            Dictionary<string, string> dir = df.Request_getMenteeInfo(mentee_id);

            menteeinfo_groupname = dir["mentee_groupname"];
            menteeinfo_location = dir["mentee_location"];
            menteeinfo_lowage = dir["mentee_minage"];
            menteeinfo_highage = dir["mentee_maxage"];
            menteeinfo_male = dir["mentee_male"];
            menteeinfo_female = dir["mentee_female"];
            menteeinfo_job = dir["mentee_job"];
            menteeinfo_company = dir["mentee_company"];
            menteeinfo_tel = dir["mentee_tel"];
            menteeinfo_address = dir["mentee_address"];
            menteeinfo_post1 = dir["mentee_post1"];
            menteeinfo_post2 = dir["mentee_post2"];
            menteeinfo_interestarea1 = dir["mentee_interestarea1"];
            menteeinfo_interestarea2 = dir["mentee_interestarea2"];
            menteeinfo_intereatarea3 = dir["mentee_interestarea3"];
            menteeinfo_favorjob = dir["mentee_favorjob"];

        }

        
    }
}
