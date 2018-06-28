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

namespace SP
{
    public partial class Member : Form
    {
        //마우스포인트 변수 선언
        private Point mousePoint;

        public Member()
        {
            InitializeComponent();

            //윈도우테두리제거방법
            this.FormBorderStyle = FormBorderStyle.None;

            //텍스트 박스의 default값
            textBox1.ForeColor = Color.Gray;
            textBox1.Text = "이름";
            textBox2.ForeColor = Color.Gray;
            textBox2.Text = "이메일";
            textBox3.ForeColor = Color.Gray;
            textBox3.Text = "아이디";
            textBox4.ForeColor = Color.Gray;
            textBox4.Text = "비밀번호";
            textBox5.ForeColor = Color.Gray;
            textBox5.Text = "비밀번호 확인";

            //배경화면
            panel1.Parent = this;
            panel1.BackColor = Color.Transparent;
        }

        //마우스로 좌표값 받아오는 함수
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        //마우스 좌표값 함수를 이용해서 폼의 위치를 마우스의 위치로 이동 시키는 함수
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X),
                    this.Top - (mousePoint.Y - e.Y));
            }
        }

        //종료 버튼
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //폼 종료
            this.Close();
        }

        //확인 버튼
        //디비에 회원 정보 저장
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //예외처리
            //디폴트값을 입력할 경우 예외처리
            if (textBox1.Text == "이름" || textBox1.Text == "이메일" || textBox1.Text == "아이디" || textBox1.Text == "비밀번호" || textBox1.Text == "비밀번호 확인")
            {
                MessageBox.Show("본인의 정보를 입력해주세요");
            }
            else
            {
                //빈칸으로 입력하거나 디폴트값 그대로 확인버튼을 누르는 경우 예외처리
                if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == "") || (textBox5.Text == ""))
                {
                    MessageBox.Show("정보를 입력해주세요");
                }
                else
                {
                    //비밀번호 확인이 올바르게 되지 않았을 경우 예외처리
                    if (textBox4.Text != textBox5.Text)
                    {
                        MessageBox.Show("비밀번호를 정확히 입력해주세요");
                    }
                    else
                    {
                        // 아이디값이 디비에 이미 있는 지 확인
                        SqlConnection con1 = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
                        SqlCommand cmd1 = new SqlCommand();

                        cmd1.Connection = con1;
                        cmd1.CommandText = "select member_id from member where member_id = '" + textBox3.Text + "'";
                        con1.Open();
                        SqlDataReader sdr = cmd1.ExecuteReader();

                        //디비에 동일한 아이디가 존재하는 경우
                        if (sdr.Read())
                        {
                            MessageBox.Show("이미 존재하는 아이디입니다");
                            con1.Close();
                        }
                        //디비에 회원정보 저장 성공
                        else
                        {
                            SqlConnection con2 = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
                            string insert_member = "insert into member values(@member_name, @member_email, @member_id, @member_pw)";

                            SqlCommand cmd2 = new SqlCommand(insert_member, con2);
                            cmd2.Parameters.AddWithValue("@member_name", textBox1.Text);
                            cmd2.Parameters.AddWithValue("@member_email", textBox2.Text);
                            cmd2.Parameters.AddWithValue("@member_id", textBox3.Text);
                            cmd2.Parameters.AddWithValue("@member_pw", textBox4.Text);

                            con2.Open();
                            cmd2.ExecuteNonQuery();
                            con2.Close();

                            MessageBox.Show("회원가입이 완료되었습니다");
                            this.Close();
                        }
                    }
                }
            }
        }

        //회원가입 텍스트 박스 초기화
        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.Text = "";
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.Black;
            textBox2.Text = "";
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.Black;
            textBox3.Text = "";
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.UseSystemPasswordChar = true;
            textBox4.ForeColor = Color.Black;
            textBox4.Text = "";
        }
        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.UseSystemPasswordChar = true;
            textBox5.ForeColor = Color.Black;
            textBox5.Text = "";
        }
    }
}