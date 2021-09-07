using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace ImageProcessor.Meashuring
{
    class AllLines
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
                Lines tmp = (Lines)al[i];
                tmp.SetBitmap(bmp);
            }
        }
        
        public void AddLines(Point _a1, Point _a2, Bitmap _bmp, Color _color, string _name)
        {
            Meashuring.Lines p = new Meashuring.Lines(_a1, _a2, _bmp, _color, _name);
            al.Add(p);
        }

        public void Draw()
        {
            for (int i = 0; i < al.Count; i++)
               ((Lines)al[i]).DrawLine();

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
                Lines tmp = (Lines)al[i];
                if (tmp.IsLine(new Point(x,y)))
                {
                    if (selected > -1)
                    {
                        Lines tmp_s = (Lines)al[selected];
                        tmp_s.isSelected = false;
                        al[selected] = tmp_s;
                        tmp_s.DrawLine();
                    }

                    selected = i;
                    tmp.isSelected = true;
                    tmp.DrawLine();
                    return true;
                }
                

            }
            return false;
        }

        public bool FixLine(int x, int y)
        {
            for (int i = 0; i < al.Count; i++)
            {
                Lines tmp = (Lines)al[i];
                if (tmp.IsLine(new Point(x,y)))
                {
                    tmp.isFixed = !tmp.isFixed;
                    tmp.DrawLine();
                    return true;
                }
            }
            return false;
        }

        public AllLines()
        {

        }

        public void MoveSelected(int Xs, int Ys)
        {
            Lines tmp = (Lines)al[selected];
            tmp.MoveLine(Xs,Ys);
        }

        public void MoveAll(int Xs, int Ys)
        {
            for (int i = 0; i < al.Count; i++)
            {
                Lines tmp = (Lines)al[i];
                tmp.MoveLine(Xs, Ys);
            }
        }

        public void DeletePoint()
        {
            ((Lines)al[selected]).ClearLine();
            al.RemoveAt(selected);
        }

        public void SetParameters(MembersMeasureManager e)
        {
            for (int i = 0; i < al.Count; i++)
            {
                Lines tmp = (Lines)al[i];
                tmp.ColorLine = e.linesColor;
                al[i] = tmp;


            }
        }

    }
}
