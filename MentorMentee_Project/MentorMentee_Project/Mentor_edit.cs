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
    public partial class Mentor_edit : Form
    {
        Data_func df = new Data_func();
        public string id = "";
     
        public bool isAdmin = false;


        public static string sex = "";  // 성별
        public static string achievement_code = ""; // 최종학력 코드
        public static string jobs_code = "";         // 직업 코드
        public static string loc_code = "";          // 지역 코드
        public static string big_code = "";          // 분야(대) 코드
        public static string mid_code = "";          // 분야(중) 코드
        public static string sml_code = "";          // 분야(소) 코드

        public static string domain = "";           // 도메인
        public static string milrank_code = "";     // 계급코드

        private Admin_main am;
        public int index_mentorlist;


        public Mentor_edit()
        {
            InitializeComponent();

        }

        public Mentor_edit(Admin_main admin_main, int index)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            am = admin_main;
            index_mentorlist = index;
        }

        

        private void Mentor_edit_Load(object sender, EventArgs e)
        {
            if (isAdmin)
            {
                admin_menu_manage_mentor.Visible = true;
                if (df.Mentor_isMentoring(id))
                {
                    MessageBox.Show("진행중인 멘토링이 있는 회원은 개인정보 수정이 불가능합니다.\n(비밀번호 초기화, 인증사항 관리만 가능)", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mentoredit_txt_name.Enabled = false;
                    mentoredit_cb_sex.Enabled = false;
                    mentoredit_txt_age.Enabled = false;
                    mentoredit_cb_achievement.Enabled = false;
                    mentoredit_txt_major.Enabled = false;
                    mentoredit_cb_jobs.Enabled = false;
                    mentoredit_cb_militaryrank.Enabled = false;
                    mentoredit_cb_big.Enabled = false;
                    mentoredit_cb_mid.Enabled = false;
                    mentoredit_cb_small.Enabled = false;
                    mentoredit_cb_location.Enabled = false;
                    mentoredit_txt_callnum1.Enabled = false;
                    mentoredit_txt_callnum2.Enabled = false;
                    mentoredit_txt_callnum3.Enabled = false;
                    mentoredit_txt_email.Enabled = false;
                    mentoredit_cb_domain.Enabled = false;
                    mentoredit_txt_domain.Enabled = false;
                    mentoredit_txt_company.Enabled = false;
                    mentoredit_txt_depart.Enabled = false;
                    mentoredit_txt_companyaddress.Enabled = false;
                    mentoredit_txt_post1.Enabled = false;
                    mentoredit_txt_post2.Enabled = false;
                    mentoredit_btn_ok.Enabled = false;
                }
            }

            else
            {
                admin_menu_manage_mentor.Visible = false;
                
            }


            // 최종학력 콤보박스 DB 연동
            mentoredit_cb_achievement.DataSource = df.Mentorjoin_educational();
            mentoredit_cb_achievement.ValueMember = "gm_num";
            mentoredit_cb_achievement.DisplayMember = "gm_name";

            // 계급 콤보박스 DB 연동
            mentoredit_cb_militaryrank.DataSource = df.Mentorjoin_militaryrank();
            mentoredit_cb_militaryrank.ValueMember = "gm_num";
            mentoredit_cb_militaryrank.DisplayMember = "gm_name";

            // 직업 콤보박스 DB 연동
            mentoredit_cb_jobs.DataSource = df.Join_jobs();
            mentoredit_cb_jobs.ValueMember = "gm_num";
            mentoredit_cb_jobs.DisplayMember = "gm_name";

            // 지역 콤보박스 DB 연동
            mentoredit_cb_location.DataSource = df.Join_location();
            mentoredit_cb_location.ValueMember = "gm_num";
            mentoredit_cb_location.DisplayMember = "gm_name";

            // 대분류 콤보박스 DB 연동
            mentoredit_cb_big.DataSource = df.Join_sortL();
            mentoredit_cb_big.ValueMember = "b_num";
            mentoredit_cb_big.DisplayMember = "b_name";


            Dictionary<string, string> drr = df.Request_getMentorInfo(id);
            mentoredit_txt_name.Text = drr["mentor_name"];
            mentoredit_cb_sex.Text = drr["mentor_sex"];
            sex = drr["mentor_sex"];
            mentoredit_txt_age.Text = drr["mentor_age"];
            mentoredit_cb_achievement.SelectedValue = drr["mentor_educational"];
            achievement_code = drr["mentor_educational"];
            mentoredit_txt_major.Text = drr["mentor_major"];
            mentoredit_cb_jobs.SelectedValue = drr["mentor_jobs"];
            jobs_code = drr["mentor_jobs"];

            // 군인? 민간인?
            if (mentoredit_cb_jobs.Text.Equals("군인"))
            {
                mentoredit_cb_militaryrank.Visible = true;
                mentoredit_cb_militaryrank.SelectedValue = drr["mentor_soldierclass"];
                milrank_code = drr["mentor_soldierclass"];
            }

            else
            {
                mentoredit_cb_militaryrank.Visible = false;
            }

            
            // 중분류 콤보박스 DB 연동
            mentoredit_cb_mid.DataSource = df.Join_SortM(drr["mentor_part1"]);
            mentoredit_cb_mid.ValueMember = "m_num";
            mentoredit_cb_mid.DisplayMember = "m_name";

            // 소분류 콤보박스 DB 연동
            mentoredit_cb_small.DataSource = df.Join_SortS(drr["mentor_part2"]);
            mentoredit_cb_small.ValueMember = "s_num";
            mentoredit_cb_small.DisplayMember = "s_name";

            mentoredit_cb_big.SelectedValue = drr["mentor_part1"];
            mentoredit_cb_mid.SelectedValue = drr["mentor_part2"];
            mentoredit_cb_small.SelectedValue = drr["mentor_part3"];
            mentoredit_cb_location.SelectedValue = drr["mentor_location"];
            big_code = drr["mentor_part1"];
            mid_code = drr["mentor_part2"];
            sml_code = drr["mentor_part3"];
            loc_code = drr["mentor_location"];


            string[] callnum = drr["mentor_tel"].Split('-');
            mentoredit_txt_callnum1.Text = callnum[0];
            mentoredit_txt_callnum2.Text = callnum[1];
            mentoredit_txt_callnum3.Text = callnum[2];

            mentoredit_txt_email.Text = drr["mentor_email"];

            // 도메인 콤보박스에 이메일 도메인이 존재하지 않는지..
            if (mentoredit_cb_domain.FindStringExact(drr["mentor_domain"]) == -1)
            {
                // 존재하지 않을경우(-1) 직접입력.
                mentoredit_txt_domain.Visible = true;
                mentoredit_cb_domain.Text = "직접입력";
                mentoredit_txt_domain.Text = drr["mentor_domain"];
            }

            else
            {
                mentoredit_txt_domain.Visible = false;
                mentoredit_cb_domain.Text = drr["mentor_domain"];
                
            }

            domain = drr["mentor_domain"];
            mentoredit_txt_company.Text = drr["mentor_company"];
            mentoredit_txt_depart.Text = drr["mentor_depart"];
            mentoredit_txt_companyaddress.Text = drr["mentor_address"];
            mentoredit_txt_post1.Text = drr["mentor_post1"];
            mentoredit_txt_post2.Text = drr["mentor_post2"];
        }



        // 성별 콤보박스를 선택했을 때
        private void mentoredit_cb_sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mentoredit_cb_sex.Text == "선택하세요")
            {
                sex = "";
            }
            else
            {
                sex = mentoredit_cb_sex.Text.ToString();
            }
        }


        // 최종학력 콤보박스를 선택했을 때
        private void mentoredit_cb_achievement_SelectedIndexChanged(object sender, EventArgs e)
        {
            achievement_code = mentoredit_cb_achievement.SelectedValue.ToString();       // 최종학력 코드 저장용 변수
        }


        // 직업 콤보박스를 선택했을 때
        private void mentoredit_cb_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            jobs_code = mentoredit_cb_jobs.SelectedValue.ToString();                    // 직업코드 저장용 변수

            if (mentoredit_cb_jobs.Text.Equals("군인"))
            {
                mentoredit_cb_militaryrank.Visible = true;
            }

            else
            {
                mentoredit_cb_militaryrank.Visible = false;
                milrank_code = "";       // 계급코드 초기화
                mentoredit_cb_militaryrank.SelectedIndex = 0;
            }
        }


        // 계급 콤보박스를 선택했을 때 
        private void mentoredit_cb_militaryrank_SelectedIndexChanged(object sender, EventArgs e)
        {
            milrank_code = mentoredit_cb_militaryrank.SelectedValue.ToString();                    // 계급코드 저장용 변수
        }


        // 분야 콤보박스(대)를 선택했을 때
        private void mentoredit_cb_big_SelectedIndexChanged(object sender, EventArgs e)
        {
            big_code = mentoredit_cb_big.SelectedValue.ToString();
            mentoredit_cb_mid.DataSource = df.Join_SortM(big_code);
            mentoredit_cb_mid.ValueMember = "m_num";
            mentoredit_cb_mid.DisplayMember = "m_name";
        }


        // 분야 콤보박스(중)를 선택했을 때
        private void mentoredit_cb_mid_SelectedIndexChanged(object sender, EventArgs e)
        {
            mid_code = mentoredit_cb_mid.SelectedValue.ToString();                      // 분야(중) 저장용 변수
            mentoredit_cb_small.DataSource = df.Join_SortS(mid_code);
            mentoredit_cb_small.ValueMember = "s_num";
            mentoredit_cb_small.DisplayMember = "s_name";
        }


        // 분야 콤보박스(소)를 선택했을 때
        private void mentoredit_cb_small_SelectedIndexChanged(object sender, EventArgs e)
        {
            sml_code = mentoredit_cb_small.SelectedValue.ToString();                      // 분야(소) 저장용 변수
        }


        // 지역 콤보박스를 선택했을 때
        private void mentoredit_cb_location_SelectedIndexChanged(object sender, EventArgs e)
        {
            loc_code = mentoredit_cb_location.SelectedValue.ToString();
        }


        // 도메인을 선택했을 때
        private void mentoredit_cb_domain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mentoredit_cb_domain.Text == "직접입력")
            {
                mentoredit_txt_domain.Visible = true;
                domain = "";
            }

            else if (mentoredit_cb_domain.Text == "선택하세요")
            {
                mentoredit_txt_domain.Visible = false;
                domain = "";
            }

            else
            {
                mentoredit_txt_domain.Visible = false;
                mentoredit_txt_domain.Text = "";
                domain = mentoredit_cb_domain.Text.ToString();
            }
        }


        // 확인 버튼을 눌렀을 때
        private void mentoredit_btn_ok_Click(object sender, EventArgs e)
        {
            if (mentoredit_txt_name.Text == "")
            {
                MessageBox.Show("이름을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (sex.Equals(""))
            {
                MessageBox.Show("성별을 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentoredit_txt_age.Text == "")
            {
                MessageBox.Show("나이를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (achievement_code.Equals(""))
            {
                MessageBox.Show("최종학력을 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentoredit_txt_major.Text == "")
            {
                MessageBox.Show("전공을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (jobs_code.Equals(""))
            {
                MessageBox.Show("직업을 선택하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentoredit_cb_jobs.Text == "군인" && milrank_code.Equals(""))
            {
                MessageBox.Show("계급을 선택하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (big_code.Equals(""))
            {
                MessageBox.Show("분야를 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mid_code.Equals(""))
            {
                MessageBox.Show("분야를 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (sml_code.Equals(""))
            {
                MessageBox.Show("분야를 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (loc_code.Equals(""))
            {
                MessageBox.Show("지역을 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentoredit_txt_callnum1.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentoredit_txt_callnum2.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentoredit_txt_callnum3.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentoredit_txt_email.Text == "")
            {
                MessageBox.Show("이메일 주소를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (domain.Equals("") && mentoredit_txt_domain.Text == "")
            {
                MessageBox.Show("도메인을 선택 혹은 입력해주세요..", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                string tel = mentoredit_txt_callnum1.Text + "-" + mentoredit_txt_callnum2.Text + "-" + mentoredit_txt_callnum3.Text;

                if (mentoredit_cb_domain.Text == "직접입력")
                {
                    domain = mentoredit_txt_domain.Text;
                }

              //  if (df.Mentorjoin_truncate(mentoredit_txt_name.Text, tel, jobs_code))
                //{
                  //  MessageBox.Show("이미 존재하는 정보입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                   // return;
               // }

                Dictionary<string, string> dir = new Dictionary<string, string>();
                dir["mentor_name"] = mentoredit_txt_name.Text;
                dir["mentor_educational"] = achievement_code;
                dir["mentor_soldierclass"] = milrank_code;
                dir["mentor_major"] = mentoredit_txt_major.Text;
                dir["mentor_jobs"] = jobs_code;
                dir["mentor_company"] = mentoredit_txt_company.Text;
                dir["mentor_depart"] = mentoredit_txt_depart.Text;
                dir["mentor_location"] = loc_code;
                dir["mentor_part1"] = big_code;
                dir["mentor_part2"] = mid_code;
                dir["mentor_part3"] = sml_code;
                dir["mentor_tel"] = tel;
                dir["mentor_post1"] = mentoredit_txt_post1.Text;
                dir["mentor_post2"] = mentoredit_txt_post2.Text;
                dir["mentor_address"] = mentoredit_txt_companyaddress.Text;
                dir["mentor_sex"] = sex;
                dir["mentor_age"] = mentoredit_txt_age.Text;
                dir["mentor_email"] = mentoredit_txt_email.Text;
                dir["mentor_domain"] = domain;
                
                // 나중에 인증서부분을 처리할때 사용될 부분.
                //dir["mentor_certification_certificate"] = mentorjoin_txt_name.Text;
                //dir["mentor_career_certificate"] = mentorjoin_txt_name.Text;
                //dir["mentor_educational_certificate"] = mentorjoin_txt_name.Text;

                if (isAdmin)
                {
                    am.admin_gv_mentorlist.DataSource = df.Mentoredit_complete(dir, id);
                    am.admin_gv_mentorlist.Rows[index_mentorlist].Selected = true;
                    am.admin_gv_mentorlist.FirstDisplayedScrollingRowIndex = am.admin_gv_mentorlist.Rows[index_mentorlist].Index;
                    MessageBox.Show("멘토정보 수정을 완료하였습니다.", "멘토정보 수정 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("정보수정을 완료하였습니다.", "정보수정 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
        }

        // 취소 버튼을 눌렀을 때
        private void mentoredit_btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // 숫자만 입력 가능하게(연락처1)
        private void mentoredit_txt_callnum1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 숫자만 입력 가능하게(연락처2)
        private void mentoredit_txt_callnum2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        
        // 숫자만 입력 가능하게(연락처3)
        private void mentoredit_txt_callnum3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 숫자만 입력 가능하게(우편번호1)
        private void mentoredit_txt_post1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 숫자만 입력 가능하게(우편번호2)
        private void mentoredit_txt_post2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 멘토 비밀번호 초기화
        private void admin_menu_mentor_resetpw_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당 멘토의 비밀번호를 0000 으로 초기화 하시겠습니까?", "비밀번호 초기화", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                df.admin_resetPW(id);
                MessageBox.Show("해당 멘토의 비밀번호를 0000 으로 초기화 하였습니다.", "비밀번호 초기화 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
