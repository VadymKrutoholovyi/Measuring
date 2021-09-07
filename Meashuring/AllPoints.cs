using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace ImageProcessor.Meashuring
{
    
    

    class AllPoints
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
        
        private const int count= 92;
        private ArrayList al = new ArrayList();
        private System.Drawing.Image g;
        private int selected = -1;
        private int old_selected = -1;

        public int Selected
        {
            get { return selected; }
        }



        public void SetGraphics(System.Drawing.Image _g)
        {
            if (g != null)
                g.Dispose();
            g = _g;
        }

        public void AddPoint(int x, int y)
        {
            Meashuring.PointsM p = new ImageProcessor.Meashuring.PointsM(x,y);

            for (int i = 0; i < count; i++)
            {
                if (al[i].ToString() == "")
                {
                    p.LetterText = letter[i];
                    al[i] = p;
                    p.DrawPoint(g, p.Coord);
                    selected = i;
                    return;
                }
                if (((PointsM)al[i]).IsDeleted)
                {
                    p.LetterText = letter[i];
                    al[i] = p;
                    selected = i;
                    p.DrawPoint(g, p.Coord);
                    return;
                }
                
            }
            MessageBox.Show("You have used all available symbols. delete wasted points.");
                
        }

        public void Draw()
        {
            for (int i = 0; i < count; i++)
            {
                if (al[i].ToString() == "")
                    return;
                if (!((PointsM)al[i]).IsDeleted)
                    ((PointsM)al[i]).DrawPoint(g, ((PointsM)al[i]).Coord);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < count; i++)
                al[i]="";
            selected = -1;
            old_selected = -1;
        }

        public bool CheckPoint(int x, int y)
        {
            for (int i = 0; i < count; i++)
            {
                if (al[i].ToString() == "")
                    return false;
                PointsM tmp = (PointsM)al[i];
                if (x >= tmp.Coord.X - tmp.RadiusMax  && x <= tmp.Coord.X + tmp.RadiusMax  && y>=tmp.Coord.Y  - tmp.RadiusMax  && y<=tmp.Coord.Y+ tmp.RadiusMax  && !tmp.IsDeleted)
                {
                    if (selected > -1)
                    {
                        PointsM tmp_s = (PointsM)al[selected];
                        tmp_s.IsSelected = false;
                        al[selected] = tmp_s;
                        tmp_s.DrawPoint(g);
                    }
                    old_selected = selected;
                    selected = i;
                    tmp.IsSelected = true;
                    tmp.DrawPoint(g);
                    return true;
                }
                

            }
            return false;
        }

        public bool FixPoint(int x, int y)
        {
            for (int i = 0; i < count; i++)
            {
                if (al[i].ToString() == "")
                    return false;
                PointsM tmp = (PointsM)al[i];
                if (x >= tmp.Coord.X - tmp.RadiusMax && x <= tmp.Coord.X + tmp.RadiusMax && y >= tmp.Coord.Y - tmp.RadiusMax && y <= tmp.Coord.Y + tmp.RadiusMax && !tmp.IsDeleted)
                {
                    tmp.IsFixed = !tmp.IsFixed;
                    tmp.DrawPoint(g);
                    return true;
                }
            }
            return false;
        }

        public AllPoints()
        {
            for (int i = 0; i < count; i++)
                al.Add("");
        }

        public void MoveSelected(Point k)
        {
            PointsM tmp = (PointsM)al[selected];
            tmp.DrawPoint(g, k);
        }

        public void DeletePoint()
        {
            PointsM tmp = (PointsM)al[selected];
            tmp.DeletePoint(g);
        }

        public void SetParameters(MembersMeasureManager e)
        {
            for (int i = 0; i < count; i++)
            {
                if (al[i].ToString() == "")
                    return;
                PointsM tmp = (PointsM)al[i];
                tmp.ColorCircle = e.pointsColor;
                tmp.RadiusMax = e.radius;
                tmp.LetterFontSize = e.fontSize;
                al[i] = tmp;


            }
        }

        public PointsM GetSelected()
        {
            if (selected == -1)
                return null;
            else
                return (PointsM)al[selected];
        }
        public PointsM GetOldSelected()
        {
            if (old_selected == -1)
                return null;
            else
                return (PointsM)al[old_selected];
        }

    }
}
