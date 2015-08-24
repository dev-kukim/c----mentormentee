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
 
    public partial class Mentee_main : Form
    {

        Data_func df = new Data_func();

        public string login_id = "";

        // 전역변수 선언 영역
        
        public static string big_code = "";           // 대분류 코드
        public static string mid_code = "";           // 중분류 코드
        public static string sml_code = "";           // 소분류 코드
       
        public static string big_name = "";           // 대분류 이름
        public static string mid_name = "";           // 중분류 이름
        public static string sml_name = "";           // 소분류 이름

        public static string sex = "";                // 성별

        public static string location_code = "";    // 지역코드
        public static string edu_code = "";       // 최종학력 코드
        public static string jobs_code = "";          // 직업코드
        public static string milrank_code = "";       // 군인일때 계급코드

        public static int minage = 0;                 // 최소나이
        public static int maxage = 100;                 // 최대나이

        public Mentee_main()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }


        // 초기 실행시..
        private void Mentee_main_Load(object sender, EventArgs e)
        {
            // 시계 관련 클래스
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            // 추천하는 멘토 그리드뷰 출력영역
            mentee_gv_recommand.DataSource = df.recommanded_Mentor(login_id);
            mentee_gv_recommand_Customizing();

            // 멘토 조건검색 그리드뷰 출력 영역
            mentee_gv_searchresults.DataSource = df.Mentee_getMentor();
            mentee_gv_searchresults_Customizing();

            // 대분류 그리드뷰 DB 연동 및 초기화 부분
            mentee_gv_big.DataSource = df.Mentee_sortL();
            mentee_gv_big.Columns[0].Visible = false;
            mentee_gv_big.Columns[1].HeaderText = "대분류";
            SetDoNotSort(mentee_gv_big);

            // 계급 콤보박스 DB 연동부분
            mentee_cb_militaryrank.DataSource = df.Mentee_militaryrank();
            mentee_cb_militaryrank.ValueMember = "gm_num";
            mentee_cb_militaryrank.DisplayMember = "gm_name";

            // 지역 콤보박스 DB 연동부분
            mentee_cb_location.DataSource = df.Mentee_location();
            mentee_cb_location.ValueMember = "gm_num";
            mentee_cb_location.DisplayMember = "gm_name";

            // 최종학력 콤보박스 DB 연동부분
            mentee_cb_achievement.DataSource = df.Mentee_educational();
            mentee_cb_achievement.ValueMember = "gm_num";
            mentee_cb_achievement.DisplayMember = "gm_name";

            // 직업 콤보박스 DB 연동부분
            mentee_cb_jobs.DataSource = df.Mentee_jobs();
            mentee_cb_jobs.ValueMember = "gm_num";
            mentee_cb_jobs.DisplayMember = "gm_name";

            // 기본 라벨 설정
            mentee_lbl_minage.Text = "0";
            mentee_lbl_maxage.Text = "99";


            // 성별 초기값
            mentee_cb_sex.SelectedIndex = 0;

        }


        // 시계관련 클래스
        void timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = login_id + "님 환영합니다.  현재시각 : " + DateTime.Now.ToString();
        }


        // 분류코드 정렬 비활성화
        private void SetDoNotSort(DataGridView dgv)
        {
            foreach (DataGridViewColumn i in dgv.Columns)
            {
                i.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


        // 검색결과 그리드뷰 커스터마이징
        private void mentee_gv_searchresults_Customizing()
        {
            mentee_gv_searchresults.Columns["mentor_name"].HeaderText = "멘토이름";
            mentee_gv_searchresults.Columns["mentor_educational"].Visible = false;                      // 학력 부분 미출력
            mentee_gv_searchresults.Columns["mentor_soldierclass"].Visible = false;                     // 계급(군인) 부분 미출력
            mentee_gv_searchresults.Columns["mentor_major"].HeaderText = "전공";
            mentee_gv_searchresults.Columns["mentor_jobs"].HeaderText = "직업";
            mentee_gv_searchresults.Columns["mentor_company"].HeaderText = "소속";
            mentee_gv_searchresults.Columns["mentor_depart"].Visible = false;                           // 분야 미출력
            mentee_gv_searchresults.Columns["mentor_location"].HeaderText = "지역";
            mentee_gv_searchresults.Columns["mentor_part1"].Visible = false;                            // 대분류 미출력
            mentee_gv_searchresults.Columns["mentor_part2"].Visible = false;                            // 중분류 미출력
            mentee_gv_searchresults.Columns["mentor_part3"].HeaderText = "분야";
            mentee_gv_searchresults.Columns["mentor_tel"].Visible = false;                              // 연락처 미출력
            mentee_gv_searchresults.Columns["mentor_post1"].Visible = false;                            // 우편번호(앞) 미출력
            mentee_gv_searchresults.Columns["mentor_post2"].Visible = false;                            // 우편번호(뒤) 미출력 
            mentee_gv_searchresults.Columns["mentor_address"].Visible = false;                          // 주소 미출력
            mentee_gv_searchresults.Columns["mentor_sex"].HeaderText = "성별";
            mentee_gv_searchresults.Columns["mentor_age"].HeaderText = "나이";
            mentee_gv_searchresults.Columns["mentor_email"].Visible = false;                            // 이메일(앞) 미출력
            mentee_gv_searchresults.Columns["mentor_domain"].Visible = false;                           // 이메일(뒤) 미출력
            mentee_gv_searchresults.Columns["mentor_loginid"].Visible = false;                          // 아이디 미출력
            mentee_gv_searchresults.Columns["mentor_joindate"].Visible = false;                         // 멘토 등록일 미출력
            mentee_gv_searchresults.Columns["mentor_certification_certificate"].Visible = false;        // 자격증 증명서 미출력
            mentee_gv_searchresults.Columns["mentor_career_certificate"].Visible = false;               // 경력 증명서 미출력
            mentee_gv_searchresults.Columns["mentor_educational_certificate"].Visible = false;          // 최종학력 증명서 미출력

         

            mentee_gv_searchresults.Columns["mentor_name"].DisplayIndex = 1;
            mentee_gv_searchresults.Columns["mentor_age"].DisplayIndex = 2;
            mentee_gv_searchresults.Columns["mentor_sex"].DisplayIndex = 3;
            mentee_gv_searchresults.Columns["mentor_major"].DisplayIndex = 4;
            mentee_gv_searchresults.Columns["mentor_company"].DisplayIndex = 5;
            mentee_gv_searchresults.Columns["mentor_location"].DisplayIndex = 6;
            mentee_gv_searchresults.Columns["mentor_part3"].DisplayIndex = 7;

            mentee_gv_searchresults.Columns["mentor_name"].Width = 80;
            mentee_gv_searchresults.Columns["mentor_age"].Width = 60;
            mentee_gv_searchresults.Columns["mentor_sex"].Width = 60;
           // mentee_gv_searchresults.Columns["지역"].Width = 60;
        }


        // 추천멘토 그리드뷰 커스터마이징
        private void mentee_gv_recommand_Customizing()
        {
            mentee_gv_recommand.Columns["mentor_name"].HeaderText = "멘토이름";
            mentee_gv_recommand.Columns["mentor_educational"].Visible = false;                      // 학력 부분 미출력
            mentee_gv_recommand.Columns["mentor_soldierclass"].Visible = false;                     // 계급(군인) 부분 미출력
            mentee_gv_recommand.Columns["mentor_major"].HeaderText = "전공";
            mentee_gv_recommand.Columns["mentor_jobs"].HeaderText = "직업";
            mentee_gv_recommand.Columns["mentor_company"].HeaderText = "소속";
            mentee_gv_recommand.Columns["mentor_depart"].Visible = false;                           // 분야 미출력
            mentee_gv_recommand.Columns["mentor_location"].HeaderText = "지역";
            mentee_gv_recommand.Columns["mentor_part1"].Visible = false;                            // 대분류 미출력
            mentee_gv_recommand.Columns["mentor_part2"].Visible = false;                            // 중분류 미출력
            mentee_gv_recommand.Columns["mentor_part3"].HeaderText = "분야";
            mentee_gv_recommand.Columns["mentor_tel"].Visible = false;                              // 연락처 미출력
            mentee_gv_recommand.Columns["mentor_post1"].Visible = false;                            // 우편번호(앞) 미출력
            mentee_gv_recommand.Columns["mentor_post2"].Visible = false;                            // 우편번호(뒤) 미출력 
            mentee_gv_recommand.Columns["mentor_address"].Visible = false;                          // 주소 미출력
            mentee_gv_recommand.Columns["mentor_sex"].HeaderText = "성별";
            mentee_gv_recommand.Columns["mentor_age"].HeaderText = "나이";
            mentee_gv_recommand.Columns["mentor_email"].Visible = false;                            // 이메일(앞) 미출력
            mentee_gv_recommand.Columns["mentor_domain"].Visible = false;                           // 이메일(뒤) 미출력
            mentee_gv_recommand.Columns["mentor_loginid"].Visible = false;                          // 아이디 미출력
            mentee_gv_recommand.Columns["mentor_joindate"].Visible = false;                         // 멘토 등록일 미출력
            mentee_gv_recommand.Columns["mentor_certification_certificate"].Visible = false;        // 자격증 증명서 미출력
            mentee_gv_recommand.Columns["mentor_career_certificate"].Visible = false;               // 경력 증명서 미출력
            mentee_gv_recommand.Columns["mentor_educational_certificate"].Visible = false;          // 최종학력 증명서 미출력

            mentee_gv_recommand.Columns["mentee_groupname"].Visible = false;
            mentee_gv_recommand.Columns["mentee_male"].Visible = false;
            mentee_gv_recommand.Columns["mentee_female"].Visible = false;
            mentee_gv_recommand.Columns["mentee_job"].Visible = false;
            mentee_gv_recommand.Columns["mentee_minage"].Visible = false;
            mentee_gv_recommand.Columns["mentee_maxage"].Visible = false;
            mentee_gv_recommand.Columns["mentee_location"].Visible = false;
            mentee_gv_recommand.Columns["mentee_company"].Visible = false;
            mentee_gv_recommand.Columns["mentee_tel"].Visible = false;
            mentee_gv_recommand.Columns["mentee_address"].Visible = false;
            mentee_gv_recommand.Columns["mentee_post1"].Visible = false;
            mentee_gv_recommand.Columns["mentee_post2"].Visible = false;
            mentee_gv_recommand.Columns["mentee_interestarea1"].Visible = false;
            mentee_gv_recommand.Columns["mentee_interestarea2"].Visible = false;
            mentee_gv_recommand.Columns["mentee_interestarea3"].Visible = false;
            mentee_gv_recommand.Columns["mentee_favorjob"].Visible = false;
            mentee_gv_recommand.Columns["mentee_loginid"].Visible = false;
            mentee_gv_recommand.Columns["mentee_joindate"].Visible = false;

            mentee_gv_recommand.Columns["mentor_name"].DisplayIndex = 1;
            mentee_gv_recommand.Columns["mentor_age"].DisplayIndex = 2;
            mentee_gv_recommand.Columns["mentor_sex"].DisplayIndex = 3;
            mentee_gv_recommand.Columns["mentor_major"].DisplayIndex = 4;
            mentee_gv_recommand.Columns["mentor_company"].DisplayIndex = 5;
            mentee_gv_recommand.Columns["mentor_location"].DisplayIndex = 6;
            mentee_gv_recommand.Columns["mentor_part3"].DisplayIndex = 7;

            mentee_gv_recommand.Columns["mentor_name"].Width = 80;
            mentee_gv_recommand.Columns["mentor_age"].Width = 60;
            mentee_gv_recommand.Columns["mentor_sex"].Width = 60;
            mentee_gv_recommand.Columns["mentor_location"].Width = 60;

        }


        // 검색 조건이 변경될때마다 실행되는것 (쿼리변경)
        private void Change_SearchCondition()
        {
            if (minage > maxage)
            {
                int temp = minage;
                minage = maxage;
                maxage = temp;
            }

            mentee_gv_searchresults.DataSource = df.Mentee_getMentor_search(edu_code, location_code, jobs_code, sex, sml_code, minage, maxage);
            mentee_gv_searchresults_Customizing();

        }
     


        // 지역 콤보박스를 선택했을 때
        private void mentee_cb_location_SelectedIndexChanged(object sender, EventArgs e)
        {
            location_code = mentee_cb_location.SelectedValue.ToString();
            mentee_lbl_location.Text = mentee_cb_location.Text.ToString();
            Change_SearchCondition();
        }


        // 계급 콤보박스 변경시
        private void mentee_cb_militaryrank_SelectedIndexChanged(object sender, EventArgs e)
        {
            milrank_code = mentee_cb_militaryrank.SelectedValue.ToString();                      // 계급코드 저장용 변수
            mentee_lbl_militaryrank.Text = mentee_cb_militaryrank.Text.ToString();               // 선택한 값에 따른 라벨출력
            Change_SearchCondition();
        }


        // 대분류 그리드뷰를 선택했을 때
        private void mentee_gv_big_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                big_code = mentee_gv_big.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                big_name = mentee_gv_big.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();       // 분야명 string
                mentee_lbl_part.Text = big_name + " -> ";

                mentee_gv_mid.DataSource = df.Mentee_SortM(big_code);
                mentee_gv_mid.Columns[0].Visible = false;
                mentee_gv_mid.Columns[1].Visible = false;
                mentee_gv_small.Columns.Clear();        // 소분류 선택부분 초기화
                mentee_gv_mid.ClearSelection();       // 중분류 선택부분 선택해제

                // 컬럼헤더 클릭시 정렬 못하게함.
                SetDoNotSort(mentee_gv_mid);

                // 헤더명 지정
                mentee_gv_mid.Columns[2].HeaderText = "중분류";

            }
        }


        // 중분류 그리드뷰를 선택했을 떄
        private void mentee_gv_mid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                mid_code = mentee_gv_mid.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();       // 분야코드 string
                mid_name = mentee_gv_mid.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();       // 분야명 string
                mentee_lbl_part.Text = big_name + " -> " + mid_name + " -> ";

                mentee_gv_small.DataSource = df.Mentee_SortS(mid_code);
                mentee_gv_small.Columns[0].Visible = false;
                mentee_gv_small.Columns[1].Visible = false;
                mentee_gv_small.ClearSelection();       // 소분류 선택부분 선택해제

                // 컬럼헤더 클릭시 정렬 못하게함.
                SetDoNotSort(mentee_gv_small);

                // 헤더명 지정
                mentee_gv_small.Columns[2].HeaderText = "소분류";

            }
        }


        // 소분류 그리드뷰를 선택했을 때
        private void mentee_gv_small_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                sml_code = mentee_gv_small.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();       // 분야코드 string
                sml_name = mentee_gv_small.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();       // 분야명 string
                mentee_lbl_part.Text = big_name + " -> " + mid_name + " -> " + sml_name;            // 라벨변경
                Change_SearchCondition();                                                       // 쿼리변경
            }
        }


        // 최종학력 콤보박스를 선택했을 때 + 쿼리변경
        private void mentee_cb_achievement_SelectedIndexChanged(object sender, EventArgs e)
        {
            edu_code = mentee_cb_achievement.SelectedValue.ToString();       // 최종학력 코드 저장용 변수
            mentee_lbl_achievement.Text = mentee_cb_achievement.Text.ToString();             // 선택한 값에 따른 라벨출력
           Change_SearchCondition();                        // 쿼리변경
        }


        // 직업 콤보박스를 선택했을 때 + 쿼리변경, 그리고 군인을 선택했을 경우에 숨겨진것 활성화 및 비활성화
        private void mentee_cb_jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            jobs_code = mentee_cb_jobs.SelectedValue.ToString();                // 직업코드 저장용 변수
            mentee_lbl_jobs.Text = mentee_cb_jobs.Text.ToString();              // 선택한 값에 따른 라벨출력

            if (mentee_cb_jobs.Text.Equals("군인"))
            {
                mentee_gbox_militaryrank_static.Visible = true;
                mentee_cb_militaryrank.Visible = true;
                mentee_lbl_militaryrank_static.Visible = true;
                mentee_lbl_militaryrank.Visible = true;
            }

            else
            {
                mentee_gbox_militaryrank_static.Visible = false;
                mentee_cb_militaryrank.Visible = false;
                mentee_lbl_militaryrank_static.Visible = false;
                mentee_lbl_militaryrank.Visible = false;
                milrank_code = "";       // 계급코드 초기화
                mentee_cb_militaryrank.SelectedIndex = 0;
            }

            Change_SearchCondition();    
        }


        // 성별 콤보박스를 선택했을 때 + 쿼리변경
        private void mentee_cb_sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sex = cbSex.SelectedIndex.ToString();         // 성별을 코드화할때 주석해제 및 밑에거 주석처리 필수         
            if (mentee_cb_sex.Text == "전체성별")
            {
                sex = "";
            }
            else
            {
                sex = mentee_cb_sex.Text.ToString();
            }

            mentee_lbl_sex.Text = mentee_cb_sex.Text.ToString();
           Change_SearchCondition();
        }


        // 코드값을 이름으로 (데이터가 바인딩 되면서 열이 추가될 때 - 외부모듈 연동)
        private void mentee_gv_searchresults_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            /*        
             * 만약에 멘토 리스트를 그냥 불러온다면 지역, 학력..등이 코드로 나올것이다. (당연히 db에는 코드로 저장되어 있으니까..)
             * 그러면 그 코드를 당연히 db랑 연동시켜서 코드가 무엇을 의미하는지 한글로 가져와야겠지...
             * 
             * 처음에는 잘 가져와진다.
             * 근데 컬럼헤더를 누르면 정렬이 되는데 이 부분에서 문제가 발생하게 된다.
             * 
             * 헤더를 누르면 정렬이 됨과 동시에 이 이벤트가 발생한다.
             * 처음 가져올때는 숫자(코드)로 찾을 수 있어서 한글을 가져올 수 있었지만
             * 2번째부터는 이미 한글로 가져온걸 또 다시 찾는다는게 웃긴거지...
             * 당연히 빈값 혹은 null이 출력될거고
             * 
             * 그래서 tryparse를 이용하였음.
             * 변환 성공시 int 변수에 변환된 값이 들어가지만
             * 변환 실패시 아무 변화가 없다. (초기값 그대로)
             */

            int jobscode_parse_result = 0;                              // 변환 성공여부 - 직업코드
            int locationcode_parse_result = 0;                          // 변환 성공여부 - 지역코드
            int part3code_parse_result = 0;                             // 변환 성공여부 - 분야코드


            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                DataGridViewRow row = mentee_gv_searchresults.Rows[index];                                                                          // 행 전체를 가져옴
                Int32.TryParse(row.Cells["mentor_jobs"].Value.ToString(), out jobscode_parse_result);                                       // 직업코드 숫자변환 가능여부 판별
                Int32.TryParse(row.Cells["mentor_location"].Value.ToString(), out locationcode_parse_result);                               // 지역코드 숫자변환 가능여부 판별
                Int32.TryParse(row.Cells["mentor_part3"].Value.ToString(), out part3code_parse_result);                                     // 분야코드 숫자변환 가능여부 판별

                if (jobscode_parse_result != 0 && locationcode_parse_result != 0 && part3code_parse_result != 0)                            // 변환 성공시 (초기값이 아닐경우)
                {
                    row.Cells["mentor_location"].Value = df.generalCode_GetName(row.Cells["mentor_location"].Value.ToString());        // 지역코드값 이름으로..
                    row.Cells["mentor_jobs"].Value = df.generalCode_GetName(row.Cells["mentor_jobs"].Value.ToString());                // 직업코드값 이름으로..
                    row.Cells["mentor_part3"].Value = df.sortCode_GetName(row.Cells["mentor_part3"].Value.ToString());                 // 분야코드값 이름으로..
                }
            }
        }


        // 조건 초기화 버튼 클릭시
        private void mentee_btn_reset_Click(object sender, EventArgs e)
        {
            mentee_cb_location.SelectedIndex = 0;       // 지역 선택부분 초기화
            mentee_cb_achievement.SelectedIndex = 0;            // 학력 선택부분 초기화
            mentee_cb_sex.SelectedIndex = 0;            // 성별 선택부분 초기화
            mentee_cb_jobs.SelectedIndex = 0;           // 직업 선택부분 초기화
            mentee_cb_militaryrank.SelectedIndex = 0;    // 계급 선택부분 초기화

            mentee_gv_big.ClearSelection();       // 대분류 선택부분 선택해제
            mentee_gv_mid.Columns.Clear();        // 중분류 선택부분 초기화
            mentee_gv_small.Columns.Clear();        // 소분류 선택부분 초기화

            mentee_txt_maxage.Text = "";                // 최대나이 입력부분 초기화
            mentee_txt_minage.Text = "";                // 최소나이 입력부분 초기화

            big_code = "";           // 대분류 코드
            mid_code = "";           // 중분류 코드
            sml_code = "";           // 소분류 코드

            big_name = "";           // 대분류 이름
            mid_name = "";           // 중분류 이름
            sml_name = "";           // 소분류 이름
            sex = "";                // 성별

            location_code = "";      // 지역코드
            edu_code = "";           // 최종학력 코드
            jobs_code = "";          // 직업코드
            milrank_code = "";       // 계급코드

            mentee_lbl_part.Text = "분야를 선택하세요.";
            mentee_lbl_maxage.Text = "99";
            mentee_lbl_minage.Text = "0";

            minage = 0;
            maxage = 100;
            Change_SearchCondition();
        }


        // 로그아웃
        private void mentee_menuitem_logout_Click(object sender, EventArgs e)
        {
            this.Hide();                    // 숨기고
            Login log = new Login();        // 로그인 모듈 선언
            log.Show();                     // 쇼
        }


        // 그냥 껐을때
        private void Mentee_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();             // 짜이찌엔~

            /* 로그인을 하고나면 창이 사라지는데 이건 close가 아니라 hide라는걸 꼭 생각. process leak 방지.. */
        }


        // 숫자만 입력가능하게...(최소나이)
        private void mentee_txt_minage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 숫자만 입력가능하게...(최대나이)
        private void mentee_txt_maxage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }


        // 멘토 검색결과 셀 더블클릭
        private void mentee_gv_searchresults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Mentee_requestMentoring mrequest = new Mentee_requestMentoring();
                mrequest.mentor_id = mentee_gv_searchresults.Rows[e.RowIndex].Cells["mentor_loginid"].Value.ToString().Trim();
                mrequest.mentee_id = login_id;
                mrequest.ShowDialog();
            }
        }


        // 추천멘토 셀 더블클릭
        private void mentee_gv_recommand_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Mentee_requestMentoring mrequest = new Mentee_requestMentoring();
                mrequest.mentor_id = mentee_gv_recommand.Rows[e.RowIndex].Cells["mentor_loginid"].Value.ToString().Trim();
                mrequest.mentee_id = login_id;
                mrequest.ShowDialog();
            }
        }

        // 메뉴 -> 종료
        private void mentee_menuitem_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 메뉴 -> 내 신청내역
        private void mentee_menu_myrequest_Click(object sender, EventArgs e)
        {
            Mentee_myrequest myrequest = new Mentee_myrequest();
            myrequest.login_id = login_id;
            myrequest.ShowDialog();
        }


        // 최소나이 값이 바뀔때 발생하는 이벤트 부분(라벨변경 및 쿼리변경)
        private void mentee_txt_minage_TextChanged(object sender, EventArgs e)
        {
            if (mentee_txt_minage.Text != "")
            {
                minage = Int32.Parse(mentee_txt_minage.Text);
                mentee_lbl_minage.Text = mentee_txt_minage.Text;
                Change_SearchCondition();
            }

            else
            {
                mentee_lbl_minage.Text = "0";
                minage = 0;
                Change_SearchCondition();
            }
        }


        // 최대나이 값이 바뀔때 발생하는 이벤트 부분(라벨변경 및 쿼리변경)
        private void mentee_txt_maxage_TextChanged(object sender, EventArgs e)
        {
            if (mentee_txt_maxage.Text != "")
            {
                maxage = Int32.Parse(mentee_txt_maxage.Text);
                mentee_lbl_maxage.Text = mentee_txt_maxage.Text;
               Change_SearchCondition();             
            }

            else
            {
                mentee_lbl_maxage.Text = "99";
                maxage = 0;
                Change_SearchCondition();
            }
        }

        // 메뉴 - 내정보 - 정보수정
        private void mentee_menu_edit_Click(object sender, EventArgs e)
        {
            if (df.Mentee_isMentoring(login_id))
            {
                MessageBox.Show("진행중 혹은 승인 대기중인 멘토링이 있는 관계로\n개인정보 수정이 불가능합니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Mentee_edit mentee_edit = new Mentee_edit();
                mentee_edit.id = login_id;
                mentee_edit.isAdmin = false;
                mentee_edit.Show();
            }
        }

        // 비번변경
        private void mentee_menu_changePW_Click(object sender, EventArgs e)
        {
            ChangePW pw = new ChangePW();
            pw.login_id = login_id;
            pw.ShowDialog();
        }   

    }
}
