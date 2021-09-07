using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessor.Meashuring
{
    class PointV
    {
        #region Properties
        private Point a1 ;

        public Point A1
        {
            get
            {
                return a1;
            }
            set
            {
                a1 = value;
            }
        }

        private bool complete = false;
        private string name = "";
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private System.Drawing.Image img;

        


        private int radiusMin = 2;

        public int RadiusMin
        {
            get
            {
                return radiusMin;
            }
            set
            {
                radiusMin = value;
            }

        }

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
                this.Draw();
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
                this.Draw();
            }
        }

        private Color colorLetter = Color.Black;

        public Color ColorLetter
        {
            get
            {
                return colorLetter;
            }
            set
            {
                colorLetter = value;
            }
        }

        private Color colorFon = Color.White;

        public Color ColorFon
        {
            get
            {
                return colorFon;
            }
            set
            {
                colorFon = value;
            }
        }

        private Color colorFonFocus = Color.Yellow;

        public Color ColorFonFocus
        {
            get
            {
                return colorFonFocus;
            }
            set
            {
                colorFonFocus = value;
            }
        }

        private Color colorFonFix = Color.Red;

        public Color ColorFonFix
        {
            get
            {
                return colorFonFix;
            }
            set
            {
                colorFonFix = value;
            }
        }

        private Color colorCircle = Color.Red;

        public Color ColorCircle
        {
            get
            {
                return colorCircle;
            }
            set
            {
                colorCircle = value;
            }
        }


        private Font letterFont = new Font("Microsoft Sans Serif", 10);

        public Font LetterFont
        {
            get
            {
                return letterFont;
            }
            set
            {
                letterFont = value;
            }
        }

        private int letterFontSize = 10;

        public int LetterFontSize
        {
            get
            {
                return letterFontSize;
            }
            set
            {
                letterFontSize = value;
                letterFont = new Font("Microsoft Sans Serif", letterFontSize);
            }
        }

        private bool letterShow = true;

        public bool LetterShow
        {
            get
            {
                return letterShow;
            }
            set
            {
                letterShow = value;
            }
        }

        private Bitmap bmp_under;

        #endregion

        private MembersMeasureManager mmem = new MembersMeasureManager();
        public MembersMeasureManager MMEM
        {
            set
            {
                mmem = value;
                letterFont = new Font("Microsoft Sans Serif", mmem.fontSize);
            }
        }
        public PointV(System.Drawing.Image _img, MembersMeasureManager _mmem)
        {
            img = _img;
            mmem = _mmem;
            letterFont = new Font("Microsoft Sans Serif", mmem.fontSize);
        }
        
        public bool AddPoint(Point _a1, string _name)
        {
            
            if (complete)
                return complete;
            name = _name;
            a1 = _a1;
            complete = true;
            return complete;
        }

        private Rectangle GetAreaKoord(int bmp_width, int bmp_height)
        {
            int leftCircleX = a1.X - mmem.radius;
            int rightCircleX = a1.X + mmem.radius;

            int topCircleY = a1.Y - mmem.radius;
            int bottomCircleY = a1.Y + mmem.radius;


            Rectangle rect = GetRectLetter();


            int leftBoxX = rect.Left;
            int rightBoxX = rect.Right;

            int topBoxX = rect.Top;
            int bottomBoxX = rect.Bottom;


            int x = Math.Min(leftBoxX, leftCircleX);
            int y = Math.Min(topBoxX, topCircleY);
            int width = Math.Max(rightBoxX - leftBoxX, rightCircleX - leftCircleX) + 1;
            int height = bottomBoxX - topBoxX + bottomCircleY - topCircleY + 1;

            if (x < 0)
                x = 0;
            if (y < 0)
                y = 0;

            if (x + width > bmp_width)
                width = bmp_width - x;
            if (y + height > bmp_height)
                height = bmp_height - y;


            Rectangle ret = new Rectangle(x, y, width, height);
            return ret;
        }
        
        private Rectangle GetRectLetter()
        {
            Point tmp = new Point(a1.X, a1.Y);
            tmp.X -= (int)(this.letterFont.SizeInPoints + 4) / 2;

            if (tmp.Y + (-mmem.radius - this.letterFont.GetHeight() - 4) > 0)
                tmp.Y += (int)(-mmem.radius - this.letterFont.GetHeight() - 4);
            else
                tmp.Y += mmem.radius;

            Rectangle rect = new Rectangle(tmp, new Size((int)this.letterFont.SizeInPoints + 4, (int)this.letterFont.GetHeight() + 4));

            return rect;
        }



        public void PutUnder()
        {

            Bitmap bmp = (Bitmap)img;
            if (bmp_under == null) return;
            Point coord = GetAreaKoord(bmp.Width, bmp.Height).Location;
            int width = bmp_under.Width;
            int height = bmp_under.Height;

            BitmapData dstData = bmp_under.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            BitmapData srcData = bmp.LockBits(
                new Rectangle(coord.X, coord.Y, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            int dstStride = dstData.Stride;
            int dstOffset = dstStride - width * 3;

            int srcStride = srcData.Stride;
            int srcOffset = srcStride - width * 3;

            unsafe
            {
                byte* src_ = (byte*)srcData.Scan0.ToPointer();
                byte* dst_ = (byte*)dstData.Scan0.ToPointer();


                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, src_ += 3, dst_ += 3)
                    {
                        src_[0] = dst_[0];
                        src_[1] = dst_[1];
                        src_[2] = dst_[2];

                    }
                    src_ += srcOffset;
                    dst_ += dstOffset;
                }

            }
            bmp.UnlockBits(srcData);
            bmp_under.UnlockBits(dstData);

        }


        private Rectangle NewUnderBMP()
        {
            Bitmap bmp = (Bitmap)img;
            Rectangle rect = GetAreaKoord(bmp.Width, bmp.Height);
            bmp_under = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);
            return rect;
        }

        public void GetUnder()
        {
            Bitmap bmp = (Bitmap)img;
            if (bmp_under != null)
                bmp_under.Dispose();
            Rectangle rect = NewUnderBMP();
            Point coord = rect.Location;
            int width = bmp_under.Width;
            int height = bmp_under.Height;

            BitmapData dstData = bmp_under.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            BitmapData srcData = bmp.LockBits(
                new Rectangle(coord.X, coord.Y, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            int dstStride = dstData.Stride;
            int dstOffset = dstStride - width * 3;

            int srcStride = srcData.Stride;
            int srcOffset = srcStride - width * 3;

            unsafe
            {
                byte* src_ = (byte*)srcData.Scan0.ToPointer();
                byte* dst_ = (byte*)dstData.Scan0.ToPointer();


                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++, src_ += 3, dst_ += 3)
                    {
                        dst_[0] = src_[0];
                        dst_[1] = src_[1];
                        dst_[2] = src_[2];

                    }
                    src_ += srcOffset;
                    dst_ += dstOffset;
                }

            }
            bmp_under.UnlockBits(srcData);
            bmp.UnlockBits(dstData);

        }


        public void Draw()
        {
            
            Graphics g = Graphics.FromImage(img);

            Brush circleBrush = new SolidBrush(mmem.pointsColor);
            Pen circlePen = new Pen(mmem.pointsColor);

            g.FillEllipse(circleBrush, a1.X - this.radiusMin, a1.Y - this.radiusMin,
                this.radiusMin * 2, this.radiusMin * 2);
            g.DrawEllipse(circlePen, a1.X - mmem.radius, a1.Y - mmem.radius,
                mmem.radius * 2, mmem.radius * 2);

            circleBrush.Dispose();
            circlePen.Dispose();

            Rectangle rect = GetRectLetter();
            Rectangle rect1 = new Rectangle(rect.X, rect.Y + 2, rect.Width, rect.Height);

            StringFormat drawFormat5 = new StringFormat();
            drawFormat5.Alignment = StringAlignment.Center;

            Pen blackPen = new Pen(Color.Black);
            Brush brushColorLetter = new SolidBrush(this.colorLetter);

            if (!mmem.letters) return;
            if (this.isFixed)
            {
                Brush brushFonFix = new SolidBrush(this.colorFonFix);

                g.FillRectangle(brushFonFix, rect);
                g.DrawRectangle(blackPen, rect);
                g.DrawString(this.name, this.letterFont, brushColorLetter, rect1, drawFormat5);

                brushFonFix.Dispose();
                brushColorLetter.Dispose();
                blackPen.Dispose();
                return;
            }

            if (!this.IsSelected)
            {
                Brush brushFon = new SolidBrush(this.colorFon);

                g.FillRectangle(brushFon, rect);
                g.DrawRectangle(blackPen, rect);
                g.DrawString(this.name, this.letterFont, brushColorLetter, rect1, drawFormat5);

                brushFon.Dispose();
                blackPen.Dispose();
                brushColorLetter.Dispose();

                return;
            }
            else
            {
                Brush brushFocusFon = new SolidBrush(this.colorFonFocus);


                g.FillRectangle(brushFocusFon, rect);
                g.DrawRectangle(blackPen, rect);
                g.DrawString(this.name, this.letterFont, brushColorLetter, rect1, drawFormat5);

                brushFocusFon.Dispose();
                blackPen.Dispose();
                brushColorLetter.Dispose();
            }

        }

        public void DrawPoint()
        {
            PutUnder();
            GetUnder();
            Draw();
        }


        public void ReDrawPoint()
        {
            GetUnder();
            Draw();
        }

        public void ErasePoint()
        {

            PutUnder();
        }

        public bool CheckMove(int Xs, int Ys)
        {
            if (a1.X + Xs - mmem.radius <= 0 || a1.X + Xs + mmem.radius >= img.Width ||
                a1.Y + Ys - mmem.radius <= 0 || a1.Y + Ys + mmem.radius >= img.Height)
                return false;

            return true;
        }

        public bool MovePoint(int Xs,int Ys)
        {
            if (a1.X + Xs - mmem.radius <= 0 || a1.X + Xs + mmem.radius >= img.Width ||
                a1.Y + Ys - mmem.radius <= 0 || a1.Y + Ys + mmem.radius >= img.Height)
                return false;

            PutUnder();
            a1.X += Xs;
            a1.Y += Ys;
            GetUnder();
            Draw();
            return true;
        }


        public bool IsPoint(int x, int y)
        {
            if (x >= a1.X - mmem.radius && x <= a1.X + mmem.radius && y >= a1.Y - mmem.radius && y <= a1.Y + mmem.radius)
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

        }
        public PointV[] GetPoints()
        {
            PointV[] ret = new PointV[1];
            ret[0] = this;
            return ret;
        }

        public MeasurementsMembers GetMeasuremets()
        {
            MeasurementsMembers mm = new MeasurementsMembers();
            mm.Xc = A1.X;
            mm.Yc = A1.Y;
            mm.name = this.name;
            return mm;
        }
    }
    
}
