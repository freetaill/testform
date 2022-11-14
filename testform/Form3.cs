using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testform
{
    public partial class Form3 : Form
    {
        int[] compare;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Owner;
            int num = form1.num;
            Size = new Size(120 + 90 * num, 150 + 90 * num);
            DynamicButton(num);
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = (Form1)Owner;
            form1.Show();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Owner;
            int num = form1.num;
            this.Controls.Clear();
            this.Controls.Add(btn_back);
            this.Controls.Add(btn_end);
            this.Controls.Add(btn_reset);
            DynamicButton(num);
        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void DynamicButton(int count)
        {
            Control[,] BTN = new Control[count, count];
            this.StartPosition = FormStartPosition.CenterScreen;
            btn_end.Location = new Point(150, 50 + 90 * count);
            btn_reset.Location = new Point(50, 50 + 90 * count);
            compare = new int[count];

            for (int y = 0; y < count; y++)
            {
                for (int x = 0; x < count; x++)
                {
                    int idx = y * count + x;
                    BTN[y, x] = new Button();

                    BTN[y, x].Name = idx.ToString();
                    BTN[y, x].Parent = this;
                    BTN[y, x].Size = new Size(90, 90);
                    int num = idx + 1;
                    //BTN[y, x].Text = "";
                    BTN[y, x].Location = new Point((50 + 90 * x), (50 + 90 * y));
                    BTN[y, x].Click += new EventHandler(message_print);

                    if ((x + y) % 2 == 1) { BTN[y, x].BackColor = Color.White; }

                    this.Controls.Add(BTN[y, x]);
                }
            }

        }

        void message_print(object? sender, EventArgs e)
        {
            //버튼 정보가져오기
            Button btn = sender as Button;
            Form1 form1 = (Form1)Owner;

            int k = Convert.ToInt32(btn.Name);
            int num = form1.num;

            if (compare[0] == 0 && k / num == 0)
            {
                btn.Text = num.ToString();
                remove_action(k, num);
                compare[k / num] = k % num + 1;
                check_fight(num, k / num, k % num);
            }
            else if (compare[k / num] == (k % num + 1))
            {
                btn.Text = "";
                compare[k / num] = 0;
                check_fight(num, k / num, k % num);
            }
            else
            {
                btn.Text = num.ToString();
                remove_action(k, num);
                compare[k / num] = k % num + 1;
                check_fight(num, k / num, k % num);
            }

        }

        void check_fight(int num, int row, int column)
        {
            // 버튼의 색을 붉은 색으로 바꾸라고 명령하는 신호
            // 기본적으로는 기본색으로 유지 또는 변경되도록 false신호 전송
            int name = 0;
            int switch_case = 0;
            bool have_can_attack_flag = false;

            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    if (i != j && compare[i] != 0 && compare[j] != 0)
                    {
                        if (compare[i] == compare[j] || Math.Abs(compare[i] - compare[j]) == Math.Abs(i - j))
                        {
                            switch_case = 1;
                            have_can_attack_flag = true;
                            name = j * num + compare[j] - 1;
                            push_color(switch_case, name, num);
                            name = i * num + compare[i] - 1;
                            push_color(switch_case, name, num);
                        }
                        else
                        {
                            switch_case = 2;
                            name = j * num + compare[j] - 1;
                            push_color(switch_case, name, num);
                            name = i * num + compare[i] - 1;
                            push_color(switch_case, name, num);
                        }
                    }
                }
                if (compare[i] == 0)
                {
                    switch_case = 3;
                    name = i;
                    push_color(switch_case, name, num);
                }
            }
        }
        void push_color(int switch_case, int name, int num)
        {
            Button btn = null;
            if (this.Controls.ContainsKey(name.ToString()))
            {
                btn = this.Controls[name.ToString()] as Button;
                switch (switch_case)
                {
                    case 1://붉은색
                        btn.BackColor = Color.Red;
                        break;
                    case 2://control색
                        if (name % 2 == 1) { btn.BackColor = Color.White; }
                        else { btn.BackColor = SystemColors.Control; }
                        break;
                    case 3://흰색
                        break;
                    case 4://전체탐색 중에 필요없는 곳에 붉은 색이 있을 경우
                        for(int i = 0; i < num; i++)
                        {
                            name = name * num + i;
                            btn = this.Controls[name.ToString()] as Button;
                            if (this.Controls.ContainsKey(name.ToString()))
                            {
                                if (name % 2 == 1) { btn.BackColor = Color.White; }
                                else { btn.BackColor = SystemColors.Control; }
                            }
                        }
                        break;
                }
            }
        }

        void remove_action(int k, int num)
        {
            Button btn = null;
            int x = (k / num) * num + compare[k / num] - 1;
            if (this.Controls.ContainsKey(x.ToString()))
            {
                btn = this.Controls[x.ToString()] as Button;
                if (compare[k / num] != 0)
                {
                    btn.Text = "";
                }
            }
        }
    }
}
