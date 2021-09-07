using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessor.Meashuring
{
    class CircleV
    {
        private Point a1 = new Point(-1,-1) ;
        private Point a2 = new Point(-1, -1);
        private bool complete = false;

        private CoordColor[] under;

        //private float length = -1;
        private int radius = -1;

        public float Radius
        {
            get
            {
                return (float)radius;
            }
        }

        public double[] Undo
        {
            get
            {
                int len = (int)(under.Length / 2);
                double[] ret = new double[len];
                
                int j = 0;
                for (int i = 0; i < len; i += 2, j++)
                {
                    ret[j] = (under[i].color.R + under[i].color.G + under[i].color.B) / 3;
                }
                j = len-1;
                for (int i = 1; i < len; i += 2, j--)
                {
                    ret[j] = (under[i].color.R + under[i].color.G + under[i].color.B) / 3;
                }


                return ret;
            }
        }

        public ArrayList Coord
        {
            get
            {
                ArrayList p = new ArrayList();
                int len = (int)(under.Length / 2);

                Point[] ret = new Point[len];

                int j = 0;
                for (int i = 0; i < len; i += 2, j++)
                {
                    ret[j] = under[i].point;
                }
                j = len - 1;
                for (int i = 1; i < len; i += 2, j--)
                {
                    ret[j] = under[i].point;
                }

                for (int i = 0; i < ret.Length; i++)
                    p.Add(ret[i]);

                    return p;
            }
        }




        private long radius2 = -1;
        private float area = -1;

        public float Area
        {
            get
            {
                return area;
            }
        }

        private float perimetr = -1;

        public float Perimetr
        {
            get
            {
                return (float)perimetr;
            }
        }

        private int diametr = -1;
 
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

        private MembersMeasureManager mmem = new MembersMeasureManager();
        public MembersMeasureManager MMEM
        {
            set
            {
                mmem = value;
            }
        }

        public CircleV(System.Drawing.Image _img, MembersMeasureManager _mmem)
        {
            img = _img;
            mmem = _mmem;
        }
        private void Metrics()
        {
            radius = (int)calcMetrics.CalcSizeF(a1, a2);
            radius2 = radius * radius;
            diametr = radius * 2;
            perimetr = (float)(2 * Math.PI * radius);
            area = (float)(Math.PI * radius2);
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
            under = new CoordColor[2 * (2 * diametr + 2)];
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

                    int y = a1.Y - (int)Math.Pow(tmp, 0.5);
                    under[count].point = new Point(x, y);
                    int stridey = stride * y;
                    int dob = x * 3 + stridey;
                    under[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);

                    y = a1.Y + (int)Math.Pow(tmp, 0.5);
                    under[count].point = new Point(x, y);
                    stridey = stride * y;
                    dob = x * 3 + stridey;
                    under[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);

                }
                for (int y = a1.Y - radius; y <= a1.Y + radius; y++)
                {
                    double tmp = radius2 - (y - a1.Y) * (y - a1.Y);

                    int x = a1.X - (int)Math.Pow(tmp, 0.5);
                    under[count].point = new Point(x, y);
                    int stridey = stride * y;
                    int dob = x * 3 + stridey;
                    under[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);

                    x = a1.X + (int)Math.Pow(tmp, 0.5);
                    under[count].point = new Point(x, y);
                    stridey = stride * y;
                    dob = x * 3 + stridey;
                    under[count++].color = Color.FromArgb((int)src_[dob + 2], (int)src_[dob + 1], (int)src_[dob]);

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
                Metrics();
                
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



                for (int x = a1.X - radius; x <= a1.X + radius; x++)
                {
                    double tmp = radius2 - (x - a1.X) * (x - a1.X);
                    int y = a1.Y - (int)Math.Pow(tmp, 0.5);
                    int stridey = stride * y;
                    int dob = x * 3 + stridey;

                    src_[dob + 2] = (byte)mmem.linesColor.R;
                    src_[dob + 1] = (byte)mmem.linesColor.G;
                    src_[dob] = (byte)mmem.linesColor.B;

                    y = a1.Y + (int)Math.Pow(tmp, 0.5);

                    stridey = stride * y;
                    dob = x * 3 + stridey;

                    src_[dob + 2] = (byte)mmem.linesColor.R;
                    src_[dob + 1] = (byte)mmem.linesColor.G;
                    src_[dob] = (byte)mmem.linesColor.B;

                }
                for (int y = a1.Y - radius; y <= a1.Y + radius; y++)
                {
                    double tmp = radius2 - (y - a1.Y) * (y - a1.Y);
                    int x = a1.X - (int)Math.Pow(tmp, 0.5);
                    int stridey = stride * y;
                    int dob = x * 3 + stridey;

                    src_[dob + 2] = (byte)mmem.linesColor.R;
                    src_[dob + 1] = (byte)mmem.linesColor.G;
                    src_[dob] = (byte)mmem.linesColor.B;

                    x = a1.X + (int)Math.Pow(tmp, 0.5);

                    stridey = stride * y;
                    dob = x * 3 + stridey;

                    src_[dob + 2] = (byte)mmem.linesColor.R;
                    src_[dob + 1] = (byte)mmem.linesColor.G;
                    src_[dob] = (byte)mmem.linesColor.B;

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
        public bool CheckMove(int Xs, int Ys)
        {

            Point q1 = new Point(a1.X + Xs, a1.Y + Ys);
            Point q2 = new Point(a2.X + Xs, a2.Y + Ys);
            int radius_tmp = (int)calcMetrics.CalcSizeF(q1, q2)+5;

            if (q1.X - radius_tmp <= 0 || q1.X + radius_tmp >= img.Width ||
                q1.Y - radius_tmp <= 0 || q1.Y + radius_tmp >= img.Height)
                return false;

            return true;
        }

    }
    public struct CoordColor
    {
        public Color color;
        public Point point;
    }
}
