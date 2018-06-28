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
    public partial class Manager : Form
    {
        //마우스포인트 변수 선언
        private Point mousePoint;
        public string id;

        public Manager()
        {
            InitializeComponent();

            //윈도우테두리제거방법
            this.FormBorderStyle = FormBorderStyle.None;
        }

        public Manager(string str)
        {
            InitializeComponent();

            id = str;

            //윈도우테두리제거방법
            this.FormBorderStyle = FormBorderStyle.None;
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

        //새로운 일정 등록 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            Insert insert = new Insert(id);
            insert.ShowDialog();
        }

        //선택한 일정 삭제 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            string str = this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[1].Value.ToString();

            //삭제 할 지 다시한번 물어보고 수행
            if (MessageBox.Show("정말로 삭제하시겠습니까?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
                string s = "delete from list where list_text = '" + str + "'";

                SqlCommand cmd = new SqlCommand(s, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            // DataSet을 가져온다
            DataSet ds = GetData();

            // DataSource 속성을 설정
            dataGridView1.DataSource = ds.Tables[0];
        }

        //검색 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (comboBox1.Text != ""))
            {
                // DataSet을 가져온다
                DataSet ds = selectedGetData(comboBox1.Text, textBox1.Text);

                // DataSource 속성을 설정
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            // DataSet을 가져온다
            DataSet ds = GetData();

            // DataSource 속성을 설정
            dataGridView1.DataSource = ds.Tables[0];
        }

        //데이터를 가져올 쿼리문 작성
        private DataSet GetData()
        {
            SqlConnection conn = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT list_title,list_text,list_tag,list_date FROM list where list_writer = '" + id + "'", conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        //검색한 데이터를 가져올 쿼리문 작성
        private DataSet selectedGetData(string c, string t)
        {
            string category = "";

            if (c.Equals("제목"))
            {
                category = "list_title";
            }
            else if (c.Equals("내용"))
            {
                category = "list_text";
            }
            else if (c.Equals("태그"))
            {
                category = "list_tag";
            }

            SqlConnection conn = new SqlConnection("data source = 210.123.254.69,1333; uid = user; pwd = 1234; database = TDL;");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT list_title,list_text,list_tag,list_date FROM list where " + category + " like '%" + t + "%' and list_writer = '" + id + "'", conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }
    }
}