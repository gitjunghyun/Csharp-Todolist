using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP
{
    public partial class Open : System.Windows.Forms.Form
    {

        public Open()
        {
            InitializeComponent();

            //윈도우테두리제거방법
            this.FormBorderStyle = FormBorderStyle.None;
        }

        //윈도우폼 실행 이후에 3초 딜레이 후 로그인 폼 실행
        private void Open_Shown(object sender, EventArgs e)
        {
            Delay(3000);
            this.Close();
        }

        //시간을 딜레이 시켜주는 함수
        //처음 시작후 로그인 화면으로 넘어가는 딜레이를 주기 위해서 사용
        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }
    }
}