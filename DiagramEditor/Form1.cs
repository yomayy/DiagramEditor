using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiagramEditor
{
    public partial class Form1 : Form
    {
        int flag;
        List<Record> records;

        public Form1()
        {
            InitializeComponent();
            records = new List<Record>();
            flag = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string value = textBox1.Text;
            string legend = textBox2.Text;

            if (value == "" || legend == "")
            {
                MessageBox.Show("Вы не ввели исходные данные", "ОЙ ОЙ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
            }
            else
            {
                string record = $"{value} -> {legend}";
                listBox1.Items.Add(record);
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();

                Record r = new Record()
                {
                    Legend = legend,
                    Value = Convert.ToDouble(value)
                };
                records.Add(r);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int k = listBox1.SelectedIndex;
            if (k == -1)
            {
                MessageBox.Show("Вы не выбрали запись для удаления", "ОЙ ОЙ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                listBox1.Items.RemoveAt(k);
                MessageBox.Show($"Запись {k + 1} - успешно удалена", "ОЙ ОЙ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                records.RemoveAt(k);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int N = records.Count();
            if (N == 0)
            {
                MessageBox.Show("Список данных пуст", "ОЙ ОЙ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                flag = 1; //flag for stolbiki
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int N = records.Count();
            if (N == 0)
            {
                MessageBox.Show("Список данных пуст", "ОЙ ОЙ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                flag = 2; //flag for krugovoi diagrammi
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
        }

        private void BuildDiagram_1(Graphics g)
        {

            Pen p = new Pen(Brushes.Cyan, 3);
            Brush b = new SolidBrush(Color.DarkOrchid);

            int N = records.Count();
            int w = (pictureBox1.Width - 50) / N - 5;
            double m = records[0].Value;
            foreach (var r in records)
            {
                if (r.Value > m)
                {
                    m = r.Value;
                }
            }
            double k = (pictureBox1.Height - 50) / m;

            List<Rectangle> shapes = new List<Rectangle>();
            for (int i = 0; i < N; i++)
            {
                shapes.Add(new Rectangle(25 + (w + 5) * i, 25, w, (int)(k * records[i].Value)));

            }

            // HomeTask - 23.02
            // Завершить разработку сценария рисования
            // столбчастой диаграммы. Высота столбцов 
            // должна соответствовать величинам из списка
            // цвет должен быть рандомным или из массива
            // заготовленных оттенков. Также нужнго будет 
            // добавить надписи к столбцам.

            Random rand = new Random();
            int red = rand.Next(0, 255);
            int green = rand.Next(0, 255);
            int blue = rand.Next(0, 255);

            Color x = Color.FromArgb(red, green, blue);
            Brush y = new SolidBrush(x);


            Rectangle r1 = new Rectangle(20, 20, 200, 300);



            g.FillRectangle(y, r1);
            g.DrawRectangle(p, r1);


            //-
            g.DrawString("Test",
                new Font("Arial", 16), Brushes.Coral, r1.X, r1.Y);
            //-

            g.DrawLine(p, 0, 0, r1.X, r1.Y);
        }

        private void BuildDiagram_2(Graphics g)
        {
            Color[] colors = new Color[]
            {
                Color.Purple, Color.Pink,
                Color.Plum, Color.RosyBrown
            };

            Brush[] brushes = new Brush[4];
            for (int i = 0; i < 4; i++)
            {
                brushes[i] = new SolidBrush(colors[i]);
            }

            Rectangle r = new Rectangle(50, 50, 150, 150);
            for (int i = 0; i < 4; i++)
            {
                g.FillPie(brushes[i], r, 45 * i, 45);
            }

            //HOMETASK-2
            // круговую диаграмму в
            // зависимости от исходных данных
            // коллекция records

        }
    

            /*g.DrawEllipse(new Pen(Color.Red, 3),
                50, 50, 200, 200);*/
        

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            if (flag == 1)
            {
                BuildDiagram_1(e.Graphics);
            } 
            else if(flag == 2)
            {
                BuildDiagram_2(e.Graphics);
            }
        }

        
    }
}
