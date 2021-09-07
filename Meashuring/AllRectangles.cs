using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace ImageProcessor.Meashuring
{
    class AllRectangles
    {
        private ArrayList al = new ArrayList();
        private Bitmap bmp;

        private int selected = -1;

        public int Selected
        {
            get { return selected; }
        }

        public void SetBitmap(Bitmap _bmp)
        {
            if (bmp != null)
                bmp.Dispose();
            bmp = _bmp;
            for (int i=0; i<al.Count; i++)
            {
                Rectangles tmp = (Rectangles)al[i];
                tmp.SetBitmap(bmp);
            }
        }
        
        public void AddRectangle(Point _a1, Point _a2, Bitmap _bmp, Color _color, string _name)
        {
            Meashuring.Rectangles p = new Meashuring.Rectangles(_a1, _a2, _bmp, _color, _name);
            al.Add(p);
        }

        public void Draw()
        {
            for (int i = 0; i < al.Count; i++)
                ((Rectangles)al[i]).allLines.Draw();

        }

        public void Clear()
        {
            al.Clear();
            selected = -1;
        }

        public bool CheckPoint(int x, int y)
        {
            for (int i = 0; i < al.Count; i++)
            {
                Rectangles tmp = (Rectangles)al[i];
                if (tmp.allLines.CheckPoint(x,y))
                {
                    if (selected > -1)
                    {
                        Rectangles tmp_s = (Rectangles)al[selected];
                        tmp_s.isSelected = false;
                        al[selected] = tmp_s;
                        tmp_s.allLines.Draw();
                    }

                    selected = i;
                    tmp.isSelected = true;
                    tmp.allLines.Draw();
                    return true;
                }
                

            }
            return false;
        }

        public bool FixLine(int x, int y)
        {
            for (int i = 0; i < al.Count; i++)
            {
                Rectangles tmp = (Rectangles)al[i];
                if (tmp.allLines.FixLine(x,y))
                {
                    return true;
                }
            }
            return false;
        }

        public AllRectangles()
        {

        }

        public void MoveSelected(int Xs, int Ys)
        {
            Rectangles tmp = (Rectangles)al[selected];
              tmp.allLines.MoveAll(Xs,Ys);
        }

        public void DeletePoint()
        {
            ((Rectangles)al[selected]).allLines.DeletePoint();
            al.RemoveAt(selected);
        }

        public void SetParameters(MembersMeasureManager e)
        {
            for (int i = 0; i < al.Count; i++)
            {
                Rectangles tmp = (Rectangles)al[i];
                tmp.allLines.SetParameters(e);
                al[i] = tmp;
            }
        }
    }
}
