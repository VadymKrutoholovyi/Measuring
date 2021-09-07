using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace ImageProcessor.Meashuring
{
    class RectanglesV
    {
        private PointV a1;
        private PointV a2;
        private LineV l1;
        private LineV l2;
        private LineV l3;
        private LineV l4;

        private bool complete = false;
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
                if (a1 == null || a2 == null) return;
                isSelected = value;
                a1.IsSelected = isSelected;
                a2.IsSelected = isSelected;
                l1.IsSelected = isSelected;
                l2.IsSelected = isSelected;
                l3.IsSelected = isSelected;
                l4.IsSelected = isSelected;

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
                if (a1 == null || a2 == null) return;
                isFixed = value;
                a1.IsFixed = isFixed;
                a2.IsFixed = isFixed;
                l1.IsFixed = isFixed;
                l2.IsFixed = isFixed;
                l3.IsFixed = isFixed;
                l4.IsFixed = isFixed;
                this.Draw();
            }
        }
        private MembersMeasureManager mmem = new MembersMeasureManager();
        public MembersMeasureManager MMEM
        {
            set
            {
                mmem = value;
                if (a1 == null) return;
                a1.MMEM = value;
                if (a2 == null) return;
                a2.MMEM = value;
                l1.MMEM = value;
                l2.MMEM = value;
                l3.MMEM = value;
                l4.MMEM = value;
            }
        }

        public RectanglesV(System.Drawing.Image _img, MembersMeasureManager _mmem)
        {
            img = _img;
            mmem = _mmem;
        }

        public void RecalcRectangles()
        {
            l1 = new LineV(img, mmem);
            l2 = new LineV(img, mmem);
            l3 = new LineV(img, mmem);
            l4 = new LineV(img, mmem);

            l1.AddPoint(a1.A1, a1.Name);
            l1.AddPoint(new Point(a2.A1.X, a1.A1.Y), "");

            l2.AddPoint(new Point(a2.A1.X, a1.A1.Y), "");
            l2.AddPoint(a2.A1, a2.Name);

            l3.AddPoint(a2.A1, a2.Name);
            l3.AddPoint(new Point(a1.A1.X, a2.A1.Y), "");

            l4.AddPoint(new Point(a1.A1.X, a2.A1.Y), "");
            l4.AddPoint(a1.A1, a1.Name);

            complete = true;
        }

        public bool AddPoint(Point _a, string _name)
        {
            PointV tmp;
            

            if (complete) return complete;
            name = name + _name;
            if (a1 == null)
            {
                tmp = new PointV(img, mmem);
                tmp.AddPoint(_a, _name);
                a1 = tmp;
                return complete;
            }
            if (a2 == null)
            {
                tmp = new PointV(img, mmem);
                tmp.AddPoint(_a, _name);
                a2 = tmp;
                RecalcRectangles();

            }
            return complete;

        }

        public void PutUnder()
        {
            if (a1 == null) return;
            a1.PutUnder();
            if (a2 == null) return;
            a2.PutUnder();
            l1.PutUnder();
            l2.PutUnder();
            l3.PutUnder();
            l4.PutUnder();
        }

        public void GetUnder()
        {
            if (a1 == null) return;
            a1.GetUnder();
            if (a2 == null) return;
            a2.GetUnder();
            l1.GetUnder();
            l2.GetUnder();
            l3.GetUnder();
            l4.GetUnder();
        }

        public void Draw()
        {
            if (a1 == null) return;
            a1.Draw();
            if (a2 == null) return;
            a2.Draw();
            l1.Draw();
            l2.Draw();
            l3.Draw();
            l4.Draw();
        }

        public void DrawRectangle()
        {
            PutUnder();
            GetUnder();
            Draw();
        }

        public void ReDrawRectangle()
        {
            GetUnder();
            Draw();
        }

        public void EraseLines()
        {
            PutUnder();
        }
        public bool CheckMove(int Xs, int Ys)
        {
            if (!a1.CheckMove(Xs, Ys) || !a2.CheckMove(Xs, Ys))
                    return false;
            return true;
        }

        public bool MoveLines(int Xs, int Ys)
        {
            if (!CheckMove(Xs, Ys))
                 return false;

            PutUnder();
            a1.AddSdvig(Xs, Ys);
            a2.AddSdvig(Xs, Ys);
            l1.AddSdvig(Xs, Ys);
            l2.AddSdvig(Xs, Ys);
            l3.AddSdvig(Xs, Ys);
            l4.AddSdvig(Xs, Ys);
            GetUnder();
            Draw();
            return true;
        }

        public bool IsLines(int x, int y)
        {
            if (a1 == null || a2 == null ) return false; 
            Point tmp = new Point(x, y);
            if (a1.IsPoint(x, y))
                return true;
            if (a2.IsPoint(x, y))
                return true;

            if (l1.IsLine(tmp))
                return true;
            if (l2.IsLine(tmp))
                return true;
            if (l3.IsLine(tmp))
                return true;
            if (l4.IsLine(tmp))
                return true;

            return false;
        }

        public void SetImage(System.Drawing.Image _img)
        {
            img = _img;
            if (a1 == null) return;
            a1.SetImage(img);
            if (a2 == null) return;
            a2.SetImage(img);
            l1.SetImage(img);
            l2.SetImage(img);
            l3.SetImage(img);
            l4.SetImage(img);
        }

        public string[] GetLetter()
        {
            string[] ret2 = new string[2];
            string[] ret1 = new string[1];
            if (a1 == null) return null;
            if (a2 == null)
            {
                ret1[0] = a1.Name;
                return ret1;
            }
            ret2[0] = a1.Name;
            ret2[1] = a2.Name;
            return ret2;
        }

        public PointV[] GetPoints()
        {
            PointV[] ret2 = new PointV[2];
            PointV[] ret1 = new PointV[1];
            if (a1 == null) return null;
            if (a2 == null)
            {
                ret1[0] = a1;
                return ret1;
            }
            ret2[0] = a1;
            ret2[1] = a2;
            return ret2;
        }

        public MeasurementsMembers GetMeasurements()
        {
            MeasurementsMembers mm = new MeasurementsMembers();
            mm.perimetr = l1.Length + l2.Length + l3.Length + l4.Length;
            mm.area = l1.Length * l2.Length;
            mm.Xc = (a1.A1.X + a2.A1.X) / 2;
            mm.Yc = (a1.A1.Y + a2.A1.Y) / 2;
            mm.name = this.name;
            ArrayList coord = new ArrayList();
            int[] lett = new int[5];
            lett[0] = 0;
            int count = 0;
            double[] undo = new double[l1.LenUndo + l2.LenUndo + l3.LenUndo + l4.LenUndo];
            double[] tmp = l1.Undo;
            ArrayList tmpA = l1.Coord;
            lett[1] = lett[0] + tmp.Length;
            for (int j = 0; j < tmp.Length; j++)
            {
                undo[count++] = tmp[j];
                coord.Add(tmpA[j]);
            }
            tmpA.Clear();
            tmp = l2.Undo;
            tmpA = l2.Coord;
            lett[2] = lett[1] + tmp.Length;
            for (int j = 0; j < tmp.Length; j++)
            {
                undo[count++] = tmp[j];
                coord.Add(tmpA[j]);
            }
            tmpA.Clear();
            tmp = l3.Undo;
            tmpA = l3.Coord;
            lett[3] = lett[2] + tmp.Length -1;
            for (int j = 0; j < tmp.Length; j++)
            {
                undo[count++] = tmp[j];
                coord.Add(tmpA[j]);
            }
            tmpA.Clear();
            tmp = l4.Undo;
            tmpA = l4.Coord;
            lett[4] = lett[3] + tmp.Length;
            for (int j = 0; j < tmp.Length; j++)
            {
                undo[count++] = tmp[j];
                coord.Add(tmpA[j]);
            }
            mm.letters = lett;
            mm.undo = undo;
            mm.pointsCoordinate = coord;
            mm.formula = this.name.Substring(0, 1) + "2" + this.name.Substring(1, 1) + "4" + this.name.Substring(0, 1);
            return mm;
        }
    }
}
