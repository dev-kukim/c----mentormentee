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
    public partial class Mentee_join : Form
    {

        Data_func df = new Data_func();

        private Join_form jform;
        private string login_id;

        public static string jobs_code = "";         // 직업 코드
        public static string favorjobs_code = "";    // 멘토 선호직업 코드
        public static string big_code = "";          // 분야(대) 코드
        public static string mid_code = "";          // 분야(중) 코드
        public static string sml_code = "";          // 분야(소) 코드
        public static string loc_code = "";          // 지역코드



        public Mentee_join(Join_form joinform, string LoginID)
        {
            InitializeComponent();
            this.jform = joinform;
            this.login_id = LoginID;
        }


        // 초기 폼 로드부분
        private void Mentee_join_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            // 직업 콤보박스 DB 연동
            menteejoin_cb_jobs.DataSource = df.Join_jobs();
            menteejoin_cb_jobs.ValueMember = "gm_num";
            menteejoin_cb_jobs.DisplayMember = "gm_name";

            // 선호직업 콤보박스 DB 연동
            menteejoin_cb_favorjobs.DataSource = df.Join_jobs();
            menteejoin_cb_favorjobs.ValueMember = "gm_num";
            menteejoin_cb_favorjobs.DisplayMember = "gm_name";

            // 지역 콤보박스 DB 연동
            menteejoin_cb_location.DataSource = df.Join_location();
            menteejoin_cb_location.ValueMember = "gm_num";
            menteejoin_cb_location.DisplayMember = "gm_name";

            // 분야(대) 콤보박스 DB 연동
            menteejoin_cb_big.DataSource = df.Join_sortL();
            menteejoin_cb_big.ValueMember = "b_num";
            menteejoin_cb_big.DisplayMember = "b_name";

        }


        // 시계 -_-
        void timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "현재시각 : " + DateTime.Now.ToString();
        }


        // 분야 콤보박스(대)를 선택했을 때
        private void menteejoin_cb_big_SelectedIndexChanged(object sender, EventArgs e)
        {
            big_code = menteejoin_cb_big.SelectedValue.ToString();
            menteejoin_cb_mid.DataSource = df.Join_SortM(big_code);
            menteejoin_cb_mid.ValueMember = "m_num";
            menteejoin_cb_mid.DisplayMember = "m_name";
        }

        // 분야 콤보박스(중)을 선택했을 때
        private void menteejoin_cb_mid_SelectedIndexChanged(object sender, EventArgs e)
        {
            mid_code = menteejoin_cb_mid.SelectedValue.ToString();                      // 분야(중) 저장용 변수
            menteejoin_cb_small.DataSource = df.Join_SortS(mid_code);
            menteejoin_cb_small.ValueMember = "s_num";
            menteejoin_cb_small.DisplayMember = "s_name";
        }


        // 분야 콤보박스(소)를 선택했을 때
        private void menteejoin_cb_small_SelectedIndexChanged(object sender, EventArgs e)
        {
            sml_code = menteejoin_cb_small.SelectedValue.ToString();
        }

        // 선호직업 콤보박스를 선택했을 때
        private void menteejoin_cb_favorjobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            favorjobs_code = menteejoin_cb_favorjobs.SelectedValue.ToString();
        }

        // 직업 콤보박스를 선택했을 때
        private void menteejoin_cb_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            jobs_code = menteejoin_cb_jobs.SelectedValue.ToString();
        }


        // 지역 콤보박스를 선택했을 떄
        private void menteejoin_cb_location_SelectedIndexChanged(object sender, EventArgs e)
        {
            loc_code = menteejoin_cb_location.SelectedValue.ToString();
        }


        // 취소버튼
        private void menteejoin_btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 확인버튼(가입버튼)을 눌렀을 때
        private void menteejoin_btn_ok_Click(object sender, EventArgs e)
        {
            if (menteejoin_txt_groupname.Text == "")
            {
                MessageBox.Show("그룹명을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if ((menteejoin_txt_groupmale.Text == "" || menteejoin_txt_groupfemale.Text == "") || (menteejoin_txt_groupmale.Text == "0" && menteejoin_txt_groupfemale.Text == "0"))
            {
                MessageBox.Show("인원수를 정확히 입력해주세요.\n대상 인원이 없을경우 0을 입력하시면 됩니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if ((menteejoin_txt_minage.Text == "" || menteejoin_txt_maxage.Text == "") || (menteejoin_txt_minage.Text == "0" && menteejoin_txt_maxage.Text == "0") || (Int32.Parse(menteejoin_txt_minage.Text) > Int32.Parse(menteejoin_txt_maxage.Text)))
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

            else if (menteejoin_txt_company.Text == "")
            {
                MessageBox.Show("소속기관을 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteejoin_txt_callnum1.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteejoin_txt_callnum2.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteejoin_txt_callnum3.Text == "")
            {
                MessageBox.Show("전화번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteejoin_txt_address.Text == "")
            {
                MessageBox.Show("주소를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteejoin_txt_post1.Text == "")
            {
                MessageBox.Show("우편번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (menteejoin_txt_post2.Text == "")
            {
                MessageBox.Show("우편번호를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (favorjobs_code == "")
            {
                MessageBox.Show("선호 멘토직업을 선택하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                string tel = menteejoin_txt_callnum1.Text + "-" + menteejoin_txt_callnum2.Text + "-" + menteejoin_txt_callnum3.Text;

                if (df.Menteejoin_truncate(menteejoin_txt_groupname.Text))
                {
                    MessageBox.Show("이미 존재하는 정보입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Dictionary<string, string> dir = new Dictionary<string, string>();
                dir["mentee_groupname"] = menteejoin_txt_groupname.Text;
                dir["mentee_male"] = menteejoin_txt_groupmale.Text;
                dir["mentee_female"] = menteejoin_txt_groupfemale.Text;
                dir["mentee_job"] = jobs_code;
                dir["mentee_minage"] = menteejoin_txt_minage.Text;
                dir["mentee_maxage"] = menteejoin_txt_maxage.Text;
                dir["mentee_location"] = loc_code;
                dir["mentee_company"] = menteejoin_txt_company.Text;
                dir["mentee_tel"] = tel;
                dir["mentee_address"] = menteejoin_txt_address.Text;
                dir["mentee_post1"] = menteejoin_txt_post1.Text;
                dir["mentee_post2"] = menteejoin_txt_post2.Text;
                dir["mentee_interestarea1"] = big_code;
                dir["mentee_interestarea2"] = mid_code;
                dir["mentee_interestarea3"] = sml_code;
                dir["mentee_favorjob"] = favorjobs_code;
                dir["mentee_loginid"] = login_id;
                dir["mentee_joindate"] = DateTime.Now.ToString();

                df.Menteejoin_complete(dir);

                bool bl = true;
                jform.join_complete(bl);

                MessageBox.Show("멘티 가입을 완료하였습니다.", "멘티가입 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
                jform.Close();
                /*
                try
                {
                    conn.Open();

                    string tel = menteejoin_txt_callnum1.Text + "-" + menteejoin_txt_callnum2.Text + "-" + menteejoin_txt_callnum3.Text;

                    // 중복체크 영역
                    string sql_truncate = "SELECT * FROM Mentee WHERE Mentee_groupname = @nm";
                    command = new SqlCommand(sql_truncate, conn);
                    command.Parameters.Add("@nm", SqlDbType.VarChar);

                    command.Parameters["@nm"].Value = menteejoin_txt_groupname.Text;

                    sr = command.ExecuteReader();

                    if (sr.HasRows)
                    {
                        MessageBox.Show("이미 존재하는 그룹명입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    else
                    {
                        // 멘티회원 추가 영역
                        string sql = "SELECT * FROM Mentee";
                        da = new SqlDataAdapter(sql, connectionString);
                        command = new SqlCommand(sql, conn);
                        cb = new SqlCommandBuilder(da);
                        da.Fill(ds, "Mentee");
                        dt = ds.Tables["Mentee"];
                        dr = dt.NewRow();

                        dr["mentee_groupname"] = menteejoin_txt_groupname.Text;
                        dr["mentee_male"] = menteejoin_txt_groupmale.Text;
                        dr["mentee_female"] = menteejoin_txt_groupfemale.Text;
                        dr["mentee_job"] = jobs_code;
                        dr["mentee_minage"] = menteejoin_txt_minage.Text;
                        dr["mentee_maxage"] = menteejoin_txt_maxage.Text;
                        dr["mentee_location"] = loc_code;
                        dr["mentee_company"] = menteejoin_txt_company.Text;
                        dr["mentee_tel"] = tel;
                        dr["mentee_address"] = menteejoin_txt_address.Text;
                        dr["mentee_post1"] = menteejoin_txt_post1.Text;
                        dr["mentee_post2"] = menteejoin_txt_post2.Text;
                        dr["mentee_interestarea1"] = big_code;
                        dr["mentee_interestarea2"] = mid_code;
                        dr["mentee_interestarea3"] = sml_code;
                        dr["mentee_favorjob"] = favorjobs_code;
                        dr["mentee_loginid"] = login_id;
                        dr["mentee_joindate"] = DateTime.Now.ToString();

                        dt.Rows.Add(dr);
                        da.Update(ds, "Mentee");
                        ds.AcceptChanges();

                        bool bl = true;
                        jform.join_complete(bl);

                        MessageBox.Show("멘티 가입을 완료하였습니다.", "멘티가입 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                        jform.Close();

                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }

                finally
                {
                    conn.Close();
                }
                 */
            }
        }

        private void menteejoin_txt_groupmale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteejoin_txt_groupfemale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteejoin_txt_minage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteejoin_txt_maxage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteejoin_txt_callnum1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteejoin_txt_callnum2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteejoin_txt_callnum3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteejoin_txt_post1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void menteejoin_txt_post2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        

    }
}
