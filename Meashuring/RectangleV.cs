using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ImageProcessor.Meashuring
{
    class RectangleV
    {private PointV a1 = null;
        private PointV a2 = null;
        private bool complete = false;
        private string name = "";
        private System.Drawing.Image img;

        public RectangleV(System.Drawing.Image _img)
        {
            img = _img;
        }

        public bool AddPoint(Point a, string _name)
        {
            if (complete)
                return complete;
            name = name + _name;
            if (a1 == null)
            {
                PointV tmp = new PointV(img, mmem);
                tmp.AddPoint(a, _name);
                a1 = tmp;
                return complete;
            }
            if (a2 == null)
            {
                PointV tmp = new PointV(img, mmem);
                tmp.AddPoint(a, _name);
                a2 = tmp;
                complete = true;
                
            }
            return complete;
        }
    }
}
