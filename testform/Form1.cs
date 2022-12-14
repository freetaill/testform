namespace testform
{
    public partial class Form1 : Form
    {
        public int num;

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //예외처리 또는 범위 지정으로 정수값만 입력되게 만들기
            bool flag = true;
            try { num = Convert.ToInt32(textBox1.Text); }

            catch (FormatException)
            {
                MessageBox.Show("정수를 입력해주세요");
                flag = false;
            }

            if (flag)
            {
                num = Convert.ToInt32(textBox1.Text);
                this.Hide();
                Form2 form2 = new Form2();
                form2.StartPosition = FormStartPosition.CenterScreen;
                form2.Owner = this;
                //form2.Show()로 구현할 시에는 자유롭게 화면전환 못함
                form2.ShowDialog();
            }
        }

        private void start2_Click(object sender, EventArgs e)
        {
            bool flag = true;
            try { num = Convert.ToInt32(textBox1.Text); }

            catch (FormatException)
            {
                MessageBox.Show("정수를 입력해주세요");
                flag = false;
            }

            if (flag)
            {
                num = Convert.ToInt32(textBox1.Text);
                this.Hide();
                Form3 form3 = new Form3();
                form3.StartPosition = FormStartPosition.CenterScreen;
                form3.Owner = this;
                //form2.Show()로 구현할 시에는 자유롭게 화면전환 못함
                form3.ShowDialog();
            }
        }
    }
}