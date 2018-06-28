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
    public partial class Find : Form
    {

        //마우스포인트 변수 선언
        private Point mousePoint;

        public Find()
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
            textBox3.Text = "이름";
            textBox4.ForeColor = Color.Gray;
            textBox4.Text = "아이디";
            textBox5.ForeColor = Color.Gray;
            textBox5.Text = "이메일";
            textBox6.ForeColor = Color.Gray;
            textBox6.Text = "아이디";
            textBox7.ForeColor = Color.Gray;
            textBox7.Text = "비밀번호";
            textBox8.ForeColor = Color.Gray;
            textBox8.Text = "변경할 비밀번호";

            //배경화면
            panel1.Parent = this;
            panel1.BackColor = Color.Transparent;
            panel2.Parent = this;
            panel2.BackColor = Color.Transparent;
            panel3.Parent = this;
            panel3.BackColor = Color.Transparent;
        }

        //아이디 찾기 텍스트 박스 초기화
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

        //비밀번호 찾기 텍스트 박스 초기화
        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.Black;
            textBox3.Text = "";
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.ForeColor = Color.Black;
            textBox4.Text = "";
        }
        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.ForeColor = Color.Black;
            textBox5.Text = "";
        }

        //비밀번호 변경 텍스트 박스 초기화
        private void textBox6_Enter(object sender, EventArgs e)
        {
            textBox6.ForeColor = Color.Black;
            textBox6.Text = "";
        }
        private void textBox7_Enter(object sender, EventArgs e)
        {
            textBox7.UseSystemPasswordChar = true;
            textBox7.ForeColor = Color.Black;
            textBox7.Text = "";
        }
        private void textBox8_Enter(object sender, EventArgs e)
        {
            textBox8.UseSystemPasswordChar = true;
            textBox8.ForeColor = Color.Black;
            textBox8.Text = "";
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

        //아이디 찾기 버튼
        private void label4_Click(object sender, EventArgs e)
        {
            // 아이디값이 디비에 이미 있는 지 확인
            SqlConnection con1 = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = con1;
            cmd1.CommandText = "select member_id from member where member_name = '" + textBox1.Text + "' and member_email = '" + textBox2.Text + "'";
            con1.Open();
            SqlDataReader sdr = cmd1.ExecuteReader();

            //디비에 동일한 아이디가 존재하는 경우
            if (sdr.Read())
            {
                string id = sdr["member_id"].ToString();
                MessageBox.Show(id);
                con1.Close();
            }
            else
            {
                MessageBox.Show("정보를 정확히 입력해주세요");
            }
        }

        //비밀번호 찾기 버튼
        private void label5_Click(object sender, EventArgs e)
        {
            // 비밀번호값이 디비에 이미 있는 지 확인
            SqlConnection con1 = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = con1;
            cmd1.CommandText = "select member_pw from member where member_name = '" + textBox3.Text + "' and member_id = '" + textBox4.Text + "' and member_email = '" + textBox5.Text + "'";
            con1.Open();
            SqlDataReader sdr = cmd1.ExecuteReader();

            //디비에 동일한 비밀번호가 존재하는 경우
            if (sdr.Read())
            {
                string pw = sdr["member_pw"].ToString();
                MessageBox.Show(pw);
                con1.Close();
            }
            else
            {
                MessageBox.Show("정보를 정확히 입력해주세요");
            }
        }

        //비밀번호 변경 버튼
        private void label6_Click(object sender, EventArgs e)
        {
            // 비밀번호값이 디비에 이미 있는 지 확인
            SqlConnection con1 = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = con1;
            cmd1.CommandText = "select member_pw from member where member_id = '" + textBox6.Text + "' and member_pw = '" + textBox7.Text + "'";
            con1.Open();
            SqlDataReader sdr = cmd1.ExecuteReader();

            //비밀번호 변경 성공
            if (sdr.Read())
            {
                SqlConnection con2 = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
                SqlCommand cmd2 = new SqlCommand();

                cmd2.Connection = con2;
                cmd2.CommandText = "update member set member_pw ='" + textBox8.Text + "' where member_id ='" + textBox6.Text + "' and member_pw ='" + textBox7.Text + "'";
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
                MessageBox.Show("비밀번호가 변경되었습니다");
                con1.Close();
            }
            else
            {
                MessageBox.Show("정보를 정확히 입력해주세요");
            }
        }
    }
}