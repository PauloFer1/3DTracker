using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WiiTest
{

    public partial class CalibrationForm : Form
    {
        private Bitmap b;
        private Graphics g;
        private int screenWidth = 512;
        private int screenHeight = 384;

        public CalibrationForm()
        {
            InitializeComponent();

            b = new Bitmap(screenWidth, screenHeight, PixelFormat.Format24bppRgb);
            g = Graphics.FromImage(b);

            drawMatrix();
        }
        public void setWii()
        {

        }
        public void setPoint(decimal x, decimal y)
        {
    /*
            g.Clear(Color.Black);
            g.DrawEllipse(new Pen(Color.Red), (int)(x), (int)(screenHeight/2-y), 2, 2);
            calPicBox.Image = b;*/
        }
        public void drawMatrix()
        {
            Pen myPen = new Pen(Color.Blue, 2);
            Pen myPen2 = new Pen(Color.Red, 2);

            // Create an array of points.
            Point[] myArray =
             {
                 new Point(10, 10),
                 new Point(200, 10),
                 new Point(200, 200),
                 new Point(10, 200),
                 new Point(10,10)
             };

            // Draw the Points to the screen before applying the 
            // transform.
            g.DrawLines(myPen, myArray);

            // Create a matrix and scale it.
            Matrix myMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            //myMatrix.Scale(3, 2, MatrixOrder.Append);
            float sX = 0.1f;
            //myMatrix.Shear(sX, 0);
            myMatrix.TransformPoints(myArray);

            // Draw the Points to the screen again after applying the 
            // transform.
            g.DrawLines(myPen2, myArray);
            calPicBox.Image = b;
        }
    }
}
