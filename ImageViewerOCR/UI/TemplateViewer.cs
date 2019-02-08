/**
Copyright 2011, Cong Nguyen

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
**/

using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using IPoVn.Engine.IPMath;
using Tesseract.OCR.AppEntry.UI;

namespace IPoVn.UI
{
    public class TemplateViewer : UserControl 
    {

        #region Member fields
        private IRenderingData _renderingData = null;
        private IRender _render = null;
        #endregion Member fields

        Rectangle selection;
        Rectangle selection1;
        Rectangle selection2;
        Rectangle selection3;
        Rectangle selection4;
        Rectangle selection5;
        //Object sess;
        Int32 val = 0;
        //Rectangle _selection;
        Rectangle _selection1;
        Rectangle _selection2;
        Rectangle _selection3;
        Rectangle _selection4;
        Rectangle _selection5;
            
        Boolean m_Drawing = false;

        //Point m_Start;
        Point m_Start1;
        Point m_Start2;
        Point m_Start3;
        Point m_Start4;
        Point m_Start5;

        SolidBrush hbr = new SolidBrush(Color.FromArgb(128, Color.Blue));

        #region Constructors and destructors

        public TemplateViewer()
        {
            this.SetStyle(
                ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);

            this.UpdateStyles();

            this.Initialize();
        }

        public void Initialize()
        {
        }

        public void Initialize(IRenderingData renderingData, IRender render)
        {
            _render = render;
            _renderingData = renderingData;
            val = 0;
            
        }
        #endregion Constructors and destructors

        #region Properties
        public Image Image
        {
            get
            {
                if (_renderingData != null)
                {
                    return _renderingData.Image;
                }

                return null;
            }
            set
            {
                if (_renderingData != null)
                {
                    _renderingData.Image = value;
                    if (_renderingData.IsDataChanged)
                    {
                        OnImageChanged();
                        _renderingData.IsDataChanged = false;
                                                
                    }
                }
            }
        }


        private void OnImageChanged()
        {
            this.Visible &= (this.Image != null);

            // correct size
            if (this.Visible)
                this.Size = new Size(this.Image.Width, this.Image.Height);
             else
                this.Size = new Size(10, 10);

            // require to redraw
            this.Invalidate(true);
                      
        }


        public IRenderingData RenderingData
        {
            get { return _renderingData; }
            set { _renderingData = value; }
        }


        public IRender Render
        {
            get { return _render; }
            set { _render = value; }
        }

        public Rectangle RectA
        {
            get { return _selection1; }
            set { selection1 = value; }
        }
        public Rectangle RectB
        {
            get { return _selection2; }
            set { selection2 = value; }
        }
        public Rectangle RectC
        {
            get { return _selection3; }
            set { selection3 = value; }
        }
        public Rectangle RectD
        {
            get { return _selection4; }
            set { selection4 = value; }
        }
        public Rectangle RectE
        {
            get { return _selection5; }
            set { selection5 = value; }
        }
        
        public void DrawRect(Rectangle rec)
        {
                       
            //Pen newp = new Pen(Color.Green, 1.0f);
            Graphics grph = this.CreateGraphics();
            //grph.DrawRectangle(newp, rec);
            grph.FillRectangle(hbr, rec); 
                        

        }

        public void ClearRect()
        {
            val = 0;

            _selection1 = new Rectangle(0, 0, 0, 0);
            _selection2 = new Rectangle(0, 0, 0, 0);
            _selection3 = new Rectangle(0, 0, 0, 0);
            _selection4 = new Rectangle(0, 0, 0, 0);
            _selection5 = new Rectangle(0, 0, 0, 0);

            selection1 = new Rectangle(0, 0, 0, 0);
            selection2 = new Rectangle(0, 0, 0, 0);
            selection3 = new Rectangle(0, 0, 0, 0);
            selection4 = new Rectangle(0, 0, 0, 0);
            selection5 = new Rectangle(0, 0, 0, 0);
               

        }

        #endregion Properties

        #region Overrides
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (_render != null && _renderingData != null)
                {
                    _render.DoRender(e.Graphics, _renderingData);
                }
                if (selection1 != null)
                {
                    //'There is a selection rectangle so draw it
                    e.Graphics.FillRectangle(hbr, selection1);
                    e.Graphics.FillRectangle(hbr, selection2);
                    e.Graphics.FillRectangle(hbr, selection3);
                    e.Graphics.FillRectangle(hbr, selection4);
                    e.Graphics.FillRectangle(hbr, selection5);

                    _selection1 = selection1;
                    _selection2 = selection2;
                    _selection3 = selection3;
                    _selection4 = selection4;
                    _selection5 = selection5;
                    
                }  
            }
            catch
            {
                // nothing
            }
            
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            //Remember where mouse was pressed
            //m_Start = e.Location;
            //'Clear the selection rectangle

            // m_Start1 = e.Location; selection = new Rectangle(0, 0, 0, 0); 
            if (val == 0) { m_Start1 = e.Location; selection1 = new Rectangle(0, 0, 0, 0);}
            if (val == 1) { m_Start2 = e.Location; selection2 = new Rectangle(0, 0, 0, 0);}
            if (val == 2) { m_Start3 = e.Location; selection3 = new Rectangle(0, 0, 0, 0);}
            if (val == 3) { m_Start4 = e.Location; selection4 = new Rectangle(0, 0, 0, 0);}
            if (val == 4) { m_Start5 = e.Location; selection5 = new Rectangle(0, 0, 0, 0);}
                                     
            this.Invalidate(true);
            //'Selection drawing is on
            m_Drawing = true;
                       
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (m_Drawing == true)
            {
                //if (val == 0) { selection = RectangleFromPoints(m_Start, e.Location); }
                if (val == 0) 
                { 
                    selection1 = RectangleFromPoints(m_Start1, e.Location);
                   
                }
                if (val == 1)
                { 
                    selection2 = RectangleFromPoints(m_Start2, e.Location);
                 
                }
                if (val == 2) 
                { 
                    selection3 = RectangleFromPoints(m_Start3, e.Location);
                   
                }
                if (val == 3)
                { 
                    selection4 = RectangleFromPoints(m_Start4, e.Location);
                   
                }
                if (val == 4)
                { 
                    selection5 = RectangleFromPoints(m_Start5, e.Location);

                }
                
                this.Invalidate();
                
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            m_Drawing = false;
            //val++;
            if (val == 0)
            {
                if (selection1.X == 0) { return; }
                else
                {
                    val = 1;
                    return;
                }
                
            }
            if (val == 1)
            {
                if (selection2.X == 0) { return; }
                else
                {
                    val = 2;
                    return;
                }
            }
            if (val == 2)
            {
                if (selection3.X == 0) { return; }
                else
                {
                    val = 3;
                    return;
                }
            }
            if (val == 3)
            {
                if (selection4.X == 0) { return; }
                else
                {
                    val = 4;
                    return;
                }
            }
            if (val == 4)
            {
                if (selection5.X == 0) { return; }
                else
                {
                    val = 5;
                    return;
                }
            }
        }

        private Rectangle RectangleFromPoints(Point p1, Point p2)
        {

            Int32 x;
            Int32 y;

            if (p1.X <= p2.X)
            {
                x = p1.X;
            }
            else
            {
                x = p2.X;
            }

            if (p1.Y <= p2.Y)
            {
                y = p1.Y;
            }
            else
            {
                y = p2.Y;
            }

            return new Rectangle(x, y, Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));

        } 

        private void DrawPolygon(Graphics grph, Brush brush, Pen pen, Polygon region)
        {
            if (grph == null || region == null)
                return;

            PointF[] points = new PointF[region.nPoints];
            for (int i = 0; i < region.nPoints; i++)
            {
                points[i] = new PointF((float)region.XPoints[i], (float)region.YPoints[i]);
            }

            using (Pen newp = new Pen(Color.Green, 1.0f))
            {
                Pen[] pens = new Pen[] { pen, newp };
                for (int i = 0; i < region.nPoints - 1; i++)
                {
                    Pen p = pens[(i + 1) % 2];
                    grph.DrawLine(p, points[i], points[i + 1]);
                }

                grph.DrawLine(pens[region.nPoints % 2], points[region.nPoints - 1], points[0]);
            }
            return;



            //grph.FillPolygon(brush, points);
            grph.DrawPolygon(pen, points);
        }
        #endregion Overrides

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ImageViewer
            // 
            this.Name = "TemplateViewer";
            this.Size = new System.Drawing.Size(267, 213);
            this.ResumeLayout(false);

        }

       
            #region Events
            #endregion Events

            #region Methods
            #endregion Methods

            #region Helepers
            #endregion Helepers
    }
}
