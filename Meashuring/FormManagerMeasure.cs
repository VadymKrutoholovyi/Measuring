using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ImageProcessor3D;

namespace ImageProcessor.Meashuring
{
    public partial class FormManagerMeasure : Form
    {
        
        private MembersMeasureManager mmem = new MembersMeasureManager();
        private bool flag = false;
        private MembersScaleFactor msf = new MembersScaleFactor();
        public Bitmap bmp = null;
        public bool dfd = false;

        public bool Is_Visible()
        {
            return this.Visible;
        }

        public void CloseManager()
        {
            this.Close();
        }

        public DataGridViewRowCollection GetDataFromGrid()
        {
            return dg.Rows;
        }

        public MembersScaleFactor Msf
        {
            get
            {
                return msf;
            }
            set
            {
                msf = value;
                FillGrid();
            }
        }
        private MeasurementsMembers[] mm = null;
        public MeasurementsMembers[] Mm
        {
            set
            {
                mm = value;
                FillGrid();
            }
        }


        public MembersMeasureManager Mmem
        {
            get
            {
                return mmem;
            }
            set
            {
                mmem = value;
                Set();
               
            }

        }
        public delegate void BuildHandler(MembersMeasureManager e);

        
        public event BuildHandler SettingsReady;
        public event BuildHandler AddFigure;
        public event BuildHandler ChangeFigure;
        public event BuildHandler RemoveFigure;
        public event BuildHandler ClearFigure;
        public event BuildHandler RedrawFigure;
        public event BuildHandler FixChanges;
        public event BuildHandler Original;


        //public delegate void ClosedHandler( object sender );
        //public event ClosedHandler MenagedClosed;

        public delegate void CutPartsHandler(MembersMeasureManager e, typeCutting l);
        public event CutPartsHandler CutParts;

        public delegate void ScaleHandler(MembersScaleFactor e);
        public event ScaleHandler ScaleChange;


        public delegate void ClickHandler(int e);
        public event ClickHandler ClickFigure;

        public delegate void MoveHandler(Point e);
        public event MoveHandler MoveReady;


        

        public FormManagerMeasure(MembersMeasureManager _mmem)
        {


            InitializeComponent();
            flag = true;
            if (_mmem == null)
                Deserialization();
            else
                mmem = _mmem;
            Set();
            flag = false;

            this.controlProphiles1.MoveReady += new ImageProcessor3D.ControlProphiles.MoveHandler(controlProphiles_MoveReady);
            this.controlProphiles1.ScanReady += new ImageProcessor3D.ControlProphiles.ScanHandler(controlProphiles_ScanReady);
            
        }

        public void SetMSF(MembersScaleFactor _msf)
        {
            msf = ImageProcessor3D.MembersScaleFactor.Deserialization(dfd);
            
        }

        private void controlProphiles_MoveReady(Point e)
        {
            if (MoveReady !=null)
                MoveReady(e);
            
            
        }

        private void controlProphiles_ScanReady(bool scanX, bool scanY, int step)
        {

        }

        private void Set()
        {
            FillGrid();
            flag = true;

            tbRadius.Text = mmem.radius.ToString();
            tbFont.Text = mmem.fontSize.ToString();
            tbThickness.Text = mmem.thickness.ToString();

            btPoint.BackColor = mmem.pointsColor;
            btLine.BackColor = mmem.linesColor;

            flag = false;
        }

        private void btSaveImage_Click(object sender, EventArgs e)
        {
            TurnOffButtons();
            if (ClearFigure != null)
                ClearFigure(mmem);
        }

        public void SetColor(Color backColor, Color foreColor)
        {
            this.panel0.BackColor = backColor;
            dg.ColumnHeadersDefaultCellStyle.BackColor = backColor;
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            
        }

        #region Ser-Deser

        public void Serialization()
        {
            Stream stream = File.Open(Application.StartupPath + @"\mmem.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, mmem);
            stream.Close();
        }
        public void Deserialization()
        {
            Stream stream;
            try
            {
                stream = File.Open(Application.StartupPath + @"\mmem.dat", FileMode.Open);
            }
            catch
            {
                mmem = new MembersMeasureManager();
                return;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                mmem = (MembersMeasureManager)formatter.Deserialize(stream);
            }
            catch
            {
                mmem = new MembersMeasureManager();
                stream.Close();
            }
            stream.Close();


        }

        #endregion

        private void btPoint_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            btPoint.BackColor = colorDialog1.Color;
            mmem.pointsColor = colorDialog1.Color;
            CommonFunctions.SetForeColor(sender);
            if (SettingsReady != null)
                SettingsReady(mmem);
        }

        private void btLine_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            btLine.BackColor = colorDialog1.Color;
            mmem.linesColor = colorDialog1.Color;
            CommonFunctions.SetForeColor(sender);
            if (SettingsReady != null)
                SettingsReady(mmem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void tbRadius_Leave(object sender, EventArgs e)
        {
            if (flag) return;
            float ret;
            try
            {
                ret = Convert.ToSingle(((TextBox)sender).Text);
                switch (Convert.ToInt16(((TextBox)sender).Tag))
                {


                    case 0:
                        mmem.radius = (int)ret;
                        if (mmem.radius < 1)
                            mmem.radius = 1;
                        tbRadius.Text = mmem.radius.ToString();
                        if (SettingsReady != null)
                            SettingsReady(mmem);
                        break;
                    case 1:
                        mmem.fontSize = (int)ret;
                        if (mmem.fontSize < 4)
                            mmem.fontSize = 4;
                        tbFont.Text = mmem.fontSize.ToString();
                        if (SettingsReady != null)
                            SettingsReady(mmem);
                        break;
                    case 2:
                        mmem.thickness = (int)ret;
                        if (mmem.thickness < 1)
                            mmem.fontSize = 1;
                        tbThickness.Text = mmem.thickness.ToString();
                        if (SettingsReady != null)
                            SettingsReady(mmem);
                        break;

                }
            }
            catch
            {
                MessageBox.Show("incorrect value");
                ((TextBox)sender).Text = "";
                ((TextBox)sender).Focus();
            }

        }

        private void tbRadius_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbRadius_Leave(sender, null);
        }


        private void btDefault_Click(object sender, EventArgs e)
        {
            TurnOffButtons();
            Deserialization();
            Set();
            if (SettingsReady != null)
                SettingsReady(mmem);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            Serialization();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (AddFigure != null)
                AddFigure(mmem);
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            TurnOffButtons();
            if (AddFigure != null)
                RemoveFigure(mmem);
            if (RedrawFigure != null)
                RedrawFigure(mmem);
        }




        private void btRedraw_Click(object sender, EventArgs e)
        {
            if (RedrawFigure != null)
                RedrawFigure(mmem);
        }

        private void btProphile_Click(object sender, EventArgs e)
        {
            if (this.Height == this.panelTop.Height + 33)
                this.Height = 350;
            else
                this.Height = this.panelTop.Height + 33;
        }
        public double vert_scale = 1;
        public void EmptyTable()
        {
            dg.Rows.Clear();
            controlProphiles1.ClearChart();
        }

        public void FillGrid()
        {
            msf = MembersScaleFactor.Deserialization(dfd);
            if (dg.ColumnCount == 0)
            {
                controlProphiles1.ClearChart();
                return;
            }
            if (mm==null) return;
            if (mm.Length == 0)
            {
                dg.Rows.Clear();
                return;
            }
            dg.Rows.Clear();

            int k = msf.meashurement.IndexOf("(");
            string un = msf.meashurement;
            if (k > 0)
                un = un.Substring(k, un.Length - k);


            for (int i = 0; i<mm.Length; i++)
            {
                float tmp = 0;
                dg.Rows.Add();
                dg.Rows[i].Cells[0].Value = mm[i].name;
                dg.Rows[i].Cells[1].Value = mm[i].type;
                tmp = (float)(mm[i].perimetr * msf.scaleFactor);
                if (tmp==0)
                    dg.Rows[i].Cells[2].Value = "-";
                else
                    dg.Rows[i].Cells[2].Value = tmp.ToString("F2") + " " + un;
                tmp = (float)(mm[i].area * msf.scaleFactor * msf.scaleFactor);
                if (tmp == 0)
                    dg.Rows[i].Cells[3].Value = "-";
                else
                    dg.Rows[i].Cells[3].Value = tmp.ToString("F2") + " sq." + un;

                if (mm[i].angle==0)
                    dg.Rows[i].Cells[4].Value = "-";
                else
                    dg.Rows[i].Cells[4].Value = mm[i].angle.ToString("F1") + " degree";
                tmp = (float)(mm[i].radius * msf.scaleFactor);
                if (tmp==0)
                    dg.Rows[i].Cells[5].Value = "-";
                else
                    dg.Rows[i].Cells[5].Value = tmp.ToString("F2") + " " + un;
                tmp = (float)(mm[i].Xc * msf.scaleFactor);
                if (tmp == 0)
                    dg.Rows[i].Cells[6].Value = "-";
                else
                    dg.Rows[i].Cells[6].Value = tmp.ToString("F2") + " " + un;
                tmp =  (float)(mm[i].Yc * msf.scaleFactor);
                if (tmp == 0)
                    dg.Rows[i].Cells[7].Value = "-";
                else
                    dg.Rows[i].Cells[7].Value = tmp.ToString("F2") + " " + un;
                dg.Rows[i].Cells[8].Value = mm[i].number;

            }
            dg.Refresh();
            this.Refresh();
            if (mm[0].complete && mm[0].type != TypeFigure.point.ToString())
                controlProphiles1.NewChart(mm[0].undo,mm[0].pointsCoordinate, bmp, "Profile " + mm[0].type + " " + mm[0].name, mm[0].letters, mm[0].formula, mm[0].perimetr * msf.scaleFactor, msf.meashurement, mm[0].pointsCoordinate, vert_scale);
            else
                controlProphiles1.ClearChart();
        }
        
        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int num = (int)dg.Rows[e.RowIndex].Cells[8].Value;
            if (ClickFigure != null)
                ClickFigure(num);
            if (mm[num].complete && mm[num].type != TypeFigure.point.ToString())
                controlProphiles1.NewChart(mm[num].undo, mm[num].pointsCoordinate, bmp, "Profile " + mm[num].type + " " + mm[num].name, mm[num].letters, mm[num].formula, mm[num].perimetr * msf.scaleFactor, msf.meashurement, mm[num].pointsCoordinate, vert_scale);
            else
                controlProphiles1.ClearChart();
  
        }

        private void cbLetters_CheckedChanged(object sender, EventArgs e)
        {
            mmem.letters = cbLetters.Checked;
            if (SettingsReady != null)
                SettingsReady(mmem);

        }

        private void btPoints_Click(object sender, EventArgs e)
        {
            //btLines.BackColor = panel0.BackColor;
            //btPolygons.BackColor = panel0.BackColor;
            //btRectangles.BackColor = panel0.BackColor;
            //btCircles.BackColor = panel0.BackColor;

            btLines.BackColor = SystemColors.InactiveCaptionText;
            btPolygons.BackColor = SystemColors.InactiveCaptionText;
            btRectangles.BackColor = SystemColors.InactiveCaptionText;
            btCircles.BackColor = SystemColors.InactiveCaptionText;

            btLines.ForeColor = SystemColors.Info;
            btPolygons.ForeColor = SystemColors.Info;
            btRectangles.ForeColor = SystemColors.Info;
            btCircles.ForeColor = SystemColors.Info;

            if (btPoints.BackColor == SystemColors.ControlLight)
            {
                btPoints.BackColor = SystemColors.InactiveCaptionText;
                btPoints.ForeColor = SystemColors.Info;
                mmem.typeDraw = TypeFigure.nothing;
            }
            else
            {
                btPoints.BackColor = SystemColors.ControlLight;
                btPoints.ForeColor = SystemColors.ControlDark;
                mmem.typeDraw = TypeFigure.point;
            }
            if (ChangeFigure != null)
                ChangeFigure(mmem);
        }

        private void btLines_Click(object sender, EventArgs e)
        {
            //btPoints.BackColor = panel0.BackColor;
            //btPolygons.BackColor = panel0.BackColor;
            //btRectangles.BackColor = panel0.BackColor;
            //btCircles.BackColor = panel0.BackColor;

            //btLines.BackColor = SystemColors.InactiveCaptionText;
            btPoints.BackColor = SystemColors.InactiveCaptionText;
            btPolygons.BackColor = SystemColors.InactiveCaptionText;
            btRectangles.BackColor = SystemColors.InactiveCaptionText;
            btCircles.BackColor = SystemColors.InactiveCaptionText;

            btPoints.ForeColor = SystemColors.Info;
            btPolygons.ForeColor = SystemColors.Info;
            btRectangles.ForeColor = SystemColors.Info;
            btCircles.ForeColor = SystemColors.Info;

            if (btLines.BackColor == SystemColors.ControlLight)
            {
                btLines.BackColor = SystemColors.InactiveCaptionText;
                btLines.ForeColor = SystemColors.Info;
                mmem.typeDraw = TypeFigure.nothing;
            }
            else
            {
                btLines.BackColor = SystemColors.ControlLight;
                btLines.ForeColor = SystemColors.ControlDark;
                mmem.typeDraw = TypeFigure.lines;
            }
            if (ChangeFigure != null)
                ChangeFigure(mmem);
        }

        private void btPolygons_Click(object sender, EventArgs e)
        {
            //btLines.BackColor = panel0.BackColor;
            //btPoints.BackColor = panel0.BackColor;
            //btRectangles.BackColor = panel0.BackColor;
            //btCircles.BackColor = panel0.BackColor;

            btLines.BackColor = SystemColors.InactiveCaptionText;
            btPoints.BackColor = SystemColors.InactiveCaptionText;
            btRectangles.BackColor = SystemColors.InactiveCaptionText;
            btCircles.BackColor = SystemColors.InactiveCaptionText;

            btPoints.ForeColor = SystemColors.Info;
            btLines.ForeColor = SystemColors.Info;
            btRectangles.ForeColor = SystemColors.Info;
            btCircles.ForeColor = SystemColors.Info;

            if (btPolygons.BackColor == SystemColors.ControlLight)
            {
                btPolygons.BackColor = SystemColors.InactiveCaptionText;
                btPolygons.ForeColor = SystemColors.Info;
                mmem.typeDraw = TypeFigure.nothing;
            }
            else
            {
                btPolygons.BackColor = SystemColors.ControlLight;
                btPolygons.ForeColor = SystemColors.ControlDark;
                mmem.typeDraw = TypeFigure.polygon;
            }
            if (ChangeFigure != null)
                ChangeFigure(mmem);
        }

        private void btRectangles_Click(object sender, EventArgs e)
        {
            //btPoints.BackColor = panel0.BackColor;
            //btLines.BackColor = panel0.BackColor;
            //btPolygons.BackColor = panel0.BackColor;
            //btCircles.BackColor = panel0.BackColor;

            btLines.BackColor = SystemColors.InactiveCaptionText;
            btPoints.BackColor = SystemColors.InactiveCaptionText;
            btPolygons.BackColor = SystemColors.InactiveCaptionText;
            btCircles.BackColor = SystemColors.InactiveCaptionText;

            btPoints.ForeColor = SystemColors.Info;
            btLines.ForeColor = SystemColors.Info;
            btPolygons.ForeColor = SystemColors.Info;
            btCircles.ForeColor = SystemColors.Info;

            if (btRectangles.BackColor == SystemColors.ControlLight)
            {
                btRectangles.BackColor = SystemColors.InactiveCaptionText;
                btRectangles.ForeColor = SystemColors.Info;
                mmem.typeDraw = TypeFigure.nothing;
            }
            else
            {
                btRectangles.BackColor = SystemColors.ControlLight;
                btRectangles.ForeColor = SystemColors.ControlDark;
                mmem.typeDraw = TypeFigure.rectangle;
            }
            if (ChangeFigure != null)
                ChangeFigure(mmem);
        }

        private void btCircles_Click(object sender, EventArgs e)
        {
            btLines.BackColor = SystemColors.InactiveCaptionText;
            btPoints.BackColor = SystemColors.InactiveCaptionText;
            btPolygons.BackColor = SystemColors.InactiveCaptionText;
            btRectangles.BackColor = SystemColors.InactiveCaptionText;

            btPoints.ForeColor = SystemColors.Info;
            btLines.ForeColor = SystemColors.Info;
            btPolygons.ForeColor = SystemColors.Info;
            btRectangles.ForeColor = SystemColors.Info;


            if (btCircles.BackColor == SystemColors.ControlLight)
            {
                btCircles.BackColor = SystemColors.InactiveCaptionText;
                btCircles.ForeColor = SystemColors.Info;
                mmem.typeDraw = TypeFigure.nothing;
            }
            else
            {
                btCircles.BackColor = SystemColors.ControlLight;
                btCircles.ForeColor = SystemColors.ControlDark;
                mmem.typeDraw = TypeFigure.circle;
            }
            if (ChangeFigure != null)
                ChangeFigure(mmem);

        }

        public void TurnOffButtons()
        {
            //btPoints.BackColor = panel0.BackColor;
            //btLines.BackColor = panel0.BackColor;
            //btPolygons.BackColor = panel0.BackColor;
            //btRectangles.BackColor = panel0.BackColor;
            //btCircles.BackColor = panel0.BackColor;
            btLines.BackColor = SystemColors.InactiveCaptionText;
            btPoints.BackColor = SystemColors.InactiveCaptionText;
            btPolygons.BackColor = SystemColors.InactiveCaptionText;
            btRectangles.BackColor = SystemColors.InactiveCaptionText;
            btCircles.BackColor = SystemColors.InactiveCaptionText;

            btPoints.ForeColor = SystemColors.Info;
            btLines.ForeColor = SystemColors.Info;
            btPolygons.ForeColor = SystemColors.Info;
            btRectangles.ForeColor = SystemColors.Info;
            btCircles.ForeColor = SystemColors.Info;

            mmem.typeDraw = TypeFigure.nothing;
            if (ChangeFigure != null)
                ChangeFigure(mmem);

        }

        private void dg_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double t = Convert.ToDouble(dg.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                double k = Convert.ToDouble(mm[e.RowIndex].perimetr);
                msf.scaleFactor = t / k;
                if (ScaleChange != null)
                    ScaleChange(msf);
                MembersScaleFactor.Serialization(msf);
                FillGrid();
                MessageBox.Show("Scale factor is changed!");
            }
            catch
            {
                MessageBox.Show("incorrect value");
            }

        }

        private void btCutLeft_Click(object sender, EventArgs e)
        {
            if (CutParts != null)
                CutParts(mmem, typeCutting.leftRight);
        }

        private void btCutRight_Click(object sender, EventArgs e)
        {
            if (CutParts != null)
                CutParts(mmem, typeCutting.rightLeft);
        }

        private void btCutProfile_Click(object sender, EventArgs e)
        {
            if (CutParts != null)
                CutParts(mmem, typeCutting.profile);
        }

        private void btTop_Click(object sender, EventArgs e)
        {
            if (CutParts != null)
                CutParts(mmem, typeCutting.top);
        }

        private void btBottom_Click(object sender, EventArgs e)
        {
            if (CutParts != null)
                CutParts(mmem, typeCutting.bottom);
        }

        private void btFix_Click(object sender, EventArgs e)
        {
            if (FixChanges != null)
                FixChanges(mmem);
        }

        private void btOriginal_Click(object sender, EventArgs e)
        {
            if (Original != null)
                Original(mmem);
        }

      /*  private void FormManagerMeasure_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MenagedClosed != null)
                MenagedClosed(this);
        }
       */
    }



    [Serializable()]
    public class MembersMeasureManager
    {
        
        public int fontSize = 12;
        public int radius = 10;
        public int thickness = 1;
        public Color pointsColor = Color.Red;
        public Color linesColor = Color.Green;
        public bool letters = true;
        public TypeFigure typeDraw = TypeFigure.nothing;

        public MembersMeasureManager()
        {

        }
        public static void Serialization(MembersMeasureManager mmem)
        {
            Stream stream = File.Open(Application.StartupPath + @"\mmem.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, mmem);
            
            stream.Close();
        }
        public static MembersMeasureManager Deserialization()
        {
            MembersMeasureManager ret;
            Stream stream;
            try
            {
                stream = File.Open(Application.StartupPath + @"\mmem.dat", FileMode.Open);
            }
            catch
            {
                ret = new MembersMeasureManager();
                return ret;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                ret = (MembersMeasureManager)formatter.Deserialize(stream);
            }
            catch
            {
                ret = new MembersMeasureManager();
                stream.Close();
            }
            stream.Close();
            ret.typeDraw = TypeFigure.nothing;
            return ret;

        }
    }

    public enum typeCutting
    {
        leftRight = 0,
        rightLeft = 1,
        profile = 2,
        top=3,
        bottom=4
    }
}