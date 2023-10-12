﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Xml;
using System.IO;

namespace logr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            
        }
        
        Graphics gr;
        int i1, i2, j1, j2,n;
        double SK_x1, SK_x2, SK_y1, SK_y2, x, y,h,a,b,c,m,l,xb,t; 
        Point[] points = new Point[25];
        int k = 0;
        string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Font MyFont = new Font("arial", 12, FontStyle.Regular);
        double[] xn = new double[0];
        double[] yn = new double[0];
        double[] xt = new double[0];
        double[] yt = new double[0];
        double s1, s2, s3, s4, s5, s6, s7;
        Pen logr, l_i_pen, l_a_pen, sq_i_pen, sq_a_pen, neu_pen, bez_uni, bez_fuller;
        double xv1 = 0, xv2 = 0, yv1 = 0, yv2 = 0;
        double xx, yyy;
        int current = -1; int n_current = -1;
        bool ismove = false;         
        bool l_i = false; bool sq_i = false; bool logran = false; bool l_a = false; bool sq_a = false; bool neu = false; bool bz_univ = false; bool f_bez = false;
        Bitmap bp;
        #region Выбор цвета
        void ColorPick(ref Pen pen)
        {
            Color col = new Color();
            if (colored)
            {
                
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    col = colorDialog1.Color;
                }
                
            }
            else
            {
                Random ran = new Random();
                col = Color.FromArgb(ran.Next(0, 256), ran.Next(0, 256), ran.Next(0, 256));
            }
            SolidBrush b = new SolidBrush(col);
            pen = new Pen(b, 2);
        }
        private void вімквимкнВибірКольоруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colored)
            {
                вімквимкнВибірКольоруToolStripMenuItem.Checked = false;
                colored = false;
            }
            else
            {
                вімквимкнВибірКольоруToolStripMenuItem.Checked = true;
                colored = true;
            }
        }
        bool colored = true;
        #endregion
        #region Добавление точек
        private void button3_Click(object sender, EventArgs e)
        {
            
            listBox2.Items.Clear();
            gr.Clear(Color.White);
            SK_x1 = SK_x1 - 1;
            SK_x2 = SK_x2 + 1;
            SK_y1 = SK_y1 - 1;
            SK_y2 = SK_y2 + 1;
            system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
            Draw();
            DrawPoints();
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {                                              
                if (!ismove)
                {
                    double x = itox(e.X);
                    double y = jtoy(e.Y);
                    AddPoint(x,  y);
                    FillTable();

                }                       
        }
        void FillTable()
        {
            dataGridView1.RowCount = 3;
            dataGridView1.ColumnCount = k + 2;
            dataGridView1.Rows[1].Cells[0].Value = "x";
            dataGridView1.Rows[2].Cells[0].Value = "y";
            for (int i = 0; i < n; i++)
            {
                dataGridView1.Rows[0].Cells[i + 1].Value = st[i];
                dataGridView1.Rows[1].Cells[i + 1].Value = xn[i];
                dataGridView1.Rows[2].Cells[i + 1].Value = yn[i];
            }
        }
        void AddPoint(double x, double y)
        {
            if(n>=25)
            {
                MessageBox.Show("Ви ввели максимальну кількість точок", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Array.Resize(ref xt, k + 1);
            Array.Resize(ref yt, k + 1);
            Array.Resize(ref xn, k + 1);
            Array.Resize(ref yn, k + 1);
            xt[k] = x;
            yt[k] = y;
            xn[k] = x;
            yn[k] = y;
            
            points[k] = new Point(xtoi(x), ytoj(y));
            gr = pictureBox1.CreateGraphics();

            //gr.DrawEllipse(Pens.Black, e.X-2,e.Y-2, 4, 4);
            gr.FillEllipse(Brushes.Black, xtoi(x) - 2, ytoj(y) - 2, 4, 4);

            gr.DrawString(st[k].ToString(), MyFont, Brushes.Green, xtoi(x), ytoj(y));
            n = xt.Length;
            Sort(ref xt,ref yt);
            k++;
            if (k >= 2)
            {
                методиІнтерполяціїToolStripMenuItem.Enabled = true;
                методиАпроксімаціїToolStripMenuItem.Enabled = true;
                криваБезьєдемоToolStripMenuItem.Enabled = true;
            }
        }
        void RemovePoint(int i,ref double []x,ref double []y)
        {
            double[] xres = new double[n - 1];
            double[] yres = new double[n - 1];
            for(int k=0;k<i;k++)
            {
                xres[k] = x[k];
                yres[k] = y[k];
            }
            for(int k = i+1;k<x.Length;k++)
            {
                xres[k-1] = x[k];
                yres[k-1] = y[k];
            }
            x = new double[n - 1];
            y = new double[n - 1];
            x = xres;
            y = yres;
        }

        void DrawPoints()
        {
            for (int i = 0; i < xt.Length; i++)
            {
                gr.FillEllipse(Brushes.Black, xtoi(xt[i]) - 2, ytoj(yt[i]) - 2, 4, 4);
                gr.DrawString(st[i].ToString(), MyFont, Brushes.Green, xtoi(xt[i]), ytoj(yt[i]));
            }
        }
        #endregion
        #region Сортировка точек
        void Swap(ref double a, ref double b)
        {
            double t = a;
            a = b;
            b = t;
        }
        void Sort(ref double []x,ref double[]y)
        {
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = n - 1; j > i; j--)
                {
                    if (x[j - 1] > x[j])
                    {
                        Swap(ref x[j - 1], ref x[j]);
                        Swap(ref y[j - 1], ref y[j]);
                    }
                }
            }
        }
        #endregion      
        #region Лагранж
        private void многочленЛагранжаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lagranzh();
            logran = true;
        }
        double g(double x)
        {
            double sum = 0;
            for (int i = 0; i < k; i++)
            {
                double l = 1;
                for (int j = 0; j < k; j++)
                {
                    if (j != i)
                    {
                        l = l * (x - xt[j]) / (xt[i] - xt[j]);
                    }

                }
                sum = sum + yt[i] * l;
            }
            return sum;
        }
        private void lagranzh()
        {
            if (logran == false)
            {
                ColorPick(ref logr);
            }
            h = (SK_x2 - SK_x1) / pictureBox1.Width;
            x = SK_x1;
            while (x < SK_x2)
            {
                gr.DrawLine(logr, xtoi(x), ytoj(g(x)), xtoi(x + h), ytoj(g(x + h)));
                x = x + h;
            }
            double[] coefs = new double[k];
            double[,] factors = new double[k, k - 1];
            double[] final_coefs = new double[k];
            int counter = 0;
            for (int i = 0; i < k; i++)
            {
                double coef = yt[i];
                int u = 0;
                for (int j = 0; j < k; j++)
                {
                    if (j != i)
                    {
                        double xj = -1 * xt[j];
                        coef /= (xt[i] + xj);
                        factors[counter, u] = xj;
                        u++;
                    }
                }
                coefs[counter] = coef;
                counter++;
            }
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < Math.Pow(2, (k - 1)); j++)
                {
                    string combination = Convert.ToString(j, 2).PadLeft(k - 1, '0');
                    int q = 0;
                    double p = 1;
                    for (int m = 0; m < k - 1; m++)
                    {
                        if (combination[m] == '0')
                        {
                            q++;
                        }
                        else
                        {
                            p *= factors[i, m];
                        }
                    }
                    final_coefs[q] += (p * coefs[i]);
                }
            }
            for (int i = 0; i < final_coefs.Length; i++)
            {
                final_coefs[i] = Math.Round(final_coefs[i], 3);

            }
            string polynom = "y = ";
            for (int i = final_coefs.Length - 1; i >= 0; i--)
            {
                string sign = " ";
                if (final_coefs[i] >= 0 && i != final_coefs.Length - 1)
                {
                    sign = " + ";
                }
                if (i > 1)
                {
                    polynom += sign + final_coefs[i] + "x^" + i;
                }
                if (i == 1)
                {
                    polynom += sign + final_coefs[i] + "x";
                }
                if (i == 0)
                {
                    polynom += sign + final_coefs[i];
                }
            }
            listBox2.Items.Add("Многочлен Лагранжа:");
            listBox2.Items.Add(polynom);
            listBox2.Items.Add("-------------------");
        }
        #endregion
        #region Линейная интеполяция
        public void lin_inter()
        {
            if (l_i == false)
            {
                ColorPick(ref l_i_pen);
            }
            listBox2.Items.Add("Лінійна інтерполяція:");
            for (int i = 0; i < n - 1; i++)
            {
                m = (yt[i + 1] - yt[i]) / (xt[i + 1] - xt[i]);
                l = yt[i] - m * xt[i];
                x = xt[i];
                h = (SK_x2 - SK_x1) / pictureBox1.Width;
                while (x < xt[i + 1])
                {
                    gr.DrawLine(l_i_pen, xtoi(x), ytoj(ap(x, m, l)), xtoi(x + h), ytoj(ap((x + h), m, l)));
                    x = x + h;
                }
                m = Math.Round(m, 2);
                l = Math.Round(l, 2);
                string ans = "";
                if (l >= 0)
                {
                    ans = "y = " + m.ToString() + "x + " + l.ToString();
                }
                else
                {
                    ans = "y = " + m.ToString() + "x " + l.ToString();
                }
                listBox2.Items.Add((i + 1).ToString() + ") " + ans);
            }
            listBox2.Items.Add("---------------------------");

        }
        private void лінійнаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            lin_inter();
            l_i = true;
        }
        double ap(double xx, double a, double b)
        {
            double fx;
            fx = a * x + b;
            return fx;
        }
        #endregion
        #region Квадратичная интерполяция
        private void square_inter()
        {
            if (sq_i == false)
            {
                ColorPick(ref sq_i_pen);
            }
            listBox2.Items.Add("Квадратична інтерполяція:");
            int g = 0;
            gr = pictureBox1.CreateGraphics();
            for (int i = 1; i < k - 1; i++)
            {

                a = ((yt[i + 1] - yt[i - 1]) / ((xt[i + 1] - xt[i - 1]) * (xt[i + 1] - xt[i]))) - ((yt[i] - yt[i - 1]) / ((xt[i] - xt[i - 1]) * (xt[i + 1] - xt[i])));
                b = ((yt[i] - yt[i - 1]) / (xt[i] - xt[i - 1])) - a * (xt[i] + xt[i - 1]);
                c = yt[i - 1] - b * xt[i - 1] - a * xt[i - 1] * xt[i - 1];
                if (i % 2 == 1)
                {
                    g++;
                    h = (SK_x2 - SK_x1) / pictureBox1.Width;
                    x = xt[i - 1];
                    while (x < xt[i + 1])
                    {
                        gr.DrawLine(sq_i_pen, xtoi(x), ytoj(s(x, a, b, c)), xtoi(x + h), ytoj(s((x + h), a, b, c)));
                        x = x + h;
                    }
                    a = Math.Round(a, 2);
                    b = Math.Round(b, 2);
                    c = Math.Round(c, 2);
                    string bs = "";
                    string cs = "";
                    if (b >= 0)
                    {
                        bs = "+" + b;
                    }
                    else
                    {
                        bs = b.ToString();
                    }
                    if (c >= 0)
                    {
                        cs = "+" + c;
                    }
                    else
                    {
                        cs = c.ToString();
                    }
                    string ans = "y = " + a.ToString() + "x^2 " + bs + "x " + cs;
                    listBox2.Items.Add(g + ") " + ans);
                    listBox2.Items.Add("---------------------");
                }

            }
        }

        private void квадратичнаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            square_inter();
            sq_i = true;
        }
        #endregion
        #region Ньютон
        private void многочленНьютонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Newton();
            neu = true;
        }
        void Newton()
        {
            if (neu == false)
            {
                ColorPick(ref neu_pen);
            }
            h = (SK_x2 - SK_x1) / pictureBox1.Width;
            x = SK_x1;

            double[] coefs = new double[k];
            for (int i = 0; i < k; i++)
            {
                for (int d = 0; d <= i; d++)
                {
                    double c = yt[d];
                    for (int j = 0; j <= i; j++)
                    {
                        if (j != d)
                        {
                            c /= (xt[d] - xt[j]);
                        }
                    }
                    coefs[i] += c;
                }
            }
            double[] final_coefs = new double[k];
            final_coefs[0] = coefs[0] + coefs[1] * xt[0] * (-1);
            final_coefs[1] = coefs[1];
            for (int i = 2; i < k; i++)
            {
                for (int j = 0; j < Math.Pow(2, i); j++)
                {
                    string comb = Convert.ToString(j, 2).PadLeft(i, '0');
                    int q = 0;
                    double p = 1;
                    for (int d = 0; d < i; d++)
                    {
                        if (comb[d] == '0')
                        {
                            q++;
                        }
                        else
                        {
                            p *= (xt[d] * (-1));
                        }
                    }
                    final_coefs[q] += (p * coefs[i]);
                }
            }
            for (int i = 0; i < k; i++)
            {
                final_coefs[i] = Math.Round(final_coefs[i], 3);

            }
            while (x < SK_x2)
            {
                gr.DrawLine(neu_pen, xtoi(x), ytoj(f(x, final_coefs)), xtoi(x + h), ytoj(f((x + h), final_coefs)));
                x = x + h;
            }
            string polynom = "y = ";
            for (int i = final_coefs.Length - 1; i >= 0; i--)
            {
                string sign = " ";
                if (final_coefs[i] >= 0 && i != final_coefs.Length - 1)
                {
                    sign = " + ";
                }
                if (i > 1)
                {
                    polynom += sign + final_coefs[i] + "x^" + i;
                }
                if (i == 1)
                {
                    polynom += sign + final_coefs[i] + "x";
                }
                if (i == 0)
                {
                    polynom += sign + final_coefs[i];
                }
            }
            listBox2.Items.Add("Многочлен Ньютона:");
            listBox2.Items.Add(polynom);
            listBox2.Items.Add("---------------------");
        }
        double f(double x, double[] kf)
        {
            double fx = 0;
            for (int i = 0; i < k; i++)
            {
                fx += kf[i] * Math.Pow(x, i);
            }
            return fx;
        }
        #endregion
        #region Линейная апроксимация
        private void лінійнаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lin_ap();
            l_a = true;
        }
        private void lin_ap()
        {
            if (l_a == false)
            {
                ColorPick(ref l_a_pen);
            }
            double sum_pr = 0;
            double sum_x = 0;
            double sum_y = 0;
            double sum_sq = 0;
            for (int i = 0; i < k; i++)
            {
                sum_pr = sum_pr + xt[i] * yt[i];
                sum_x = sum_x + xt[i];
                sum_y = sum_y + yt[i];
                sum_sq = sum_sq + xt[i] * xt[i];
            }
            double a = (k * sum_pr - sum_x * sum_y) / (k * sum_sq - sum_x * sum_x);
            double b = (sum_y - a * sum_x) / k;

            h = (SK_x2 - SK_x1) / pictureBox1.Width;
            x = SK_x1;
            while (x < SK_x2)
            {
                gr.DrawLine(l_a_pen, xtoi(x), ytoj(ap(x, a, b)), xtoi(x + h), ytoj(ap((x + h), a, b)));
                x = x + h;
            }
            a = Math.Round(a, 2);
            b = Math.Round(b, 2);
            string bs = "";
            if (b >= 0)
            {
                bs = " + " + b.ToString();
            }
            else
            {
                bs = b.ToString();
            }
            string ans = "y = " + a.ToString() + "x " + bs;
            listBox2.Items.Add("Лінійна апроксимація");
            listBox2.Items.Add(ans);
            listBox2.Items.Add("--------------------------");
        }
        #endregion
        #region Квадратичная апроксимация
        private void квадратичнаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sq_aprok();
            sq_a = true;
        }
        private void sq_aprok()
        {
            if (sq_a == false)
            {
                ColorPick(ref sq_a_pen);
            }
            int n = xt.Length;
            sum(ref s1, ref s2, ref s3, ref s4, ref s5, ref s6, ref s7);
            double det_1 = k * s2 * s4 + 2 * (s1 * s2 * s3) - s2 * s2 * s2 - s1 * s1 * s4 - k * s3 * s3;
            double det_2 = s5 * s2 * s4 + s6 * s2 * s3 + s7 * s1 * s3 - s2 * s2 * s7 - s6 * s1 * s4 - s5 * s3 * s3;
            double det_3 = k * s6 * s4 + s1 * s2 * s7 + s2 * s5 * s3 - s2 * s6 * s2 - s1 * s5 * s4 - k * s3 * s7;
            double det_4 = k * s2 * s7 + s1 * s5 * s3 + s1 * s6 * s2 - s5 * s2 * s2 - s1 * s1 * s7 - k * s6 * s3;
            double a = det_4 / det_1;
            double b = det_3 / det_1;
            double c = det_2 / det_1;
            h = (SK_x2 - SK_x1) / pictureBox1.Width;
            x = SK_x1;
            while (x < SK_x2)
            {
                gr.DrawLine(sq_a_pen, xtoi(x), ytoj(s(x, a, b, c)), xtoi(x + h), ytoj(s((x + h), a, b, c)));
                x = x + h;
            }
            a = Math.Round(a, 2);
            b = Math.Round(b, 2);
            c = Math.Round(c, 2);
            string bs = "";
            string cs = "";
            if (b >= 0)
            {
                bs = "+" + b;
            }
            else
            {
                bs = b.ToString();
            }
            if (c >= 0)
            {
                cs = "+" + c;
            }
            else
            {
                cs = c.ToString();
            }
            string ans = "y = " + a.ToString() + "x^2 " + bs + "x " + cs;
            listBox2.Items.Add("Квадратична апроксимація");
            listBox2.Items.Add(ans);
            listBox2.Items.Add("--------------------------");
        }
        double s(double xx, double a, double b, double c)
        {
            double fx;
            fx = a * x * x + b * x + c;
            return fx;
        }
        #endregion
        #region Система координат и меню
        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if(bz_univ)
            {
                return;
            }
            
            if (e.Delta > 0)
            {
                if ((SK_x2 - SK_x1) == 2)
                {
                    return;
                }
                listBox2.Items.Clear();
                gr.Clear(Color.White);
                SK_x1 = SK_x1 + 1;
                SK_x2 = SK_x2 - 1;
                SK_y1 = SK_y1 + 1;  //масштабирование
                SK_y2 = SK_y2 - 1;
                system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
                Draw();
                DrawPoints();
            }
            else
            {
                
                listBox2.Items.Clear();
                gr.Clear(Color.White);
                SK_x1 = SK_x1 - 1;
                SK_x2 = SK_x2 + 1;
                SK_y1 = SK_y1 - 1;  //масштабирование
                SK_y2 = SK_y2 + 1;
                system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
                Draw();
                DrawPoints();
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            listBox2.Items.Clear();
            gr.Clear(Color.White);
            SK_x1 = SK_x1 + 1;
            SK_x2 = SK_x2 - 1;
            SK_y1 = SK_y1 + 1;  //масштабирование
            SK_y2 = SK_y2 - 1;
            system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
            Draw();
            DrawPoints();
        }
        private void параметриСистемиКоординатToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!panel2.Visible )
            {
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear(ref xt,ref yt);
        }
        void sys_set()
        {
            try
            {
                SK_x1 = double.Parse(textBox1.Text);
                SK_x2 = double.Parse(textBox2.Text);
                SK_y1 = double.Parse(textBox3.Text);
                SK_y2 = double.Parse(textBox4.Text);
                if ((SK_x1 + SK_x2) != (SK_y1 + SK_y2))
                {
                    throw new Exception();
                }
                pictureBox1.Enabled = true;
                gr = pictureBox1.CreateGraphics(); //выполнение построений системы
                gr.Clear(Color.White);
                i1 = 0;
                j1 = 0;
                i2 = pictureBox1.Width - 1;
                j2 = pictureBox1.Height - 1;
                system_koor(SK_x1, SK_x2, SK_y1, SK_y2);                
                but_nxt.Enabled = true;
                but_prv.Enabled = true;
                but_up.Enabled = true;
                but_down.Enabled = true;
                налаштуванняToolStripMenuItem.Enabled = true;
                //isdrawned = true;
                groupBox3.Visible = true;
                

            }
            catch (Exception)
            {
                MessageBox.Show("Невірний формат данних або система неквадратна", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            sys_set();
            groupBox1.Visible = false;
            dataGridView1.Visible = true;
        }
        private void додатиТочкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
        }
        private void відображатиКординатиТочокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }
        private void button3_Click_2(object sender, EventArgs e)
        {
            try
            {
                double x = double.Parse(txtX.Text);
                double y = double.Parse(txtY.Text);
                if((x<SK_x1)||(x>SK_x2||(y<SK_y1)||(y>SK_y2)))
                {
                    throw new Exception();
                }
                AddPoint(x,  y);
                //groupBox3.Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Невірний формат данних або точка не в системі", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        void Clear(ref double []xt,ref double []yt)
        {
            Array.Clear(yt, 0, xt.Length);
            Array.Clear(xt, 0, yt.Length);
            gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);
            system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
            k = 0;
            n = 0;
            listBox2.Items.Clear();
            logran = false; l_a = false; l_i = false; sq_a = false; sq_i = false; neu = false;  bz_univ = false; f_bez = false;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            k = 0;
            bz_un.Enabled = false;
            t_bz = 0;
            progressBar1.Value = 0;
            t_shw.Text = 0.ToString();
            panel1.Visible = false;
            
            
        }
        private void видалитиТочкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear(ref xt,ref yt);
            Clear(ref xn, ref yn);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            ;
            listBox2.Items.Clear();
            gr.Clear(Color.White);
            SK_x1 = SK_x1 - 1;
            SK_x2 = SK_x2 - 1;
            system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
            Draw();
            DrawPoints();
        }
        private void but_nxt_Click(object sender, EventArgs e)
        {
            
            listBox2.Items.Clear();
            gr.Clear(Color.White);
            SK_x1 = SK_x1 + 1;
            SK_x2 = SK_x2 + 1;
            system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
            Draw();
            DrawPoints();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            
            listBox2.Items.Clear();
            gr.Clear(Color.White);
            SK_y1 = SK_y1 + 1;
            SK_y2 = SK_y2 + 1;
            system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
            Draw();
            DrawPoints();
        }
        private void but_down_Click(object sender, EventArgs e)
        {
            
            listBox2.Items.Clear();
            gr.Clear(Color.White);
            SK_y1 = SK_y1 - 1;
            SK_y2 = SK_y2 - 1;
            system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
            Draw();
            DrawPoints();
        }


        bool isremoving = false;
        private void button5_Click_1(object sender, EventArgs e)
        {
            isremoving = true;
        }

        void system_koor(double x1, double x2, double y1, double y2) //построение системы координат
        {
            Pen pen_setka = new Pen(Brushes.LightBlue, 1);
            pen_setka.DashStyle = DashStyle.Dash;
            for (int p = (int)x1; p <= (int)x2; p++)
            {
                gr.DrawLine(pen_setka, xtoi(p), ytoj(y2), xtoi(p), ytoj(y1));
            }
            for (int p = (int)y1; p <= (int)y2; p++)
            {
                gr.DrawLine(pen_setka, xtoi(x1), ytoj(p), xtoi(x2), ytoj(p));
            }
            Pen pen_os = new Pen(Brushes.Blue, 1);
            pen_os.EndCap = LineCap.ArrowAnchor;
            pen_os.StartCap = LineCap.Triangle;
            gr.DrawLine(pen_os, xtoi(x1), ytoj(0), xtoi(x2), ytoj(0));
            gr.DrawLine(pen_os, xtoi(0), ytoj(y1), xtoi(0), ytoj(y2));
            Font MyFont = new Font("arial", 8, FontStyle.Regular);
            for (int p = 1; p <= x2; p++)
            {
                gr.DrawString(Convert.ToString(p), MyFont, Brushes.Blue, new Point(xtoi(p - 0.2), ytoj(-0.05)));
            }
            for (int p = -1; p >= x1; p--)
            {
                gr.DrawString(Convert.ToString(p), MyFont, Brushes.Blue, new Point(xtoi(p - 0.2), ytoj(-0.05)));
            }
            for (int p = 0; p <= y2; p++)
            {
                gr.DrawString(Convert.ToString(p), MyFont, Brushes.Blue, new Point(xtoi(-0.5), ytoj(p + 0.1)));
            }
            for (int p = -1; p >= y1; p--)
            {
                gr.DrawString(Convert.ToString(p), MyFont, Brushes.Blue, new Point(xtoi(-0.6), ytoj(p + 0.1)));
            }
        }

        private void анімаціяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bz_uni();
            bz_univ = true;
        }
        #endregion
        #region Вспомогательные методы
        double length(double x1, double y1,double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            current = -1;
            ismove = false;
            for(int i=0;i<xt.Length;i++)
            {
                if(length(xtoi(xt[i]),ytoj(yt[i]),e.X,e.Y)<5)
                {
                    current = i;
                    for(int k=0;k<xt.Length;k++)
                    {
                        if(xt[i] == xn[k])
                        {
                            n_current = k;
                        }
                    }
                    ismove = true;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
            
        {
            
            if(ismove && e.Button == MouseButtons.Left )
            {
                gr.Clear(Color.White);
                xt[current] = itox(e.X);
                yt[current] = jtoy(e.Y);
                xn[n_current] = itox(e.X);
                yn[n_current] = jtoy(e.Y);
                system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
                DrawPoints();
                listBox2.Items.Clear();
                Draw();
                FillTable();
            }
            x_current.Text = itox(e.X).ToString();
            label10.Text = jtoy(e.Y).ToString();
            //if(!ismove && e.Button == MouseButtons.Left)
            //{
            //    SK_x1 += itox(e.X)/1000;
            //    SK_x2 -= itox(e.X)/1000;
            //    SK_y1 += ytoj(e.Y) / 1000;
            //    SK_y2 -= ytoj(e.Y) / 1000;
            //    gr.Clear(Color.White);
            //    system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
            //}
        }

        private void перетягуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorPick(ref bez_fuller);
            full_bezie();
            f_bez = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Clear(ref xt,ref yt);
            Clear(ref xn, ref yn);
            isfirst = true;
            pictureBox1.Enabled = false;
        }

        private void Draw()
        {
            if (l_i)
            {
                lin_inter();
            }
            if (sq_i)
            {
                square_inter();
            }
            if (logran)
            {
                lagranzh();
            }
            if (l_a)
            {
                lin_ap();
            }
            if (sq_a)
            {
                sq_aprok();
            }
            if (neu)
            {
                Newton();
            }
            if (bz_univ)
            {
                // t_bz = 0;
                bz_uni();
            }
            if(f_bez)
            {
                full_bezie();
            }
        }

        public double razn(double[] y, double[] x)
        {
            double a = 0;
            if (y.Length > 2)
            {
                double[] xn1 = x;
                double[] yn1 = y;
                double[] xn2 = x;
                double[] yn2 = y;
                Array.Clear(xn1, 0, 1);
                Array.Clear(yn1, 0, 1);
                Array.Clear(xn2, x.Length - 1, 1);
                Array.Clear(yn2, x.Length - 1, 1);
                a = (razn(yn1, xn1) - razn(yn2, xn2)) / (x[x.Length - 1] - x[0]);
            }
            else if (y.Length == 2)
            {
                a = (y[1] - y[0]) / (x[1] - x[0]);
            }
            return a;
        }
        void sum(ref double s1, ref double s2, ref double s3, ref double s4, ref double s5, ref double s6, ref double s7)
        {
            s1 = 0; s2 = 0; s3 = 0; s4 = 0; s5 = 0; s6 = 0; s7 = 0;
            for (int i = 0; i < k; i++)
            {
                s1 = s1 + xt[i];
                s2 = s2 + Math.Pow(xt[i], 2);
                s3 = s3 + Math.Pow(xt[i], 3);
                s4 = s4 + Math.Pow(xt[i], 4);
                s5 = s5 + yt[i];
                s6 = s6 + xt[i] * yt[i];
                s7 = s7 + xt[i] * xt[i] * yt[i];
            }
        }
        #endregion
        #region Кривые Безье
        
        void bz_uni()
        {
            if (bz_univ == false)
            {
                ColorPick(ref bez_uni);
            }
            t_bz = 0;
            n_p = n - 1;
            bz_line();
            panel1.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 2;
            progressBar1.Value = 0;
            bz_un.Enabled = true;
            uzli_x = new double[n];
            uzli_y = new double[n];

        }


        bool isfirst = true;
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (isfirst)
            {
                //Clear(ref xt,);
                sys_set();
                //DrawPoints();
                //Draw();
                isfirst = false;
            }                      
        }

        

        void bz_line()
        {
            for (int i = 0; i < n - 1; i++)
            {
                gr.DrawLine(Pens.Black, xtoi(xn[i]), ytoj(yn[i]), xtoi(xn[i + 1]), ytoj(yn[i + 1]));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bp);
        }

        private void підгрузитиТочкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            string path = Application.StartupPath;
            string fileName = "";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
            }
            //StreamReader sr = new StreamReader(path);
            string[] stext = File.ReadAllLines(fileName);
            for(int i =0;i<stext.Length;i++)
            {
                string[] part = stext[i].Split();
                AddPoint( double.Parse(part[0]),  double.Parse(part[1]));
            }
        }
        //Form_menu home = new Form_menu();
        private void button2_Click_2(object sender, EventArgs e)
        {
            Clear(ref xt, ref yt);
            gr.Clear(Color.White);
            Hide();
        }

        private void методиІнтерполяціїToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void bz_un_Tick(object sender, EventArgs e)
        {
            BEZIER(t_bz, ref x1, ref y1);
            bz_anim(t_bz, Color.White);
            //gr.Clear(Color.White);
            system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
            DrawPoints();
            bz_line();
            bez_limit(t_bz);
            t_bz += 0.02;
            bz_anim(t_bz, Color.Blue);
            if (t_bz < 1.02)
            {
                double x2 = 5;
                double y2 = 5;
                BEZIER(t_bz, ref x2, ref y2);
                gr.DrawLine(bez_uni, xtoi(x1), ytoj(y1), xtoi(x2), ytoj(y2));
                t_shw.Text = Math.Round(t_bz, 2).ToString();
                progressBar1.PerformStep();
            }
            else
            {
                bz_un.Enabled = false;
            }
            
        }
        void bez_limit(double t)
        {
            
            for (double i = 0; i <= t-0.02; i = i + 0.02)
            {
                double x1 = 0, x2 = 0, y1 = 0, y2 = 0;
                BEZIER(i, ref x1, ref y1);
                BEZIER(i + 0.02, ref x2, ref y2);
                gr.DrawLine(bez_uni, xtoi(x1), ytoj(y1), xtoi(x2), ytoj(y2));  //bez_uni
            }
        }
        void full_bezie()
        {                            
            n_p = xt.Length-1;
            double time = 0;
            while (time <= 1)
            {
                Math.Round(time, 2);
                double x1 = 0; double y1 = 0; double x2 = 0; double y2 = 0;
                BEZIER(time, ref x1, ref y1);                
                time += 0.02;
                BEZIER(time, ref x2, ref y2);
                gr.DrawLine(bez_fuller, xtoi(x1), ytoj(y1), xtoi(x2), ytoj(y2));                
            }

        }
        
        void bz_anim(double t, Color fill)
        {
            Brush brush = new SolidBrush(fill);
            Pen pencill = new Pen(fill);
            double xi = 0; double yi = 0;
            uzli_x = xn;
            uzli_y = yn;
            double[] xv; double[] yv;
            for (int i = n - 1; i > 1; i--)
            {
                xv = new double[i];
                yv = new double[i];
                for (int k = 0; k < i; k++)
                {
                    counter_bz(uzli_x[k], uzli_x[k + 1], uzli_y[k], uzli_y[k + 1], t_bz - 0.02, ref xi, ref yi);
                    gr.FillEllipse(brush, xtoi(xi) - 2, ytoj(yi) - 2, 4, 4);
                    xv[k] = xi;
                    yv[k] = yi;
                }

                for (int j = 0; j < xv.Length - 1; j++)
                {
                    gr.DrawLine(pencill, xtoi(xv[j]), ytoj(yv[j]), xtoi(xv[j + 1]), ytoj(yv[j + 1]));
                }
                Array.Resize(ref uzli_x, uzli_x.Length - 1);
                Array.Resize(ref uzli_y, uzli_y.Length - 1);
                for (int y = 0; y < xv.Length; y++)
                {
                    uzli_x[y] = xv[y];
                    uzli_y[y] = yv[y];
                }
            }
        }
        #region Math
        void counter_bz(double x1, double x2, double y1, double y2, double t, ref double xx, ref double yyy)
        {
            xx = (1 - t) * x1 + t * x2;
            yyy = (1 - t) * y1 + t * y2;
        }
        double C(int n, int i)
        {
            int ch = 1;
            for (int k = 1; k <= n; k++)
            {
                ch *= k;
            }
            int i_fact = 1;
            for (int k = 1; k <= i; k++)
            {
                i_fact *= k;
            }
            int delta_fact = 1;
            for (int k = 1; k <= (n - i); k++)
            {
                delta_fact *= k;
            }
            int zn = delta_fact * i_fact;
            double res = ch / zn;
            return res;
        }
        double B(int n, int i, double t)
        {
            double res = 1;
            res *= C(n_p, i);
            res *= Math.Pow(t, i);
            res *= Math.Pow((1 - t), (n - i));
            return res;
        }
        double R(double x, double t)
        {
            double res = 1;

            for (int i = 0; i <= n; i++)
            {
                res *= B(n, i, t) * x;
            }
            return res;
        }
        

        void BEZIER(double t, ref double xx, ref double yy)
        {
            xx = 0;
            yy = 0;
            for (int i = 0; i <= n_p; i++)
            {
                xx += B(n_p, i, t) * xn[i];
                yy += B(n_p, i, t) * yn[i];
            }
        }
        #endregion
        #endregion
        #region Перевод Единиц
        int xtoi(double x)
        {
            int ii;
            ii = i1 + (int)((x - SK_x1) * ((i2 - i1) / (SK_x2 - SK_x1)));
            return ii;
        }
        double itox(int i)
        {
            double x;
            x = (i - i1) / ((i2 - i1) / (SK_x2 - SK_x1)) + SK_x1; //функция для перевода координат из пиксельной в декартовую систему
            x = Math.Round(x, 1); //
            return x;
        }
        int ytoj(double y)
        {
            int jj;
            jj = j2 + (int)((y - SK_y1) * (j1 - j2) / (SK_y2 - SK_y1));
            return jj;
        }
        double jtoy(int j)
        {
            double y;
            y = (j - j2) * (SK_y2 - SK_y1) / (j1 - j2) + SK_y1; //функция для перевода координат из пиксельной в декартовую систему
            y = Math.Round(y, 1); //
            return y;
        }
        #endregion
        #region Часы
        #region Обозначенные функции
        void ozn_BEZIER(double t, ref double xx, ref double yy, double[] x, double[] y)
        {
            xx = 0;
            yy = 0;
            for (int i = 0; i <= n_p; i++)
            {
                xx += ozn_B(x.Length - 1, i, t, x, y) * x[i];
                yy += ozn_B(x.Length - 1, i, t, x, y) * y[i];
            }
        }
        double ozn_B(int n, int i, double t, double[] x, double[] y)
        {
            double res = 1;
            res *= C(x.Length - 1, i);
            res *= Math.Pow(t, i);
            res *= Math.Pow((1 - t), (n - i));
            return res;
        }
        void ozn_full_bezie(double[] x, double[] y)
        {
            n_p = x.Length - 1;
            double time = 0;
            while (time <= 1)
            {
                Math.Round(time, 2);
                double x1 = 0; double y1 = 0; double x2 = 0; double y2 = 0;
                ozn_BEZIER(time, ref x1, ref y1, x, y);
                //listBox1.Items.Add(x1);
                time += 0.02;
                ozn_BEZIER(time, ref x2, ref y2, x, y);
                gr.DrawLine(Pens.Black, xtoi(x1), ytoj(y1), xtoi(x2), ytoj(y2));
                //listBox1.Items.Add(time);
            }
        }
        #endregion
        #region Цифры
        void zn_3()
        {
            double[] x = new double[4] { -3, 0, 0, -3 };
            double[] y = new double[4] { 3, 3, 0, 0 };
            double[] x1 = new double[4] { -3, 0, 0, -3 };
            double[] y1 = new double[4] { -3, -3, 0, 0 };
            ozn_full_bezie(x, y);
            ozn_full_bezie(x1, y1);
        }
        void zn_1()
        {
            double[] x = new double[3] { -3, -1, 0 };
            double[] y = new double[3] { 0, 1, 3 };
            double[] x1 = new double[2] { 0, 0 };
            double[] y1 = new double[2] { 3, -3 };
            ozn_full_bezie(x, y);
            ozn_full_bezie(x1, y1);
        } 
        void zn_4()
        {
            double[] x = new double[4] { -3, -3, -2, 0 };
            double[] y = new double[4] { 3, 1, 0, 0 };
            double[] x1 = new double[2] { 0, 0 };
            double[] y1 = new double[2] { 3, -3 };
            ozn_full_bezie(x, y);
            ozn_full_bezie(x1, y1);
        }
        void zn_5()
        {
            double[] x = new double[5] { 0, 2.5, 5,2, 0 };
            double[] y = new double[5] { 1, 1.5,-1.7, -3.1, -3.1 };
            double[] x1 = new double[2] { 0, 0 };
            double[] y1 = new double[2] { 1, 3 };
            double[] x2 = new double[2] { 0, 3 };
            double[] y2 = new double[2] { 3, 3 };
            ozn_full_bezie(x, y);
            ozn_full_bezie(x1, y1);
            ozn_full_bezie(x2, y2);
        }
        #endregion
        #endregion
        #region mess
        private void DrawSquare()
        {
            h = (SK_x2 - SK_x1) / pictureBox1.Width;
            x = a;
            while (a < b)
            {
                gr.DrawLine(Pens.Red, xtoi(x), ytoj(s(x, a, b, c)), xtoi(x + h), ytoj(s((x + h), a, b, c)));
                x = x + h;
            }
        }
        double bz_x(double t)
        {
            double x1 = xt[0];
            double x2 = xt[1];
            double x3 = xt[2];
            double x_res;
            x_res = Math.Pow((1 - t), 2) * x1 + 2 * (1 - t) * t * x2 + t * t * x3;
            return x_res;
        }
        double bz4_x(double t)
        {
            double x1 = xt[0];
            double x2 = xt[1];
            double x3 = xt[2];
            double x4 = xt[3];
            double x_res;
            double a = Math.Pow((1 - t), 3) * x1;
            double b = 3 * Math.Pow((1 - t), 2) * t * x2;
            double c = 3 * (1 - t) * Math.Pow(t, 2) * x3;
            double d = Math.Pow(t, 3) * x4;
            x_res = a + b + c + d;
            return x_res;
        }
        double bz4_y(double t)
        {
            double x1 = yt[0];
            double x2 = yt[1];
            double x3 = yt[2];
            double x4 = yt[3];
            double x_res;
            double a = Math.Pow((1 - t), 3) * x1;
            double b = 3 * Math.Pow((1 - t), 2) * t * x2;
            double c = 3 * (1 - t) * Math.Pow(t, 2) * x3;
            double d = Math.Pow(t, 3) * x4;
            x_res = a + b + c + d;
            return x_res;
        }
        double bz_y(double t)
        {
            double y1 = yt[0];
            double y2 = yt[1];
            double y3 = yt[2];
            double x_res;
            x_res = Math.Pow((1 - t), 2) * y1 + 2 * (1 - t) * t * y2 + t * t * y3;
            return x_res;
        }
        double t_bz;
        private void bezier_Tick(object sender, EventArgs e)
        {
            if (t <= 1)
            {
                gr.FillEllipse(Brushes.Black, xtoi(xb) - 2, ytoj(R(xb, t)) - 2, 4, 4);
                t = t + 0.02;
                xb = xb + Math.PI / 30;
            }

        }
        
        double x1, y1;

        private void bz_f_4_Tick(object sender, EventArgs e)
        {
            gr.DrawLine(Pens.Green, xtoi(xt[0]), ytoj(yt[0]), xtoi(xt[1]), ytoj(yt[1]));
            gr.DrawLine(Pens.Green, xtoi(xt[1]), ytoj(yt[1]), xtoi(xt[2]), ytoj(yt[2]));
            gr.DrawLine(Pens.Green, xtoi(xt[2]), ytoj(yt[2]), xtoi(xt[3]), ytoj(yt[3]));
            double x1 = bz4_x(t_bz);
            double y1 = bz4_y(t_bz);
            //listBox1.Items.Add(x1);
            //listBox1.Items.Add(y1);
            t_bz += 0.02;
            progressBar1.PerformStep();

            if ((x1 <= xt[3]) && (x1 >= xt[0]))
            {
                double x2 = bz4_x(t_bz);
                double y2 = bz4_y(t_bz);
                gr.DrawLine(Pens.Red, xtoi(x1), ytoj(y1), xtoi(x2), ytoj(y2));
                t_shw.Text = t_bz.ToString();
                //gr.FillEllipse(Brushes.Red, xtoi(x2)-2, ytoj(y2)-2,4,4);
                //gr.FillEllipse(Brushes.Red, (xtoi(xv1)) - 2, ytoj(yv1) - 2, 4, 4);
                //gr.FillEllipse(Brushes.Red, xtoi(xv2) - 2, ytoj(yv2) - 2, 4, 4);
                //gr.DrawLine(Pens.Black, xtoi(xv1), ytoj(yv1), xtoi(xv2), ytoj(yv2));
                //gr.DrawLine(Pens.White, xtoi(xv1), ytoj(yv1), xtoi(xv2), ytoj(yv2));
                //gr.DrawLine(Pens.Black, xtoi(xv1), ytoj(yv1), xtoi(x1) - 3, ytoj(y1) - 3);
                //gr.DrawLine(Pens.Black, xtoi(x1) - 3, ytoj(y1) - 3, xtoi(xv2), ytoj(y1));

            }
            else
            {
                //bz_f_4.Enabled = false;
            }

        }
        int n_p;


        double[] uzli_x;
        double[] uzli_y;





        private void bz_t_Tick(object sender, EventArgs e)
        {
            //gr.FillEllipse(Brushes.White, (xtoi(xv1)) - 2, ytoj(yv1) - 2, 4, 4);
            //gr.FillEllipse(Brushes.White, xtoi(xv2) - 2, ytoj(yv2) - 2, 4, 4);

            gr.DrawLine(Pens.Green, xtoi(xt[0]), ytoj(yt[0]), xtoi(xt[1]), ytoj(yt[1]));
            gr.DrawLine(Pens.Green, xtoi(xt[1]), ytoj(yt[1]), xtoi(xt[2]), ytoj(yt[2]));
            //gr.DrawLine(Pens.Black, xtoi(x1), ytoj(y1), xtoi(x2), ytoj(y2));
            double xp = x1;
            double yp = y1;
            x1 = bz_x(t_bz);
            y1 = bz_y(t_bz);
            //gr.DrawLine(Pens.White, xtoi(xv1), ytoj(yv1), xtoi(xp) - 3, ytoj(yp) - 3);
            //gr.DrawLine(Pens.White, xtoi(xp) - 3, ytoj(yp) - 3, xtoi(xv2), ytoj(y1));
            //system_koor(SK_x1, SK_x2, SK_y1, SK_y2);
            double x11 = xt[0];
            double x22 = xt[1];
            double y11 = yt[0];
            double y22 = yt[1];
            double x33 = xt[2];
            double y33 = yt[2];
            counter_bz(x11, x22, y11, y22, t_bz, ref xv1, ref yv1);
            counter_bz(x22, x33, y22, y33, t_bz, ref xv2, ref yv2);
            t_bz += 0.02;
            progressBar1.PerformStep();

            if (x1 < xt[2])
            {
                double x2 = bz_x(t_bz);
                double y2 = bz_y(t_bz);
                gr.DrawLine(Pens.Black, xtoi(x1), ytoj(y1), xtoi(x2), ytoj(y2));
                t_shw.Text = t_bz.ToString();
                //gr.FillEllipse(Brushes.Red, xtoi(x2)-2, ytoj(y2)-2,4,4);
                gr.FillEllipse(Brushes.Red, (xtoi(xv1)) - 2, ytoj(yv1) - 2, 4, 4);
                gr.FillEllipse(Brushes.Red, xtoi(xv2) - 2, ytoj(yv2) - 2, 4, 4);
                //gr.DrawLine(Pens.Black, xtoi(xv1), ytoj(yv1), xtoi(xv2), ytoj(yv2));
                //gr.DrawLine(Pens.White, xtoi(xv1), ytoj(yv1), xtoi(xv2), ytoj(yv2));
                //gr.DrawLine(Pens.Black, xtoi(xv1), ytoj(yv1), xtoi(x1) - 3, ytoj(y1) - 3);
                //gr.DrawLine(Pens.Black, xtoi(x1) - 3, ytoj(y1) - 3, xtoi(xv2), ytoj(y1));

            }
            else
            {
                //bz_t.Enabled = false;
            }

        }
        #endregion                           
    }
}
