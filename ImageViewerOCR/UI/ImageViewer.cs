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
    public class ImageViewer : UserControl 
    {

        #region Member fields
        private IRenderingData _renderingData = null;
        private IRender _render = null;
        #endregion Member fields

        Rectangle selection;
        Object sess;
        Int32 val;
        Rectangle _selection;
        Boolean m_Drawing = false;
        Point m_Start;
       
        SolidBrush hbr = new SolidBrush(Color.FromArgb(128, Color.Blue));

        #region Constructors and destructors

        public ImageViewer()
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

        public Rectangle  RectA
        {
            get { return _selection; }
            set { selection = value; }
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
                if (selection != null)
                {
                    //'There is a selection rectangle so draw it
                    e.Graphics.FillRectangle(hbr, selection);
                    _selection = selection;
                    
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
            m_Start = e.Location;
            //'Clear the selection rectangle
                                  
            this.Invalidate(true);
            //'Selection drawing is on
            m_Drawing = true;
                       
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (m_Drawing == true)
            {
                selection = RectangleFromPoints(m_Start, e.Location);
                
                this.Invalidate();
                
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            m_Drawing = false;
            val++;
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
            this.Name = "ImageViewer";
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
