using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace ImageProcessor.Meashuring
{
    class Figure
    {
        private TypeFigure typeFigure = TypeFigure.nothing;
        public TypeFigure Type_Figure
        {
            get
            {
                return typeFigure;
            }
            set
            {
                typeFigure = value;
            }
        
        }
        private CirclesV circle = null;
        private RectanglesV rectangle = null;
        private PointV point = null;
        private LinesV lines = null;
        private PolygonV polygons = null;
        private bool complete = false;
        public bool Complete
        {
            get
            {
                return complete;
            }
            set
            {
                complete = value;
            }
        }

        private MembersMeasureManager mmem = new MembersMeasureManager();
        public MembersMeasureManager MMEM
        {
            set
            {
                mmem = value;
                switch (typeFigure)
                {
                    case TypeFigure.point:
                        point.MMEM = value;
                        break;
                    case TypeFigure.lines:
                        lines.MMEM = value;
                        break;
                    case TypeFigure.circle:
                        circle.MMEM = value;
                        break;
                    case TypeFigure.rectangle:
                        rectangle.MMEM = value;
                        break;
                    case TypeFigure.polygon:
                        polygons.MMEM = value;
                        break;
                    case TypeFigure.nothing:
                        break;
                }
            }
        }
        private string name = "";
        private bool isSelected = false;
        private System.Drawing.Image img;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                switch (typeFigure)
                {
                    case TypeFigure.point:
                        point.IsSelected=IsSelected;
                        break;
                    case TypeFigure.lines:
                        lines.IsSelected = IsSelected;
                        break;
                    case TypeFigure.circle:
                        circle.IsSelected = IsSelected;
                        break;
                    case TypeFigure.rectangle:
                        rectangle.IsSelected = IsSelected;
                        break;
                    case TypeFigure.polygon:
                        polygons.IsSelected = IsSelected;
                        break;
                    case TypeFigure.nothing:
                        break;
                }
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
                switch (typeFigure)
                {
                    case TypeFigure.point:
                        point.IsFixed = isFixed;
                        break;
                    case TypeFigure.lines:
                        lines.IsFixed = IsFixed;
                        break;
                    case TypeFigure.circle:
                        circle.IsFixed = IsFixed;
                        break;
                    case TypeFigure.rectangle:
                        rectangle.IsFixed = IsFixed;
                        break;
                    case TypeFigure.polygon:
                        polygons.IsFixed = IsFixed;
                        break;
                    case TypeFigure.nothing:
                        break;
                }
            }
        }

        public Figure(TypeFigure _typeFigure, System.Drawing.Image _img, MembersMeasureManager _mmem)
        {
            typeFigure = _typeFigure;
            img = _img;
            mmem = _mmem;
            switch (typeFigure)
            {
                case TypeFigure.point:
                    point = new PointV(img, mmem);
                    break;
                case TypeFigure.lines:
                    lines = new LinesV(img, mmem);
                    break;
                case TypeFigure.circle:
                    circle = new CirclesV(img, mmem);
                    break;
                case TypeFigure.rectangle:
                    rectangle = new RectanglesV(img, mmem);
                    break;
                case TypeFigure.polygon:
                    polygons = new PolygonV(img, mmem);
                    break;
                case TypeFigure.nothing:
                    break;
            }

        }

        public bool AddPoint(Point _point, string _name)
        {
            
            name = name + _name;
            switch (typeFigure)
            {
                case TypeFigure.point:
                    if (complete) return complete;
                    complete = point.AddPoint(_point, _name);
                    point.DrawPoint();
                    break;
                case TypeFigure.lines:
                    complete = lines.AddPoint(_point, _name);
                    lines.DrawLines();
                    break;
                case TypeFigure.circle:
                    complete = circle.AddPoint(_point, _name);
                    circle.DrawCircle();
                    break;
                case TypeFigure.rectangle:
                    complete = rectangle.AddPoint(_point, _name);
                    rectangle.DrawRectangle();
                    break;
                case TypeFigure.polygon:
                    complete = polygons.AddPoint(_point, _name);
                    polygons.DrawLines();
                    break;
                case TypeFigure.nothing:
                    break;
            }
            return complete;
        }

        public void Draw()
        {
            switch (typeFigure)
            {
                case TypeFigure.point:
                    point.DrawPoint();
                    break;
                case TypeFigure.lines:
                    lines.DrawLines();
                    break;
                case TypeFigure.circle:
                    circle.DrawCircle();
                    break;
                case TypeFigure.rectangle:
                    rectangle.DrawRectangle();
                    break;
                case TypeFigure.polygon:
                    polygons.DrawLines();
                    break;
                case TypeFigure.nothing:
                    break;
            }
        }

        public void ReDraw()
        {
            switch (typeFigure)
            {
                case TypeFigure.point:
                    point.ReDrawPoint();
                    break;
                case TypeFigure.lines:
                    lines.ReDrawLines();
                    break;
                case TypeFigure.circle:
                    circle.ReDrawCircle();
                    break;
                case TypeFigure.rectangle:
                    rectangle.ReDrawRectangle();
                    break;
                case TypeFigure.polygon:
                    polygons.ReDrawLines();
                    break;
                case TypeFigure.nothing:
                    break;
            }
        }



        public void Clear()
        {

        }

        public string[] GetLetter()
        {
            string[] ret = null;
            switch (typeFigure)
            {
                case TypeFigure.point:
                    ret = new string[1];
                    ret[0] = point.Name;
                    return ret;

                case TypeFigure.lines:
                    ret = lines.GetLetter();
                    break;
                case TypeFigure.circle:
                    ret = circle.GetLetter();
                    break;
                case TypeFigure.rectangle:
                    ret = rectangle.GetLetter();
                    break;
                case TypeFigure.polygon:
                    ret = polygons.GetLetter();
                    break;
                case TypeFigure.nothing:
                    break;
            }
            return ret;
        }

        public void Remove()
        {
            switch (typeFigure)
            {
                case TypeFigure.point:
                    point.ErasePoint();
                    break;
                case TypeFigure.lines:
                    lines.EraseLines();
                    break;
                case TypeFigure.circle:
                    circle.EraseLines();
                    break;
                case TypeFigure.rectangle:
                    rectangle.EraseLines();
                    break;
                case TypeFigure.polygon:
                    polygons.EraseLines();
                    break;
                case TypeFigure.nothing:
                    break;
            }
        }

        public bool IsFigure(int x, int y)
        {
            switch (typeFigure)
            {
                case TypeFigure.point:
                    return point.IsPoint(x, y);
                case TypeFigure.lines:
                    return lines.IsLines(x, y);

                case TypeFigure.circle:
                    return circle.IsLines(x, y);
                    
                case TypeFigure.rectangle:
                    return rectangle.IsLines(x, y);
                    
                case TypeFigure.polygon:
                    return polygons.IsLines(x, y);
                    
                case TypeFigure.nothing:
                    break;
            }
            return false;
        }

       

        public bool CheckMove(int Xs, int Ys)
        {
            switch (typeFigure)
            {
                case TypeFigure.point:
                    return point.CheckMove(Xs, Ys);

                case TypeFigure.lines:
                    return lines.CheckMove(Xs, Ys);
                    
                case TypeFigure.circle:
                    return circle.CheckMove(Xs, Ys);
                    
                case TypeFigure.rectangle:
                    return rectangle.CheckMove(Xs, Ys);
                    
                case TypeFigure.polygon:
                    return polygons.CheckMove(Xs, Ys);
                    
                case TypeFigure.nothing:
                    break;
            }
            return false;
        }

        public void Move(int Xs, int Ys)
        {
            switch (typeFigure)
            {
                case TypeFigure.point:
                    point.MovePoint(Xs, Ys);
                    break;
                case TypeFigure.lines:
                    lines.MoveLines(Xs, Ys);
                    break;
                case TypeFigure.circle:
                    circle.MoveLines(Xs, Ys);
                    break;
                case TypeFigure.rectangle:
                    rectangle.MoveLines(Xs, Ys);
                    break;
                case TypeFigure.polygon:
                    polygons.MoveLines(Xs, Ys);
                    break;
                case TypeFigure.nothing:
                    break;
            }
        }

        public void SetImage(System.Drawing.Image _img)
        {
            img = _img;
            switch (typeFigure)
            {
                case TypeFigure.point:
                    point.SetImage(img);
                    break;
                case TypeFigure.lines:
                    lines.SetImage(img);
                    break;
                case TypeFigure.circle:
                    circle.SetImage(img);
                    break;
                case TypeFigure.rectangle:
                    rectangle.SetImage(img);
                    break;
                case TypeFigure.polygon:
                    polygons.SetImage(img);
                    break;
                case TypeFigure.nothing:
                    break;
            }
        }

        public float GetRadius()
        {
            if (typeFigure == TypeFigure.circle)
                return circle.Radius;
            return 0;
        }

        public PointV[] GetPoints()
        {
            switch (typeFigure)
            {
                case TypeFigure.point:
                    return point.GetPoints();
                case TypeFigure.lines:
                    return lines.GetPoints();
                case TypeFigure.circle:
                    return circle.GetPoints();
                case TypeFigure.rectangle:
                    return rectangle.GetPoints();
                case TypeFigure.polygon:
                    return polygons.GetPoints();
                case TypeFigure.nothing:
                    break;
            }
            return null;
        }

        public void Recalc()
        {
            switch (typeFigure)
            {
                case TypeFigure.point:
                    break;
                case TypeFigure.lines:
                    lines.RecalcLines();
                    break;
                case TypeFigure.circle:
                    circle.RecalcCircles();
                    break;
                case TypeFigure.rectangle:
                    rectangle.RecalcRectangles();
                    break;
                case TypeFigure.polygon:
                    polygons.RecalcPolygon();
                    break;
                case TypeFigure.nothing:
                    break;
            }
        }

        public MeasurementsMembers GetMeasurements()
        {
            MeasurementsMembers mm = new MeasurementsMembers();
            if (this.complete)
                switch (typeFigure)
                {
                    case TypeFigure.point:
                        mm = point.GetMeasuremets();
                        mm.type = TypeFigure.point.ToString();
                        
                        break;
                    case TypeFigure.lines:
                        mm = lines.GetMeasurements();
                        mm.type = TypeFigure.lines.ToString();
                        break;
                    case TypeFigure.circle:
                        mm = circle.GetMeasurements();
                        mm.type = TypeFigure.circle.ToString();
                        break;
                    case TypeFigure.rectangle:
                        mm = rectangle.GetMeasurements();
                        mm.type = TypeFigure.rectangle.ToString();
                        break;
                    case TypeFigure.polygon:
                        mm = polygons.GetMeasurements();
                        mm.type = TypeFigure.polygon.ToString();
                        break;
                    case TypeFigure.nothing:
                        break;
                }
            mm.complete = complete;
            return mm;
        }
    }
    public enum TypeFigure
    { 
        point,
        lines,
        circle,
        rectangle,
        polygon,
        nothing
    }

    public class MeasurementsMembers
    {
        public string name = "-";
        public float perimetr = 0;
        public float area = 0;
        public float angle = 0;
        public float Xc = 0;
        public float Yc = 0;
        public float radius = 0;
        public string type="";
        public int number = -1;
        public double[] undo = null;
        public int[] letters = null;
        public string formula="";
        public bool complete = false;
        public ArrayList pointsCoordinate=new ArrayList();

        public MeasurementsMembers()
        { }
    }
}
