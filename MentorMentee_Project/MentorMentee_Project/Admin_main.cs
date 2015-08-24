using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentorMentee_Project
{
    public partial class Admin_main : Form
    {

        Data_func df = new Data_func();
      
        public static string requested_time = "";
        public static string groupname = "";

        int index_generalL;          // (그리드뷰 인덱스)공통코드 - 대
        int index_generalM;          // (그리드뷰 인덱스)공통코드 - 중

        int index_sortL;            // (그리드뷰 인덱스)분류코드 - 대
        int index_sortM;            // (그리드뷰 인덱스)분류코드 - 중
        int index_sortS;            // (그리드뷰 인덱스)분류코드 - 소

        public int index_allrequestlist;        // (그리드뷰 인덱스)전체신청내역
        public int index_mentorlist;            // (그리드뷰 인덱스)멘토 리스트
        public int index_menteelist;            // (그리드뷰 인덱스)멘티 리스트

        public static string generalL_code = "";            // 공통코드 - 대
        public static string generalM_code = "";            // 공통코드 - 중

        public static string sortL_code = "";               // 분류코드 - 대
        public static string sortM_code = "";               // 분류코드 - 중
        public static string sortS_code = "";               // 분류코드 - 소


        // print 관련
        int iTotalWidth;
        int iCellHeight;
        int iHeaderHeight;
        int iRow = 0;

        bool bFirstPage;
        bool bNewPage;

        private int _pageCount;
        private int _totalPage;

        StringFormat strFormat;
        ArrayList arrColumnLefts = new ArrayList();
        ArrayList arrColumnWidths = new ArrayList();


        // Admin_mainform Initialize
        public Admin_main()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }



        private void Admin_main_Load(object sender, EventArgs e)
        {
            // 하단 시계 관련 클래스
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            
            // 전체 신청내역
            admin_gv_allrequestlist.DataSource = df.getRequestHistory();
            admin_gv_allrequestlist_Customizing();
            admin_gv_allrequestlist.ClearSelection();
            label_reset();

            // 멘토 리스트
            admin_gv_mentorlist.DataSource = df.getMentor();
            admin_gv_mentorlist_Customizing();

            // 멘티 리스트
            admin_gv_menteelist.DataSource = df.getMentee();
            admin_gv_menteelist_Customizing();
           
        }

    

        // statusstrip clock
        void timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "관리자님 환영합니다.  현재시각 : " + DateTime.Now.ToString();
        }


        // label clear
        private void label_reset()
        {
            admin_lbl_adminapprove.Text = "";
            admin_lbl_enddate.Text = "";
            admin_lbl_hopeperiod.Text = "";
            admin_lbl_menteeaddress.Text = "";
            admin_lbl_menteeage.Text = "";
            admin_lbl_menteecompany.Text = "";
            admin_lbl_menteefavorjobs.Text = "";
            admin_lbl_menteegroupname.Text = "";
            admin_lbl_menteeinterestpart.Text = "";
            admin_lbl_menteejobs.Text = "";
            admin_lbl_menteelocation.Text = "";
            admin_lbl_menteesex.Text = "";
            admin_lbl_menteetel.Text = "";
            admin_lbl_mentoraddress.Text = "";
            admin_lbl_mentorage.Text = "";
            admin_lbl_mentorapprove.Text = "";
            admin_lbl_mentorcompany.Text = "";
            admin_lbl_mentoremail.Text = "";
            admin_lbl_mentorjobs.Text = "";
            admin_lbl_mentorlocation.Text = "";
            admin_lbl_mentorname.Text = "";
            admin_lbl_mentorpart.Text = "";
            admin_lbl_mentorsex.Text = "";
            admin_lbl_mentortel.Text = "";
            admin_lbl_possibletime.Text = "";
            admin_lbl_requestdate.Text = "";
            admin_lbl_status.Text = "";

        }


        // admin_gv_allrequest customizing (headertext, visible..)
        private void admin_gv_allrequestlist_Customizing()
        {
            admin_gv_allrequestlist.AutoGenerateColumns = false;  // 이 코드를 안써주면 지 마음대로 그리드뷰가 생성되어서 순서가 안먹힘.

            admin_gv_allrequestlist.Columns["R_matching"].HeaderText = "상태";
            admin_gv_allrequestlist.Columns["R_mentee_groupname"].HeaderText = "그룹명";
            admin_gv_allrequestlist.Columns["R_mentee_location"].Visible = false;                 // 지역 부분 미출력
            admin_gv_allrequestlist.Columns["R_mentee_lowage"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_highage"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_male"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_female"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_job"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_company"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_tel"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_address"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_post1"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_post2"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_interestarea1"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_interestarea2"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_interestarea3"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_favorjob"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_name"].HeaderText = "멘토이름";
            admin_gv_allrequestlist.Columns["R_mentor_educational"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_soldierclass"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_major"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_jobs"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_company"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_depart"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_location"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_part1"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_part2"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_part3"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_tel"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_post1"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_post2"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_address"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_sex"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_age"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_email"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_domain"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentee_id"].Visible = false;
            admin_gv_allrequestlist.Columns["R_mentor_id"].Visible = false;
            admin_gv_allrequestlist.Columns["R_request_time"].HeaderText = "신청일";
            admin_gv_allrequestlist.Columns["R_cancel_time"].Visible = false;
            admin_gv_allrequestlist.Columns["R_start_time"].Visible = false;
            admin_gv_allrequestlist.Columns["R_finish_time"].Visible = false;
            admin_gv_allrequestlist.Columns["R_matching_time"].Visible = false;
            admin_gv_allrequestlist.Columns["R_complete_time"].Visible = false;
            admin_gv_allrequestlist.Columns["R_admin_agree_time"].Visible = false;
            admin_gv_allrequestlist.Columns["R_clock_time"].Visible = false;

            admin_gv_allrequestlist.Columns["R_matching"].DisplayIndex = 1;
            admin_gv_allrequestlist.Columns["R_mentee_groupname"].DisplayIndex = 2;
            admin_gv_allrequestlist.Columns["R_mentor_name"].DisplayIndex = 3;
            admin_gv_allrequestlist.Columns["멘토링 희망기간"].DisplayIndex = 4;
            admin_gv_allrequestlist.Columns["R_mentee_company"].DisplayIndex = 5;

            admin_gv_allrequestlist.Columns["R_matching"].Width = 110;
            admin_gv_allrequestlist.Columns["R_mentor_name"].Width = 90;
        }


        // admin_gv_mentorlist customizing (headertext, visible..)
        private void admin_gv_mentorlist_Customizing()
        {
            
            admin_gv_mentorlist.AutoGenerateColumns = false;  // 이 코드를 안써주면 지 마음대로 그리드뷰가 생성되어서 순서가 안먹힘.

            admin_gv_mentorlist.Columns["mentor_name"].HeaderText = "이름";
            admin_gv_mentorlist.Columns["mentor_educational"].HeaderText = "학력";
            admin_gv_mentorlist.Columns["mentor_soldierclass"].Visible = false;
            admin_gv_mentorlist.Columns["mentor_major"].HeaderText = "전공";
            admin_gv_mentorlist.Columns["mentor_jobs"].HeaderText = "직업";
            admin_gv_mentorlist.Columns["mentor_company"].Visible = false;
            admin_gv_mentorlist.Columns["mentor_depart"].Visible = false;
            admin_gv_mentorlist.Columns["mentor_location"].HeaderText = "지역";
            admin_gv_mentorlist.Columns["mentor_part1"].Visible = false;
            admin_gv_mentorlist.Columns["mentor_part2"].Visible = false;
            admin_gv_mentorlist.Columns["mentor_part3"].HeaderText = "분야";
            admin_gv_mentorlist.Columns["mentor_tel"].HeaderText = "연락처";
            admin_gv_mentorlist.Columns["mentor_post1"].Visible = false;
            admin_gv_mentorlist.Columns["mentor_post2"].Visible = false;
            admin_gv_mentorlist.Columns["mentor_address"].Visible = false;
            admin_gv_mentorlist.Columns["mentor_sex"].HeaderText = "성별";
            admin_gv_mentorlist.Columns["mentor_age"].HeaderText = "나이";
            admin_gv_mentorlist.Columns["mentor_email"].Visible = false;
            admin_gv_mentorlist.Columns["mentor_domain"].Visible = false;
            admin_gv_mentorlist.Columns["mentor_loginid"].HeaderText = "아이디";
            admin_gv_mentorlist.Columns["mentor_joindate"].HeaderText = "가입일";
            admin_gv_mentorlist.Columns["mentor_certification_certificate"].HeaderText = "자격인증";
            admin_gv_mentorlist.Columns["mentor_career_certificate"].HeaderText = "경력인증";
            admin_gv_mentorlist.Columns["mentor_educational_certificate"].HeaderText = "학력인증";

            admin_gv_mentorlist.Columns["mentor_loginid"].DisplayIndex = 1;
            admin_gv_mentorlist.Columns["mentor_name"].DisplayIndex = 2;
            admin_gv_mentorlist.Columns["mentor_sex"].DisplayIndex = 3;
            admin_gv_mentorlist.Columns["mentor_age"].DisplayIndex = 4;
            admin_gv_mentorlist.Columns["mentor_jobs"].DisplayIndex = 5;
            admin_gv_mentorlist.Columns["mentor_part3"].DisplayIndex = 6;
            admin_gv_mentorlist.Columns["mentor_location"].DisplayIndex = 7;
            admin_gv_mentorlist.Columns["mentor_major"].DisplayIndex = 8;
            admin_gv_mentorlist.Columns["mentor_educational"].DisplayIndex = 9;
            admin_gv_mentorlist.Columns["소속기관/부서"].DisplayIndex = 10;
            admin_gv_mentorlist.Columns["주소"].DisplayIndex = 11;
            admin_gv_mentorlist.Columns["mentor_tel"].DisplayIndex = 12;
            admin_gv_mentorlist.Columns["이메일"].DisplayIndex = 13;
            admin_gv_mentorlist.Columns["mentor_joindate"].DisplayIndex = 14;
            admin_gv_mentorlist.Columns["mentor_certification_certificate"].DisplayIndex = 15;
            admin_gv_mentorlist.Columns["mentor_career_certificate"].DisplayIndex = 16;
            admin_gv_mentorlist.Columns["mentor_educational_certificate"].DisplayIndex = 17;

            admin_gv_mentorlist.Columns["mentor_loginid"].Width = 80;
            admin_gv_mentorlist.Columns["mentor_name"].Width = 80;
            admin_gv_mentorlist.Columns["mentor_sex"].Width = 50;
            admin_gv_mentorlist.Columns["mentor_age"].Width = 50;
            admin_gv_mentorlist.Columns["mentor_location"].Width = 60;
            admin_gv_mentorlist.Columns["mentor_educational"].Width = 70;
            admin_gv_mentorlist.Columns["소속기관/부서"].Width = 150;
            admin_gv_mentorlist.Columns["주소"].Width = 270;
            admin_gv_mentorlist.Columns["이메일"].Width = 170;
            admin_gv_mentorlist.Columns["mentor_joindate"].Width = 150;
            
        }


        // admin_gv_menteelist customizing (headertext, visible..)
        private void admin_gv_menteelist_Customizing()
        {
            
            admin_gv_menteelist.AutoGenerateColumns = false;

            admin_gv_menteelist.Columns["mentee_groupname"].HeaderText = "그룹명";
            admin_gv_menteelist.Columns["mentee_male"].Visible = false;
            admin_gv_menteelist.Columns["mentee_female"].Visible = false;
            admin_gv_menteelist.Columns["mentee_job"].HeaderText = "직업";
            admin_gv_menteelist.Columns["mentee_minage"].Visible = false;
            admin_gv_menteelist.Columns["mentee_maxage"].Visible = false;
            admin_gv_menteelist.Columns["mentee_location"].HeaderText = "지역";
            admin_gv_menteelist.Columns["mentee_company"].HeaderText = "소속";
            admin_gv_menteelist.Columns["mentee_tel"].HeaderText = "연락처";
            admin_gv_menteelist.Columns["mentee_address"].Visible = false;
            admin_gv_menteelist.Columns["mentee_post1"].Visible = false;
            admin_gv_menteelist.Columns["mentee_post2"].Visible = false;
            admin_gv_menteelist.Columns["mentee_interestarea1"].Visible = false;
            admin_gv_menteelist.Columns["mentee_interestarea2"].Visible = false;
            admin_gv_menteelist.Columns["mentee_interestarea3"].HeaderText = "관심분야";
            admin_gv_menteelist.Columns["mentee_favorjob"].HeaderText = "관심직업";
            admin_gv_menteelist.Columns["mentee_loginid"].HeaderText = "아이디";
            admin_gv_menteelist.Columns["mentee_joindate"].HeaderText = "가입일";

            admin_gv_menteelist.Columns["mentee_loginid"].DisplayIndex = 1;
            admin_gv_menteelist.Columns["mentee_groupname"].DisplayIndex = 2;
            admin_gv_menteelist.Columns["성별비율"].DisplayIndex = 3;
            admin_gv_menteelist.Columns["나이대"].DisplayIndex = 4;
            admin_gv_menteelist.Columns["mentee_job"].DisplayIndex = 5;
            admin_gv_menteelist.Columns["mentee_location"].DisplayIndex = 6;
            admin_gv_menteelist.Columns["mentee_company"].DisplayIndex = 7;
            admin_gv_menteelist.Columns["주소"].DisplayIndex = 8;
            admin_gv_menteelist.Columns["mentee_tel"].DisplayIndex = 9;
            admin_gv_menteelist.Columns["mentee_interestarea3"].DisplayIndex = 10;
            admin_gv_menteelist.Columns["mentee_favorjob"].DisplayIndex = 11;
            admin_gv_menteelist.Columns["mentee_joindate"].DisplayIndex = 12;

            admin_gv_menteelist.Columns["mentee_loginid"].Width = 80;
            admin_gv_menteelist.Columns["mentee_groupname"].Width = 100;
            admin_gv_menteelist.Columns["성별비율"].Width = 130;
            admin_gv_menteelist.Columns["나이대"].Width = 80;
            admin_gv_menteelist.Columns["mentee_job"].Width = 100;
            admin_gv_menteelist.Columns["mentee_location"].Width = 60;
            admin_gv_menteelist.Columns["mentee_company"].Width = 150;
            admin_gv_menteelist.Columns["주소"].Width = 300;
            admin_gv_menteelist.Columns["mentee_tel"].Width = 100;
            admin_gv_menteelist.Columns["mentee_interestarea3"].Width = 100;
            admin_gv_menteelist.Columns["mentee_favorjob"].Width = 100;
            admin_gv_menteelist.Columns["mentee_joindate"].Width = 150;
            
        }

        


        /* 전체 신청내역 부분 */

        // 전체 신청내역 바인딩시...
        private void admin_gv_allrequestlist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {             
            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {     
                DataGridViewRow row = admin_gv_allrequestlist.Rows[index];

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
            
                row.Cells["멘토링 희망기간"].Value = row.Cells["R_start_time"].Value + " ~ " + row.Cells["R_finish_time"].Value;
            
            }
                   
        }


        // 전체 신청내역을 클릭했을 때
        private void admin_gv_allrequestlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                admin_gv_allrequestlist_GetInformation(e.RowIndex);
                /*
                string status = admin_gv_allrequestlist.Rows[e.RowIndex].Cells["R_matching"].Value.ToString();

                string a = admin_gv_allrequestlist.Rows[e.RowIndex].Cells["R_mentee_groupname"].Value.ToString();
                string b = admin_gv_allrequestlist.Rows[e.RowIndex].Cells["R_request_time"].Value.ToString();

                Dictionary<string, string> drr = df.drr(a, b);

                // mentee infolabel
                admin_lbl_menteegroupname.Text = drr["R_mentee_groupname"];
                admin_lbl_menteetel.Text = drr["R_mentee_tel"];
                admin_lbl_menteeage.Text = drr["R_mentee_age"];
                admin_lbl_menteesex.Text = drr["R_mentee_sex"];
                admin_lbl_menteejobs.Text = drr["R_mentee_job"];
                admin_lbl_menteelocation.Text = drr["R_mentee_location"];
                admin_lbl_menteecompany.Text = drr["R_mentee_company"];
                admin_lbl_menteeaddress.Text = drr["R_mentee_address"];
                admin_lbl_menteeinterestpart.Text = drr["R_mentee_interestarea3"];
                admin_lbl_menteefavorjobs.Text = drr["R_mentee_favorjob"];

                // mentor infolabel
                admin_lbl_mentorname.Text = drr["R_mentor_name"];
                admin_lbl_mentortel.Text = drr["R_mentor_tel"];
                admin_lbl_mentorsex.Text = drr["R_mentor_sex"];
                admin_lbl_mentorage.Text = drr["R_mentor_age"];
                admin_lbl_mentorjobs.Text = drr["R_mentor_jobs"];
                admin_lbl_mentorlocation.Text = drr["R_mentor_location"];
                admin_lbl_mentorcompany.Text = drr["R_mentor_company"];
                admin_lbl_mentoraddress.Text = drr["R_mentor_address"];
                admin_lbl_mentorpart.Text = drr["R_mentor_part3"];
                admin_lbl_mentoremail.Text = drr["R_mentor_email"];

                // status label
                switch (drr["R_matching"])
                {
                    case "waiting":
                        admin_lbl_status.Text = "승인대기(관리자)";
                        admin_lbl_enddate.Text = "-";
                        admin_lbl_mentorapprove.Text = "-";
                        admin_lbl_adminapprove.Text = "-";
                        admin_btn_approve.Enabled = true;
                        admin_btn_cancel.Enabled = true;
                        admin_btn_finish.Enabled = false;
                        break;
                    case "cancel":
                        admin_lbl_status.Text = "취소 (" + drr["R_cancel_time"] + ")";
                        admin_lbl_enddate.Text = "-";
                        admin_lbl_mentorapprove.Text = "-";
                        admin_lbl_adminapprove.Text = "-";
                        admin_btn_approve.Enabled = false;
                        admin_btn_cancel.Enabled = false;
                        admin_btn_finish.Enabled = false;
                        break;
                    case "cancel(1)":
                        admin_lbl_status.Text = "취소(멘토) (" + drr["R_cancel_time"] + ")";
                        admin_lbl_enddate.Text = "-";
                        admin_lbl_mentorapprove.Text = "-";
                        admin_lbl_adminapprove.Text = "-";
                        admin_btn_approve.Enabled = false;
                        admin_btn_cancel.Enabled = false;
                        admin_btn_finish.Enabled = false;
                        break;
                    case "cancel(2)":
                        admin_lbl_status.Text = "취소(멘티) (" + drr["R_cancel_time"] + ")";
                        admin_lbl_enddate.Text = "-";
                        admin_lbl_mentorapprove.Text = "-";
                        admin_lbl_adminapprove.Text = "-";
                        admin_btn_approve.Enabled = false;
                        admin_btn_cancel.Enabled = false;
                        admin_btn_finish.Enabled = false;
                        break;
                    case "cancel(admin)":
                        admin_lbl_status.Text = "취소(관리자) (" + drr["R_cancel_time"] + ")";
                        admin_lbl_enddate.Text = "-";
                        admin_lbl_mentorapprove.Text = "-";
                        admin_lbl_adminapprove.Text = "-";
                        admin_btn_approve.Enabled = false;
                        admin_btn_cancel.Enabled = false;
                        admin_btn_finish.Enabled = false;
                        break;
                    case "cancel(12)":
                        admin_lbl_status.Text = "취소 (" + drr["R_cancel_time"] + ")";
                        admin_lbl_enddate.Text = "-";
                        admin_lbl_mentorapprove.Text = "-";
                        admin_lbl_adminapprove.Text = "-";
                        admin_btn_approve.Enabled = false;
                        admin_btn_cancel.Enabled = false;
                        admin_btn_finish.Enabled = false;
                        break;
                    case "matching":
                        admin_lbl_status.Text = "최종승인";
                        admin_lbl_enddate.Text = "-";
                        admin_lbl_mentorapprove.Text = drr["R_matching_time"];
                        admin_lbl_adminapprove.Text = drr["R_admin_agree_time"];
                        admin_btn_approve.Enabled = false;
                        admin_btn_cancel.Enabled = false;
                        admin_btn_finish.Enabled = true;
                        break;
                    case "agree(admin)":
                        admin_lbl_status.Text = "승인(관리자)";
                        admin_lbl_enddate.Text = "-";
                        admin_lbl_mentorapprove.Text = "-";
                        admin_lbl_adminapprove.Text = drr["R_admin_agree_time"];
                        admin_btn_approve.Enabled = false;
                        admin_btn_cancel.Enabled = false;
                        admin_btn_finish.Enabled = true;
                        break;
                    case "fin":
                        admin_lbl_status.Text = "종료";
                        admin_lbl_enddate.Text = drr["R_finish_time"];
                        admin_lbl_mentorapprove.Text = drr["R_matching_time"];
                        admin_lbl_adminapprove.Text = drr["R_admin_agree_time"];
                        admin_btn_approve.Enabled = false;
                        admin_btn_cancel.Enabled = false;
                        admin_btn_finish.Enabled = false;
                        break;
                    default:
                        break;
                }

                admin_lbl_requestdate.Text = drr["R_request_time"];
                admin_lbl_hopeperiod.Text = drr["R_start_time"] + " ~ " + drr["R_finish_time"];
                admin_lbl_possibletime.Text = drr["R_clock_time"];

                requested_time = drr["R_request_time"];
                groupname = drr["R_mentee_groupname"];
                 */
            }

        }


        // 전체 신청내역 키보드 이벤트 (up & down)
        private void admin_gv_allrequestlist_KeyDown(object sender, KeyEventArgs e)
        {
            int row = admin_gv_allrequestlist.RowCount;
            if (row > 0 && admin_gv_allrequestlist.CurrentRow.Selected == true)
            {
                if (e.KeyCode.Equals(Keys.Down))
                {
                    index_allrequestlist = admin_gv_allrequestlist.SelectedCells[0].OwningRow.Index + 1;
                    if (index_allrequestlist >= row)
                    {
                        index_allrequestlist += -1;
                    }
                }

                else if (e.KeyCode.Equals(Keys.Up))
                {
                    index_allrequestlist = admin_gv_allrequestlist.SelectedCells[0].OwningRow.Index - 1;
                    if (index_allrequestlist < 0)
                    {
                        index_allrequestlist = 0;
                    }
                }

                else
                {
                    e.SuppressKeyPress = true;
                    return;
                }

                //MessageBox.Show("현재 인덱스값 : " + index_allrequestlist.ToString() + "\n전체 row값 : " + row.ToString());
                if (index_allrequestlist < row && index_allrequestlist != -1)
                {
                    admin_gv_allrequestlist_GetInformation(index_allrequestlist);
                }
            }
        }


        // 클릭하거나 키보드 이벤트 발생시 전체 신청내역의 정보를 가져와서 출력
        public void admin_gv_allrequestlist_GetInformation(int index)
        {
            index_allrequestlist = index;
            string status = admin_gv_allrequestlist.Rows[index].Cells["R_matching"].Value.ToString();

            string a = admin_gv_allrequestlist.Rows[index].Cells["R_mentee_groupname"].Value.ToString();
            string b = admin_gv_allrequestlist.Rows[index].Cells["R_request_time"].Value.ToString();

            Dictionary<string, string> drr = df.drr(a, b);

            // mentee infolabel
            admin_lbl_menteegroupname.Text = drr["R_mentee_groupname"];
            admin_lbl_menteetel.Text = drr["R_mentee_tel"];
            admin_lbl_menteeage.Text = drr["R_mentee_age"];
            admin_lbl_menteesex.Text = drr["R_mentee_sex"];
            admin_lbl_menteejobs.Text = drr["R_mentee_job"];
            admin_lbl_menteelocation.Text = drr["R_mentee_location"];
            admin_lbl_menteecompany.Text = drr["R_mentee_company"];
            admin_lbl_menteeaddress.Text = drr["R_mentee_address"];
            admin_lbl_menteeinterestpart.Text = drr["R_mentee_interestarea3"];
            admin_lbl_menteefavorjobs.Text = drr["R_mentee_favorjob"];

            // mentor infolabel
            admin_lbl_mentorname.Text = drr["R_mentor_name"];
            admin_lbl_mentortel.Text = drr["R_mentor_tel"];
            admin_lbl_mentorsex.Text = drr["R_mentor_sex"];
            admin_lbl_mentorage.Text = drr["R_mentor_age"];
            admin_lbl_mentorjobs.Text = drr["R_mentor_jobs"];
            admin_lbl_mentorlocation.Text = drr["R_mentor_location"];
            admin_lbl_mentorcompany.Text = drr["R_mentor_company"];
            admin_lbl_mentoraddress.Text = drr["R_mentor_address"];
            admin_lbl_mentorpart.Text = drr["R_mentor_part3"];
            admin_lbl_mentoremail.Text = drr["R_mentor_email"];

            // status label
            switch (drr["R_matching"])
            {
                case "waiting":
                    admin_lbl_status.Text = "승인대기(관리자)";
                    admin_lbl_enddate.Text = "-";
                    admin_lbl_mentorapprove.Text = "-";
                    admin_lbl_adminapprove.Text = "-";
                    admin_btn_approve.Enabled = true;
                    admin_btn_cancel.Enabled = true;
                    admin_btn_finish.Enabled = false;
                    break;
                case "cancel":
                    admin_lbl_status.Text = "취소 (" + drr["R_cancel_time"] + ")";
                    admin_lbl_enddate.Text = "-";
                    admin_lbl_mentorapprove.Text = "-";
                    admin_lbl_adminapprove.Text = "-";
                    admin_btn_approve.Enabled = false;
                    admin_btn_cancel.Enabled = false;
                    admin_btn_finish.Enabled = false;
                    break;
                case "cancel(1)":
                    admin_lbl_status.Text = "취소(멘토) (" + drr["R_cancel_time"] + ")";
                    admin_lbl_enddate.Text = "-";
                    admin_lbl_mentorapprove.Text = "-";
                    admin_lbl_adminapprove.Text = "-";
                    admin_btn_approve.Enabled = false;
                    admin_btn_cancel.Enabled = false;
                    admin_btn_finish.Enabled = false;
                    break;
                case "cancel(2)":
                    admin_lbl_status.Text = "취소(멘티) (" + drr["R_cancel_time"] + ")";
                    admin_lbl_enddate.Text = "-";
                    admin_lbl_mentorapprove.Text = "-";
                    admin_lbl_adminapprove.Text = "-";
                    admin_btn_approve.Enabled = false;
                    admin_btn_cancel.Enabled = false;
                    admin_btn_finish.Enabled = false;
                    break;
                case "cancel(admin)":
                    admin_lbl_status.Text = "취소(관리자) (" + drr["R_cancel_time"] + ")";
                    admin_lbl_enddate.Text = "-";
                    admin_lbl_mentorapprove.Text = "-";
                    admin_lbl_adminapprove.Text = "-";
                    admin_btn_approve.Enabled = false;
                    admin_btn_cancel.Enabled = false;
                    admin_btn_finish.Enabled = false;
                    break;
                case "cancel(12)":
                    admin_lbl_status.Text = "취소 (" + drr["R_cancel_time"] + ")";
                    admin_lbl_enddate.Text = "-";
                    admin_lbl_mentorapprove.Text = "-";
                    admin_lbl_adminapprove.Text = "-";
                    admin_btn_approve.Enabled = false;
                    admin_btn_cancel.Enabled = false;
                    admin_btn_finish.Enabled = false;
                    break;
                case "matching":
                    admin_lbl_status.Text = "최종승인";
                    admin_lbl_enddate.Text = "-";
                    admin_lbl_mentorapprove.Text = drr["R_matching_time"];
                    admin_lbl_adminapprove.Text = drr["R_admin_agree_time"];
                    admin_btn_approve.Enabled = false;
                    admin_btn_cancel.Enabled = false;
                    admin_btn_finish.Enabled = true;
                    break;
                case "agree(admin)":
                    admin_lbl_status.Text = "승인(관리자)";
                    admin_lbl_enddate.Text = "-";
                    admin_lbl_mentorapprove.Text = "-";
                    admin_lbl_adminapprove.Text = drr["R_admin_agree_time"];
                    admin_btn_approve.Enabled = false;
                    admin_btn_cancel.Enabled = false;
                    admin_btn_finish.Enabled = true;
                    break;
                case "fin":
                    admin_lbl_status.Text = "종료";
                    admin_lbl_enddate.Text = drr["R_finish_time"];
                    admin_lbl_mentorapprove.Text = drr["R_matching_time"];
                    admin_lbl_adminapprove.Text = drr["R_admin_agree_time"];
                    admin_btn_approve.Enabled = false;
                    admin_btn_cancel.Enabled = false;
                    admin_btn_finish.Enabled = false;
                    break;
                default:
                    break;
            }

            admin_lbl_requestdate.Text = drr["R_request_time"];
            admin_lbl_hopeperiod.Text = drr["R_start_time"] + " ~ " + drr["R_finish_time"];
            admin_lbl_possibletime.Text = drr["R_clock_time"];

            requested_time = drr["R_request_time"];
            groupname = drr["R_mentee_groupname"];
        }


        // 전체 신청내역 탭에서 아무곳이나 클릭했을때
        private void admin_tab_allrequestlist_Click(object sender, EventArgs e)
        {
            label_reset();
            admin_gv_allrequestlist.ClearSelection();
            admin_btn_approve.Enabled = false;
            admin_btn_cancel.Enabled = false;
            admin_btn_finish.Enabled = false;
        }


        // 승인 버튼을 눌렀을 때
        private void admin_btn_approve_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("승인하시겠습니까?", "1차승인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    admin_gv_allrequestlist.DataSource = df.modifystatus(requested_time, groupname, 0);
                    //label_reset();
                    admin_gv_allrequestlist.Rows[index_allrequestlist].Selected = true;
                    admin_gv_allrequestlist_GetInformation(index_allrequestlist);
                    MessageBox.Show("승인을 완료하였습니다.", "1차승인 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }

        }


        // 취소(반려) 버튼을 눌렀을 때
        private void admin_btn_cancel_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("취소(반려)하시겠습니까?", "취소(반려)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    admin_gv_allrequestlist.DataSource = df.modifystatus(requested_time, groupname, 1);
                    //label_reset();
                    admin_gv_allrequestlist.Rows[index_allrequestlist].Selected = true;
                    admin_gv_allrequestlist_GetInformation(index_allrequestlist);
                    MessageBox.Show("취소를 완료하였습니다.", "취소완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }

        }


        // 종료 버튼을 눌렀을 때
        private void admin_btn_finish_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("해당 멘토링을 종료하시겠습니까?", "종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    admin_gv_allrequestlist.DataSource = df.modifystatus(requested_time, groupname, 2);
                    //label_reset();
                    admin_gv_allrequestlist.Rows[index_allrequestlist].Selected = true;
                    admin_gv_allrequestlist_GetInformation(index_allrequestlist);
                    MessageBox.Show("종료하였습니다.", "종료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

            }

        }



    
        /* mentor list */
        // 멘토 리스트 바인딩시
        private void admin_gv_mentorlist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int jobscode_parse_result = 0;                              // 변환 성공여부 - 직업코드
            int locationcode_parse_result = 0;                          // 변환 성공여부 - 지역코드
            int part3code_parse_result = 0;                             // 변환 성공여부 - 분야코드
            int educationalcode_parse_result = 0;                       // 변환 성공여부 - 학력코드

            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                DataGridViewRow row = admin_gv_mentorlist.Rows[index];

                Int32.TryParse(row.Cells["mentor_jobs"].Value.ToString(), out jobscode_parse_result);                                       // 직업코드 숫자변환 가능여부 판별
                Int32.TryParse(row.Cells["mentor_location"].Value.ToString(), out locationcode_parse_result);                               // 지역코드 숫자변환 가능여부 판별
                Int32.TryParse(row.Cells["mentor_part3"].Value.ToString(), out part3code_parse_result);
                Int32.TryParse(row.Cells["mentor_educational"].Value.ToString(), out educationalcode_parse_result);


                if (jobscode_parse_result != 0 && locationcode_parse_result != 0 && part3code_parse_result != 0 && educationalcode_parse_result != 0)                            // 변환 성공시 (초기값이 아닐경우)
                {
                    row.Cells["mentor_location"].Value = df.generalCode_GetName(row.Cells["mentor_location"].Value.ToString());                     // 지역코드값 이름으로..
                    row.Cells["mentor_jobs"].Value = df.generalCode_GetName(row.Cells["mentor_jobs"].Value.ToString());                             // 직업코드값 이름으로..
                    row.Cells["mentor_part3"].Value = df.sortCode_GetName(row.Cells["mentor_part3"].Value.ToString());                              // 분야코드값 이름으로..
                    row.Cells["mentor_educational"].Value = df.generalCode_GetName(row.Cells["mentor_educational"].Value.ToString());               // 학력코드값 이름으로..
                }

                row.Cells["주소"].Value = row.Cells["mentor_post1"].Value + "-" + row.Cells["mentor_post2"].Value + " " + row.Cells["mentor_address"].Value;
                row.Cells["소속기관/부서"].Value = row.Cells["mentor_company"].Value + " / " + row.Cells["mentor_depart"].Value;
                row.Cells["이메일"].Value = row.Cells["mentor_email"].Value + "@" + row.Cells["mentor_domain"].Value;
            }
        }

        // 멘토 리스트 클릭
        private void admin_gv_mentorlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                index_mentorlist = admin_gv_mentorlist.SelectedCells[0].OwningRow.Index;
            }
        }

        // 멘토 리스트 더블클릭
        private void admin_gv_mentorlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                index_mentorlist = Convert.ToInt32(admin_gv_mentorlist.SelectedRows[0].Index);
                admin_gv_mentorlist_OpenMentorEdit(index_mentorlist);
            }
        }

        // 멘토 리스트 키다운 이벤트
        private void admin_gv_mentorlist_KeyDown(object sender, KeyEventArgs e)
        {
            int row = admin_gv_mentorlist.RowCount;
            if (row > 0 && admin_gv_mentorlist.CurrentRow.Selected == true)
            {
                if (e.KeyCode.Equals(Keys.Down))
                {
                    index_mentorlist = admin_gv_mentorlist.SelectedCells[0].OwningRow.Index + 1;
                    if (index_mentorlist >= row)
                    {
                        index_mentorlist += -1;
                    }
                    
                }

                else if (e.KeyCode.Equals(Keys.Up))
                {
                    index_mentorlist = admin_gv_mentorlist.SelectedCells[0].OwningRow.Index - 1;
                    if (index_mentorlist < 0)
                    {
                        index_mentorlist = 0;
                    }
                }

                else if(e.KeyCode.Equals(Keys.Enter))
                {
                    //MessageBox.Show("현재 인덱스값 : " + index_mentorlist.ToString() + "\n전체 row값 : " + row.ToString());
                    e.SuppressKeyPress = true;
                    if (index_mentorlist < row && index_mentorlist != -1)
                    {
                        admin_gv_mentorlist_OpenMentorEdit(index_mentorlist);
                    }
                }
                
            }
        }

        // 멘토 정보수정 창 호출
        public void admin_gv_mentorlist_OpenMentorEdit(int index)
        {
            Mentor_edit mentor_edit = new Mentor_edit(this, index);
            mentor_edit.isAdmin = true;
            mentor_edit.id = admin_gv_mentorlist.Rows[index].Cells["mentor_loginid"].Value.ToString().Trim();
            mentor_edit.Show();
        }

        // 멘토 리스트 탭 진입시 초기작업
        private void admin_tab_mentorlist_Layout(object sender, LayoutEventArgs e)
        {
            admin_gv_mentorlist.ClearSelection();
            index_mentorlist = 0;
        }



        /* mentee list */
        // 멘티 리스트 바인딩시
        private void admin_gv_menteelist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int jobscode_parse_result = 0;                              // 변환 성공여부 - 직업코드
            int locationcode_parse_result = 0;                          // 변환 성공여부 - 지역코드
            int interestedpartcode_parse_result = 0;                    // 변환 성공여부 - 관심분야코드
            int interestedjobscode_parse_result = 0;                    // 변환 성공여부 - 관심지역코드

            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                DataGridViewRow row = admin_gv_menteelist.Rows[index];
                Int32.TryParse(row.Cells["mentee_job"].Value.ToString(), out jobscode_parse_result);                                       // 직업코드 숫자변환 가능여부 판별
                Int32.TryParse(row.Cells["mentee_location"].Value.ToString(), out locationcode_parse_result);                               // 지역코드 숫자변환 가능여부 판별
                Int32.TryParse(row.Cells["mentee_interestarea3"].Value.ToString(), out interestedpartcode_parse_result);
                Int32.TryParse(row.Cells["mentee_favorjob"].Value.ToString(), out interestedjobscode_parse_result);

                if (jobscode_parse_result != 0 && locationcode_parse_result != 0 && interestedjobscode_parse_result != 0 && interestedpartcode_parse_result != 0)                            // 변환 성공시 (초기값이 아닐경우)
                {
                    row.Cells["mentee_location"].Value = df.generalCode_GetName(row.Cells["mentee_location"].Value.ToString());        // 지역코드값 이름으로..
                    row.Cells["mentee_job"].Value = df.generalCode_GetName(row.Cells["mentee_job"].Value.ToString());                // 직업코드값 이름으로..
                    row.Cells["mentee_interestarea3"].Value = df.sortCode_GetName(row.Cells["mentee_interestarea3"].Value.ToString());                 // 분야코드값 이름으로..
                    row.Cells["mentee_favorjob"].Value = df.generalCode_GetName(row.Cells["mentee_favorjob"].Value.ToString());                 // 학력코드값 이름으로..
                }

                row.Cells["성별비율"].Value = "남 : " + row.Cells["mentee_male"].Value + "명 / 여 : " + row.Cells["mentee_female"].Value + "명";
                row.Cells["나이대"].Value = row.Cells["mentee_minage"].Value + "세 ~ " + row.Cells["mentee_maxage"].Value + "세";
                row.Cells["주소"].Value = row.Cells["mentee_post1"].Value + "-" + row.Cells["mentee_post2"].Value + " " + row.Cells["mentee_address"].Value;

            }

        }

        // 멘티 리스트 더블클릭
        private void admin_gv_menteelist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                index_menteelist = Convert.ToInt32(admin_gv_menteelist.SelectedRows[0].Index);
                admin_gv_menteelist_OpenMenteeEdit(index_menteelist);
            }
        }

        // 멘티 리스트 탭 진입시 초기작업
        private void admin_tab_menteelist_Layout(object sender, LayoutEventArgs e)
        {
            admin_gv_menteelist.ClearSelection();
            index_menteelist = 0;
        }

        // 멘티 리스트 클릭
        private void admin_gv_menteelist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                index_menteelist = admin_gv_menteelist.SelectedCells[0].OwningRow.Index;
            }
        }

        // 멘티 리스트 키다운 이벤트
        private void admin_gv_menteelist_KeyDown(object sender, KeyEventArgs e)
        {

            int row = admin_gv_menteelist.RowCount;
            if (row > 0 && admin_gv_menteelist.CurrentRow.Selected == true)
            {
                if (e.KeyCode.Equals(Keys.Down))
                {
                    index_menteelist = admin_gv_menteelist.SelectedCells[0].OwningRow.Index + 1;
                    if (index_menteelist >= row)
                    {
                        index_menteelist += -1;
                    }
                }

                else if (e.KeyCode.Equals(Keys.Up))
                {
                    index_menteelist = admin_gv_menteelist.SelectedCells[0].OwningRow.Index - 1;
                    if (index_menteelist < 0)
                    {
                        index_menteelist = 0;
                    }
                }

                else if (e.KeyCode.Equals(Keys.Enter))
                {
                    // MessageBox.Show("현재 인덱스값 : " + index_menteelist.ToString() + "\n전체 row값 : " + row.ToString());
                    e.SuppressKeyPress = true;
                    if (index_menteelist < row && index_menteelist != -1)
                    {
                        admin_gv_menteelist_OpenMenteeEdit(index_menteelist);
                    }

                }

            }

        }

        // 멘티 정보수정 창 호출
        public void admin_gv_menteelist_OpenMenteeEdit(int index)
        {
            Mentee_edit mentee_edit = new Mentee_edit(this, index_menteelist);
            mentee_edit.isAdmin = true;
            mentee_edit.id = admin_gv_menteelist.Rows[index].Cells["mentee_loginid"].Value.ToString().Trim();
            mentee_edit.Show();
        }


        

       
    

        /* 인쇄관련 기능 */
        // 멘토인쇄 버튼 클릭
        private void btnMentorPrint_Click(object sender, EventArgs e)
        {
            /*
            PrintDialog pd = new PrintDialog();
            pd.Document = printDocument1;
            pd.Document.DefaultPageSettings.Landscape = true;
            pd.UseEXDialog = true;

            if (DialogResult.OK == pd.ShowDialog())
            {
              //  printDocument1.DefaultPageSettings.Landscape = true; // 가로모드
                printDocument1.Print();
               
            }
            */

            // PrintPreviewDialog pv = new PrintPreviewDialog();
            //pv.ClientSize 
            //pv.Document = printDocument1;
            //pv.ShowDialog();
            
            //printDocument1.DefaultPageSettings.Landscape = true; // 가로모드
          //  PrintPreviewDialog pdia = new PrintPreviewDialog();
           // pdia.Document = printDocument1;
           // pdia.Document.DefaultPageSettings.Landscape = true;
            //pdia.ShowDialog();
            //printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
             printPreviewDialog1.Document = printDocument_Mentor;
             printPreviewDialog1.Document.DefaultPageSettings.Landscape = true;
             printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
             printPreviewDialog1.ClientSize = new System.Drawing.Size(900, 700);
             printPreviewDialog1.ShowDialog();
             PrintDialog pd = new PrintDialog();
             pd.Document = printDocument_Mentor;
         
             //if (DialogResult.OK == pd.ShowDialog())
             //{
                 //  printDocument1.DefaultPageSettings.Landscape = true; // 가로모드
               //  printDocument1.Print();

             //}

        }

        // 멘티인쇄 버튼 클릭
        private void btnMenteePrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument_Mentee;
            printPreviewDialog1.Document.DefaultPageSettings.Landscape = true;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printPreviewDialog1.ClientSize = new System.Drawing.Size(900, 700);
            printPreviewDialog1.ShowDialog();
            PrintDialog pd = new PrintDialog();
            pd.Document = printDocument_Mentee;
  
            
        }

        // 멘토인쇄 초기화
        private void printDocument_Mentor_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                _pageCount = 0;
                _totalPage = 0;

                bFirstPage = true;
                bNewPage = true;

                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvCol in admin_gv_mentorlist.Columns)
                {
                    if (dgvCol.Visible != false)
                    {
                        iTotalWidth += dgvCol.Width;
                    }
                
                }

            }

            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        // 멘티인쇄 초기화
        private void printDocument_Mentee_BeginPrint(object sender, PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                _pageCount = 0;

                bFirstPage = true;
                bNewPage = true;

                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvCol in admin_gv_menteelist.Columns)
                {
                    if (dgvCol.Visible != false)
                    {
                        iTotalWidth += dgvCol.Width;
                    }

                }

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        // 멘토인쇄
        private void printDocument_Mentor_PrintPage(object sender, PrintPageEventArgs e)
        {
          //  printDocument1.DefaultPageSettings.Landscape = true;
            /*
            Bitmap bm = new Bitmap(this.admin_gv_mentorlist.Width, this.admin_gv_mentorlist.Height);
            admin_gv_mentorlist.DrawToBitmap(bm, new Rectangle(0, 0, this.admin_gv_mentorlist.Width, this.admin_gv_mentorlist.Height));
            e.Graphics.DrawImage(bm, 0, 0);
             */
            //MessageBox.Show(bFirstPage.ToString() + "," + bNewPage.ToString());

            try
            {
                double iLeftMargin = e.MarginBounds.Left - 70;
                int iTopMargin = e.MarginBounds.Top;

                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;


                var list = from DataGridViewColumn GridCol in admin_gv_mentorlist.Columns orderby GridCol.DisplayIndex select GridCol;      // column DisplayIndex에 따라서 foreach 정렬

                if (bFirstPage)
                {
                  //  MessageBox.Show(bFirstPage.ToString());
                    //foreach (DataGridViewColumn gridCol in admin_gv_mentorlist.Columns)
                    foreach (DataGridViewColumn gridCol in list)
                    {
                        if (gridCol.Visible != false)
                        {
                            iTmpWidth = (int)(Math.Floor((double)((double)gridCol.Width / (double)iTotalWidth * (double)iTotalWidth * ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                            // 그리드뷰 헤더부분 높이지정
                            iHeaderHeight = (int)(e.Graphics.MeasureString(gridCol.HeaderText, new Font("맑은 고딕", 9), iTmpWidth).Height) + 3;

                            // 좌측 margin
                            arrColumnLefts.Add(iLeftMargin);

                            // 컬럼 가로길이
                           // arrColumnWidths.Add(iTmpWidth);
                            //iLeftMargin += iTmpWidth;
                            arrColumnWidths.Add(gridCol.Width * 0.6);
                            iLeftMargin += gridCol.Width * 0.6;
                        }
                    }
                }

                while (iRow <= admin_gv_mentorlist.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = admin_gv_mentorlist.Rows[iRow];

                    // 각 셀 높이 지정
                    iCellHeight = Convert.ToInt32(GridRow.Height) + 2;
                    
                    // 각 항목 margin값 배열 원소
                    int iCount = 0;

                    // 다음 페이지로 넘어가지냐 안넘어가지냐 여부
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }

                    else
                    {
                        if (bNewPage)
                        {      
                            // 헤더 그리기
                            e.Graphics.DrawString("전체 멘토 리스트", new Font("맑은 고딕", 15, FontStyle.Bold), Brushes.Black, 30, 60);
                            String strDate = "출력일 : " + DateTime.Now.ToLongDateString();

                            // 날짜 그리기
                            e.Graphics.DrawString(strDate, new Font("맑은 고딕", 10), Brushes.Black,
                                e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(strDate, new Font("맑은 고딕", 15), e.MarginBounds.Width).Width + 180),
                                e.MarginBounds.Top - e.Graphics.MeasureString("전체 멘토 리스트", new Font("맑은 고딕", 15), e.MarginBounds.Width).Height);
               
                            iTopMargin = e.MarginBounds.Top;

                            // 컬럼헤더 그리기 
                            foreach (DataGridViewColumn GridCol in list)
                            {
                                if (GridCol.Visible != false)
                                {

                                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(Convert.ToInt32(arrColumnLefts[iCount]), iTopMargin, Convert.ToInt32(arrColumnWidths[iCount]), iHeaderHeight));

                                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(Convert.ToInt32(arrColumnLefts[iCount]), iTopMargin, Convert.ToInt32(arrColumnWidths[iCount]), iHeaderHeight));

                                    e.Graphics.DrawString(GridCol.HeaderText, new Font("맑은 고딕", 8), new SolidBrush(GridCol.InheritedStyle.ForeColor), new RectangleF(Convert.ToInt32(arrColumnLefts[iCount]), iTopMargin,
                                        Convert.ToInt32(arrColumnWidths[iCount]), iHeaderHeight), strFormat);
                                    iCount++;
                                }
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }

                        iCount = 0;

                        // 내용물 그리기  
                        var list2 = from DataGridViewCell Cel in GridRow.Cells orderby Cel.OwningColumn.DisplayIndex select Cel;            // 해당되는 컬럼의 DisplayIndex에 따라서 foreach 정렬
                        foreach (DataGridViewCell Cel in list2)
                        {
                            if (Cel.Value != null && Cel.Visible != false)
                            {
                              
                                e.Graphics.DrawString(Cel.Value.ToString(),
                                new Font("맑은 고딕", 5),
                                new SolidBrush(Cel.InheritedStyle.ForeColor),
                                new RectangleF(Convert.ToInt32(arrColumnLefts[iCount])+2,
                                (float)iTopMargin,
                                Convert.ToInt32(arrColumnWidths[iCount]), (float)iCellHeight),
                                strFormat);

                                // 셀 테두리 그리기 
                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle(Convert.ToInt32(arrColumnLefts[iCount]), iTopMargin,
                                    Convert.ToInt32(arrColumnWidths[iCount]), iCellHeight));
                                iCount++;
                            }

                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                // 데이터가 더 있을 경우 다음페이지로 이동
                if (bMorePagesToPrint)
                {
                    e.HasMorePages = true;
                    _pageCount++;
                }
                else
                {
                    ++_pageCount;
                    _totalPage = _pageCount;
                    e.HasMorePages = false;
                }

                // 현재 페이지
                e.Graphics.DrawString(_pageCount.ToString() +" 페이지", new Font("맑은 고딕", 10), Brushes.Black, e.MarginBounds.Right, e.MarginBounds.Bottom);
   
            }

            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        // 멘티인쇄
        private void printDocument_Mentee_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                double iLeftMargin = e.MarginBounds.Left - 70;
                int iTopMargin = e.MarginBounds.Top;

                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                var list = from DataGridViewColumn GridCol in admin_gv_menteelist.Columns orderby GridCol.DisplayIndex select GridCol;      // DisplayIndex에 따라서 foreach 정렬

                if (bFirstPage)
                {
                    foreach (DataGridViewColumn gridCol in list)
                    {
                        if (gridCol.Visible != false)
                        {
                            iTmpWidth = (int)(Math.Floor((double)((double)gridCol.Width / (double)iTotalWidth  * (double)iTotalWidth * ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                            // 그리드뷰 헤더부분 높이지정
                            iHeaderHeight = (int)(e.Graphics.MeasureString(gridCol.HeaderText, new Font("맑은 고딕", 9), iTmpWidth).Height) + 2;

                            // 좌측 margin
                            arrColumnLefts.Add(iLeftMargin);

                            // 컬럼 가로길이
                            // arrColumnWidths.Add(iTmpWidth);
                            //iLeftMargin += iTmpWidth;
                            arrColumnWidths.Add(gridCol.Width * 0.7);
                            iLeftMargin += gridCol.Width * 0.7;
                        }
                    }
                }

                while (iRow <= admin_gv_menteelist.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = admin_gv_menteelist.Rows[iRow];

                    // 각 셀 높이 지정
                    iCellHeight = Convert.ToInt32(GridRow.Height);

                    // ???
                    int iCount = 0;

                    // 다음 페이지로 넘어가지냐 안넘어가지냐 여부
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }

                    else
                    {
                        if (bNewPage)
                        {

                            // 헤더 그리기
                            e.Graphics.DrawString("전체 멘티 리스트", new Font("맑은 고딕", 15, FontStyle.Bold), Brushes.Black, 30, 60);
                            String strDate = "출력일 : " + DateTime.Now.ToLongDateString();

                            // 날짜 그리기
                            e.Graphics.DrawString(strDate, new Font("맑은 고딕", 10), Brushes.Black,
                                e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(strDate, new Font("맑은 고딕", 15), e.MarginBounds.Width).Width + 100),
                                e.MarginBounds.Top - e.Graphics.MeasureString("전체 멘티 리스트", new Font("맑은 고딕", 15), e.MarginBounds.Width).Height);

                            iTopMargin = e.MarginBounds.Top;

                            // 컬럼헤더 그리기 
                            //var list = from DataGridViewColumn GridCol in admin_gv_mentorlist.Columns orderby GridCol.DisplayIndex select GridCol;      // DisplayIndex에 따라서 foreach 정렬
                            foreach (DataGridViewColumn GridCol in list)
                            {
                                if (GridCol.Visible != false)
                                {

                                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(Convert.ToInt32(arrColumnLefts[iCount]), iTopMargin, Convert.ToInt32(arrColumnWidths[iCount]), iHeaderHeight));

                                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(Convert.ToInt32(arrColumnLefts[iCount]), iTopMargin, Convert.ToInt32(arrColumnWidths[iCount]), iHeaderHeight));

                                    e.Graphics.DrawString(GridCol.HeaderText, new Font("맑은 고딕", 8), new SolidBrush(GridCol.InheritedStyle.ForeColor), new RectangleF(Convert.ToInt32(arrColumnLefts[iCount]), iTopMargin,
                                        Convert.ToInt32(arrColumnWidths[iCount]), iHeaderHeight), strFormat);
                                    iCount++;
                                }
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }

                        iCount = 0;

                        // 내용물 그리기  
                        var list2 = from DataGridViewCell Cel in GridRow.Cells orderby Cel.OwningColumn.DisplayIndex select Cel;            // 해당되는 컬럼의 DisplayIndex에 따라서 foreach 정렬
                        foreach (DataGridViewCell Cel in list2)
                        {
                            if (Cel.Value != null && Cel.Visible != false)
                            {

                                e.Graphics.DrawString(Cel.Value.ToString(),
                                new Font("맑은 고딕", 5),
                                new SolidBrush(Cel.InheritedStyle.ForeColor),
                                new RectangleF(Convert.ToInt32(arrColumnLefts[iCount])+2,
                                (float)iTopMargin,
                                Convert.ToInt32(arrColumnWidths[iCount]), (float)iCellHeight),
                                strFormat);

                                // 셀 테두리 그리기 
                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle(Convert.ToInt32(arrColumnLefts[iCount]), iTopMargin,
                                    Convert.ToInt32(arrColumnWidths[iCount]), iCellHeight));
                                iCount++;
                            }

                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                // 데이터가 더 있을 경우 다음페이지로 이동
                if (bMorePagesToPrint)
                {
                    e.HasMorePages = true;
                    _pageCount++;
                }
                else
                {
                    ++_pageCount;
                    e.HasMorePages = false;
                }

                // 현재 페이지
                e.Graphics.DrawString(_pageCount.ToString() + " 페이지", new Font("맑은 고딕", 10), Brushes.Black, e.MarginBounds.Right, e.MarginBounds.Bottom);

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        // 구현예정
        private void admin_menu_printsetup_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.AllowCurrentPage = true;
            pd.AllowSelection = true;
            pd.AllowSomePages = true;
            pd.ShowDialog();
        }



        /* 공통코드 등록 기능부분 */
        // 공통코드 - 탭에서 아무곳이나 클릭시 선택 해제
        private void admin_tab_generalCode_Click(object sender, EventArgs e)
        {
            admin_gv_generalL.ClearSelection();
            admin_gv_generalM.ClearSelection();
           // admin_txt_generalL_Code.Text = "";
           // admin_txt_generalL_Name.Text = "";
           // admin_txt_generalM_Code.Text = "";
           // admin_txt_generalM_Name.Text = "";
            //admin_txt_generalM_Parent.Text = "";
        }


        // 공통코드 - 초기
        private void admin_tab_generalCode_Layout(object sender, LayoutEventArgs e)
        {
            admin_gv_generalL.DataSource = df.getGeneralCode_L();
            admin_gv_generalL_Customizing();
            admin_gv_generalL.ClearSelection();
            admin_gv_generalM.DataSource = null;        // 중분류 데이터셋 초기화
            admin_txt_generalL_Code.Text = "";
            admin_txt_generalL_Name.Text = "";
            admin_txt_generalM_Code.Text = "";
            admin_txt_generalM_Name.Text = "";
            admin_txt_generalM_Parent.Text = "";
            admin_btn_generalM_add.Enabled = false;
            admin_btn_generalM_delete.Enabled = false;
        }


        // 공통코드 - 대분류 그리드뷰 데이터셋 바인딩시 발생되는 이벤트
        private void admin_gv_generalL_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                DataGridViewRow row = admin_gv_generalL.Rows[index];
                string big_code = row.Cells["gl_num"].Value.ToString().Trim();

                if (df.GeneralCodeL_status(big_code))
                {
                    if (big_code.Equals("01") || big_code.Equals("02") || big_code.Equals("03") || big_code.Equals("04"))
                    {
                        row.Cells["삭제가능 여부"].Value = "불가능(기본코드)";
                        row.Cells["삭제가능 여부"].Style.ForeColor = Color.Red;
                    }

                    else
                    {
                        row.Cells["삭제가능 여부"].Value = "불가능";
                        row.Cells["삭제가능 여부"].Style.ForeColor = Color.Red;
                    }
                }
                
                else
                {
                    row.Cells["삭제가능 여부"].Value = "가능";
                    row.Cells["삭제가능 여부"].Style.ForeColor = Color.Black;
                }

                


            }
        }


        // 공통코드 - 중분류 그리드뷰 데이터셋 바인딩시 발생되는 이벤트
        private void admin_gv_generalM_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                DataGridViewRow row = admin_gv_generalM.Rows[index];
                string mid_code = row.Cells["gm_num"].Value.ToString().Trim();
                if (df.GeneralCodeM_status(mid_code))
                {

                    row.Cells["삭제가능 여부"].Value = "불가능";
                    row.Cells["삭제가능 여부"].Style.ForeColor = Color.Red;
                }

                else
                {
                    row.Cells["삭제가능 여부"].Value = "가능";
                    row.Cells["삭제가능 여부"].Style.ForeColor = Color.Black;
                }
            }
        }


        // 공통코드 - 대분류 삭제
        private void admin_btn_generalL_delete_Click(object sender, EventArgs e)
        {
            if (admin_gv_generalL.CurrentRow.Selected == false)
            {
                MessageBox.Show("삭제하실 코드를 선택하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                
                if (MessageBox.Show("해당 코드를 삭제하시겠습니까?\n대분류를 삭제하실 경우 중분류도 함께 삭제됩니다.", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    admin_gv_generalL.DataSource = df.GeneralCodeL_Delete(generalL_code);
                    admin_gv_generalL_Customizing();
                    admin_gv_generalL.ClearSelection();
                    admin_gv_generalM.DataSource = null;        // 중분류 데이터셋 초기화
                    index_generalL = -1;
                    index_generalM = -1;
                    MessageBox.Show("해당 코드를 삭제하였습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }


        // 공통코드 - 대분류 등록
        private void btnGeneralAdd1_Click(object sender, EventArgs e)
        {
            if (admin_txt_generalL_Code.Text == "")
            {
                MessageBox.Show("등록하실 코드를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (admin_txt_generalL_Name.Text == "")
            {
                MessageBox.Show("등록하실 코드명을 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                if (df.GeneralCodeL_truncate(admin_txt_generalL_Code.Text, admin_txt_generalL_Name.Text))
                {
                    MessageBox.Show("이미 등록된 코드 혹은 코드명입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else
                {
                    admin_gv_generalL.DataSource = df.GeneralCodeL_add(admin_txt_generalL_Code.Text, admin_txt_generalL_Name.Text);
                    admin_gv_generalL_Customizing();
                    admin_gv_generalL.ClearSelection();
                    admin_gv_generalM.DataSource = null;        // 중분류 데이터셋 초기화
                    admin_txt_generalL_Code.Text = "";
                    admin_txt_generalL_Name.Text = "";
                    admin_txt_generalM_Code.Text = "";
                    admin_txt_generalM_Name.Text = "";
                    admin_txt_generalM_Parent.Text = "";
                    MessageBox.Show("해당 코드를 등록하였습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        // 공통코드 - 중분류 등록
        private void admin_btn_generalM_add_Click(object sender, EventArgs e)
        {
            if (admin_txt_generalM_Code.Text == "")
            {
                MessageBox.Show("등록하실 코드를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (admin_txt_generalM_Name.Text == "")
            {
                MessageBox.Show("등록하실 코드명을 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (admin_txt_generalM_Code.Text.Length != 2 && admin_txt_generalM_Code.Text.Length != 4)
            {
                MessageBox.Show("2자리 혹은 4자리의 숫자로 입력하세요.\n4자리로 입력하시는 경우 앞의 두 자리 숫자가 대분류랑 일치해야 합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                string gmnum = "";
                string gmname = admin_txt_generalM_Name.Text;
                string gmparent = admin_txt_generalM_Parent.Text;         

                // 코드를 2자리로 입력한 경우
                if (admin_txt_generalM_Code.Text.Length == 2)
                {
                    // 부모(대분류)코드 + 입력코드
                    gmnum = admin_txt_generalM_Parent.Text + admin_txt_generalM_Code.Text;
               
                }

                // 코드를 4자리로 입력한 경우
                else if (admin_txt_generalM_Code.Text.Length == 4)
                {
                    gmnum = admin_txt_generalM_Code.Text;
                    string pcode = gmnum.Substring(0, 2);       // 대분류 코드만 가져옴
                    string code = gmnum.Substring(2);           // 나머지
                    if (!pcode.Equals(gmparent))                // 입력한 앞 2자리 대분류 코드가 일치하지 않을경우
                    {
                        pcode = gmparent;
                        gmnum = pcode + code;
                        MessageBox.Show("앞의 두 자리 숫자가 대분류랑 일치하지 않아서\n자동으로 수정하여 등록합니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


                if (df.GeneralCodeM_truncate(gmnum, gmname))
                {
                    MessageBox.Show("이미 등록된 코드 혹은 코드명입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else
                {
                    admin_gv_generalM.DataSource = df.GeneralCodeM_add(gmnum, gmname, gmparent);
                    admin_gv_generalM_Customizing();
                    admin_gv_generalM.ClearSelection();
                    admin_txt_generalM_Code.Text = "";
                    admin_txt_generalM_Name.Text = "";
                    MessageBox.Show("해당 코드를 등록하였습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        // 공통코드 - 중분류 삭제
        private void admin_btn_generalM_delete_Click(object sender, EventArgs e)
        {
            if (admin_gv_generalM.CurrentRow.Selected == false)
            {
                MessageBox.Show("삭제하실 코드를 선택하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                
                if (MessageBox.Show("해당 코드를 삭제하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    string gmparent = admin_txt_generalM_Parent.Text;
                   // MessageBox.Show(index_mid.ToString());
                    admin_gv_generalM.DataSource = df.GeneralCodeM_Delete(index_generalM, gmparent);
                    admin_gv_generalM_Customizing();
                    admin_gv_generalM.ClearSelection();
                    index_generalM = -1;
                    admin_txt_generalM_Code.Text = "";
                    admin_txt_generalM_Name.Text = "";

                    MessageBox.Show("해당 코드를 삭제하였습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        // 공통코드 대분류 그리드뷰 커스터마이징
        private void admin_gv_generalL_Customizing()
        {
            admin_gv_generalL.AutoGenerateColumns = false;
            admin_gv_generalL.Columns["gl_num"].HeaderText = "코드";
            admin_gv_generalL.Columns["gl_name"].HeaderText = "분류명";

            admin_gv_generalL.Columns["gl_num"].DisplayIndex = 0;
            admin_gv_generalL.Columns["gl_name"].DisplayIndex = 1;
            admin_gv_generalL.Columns["삭제가능 여부"].DisplayIndex = 2;

            admin_gv_generalL.Columns["gl_num"].Width = 45;

        }


        // 공통코드 중분류 그리드뷰 커스터마이징
        private void admin_gv_generalM_Customizing()
        {
            admin_gv_generalM.AutoGenerateColumns = false;
            admin_gv_generalM.Columns["gm_num"].HeaderText = "코드";
            admin_gv_generalM.Columns["gm_name"].HeaderText = "분류명";

            admin_gv_generalM.Columns["gm_num"].DisplayIndex = 0;
            admin_gv_generalM.Columns["gm_name"].DisplayIndex = 1;
            admin_gv_generalM.Columns["삭제가능 여부"].DisplayIndex = 2;

            admin_gv_generalM.Columns["gm_num"].Width = 45;
            admin_gv_generalM.Columns["gm_name"].Width = 100;
        }


        // 공통코드 - 대분류 셀 클릭시
        private void admin_gv_generalL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                index_generalL = Convert.ToInt32(admin_gv_generalL.SelectedRows[0].Index);
                generalL_code = admin_gv_generalL.Rows[e.RowIndex].Cells["gl_num"].Value.ToString();
                admin_gv_generalM.DataSource = df.getGeneralCode_M(generalL_code);
                admin_gv_generalM.ClearSelection();
                admin_gv_generalM_Customizing();

                admin_btn_generalM_add.Enabled = true;
                admin_btn_generalM_delete.Enabled = false;

                admin_txt_generalL_Code.Text = admin_gv_generalL.Rows[e.RowIndex].Cells["gl_num"].Value.ToString();
                admin_txt_generalL_Name.Text = admin_gv_generalL.Rows[e.RowIndex].Cells["gl_name"].Value.ToString();
                admin_txt_generalM_Parent.Text = admin_gv_generalL.Rows[e.RowIndex].Cells["gl_num"].Value.ToString();

                index_generalM = -1;

                if (admin_gv_generalL.Rows[e.RowIndex].Cells["삭제가능 여부"].Value.ToString().Equals("불가능") || admin_gv_generalL.Rows[e.RowIndex].Cells["삭제가능 여부"].Value.ToString().Equals("불가능(기본코드)"))
                {
                    admin_btn_generalL_delete.Enabled = false;
                }

                else
                {
                    admin_btn_generalL_delete.Enabled = true;
                }

            }
        }


        // 공통코드 - 중분류 셀 클릭시
        private void admin_gv_generalM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                index_generalM = Convert.ToInt32(admin_gv_generalM.SelectedRows[0].Index);
                generalM_code = admin_gv_generalM.Rows[e.RowIndex].Cells["gm_num"].Value.ToString();
                admin_txt_generalM_Code.Text = admin_gv_generalM.Rows[e.RowIndex].Cells["gm_num"].Value.ToString();
                admin_txt_generalM_Name.Text = admin_gv_generalM.Rows[e.RowIndex].Cells["gm_name"].Value.ToString();


                if (admin_gv_generalM.Rows[e.RowIndex].Cells["삭제가능 여부"].Value.ToString().Equals("불가능"))
                {
                    admin_btn_generalM_delete.Enabled = false;
                }

                else
                {
                    admin_btn_generalM_delete.Enabled = true;
                }
            }
        } 




        /* 분류코드 등록 기능부분 */
        // 분류코드 - 초기
        private void admin_tab_sortcode_Layout(object sender, LayoutEventArgs e)
        {
            admin_gv_sortL.DataSource = df.getSortCode_L();
            admin_gv_sortL_Customizing();
            admin_gv_sortL.ClearSelection();
            admin_gv_sortM.DataSource = null;        // 중분류 데이터셋 초기화
            admin_gv_sortS.DataSource = null;
            admin_txt_sortL_Code.Text = "";
            admin_txt_sortL_Name.Text = "";
            admin_txt_sortM_Code.Text = "";
            admin_txt_sortM_Name.Text = "";
            admin_txt_sortM_Parent.Text = "";
            admin_txt_sortS_Code.Text = "";
            admin_txt_sortS_Name.Text = "";
            admin_txt_sortS_Parent1.Text = "";
            admin_txt_sortS_Parent2.Text = "";
            admin_btn_sortM_add.Enabled = false;
            admin_btn_sortS_add.Enabled = false;
            admin_btn_sortM_delete.Enabled = false;
            admin_btn_sortS_delete.Enabled = false;
        }


        // 분류코드 - 대분류 그리드뷰 커스터마이징
        public void admin_gv_sortL_Customizing()
        {
            admin_gv_sortL.AutoGenerateColumns = false;
            admin_gv_sortL.Columns["b_num"].HeaderText = "코드";
            admin_gv_sortL.Columns["b_name"].HeaderText = "분류명";

            admin_gv_sortL.Columns["b_num"].DisplayIndex = 0;
            admin_gv_sortL.Columns["b_name"].DisplayIndex = 1;
            admin_gv_sortL.Columns["삭제가능 여부"].DisplayIndex = 2;

            admin_gv_sortL.Columns["b_num"].Width = 45;
            admin_gv_sortL.Columns["b_name"].Width = 150;
        }


        // 분류코드 - 중분류 그리드뷰 커스터마이징
        public void admin_gv_sortM_Customizing()
        {
            admin_gv_sortM.AutoGenerateColumns = false;
            admin_gv_sortM.Columns["m_num"].HeaderText = "코드";
            admin_gv_sortM.Columns["m_name"].HeaderText = "분류명";

            admin_gv_sortM.Columns["m_num"].DisplayIndex = 0;
            admin_gv_sortM.Columns["m_name"].DisplayIndex = 1;
            admin_gv_sortM.Columns["삭제가능 여부"].DisplayIndex = 2;

            admin_gv_sortM.Columns["m_num"].Width = 45;
            admin_gv_sortM.Columns["m_name"].Width = 150;
        }


        // 분류코드 - 소분류 그리드뷰 커스터마이징
        public void admin_gv_sortS_Customizing()
        {
            admin_gv_sortS.AutoGenerateColumns = false;
            admin_gv_sortS.Columns["s_num"].HeaderText = "코드";
            admin_gv_sortS.Columns["s_name"].HeaderText = "분류명";

            admin_gv_sortS.Columns["s_num"].DisplayIndex = 0;
            admin_gv_sortS.Columns["s_name"].DisplayIndex = 1;
            admin_gv_sortS.Columns["삭제가능 여부"].DisplayIndex = 2;

            admin_gv_sortS.Columns["s_num"].Width = 45;
            admin_gv_sortS.Columns["s_name"].Width = 150;
        }


        // 분류코드 - 대분류 셀 클릭시
        private void admin_gv_sortL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                index_sortL = Convert.ToInt32(admin_gv_sortL.SelectedRows[0].Index);
                sortL_code = admin_gv_sortL.Rows[e.RowIndex].Cells["b_num"].Value.ToString();

                admin_gv_sortM.DataSource = df.getSortCode_M(sortL_code);
                admin_gv_sortM.ClearSelection();
                admin_gv_sortM_Customizing();

                admin_gv_sortS.DataSource = null;

                admin_txt_sortL_Code.Text = admin_gv_sortL.Rows[e.RowIndex].Cells["b_num"].Value.ToString();
                admin_txt_sortL_Name.Text = admin_gv_sortL.Rows[e.RowIndex].Cells["b_name"].Value.ToString();
                admin_txt_sortM_Parent.Text = admin_gv_sortL.Rows[e.RowIndex].Cells["b_num"].Value.ToString();
                admin_txt_sortM_Code.Text = "";
                admin_txt_sortM_Name.Text = "";
                admin_txt_sortS_Code.Text = "";
                admin_txt_sortS_Name.Text = "";
                admin_txt_sortS_Parent1.Text = "";
                admin_txt_sortS_Parent2.Text = "";
                admin_btn_sortM_add.Enabled = true;
                admin_btn_sortS_add.Enabled = false;
                admin_btn_sortM_delete.Enabled = false;
                admin_btn_sortS_delete.Enabled = false;


                index_generalM = -1;

                if (admin_gv_sortL.Rows[e.RowIndex].Cells["삭제가능 여부"].Value.ToString().Equals("불가능"))
                {
                    admin_btn_sortL_delete.Enabled = false;
                }

                else
                {
                    admin_btn_sortL_delete.Enabled = true;
                }

            }

        }


        // 분류코드 - 대분류 데이터셋 바인딩시
        private void admin_gv_sortL_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                DataGridViewRow row = admin_gv_sortL.Rows[index];
                string big_code = row.Cells["b_num"].Value.ToString().Trim();

                if (df.SortCodeL_status(big_code))
                {
                    row.Cells["삭제가능 여부"].Value = "불가능";
                    row.Cells["삭제가능 여부"].Style.ForeColor = Color.Red;
                }

                else
                {
                    row.Cells["삭제가능 여부"].Value = "가능";
                    row.Cells["삭제가능 여부"].Style.ForeColor = Color.Black;
                }
            }
        }


        // 분류코드 - 중분류 셀 클릭시
        private void admin_gv_sortM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                index_sortM = Convert.ToInt32(admin_gv_sortM.SelectedRows[0].Index);
                sortM_code = admin_gv_sortM.Rows[e.RowIndex].Cells["m_num"].Value.ToString();
                admin_gv_sortS.DataSource = df.getSortCode_S(sortM_code);
                admin_gv_sortS.ClearSelection();
                admin_gv_sortS_Customizing();

                admin_txt_sortM_Code.Text = admin_gv_sortM.Rows[e.RowIndex].Cells["m_num"].Value.ToString();
                admin_txt_sortM_Name.Text = admin_gv_sortM.Rows[e.RowIndex].Cells["m_name"].Value.ToString();
                admin_txt_sortS_Parent1.Text = admin_txt_sortM_Parent.Text;
                admin_txt_sortS_Parent2.Text = admin_gv_sortM.Rows[e.RowIndex].Cells["m_num"].Value.ToString();
                admin_txt_sortS_Code.Text = "";
                admin_txt_sortS_Name.Text = "";
                admin_btn_sortS_add.Enabled = true;

                if (admin_gv_sortM.Rows[e.RowIndex].Cells["삭제가능 여부"].Value.ToString().Equals("불가능"))
                {
                    admin_btn_sortM_delete.Enabled = false;
                }

                else
                {
                    admin_btn_sortM_delete.Enabled = true;
                }

            }
        }


        // 분류코드 - 중분류 데이터셋 바인딩시
        private void admin_gv_sortM_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                DataGridViewRow row = admin_gv_sortM.Rows[index];
                string mid_code = row.Cells["m_num"].Value.ToString().Trim();

                if (df.SortCodeM_status(mid_code))
                {
                    row.Cells["삭제가능 여부"].Value = "불가능";
                    row.Cells["삭제가능 여부"].Style.ForeColor = Color.Red;
                }

                else
                {
                    row.Cells["삭제가능 여부"].Value = "가능";
                    row.Cells["삭제가능 여부"].Style.ForeColor = Color.Black;
                }
            }
        }


        // 분류코드 - 소분류 셀 클릭시
        private void admin_gv_sortS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                index_sortS = Convert.ToInt32(admin_gv_sortS.SelectedRows[0].Index);
                sortS_code = admin_gv_sortS.Rows[e.RowIndex].Cells["s_num"].Value.ToString();
                admin_txt_sortS_Code.Text = admin_gv_sortS.Rows[e.RowIndex].Cells["s_num"].Value.ToString();
                admin_txt_sortS_Name.Text = admin_gv_sortS.Rows[e.RowIndex].Cells["s_name"].Value.ToString();

                if (admin_gv_sortS.Rows[e.RowIndex].Cells["삭제가능 여부"].Value.ToString().Equals("불가능"))
                {
                    admin_btn_sortS_delete.Enabled = false;
                }

                else
                {
                    admin_btn_sortS_delete.Enabled = true;
                }

            }
        }


        // 분류코드 - 소분류 데이터셋 바인딩시
        private void admin_gv_sortS_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                DataGridViewRow row = admin_gv_sortS.Rows[index];
                string sml_code = row.Cells["s_num"].Value.ToString().Trim();

                if (df.SortCodeS_status(sml_code))
                {
                    row.Cells["삭제가능 여부"].Value = "불가능";
                    row.Cells["삭제가능 여부"].Style.ForeColor = Color.Red;
                }

                else
                {
                    row.Cells["삭제가능 여부"].Value = "가능";
                    row.Cells["삭제가능 여부"].Style.ForeColor = Color.Black;
                }
            }
        }


        // 분류코드 - 탭에서 아무곳이나 클릭시 선택 해제
        private void admin_tab_sortcode_Click(object sender, EventArgs e)
        {
            admin_gv_sortL.ClearSelection();
            admin_gv_sortM.ClearSelection();
            admin_gv_sortS.ClearSelection();

        }


        // 분류코드 - 대분류 추가
        private void admin_btn_sortL_add_Click(object sender, EventArgs e)
        {
            if (admin_txt_sortL_Code.Text == "")
            {
                MessageBox.Show("등록하실 코드를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (admin_txt_sortL_Name.Text == "")
            {
                MessageBox.Show("등록하실 코드명을 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                if (df.SortCodeL_truncate(admin_txt_sortL_Code.Text, admin_txt_sortL_Name.Text))
                {
                    MessageBox.Show("이미 등록된 코드 혹은 코드명입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else
                {
                    admin_gv_sortL.DataSource = df.SortCodeL_add(admin_txt_sortL_Code.Text, admin_txt_sortL_Name.Text);
                    admin_gv_sortL_Customizing();
                    admin_gv_sortL.ClearSelection();
                    admin_gv_sortM.DataSource = null;        // 중분류 데이터셋 초기화
                    admin_txt_sortL_Code.Text = "";
                    admin_txt_sortL_Name.Text = "";
                    //admin_txt_generalM_Code.Text = "";
                    //admin_txt_generalM_Name.Text = "";
                    //admin_txt_generalM_Parent.Text = "";
                    MessageBox.Show("해당 코드를 등록하였습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        // 분류코드 - 대분류 삭제
        private void admin_btn_sortL_delete_Click(object sender, EventArgs e)
        {
            if (admin_gv_sortL.CurrentRow.Selected == false)
            {
                MessageBox.Show("삭제하실 코드를 선택하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                if (MessageBox.Show("해당 코드를 삭제하시겠습니까?\n대분류를 삭제하실 경우 중분류, 소분류도 함께 삭제됩니다.", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {

                    admin_gv_sortL.DataSource = df.SortCodeL_Delete(sortL_code);
                    admin_gv_sortL_Customizing();
                    admin_gv_sortL.ClearSelection();
                    admin_gv_sortM.DataSource = null;        // 중분류 데이터셋 초기화
                    admin_gv_sortS.DataSource = null;        // 소분류 데이터셋 초기화
                 
                    MessageBox.Show("해당 코드를 삭제하였습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }


        // 분류코드 - 중분류 추가
        private void admin_btn_sortM_add_Click(object sender, EventArgs e)
        {
            if (admin_txt_sortM_Code.Text == "")
            {
                MessageBox.Show("등록하실 코드를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (admin_txt_sortM_Name.Text == "")
            {
                MessageBox.Show("등록하실 코드명을 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (admin_txt_sortM_Code.Text.Length != 2 && admin_txt_sortM_Code.Text.Length != 4)
            {
                MessageBox.Show("2자리 혹은 4자리의 숫자로 입력하세요.\n4자리로 입력하시는 경우 앞의 두 자리 숫자가 대분류랑 일치해야 합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                string num = "";
                string name = admin_txt_sortM_Name.Text;
                string parent = admin_txt_sortM_Parent.Text;

                // 코드를 2자리로 입력한 경우
                if (admin_txt_sortM_Code.Text.Length == 2)
                {
                    // 부모(대분류)코드 + 입력코드
                    num = admin_txt_sortM_Parent.Text + admin_txt_sortM_Code.Text;

                }

                // 코드를 4자리로 입력한 경우
                else if (admin_txt_sortM_Code.Text.Length == 4)
                {
                    num = admin_txt_sortM_Code.Text;
                    string pcode = num.Substring(0, 2);       // 대분류 코드만 가져옴
                    string code = num.Substring(2);           // 나머지
                    if (!pcode.Equals(parent))                // 입력한 앞 2자리 대분류 코드가 일치하지 않을경우
                    {
                        pcode = parent;
                        num = pcode + code;
                        MessageBox.Show("앞의 두 자리 숫자가 대분류랑 일치하지 않아서\n자동으로 수정하여 등록합니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


                if (df.SortCodeM_truncate(num, name))
                {
                    MessageBox.Show("이미 등록된 코드 혹은 코드명입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else
                {
                    admin_gv_sortM.DataSource = df.SortCodeM_add(num, name, parent);
                    admin_gv_sortM_Customizing();
                    admin_gv_sortM.ClearSelection();
                    admin_txt_sortM_Code.Text = "";
                    admin_txt_sortM_Name.Text = "";
                    MessageBox.Show("해당 코드를 등록하였습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        // 분류코드 - 중분류 삭제
        private void admin_btn_sortM_delete_Click(object sender, EventArgs e)
        {
            if (admin_gv_sortM.CurrentRow.Selected == false)
            {
                MessageBox.Show("삭제하실 코드를 선택하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                if (MessageBox.Show("해당 코드를 삭제하시겠습니까?\n중분류를 삭제하실 경우 소분류도 함께 삭제됩니다.", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {

                    admin_gv_sortM.DataSource = df.SortCodeM_Delete(sortM_code);
                    admin_gv_sortM_Customizing();
                    admin_gv_sortM.ClearSelection();
                    admin_gv_sortS.DataSource = null;        // 소분류 데이터셋 초기화

                    MessageBox.Show("해당 코드를 삭제하였습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }


        // 분류코드 - 소분류 추가
        private void admin_btn_sortS_add_Click(object sender, EventArgs e)
        {
            if (admin_txt_sortS_Code.Text == "")
            {
                MessageBox.Show("등록하실 코드를 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (admin_txt_sortS_Name.Text == "")
            {
                MessageBox.Show("등록하실 코드명을 입력하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (admin_txt_sortS_Code.Text.Length != 2 && admin_txt_sortS_Code.Text.Length != 4 && admin_txt_sortS_Code.Text.Length != 6)
            {
                MessageBox.Show("2자리 혹은 4자리나 6자리의 숫자로 입력하세요.\n6자리로 입력하시는 경우 앞의 두 자리 숫자는 대분류,\n나머지 앞의 두 자리 숫자는 중분류 뒤 두 자리 숫자랑 일치해야 하며\n4자리로 입력하시는 경우 앞의 두 자리 숫자는 중분류랑 일치해야 합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else
            {
                string num = "";
                string name = admin_txt_sortS_Name.Text;
                string parent = admin_txt_sortS_Parent1.Text;       // 대분류코드
                string parent2 = admin_txt_sortS_Parent2.Text;      // 중분류코드

                // 코드를 2자리로 입력한 경우
                if (admin_txt_sortS_Code.Text.Length == 2)
                {
                    // 부모(대·중분류)코드 + 입력코드
                    num = parent2 + admin_txt_sortS_Code.Text;

                }
                
                // 코드를 4자리로 입력한 경우
                else if (admin_txt_sortS_Code.Text.Length == 4)
                {
                    num = parent + admin_txt_sortS_Code.Text;  // 대분류값 + 입력한 값

                    string mcode = num.Substring(2, 2);       // 입력한 코드번호에서 중분류 코드부분만 가져옴
                    string code = num.Substring(4);           // 입력한 코드번호에서 나머지(실제 소분류 코드부분)
                    string substring_sortcodeM = parent2.Substring(2);       // 중분류 코드부분만 가져옴

                    if (!mcode.Equals(substring_sortcodeM))
                    {
                        mcode = substring_sortcodeM;
                        num = parent + mcode + code;
                        MessageBox.Show("앞의 두 자리 숫자가 중분류랑 일치하지 않아서\n자동으로 수정하여 등록합니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    
                }

                // 코드를 6자리로 입력한 경우
                else if (admin_txt_sortS_Code.Text.Length == 6)
                {
                    num = admin_txt_sortS_Code.Text;
                    string bcode = num.Substring(0, 2);           // 입력한 코드번호에서 대분류 코드만 가져옴
                    string mcode = num.Substring(2, 2);           // 입력한 코드번호에서 중분류 코드만 가져옴
                    string code = num.Substring(4);               // 나머지(실제 소분류 코드부분) 

                    string sortcodeL = parent;                    // 실제 대분류 코드 가져옴
                    string substring_sortcodeM = parent2.Substring(2, 2);

                    if (!bcode.Equals(sortcodeL) || !mcode.Equals(substring_sortcodeM))               
                    {
                        bcode = sortcodeL;
                        mcode = substring_sortcodeM;
                        num = bcode + mcode + code;
                        MessageBox.Show("앞의 코드가 대분류, 중분류랑 일치하지 않아서\n자동으로 수정하여 등록합니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


                if (df.SortCodeS_truncate(num, name))
                {
                    MessageBox.Show("이미 등록된 코드 혹은 코드명입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                else
                {
                    admin_gv_sortS.DataSource = df.SortCodeS_add(num, name, parent2);
                    admin_gv_sortS_Customizing();
                    admin_gv_sortS.ClearSelection();
                    admin_txt_sortS_Code.Text = "";
                    admin_txt_sortS_Name.Text = "";
                    MessageBox.Show("해당 코드를 등록하였습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        // 분류코드 - 소분류 삭제
        private void admin_btn_sortS_delete_Click(object sender, EventArgs e)
        {
            if (admin_gv_sortS.CurrentRow.Selected == false)
            {
                MessageBox.Show("삭제하실 코드를 선택하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                if (MessageBox.Show("해당 코드를 삭제하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {

                    admin_gv_sortS.DataSource = df.SortCodeS_Delete(sortS_code);
                    admin_gv_sortS_Customizing();
                    admin_gv_sortS.ClearSelection();
                    admin_txt_sortS_Code.Text = "";
                    admin_txt_sortS_Name.Text = "";
                    MessageBox.Show("해당 코드를 삭제하였습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }






        /* commons */
        // 걍 껐을때..
        private void Admin_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // 메뉴 - 로그아웃
        private void admin_menu_logout_Click(object sender, EventArgs e)
        {
            this.Hide();                    // 숨기고
            Login log = new Login();        // 로그인 모듈 선언
            log.Show();                     // 쇼
        }

        // 메뉴 - 종료
        private void admin_menu_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ???
        private void admin_menu_info_Click(object sender, EventArgs e)
        {
            info info = new info();
            info.ShowDialog();
        }

        // 비번번경
        private void admin_menu_changePW_Click(object sender, EventArgs e)
        {
            ChangePW pw = new ChangePW();
            pw.login_id = "admin";
            pw.ShowDialog();
        }
       
    }
}
