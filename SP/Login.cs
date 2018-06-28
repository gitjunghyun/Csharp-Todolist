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
    public partial class Login : System.Windows.Forms.Form
    {

        //마우스포인트 변수 선언
        private Point mousePoint;
        public string id;

        public Login()
        {
            InitializeComponent();

            //윈도우테두리제거방법
            this.FormBorderStyle = FormBorderStyle.None;

            //텍스트 박스의 default값
            textBox1.ForeColor = Color.Gray;
            textBox1.Text = "아이디를 입력해주세요";
            textBox2.ForeColor = Color.Gray;
            textBox2.Text = "비밀번호를 입력해주세요";

            //배경화면
            panel1.Parent = this;
            panel1.BackColor = Color.Transparent;
            panel3.Parent = this;
            panel3.BackColor = Color.Transparent;
            label1.Parent = panel3;
            label1.BackColor = Color.Transparent;
            label2.Parent = panel3;
            label2.BackColor = Color.Transparent;
        }

        //종료 버튼
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //종료 할 지 다시한번 물어보고 종료
            if (MessageBox.Show("정말로 종료하시겠습니까?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //폼 종료
                Application.Exit();
            }
        }

        //마우스로 좌표값 받아오는 함수
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        //마우스 좌표값 함수를 이용해서 폼의 위치를 마우스의 위치로 이동 시키는 함수
        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X),
                    this.Top - (mousePoint.Y - e.Y));
            }
        }

        //아이디 입력 받는 텍스트 박스 초기화
        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.Text = "";
        }

        //비밀번호 입력 받는 텍스트 박스 초기화
        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            textBox2.ForeColor = Color.Black;
            textBox2.Text = "";
        }

        //로그인 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            // 아이디값이 디비에 이미 있는 지 확인
            SqlConnection con1 = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
            SqlCommand cmd1 = new SqlCommand();

            cmd1.Connection = con1;
            cmd1.CommandText = "select * from member where member_id = '" + textBox1.Text + "'";
            con1.Open();
            SqlDataReader sdr = cmd1.ExecuteReader();

            //디비에 동일한 아이디가 존재하는 경우
            if (sdr.Read())
            {
                id = sdr["member_id"].ToString();
                string pw = sdr["member_pw"].ToString();

                if (pw.Equals(textBox2.Text))
                {
                    MessageBox.Show("로그인 성공");
                    TD td = new TD(this , id);
                    td.Show();
                    this.Opacity = 0;
                    this.ShowInTaskbar = false;
                }
                else
                {
                    MessageBox.Show("비밀번호가 틀립니다");
                }
                con1.Close();
            }
            else
            {
                con1.Close();
                MessageBox.Show("해당 아이디가 없습니다");
            }
        }

        //아이디, 비밀번호 찾기 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            Find find = new Find();
            find.ShowDialog();
        }

        //회원가입 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            Member member = new Member();
            member.ShowDialog();
        }
    }
}