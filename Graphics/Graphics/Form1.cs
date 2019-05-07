using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Graphics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Bitmap bitmap = new Bitmap(chart1.Width, chart1.Height);
            //Rectangle rectangle = new Rectangle(0, 0, chart1.Width, chart1.Height);
            //chart1.DrawToBitmap(bitmap, rectangle);
            //pictureBox1.Image = bitmap;

        }

        private void buttonCalc_Click(object sender,EventArgs e)
        {
            if (comboBox1.SelectedItem == null) {
                MessageBox.Show("Please, enter function from comboBox");
                return;
            }
            chart1.Series[0].Name = comboBox1.SelectedItem.ToString();
            try
            {
                bool sin = comboBox1.SelectedItem.ToString().Equals("y = sin(x)");
                double Xmin = double.Parse(textBox1.Text);
                double Xmax = double.Parse(textBox2.Text);
                double Step = double.Parse(textBox3.Text);
            
            if(Xmin >= Xmax)
            {
                MessageBox.Show("A should be bigger then B");
                return;
            }

            if(Step > Xmax - Xmin || Step <= 0)
            {
                MessageBox.Show("Please, enter correct step");
                return;
            }

            int count = (int)Math.Ceiling((Xmax - Xmin) / Step)+ 1;
            double[] x = new double[count];
            double[] y1 = new double[count];

            double[] y2 = new double[count];
            for (int i = 0; i < count; i++)

            {
                x[i] = Xmin + Step * i;
                if (sin)
                {
                    y1[i] = Math.Sin(x[i]);
                }
                else
                {
                    y1[i] = Math.Cos(x[i]);
                }

            }
            
            chart1.ChartAreas[0].AxisX.Minimum = Xmin;

            chart1.ChartAreas[0].AxisX.Maximum = Xmax;
            
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = Step;
            
            chart1.Series[0].Points.DataBindXY(x, y1);
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect data!");
            }
        }
    }
}
