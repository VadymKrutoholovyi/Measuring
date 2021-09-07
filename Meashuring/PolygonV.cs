using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace ImageProcessor.Meashuring
{
    class PolygonV
    {
        private ArrayList points = new ArrayList();
        private ArrayList lines = new ArrayList();
        private bool complete = false;
        private string name = "";
        private System.Drawing.Image img;
        private MembersMeasureManager mmem = new MembersMeasureManager();
        public MembersMeasureManager MMEM
        {
            set
            {
                mmem = value;
                for (int i = 0; i < points.Count; i++)
                    ((PointV)points[i]).MMEM = value;
                for (int i = 0; i < lines.Count; i++)
                    ((LineV)lines[i]).MMEM = value;
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
                for (int i = 0; i < points.Count; i++)
                    ((PointV)points[i]).IsSelected = isSelected;
                for (int i = 0; i < lines.Count; i++)
                    ((LineV)lines[i]).IsSelected = isSelected;
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
                for (int i = 0; i < points.Count; i++)
                    ((PointV)points[i]).IsFixed = isFixed;
                for (int i = 0; i < lines.Count; i++)
                    ((LineV)lines[i]).IsFixed = isFixed;
                this.Draw();
            }
        }

        public PolygonV(System.Drawing.Image _img, MembersMeasureManager _mmem)
        {
            img = _img;
            mmem = _mmem;
        }


        public void RecalcPolygon()
        {

            if (points.Count < 2)
                return;
            EraseLines();
            lines.Clear(); 
            for (int i = 0; i < points.Count-1; i++)
            {
                LineV tmpL = new LineV(img, mmem);
                PointV tmp1 = (PointV)points[i];
                PointV tmp2 = (PointV)points[i+1];

                tmpL.AddPoint(tmp1.A1, tmp1.Name);
                tmpL.AddPoint(tmp2.A1, tmp2.Name);

                lines.Add(tmpL);
            
            }
            LineV tmpL_ = new LineV(img, mmem);
            PointV tmp2_ = (PointV)points[0];
            PointV tmp1_ = (PointV)points[points.Count - 1];

            tmpL_.AddPoint(tmp1_.A1, tmp1_.Name);
            tmpL_.AddPoint(tmp2_.A1, tmp2_.Name);

            lines.Add(tmpL_);
            complete = true;
        }

        public bool AddPoint(Point _a1, string _name)
        {
            name = name + _name;
            PointV tmpP = new PointV(img, mmem);
            tmpP.AddPoint(_a1, _name);
            points.Add(tmpP);
            RecalcPolygon();
            return complete;
        }

        public void PutUnder()
        {
            for (int i = 0; i < points.Count; i++)
                ((PointV)points[i]).PutUnder();
            for (int i=0; i<lines.Count; i++)
                ((LineV)lines[i]).PutUnder();
        }

        public void GetUnder()
        {
            for (int i = 0; i < points.Count; i++)
                ((PointV)points[i]).GetUnder();
            for (int i = 0; i < lines.Count; i++)
                ((LineV)lines[i]).GetUnder();
        }

        public void Draw()
        {
            for (int i = 0; i < points.Count; i++)
                ((PointV)points[i]).Draw();
            for (int i = 0; i < lines.Count; i++)
                ((LineV)lines[i]).Draw();
        }

        public void DrawLines()
        {
            PutUnder();
            GetUnder();
            Draw();
        }

        public void ReDrawLines()
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
            for (int i = 0; i < points.Count; i++)
                if (!((PointV)points[i]).CheckMove(Xs, Ys))
                    return false;
            return true;
        }

        public bool MoveLines(int Xs, int Ys)
        {
            for (int i = 0; i < points.Count; i++)
                if (!((PointV)points[i]).CheckMove(Xs, Ys))
                    return false;

            PutUnder();

            for (int i = 0; i < points.Count; i++)
                ((PointV)points[i]).AddSdvig(Xs,Ys);
            for (int i = 0; i < lines.Count; i++)
                ((LineV)lines[i]).AddSdvig(Xs, Ys);

            GetUnder();
            Draw();
            return true;
        }

        public bool IsLines(int x, int y)
        {
            Point tmp = new Point(x, y);
            for (int i = 0; i < points.Count; i++)
            {
                if (((PointV)points[i]).IsPoint(x, y))
                    return true;
            }
            for (int i = 0; i < lines.Count; i++)
                if (((LineV)lines[i]).IsLine(tmp))
                    return true;
            return false;
        }

        public void SetImage(System.Drawing.Image _img)
        {
            img = _img;
            for (int i = 0; i < points.Count; i++)
                ((PointV)points[i]).SetImage(img);
            for (int i = 0; i < lines.Count; i++)
                ((LineV)lines[i]).SetImage(img);
        }

        public string[] GetLetter()
        {
            string[] ret = new string[points.Count];
            for (int i = 0; i < points.Count; i++)
                ret[i] = ((PointV)points[i]).Name;
            return ret;

        }
        public PointV[] GetPoints()
        {
            PointV[] ret = new PointV[points.Count];
            for (int i = 0; i < points.Count; i++)
                ret[i] = (PointV)points[i];
            return ret;
        }

        public MeasurementsMembers GetMeasurements()
        {
            MeasurementsMembers mm = new MeasurementsMembers();
            long Xc = 0;
            long Yc = 0;
            for (int i = 0; i < points.Count; i++)
            {
                Xc += ((PointV)points[i]).A1.X;
                Yc += ((PointV)points[i]).A1.Y;
            }
            Xc = Xc / points.Count;
            Yc = Yc / points.Count;
            mm.Xc = Xc;
            mm.Yc = Yc;

            float perimetr = 0;
            int len = 0;
           
            for (int i = 0; i < lines.Count; i++)
            {
                perimetr += ((LineV)lines[i]).Length;
                len += ((LineV)lines[i]).LenUndo;
            }
            mm.perimetr = perimetr;

            double[] undo = new double[len];
            ArrayList coord = new ArrayList();
            int count = 0;

            int[] lett = new int[points.Count+1];
            lett[0] = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                double[] tmp = ((LineV)lines[i]).Undo;
                ArrayList tmpL = ((LineV)lines[i]).Coord;
                lett[i + 1] = lett[i] + tmp.Length;
                for (int j = 0; j < tmp.Length; j++)
                {
                    undo[count++] = tmp[j];
                    coord.Add(tmpL[j]);
                    
                }
            }

            lett[lett.Length - 1]--;
            mm.name = this.name;
            mm.letters = lett;
            Point2[] pnts = new Point2[points.Count];
            for (int i = 0; i < points.Count; i++)
                pnts[i]= new Point2(((PointV)points[i]).A1);

            mm.area = (float)morfology.PolygonArea(pnts);
            mm.undo = undo;
            mm.formula = this.name + this.name.Substring(0, 1);
            mm.pointsCoordinate = coord;

            return mm;
        }
    }
}
