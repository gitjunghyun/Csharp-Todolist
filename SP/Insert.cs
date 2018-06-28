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
    public partial class Insert : System.Windows.Forms.Form
    {
        //마우스포인트 변수 선언
        private Point mousePoint;
        public string id;

        public Insert()
        {
            InitializeComponent();
        }

        public Insert(String str)
        {
            InitializeComponent();

            id = str;

            //윈도우테두리제거방법
            this.FormBorderStyle = FormBorderStyle.None;

            //텍스트 박스의 default값
            textBox1.ForeColor = Color.Gray;
            textBox1.Text = "제목";
            textBox2.ForeColor = Color.Gray;
            textBox2.Text = "내용";
            textBox3.ForeColor = Color.Gray;
            textBox3.Text = "#태그";

            //배경화면
            panel2.Parent = this;
            panel2.BackColor = Color.Transparent;
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

        //등록 취소 버튼
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //취소 할 지 다시한번 물어보고 종료
            if (MessageBox.Show("취소하시겠습니까?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //폼 종료
                this.Close();
            }
        }

        //등록 완료 버튼
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //등록 할 지 다시한번 물어보고 종료
            if (MessageBox.Show("일정을 등록하시겠습니까?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                SqlConnection con1 = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
                string insert_member = "insert into list values(@list_writer, @list_title, @list_text, @list_tag , @list_date)";

                SqlCommand cmd1 = new SqlCommand(insert_member, con1);
                cmd1.Parameters.AddWithValue("@list_writer", id);
                cmd1.Parameters.AddWithValue("@list_title", textBox1.Text);
                cmd1.Parameters.AddWithValue("@list_text", textBox2.Text);
                cmd1.Parameters.AddWithValue("@list_tag", textBox3.Text);
                cmd1.Parameters.AddWithValue("@list_date", monthCalendar1.SelectionEnd.ToShortDateString());

                con1.Open();
                cmd1.ExecuteNonQuery();
                con1.Close();

                MessageBox.Show("일정등록이 완료되었습니다");
                this.Close();

                //폼 종료
                this.Close();
            }
        }

        //텍스트박스 초기화
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

        //종료 버튼
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //폼 종료
            this.Close();
        }
    }
}
