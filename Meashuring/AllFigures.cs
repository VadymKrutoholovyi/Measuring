using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace ImageProcessor.Meashuring
{
    public class AllFigures
    {
        private string[] letter = {   "A","B","C","D","E","F","G","H","I",
									  "J","K","L","M","N","O","P","Q","R",
                                      "S","T","U","V","W","X","Y","Z","0",
                                      "1","2","3","4","5","6","7","8","9",
                                      "a","b","c","d","e","f","g","h","i",
									  "j","k","l","m","n","o","p","q","r",
                                      "s","t","u","v","w","x","y","z",")",
                                      "!","@","#","$","%","^","&","*","(",
                                      "-","_","+","=","|","{","}","[","]",
                                      ":","'",";","<",">",",",".","?","~",
                                      "`","/"};

        private const int count = 92;
        private ArrayList al_letter = new ArrayList();
        
        
        private ArrayList al = new ArrayList();
        

        private System.Drawing.Image img;
        private MembersMeasureManager mmem = new MembersMeasureManager();
        public MembersMeasureManager MMEM
        {
            set
            {
                mmem = value;
                for (int i = al.Count - 1; i >= 0; i--)
                    ((Figure)al[i]).MMEM = value;
            }
        }
        

        private string GetLetter()
        {
            string name = "";
         
            for (int i = 0; i < count; i++)
            {
                if ((string)al_letter[i] == "")
                {
                    name = letter[i];
                    al_letter[i] = letter[i];
                    return name;
                }
                
            }
            if (name == "")
                MessageBox.Show("You have used all available symbols. delete wasted points.");
            return name;
        }

        private void FreeLetter(string[] letters)
        {
            for (int i = 0; i < letters.Length; i++)
                for (int j = 0; j < count; j++)
                    if (letters[i] == letter[j])
                        al_letter[j] = "";

        }


        public AllFigures(System.Drawing.Image _img, MembersMeasureManager _mmem)
        {
            al_letter.Clear();
            img = _img;
            mmem = _mmem;
            for (int i = 0; i < count; i++)
                al_letter.Add("");
        }

        private void GetAllLetter()
        { 
            al_letter.Clear();
            for (int i = 0; i < count; i++)
                al_letter.Add("");
            for (int i = 0; i < all_points.Count; i++)
                for (int j = 0; j < letter.Length; j++ )
                    if (((PointV)all_points[i]).Name == letter[j])
                    {
                        al_letter[j] = letter[j];
                        break;
                    }
        }

        public void ClearUnCompleted()
        {
            for (int i = 0; i < al.Count; i++)
            {
                if (!((Figure)al[i]).Complete)
                    ((Figure)al[i]).Remove();
            }
            for (int i = al.Count-1; i >= 0; i--)
                if (!((Figure)al[i]).Complete)
                    al.RemoveAt(i);
            GetAllPoints();
            GetAllLetter();
        }

        public TypeFigure GetLastType()
        {
            if (al.Count == 0) return TypeFigure.nothing;
            return((Figure)al[al.Count - 1]).Type_Figure;
        }

        public bool GetLastComplete()
        {
            return ((Figure)al[al.Count - 1]).Complete;
        }

        public void Add(TypeFigure _typeFigure)
        {
            if (_typeFigure == TypeFigure.nothing) return;
            ClearUnCompleted();
            Figure fig = new Figure(_typeFigure, img, mmem);
            al.Add(fig);

        }
        
        
        public void AddPoint(Point _point)
        {
            if (al.Count == 0) return;
            string name = GetLetter();
            if (name != "")
                ((Figure)al[al.Count-1]).AddPoint(_point, name);
            
        }
       

        public int Check(int x, int y)
        {
            for (int i = 0; i < al.Count; i++)
            {
                if (((Figure)al[i]).IsFigure(x, y))
                    return i;
            }
            return -1;
        }

        public void SetSelected(int num)
        {
            for (int i = 0; i < al.Count; i++)
            {
                if (((Figure)al[i]).IsSelected)
                    ((Figure)al[i]).IsSelected = false;
            }
            ((Figure)al[num]).IsSelected = true;
        }

        public void SetFixed(int num)
        {

            ((Figure)al[num]).IsFixed = !((Figure)al[num]).IsFixed;

        }

        public void SetImage(System.Drawing.Image _img)
        {
            img = _img;
            for (int i = 0; i < al.Count; i++)
                ((Figure)al[i]).SetImage(img);

        }

        public int Count()
        {
            return al.Count;
        }

        public void DrawThick(int thickness, Color color)
        {

            if (thickness < 2) return;
            Figure tmp = null;
            Graphics g = Graphics.FromImage(img);
            PointV[] pointsV;
            Point[] points;
            Pen pen = new Pen(color, thickness);
            for (int i = 0; i < al.Count; i++)
            {
                tmp = (Figure)al[i];
                if (tmp.Complete)
                {
                    pointsV = tmp.GetPoints();
                    points = new Point[pointsV.Length];
                    for (int j = 0; j < points.Length; j++)
                        points[j] = pointsV[j].A1;
                    switch (tmp.Type_Figure)
                    {
                        case TypeFigure.lines:
                            g.DrawLines(pen, points);
                            break;
                        case TypeFigure.circle:
                            int rad = (int)tmp.GetRadius();
                            g.DrawEllipse(pen, points[0].X - rad, points[0].Y - rad, rad * 2, rad * 2);
                            break;
                        case TypeFigure.rectangle:
                            Point[] p = new Point[4];
                            p[0] = new Point(points[0].X, points[0].Y);
                            p[1] = new Point(points[1].X, points[0].Y);
                            p[2] = new Point(points[1].X, points[1].Y);
                            p[3] = new Point(points[0].X, points[1].Y);


                            g.DrawPolygon(pen, p);
                            break;
                        case TypeFigure.polygon:
                            g.DrawPolygon(pen, points);
                            break;
                    }
                }

            }

        }


        public bool IsFixed(int num)
        {
            return ((Figure)al[num]).IsFixed;
        }


        public void MoveSelected(int Xs, int Ys)
        {
            for (int i = 0; i < al.Count; i++)
            {
                if (((Figure)al[i]).IsFixed)
                {

                    if (!((Figure)al[i]).CheckMove(Xs, Ys))
                        return;
                }
            }
            for (int i = 0; i < al.Count; i++)
            {
                if (((Figure)al[i]).IsFixed)
                {

                    ((Figure)al[i]).Move(Xs, Ys);
                }
            }
        }

        public void Redraw()
        { 
              for (int i = 0; i < al.Count; i++)
                ((Figure)al[i]).ReDraw();  
        }
        public void RemoveFixed()
        {
            for (int i = 0; i < al.Count; i++)
                if (((Figure)al[i]).IsFixed)
                {
                    ((Figure)al[i]).Remove();
                    al_letter[i] = "";
                }
            for (int i = al.Count-1; i >= 0; i--)
                if (((Figure)al[i]).IsFixed)

                    al.RemoveAt(i);

        }

        public void RemoveAll()
        {
           al.Clear();
           all_numbers.Clear();
           all_points.Clear();
           for (int j = 0; j < count; j++)
                   al_letter[j] = "";
        }
        private ArrayList all_points = new ArrayList();
        private ArrayList all_numbers = new ArrayList();
        private void GetAllPoints()
        {
            all_points.Clear();
            all_numbers.Clear();

            for (int i = 0; i < al.Count; i++)
            {
                PointV[] tmp = ((Figure)al[i]).GetPoints();
                if (tmp != null)
                    for (int j = 0; j < tmp.Length; j++)
                    {
                        all_points.Add(tmp[j]);
                        all_numbers.Add(i);
                    }
            }
        }
        public int currentPoint=-1;
        private int currentFigNum = -1;
        public void CheckPoints(int x, int y)
        {
            GetAllPoints();
            for (int i = 0; i < all_points.Count; i++)
                if(((PointV)all_points[i]).IsPoint(x, y))
                {
                    currentPoint = i;
                    currentFigNum = (int)all_numbers[i];
                    return;
                }
            currentPoint = -1;
            currentFigNum = -1;

        }

        public void MoveOnePoint(int Xs, int Ys)
        {

            if (currentPoint == -1) return;

            if (!((PointV)all_points[currentPoint]).CheckMove(Xs,Ys))
                return;

            if (((Figure)al[currentFigNum]).Type_Figure == TypeFigure.circle)
            {
                ((PointV)all_points[currentPoint]).MovePoint(Xs, Ys);
                bool ch = ((Figure)al[currentFigNum]).CheckMove(0, 0);
                ((PointV)all_points[currentPoint]).MovePoint(-Xs, -Ys);
                if (!ch)
                    return;
            }

            ((Figure)al[currentFigNum]).Remove();
            ((PointV)all_points[currentPoint]).MovePoint(Xs, Ys);
            ((Figure)al[currentFigNum]).Recalc();
            ((Figure)al[currentFigNum]).Draw();


        }
        public MeasurementsMembers[] GetMeasurements()
        {
            MeasurementsMembers[] mm = new MeasurementsMembers[al.Count];
            for (int i = 0; i < al.Count; i++)
            {
                mm[i] = ((Figure)al[i]).GetMeasurements();
                mm[i].number = i;
            }
            return mm;
        }

        public float GetPerimeter(int num)
        {
            MeasurementsMembers m = ((Figure)al[num]).GetMeasurements();
            return m.perimetr;
        }

        public void UncheckFix()
        {
            for (int i = 0; i < al.Count; i++)
            {
                ((Figure)al[i]).IsFixed = false;
                
            }
        }
        public void CheckFixNum(int num)
        {
            ((Figure)al[num]).IsFixed = true;
        }

        public ArrayList GetCoordinatesByLetter(string letter)
        {
            MeasurementsMembers m_m;
            ArrayList ret= new ArrayList();
            Point tmp= new Point(-1,-1);
            for (int i = 0; i < al.Count; i++)
            {
                m_m = ((Figure)al[i]).GetMeasurements();
                if (m_m.name == letter)
                {
                    tmp.X = (int)m_m.Xc;
                    tmp.Y = (int)m_m.Yc;
                    ret.Add(tmp);
                    return ret;
                }
            }
            return ret;
        }
        public ArrayList GetCoordinatesAllPoints()
        {
            ArrayList ret = new ArrayList();
            MeasurementsMembers m_m;
            Point tmp= new Point(-1,-1);
            for (int i = 0; i < al.Count; i++)
            {
                m_m = ((Figure)al[i]).GetMeasurements();
                tmp.X = (int)m_m.Xc;
                tmp.Y = (int)m_m.Yc;
                ret.Add(tmp);
            }
            return ret;
        }

    }

}
