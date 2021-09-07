using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessor.Meashuring
{
    
    class PointsM
    {
        #region Properties

        private Point coord = new Point(0,0);
        private PointsM previous = null;

        public PointsM Previous
        {
            get
            {
                return previous;
            }
            set
            {
                previous = value;
            }
        }

        private PointsM next = null;

        public PointsM Next
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }

        public Point Coord
        {
            get
            {
                return coord;
            }
            set
            {
                coord = value;
            }

        }

        private int radiusMin=2;

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

        private int radiusMax = 10;

        public int RadiusMax
        {
            get
            {
                return radiusMax;
            }
            set
            {
                radiusMax = value;
            }

        }

        private bool isDeleted = false;

        public bool IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value;
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

        private Position letterPosition = Position.up;

        public Position LetterPosition
        {
            get
            {
                return letterPosition;
            }
            set
            {
                letterPosition = value;
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

        private string letterText = "-";

        public string LetterText
        {
            get
            {
                return letterText;
            }
            set
            {
                letterText = value;
            }
        }

        private Bitmap bmp_under;

        #endregion

        public PointsM(int x, int y)
        {
            this.coord.X = x;
            this.coord.Y = y;
        }

        #region Methods

        private Rectangle GetAreaKoord(Point e, int bmp_width, int bmp_hieght)
        {
            int leftCircleX = e.X - this.radiusMax;
            int rightCircleX = e.X + this.radiusMax;

            int topCircleY = e.Y - this.radiusMax;
            int bottomCircleY = e.Y + this.radiusMax;

            
            Rectangle rect = GetRectLetter(e);


            int leftBoxX=rect.Left;
            int rightBoxX=rect.Right;

            int topBoxX=rect.Top;
            int bottomBoxX=rect.Bottom;

            
            int x = Math.Min(leftBoxX,leftCircleX);
            int y = Math.Min(topBoxX, topCircleY);
            int width = Math.Max(rightBoxX - leftBoxX, rightCircleX - leftCircleX) + 1;
            int height = bottomBoxX - topBoxX + bottomCircleY - topCircleY + 1;

            if (x < 0)
                x = 0;
            if (y < 0)
                y = 0;

            if (x + width > bmp_width)
                width = bmp_width - x;
            if (y + height > bmp_hieght)
                height = bmp_hieght - y;
            
            
            Rectangle ret = new Rectangle(x,y,width,height);
            return ret;
        }

        private Rectangle GetRectLetter(Point e)
        {
            Point tmp = new Point(e.X, e.Y);
            tmp.X -= (int)(this.letterFont.SizeInPoints + 4) / 2;

            if (tmp.Y + (-this.radiusMax - this.letterFont.GetHeight() - 4) > 0)
                tmp.Y += (int)(-this.radiusMax - this.letterFont.GetHeight() - 4);
            else
                tmp.Y += this.radiusMax;

            Rectangle rect = new Rectangle(tmp, new Size((int)this.letterFont.SizeInPoints + 4, (int)this.letterFont.GetHeight() + 4));

            return rect;
        }

        private void DrawPoint(Graphics g)
        {
            
            if (this.isDeleted) return;

            Brush circleBrush = new SolidBrush(this.colorCircle);
            Pen circlePen = new Pen(this.colorCircle);
            
            g.FillEllipse(circleBrush, this.coord.X - this.radiusMin, this.coord.Y - this.radiusMin,
                this.radiusMin * 2, this.radiusMin * 2);
            g.DrawEllipse(circlePen, this.coord.X - this.radiusMax, this.coord.Y - this.radiusMax,
                this.radiusMax * 2, this.radiusMax * 2);

            circleBrush.Dispose();
            circlePen.Dispose();

            Point tmp = new Point(this.coord.X, this.coord.Y);
            
            Rectangle rect = GetRectLetter(tmp);
            Rectangle rect1 = new Rectangle(rect.X, rect.Y + 2, rect.Width, rect.Height);

            StringFormat drawFormat5 = new StringFormat();
            drawFormat5.Alignment = StringAlignment.Center;

            Pen blackPen = new Pen(Color.Black);
            Brush brushColorLetter = new SolidBrush(this.colorLetter);
            
            if (this.isFixed)
            {
                Brush brushFonFix = new SolidBrush(this.colorFonFix);
                
                g.FillRectangle(brushFonFix, rect);
                g.DrawRectangle(blackPen, rect);
                g.DrawString(this.letterText, this.letterFont, brushColorLetter, rect1, drawFormat5);

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
                g.DrawString(this.letterText, this.letterFont, brushColorLetter, rect1, drawFormat5);

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
                g.DrawString(this.letterText, this.letterFont, brushColorLetter, rect1, drawFormat5);

                brushFocusFon.Dispose();
                blackPen.Dispose();
                brushColorLetter.Dispose();
            }
        }


        public void DrawPoint(System.Drawing.Image img, Point newCoord)
        {
            
            Rectangle rect_new = GetAreaKoord(newCoord, img.Width, img.Height);
            if (bmp_under != null)
            {
                Rectangle rect_old = GetAreaKoord(this.Coord, img.Width, img.Height);
                DrawImage((Bitmap)img, bmp_under, rect_old.Location);
                bmp_under.Dispose();
            }
            bmp_under = new Bitmap(rect_new.Width, rect_new.Height, PixelFormat.Format24bppRgb);
            DrawImageBack((Bitmap)img, bmp_under, rect_new.Location);
            this.Coord = newCoord;
            DrawPoint(Graphics.FromImage(img));
        }

        public void DrawPoint(System.Drawing.Image img)
        {
            DrawPoint(Graphics.FromImage(img));
        }

        public void DeletePoint(System.Drawing.Image img)
        {
            if (bmp_under != null)
            {
                Rectangle rect_old = GetAreaKoord(this.Coord, img.Width, img.Height);
                DrawImage((Bitmap)img, bmp_under, rect_old.Location);
                bmp_under.Dispose();
                this.IsDeleted = true;
            }
        }

        public static void DrawImage(Bitmap src, Bitmap dst, Point coord)
        {


            int width = dst.Width;
            int height = dst.Height;

            BitmapData dstData = dst.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            BitmapData srcData = src.LockBits(
                new Rectangle(coord.X, coord.Y, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            int dstStride = dstData.Stride;
            int dstOffset = dstStride - width*3;

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
            src.UnlockBits(srcData);
            dst.UnlockBits(dstData);

        }

        public static void DrawImageBack(Bitmap src, Bitmap dst, Point coord)
        {


            int width = dst.Width;
            int height = dst.Height;

            BitmapData dstData = dst.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            BitmapData srcData = src.LockBits(
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
            src.UnlockBits(srcData);
            dst.UnlockBits(dstData);

        }

        #endregion
    }
    enum Position
    {
        up,
        down,
        left,
        right
    }

}
