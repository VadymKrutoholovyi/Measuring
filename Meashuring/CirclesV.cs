using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace ImageProcessor.Meashuring
{
    class CirclesV
    {
        private PointV a1;
        private PointV a2;
        private CircleV circle;
        private bool complete = false;
        private string name = "";
        private System.Drawing.Image img;

        private bool isSelected = false;

        public float Radius
        {
            get
            {
                if (circle != null)
                    return circle.Radius;
                else
                    return 0.0f;
            }
        }

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
                circle.IsSelected = isSelected;
                
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
                circle.IsFixed = isFixed;
                
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
                circle.MMEM = value;
            }
        }

        public CirclesV(System.Drawing.Image _img, MembersMeasureManager _mmem)
        {
            img = _img;
            mmem = _mmem;
        }

        public void RecalcCircles()
        {
            circle = new CircleV(img, mmem);
            circle.AddPoint(a1.A1, a1.Name);
            circle.AddPoint(a2.A1, a2.Name);
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
                if (!CheckNew(a1.A1, _a))
                    return complete;
                tmp = new PointV(img, mmem);
                tmp.AddPoint(_a, _name);
                a2 = tmp;
                RecalcCircles();

            }
            return complete;

        }

        public void PutUnder()
        {
            if (a1!=null)
                a1.PutUnder();
            if (a2 != null)
                a2.PutUnder();
            if (circle!=null)
                circle.PutUnder();
        }

        public void GetUnder()
        {
            if (a1 != null)
                a1.GetUnder();
            if (a2 != null)
                a2.GetUnder();
            if (circle != null)
                circle.GetUnder();
        }

        public void Draw()
        {
            if (a1 != null)
                a1.Draw();
            if (a2 != null)
                a2.Draw();
            if (circle != null)
                circle.Draw();
        }

        public void DrawCircle()
        {
            PutUnder();
            GetUnder();
            Draw();
        }

        public void ReDrawCircle()
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
            Point q1 = new Point(a1.A1.X + Xs, a1.A1.Y + Ys);
            Point q2 = new Point(a2.A1.X + Xs, a2.A1.Y + Ys);
            int radius_tmp = (int)calcMetrics.CalcSizeF(q1, q2) + 1;

            if (q1.X - radius_tmp <= 0 || q1.X + radius_tmp >= img.Width ||
                q1.Y - radius_tmp <= 0 || q1.Y + radius_tmp >= img.Height)
                return false;

            return true;
        }

        private bool CheckNew(Point q1, Point q2)
        {
            int radius_tmp = (int)calcMetrics.CalcSizeF(q1, q2) + 1;

            if (q1.X - radius_tmp <= 0 || q1.X + radius_tmp >= img.Width ||
                q1.Y - radius_tmp <= 0 || q1.Y + radius_tmp >= img.Height)
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
            circle.AddSdvig(Xs, Ys);
            GetUnder();
            Draw();
            return true;
        }

        public bool IsLines(int x, int y)
        {
            if (a1 == null || a2 == null || circle == null) return false; 
            Point tmp = new Point(x, y);
            if (a1.IsPoint(x, y))
                return true;
            if (a2.IsPoint(x, y))
                return true;

            if (circle.IsLine(tmp))
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
            if (circle == null) return;
            circle.SetImage(img);
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
            mm.name = this.name;
            mm.radius = circle.Radius;
            mm.area = circle.Area;
            mm.perimetr = circle.Perimetr;
            mm.Xc = a1.A1.X;
            mm.Yc = a1.A1.Y;
            mm.undo = circle.Undo;
            int[] let = new int[2];
            let[0] = 0;
            let[1] = mm.undo.Length-1;
            mm.letters = let;
            mm.formula = "00";
            mm.pointsCoordinate = circle.Coord;
            return mm;
        }
    }
}
