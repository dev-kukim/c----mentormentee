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
    public partial class Mentee_edit : Form
    {
        Data_func df = new Data_func();
        public string id = "";
        public bool isAdmin = false;

        private Admin_main am;
        private int index_menteelist;


        public static string jobs_code = "";         // 직업 코드
        public static string favorjobs_code = "";    // 멘토 선호직업 코드
        public static string big_code = "";          // 분야(대) 코드
        public static string mid_code = "";          // 분야(중) 코드
        public static string sml_code = "";          // 분야(소) 코드
        public static string loc_code = "";          // 지역코드



        public Mentee_edit()
        {
            InitializeComponent();
        }

        public Mentee_edit(Admin_main admin_main, int index)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            am = admin_main;
            index_menteelist = index;
        }

        private void Mentee_edit_Load(object sender, EventArgs e)
        {
            if (isAdmin)
            {
                admin_menu_manage_mentee.Visible = true;
                if (df.Mentee_isMentoring(id))
                {
                    MessageBox.Show("진행중인 멘토링이 있는 회원은 개인정보 수정이 불가능합니다.\n(비밀번호 초기화만 가능)", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    menteeedit_txt_groupname.Enabled = false;
                    menteeedit_txt_minage.Enabled = false;
                    menteeedit_txt_maxage.Enabled = false;
                    menteeedit_txt_groupmale.Enabled = false;
                    menteeedit_txt_groupfemale.Enabled = false;
                    menteeedit_cb_jobs.Enabled = false;
                    menteeedit_cb_big.Enabled = false;
                    menteeedit_cb_mid.Enabled = false;
                    menteeedit_cb_small.Enabled = false;
                    menteeedit_cb_location.Enabled = false;
                    menteeedit_txt_callnum1.Enabled = false;
                    menteeedit_txt_callnum2.Enabled = false;
                    menteeedit_txt_callnum3.Enabled = false;
                    menteeedit_txt_company.Enabled = false;
                    menteeedit_txt_address.Enabled = false;
                    menteeedit_txt_post1.Enabled = false;
                    menteeedit_txt_post2.Enabled = false;
                    menteeedit_cb_favorjobs.Enabled = false;
                    menteeedit_btn_ok.Enabled = false;
                }
            }

            else
            {
                admin_menu_manage_mentee.Visible = false;
            }

            // 직업 콤보박스 DB 연동
            menteeedit_cb_jobs.DataSource = df.Join_jobs();
            menteeedit_cb_jobs.ValueMember = "gm_num";
            menteeedit_cb_jobs.DisplayMember = "gm_name";

            // 선호직업 콤보박스 DB 연동
            menteeedit_cb_favorjobs.DataSource = df.Join_jobs();
            menteeedit_cb_favorjobs.ValueMember = "gm_num";
            menteeedit_cb_favorjobs.DisplayMember = "gm_name";

            // 지역 콤보박스 DB 연동
            menteeedit_cb_location.DataSource = df.Join_location();
            menteeedit_cb_location.ValueMember = "gm_num";
            menteeedit_cb_location.DisplayMember = "gm_name";

            // 분야(대) 콤보박스 DB 연동
            menteeedit_cb_big.DataSource = df.Join_sortL();
            menteeedit_cb_big.ValueMember = "b_num";
            menteeedit_cb_big.DisplayMember = "b_name";


            Dictionary<string, string> drr = df.Request_getMenteeInfo(id);
            menteeedit_txt_groupname.Text = drr["mentee_groupname"];
            menteeedit_txt_groupmale.Text = drr["mentee_male"];
            menteeedit_txt_groupfemale.Text = drr["mentee_female"];
            menteeedit_txt_minage.Text = drr["mentee_minage"];
            menteeedit_txt_maxage.Text = drr["mentee_maxage"];

            // 중분류 콤보박스 DB 연동
            menteeedit_cb_mid.DataSource = df.Join_SortM(drr["mentee_interestarea1"]);
            menteeedit_cb_mid.ValueMember = "m_num";
            menteeedit_cb_mid.DisplayMember = "m_name";

            // 소분류 콤보박스 DB 연동
            menteeedit_cb_small.DataSource = df.Join_SortS(drr["mentee_interestarea2"]);
            menteeedit_cb_small.ValueMember = "s_num";
            menteeedit_cb_small.DisplayMember = "s_name";


            menteeedit_cb_big.SelectedValue = drr["mentee_interestarea1"];
            big_code = drr["mentee_interestarea1"];
            menteeedit_cb_mid.SelectedValue = drr["mentee_interestarea2"];
            mid_code = drr["mentee_interestarea2"];
            menteeedit_cb_small.SelectedValue = drr["mentee_interestarea3"];
            sml_code = drr["mentee_interestarea3"];
            menteeedit_cb_location.SelectedValue = drr["mentee_location"];
            loc_code = drr["mentee_location"];
            menteeedit_cb_jobs.SelectedValue = drr["mentee_job"];
            jobs_code = drr["mentee_job"];
            menteeedit_txt_company.Text = drr["mentee_company"];

            string[] callnum = drr["mentee_tel"].Split('-');
            menteeedit_txt_callnum1.Text = callnum[0];
            menteeedit_txt_callnum2.Text = callnum[1];
            menteeedit_txt_callnum3.Text = callnum[2];

            menteeedit_txt_address.Text = drr["mentee_address"];
            menteeedit_txt_post1.Text = drr["mentee_post1"];
            menteeedit_txt_post2.Text = drr["mentee_post2"];

            menteeedit_cb_favorjobs.SelectedValue = drr["mentee_favorjob"];
            favorjobs_code = drr["mentee_favorjob"];

        }


        // 분야 콤보박스(대)를 선택했을 때
        private void menteeedit_cb_big_SelectedIndexChanged(object sender, EventArgs e)
        {
            big_code = menteeedit_cb_big.SelectedValue.ToString();
            menteeedit_cb_mid.DataSource = df.Join_SortM(big_code);
            menteeedit_cb_mid.ValueMember = "m_num";
            menteeedit_cb_mid.DisplayMember = "m_name";
        }


        // 분야 콤보박스(중)를 선택했을 떄
        private void menteeedit_cb_mid_SelectedIndexChanged(object sender, EventArgs e)
        {
            mid_code = menteeedit_cb_mid.SelectedValue.ToString();                      // 분야(중) 저장용 변수
            menteeedit_cb_small.DataSource = df.Join_SortS(mid_code);
            menteeedit_cb_small.ValueMember = "s_num";
            menteeedit_cb_small.DisplayMember = "s_name";
        }


        // 분야 콤보박스(소)를 선택했을 때
        private void menteeedit_cb_small_SelectedIndexChanged(object sender, EventArgs e)
        {
            sml_code = menteeedit_cb_small.SelectedValue.ToString();
        }


        // 지역 콤보박스를 선택했을 때
        private void menteeedit_cb_location_SelectedIndexChanged(object sender, EventArgs e)
        {
            loc_code = menteeedit_cb_location.SelectedValue.ToString();
        }


        // 직업 콤보박스를 선택했을 때
        private void menteeedit_cb_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            jobs_code = menteeedit_cb_jobs.SelectedValue.ToString();
        }


        // 선호직업 콤보박스를 선택했을 때
        private void menteeedit_cb_favorjobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            favorjobs_code = menteeedit_cb_favorjobs.SelectedValue.ToString();
        }


        
        private void menteeedit_txt_groupmale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteeedit_txt_groupfemale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteeedit_txt_minage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteeedit_txt_maxage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteeedit_txt_callnum1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteeedit_txt_callnum2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteeedit_txt_callnum3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteeedit_txt_post1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteeedit_txt_post2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 확인버튼
        private void menteeedit_btn_ok_Click(object sender, EventArgs e)
        {
            if (menteeedit_txt_groupname.Text == "")
            {
                MessageBox.Show("그룹명을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if ((menteeedit_txt_groupmale.Text == "" || menteeedit_txt_groupfemale.Text == ""))
            {
                MessageBox.Show("인원수를 정확히 입력해주세요.\n대상 인원이 없을경우 0을 입력하시면 됩니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if ((menteeedit_txt_minage.Text == "" || menteeedit_txt_maxage.Text == "") || (menteeedit_txt_minage.Text == "0" && menteeedit_txt_maxage.Text == "0") || (Int32.Parse(menteeedit_txt_minage.Text) > Int32.Parse(menteeedit_txt_maxage.Text)))
            {
                MessageBox.Show("나이를 정확히 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (big_code.Equals(""))
            {
                MessageBox.Show("관심분야를 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (mid_code.Equals(""))
            {
                MessageBox.Show("관심분야를 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (sml_code.Equals(""))
            {
                MessageBox.Show("관심분야를 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (loc_code == "")
            {
                MessageBox.Show("지역을 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (jobs_code == "")
            {
                MessageBox.Show("직업을 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteeedit_txt_company.Text == "")
            {
                MessageBox.Show("소속기관을 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteeedit_txt_callnum1.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteeedit_txt_callnum2.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteeedit_txt_callnum3.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteeedit_txt_address.Text == "")
            {
                MessageBox.Show("주소를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteeedit_txt_post1.Text == "")
            {
                MessageBox.Show("우편번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteeedit_txt_post2.Text == "")
            {
                MessageBox.Show("우편번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (favorjobs_code == "")
            {
                MessageBox.Show("선호 멘토직업을 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                string tel = menteeedit_txt_callnum1.Text + "-" + menteeedit_txt_callnum2.Text + "-" + menteeedit_txt_callnum3.Text;

                if (df.Menteejoin_truncate(menteeedit_txt_groupname.Text))
                {
                    MessageBox.Show("이미 존재하는 정보입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Dictionary<string, string> dir = new Dictionary<string, string>();
                dir["mentee_groupname"] = menteeedit_txt_groupname.Text;
                dir["mentee_male"] = menteeedit_txt_groupmale.Text;
                dir["mentee_female"] = menteeedit_txt_groupfemale.Text;
                dir["mentee_job"] = jobs_code;
                dir["mentee_minage"] = menteeedit_txt_minage.Text;
                dir["mentee_maxage"] = menteeedit_txt_maxage.Text;
                dir["mentee_location"] = loc_code;
                dir["mentee_company"] = menteeedit_txt_company.Text;
                dir["mentee_tel"] = tel;
                dir["mentee_address"] = menteeedit_txt_address.Text;
                dir["mentee_post1"] = menteeedit_txt_post1.Text;
                dir["mentee_post2"] = menteeedit_txt_post2.Text;
                dir["mentee_interestarea1"] = big_code;
                dir["mentee_interestarea2"] = mid_code;
                dir["mentee_interestarea3"] = sml_code;
                dir["mentee_favorjob"] = favorjobs_code;
                dir["mentee_loginid"] = id;

                am.admin_gv_menteelist.DataSource = df.Menteeedit_complete(dir, id);
                am.admin_gv_menteelist.Rows[index_menteelist].Selected = true;
                am.admin_gv_menteelist.FirstDisplayedScrollingRowIndex = am.admin_gv_menteelist.Rows[index_menteelist].Index;
                MessageBox.Show("멘티정보 수정을 완료하였습니다.", "멘티정보 수정 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        // 취소버튼
        private void menteeedit_btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 멘티 비밀번호 초기화
        private void admin_menu_mentee_resetpw_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당 멘티의 비밀번호를 0000 으로 초기화 하시겠습니까?", "비밀번호 초기화", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                df.admin_resetPW(id);
                MessageBox.Show("해당 멘티의 비밀번호를 0000 으로 초기화 하였습니다.", "비밀번호 초기화 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
