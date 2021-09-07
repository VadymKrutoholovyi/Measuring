using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageProcessor.Meashuring
{
    class Lines
    {
        #region Properties
        private CoordColor[] under;
        private Point a1 = new Point();
        private Point a2 = new Point();
        private Color color=Color.Blue;
        private float length = -1;
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

        public Lines(Point _a1, Point _a2, Bitmap _bmp, Color _color, string _name)
        {
            a1 = _a1;
            a2 = _a2;
            bmp = _bmp;
            name = _name;
            color = _color;
            under = GetUnderCoordColor();
            DrawLine();
        }

        #region Methods

        private CoordColor[] GetUnderCoordColor()
        {
            CoordColor[] ret;
            BitmapData srcData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int stride = srcData.Stride;
            unsafe
            {
                byte* src_ = (byte*)srcData.Scan0.ToPointer();
                if (Math.Abs(a2.X - a1.X) > Math.Abs(a2.Y - a1.Y))
                {
                    int minX = Math.Min(a1.X, a2.X);
                    int maxX = Math.Max(a1.X, a2.X);
                    float lll_1 = ((float)a2.Y - a1.Y) / (a2.X - a1.X);
                    
                    ret = new CoordColor[maxX - minX];
                    int shag = 1;
                    if (a1.X > a2.X)
                        shag = -1;
                    int count = 0;
                    for (int x = a1.X; x != a2.X; x += shag)
                    {
                        int y = (int)((x - a1.X) * lll_1 + a1.Y);
                        ret[count].point = new Point(x, y);
                        int stridey = stride * y;
                        int dob = x * 3 + stridey;
                        ret[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);
                    }
                }
                else
                {
                    int minY = Math.Min(a1.Y, a2.Y);
                    int maxY = Math.Max(a1.Y, a2.Y);
                    float lll_2 = ((float)a2.X - a1.X) / (a2.Y - a1.Y);
                    ret = new CoordColor[maxY - minY];
                    int shag = 1;
                    if (a1.Y > a2.Y)
                        shag = -1;
                    int count = 0;
                    for (int y = a1.Y; y != a2.Y; y += shag)
                    {
                        int x = (int)((y - a1.Y) * lll_2 + a1.X);

                        ret[count].point = new Point(x, y);

                        int stridey = stride * y;
                        int dob = x * 3 + stridey;

                        ret[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);
                    }
                }
            }

            bmp.UnlockBits(srcData);
            return ret;

        }

        private void SetUnderCoordColor()
        {
            if (under == null) return;
            for (int i = 0; i < under.Length; i++)
                bmp.SetPixel(under[i].point.X, under[i].point.Y, under[i].color); 
        }

        public void DrawLine()
        {

            BitmapData srcData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int stride = srcData.Stride;
            unsafe
            {
                byte* src_ = (byte*)srcData.Scan0.ToPointer();
                if (Math.Abs(a2.X - a1.X) > Math.Abs(a2.Y - a1.Y))
                {
                    int minX = Math.Min(a1.X, a2.X);
                    int maxX = Math.Max(a1.X, a2.X);
                    float lll_1 = ((float)a2.Y - a1.Y) / (a2.X - a1.X);
                    int shag = 1;
                    if (a1.X > a2.X)
                        shag = -1;
                    for (int x = a1.X; x != a2.X; x += shag)
                    {
                        int y = (int)((x - a1.X) * lll_1 + a1.Y);
                        int stridey = stride * y;
                        int dob = x * 3 + stridey;
                        src_[dob + 2] = (byte)color.R;
                        src_[dob + 1] = (byte)color.G;
                        src_[dob ] = (byte)color.B;
                    }
                }
                else
                {
                    int minY = Math.Min(a1.Y, a2.Y);
                    int maxY = Math.Max(a1.Y, a2.Y);
                    float lll_2 = ((float)a2.X - a1.X) / (a2.Y - a1.Y);
                    int shag = 1;
                    if (a1.Y > a2.Y)
                        shag = -1;
                    for (int y = a1.Y; y != a2.Y; y += shag)
                    {
                        int x = (int)((y - a1.Y) * lll_2 + a1.X);
                        int stridey = stride * y;
                        int dob = x * 3 + stridey;
                        src_[dob + 2] = (byte)color.R;
                        src_[dob + 1] = (byte)color.G;
                        src_[dob] = (byte)color.B;
                    }
                }

            }

            bmp.UnlockBits(srcData);
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
