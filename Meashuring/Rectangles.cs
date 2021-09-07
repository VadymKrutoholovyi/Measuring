using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageProcessor.Meashuring
{
    class Rectangles
    {
        public AllLines allLines= new AllLines();
        
        private Point a1=new Point();
        private Point a3 = new Point();
        private Point a2 = new Point();
        private Point a4 = new Point();




        private Bitmap bmp;
        private Color color;
        private string name;
        public bool isSelected = false;

        public void SetBitmap(Bitmap _bmp)
        {
            if (bmp != null)
                bmp.Dispose();
            bmp = _bmp;
        }

        public Rectangles(Point _a1, Point _a2, Bitmap _bmp, Color _color, string _name)
        {
            bmp = _bmp;
            color = _color;
            name = _name;
            a1 = _a1;
            a2 = _a2;
            a3.X = a2.X;
            a3.Y = a1.Y;
            a4.X = a1.X;
            a4.Y = a2.Y;

            
            //a1_end.X = a3.X - 1;
            //a1_end.Y = a1.Y;

            //a3_end.X = a3.X;
            //a3_end.Y = a4.Y-1;

            //a2_end.X = a4.X+1;
            //a2_end.Y = a2.Y;

            //a4_end.X = a4.X;
            //a4_end.Y = a1.Y+1;
 
            allLines.AddLines(a1, a3,_bmp, color,name);
            allLines.AddLines(a3, a2, _bmp, color, name);
            allLines.AddLines(a2, a4, _bmp, color, name);
            allLines.AddLines(a4, a1, _bmp, color, name);

        }


    }
}
