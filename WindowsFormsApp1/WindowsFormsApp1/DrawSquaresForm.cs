using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{ 
    public class DrawSquaresForm:Form
    {
        private int squareCount;

        public DrawSquaresForm(int squareCount)
        {
            this.squareCount = squareCount;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;
            var brush = new SolidBrush(Color.Black);

            for (int i = 0; i < squareCount; i++)
            {
                graphics.FillRectangle(brush, 10 + i * 50, 10, 40, 40);
            }
        }

    }
}
