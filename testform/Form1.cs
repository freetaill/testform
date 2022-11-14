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
            //����ó�� �Ǵ� ���� �������� �������� �Էµǰ� �����
            bool flag = true;
            try { num = Convert.ToInt32(textBox1.Text); }

            catch (FormatException)
            {
                MessageBox.Show("������ �Է����ּ���");
                flag = false;
            }

            if (flag)
            {
                num = Convert.ToInt32(textBox1.Text);
                this.Hide();
                Form2 form2 = new Form2();
                form2.StartPosition = FormStartPosition.CenterScreen;
                form2.Owner = this;
                //form2.Show()�� ������ �ÿ��� �����Ӱ� ȭ����ȯ ����
                form2.ShowDialog();
            }
        }

        private void start2_Click(object sender, EventArgs e)
        {
            bool flag = true;
            try { num = Convert.ToInt32(textBox1.Text); }

            catch (FormatException)
            {
                MessageBox.Show("������ �Է����ּ���");
                flag = false;
            }

            if (flag)
            {
                num = Convert.ToInt32(textBox1.Text);
                this.Hide();
                Form3 form3 = new Form3();
                form3.StartPosition = FormStartPosition.CenterScreen;
                form3.Owner = this;
                //form2.Show()�� ������ �ÿ��� �����Ӱ� ȭ����ȯ ����
                form3.ShowDialog();
            }
        }
    }
}