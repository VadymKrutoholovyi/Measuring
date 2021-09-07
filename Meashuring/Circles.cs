using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageProcessor.Meashuring
{
    class Circles
    {
        #region Properties
        private CoordColor[] under;
        private Point a1 = new Point();
        private Point a2 = new Point();
        private Color color=Color.Blue;
        private float length = -1;
        private int radius = -1;
        private long radius2 = -1;
        private float area = -1;
        private float perimetr = -1;
        private int diametr = -1;
        public Color ColorLine
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        private string name = "";
        private Bitmap bmp;
        public bool isSelected = false;
        public bool isFixed = false;
        

        #endregion
        private void Metrics()
        {
            radius = (int)calcMetrics.CalcSizeF(a1, a2);
            radius2 = radius * radius;
            diametr = radius * 2;
            perimetr = (float)(2 * Math.PI * radius);
            area = (float)(Math.PI * radius2);
        }

        public Circles(Point _a1, Point _a2, Bitmap _bmp, Color _color, string _name)
        {
            
            
            a1 = _a1;
            a2 = _a2;
            Metrics();
            
            bmp = _bmp;
            name = _name;
            color = _color;
            under = GetUnderCoordColor();
            DrawLine();
        }

        #region Methods

        private CoordColor[] GetUnderCoordColor()
        {
            CoordColor[] ret = new CoordColor[2*(2*diametr+2)];
            BitmapData srcData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int stride = srcData.Stride;
            int count = 0;
            unsafe
            {
                byte* src_ = (byte*)srcData.Scan0.ToPointer();

                    for (int x = a1.X - radius; x <= a1.X + radius; x++)
                    {
                        double tmp = radius2 - (x - a1.X) * (x - a1.X);

                        int y = a1.Y + (int)Math.Pow(tmp,0.5);
                        ret[count].point = new Point(x, y);
                        int stridey = stride * y;
                        int dob = x * 3 + stridey;
                        ret[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);

                        y = a1.Y - (int)Math.Pow(tmp,0.5);
                        ret[count].point = new Point(x, y);
                        stridey = stride * y;
                        dob = x * 3 + stridey;
                        ret[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);
                    
                    }
                    for (int y = a1.Y - radius; y <= a1.Y + radius; y++)
                    {
                        double tmp = radius2 - (y - a1.Y) * (y - a1.Y);

                        int x = a1.X + (int)Math.Pow(tmp, 0.5);
                        ret[count].point = new Point(x, y);
                        int stridey = stride * y;
                        int dob = x * 3 + stridey;
                        ret[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);

                        x = a1.X - (int)Math.Pow(tmp, 0.5);
                        ret[count].point = new Point(x, y);
                        stridey = stride * y;
                        dob = x * 3 + stridey;
                        ret[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);

                    }
            }

            bmp.UnlockBits(srcData);
            return ret;
        }
        public void DrawLine()
        {

            BitmapData srcData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int stride = srcData.Stride;

            unsafe
            {
                byte* src_ = (byte*)srcData.Scan0.ToPointer();



                for (int x = a1.X - radius; x <= a1.X + radius; x++)
                {
                    double tmp = radius2 - (x - a1.X) * (x - a1.X);
                    int y = a1.Y + (int)Math.Pow(tmp, 0.5);
                    int stridey = stride * y;
                    int dob = x * 3 + stridey;

                    src_[dob + 2] = (byte)color.R;
                    src_[dob + 1] = (byte)color.G;
                    src_[dob] = (byte)color.B;

                    y = a1.Y - (int)Math.Pow(tmp, 0.5);

                    stridey = stride * y;
                    dob = x * 3 + stridey;

                    src_[dob + 2] = (byte)color.R;
                    src_[dob + 1] = (byte)color.G;
                    src_[dob] = (byte)color.B;

                }
                for (int y = a1.Y - radius; y <= a1.Y + radius; y++)
                {
                    double tmp = radius2 - (y - a1.Y) * (y - a1.Y);
                    int x = a1.X + (int)Math.Pow(tmp, 0.5);
                    int stridey = stride * y;
                    int dob = x * 3 + stridey;

                    src_[dob + 2] = (byte)color.R;
                    src_[dob + 1] = (byte)color.G;
                    src_[dob] = (byte)color.B;

                    x = a1.X - (int)Math.Pow(tmp, 0.5);

                    stridey = stride * y;
                    dob = x * 3 + stridey;

                    src_[dob + 2] = (byte)color.R;
                    src_[dob + 1] = (byte)color.G;
                    src_[dob] = (byte)color.B;

                }

            }

            bmp.UnlockBits(srcData);
        }

        private void SetUnderCoordColor()
        {
            if (under == null) return;
            for (int i = 0; i < under.Length; i++)
                bmp.SetPixel(under[i].point.X, under[i].point.Y, under[i].color); 
        }

       
        public void ClearLine()
        {
            SetUnderCoordColor();
        }


        public void DrawLine(Point _a1, Point _a2, Color _color)
        {
            SetUnderCoordColor();
            a1 = _a1;
            a2 = _a2;
            color = _color;
            under = GetUnderCoordColor();
            DrawLine();
        }

        public void MoveLine(int Xs, int Ys)
        {
            SetUnderCoordColor();
            a1.X += Xs;
            a1.Y += Ys;
            a2.X += Xs;
            a2.Y += Ys;
            under = GetUnderCoordColor();
            DrawLine();
        }

        public bool IsLine(Point x)
        {
            for (int i = 0; i < under.Length; i++)
                if (x == under[i].point)
                    return true;
            return false;
        }

        public void SetBitmap(Bitmap _bmp)
        {
            if (bmp != null)
                bmp.Dispose();
            bmp = _bmp;
        }

        #endregion
    }
}
