using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentorMentee_Project
{
    class Data_func
    {

        public static string connectionString = "Server=.\\;" + "DataBase=mm2;" +
                    "Integrated Security=SSPI;" + "Connect Timeout=5";
        SqlConnection conn = new SqlConnection(connectionString);
        SqlConnection conn2 = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();
        SqlCommand command2 = new SqlCommand();
        SqlDataReader reader = null;
        SqlDataReader reader2 = null;
        SqlDataAdapter da = null;       // sql data adapter
        DataSet ds = new DataSet();              // dataset
        DataTable dt = null;            // datatable
        DataRow dr = null;             // datarow


        /* login */

        public string login_check(string member_id, string member_pw)
        {
            string login_category = "";
            string query = "select * from member where id= '" + member_id + "' and pw = '" + member_pw + "'";

            try
            {
                command.Connection = conn;
                command.CommandText = query;
                conn.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    switch (reader["category"].ToString().Trim())
                    {
                        case "a":
                            login_category = "mentee";
                            break;
                        case "b":
                            login_category = "mentor";
                            break;
                        case "c":
                            login_category = "admin";
                            break;
                        default:
                            break;
                    }
                }

                else
                {
                    MessageBox.Show("일치하는 계정이 없습니다.\n아이디와 비밀번호를 다시한번 확인해 주시기 바랍니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            return login_category;
        }




        /* join - common */

        // 가입시 아이디 중복체크
        public bool Join_truncate(string id)
        {
            try
            {
                conn.Open();
                string query = "SELECT id FROM member WHERE id = @id";
                command = new SqlCommand(query, conn);
                command.Parameters.Add("@id", SqlDbType.VarChar);
                command.Parameters["@id"].Value = id;
                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return reader.HasRows;
        }

        // 가입완료(DB insert - id, pw 부분만)
        public void Join_complete(string id, string pw, bool flag)
        {
            string sql = "SELECT * FROM member";
            da = new SqlDataAdapter(sql, connectionString);
            command = new SqlCommand(sql, conn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds, "member");
            dt = ds.Tables["member"];
            dr = dt.NewRow();
            dr["id"] = id;
            dr["pw"] = pw;

            if (flag == true)
            {
                dr["category"] = "a";       // 멘티
            }
            else if (flag == false)
            {
                dr["category"] = "b";       // 멘토
            }

            dt.Rows.Add(dr);
            da.Update(ds, "member");
            ds.AcceptChanges();
        }

        // 대분류 그리드뷰
        public DataTable Join_sortL()
        {
            string query = "select * from sort_code_L order by b_num asc";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("b_num", typeof(string));
            dt.Columns.Add("b_name", typeof(string));
            dr = dt.NewRow();
            dr["b_num"] = "";
            dr["b_name"] = "선택하세요";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }

        // 중분류 그리드뷰
        public DataTable Join_SortM(string big_code)
        {
            string query = "select * from sort_code_M where m_parent = '" + big_code + "' order by m_num asc";

            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("m_num", typeof(string));
            dt.Columns.Add("m_name", typeof(string));
            dr = dt.NewRow();
            dr["m_num"] = "";
            dr["m_name"] = "선택하세요";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }

        // 소분류 그리드뷰
        public DataTable Join_SortS(string mid_code)
        {
            string query = "select * from sort_code_S where s_parent = '" + mid_code + "' order by s_num asc";

            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("s_num", typeof(string));
            dt.Columns.Add("s_name", typeof(string));
            dr = dt.NewRow();
            dr["s_num"] = "";
            dr["s_name"] = "선택하세요";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }

        // 지역 콤보박스
        public DataTable Join_location()
        {
            string query = "select * from general_code_M where gm_parent = '03' order by gm_num asc";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("gm_num", typeof(string));
            dt.Columns.Add("gm_name", typeof(string));
            dr = dt.NewRow();
            dr["gm_num"] = "";
            dr["gm_name"] = "선택하세요";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }

        // 직업(멘티부분 선호직업) 콤보박스
        public DataTable Join_jobs()
        {
            string query = "select * from general_code_M where gm_parent = '04' order by gm_num asc";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("gm_num", typeof(string));
            dt.Columns.Add("gm_name", typeof(string));
            dr = dt.NewRow();
            dr["gm_num"] = "";
            dr["gm_name"] = "선택하세요";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }




        /* join - mentor */

        // 직업이 군인인 사람들을 위한 계급 콤보박스
        public DataTable Mentorjoin_militaryrank()
        {
            string query = "select * from general_code_M where gm_parent = '02' order by gm_num asc";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("gm_num", typeof(string));
            dt.Columns.Add("gm_name", typeof(string));
            dr = dt.NewRow();
            dr["gm_num"] = "";
            dr["gm_name"] = "선택하세요";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }

        // 최종학력 콤보박스
        public DataTable Mentorjoin_educational()
        {
            string query = "select * from general_code_M where gm_parent = '01' order by gm_num asc";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("gm_num", typeof(string));
            dt.Columns.Add("gm_name", typeof(string));
            dr = dt.NewRow();
            dr["gm_num"] = "";
            dr["gm_name"] = "선택하세요";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }
        
        // 가입된 멘토가 있는지 중복체크 - 멘토 정보기반
        public bool Mentorjoin_truncate(string name, string tel, string jobs)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM mentor WHERE mentor_name=@nm AND mentor_tel=@ph AND mentor_jobs=@jobs";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@nm", SqlDbType.VarChar);
                command.Parameters.Add("@ph", SqlDbType.VarChar);
                command.Parameters.Add("@jobs", SqlDbType.VarChar);

                command.Parameters["@nm"].Value = name;
                command.Parameters["@ph"].Value = tel;
                command.Parameters["@jobs"].Value = jobs;
                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }

        // 멘토 가입완료
        public void Mentorjoin_complete(Dictionary<string, string> dir)
        {
            string sql = "SELECT * FROM Mentor";
            da = new SqlDataAdapter(sql, connectionString);
            command = new SqlCommand(sql, conn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds, "Mentor");
            dt = ds.Tables["Mentor"];
            dr = dt.NewRow();

            dr["mentor_name"] = dir["mentor_name"];
            dr["mentor_educational"] = dir["mentor_educational"];
            dr["mentor_soldierclass"] = dir["mentor_soldierclass"];
            dr["mentor_major"] = dir["mentor_major"];
            dr["mentor_jobs"] = dir["mentor_jobs"];
            dr["mentor_company"] = dir["mentor_company"];
            dr["mentor_depart"] = dir["mentor_depart"];
            dr["mentor_location"] = dir["mentor_location"];
            dr["mentor_part1"] = dir["mentor_part1"];
            dr["mentor_part2"] = dir["mentor_part2"];
            dr["mentor_part3"] = dir["mentor_part3"];
            dr["mentor_tel"] = dir["mentor_tel"];
            dr["mentor_post1"] = dir["mentor_post1"];
            dr["mentor_post2"] = dir["mentor_post2"];
            dr["mentor_address"] = dir["mentor_address"];
            dr["mentor_sex"] = dir["mentor_sex"];
            dr["mentor_age"] = dir["mentor_age"];
            dr["mentor_email"] = dir["mentor_email"];
            dr["mentor_domain"] = dir["mentor_domain"];
            dr["mentor_loginid"] = dir["mentor_loginid"];
            dr["mentor_joindate"] = dir["mentor_joindate"];

            // 나중에 인증서부분을 처리할때 사용될 부분.
            //dr["mentor_certification_certificate"] = dir["mentor_certification_certificate"];
            //dr["mentor_career_certificate"] = dir["mentor_career_certificate"];
            //dr["mentor_educational_certificate"] = dir["mentor_educational_certificate"];

            dt.Rows.Add(dr);
            da.Update(ds, "Mentor");
            ds.AcceptChanges();

        }



        /* edit - mentor */

        // 멘토정보 수정 완료
        public DataTable Mentoredit_complete(Dictionary<string, string> dir, string id)
        {
            try
            {
                conn.Open();
                string sql_update = @"UPDATE mentor SET mentor_name = @mentor_name, mentor_educational = @mentor_educational, mentor_major = @mentor_major,
                    mentor_jobs = @mentor_jobs, mentor_soldierclass = @mentor_soldierclass, mentor_company = @mentor_company, mentor_depart = @mentor_depart,
                    mentor_location = @mentor_location, mentor_part1 = @mentor_part1, mentor_part2 = @mentor_part2,
                    mentor_part3 = @mentor_part3, mentor_tel = @mentor_tel, mentor_post1 = @mentor_post1,
                    mentor_post2 = @mentor_post2, mentor_address = @mentor_address, mentor_sex = @mentor_sex,
                    mentor_age = @mentor_age, mentor_email = @mentor_email, mentor_domain = @mentor_domain where mentor_loginid = @id";
                command = new SqlCommand(sql_update, conn);
                command.Parameters.AddWithValue("@mentor_name", dir["mentor_name"]);
                command.Parameters.AddWithValue("@mentor_educational", dir["mentor_educational"]);
                command.Parameters.AddWithValue("@mentor_major", dir["mentor_major"]);
                command.Parameters.AddWithValue("@mentor_jobs", dir["mentor_jobs"]);
                command.Parameters.AddWithValue("@mentor_soldierclass", dir["mentor_soldierclass"]);
                command.Parameters.AddWithValue("@mentor_company", dir["mentor_company"]);
                command.Parameters.AddWithValue("@mentor_depart", dir["mentor_depart"]);
                command.Parameters.AddWithValue("@mentor_location", dir["mentor_location"]);
                command.Parameters.AddWithValue("@mentor_part1", dir["mentor_part1"]);
                command.Parameters.AddWithValue("@mentor_part2", dir["mentor_part2"]);
                command.Parameters.AddWithValue("@mentor_part3", dir["mentor_part3"]);
                command.Parameters.AddWithValue("@mentor_tel", dir["mentor_tel"]);
                command.Parameters.AddWithValue("@mentor_post1", dir["mentor_post1"]);
                command.Parameters.AddWithValue("@mentor_post2", dir["mentor_post2"]);
                command.Parameters.AddWithValue("@mentor_address", dir["mentor_address"]);
                command.Parameters.AddWithValue("@mentor_sex", dir["mentor_sex"]);
                command.Parameters.AddWithValue("@mentor_age", dir["mentor_age"]);
                command.Parameters.AddWithValue("@mentor_email", dir["mentor_email"]);
                command.Parameters.AddWithValue("@mentor_domain", dir["mentor_domain"]);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                string query = "SELECT * FROM mentor";
                da = new SqlDataAdapter(query, connectionString);
                dt = new DataTable();
                da.Fill(dt);

                return dt;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dt;

        }


        // 진행중인 멘토링이 있는지..
        public bool Mentor_isMentoring(string id)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM request_history WHERE R_mentor_id = @id AND (R_matching <> 'fin' OR R_matching <> 'cancel(2)' OR R_matching <> 'cancel(1)' OR R_matching <> 'cancel(admin)')";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@id", SqlDbType.VarChar);
                command.Parameters["@id"].Value = id;

                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }


        // 멘토/멘티 비밀번호 리셋(관리자용)
        public void admin_resetPW(string id)
        {
            try
            {
                conn.Open();
                string sql = "UPDATE member SET pw = '0000' WHERE id = @id";
                command = new SqlCommand(sql, conn);
                command.Parameters.Add("@id", SqlDbType.VarChar);
                command.Parameters["@id"].Value = id;
                command.ExecuteNonQuery();
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }
        }








        /* edit - mentee */

        // 진행중인 멘토링이 있는지 ..
        public bool Mentee_isMentoring(string id)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM request_history WHERE R_mentee_id = @id AND (R_matching <> 'fin' OR R_matching <> 'cancel(2)' OR R_matching <> 'cancel(1)' OR R_matching <> 'cancel(admin)')";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@id", SqlDbType.VarChar);
                command.Parameters["@id"].Value = id;

                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }


        // 멘티정보 수정 완료
        public DataTable Menteeedit_complete(Dictionary<string, string> dir, string id)
        {
            try
            {
                conn.Open();
                string sql_update = @"UPDATE mentee SET mentee_groupname = @mentee_groupname, mentee_male = @mentee_male, mentee_female = @mentee_female, mentee_job = @mentee_job
                    , mentee_minage = @mentee_minage, mentee_maxage = @mentee_maxage, mentee_location = @mentee_location, mentee_company = @mentee_company 
                    , mentee_address = @mentee_address, mentee_post1 = @mentee_post1, mentee_post2 = @mentee_post2, mentee_interestarea1 = @mentee_interestarea1
                    , mentee_interestarea2 = @mentee_interestarea2 ,mentee_interestarea3 = @mentee_interestarea3, 
                      mentee_favorjob = @mentee_favorjob where mentee_loginid = @id";
                command = new SqlCommand(sql_update, conn);
                command.Parameters.AddWithValue("@mentee_groupname", dir["mentee_groupname"]);
                command.Parameters.AddWithValue("@mentee_male", dir["mentee_male"]);
                command.Parameters.AddWithValue("@mentee_female", dir["mentee_female"]);
                command.Parameters.AddWithValue("@mentee_job", dir["mentee_job"]);
                command.Parameters.AddWithValue("@mentee_minage", dir["mentee_minage"]);
                command.Parameters.AddWithValue("@mentee_maxage", dir["mentee_maxage"]);
                command.Parameters.AddWithValue("@mentee_location", dir["mentee_location"]);
                command.Parameters.AddWithValue("@mentee_company", dir["mentee_company"]);
                command.Parameters.AddWithValue("@mentee_address", dir["mentee_address"]);
                command.Parameters.AddWithValue("@mentee_post1", dir["mentee_post1"]);
                command.Parameters.AddWithValue("@mentee_post2", dir["mentee_post2"]);
                command.Parameters.AddWithValue("@mentee_interestarea1", dir["mentee_interestarea1"]);
                command.Parameters.AddWithValue("@mentee_interestarea2", dir["mentee_interestarea2"]);
                command.Parameters.AddWithValue("@mentee_interestarea3", dir["mentee_interestarea3"]);
                command.Parameters.AddWithValue("@mentee_favorjob", dir["mentee_favorjob"]);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                string query = "SELECT * FROM mentee";
                da = new SqlDataAdapter(query, connectionString);
                dt = new DataTable();
                da.Fill(dt);

                return dt;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dt;

        }










        /* join - mentee */

        // 가입된 멘티가 있는지 중복체크 - 멘토 정보기반
        public bool Menteejoin_truncate(string groupname)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM Mentee WHERE Mentee_groupname = @nm";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@nm", SqlDbType.VarChar);
                command.Parameters["@nm"].Value = groupname;
                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }

        // 멘티 가입완료
        public void Menteejoin_complete(Dictionary<string, string> dir)
        {
            string sql = "SELECT * FROM Mentee";
            da = new SqlDataAdapter(sql, connectionString);
            command = new SqlCommand(sql, conn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds, "Mentee");
            dt = ds.Tables["Mentee"];
            dr = dt.NewRow();

            dr["mentee_groupname"] = dir["mentee_groupname"];
            dr["mentee_male"] = dir["mentee_male"];
            dr["mentee_female"] = dir["mentee_female"];
            dr["mentee_job"] = dir["mentee_job"];
            dr["mentee_minage"] = dir["mentee_minage"];
            dr["mentee_maxage"] = dir["mentee_maxage"];
            dr["mentee_location"] = dir["mentee_location"];
            dr["mentee_company"] = dir["mentee_company"];
            dr["mentee_tel"] = dir["mentee_tel"];
            dr["mentee_address"] = dir["mentee_address"];
            dr["mentee_post1"] = dir["mentee_post1"];
            dr["mentee_post2"] = dir["mentee_post2"];
            dr["mentee_interestarea1"] = dir["mentee_interestarea1"];
            dr["mentee_interestarea2"] = dir["mentee_interestarea2"];
            dr["mentee_interestarea3"] = dir["mentee_interestarea3"];
            dr["mentee_favorjob"] = dir["mentee_favorjob"];
            dr["mentee_loginid"] = dir["mentee_loginid"];
            dr["mentee_joindate"] = dir["mentee_joindate"];

            dt.Rows.Add(dr);
            da.Update(ds, "Mentee");
            ds.AcceptChanges();

        }





        /* mentor */

        // 멘토 DataTable(초기)
        public DataTable Mentor_request(string id)
        {
            string query = "SELECT * FROM request_history WHERE R_mentor_id = '" + id + "'";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("성별(남/여)");
            dt.Columns.Add("나이");
            dr = dt.NewRow();
            da.Fill(dt);
            return dt;
        }


        // 자기한테 신청된 내역 상황 카운팅
        public Dictionary<string, string> Mentor_statusCount(string id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            try
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM request_history WHERE R_mentor_id = '" + id + "' AND R_matching = 'matching'";
                command = new SqlCommand(query, conn);
                dic["processing"] = command.ExecuteScalar().ToString();


                // 신청현황 - 승인대기
                query = "SELECT COUNT(*) FROM request_history WHERE R_mentor_id = '" + id + "' AND (R_matching = 'agree(admin)' OR R_matching = 'waiting')";
                command = new SqlCommand(query, conn);
                dic["waiting"] = command.ExecuteScalar().ToString();


                // 신청현황 - 취소
                query = "SELECT COUNT(*) FROM request_history WHERE R_mentor_id = '" + id + "' AND (R_matching = 'cancel' OR R_matching = 'cancel(admin)' OR R_matching = 'cancel(1)' OR R_matching = 'cancel(2)' OR R_matching = 'cancel(12)')";
                command = new SqlCommand(query, conn);
                dic["canceled"] = command.ExecuteScalar().ToString();


                // 신청현황 - 종료
                query = "SELECT COUNT(*) FROM request_history WHERE R_mentor_id = '" + id + "' AND R_matching = 'fin'";
                command = new SqlCommand(query, conn);
                dic["finished"] = command.ExecuteScalar().ToString();

                return dic;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dic;
        }


        // 상태변경
        public DataTable Mentor_changeStatus(string groupname, string requestedtime, string id, int flag)
        {
            try
            {
                conn.Open();
                string sql_updatestatus = "";

                switch (flag)
                {
                    case 0:
                        sql_updatestatus = "UPDATE request_history SET R_matching = @status, R_matching_time = @matchingtime WHERE R_mentee_groupname = '" + groupname +
                                 "' AND R_request_time = '" + requestedtime + "'";
                        command = new SqlCommand(sql_updatestatus, conn);
                        command.Parameters.Add("@status", SqlDbType.VarChar);
                        command.Parameters["@status"].Value = "matching";
                        command.Parameters.Add("@matchingtime", SqlDbType.VarChar);
                        command.Parameters["@matchingtime"].Value = DateTime.Now.ToString();
                        break;
                    case 1:
                        sql_updatestatus = "UPDATE request_history SET R_matching = @status, R_cancel_time = @canceltime WHERE R_mentee_groupname = '" + groupname +
                                 "' AND R_request_time = '" + requestedtime + "'";
                        command = new SqlCommand(sql_updatestatus, conn);
                        command.Parameters.Add("@status", SqlDbType.VarChar);
                        command.Parameters["@status"].Value = "cancel(1)";
                        command.Parameters.Add("@canceltime", SqlDbType.VarChar);
                        command.Parameters["@canceltime"].Value = DateTime.Now.ToString();
                        break;
                    case 2:
                        sql_updatestatus = "UPDATE request_history SET R_matching = @status, R_complete_time = @completetime WHERE R_mentee_groupname = '" + groupname +
                                 "' AND R_request_time = '" + requestedtime + "'";
                        command = new SqlCommand(sql_updatestatus, conn);
                        command.Parameters.Add("@status", SqlDbType.VarChar);
                        command.Parameters["@status"].Value = "fin";
                        command.Parameters.Add("@completetime", SqlDbType.VarChar);
                        command.Parameters["@completetime"].Value = DateTime.Now.ToString();
                        break;
                    case 3:
                        sql_updatestatus = "DELETE FROM request_history WHERE R_mentee_groupname = '" + groupname +
                                 "' AND R_request_time = '" + requestedtime + "'";
                        command = new SqlCommand(sql_updatestatus, conn);
                        break;
                    default:
                        break;
                }

                command.ExecuteNonQuery();

                string query = "SELECT * FROM request_history WHERE R_mentor_id = '" + id +"'";
                da = new SqlDataAdapter(query, connectionString);
                dt = new DataTable();
                dt.Columns.Add("성별(남/여)");
                dt.Columns.Add("나이");
                dr = dt.NewRow();
                da.Fill(dt);

                return dt;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dt;
        }

        // 멘토정보 가져오기(정보수정..등)
        public Dictionary<string, string> Mentor_info(string mentorid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string sql_mentor = "SELECT * FROM Mentor WHERE mentor_loginid = @mentorid";
            try
            {
                command = new SqlCommand(sql_mentor, conn);
                conn.Open();
                command.Parameters.Add("@mentorid", SqlDbType.VarChar);
                command.Parameters["@mentorid"].Value = mentorid;
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    dic["mentor_name"] = reader["mentor_name"].ToString().Trim();
                    dic["mentor_educational"] = reader["mentor_educational"].ToString().Trim();
                    dic["mentor_soldierclass"] = reader["mentor_soldierclass"].ToString().Trim();
                    dic["mentor_major"] = reader["mentor_major"].ToString().Trim();
                    dic["mentor_jobs"] = reader["mentor_jobs"].ToString().Trim();
                    dic["mentor_company"] = reader["mentor_company"].ToString().Trim();
                    dic["mentor_depart"] = reader["mentor_depart"].ToString().Trim();
                    dic["mentor_location"] = reader["mentor_location"].ToString().Trim();
                    dic["mentor_part1"] = reader["mentor_part1"].ToString().Trim();
                    dic["mentor_part2"] = reader["mentor_part2"].ToString().Trim();
                    dic["mentor_part3"] = reader["mentor_part3"].ToString().Trim();
                    dic["mentor_tel"] = reader["mentor_tel"].ToString().Trim();
                    dic["mentor_post1"] = reader["mentor_post1"].ToString().Trim();
                    dic["mentor_post2"] = reader["mentor_post2"].ToString().Trim();
                    dic["mentor_address"] = reader["mentor_address"].ToString().Trim();
                    dic["mentor_sex"] = reader["mentor_sex"].ToString().Trim();
                    dic["mentor_age"] = reader["mentor_age"].ToString().Trim();
                    dic["mentor_email"] = reader["mentor_email"].ToString().Trim();
                    dic["mentor_domain"] = reader["mentor_domain"].ToString().Trim();
                }

                return dic;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dic;
        }




        /* mentee */

        // 추천하는 멘토 Dataset
        public DataTable recommanded_Mentor(string id)
        {
            string query = @"SELECT * FROM Mentor INNER JOIN Mentee ON Mentor.mentor_jobs = Mentee.mentee_favorjob 
                                 AND (Mentor.mentor_part3 = Mentee.mentee_interestarea3 
                                 OR Mentor.mentor_location = Mentee.mentee_location) WHERE Mentee.mentee_loginid = '" + id + "'";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            da.Fill(dt);

            return dt;

        }

        // 멘토 Dataset(초기)
        public DataTable Mentee_getMentor()
        {
            string query = "select * from mentor";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // 멘토 Dataset(조건검색)
        public DataTable Mentee_getMentor_search(string edu, string loc, string jobs, string sex, string sml, int min, int max)
        {
            string query = "select * from Mentor where mentor_educational like '%" + edu + "%' and mentor_location like '%" + loc + "%' and mentor_jobs like '%" + jobs + "%' and mentor_sex like '%" + sex + "%' and mentor_part3 like '%" + sml + "%' and mentor_age between " + min + " and " + max + "";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // 대분류 그리드뷰
        public DataTable Mentee_sortL()
        {
            string query = "select * from sort_code_L order by b_num asc";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // 계급 콤보박스
        public DataTable Mentee_militaryrank()
        {
            string query = "select * from general_code_M where gm_parent = '02' order by gm_num asc";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("gm_num", typeof(string));
            dt.Columns.Add("gm_name", typeof(string));
            dr = dt.NewRow();
            dr["gm_num"] = "";
            dr["gm_name"] = "전체계급";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }

        // 지역 콤보박스
        public DataTable Mentee_location()
        {
            string query = "select * from general_code_M where gm_parent = '03' order by gm_num asc";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("gm_num", typeof(string));
            dt.Columns.Add("gm_name", typeof(string));
            dr = dt.NewRow();
            dr["gm_num"] = "";
            dr["gm_name"] = "전체지역";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }

        // 최종학력 콤보박스
        public DataTable Mentee_educational()
        {
            string query = "select * from general_code_M where gm_parent = '01' order by gm_num asc";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("gm_num", typeof(string));
            dt.Columns.Add("gm_name", typeof(string));
            dr = dt.NewRow();
            dr["gm_num"] = "";
            dr["gm_name"] = "전체학력";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }

        // 직업 콤보박스
        public DataTable Mentee_jobs()
        {
            string query = "select * from general_code_M where gm_parent = '04' order by gm_num asc";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("gm_num", typeof(string));
            dt.Columns.Add("gm_name", typeof(string));
            dr = dt.NewRow();
            dr["gm_num"] = "";
            dr["gm_name"] = "전체직업";
            dt.Rows.Add(dr);
            da.Fill(dt);
            return dt;
        }

        // 중분류 그리드뷰
        public DataTable Mentee_SortM(string big_code)
        {
            string query = "select * from sort_code_M where m_parent = '" + big_code + "' order by m_num asc";

            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // 소분류 그리드뷰
        public DataTable Mentee_SortS(string mid_code)
        {
            string query = "select * from sort_code_S where s_parent = '" + mid_code + "' order by s_num asc";

            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }



        

        /* mentee - myrequest */

        // 초기 로딩부분 - 로그인한 아이디 기반
        public DataTable Myrequest(string id)
        {
            string query = "SELECT * FROM request_history WHERE R_mentee_id = '" + id + "'";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // 전채신청내역 - label(DB)
        public Dictionary<string, string> Myrequest_info(string groupname, string requesttime)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string query = @"SELECT * FROM request_history WHERE R_mentee_groupname = '" + groupname +
                                 "' AND R_request_time = '" + requesttime + "'";

            try
            {
                command2 = new SqlCommand(query, conn2);
                conn2.Open();
                reader2 = command2.ExecuteReader();

                if (reader2.Read())
                {
                    // info
                    dic["R_mentor_name"] = reader2["R_mentor_name"].ToString().Trim();
                    dic["R_mentor_age"] = reader2["R_mentor_age"].ToString().Trim() + "세";
                    dic["R_mentor_sex"] = reader2["R_mentor_sex"].ToString().Trim();
                    dic["R_mentor_major"] = reader2["R_mentor_major"].ToString().Trim();
                    dic["R_mentor_location"] = generalCode_GetName(reader2["R_mentor_location"].ToString().Trim());
                    dic["R_mentor_jobs"] = generalCode_GetName(reader2["R_mentor_jobs"].ToString().Trim());
                    dic["R_mentor_tel"] = reader2["R_mentor_tel"].ToString().Trim();
                    dic["R_mentor_part3"] = sortCode_GetName(reader2["R_mentor_part3"].ToString().Trim());
                    dic["R_mentor_company"] = reader2["R_mentor_company"].ToString().Trim();
                    dic["R_clock_time"] = reader2["R_clock_time"].ToString().Trim();
                    dic["R_mentor_email"] = reader2["R_mentor_email"].ToString().Trim() + "@" + reader2["R_mentor_domain"].ToString().Trim();
                    dic["R_time"] = reader2["R_start_time"].ToString().Trim() + " ~ " + reader2["R_finish_time"].ToString().Trim();
                    dic["R_request_time"] = reader2["R_request_time"].ToString().Trim();

                    // status
                    dic["R_matching"] = reader2["R_matching"].ToString().Trim();

                    // canceled time
                    dic["R_cancel_time"] = reader2["R_cancel_time"].ToString().Trim();

                    // groupname
                    dic["R_mentee_groupname"] = reader2["R_mentee_groupname"].ToString().Trim();

                    // finished time
                    dic["R_finish_time"] = reader2["R_finish_time"].ToString().Trim();

                    // admin approved time
                    dic["R_admin_agree_time"] = reader2["R_admin_agree_time"].ToString().Trim();

                    // request time
                    dic["R_request_time"] = reader2["R_request_time"].ToString().Trim();

                    // start time
                    dic["R_start_time"] = reader2["R_start_time"].ToString().Trim();

                    // finish time
                    dic["R_finish_time"] = reader2["R_finish_time"].ToString().Trim();

                    // clock
                    dic["R_clock_time"] = reader2["R_clock_time"].ToString().Trim();

                }

                return dic;


            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn2.Close();
            }

            return dic;

        }

        // 전체신청내역 - 상태변경
        public DataTable Myrequest_modifyStatus(string groupname, string requestedtime, int flag, string id)
        {
            try
            {
                conn.Open();
                string sql_updatestatus = "";

                switch (flag)
                {
                    case 0:
                        sql_updatestatus = "UPDATE request_history SET R_matching = @status, R_cancel_time = @matchingtime WHERE R_mentee_groupname = '" + groupname +
                                 "' AND R_request_time = '" + requestedtime + "'";
                        command = new SqlCommand(sql_updatestatus, conn);
                        command.Parameters.Add("@status", SqlDbType.VarChar);
                        command.Parameters["@status"].Value = "cancel(2)";
                        command.Parameters.Add("@matchingtime", SqlDbType.VarChar);
                        command.Parameters["@matchingtime"].Value = DateTime.Now.ToString();
                        break;
                    case 1:
                        sql_updatestatus = "DELETE FROM request_history WHERE R_mentee_groupname = '" + groupname +
                                 "' AND R_request_time = '" + requestedtime + "'";
                        command = new SqlCommand(sql_updatestatus, conn);
                        break;
                    case 2:
                        sql_updatestatus = "UPDATE request_history SET R_matching = @status, R_matching_time = @matchingtime WHERE R_mentee_groupname = '" + groupname +
                                 "' AND R_request_time = '" + requestedtime + "'";
                        command = new SqlCommand(sql_updatestatus, conn);
                        command.Parameters.Add("@status", SqlDbType.VarChar);
                        command.Parameters["@status"].Value = "fin";
                        command.Parameters.Add("@matchingtime", SqlDbType.VarChar);
                        command.Parameters["@matchingtime"].Value = DateTime.Now.ToString();
                        break;
                    default:
                        break;
                }

                command.ExecuteNonQuery();
                string query = "SELECT * FROM request_history WHERE R_mentee_id = '" + id + "'";
                da = new SqlDataAdapter(query, connectionString);
                dt = new DataTable();
                da.Fill(dt);

                return dt;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dt;
        }




        /* mentee - request */

        // 초기 로딩부분 - 멘토 정보 출력
        public Dictionary<string, string> Request_info(string id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string query = "SELECT * FROM Mentor WHERE mentor_loginid = '" + id + "'";

            try
            {
                command2 = new SqlCommand(query, conn2);
                conn2.Open();
                reader2 = command2.ExecuteReader();

                if (reader2.Read())
                {
                    dic["mentor_name"] = reader2["mentor_name"].ToString().Trim();
                    dic["mentor_sex"] = reader2["mentor_sex"].ToString().Trim();
                    dic["mentor_age"] = reader2["mentor_age"].ToString().Trim();
                    dic["mentor_major"] = reader2["mentor_major"].ToString().Trim();
                    dic["mentor_jobs"] = generalCode_GetName(reader2["mentor_jobs"].ToString().Trim());
                    dic["mentor_location"] = generalCode_GetName(reader2["mentor_location"].ToString().Trim());
                    dic["mentor_company"] = reader2["mentor_company"].ToString().Trim();
                    dic["mentor_part3"] = sortCode_GetName(reader2["mentor_part3"].ToString().Trim());
                    dic["mentor_email"] = reader2["mentor_email"].ToString().Trim() + "@" + reader2["mentor_domain"].ToString().Trim();
                    dic["mentor_tel"] = reader2["mentor_tel"].ToString().Trim();
                }

                return dic;
          
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn2.Close();
            }

            return dic;
        }
    
        // 멘토정보 가져옴.
        public Dictionary<string, string> Request_getMentorInfo(string mentorid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string sql_mentor = "SELECT * FROM Mentor WHERE mentor_loginid = @mentorid";

            try
            {
                command = new SqlCommand(sql_mentor, conn);
                conn.Open();
                command.Parameters.Add("@mentorid", SqlDbType.VarChar);
                command.Parameters["@mentorid"].Value = mentorid;
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    dic["mentor_name"] = reader["mentor_name"].ToString().Trim();
                    dic["mentor_educational"] = reader["mentor_educational"].ToString().Trim();
                    dic["mentor_soldierclass"] = reader["mentor_soldierclass"].ToString().Trim();
                    dic["mentor_major"] = reader["mentor_major"].ToString().Trim();
                    dic["mentor_jobs"] = reader["mentor_jobs"].ToString().Trim();
                    dic["mentor_company"] = reader["mentor_company"].ToString().Trim();
                    dic["mentor_depart"] = reader["mentor_depart"].ToString().Trim();
                    dic["mentor_location"] = reader["mentor_location"].ToString().Trim();
                    dic["mentor_part1"] = reader["mentor_part1"].ToString().Trim();
                    dic["mentor_part2"] = reader["mentor_part2"].ToString().Trim();
                    dic["mentor_part3"] = reader["mentor_part3"].ToString().Trim();
                    dic["mentor_tel"] = reader["mentor_tel"].ToString().Trim();
                    dic["mentor_post1"] = reader["mentor_post1"].ToString().Trim();
                    dic["mentor_post2"] = reader["mentor_post2"].ToString().Trim();
                    dic["mentor_address"] = reader["mentor_address"].ToString().Trim();
                    dic["mentor_sex"] = reader["mentor_sex"].ToString().Trim();
                    dic["mentor_age"] = reader["mentor_age"].ToString().Trim();
                    dic["mentor_email"] = reader["mentor_email"].ToString().Trim();
                    dic["mentor_domain"] = reader["mentor_domain"].ToString().Trim();
                }

                return dic;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dic;

        }

        // 멘티정보 가져옴.
        public Dictionary<string, string> Request_getMenteeInfo(string menteeid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string sql_mentee = "SELECT * FROM Mentee WHERE mentee_loginid = @menteeid";

            try
            {
                command = new SqlCommand(sql_mentee, conn);
                conn.Open();
                command.Parameters.Add("@menteeid", SqlDbType.VarChar);
                command.Parameters["@menteeid"].Value = menteeid;
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    dic["mentee_groupname"] = reader["mentee_groupname"].ToString().Trim();
                    dic["mentee_location"] = reader["mentee_location"].ToString().Trim();
                    dic["mentee_minage"] = reader["mentee_minage"].ToString().Trim();
                    dic["mentee_maxage"] = reader["mentee_maxage"].ToString().Trim();
                    dic["mentee_male"] = reader["mentee_male"].ToString().Trim();
                    dic["mentee_female"] = reader["mentee_female"].ToString().Trim();
                    dic["mentee_job"] = reader["mentee_job"].ToString().Trim();
                    dic["mentee_company"] = reader["mentee_company"].ToString().Trim();
                    dic["mentee_tel"] = reader["mentee_tel"].ToString().Trim();
                    dic["mentee_address"] = reader["mentee_address"].ToString().Trim();
                    dic["mentee_post1"] = reader["mentee_post1"].ToString().Trim();
                    dic["mentee_post2"] = reader["mentee_post2"].ToString().Trim();
                    dic["mentee_interestarea1"] = reader["mentee_interestarea1"].ToString().Trim();
                    dic["mentee_interestarea2"] = reader["mentee_interestarea2"].ToString().Trim();
                    dic["mentee_interestarea3"] = reader["mentee_interestarea3"].ToString().Trim();
                    dic["mentee_favorjob"] = reader["mentee_favorjob"].ToString().Trim();
                }

                return dic;
            }


            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dic;
        }

        // 신청시 중복체크를 하는 영역
        public bool Request_truncate(string mentorid, string menteeid)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM request_history WHERE R_mentor_id = @mentorid AND R_mentee_id = @menteeid AND (R_matching <> 'fin' AND R_matching <> 'cancel' AND R_matching <> 'cancel(1)' AND R_matching <> 'cancel(2)' AND R_matching <> 'cancel(12)')";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@mentorid", SqlDbType.VarChar);
                command.Parameters.Add("@menteeid", SqlDbType.VarChar);
                command.Parameters["@mentorid"].Value = mentorid;
                command.Parameters["@menteeid"].Value = menteeid;

                reader = command.ExecuteReader();
                return reader.HasRows;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;

        }
        
        // 중복된게 없을때 DB Insert
        public void Request_complete(Dictionary<string, string> dir)
        {
            string sql = "SELECT * FROM request_history";
            da = new SqlDataAdapter(sql, connectionString);
            command = new SqlCommand(sql, conn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds, "request_history");
            dt = ds.Tables["request_history"];
            dr = dt.NewRow();
            dr["R_matching"] = "waiting";
            dr["R_mentee_groupname"] = dir["R_mentee_groupname"];
            dr["R_mentee_location"] = dir["R_mentee_location"];
            dr["R_mentee_lowage"] = dir["R_mentee_lowage"];
            dr["R_mentee_highage"] = dir["R_mentee_highage"];
            dr["R_mentee_male"] = dir["R_mentee_male"];
            dr["R_mentee_female"] = dir["R_mentee_female"];
            dr["R_mentee_job"] = dir["R_mentee_job"];
            dr["R_mentee_company"] = dir["R_mentee_company"];
            dr["R_mentee_tel"] = dir["R_mentee_tel"];
            dr["R_mentee_address"] = dir["R_mentee_address"];
            dr["R_mentee_post1"] = dir["R_mentee_post1"];
            dr["R_mentee_post2"] = dir["R_mentee_post2"];
            dr["R_mentee_interestarea1"] = dir["R_mentee_interestarea1"];
            dr["R_mentee_interestarea2"] = dir["R_mentee_interestarea2"];
            dr["R_mentee_interestarea3"] = dir["R_mentee_interestarea3"];
            dr["R_mentee_favorjob"] = dir["R_mentee_favorjob"];
            dr["R_mentor_name"] = dir["R_mentor_name"];
            dr["R_mentor_educational"] = dir["R_mentor_educational"];
            dr["R_mentor_soldierclass"] = dir["R_mentor_soldierclass"];
            dr["R_mentor_major"] = dir["R_mentor_major"];
            dr["R_mentor_jobs"] = dir["R_mentor_jobs"];
            dr["R_mentor_company"] = dir["R_mentor_company"];
            dr["R_mentor_depart"] = dir["R_mentor_depart"];
            dr["R_mentor_location"] = dir["R_mentor_location"];
            dr["R_mentor_part1"] = dir["R_mentor_part1"];
            dr["R_mentor_part2"] = dir["R_mentor_part2"];
            dr["R_mentor_part3"] = dir["R_mentor_part3"];
            dr["R_mentor_tel"] = dir["R_mentor_tel"];
            dr["R_mentor_post1"] = dir["R_mentor_post1"];
            dr["R_mentor_post2"] = dir["R_mentor_post2"];
            dr["R_mentor_address"] = dir["R_mentor_address"];
            dr["R_mentor_sex"] = dir["R_mentor_sex"];
            dr["R_mentor_age"] = dir["R_mentor_age"];
            dr["R_mentor_email"] = dir["R_mentor_email"];
            dr["R_mentor_domain"] = dir["R_mentor_domain"];
            dr["R_mentee_id"] = dir["R_mentee_id"];
            dr["R_mentor_id"] = dir["R_mentor_id"];
            dr["R_request_time"] = dir["R_request_time"];                        // 요청시간
            dr["R_cancel_time"] = dir["R_cancel_time"];                                             // 취소시간
            dr["R_start_time"] = dir["R_start_time"];             // 시작일
            dr["R_finish_time"] = dir["R_finish_time"];                 // 종료일
            dr["R_matching_time"] = dir["R_matching_time"];                                             // 최종승인 - 진행시간
            dr["R_complete_time"] = dir["R_complete_time"];                                            // 완료시간
            dr["R_admin_agree_time"] = dir["R_admin_agree_time"];                                      // 관리자 승인 시간
            dr["R_clock_time"] = dir["R_clock_time"]; 

            dt.Rows.Add(dr);
            da.Update(ds, "request_history");
            ds.AcceptChanges();

        }





        /* admin page */

        // 전체 신청내역 Dataset
        public DataTable getRequestHistory()
        {
            string query = "SELECT * FROM request_history";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("멘토링 희망기간");
            dr = dt.NewRow();
            da.Fill(dt);
            return dt;
        }


        // 전체신청내역 - 상태변경
        public DataTable modifystatus(string requested_time, string groupname, int flag)
        {
            try
            {
                conn.Open();
                string sql_updatestatus = "";

                switch (flag)
                {
                    case 0:
                        sql_updatestatus = "UPDATE request_history SET R_matching = @status, R_admin_agree_time = @adminagreetime WHERE R_mentee_groupname = '" + groupname +
                             "' AND R_request_time = '" + requested_time + "'";
                        command = new SqlCommand(sql_updatestatus, conn);
                        command.Parameters.Add("@status", SqlDbType.VarChar);
                        command.Parameters["@status"].Value = "agree(admin)";
                        command.Parameters.Add("@adminagreetime", SqlDbType.VarChar);
                        command.Parameters["@adminagreetime"].Value = DateTime.Now.ToString();
                        break;
                    case 1:
                        sql_updatestatus = "UPDATE request_history SET R_matching = @status, R_cancel_time = @admincanceltime WHERE R_mentee_groupname = '" + groupname +
                                "' AND R_request_time = '" + requested_time + "'";
                        command = new SqlCommand(sql_updatestatus, conn);
                        command.Parameters.Add("@status", SqlDbType.VarChar);
                        command.Parameters["@status"].Value = "cancel(admin)";
                        command.Parameters.Add("@admincanceltime", SqlDbType.VarChar);
                        command.Parameters["@admincanceltime"].Value = DateTime.Now.ToString();
                        break;
                    case 2:
                        sql_updatestatus = "UPDATE request_history SET R_matching = @status, R_complete_time = @admincompletetime WHERE R_mentee_groupname = '" + groupname +
                                 "' AND R_request_time = '" + requested_time + "'";
                        command = new SqlCommand(sql_updatestatus, conn);
                        command.Parameters.Add("@status", SqlDbType.VarChar);
                        command.Parameters["@status"].Value = "fin";
                        command.Parameters.Add("@admincompletetime", SqlDbType.VarChar);
                        command.Parameters["@admincompletetime"].Value = DateTime.Now.ToString();
                        break;
                    default:
                        break;
                }

                command.ExecuteNonQuery();

                string query = "SELECT * FROM request_history";
                da = new SqlDataAdapter(query, connectionString);
                dt = new DataTable();
                dt.Columns.Add("멘토링 희망기간");
                dr = dt.NewRow();
                da.Fill(dt);

                return dt;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dt;
        }


        // 전체신청내역 - label (DB)
        public Dictionary<string, string> drr(string a, string b)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string query = @"SELECT * FROM request_history WHERE R_mentee_groupname = '" + a +
                                 "' AND R_request_time = '" + b + "'";

            try
            {
                command2 = new SqlCommand(query, conn2);
                conn2.Open();
                reader2 = command2.ExecuteReader();

                if (reader2.Read())
                {
                    // mentee
                    dic["R_mentee_groupname"] = reader2["R_mentee_groupname"].ToString().Trim();
                    dic["R_mentee_tel"] = reader2["R_mentee_tel"].ToString().Trim();
                    dic["R_mentee_age"] = reader2["R_mentee_lowage"].ToString().Trim() + "세 ~ " + reader2["R_mentee_highage"].ToString().Trim() + "세";
                    dic["R_mentee_sex"] = "남 : " + reader2["R_mentee_male"].ToString().Trim() + "명 / 여 : " + reader2["R_mentee_female"].ToString().Trim() + "명";
                    dic["R_mentee_job"] = generalCode_GetName(reader2["R_mentee_job"].ToString().Trim());
                    dic["R_mentee_location"] = generalCode_GetName(reader2["R_mentee_location"].ToString().Trim());
                    dic["R_mentee_company"] = reader2["R_mentee_company"].ToString().Trim();
                    dic["R_mentee_address"] = reader2["R_mentee_address"].ToString().Trim();
                    dic["R_mentee_interestarea3"] = sortCode_GetName(reader2["R_mentee_interestarea3"].ToString().Trim());
                    dic["R_mentee_favorjob"] = generalCode_GetName(reader2["R_mentee_favorjob"].ToString().Trim());
                    dic["R_mentee_post"] = reader2["R_mentee_post1"].ToString().Trim() + "-" + reader2["R_mentee_post2"].ToString().Trim();

                    // mentor
                    dic["R_mentor_name"] = reader2["R_mentor_name"].ToString().Trim();
                    dic["R_mentor_tel"] = reader2["R_mentor_tel"].ToString().Trim();
                    dic["R_mentor_sex"] = reader2["R_mentor_sex"].ToString().Trim();
                    dic["R_mentor_age"] = reader2["R_mentor_age"].ToString().Trim();
                    dic["R_mentor_jobs"] = generalCode_GetName(reader2["R_mentor_jobs"].ToString().Trim());
                    dic["R_mentor_location"] = generalCode_GetName(reader2["R_mentor_location"].ToString().Trim());
                    dic["R_mentor_company"] = reader2["R_mentor_company"].ToString().Trim();
                    dic["R_mentor_address"] = reader2["R_mentor_address"].ToString().Trim();
                    dic["R_mentor_part3"] = sortCode_GetName(reader2["R_mentor_part3"].ToString().Trim());
                    dic["R_mentor_email"] = reader2["R_mentor_email"].ToString().Trim() + "@" + reader2["R_mentor_domain"].ToString().Trim();

                    // status
                    dic["R_matching"] = reader2["R_matching"].ToString().Trim();

                    // canceled time
                    dic["R_cancel_time"] = reader2["R_cancel_time"].ToString().Trim();

                    // matched time
                    dic["R_matching_time"] = reader2["R_matching_time"].ToString().Trim();

                    // finished time
                    dic["R_finish_time"] = reader2["R_finish_time"].ToString().Trim();

                    // admin approved time
                    dic["R_admin_agree_time"] = reader2["R_admin_agree_time"].ToString().Trim();

                    // request time
                    dic["R_request_time"] = reader2["R_request_time"].ToString().Trim();

                    // start time
                    dic["R_start_time"] = reader2["R_start_time"].ToString().Trim();

                    // finish time
                    dic["R_finish_time"] = reader2["R_finish_time"].ToString().Trim();

                    // clock
                    dic["R_clock_time"] = reader2["R_clock_time"].ToString().Trim();
                }

                return dic;



            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn2.Close();
            }

            return dic;

        }


        // 멘토리스트 Dataset
        public DataTable getMentor()
        {
            string query = "select * from mentor";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("소속기관/부서");
            dt.Columns.Add("주소");
            dt.Columns.Add("이메일");
            dr = dt.NewRow();
            da.Fill(dt);
            return dt;
        }


        // 멘티리스트 Dataset
        public DataTable getMentee()
        {
            string query = "select * from Mentee";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("나이대");
            dt.Columns.Add("성별비율");
            dt.Columns.Add("주소");
            dr = dt.NewRow();
            da.Fill(dt);
            return dt;
        }


        // 공통코드 - 대분류
        public DataTable getGeneralCode_L()
        {
            string query = "SELECT * FROM general_code_L";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("삭제가능 여부");
            dr = dt.NewRow();
            da.Fill(dt);
            return dt;
        }


        
        // 공통코드 - 중분류
        public DataTable getGeneralCode_M(string big_code)
        {
            string query = "SELECT gm_num, gm_name FROM general_code_M WHERE gm_parent = '" + big_code + "'";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("삭제가능 여부");
            dr = dt.NewRow();
            da.Fill(dt);
            return dt;
        }

        // 공통코드 대분류 - 사용중인지 아닌지..
        public bool GeneralCodeL_status(string big_code)
        {

            try
            {
                conn.Open();
                string query = @"SELECT Mentee.mentee_loginid, Mentor.mentor_loginid, request_history.R_matching
                             FROM general_code_L INNER JOIN
                             general_code_M ON general_code_L.gl_num = general_code_M.gm_parent FULL JOIN
                             Mentor ON general_code_M.gm_num = Mentor.mentor_educational OR general_code_M.gm_num = Mentor.mentor_soldierclass OR general_code_M.gm_num = Mentor.mentor_jobs OR 
                             general_code_M.gm_num = Mentor.mentor_location FULL JOIN
                             request_history ON general_code_M.gm_num = request_history.R_mentee_location OR general_code_M.gm_num = request_history.R_mentee_job OR 
                             general_code_M.gm_num = request_history.R_mentee_favorjob OR general_code_M.gm_num = request_history.R_mentor_educational OR 
                             general_code_M.gm_num = request_history.R_mentor_soldierclass OR general_code_M.gm_num = request_history.R_mentor_jobs OR 
                             general_code_M.gm_num = request_history.R_mentor_location FULL JOIN
                             Mentee ON general_code_M.gm_num = Mentee.mentee_favorjob OR general_code_M.gm_num = Mentee.mentee_location OR general_code_M.gm_num = Mentee.mentee_job
                             WHERE general_code_L.gl_num = '" + big_code + "'";
                command = new SqlCommand(query, conn);
                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }

        // 공통코드 중분류 - 사용중인지 아닌지..
        public bool GeneralCodeM_status(string mid_code)
        {
            try
            {
                conn.Open();
                string query = @"SELECT Mentee.mentee_loginid, Mentor.mentor_loginid, request_history.R_matching
                             FROM general_code_M RIGHT JOIN
                             Mentor ON general_code_M.gm_num = Mentor.mentor_educational OR general_code_M.gm_num = Mentor.mentor_soldierclass OR general_code_M.gm_num = Mentor.mentor_jobs OR 
                             general_code_M.gm_num = Mentor.mentor_location LEFT JOIN
                             request_history ON general_code_M.gm_num = request_history.R_mentee_location OR general_code_M.gm_num = request_history.R_mentee_job OR 
                             general_code_M.gm_num = request_history.R_mentee_favorjob OR general_code_M.gm_num = request_history.R_mentor_educational OR 
                             general_code_M.gm_num = request_history.R_mentor_soldierclass OR general_code_M.gm_num = request_history.R_mentor_jobs OR 
                             general_code_M.gm_num = request_history.R_mentor_location LEFT JOIN
                             Mentee ON general_code_M.gm_num = Mentee.mentee_favorjob OR general_code_M.gm_num = Mentee.mentee_location OR general_code_M.gm_num = Mentee.mentee_job
                             WHERE general_code_M.gm_num = '" + mid_code + "'";
                command = new SqlCommand(query, conn);
                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }




        

        
        // 분류코드 - 대
        public DataTable getSortCode_L()
        {
            string query = "SELECT b_num, b_name FROM sort_code_L ORDER BY b_num";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("삭제가능 여부");
            dr = dt.NewRow();
            da.Fill(dt);
            return dt;
        }

          
        
        // 분류코드 - 중
        public DataTable getSortCode_M(string big_code)
        {
            string query = "SELECT m_num, m_name FROM sort_code_M WHERE m_parent = '" + big_code + "' ORDER BY m_num";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("삭제가능 여부");
            dr = dt.NewRow();
            da.Fill(dt);
            return dt;
        }

        
        // 분류코드 - 소
        public DataTable getSortCode_S(string mid_code)
        {
            string query = "SELECT s_num, s_name FROM sort_code_S WHERE s_parent = '" + mid_code + "' ORDER BY s_num";
            da = new SqlDataAdapter(query, connectionString);
            dt = new DataTable();
            dt.Columns.Add("삭제가능 여부");
            dr = dt.NewRow();
            da.Fill(dt);
            return dt;
        }

        


        // 공통코드 대분류 삭제
        public DataTable GeneralCodeL_Delete(string glnum)
        {
            try
            {
                conn.Open();
                string sql = @"DELETE FROM general_code_L WHERE gl_num = @glnum";                        
                command = new SqlCommand(sql, conn);
                command.Parameters.Add("@glnum", SqlDbType.VarChar);
                command.Parameters["@glnum"].Value = glnum;
                command.ExecuteNonQuery();

                sql = @"DELETE FROM general_code_M WHERE gm_parent = @glnum";       // DELETE 구문은 한꺼번에 여러개 삭제 불가능..
                command = new SqlCommand(sql, conn);
                command.Parameters.Add("@glnum", SqlDbType.VarChar);
                command.Parameters["@glnum"].Value = glnum;
                command.ExecuteNonQuery();

                string query = "SELECT * FROM general_code_L ORDER BY gl_num ASC";
                da = new SqlDataAdapter(query, connectionString);
                dt = new DataTable();
                da.Fill(dt);

                return dt;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dt;

        }


        // 공통코드 대분류 등록시 중복체크
        public bool GeneralCodeL_truncate(string glnum, string glname)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM general_code_L WHERE gl_num = @glnum OR gl_name = @glname";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@glnum", SqlDbType.VarChar);
                command.Parameters.Add("@glname", SqlDbType.VarChar);
                command.Parameters["@glnum"].Value = glnum;
                command.Parameters["@glname"].Value = glname;

                reader = command.ExecuteReader();
                return reader.HasRows;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }

        // 공통코드 대분류 등록
        public DataTable GeneralCodeL_add(string glnum, string glname)
        {
            string sql = "SELECT * FROM general_code_L ORDER BY gl_num";
            da = new SqlDataAdapter(sql, connectionString);
            ds = new DataSet("general_code_L");             // 초기화를 안할경우 데이터 뻑남.
            command = new SqlCommand(sql, conn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds, "general_code_L");
            dt = ds.Tables["general_code_L"];
            dr = dt.NewRow();
            dr["gl_num"] = glnum;
            dr["gl_name"] = glname;
            dt.Rows.Add(dr);
            da.Update(ds, "general_code_L");
            ds.AcceptChanges();

            da = new SqlDataAdapter(sql, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;

        }


        // 공통코드 중분류 등록시 중복체크
        public bool GeneralCodeM_truncate(string gmnum, string gmname)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM general_code_M WHERE gm_num = @gmnum OR gm_name = @gmname";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@gmnum", SqlDbType.VarChar);
                command.Parameters.Add("@gmname", SqlDbType.VarChar);
                command.Parameters["@gmnum"].Value = gmnum;
                command.Parameters["@gmname"].Value = gmname;

                reader = command.ExecuteReader();
                return reader.HasRows;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }

        // 공통코드 중분류 등록
        public DataTable GeneralCodeM_add(string gmnum, string gmname, string gmparent)
        {
            string sql = "SELECT * FROM general_code_M WHERE gm_parent = '" + gmparent + "' ORDER BY gm_num";
            da = new SqlDataAdapter(sql, connectionString);
            ds = new DataSet("general_code_M");             // 초기화를 안할경우 데이터 뻑남.
            command = new SqlCommand(sql, conn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds, "general_code_M");
            dt = ds.Tables["general_code_M"];
            dr = dt.NewRow();
            dr["gm_num"] = gmnum;
            dr["gm_name"] = gmname;
            dr["gm_parent"] = gmparent;
            dt.Rows.Add(dr);
            da.Update(ds, "general_code_M");
            ds.AcceptChanges();

            sql = "SELECT * FROM general_code_M WHERE gm_parent = '" + gmparent + "' ORDER BY gm_num";
            da = new SqlDataAdapter(sql, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        // 공통코드 중분류 삭제
        public DataTable GeneralCodeM_Delete(int index, string gmparent)
        {
            string sql = "SELECT * FROM general_code_M WHERE gm_parent = '" + gmparent + "' ORDER BY gm_num";
            da = new SqlDataAdapter(sql, connectionString);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            ds = new DataSet("general_code_M");             // 초기화를 안할경우 데이터 뻑남.
            da.Fill(ds, "general_code_M");

            dt = ds.Tables["general_code_M"];
            dt.Rows[index].Delete();

            da.Update(ds, "general_code_M");
            ds.AcceptChanges();

            da = new SqlDataAdapter(sql, connectionString);
            dt = new DataTable();
            da.Fill(dt);

            return dt;
        }


        // 분류코드 대분류 - 사용중인지 아닌지
        public bool SortCodeL_status(string big_code)
        {

            try
            {
                conn.Open();
                string query = @"SELECT Mentee.mentee_loginid, Mentor.mentor_loginid, request_history.R_matching
                             FROM request_history RIGHT JOIN
               sort_code_M INNER JOIN
               sort_code_L ON sort_code_M.m_parent = sort_code_L.b_num INNER JOIN
               sort_code_S ON sort_code_M.m_num = sort_code_S.s_parent RiGHT JOIN
               Mentor ON sort_code_L.b_num = Mentor.mentor_part1 OR sort_code_M.m_num = Mentor.mentor_part2 OR sort_code_S.s_num = Mentor.mentor_part3 FULL JOIN
               Mentee ON sort_code_L.b_num = Mentee.mentee_interestarea1 OR sort_code_M.m_num = Mentee.mentee_interestarea2 OR sort_code_S.s_num = Mentee.mentee_interestarea3 ON 
               request_history.R_mentee_interestarea1 = sort_code_L.b_num OR request_history.R_mentee_interestarea2 = sort_code_M.m_num OR 
               request_history.R_mentee_interestarea3 = sort_code_S.s_num OR request_history.R_mentor_part1 = sort_code_L.b_num OR request_history.R_mentor_part2 = sort_code_M.m_num OR 
               request_history.R_mentor_part3 = sort_code_S.s_num
                             WHERE sort_code_L.b_num = '" + big_code + "'";
                command = new SqlCommand(query, conn);
                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }


        // 분류코드 중분류 - 사용중인지 아닌지
        public bool SortCodeM_status(string mid_code)
        {

            try
            {
                conn.Open();
                string query = @"SELECT Mentee.mentee_loginid, Mentor.mentor_loginid, request_history.R_matching
                             FROM request_history RIGHT JOIN
               sort_code_M INNER JOIN
               sort_code_S ON sort_code_M.m_num = sort_code_S.s_parent RiGHT JOIN
               Mentor ON sort_code_M.m_num = Mentor.mentor_part2 OR sort_code_S.s_num = Mentor.mentor_part3 FULL JOIN
               Mentee ON sort_code_M.m_num = Mentee.mentee_interestarea2 OR sort_code_S.s_num = Mentee.mentee_interestarea3 ON 
               request_history.R_mentee_interestarea2 = sort_code_M.m_num OR 
               request_history.R_mentee_interestarea3 = sort_code_S.s_num OR request_history.R_mentor_part2 = sort_code_M.m_num OR 
               request_history.R_mentor_part3 = sort_code_S.s_num
                             WHERE sort_code_M.m_num = '" + mid_code + "'";
                command = new SqlCommand(query, conn);
                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }


        // 분류코드 소분류 - 사용중인지 아닌지
        public bool SortCodeS_status(string sml_code)
        {

            try
            {
                conn.Open();
                string query = @"SELECT Mentee.mentee_loginid, Mentor.mentor_loginid, request_history.R_matching
                             FROM request_history RIGHT JOIN
               sort_code_S RiGHT JOIN
               Mentor ON sort_code_S.s_num = Mentor.mentor_part3 FULL JOIN
               Mentee ON sort_code_S.s_num = Mentee.mentee_interestarea3 ON
               request_history.R_mentee_interestarea3 = sort_code_S.s_num OR 
               request_history.R_mentor_part3 = sort_code_S.s_num
                             WHERE sort_code_S.s_num = '" + sml_code + "'";
                command = new SqlCommand(query, conn);
                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }


        // 분류코드 대분류 등록시 중복체크
        public bool SortCodeL_truncate(string bnum, string bname)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM sort_code_L WHERE b_num = @bnum OR b_name = @bname";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@bnum", SqlDbType.VarChar);
                command.Parameters.Add("@bname", SqlDbType.VarChar);
                command.Parameters["@bnum"].Value = bnum;
                command.Parameters["@bname"].Value = bname;

                reader = command.ExecuteReader();
                return reader.HasRows;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }


        // 분류코드 대분류 등록
        public DataTable SortCodeL_add(string bnum, string bname)
        {
            string sql = "SELECT * FROM sort_code_L ORDER BY b_num";
            da = new SqlDataAdapter(sql, connectionString);
            ds = new DataSet("sort_code_L");             // 초기화를 안할경우 데이터 뻑남.
            command = new SqlCommand(sql, conn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds, "sort_code_L");
            dt = ds.Tables["sort_code_L"];
            dr = dt.NewRow();
            dr["b_num"] = bnum;
            dr["b_name"] = bname;
            dt.Rows.Add(dr);
            da.Update(ds, "sort_code_L");
            ds.AcceptChanges();

            da = new SqlDataAdapter(sql, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;

        }


        // 분류코드 대분류 삭제
        public DataTable SortCodeL_Delete(string bnum)
        {
            try
            {
                conn.Open();
                string sql = @"DELETE FROM sort_code_L WHERE b_num = @bnum";
                command = new SqlCommand(sql, conn);
                command.Parameters.Add("@bnum", SqlDbType.VarChar);
                command.Parameters["@bnum"].Value = bnum;
                command.ExecuteNonQuery();

                sql = @"DELETE FROM sort_code_M WHERE m_parent = @bnum";       // DELETE 구문은 한꺼번에 여러개 삭제 불가능..
                command = new SqlCommand(sql, conn);
                command.Parameters.Add("@bnum", SqlDbType.VarChar);
                command.Parameters["@bnum"].Value = bnum;
                command.ExecuteNonQuery();

                sql = @"DELETE FROM sort_code_S WHERE SUBSTRING(s_parent,0,2) = @bnum";       // DELETE 구문은 한꺼번에 여러개 삭제 불가능..
                command = new SqlCommand(sql, conn);
                command.Parameters.Add("@bnum", SqlDbType.VarChar);
                command.Parameters["@bnum"].Value = bnum;
                command.ExecuteNonQuery();

                string query = "SELECT * FROM sort_code_L ORDER BY b_num ASC";
                da = new SqlDataAdapter(query, connectionString);
                dt = new DataTable();
                da.Fill(dt);

                return dt;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dt;

        }


        // 분류코드 중분류 등록시 중복체크
        public bool SortCodeM_truncate(string mnum, string mname)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM sort_code_M WHERE m_num = @mnum OR m_name = @mname";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@mnum", SqlDbType.VarChar);
                command.Parameters.Add("@mname", SqlDbType.VarChar);
                command.Parameters["@mnum"].Value = mnum;
                command.Parameters["@mname"].Value = mname;

                reader = command.ExecuteReader();
                return reader.HasRows;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }


        // 분류코드 중분류 등록
        public DataTable SortCodeM_add(string mnum, string mname, string mparent)
        {
            string sql = "SELECT * FROM sort_code_M ORDER BY m_num";
            da = new SqlDataAdapter(sql, connectionString);
            ds = new DataSet("sort_code_M");             // 초기화를 안할경우 데이터 뻑남.
            command = new SqlCommand(sql, conn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds, "sort_code_M");
            dt = ds.Tables["sort_code_M"];
            dr = dt.NewRow();
            dr["m_num"] = mnum;
            dr["m_name"] = mname;
            dr["m_parent"] = mparent;
            dt.Rows.Add(dr);
            da.Update(ds, "sort_code_M");
            ds.AcceptChanges();

            sql = "SELECT * FROM sort_code_M WHERE m_parent = '" + mparent + "' ORDER BY m_num";
            da = new SqlDataAdapter(sql, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;

        }


        // 분류코드 중분류 삭제
        public DataTable SortCodeM_Delete(string mnum)
        {
            try
            {
                conn.Open();
                string sql = @"DELETE FROM sort_code_M WHERE m_num = @mnum";       // DELETE 구문은 한꺼번에 여러개 삭제 불가능..
                command = new SqlCommand(sql, conn);
                command.Parameters.Add("@mnum", SqlDbType.VarChar);
                command.Parameters["@mnum"].Value = mnum;
                command.ExecuteNonQuery();

                sql = @"DELETE FROM sort_code_S WHERE s_parent = @mnum";       // DELETE 구문은 한꺼번에 여러개 삭제 불가능..
                command = new SqlCommand(sql, conn);
                command.Parameters.Add("@mnum", SqlDbType.VarChar);
                command.Parameters["@mnum"].Value = mnum;
                command.ExecuteNonQuery();

                string mnumparent = mnum.Substring(0, 2);
                string query = "SELECT * FROM sort_code_M WHERE m_parent = '" + mnumparent + "' ORDER BY m_num";
                da = new SqlDataAdapter(query, connectionString);
                dt = new DataTable();
                da.Fill(dt);

                return dt;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dt;

        }




        // 분류코드 소분류 등록시 중복체크
        public bool SortCodeS_truncate(string snum, string sname)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM sort_code_S WHERE s_num = @snum OR s_name = @sname";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@snum", SqlDbType.VarChar);
                command.Parameters.Add("@sname", SqlDbType.VarChar);
                command.Parameters["@snum"].Value = snum;
                command.Parameters["@sname"].Value = sname;

                reader = command.ExecuteReader();
                return reader.HasRows;

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }


        // 분류코드 소분류 등록
        public DataTable SortCodeS_add(string snum, string sname, string sparent)
        {
            string sql = "SELECT * FROM sort_code_S ORDER BY s_num";
            da = new SqlDataAdapter(sql, connectionString);
            ds = new DataSet("sort_code_M");             // 초기화를 안할경우 데이터 뻑남.
            command = new SqlCommand(sql, conn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds, "sort_code_S");
            dt = ds.Tables["sort_code_S"];
            dr = dt.NewRow();
            dr["s_num"] = snum;
            dr["s_name"] = sname;
            dr["s_parent"] = sparent;
            dt.Rows.Add(dr);
            da.Update(ds, "sort_code_S");
            ds.AcceptChanges();

            sql = "SELECT * FROM sort_code_S WHERE s_parent = '" + sparent + "' ORDER BY s_num";
            da = new SqlDataAdapter(sql, connectionString);
            dt = new DataTable();
            da.Fill(dt);
            return dt;

        }


        // 분류코드 소분류 삭제
        public DataTable SortCodeS_Delete(string snum)
        {
            try
            {
                conn.Open();
                string sql = @"DELETE FROM sort_code_S WHERE s_num = @snum";       // DELETE 구문은 한꺼번에 여러개 삭제 불가능..
                command = new SqlCommand(sql, conn);
                command.Parameters.Add("@snum", SqlDbType.VarChar);
                command.Parameters["@snum"].Value = snum;
                command.ExecuteNonQuery();

                string snumparent = snum.Substring(0, 4);
                string query = "SELECT * FROM sort_code_S WHERE s_parent = '" + snumparent + "' ORDER BY s_num";
                da = new SqlDataAdapter(query, connectionString);
                dt = new DataTable();
                da.Fill(dt);

                return dt;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                conn.Close();
            }

            return dt;

        }









        /* common */

        // 공통코드명 가져오는부분
        public string generalCode_GetName(string code)
        {
            string gm_name = "";
            try
            {
                string query = "select gm_name from general_code_M where gm_num = '" + code + "'";
                command = new SqlCommand(query, conn);
                conn.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    gm_name = reader.GetString(0);
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

            return gm_name;
        }


        // 분류코드명 가져오는부분
        public string sortCode_GetName(string code)
        {
            string gm_name = "";
            try
            {
                string query = "select s_name from sort_code_S where s_num = '" + code + "'";
                command = new SqlCommand(query, conn);
                conn.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    gm_name = reader.GetString(0);
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

            return gm_name;
        }

        // 비밀번호 변경 - 입력한 기존 비밀번호가 일치?)
        public bool ChangePW_isMember(string id, string pw)
        {
            try
            {
                conn.Open();
                string sql_truncate = "SELECT * FROM member WHERE id=@id AND pw=@pw";
                command = new SqlCommand(sql_truncate, conn);
                command.Parameters.Add("@id", SqlDbType.VarChar);
                command.Parameters.Add("@pw", SqlDbType.VarChar);

                command.Parameters["@id"].Value = id;
                command.Parameters["@pw"].Value = pw;
                reader = command.ExecuteReader();
                return reader.HasRows;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }

            return reader.HasRows;
        }

        // 비밀번호 변경 - 변경
        public void ChangePW_updatePW(string id, string pw)
        {
            try
            {
                conn.Open();
                string sql_update = "UPDATE member SET pw = @pw WHERE id = @id";
                command = new SqlCommand(sql_update, conn);
                command.Parameters.Add("@pw", SqlDbType.VarChar);
                command.Parameters.Add("@id", SqlDbType.VarChar);

                command.Parameters["@pw"].Value = pw;
                command.Parameters["@id"].Value = id;
                command.ExecuteNonQuery();
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                reader.Close();
                conn.Close();
            }
        }

    }

}
