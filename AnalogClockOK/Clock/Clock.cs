/*

    Copyright © 2016, Shawn Lee

*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clock2
{
    public partial class Clock : Form
    {
        private int centerX = 100;
        private int centerY = 100;
        private int radius = 100;
        private int speed;
        DateTime dateTime;
        public Clock()
        {
            dateTime = DateTime.Now;
            speed = 1;
            InitializeComponent();
            timer1.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTime = dateTime.AddSeconds(speed);
            ClockPanel.Invalidate();
        }
        
        private void ClockPanel_Paint(object sender, PaintEventArgs e)
        {
            float x;
            float y;

            // Draw Seconds
            ClockWorks(dateTime.Second, radius, .90, out x, out y);

            Pen redPen = new Pen(Color.Red, 2);
            e.Graphics.DrawLine(redPen, centerX, centerY, x, y);
            redPen.Dispose();
            
            // Draw Minutes
            ClockWorks(dateTime.Minute, radius, .85, out x, out y);

            Pen blackPen = new Pen(Color.Black, 4);
            e.Graphics.DrawLine(blackPen, centerX, centerY, x, y);
            blackPen.Dispose();
             
            // Draw Hours
            ClockWorks(dateTime.Hour % 12 * 5 + dateTime.Minute / 12, radius, .70, out x, out y);
            
            Pen blackPen2 = new Pen(Color.Black, 6);
            e.Graphics.DrawLine(blackPen2, centerX, centerY, x, y);
            blackPen2.Dispose();
        }
        
        private void ClockWorks(int time, int radius, double shortenRatio, out float xPosition, out float yPosition)
        {
            double x = 0;
            double y = 0;
            int angle = 0;

            if (time < 15)
            {
                angle = 90 - (time * 6);
            }
            else if (time < 30)
            {
                angle = 360 - (time % 15) * 6;
            }
            else if (time < 45)
            {
                angle = 270 - (time % 15) * 6;
            }
            else if (time < 60)
            {
                angle = 180 - (time % 15) * 6;
            }

            double ang = angle * (Math.PI / 180);

            x = radius + (Math.Cos(ang) * radius * shortenRatio);
            y = radius - (Math.Sin(ang) * radius * shortenRatio);

            xPosition = Convert.ToSingle(x);
            yPosition = Convert.ToSingle(y);
        }

        private void ClockPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            speed += 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            speed -= 1;
        }
    }
}
