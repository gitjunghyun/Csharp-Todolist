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
    public partial class TD : System.Windows.Forms.Form
    {
        //마우스포인트 변수 선언
        private Point mousePoint;
        public string id;

        //로그인 폼을 제어하기 위해 선언
        Login login;

        public TD()
        {
            InitializeComponent();

            //윈도우테두리제거방법
            this.FormBorderStyle = FormBorderStyle.None;
        }

        //생성자로 부모폼을 받아온다
        public TD(Login form, String str)
        {
            InitializeComponent();

            login = form;
            id = str;
            label6.Text = id + "님 반갑습니다";

            //배경화면 설정
            label6.Parent = pictureBox1;
            label6.BackColor = Color.Transparent;
            label1.Parent = this;
            label1.BackColor = Color.Transparent;
            label2.Parent = this;
            label2.BackColor = Color.Transparent;
            label3.Parent = this;
            label3.BackColor = Color.Transparent;
            label4.Parent = this;
            label4.BackColor = Color.Transparent;
            label5.Parent = this;
            label5.BackColor = Color.Transparent;

            //윈도우테두리제거방법
            this.FormBorderStyle = FormBorderStyle.None;
        }

        //마우스로 좌표값 받아오는 함수
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        //마우스 좌표값 함수를 이용해서 폼의 위치를 마우스의 위치로 이동 시키는 함수
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X),
                    this.Top - (mousePoint.Y - e.Y));
            }
        }

        //뒤로가기 버튼
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //로그아웃 할 지 다시한번 물어보고 실행
            if (MessageBox.Show("로그아웃 하시겠습니까?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //로그아웃
                this.Close();
                login.ShowInTaskbar = true;
                login.Opacity = 100;
            }
        }

        //종료 버튼
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //종료 할 지 다시한번 물어보고 종료
            if (MessageBox.Show("정말로 종료하시겠습니까?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //프로그램 종료
                Application.Exit();
            }
        }

        //일정 등록 버튼
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Insert insert = new Insert(id);
            insert.ShowDialog();
        }

        //일정 관리 버튼
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager(id);
            manager.ShowDialog();
        }

        //폼이 열릴 때 일정 출력
        private void TD_Load(object sender, EventArgs e)
        {
            string[] title = new string[5];

            for (int i = 0; i < 5; i++)
            {
                title[i] = "등록된 일정이 없습니다";
            }

            // 디비에서 아이디가 일치하는 데이터 가져오기
            SqlConnection con = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "select list_title,list_date from list where list_writer = '" + id + "'";
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            for (int i = 0; sdr.Read(); i++) { 
                title[i] = sdr["list_title"].ToString();
                if(i == 4)
                {
                    break;
                }
            }
            con.Close();

            label1.Text = title[0];
            label2.Text = title[1];
            label3.Text = title[2];
            label4.Text = title[3];
            label5.Text = title[4];
        }
    }
}