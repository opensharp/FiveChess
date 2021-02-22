using System;
using System.Drawing;
using System.Windows.Forms;
namespace 五子棋AI
{
    public partial class 五子棋AI : Form
    {
        bool Black = true;
        PictureBox[] pic = new PictureBox[225];
        int[] Flag = new int[225];
        int[,] ValueBlack = new int[255, 4];
        int[,] ValueWhite = new int[255, 4];
        public 五子棋AI()
        {
            InitializeComponent();
            for (int i = 0; i < 225; i++)
            {
                pic[i] = new PictureBox();
                pic[i].Location = new Point(6 + 37 * (i % 15), 6 + 37 * (i / 15));
                pic[i].Size = new Size(20, 20);
                pic[i].Cursor = Cursors.Hand;
                pic[i].Tag = i;
                pic[i].BackColor = Color.Transparent;
                pic[i].Click += new EventHandler(pic_Click);
                Controls.Add(pic[i]);
            }
        }
        bool CheckBlock(int num, int col1, int col2)
        {
            try
            {
                if ((col1 / 15) == (col2 / 15))
                {
                    if (Black)
                    {
                        if (Flag[num] == 1)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (Flag[num] == 2)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        void CheckResult(int num)
        {
            int Count1 = 0, Count2 = 0, Count3 = 0, Count4 = 0;
            for (int i = 1; i < 5; i++)
            {
                if (CheckBlock(num + i, num, num + i))
                {
                    Count1++;
                }
                if (CheckBlock(num - i, num, num - i))
                {
                    Count1++;
                }
                if (CheckBlock(num + i * 15, num, num))
                {
                    Count2++;
                }
                if (CheckBlock(num - i * 15, num, num))
                {
                    Count2++;
                }
                if (CheckBlock(num + i * 16, num, num + i))
                {
                    Count3++;
                }
                if (CheckBlock(num - i * 16, num, num - i))
                {
                    Count3++;
                }
                if (CheckBlock(num + i * 14, num, num - i))
                {
                    Count4++;
                }
                if (CheckBlock(num - i * 14, num, num + i))
                {
                    Count4++;
                }
            }
            if ((Count1 == 4) || (Count2 == 4) || (Count3 == 4) || (Count4 == 4))
            {
                if (Black)
                {
                    MessageBox.Show("黑子胜，按确定键重新开始！", "提示");
                }
                else
                {
                    MessageBox.Show("白子胜，按确定键重新开始！", "提示");
                }
                Application.Restart();
            }
        }
        private void pic_Click(object sender, EventArgs e)
        {
            SetBlock((int)((PictureBox)sender).Tag);
            SetBlock(FindBlock());
        }
        int FindBlock()
        {
            int Num = 0, Value = 0;
            for (int i = 0; i < 225; i++)
            {
                if (Flag[i] == 0)
                {
                    int now = AddValue(i);
                    if (now >= Value)
                    {
                        Value = now;
                        Num = i;
                    }
                }
            }
            return Num;
        }
        bool CheckExist(int num, int col1, int col2)
        {
            if ((col1 / 15) != (col2 / 15))
            {
                return false;
            }
            else if((num < 0) || (num > 224))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        void SetBlock(int num)
        {
            PictureBox Current = pic[num];
            if (Black)
            {
                Current.BackgroundImage = Properties.Resources.Black;
                Flag[num] = 1;
                for (int i = 1; i < 5; i++)
                {
                    if (CheckExist(num + i, num, num + i))
                    {
                        ValueBlack[num + i, 0]++;
                    }
                    if (CheckExist(num - i, num, num - i))
                    {
                        ValueBlack[num - i, 0]++;
                    }
                    if (CheckExist(num + i * 15, num, num))
                    {
                        ValueBlack[num + i * 15, 1]++;
                    }
                    if (CheckExist(num - i * 15, num, num))
                    {
                        ValueBlack[num - i * 15, 1]++;
                    }
                    if (CheckExist(num + i * 16, num, num + i))
                    {
                        ValueBlack[num + i * 16, 2]++;
                    }
                    if (CheckExist(num - i * 16, num, num - i))
                    {
                        ValueBlack[num - i * 16, 2]++;
                    }
                    if (CheckExist(num + i * 14, num, num - i))
                    {
                        ValueBlack[num + i * 14, 3]++;
                    }
                    if (CheckExist(num - i * 14, num, num + i))
                    {
                        ValueBlack[num - i * 14, 3]++;
                    }
                }
            }
            else
            {
                Current.BackgroundImage = Properties.Resources.White;
                Flag[num] = 2;
                for (int i = 1; i < 5; i++)
                {
                    if (CheckExist(num + i, num, num + i))
                    {
                        ValueBlack[num + i, 0]++;
                    }
                    if (CheckExist(num - i, num, num - i))
                    {
                        ValueBlack[num - i, 0]++;
                    }
                    if (CheckExist(num + i * 15, num, num))
                    {
                        ValueBlack[num + i * 15, 1]++;
                    }
                    if (CheckExist(num - i * 15, num, num))
                    {
                        ValueBlack[num - i * 15, 1]++;
                    }
                    if (CheckExist(num + i * 16, num, num + i))
                    {
                        ValueBlack[num + i * 16, 2]++;
                    }
                    if (CheckExist(num - i * 16, num, num - i))
                    {
                        ValueBlack[num - i * 16, 2]++;
                    }
                    if (CheckExist(num + i * 14, num, num - i))
                    {
                        ValueBlack[num + i * 14, 3]++;
                    }
                    if (CheckExist(num - i * 14, num, num + i))
                    {
                        ValueBlack[num - i * 14, 3]++;
                    }
                }
            }
            CheckResult(num);
            Black = !Black;
            Current.Cursor = Cursors.Default;
            Current.Enabled = false;
        }
        int AddValue(int num)
        {
            int Value = (int)Math.Pow(5, ValueWhite[num, 0]);
            Value +=(int)Math.Pow(5,ValueWhite[num, 1]);
            Value += (int)Math.Pow(5, ValueWhite[num, 2]);
            Value += (int)Math.Pow(5, ValueWhite[num, 3]);
            Value += (int)Math.Pow(2, ValueBlack[num, 0]);
            Value += (int)Math.Pow(2, ValueBlack[num, 1]);
            Value += (int)Math.Pow(2, ValueBlack[num, 2]);
            Value += (int)Math.Pow(2, ValueBlack[num, 3]);
            return Value;
        }
    }
}