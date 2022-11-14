using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace testform
{
    public partial class Form2 : Form
    {
        int[] compare;
        Stack stack = new Stack();

        public Form2()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Owner;
            int num = form1.num;
            int[] col = new int[num + 1];
            compare = new int[num];
            Size = new Size(120 + 90 * num, 150 + 90 * num);
            possibility_push(col, 0);
            DynamicButton(num);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cha_Click(object sender, EventArgs e)
        {

            // 스택에 들어가는 순간 "object?"형식으로 포장되듯이
            // 형변환되기 때문에 꺼낼 때 넣기 전 형식으로
            // 현변환 해주어야한다.
            bool flag = false;
            Stack copy = new Stack();
            copy = (Stack)stack.Clone();
            for (int i = 0; i < stack.Count; i++)
            {
                int[] kk = (int[])copy.Pop();
                if (kk.SequenceEqual(compare))
                {
                    flag = true;
                }
            }
            if (flag == true) { MessageBox.Show("정답"); }
            else { MessageBox.Show("오답"); }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 =(Form1)Owner;
            form1.Show();
        }
        /// <summary>
        /// 버튼 컨트롤 동적으로 생성하기
        /// </summary>
        public void DynamicButton(int count)
        {
            Control[,] BTN = new Control[count, count];
            //Size = new Size(120 + 90 * count, 150 + 90 * count);
            this.StartPosition = FormStartPosition.CenterScreen;
            cha.Location = new Point(50, 50 + 90 * count);
            btnEnd.Location = new Point(150, 50 + 90 * count);

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
            }
            else if (compare[k / num] == (k % num + 1))
            {
                btn.Text = "";
                compare[k / num] = 0;
            }
            else
            {
                btn.Text = num.ToString();
                remove_action(k, num);
                compare[k / num] = k % num + 1;
            }
            
        }

        void remove_action(int k, int num)
        {
            Button btn = null;
            int x = (k / num) * num + compare[k / num] - 1;
            if (this.Controls.ContainsKey(x.ToString()))
            {
                btn = this.Controls[x.ToString()] as Button;
                if (compare[k/num] != 0)
                {
                    btn.Text = "";
                }
            }
        }

        //여왕이 서로 공격가능한 위치에 있는지를 판별하는 역할
        bool compare_queen(int[] col, int i)
        {
            int k = 1;
            bool flag = true;
            while (k < i && flag)
            {
                if (col[i] == col[k] || Math.Abs(col[i] - col[k]) == (i - k))
                    flag = false;
                k += 1;
            }
            return flag;
        }
        //조건이 참이 되는 모든 경우의 수를 col을 이용해 stack에 push하는 역할 수행
        void possibility_push(int[] col, int i)
        {
            int n = col.Length - 1;
            if (compare_queen(col, i))
            {
                if (i == n)
                {
                    int[] set = new int[n];
                    for (int x = 0; x < n; x++)
                    {
                        set[x] = col[x + 1];
                    }
                    stack.Push(set);
                }

                else
                {
                    for (int x = 0; x < n; x++)
                    {
                        col[i + 1] = x + 1;
                        possibility_push(col, i + 1);
                    }
                }
            }

        }
    }
}
