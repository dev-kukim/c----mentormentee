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
    public partial class Mentor_join : Form
    {
        Data_func df = new Data_func();

        private Join_form jform;
        private string login_id;

        public static string sex = "";  // 성별
        public static string achievement_code = ""; // 최종학력 코드
        public static string jobs_code = "";         // 직업 코드
        public static string loc_code = "";          // 지역 코드
        public static string big_code = "";          // 분야(대) 코드
        public static string mid_code = "";          // 분야(중) 코드
        public static string sml_code = "";          // 분야(소) 코드

        public static string domain = "";           // 도메인
        public static string milrank_code = "";     // 계급코드
            
        public Mentor_join(Join_form joinform, string LoginID)
        {
            InitializeComponent();
            this.jform = joinform;
            this.login_id = LoginID;
        }


        // 초기 폼 로드부분
        private void Mentor_join_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            // 최종학력 콤보박스 DB 연동
            mentorjoin_cb_achievement.DataSource = df.Mentorjoin_educational();
            mentorjoin_cb_achievement.ValueMember = "gm_num";
            mentorjoin_cb_achievement.DisplayMember = "gm_name";

            // 계급 콤보박스 DB 연동
            mentorjoin_cb_militaryrank.DataSource = df.Mentorjoin_militaryrank();
            mentorjoin_cb_militaryrank.ValueMember = "gm_num";
            mentorjoin_cb_militaryrank.DisplayMember = "gm_name";

            // 직업 콤보박스 DB 연동
            mentorjoin_cb_jobs.DataSource = df.Join_jobs();
            mentorjoin_cb_jobs.ValueMember = "gm_num";
            mentorjoin_cb_jobs.DisplayMember = "gm_name";

            // 지역 콤보박스 DB 연동
            mentorjoin_cb_location.DataSource = df.Join_location();
            mentorjoin_cb_location.ValueMember = "gm_num";
            mentorjoin_cb_location.DisplayMember = "gm_name";

            // 대분류 콤보박스 DB 연동
            mentorjoin_cb_big.DataSource = df.Join_sortL();
            mentorjoin_cb_big.ValueMember = "b_num";
            mentorjoin_cb_big.DisplayMember = "b_name";

            // 성별 콤보박스 초기선택
            mentorjoin_cb_sex.SelectedIndex = 0;

            // 도메인 콤보박스 초기선택
            mentorjoin_cb_domain.SelectedIndex = 0;

        }


        // 시계 -_-
        void timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "현재시각 : " + DateTime.Now.ToString();
        }


        // 성별 콤보박스를 선택했을 때
        private void mentorjoin_cb_sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sex = mentorjoin_cb_sex.SelectedValue.ToString();         // 성별을 코드화할때 주석해제 및 밑에거 주석처리 필수         
            if (mentorjoin_cb_sex.Text == "선택하세요")
            {
                sex = "";
            }
            else
            {
                sex = mentorjoin_cb_sex.Text.ToString();
            }

        }
        

        // 최종학력 콤보박스를 선택했을 때
        private void mentorjoin_cb_achievement_SelectedIndexChanged(object sender, EventArgs e)
        {
            achievement_code = mentorjoin_cb_achievement.SelectedValue.ToString();       // 최종학력 코드 저장용 변수
        }


        // 직업 콤보박스를 선택했을 때
        private void mentorjoin_cb_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            jobs_code = mentorjoin_cb_jobs.SelectedValue.ToString();                    // 직업코드 저장용 변수

            if (mentorjoin_cb_jobs.Text.Equals("군인"))
            {
                mentorjoin_cb_militaryrank.Visible = true;
            }

            else
            {              
                mentorjoin_cb_militaryrank.Visible = false;              
                milrank_code = "";       // 계급코드 초기화
                mentorjoin_cb_militaryrank.SelectedIndex = 0;
            }

        }


        // 계급 콤보박스를 선택했을 때
        private void mentorjoin_cb_militaryrank_SelectedIndexChanged(object sender, EventArgs e)
        {
            milrank_code = mentorjoin_cb_militaryrank.SelectedValue.ToString();                    // 계급코드 저장용 변수
        }


        // 분야 콤보박스(대)를 선택했을 때
        private void mentorjoin_cb_big_SelectedIndexChanged(object sender, EventArgs e)
        {
            big_code = mentorjoin_cb_big.SelectedValue.ToString();
            mentorjoin_cb_mid.DataSource = df.Join_SortM(big_code);
            mentorjoin_cb_mid.ValueMember = "m_num";
            mentorjoin_cb_mid.DisplayMember = "m_name";
        }


        // 분야 콤보박스(중)을 선택했을 때
        private void mentorjoin_cb_mid_SelectedIndexChanged(object sender, EventArgs e)
        {
            mid_code = mentorjoin_cb_mid.SelectedValue.ToString();                      // 분야(중) 저장용 변수
            mentorjoin_cb_small.DataSource = df.Join_SortS(mid_code);
            mentorjoin_cb_small.ValueMember = "s_num";
            mentorjoin_cb_small.DisplayMember = "s_name";

        }


        // 분야 콤보박스(소)를 선택했을 때
        private void mentorjoin_cb_small_SelectedIndexChanged(object sender, EventArgs e)
        {
            sml_code = mentorjoin_cb_small.SelectedValue.ToString();                      // 분야(소) 저장용 변수
        }


        // 지역 콤보박스를 선택했을 때
        private void mentorjoin_cb_location_SelectedIndexChanged(object sender, EventArgs e)
        {
            loc_code = mentorjoin_cb_location.SelectedValue.ToString();
        }


        // 이메일 도메인을 선택했을 때
        private void mentorjoin_cb_domain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //domain = mentorjoin_cb_domain.SelectedValue.ToString();       // 도메인 코드화할때 사용..
            if (mentorjoin_cb_domain.Text == "직접입력")
            {
                mentorjoin_txt_domain.Visible = true;
                domain = "";
            }

            else if (mentorjoin_cb_domain.Text == "선택하세요")
            {
                mentorjoin_txt_domain.Visible = false;
                domain = "";
            }

            else
            {
                mentorjoin_txt_domain.Visible = false;
                mentorjoin_txt_domain.Text = "";
                domain = mentorjoin_cb_domain.Text.ToString();
            }
            
        }


        // 숫자만 입력 가능하게..(나이)
        private void mentorjoin_txt_age_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 숫자만 입력 가능하게..(연락처1)
        private void mentorjoin_txt_callnum1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 숫자만 입력 가능하게..(연락처2)
        private void mentorjoin_txt_callnum2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 숫자만 입력 가능하게..(연락처3)
        private void mentorjoin_txt_callnum3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 숫자만 입력 가능하게..(우편번호1)
        private void mentorjoin_txt_post1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 숫자만 입력 가능하게..(우편번호2)
        private void mentorjoin_txt_post2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 확인 버튼을 눌렀을 때
        private void mentorjoin_btn_ok_Click(object sender, EventArgs e)
        {
            if (mentorjoin_txt_name.Text == "")
            {
                MessageBox.Show("이름을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (sex.Equals(""))
            {
                MessageBox.Show("성별을 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentorjoin_txt_age.Text == "")
            {
                MessageBox.Show("나이를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (achievement_code.Equals(""))
            {
                MessageBox.Show("최종학력을 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentorjoin_txt_major.Text == "")
            {
                MessageBox.Show("전공을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (jobs_code.Equals(""))
            {
                MessageBox.Show("직업을 선택하세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentorjoin_cb_jobs.Text == "군인" && milrank_code.Equals(""))
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

            else if (mentorjoin_txt_callnum1.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentorjoin_txt_callnum2.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentorjoin_txt_callnum3.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mentorjoin_txt_email.Text == "")
            {
                MessageBox.Show("이메일 주소를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (domain.Equals("") && mentorjoin_txt_domain.Text == "")
            {
                MessageBox.Show("도메인을 선택 혹은 입력해주세요..", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                string tel = mentorjoin_txt_callnum1.Text + "-" + mentorjoin_txt_callnum2.Text + "-" + mentorjoin_txt_callnum3.Text;

                if (mentorjoin_cb_domain.Text == "직접입력")
                {
                    domain = mentorjoin_txt_domain.Text;
                }

                if (df.Mentorjoin_truncate(mentorjoin_txt_name.Text, tel, jobs_code))
                {
                    MessageBox.Show("이미 존재하는 정보입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Dictionary<string, string> dir = new Dictionary<string, string>();
                dir["mentor_name"] = mentorjoin_txt_name.Text;
                dir["mentor_educational"] = achievement_code;
                dir["mentor_soldierclass"] = milrank_code;
                dir["mentor_major"] = mentorjoin_txt_major.Text;
                dir["mentor_jobs"] = jobs_code;
                dir["mentor_company"] = mentorjoin_txt_company.Text;
                dir["mentor_depart"] = mentorjoin_txt_depart.Text;
                dir["mentor_location"] = loc_code;
                dir["mentor_part1"] = big_code;
                dir["mentor_part2"] = mid_code;
                dir["mentor_part3"] = sml_code;
                dir["mentor_tel"] = tel;
                dir["mentor_post1"] = mentorjoin_txt_post1.Text;
                dir["mentor_post2"] = mentorjoin_txt_post2.Text;
                dir["mentor_address"] = mentorjoin_txt_companyaddress.Text;
                dir["mentor_sex"] = sex;
                dir["mentor_age"] = mentorjoin_txt_age.Text;
                dir["mentor_email"] = mentorjoin_txt_email.Text;
                dir["mentor_domain"] = domain;
                dir["mentor_loginid"] = login_id;
                dir["mentor_joindate"] = DateTime.Now.ToString();

                // 나중에 인증서부분을 처리할때 사용될 부분.
                //dir["mentor_certification_certificate"] = mentorjoin_txt_name.Text;
                //dir["mentor_career_certificate"] = mentorjoin_txt_name.Text;
                //dir["mentor_educational_certificate"] = mentorjoin_txt_name.Text;

                df.Mentorjoin_complete(dir);

                bool bl = false;
                jform.join_complete(bl);

                MessageBox.Show("멘토 가입을 완료하였습니다.", "멘토가입 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
                jform.Close();
                
            }

        }


        // 취소버튼
        private void mentorjoin_btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

    }
}
