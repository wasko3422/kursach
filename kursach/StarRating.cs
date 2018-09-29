using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace kursach {

    sealed class StarRating : PictureBox
    {
        public StarRating()
        {
            _pOutlineNorm = new Pen(_normOutlineColor);
            _pOutlineFaint = new Pen(_faintOutlineColor);
            DoubleBuffered = true;
            MaximumSize = new Size(700, 80);
            MinimumSize = new Size(100, 1);
        }

        enum FillState
        {
            None,         
            HoverFilled,  
            PermaFilled   
        }

        public bool Rated { get;  set; }
        public short? Rating { get; private set; }

        public Dictionary<int, int> pairs = new Dictionary<int, int>();

        public gost gost2;


        private readonly List<FillState> _stars = new List<FillState>(5) { FillState.None, FillState.None, FillState.None, FillState.None, FillState.None };
        private readonly List<FillState> _savedStars = new List<FillState>(5) { FillState.None, FillState.None, FillState.None, FillState.None, FillState.None };

        readonly GraphicsPath _gp = new GraphicsPath();

        private readonly Pen _pOutlineNorm;
        private Color _normOutlineColor = Color.Yellow;
        public Color NormalOutlineColor
        {
            get { return _normOutlineColor; }
            set
            {
                _pOutlineNorm.Color = value;
                _normOutlineColor = value;
            }
        }

        private readonly Pen _pOutlineFaint;
        private Color _faintOutlineColor = Color.Yellow;


        public Color FaintOutlineColor
        {
            get { return _faintOutlineColor; }
            set
            {
                _pOutlineFaint.Color = value;
                _faintOutlineColor = value;
            }
        }

        private Color _starFillColor = Color.Yellow;
        public Color StarFillColor
        {
            get { return _starFillColor; }
            set { _starFillColor = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            _gp.ClearMarkers();

            int numFilled = 0;


            for (int i = 0; i < 5; i++)
            {
                FillState fs = Rated ? _savedStars[i] : _stars[i];
                DrawStar(e.Graphics, fs, i );
                if (fs != FillState.None) numFilled++;

            }

            e.Graphics.SetClip(new RectangleF(0, 0, (numFilled * 20) + 1, Height), CombineMode.Replace);
            using (LinearGradientBrush lgb = new LinearGradientBrush(DisplayRectangle, _starFillColor, _starFillColor, 90f))
            {
                e.Graphics.FillPath(lgb, _gp);
            }
            e.Graphics.ResetClip();
        }

 
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!Rated)
            {
                int upperRating = e.X / 40 + 1;
                for (int i = 0; i < 5; i++)
                {
                    if (i < upperRating)
                    {
                        _stars[i] = FillState.HoverFilled;
                    }
                    else
                    {
                        _stars[i] = FillState.None;
                    }
                }
                Invalidate();
            }
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (Rated)
            {
                return;
            }
            Rated = true;
            Rating = 0;
            for (int i = 0; i < 5; i++)
            {
                if (_stars[i] == FillState.HoverFilled)
                {
                    _savedStars[i] = FillState.PermaFilled;
                    Rating++;
                }
                else
                {
                    _savedStars[i] = FillState.None;
                }
            }
            try
            {
                gost2.dataGridView1.CurrentRow.Cells[3].Value = Math.Round(((double)(Convert.ToDouble(gost2.dataGridView1.CurrentRow.Cells[3].Value) + Rating) / 2), 1);
                gost2.indexs.Add(gost2.dataGridView1.CurrentRow.Index);
                pairs.Add(gost2.dataGridView1.CurrentRow.Index, Convert.ToInt32(Rating));
                Invalidate();
            }
            catch
            {
                MessageBox.Show("Выберите книгу, чтобы поставить оценку!");
                clear();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!Rated)
            {
                for (int i = 0; i < 5; i++)
                {
                    _stars[i] = FillState.None;
                }
                Invalidate();
            }
        }

        public void redraw()
        {
            if (!Rated)
            {
                for (int i = 0; i < 5; i++)
                {
                    _stars[i] = FillState.None;
                }
                Invalidate();
            }
        }

        public void fullfill(int x)
        {
            for (int i = 0; i < pairs[x] ; i++)
            {
                _stars[i] = FillState.HoverFilled;
                _savedStars[i] = FillState.PermaFilled;
            }

            Invalidate();
        }

        public void clear()
        {
            for (int i = 0; i < 5; i++)
            {
                _stars[i] = FillState.None ;
                _savedStars[i] = FillState.None;
            }
            Invalidate();
        }


        private void DrawStar(Graphics graphics, FillState fillState, int left)
        {
            Point[] star = new[] {
                            new Point( (40 * left) + 2   , 12),
                            new Point((40 * left) + 16   , 12),
                            new Point((40 * left) + 20    , 0),
                            new Point((40 * left) + 24    , 12),
                            new Point((40 * left) + 38    , 12),
                            new Point((40 * left) + 28    , 20),
                            new Point((40 * left) + 36    , 36),
                            new Point((40 * left) + 20    , 26),
                            new Point((40 * left) + 4     , 36),
                            new Point((40 * left) + 12    , 20)
        };

            using (LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(new Point(left, 0), new Size(50, 50)), _starFillColor,  _starFillColor, 90f))
            {
                if (fillState != FillState.PermaFilled)
                {
                    graphics.DrawPolygon(_pOutlineFaint, star);
                    if (fillState == FillState.HoverFilled)
                    {
                        lgb.LinearColors = new[] { Color.FromArgb(100, _starFillColor), Color.FromArgb(1, _starFillColor) };
                        graphics.FillPolygon(lgb, star);
                    }
                }
                else
                {
                    graphics.DrawPolygon(_pOutlineNorm, star);
                    if (fillState == FillState.PermaFilled)
                    {
                        graphics.FillPolygon(lgb, star);
                    }
                }
            }
        }
    }
}