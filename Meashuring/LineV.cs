using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessor.Meashuring
{
    class LineV
    {
        private Point a1 = new Point(-1,-1) ;
        private Point a2 = new Point(-1, -1);
        private bool complete = false;

        private CoordColor[] under;

        

        public float Length
        {
            get
            {
                if (a1.X > -1 && a2.X > -1)
                    return calcMetrics.CalcSizeF(a1, a2);
                else
                    return 0;
            }
        }

        public double[] Undo
        {
            get
            { 
                double[] ret = new double[under.Length];
                for(int i=0; i<under.Length; i++)
                    ret[i]=(under[i].color.R + under[i].color.G + under[i].color.B)/3;
                return ret;
            }
        }

        public ArrayList Coord
        {
            get
            {
                ArrayList ret = new ArrayList();
                for (int i = 0; i < under.Length; i++)
                    ret.Add(under[i].point);
                return ret;
            }
        }
        public int LenUndo
        {
            get
            {
                return under.Length;
                
            }
        }



        private MembersMeasureManager mmem = new MembersMeasureManager();
        public MembersMeasureManager MMEM
        {
            set
            {
                mmem = value;
            }
        }

        private string name = "";
        private System.Drawing.Image img;
        private bool isSelected = false;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
            }
        }

        private bool isFixed = false;

        public bool IsFixed
        {
            get
            {
                return isFixed;
            }
            set
            {
                isFixed = value;
            }
        }

        public LineV(System.Drawing.Image _img, MembersMeasureManager _mmem)
        {
            img = _img;
            mmem = _mmem;
        }


        private void SetUnderCoordColor()
        {
            if (under == null) return;
            Bitmap bmp = (Bitmap)img;

            BitmapData srcData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int stride = srcData.Stride;
            unsafe
            {
                byte* src_ = (byte*)srcData.Scan0.ToPointer();


                for (int i = 0; i < under.Length; i++)
                {
                    int stridey = stride * under[i].point.Y;
                    int dob = under[i].point.X * 3 + stridey;
                    src_[dob + 2] = under[i].color.R;
                    src_[dob + 1] = under[i].color.G;
                    src_[dob] = under[i].color.B;
                }
            }

            bmp.UnlockBits(srcData);

        }

        private void GetUnderCoordColor()
        {

            Bitmap bmp = (Bitmap)img;

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

                    under = new CoordColor[maxX - minX];
                    int shag = 1;
                    if (a1.X > a2.X)
                        shag = -1;
                    int count = 0;
                    for (int x = a1.X; x != a2.X; x += shag)
                    {
                        int y = (int)((x - a1.X) * lll_1 + a1.Y);
                        under[count].point = new Point(x, y);
                        int stridey = stride * y;
                        int dob = x * 3 + stridey;
                        under[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);
                    }
                }
                else
                {
                    int minY = Math.Min(a1.Y, a2.Y);
                    int maxY = Math.Max(a1.Y, a2.Y);
                    float lll_2 = ((float)a2.X - a1.X) / (a2.Y - a1.Y);
                    under = new CoordColor[maxY - minY];
                    int shag = 1;
                    if (a1.Y > a2.Y)
                        shag = -1;
                    int count = 0;
                    for (int y = a1.Y; y != a2.Y; y += shag)
                    {
                        int x = (int)((y - a1.Y) * lll_2 + a1.X);

                        under[count].point = new Point(x, y);

                        int stridey = stride * y;
                        int dob = x * 3 + stridey;

                        under[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);
                    }
                }
            }

            bmp.UnlockBits(srcData);
        

        }

        public bool AddPoint(Point a, string _name)
        {
            if (complete) return complete;
            name = name + _name;
            if (a1.X == -1)
            {
                a1 = a;
                return complete;
            }
            if (a2.X == -1)
            {
                a2 = a;
                complete = true;
                
            }
            return complete;
        }

        public void PutUnder()
        {
            SetUnderCoordColor();
            
        }
        public void GetUnder()
        {
            GetUnderCoordColor();

        }
        public void Draw()
        {
            Bitmap bmp = (Bitmap)img;
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
                        src_[dob + 2] = (byte)mmem.linesColor.R;
                        src_[dob + 1] = (byte)mmem.linesColor.G;
                        src_[dob] = (byte)mmem.linesColor.B;
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
                        src_[dob + 2] = (byte)mmem.linesColor.R;
                        src_[dob + 1] = (byte)mmem.linesColor.G;
                        src_[dob] = (byte)mmem.linesColor.B;
                    }
                }

            }

            bmp.UnlockBits(srcData);
        }

        public bool IsLine(Point x)
        {
            for (int i = 0; i < under.Length; i++)
                if (x == under[i].point)
                    return true;
            return false;
        }

        public void SetImage(System.Drawing.Image _img)
        {
            img = _img;
        }

        public void AddSdvig(int Xs, int Ys)
        {
            a1.X += Xs;
            a1.Y += Ys;

            a2.X += Xs;
            a2.Y += Ys;

        }

    }

}
