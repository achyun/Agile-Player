// This file is part of Agile Player
// An Audio player with downsampler, upsampler and bit-converter
// written in C#.
// 
// Copyright © Alaa Ibrahim Hadid 2022
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// Author email: mailto:alaahadidfreeware@gmail.com
//
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using APlayer.Core;

namespace APlayer
{
    public partial class MediaBar : Control
    {
        public MediaBar()
        {
            InitializeComponent();
            ControlStyles flag = ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint;
            this.SetStyle(flag, true);
            TickTextColor = Color.Black;
            MediaRecColor = Color.Lime;
            TickColor = Color.White;
            ToolTipColor = Color.Blue;
            ToolTipTextColor = Color.White;
            MediaLineColor = Color.White;

            ticks_spacing = 5;

            mediaRectHeight = 5;
            mediaDuration = 0;
            timeSpace = 1000;
            MilliPixel = 1;
            ViewPortOffset = 0;
            currentTime = 0;

            // Ticks buffer contain positions of ticks on the bar across the width of the control
            TicksBuffer = new List<int>();
            // Ticks buffer contain positions of seconds with big tick on the bar across the width of the control
            SecsBuffer = new List<int>();
            // Times buffer contain strings of seconds on second positions
            TimesBuffer = new List<string>();
            TimesPositionsBuffer = new List<int>();
            TimesSizes = new List<Size>();
            CaclulateMillipixels();
            toolTipTimer.Tick += toolTipTimer_Tick;
        }
        private int ticks_spacing;
        private double mediaDuration;
        public int timeSpace;
        private double currentTime;
        // Ticks buffer contain positions of ticks on the bar across the width of the control
        private List<int> TicksBuffer = new List<int>();
        // Ticks buffer contain positions of seconds with big tick on the bar across the width of the control
        private List<int> SecsBuffer = new List<int>();
        // Times buffer contain strings of seconds on second positions
        private List<string> TimesBuffer = new List<string>();
        private List<int> TimesPositionsBuffer = new List<int>();
        private List<Size> TimesSizes = new List<Size>();

        private string toolTipText = "";
        private Size toolTipSize = Size.Empty;
        private Point toolTipLocation = new Point();
        private Timer toolTipTimer = new Timer();
        private int toolTipTimerCounter = 3;
        private bool showToolTip = false;
        private bool isTimeCursorMove;
        private int toolTipMaxX = 0;
        public int MediaLineX = 0;
        private bool doInvalidateOnTick;

        private Rectangle MediaRectanlge;
        private int mediaRectHeight = 10;

        // Colors   
        private Color color_tick_text = Color.Lime;
        private Color color_media_rec = Color.Lime;
        private Color color_ticks = Color.White;
        private Color color_media_line = Color.White;
        private Color color_tooltip = Color.White;
        private Color color_tooltip_text = Color.White;

        // Properties
        /// <summary>
        /// Get or set the tick color
        /// </summary>
        public Color MediaRecColor
        {
            get { return color_media_rec; }
            set
            {
                if (value == null)
                    return;
                color_media_rec = value;
                MediaRecSolideBrush = new SolidBrush(color_media_rec);
            }
        }
        public SolidBrush MediaRecSolideBrush { get; set; }


        /// <summary>
        /// Get or set the tick color
        /// </summary>
        public Color TickColor
        {
            get { return color_ticks; }
            set
            {
                if (value == null)
                    return;
                color_ticks = value;
                TicksSolideBrush = new SolidBrush(color_ticks);
                TicksPen = new Pen(TicksSolideBrush);
                TicksPen2 = new Pen(TicksSolideBrush, 2);
            }
        }
        public SolidBrush TicksSolideBrush { get; set; }
        public Pen TicksPen { get; set; }
        public Pen TicksPen2 { get; set; }

        /// <summary>
        /// Get or set the tick color
        /// </summary>
        public Color TickTextColor
        {
            get { return color_tick_text; }
            set
            {
                if (value == null)
                    return;
                color_tick_text = value;
                TickTextSolideBrush = new SolidBrush(color_tick_text);
                TickTextPen = new Pen(TickTextSolideBrush);
                TickTextPen2 = new Pen(TickTextSolideBrush, 2);
            }
        }
        public SolidBrush TickTextSolideBrush { get; set; }
        public Pen TickTextPen { get; set; }
        public Pen TickTextPen2 { get; set; }

        /// <summary>
        /// Get or set the media line color
        /// </summary>
        public Color MediaLineColor
        {
            get { return color_media_line; }
            set
            {
                if (value == null)
                    return;
                color_media_line = value;
                MediaLineSolideBrush = new SolidBrush(color_media_line);
                MediaLinePen = new Pen(MediaLineSolideBrush);
                MediaLinePen2 = new Pen(MediaLineSolideBrush, 2);
            }
        }
        public SolidBrush MediaLineSolideBrush { get; set; }
        public Pen MediaLinePen { get; set; }
        public Pen MediaLinePen2 { get; set; }

        /// <summary>
        /// Get or set the tick color
        /// </summary>
        public Color ToolTipColor
        {
            get { return color_tooltip; }
            set
            {
                if (value == null)
                    return;
                color_tooltip = value;
                ToolTipSolideBrush = new SolidBrush(color_tooltip);
                ToolTipPen = new Pen(ToolTipSolideBrush);
                ToolTipPen2 = new Pen(ToolTipSolideBrush, 2);
            }
        }
        public SolidBrush ToolTipSolideBrush { get; set; }
        public Pen ToolTipPen { get; set; }
        public Pen ToolTipPen2 { get; set; }

        /// <summary>
        /// Get or set the tick color
        /// </summary>
        public Color ToolTipTextColor
        {
            get { return color_tooltip_text; }
            set
            {
                if (value == null)
                    return;
                color_tooltip_text = value;
                ToolTipTextSolideBrush = new SolidBrush(color_tooltip_text);
                ToolTipTextPen = new Pen(ToolTipTextSolideBrush);
                ToolTipTextPen2 = new Pen(ToolTipTextSolideBrush, 2);
            }
        }
        public SolidBrush ToolTipTextSolideBrush { get; set; }
        public Pen ToolTipTextPen { get; set; }
        public Pen ToolTipTextPen2 { get; set; }

        /// <summary>
        /// Get or set the time space in Milliseconds.
        /// </summary>
        public double MediaDuration
        {
            get { return mediaDuration; }
            set
            {
                mediaDuration = value;
                CaclulateMillipixels();
                doInvalidateOnTick = true;
            }
        }
        /// <summary>
        /// Get or set the milli-pixel value; how many milliseconds each pixel presents.
        /// </summary>
        public int MilliPixel { get; set; }
        /// <summary>
        /// Get or set the viewport offset
        /// </summary>
        public int ViewPortOffset { get; set; }
        /// <summary>
        /// Get or set the timing format.
        /// </summary>
        public string TimingFormat
        {
            get; set;
        }
        // Events
        public event EventHandler<TimeChangeArgs> TimeChangeRequest;

        public void Tick(double time)
        {
            CalculateMediaLineX(time);
        }
        private void CalculateMediaLineX(double time)
        {
            int old = MediaLineX;
            long time_in_mp = GetPixelOftime(time);
            MediaLineX = (int)(time_in_mp - ViewPortOffset);

            if (old != MediaLineX || doInvalidateOnTick)
            {
                doInvalidateOnTick = false;
                Invalidate();
            }
        }
        public void CalculateTicksBuffer()
        {
            TicksBuffer.Clear();
            SecsBuffer.Clear();
            TimesBuffer.Clear();
            TimesSizes.Clear();
            TimesPositionsBuffer.Clear();
            //calculate tick space
            if (MilliPixel > 0)
            {
                int ticPixels = (int)(1000 / MilliPixel);
                ticPixels = ((ticPixels % ticks_spacing) + ticks_spacing);
                int secPixels = ticPixels * 13;
                // Draw ticks
                for (int x = 0; x < this.Width; x++)
                {
                    int pix = x + ViewPortOffset;
                    //each ticksSpace pixels, draw small tick
                    if (pix % ticPixels == 0)
                    {
                        TicksBuffer.Add(x);
                    }
                    //each secPixels pixels, draw big tick and time
                    if (pix % secPixels == 0)
                    {
                        SecsBuffer.Add(x);

                        TimesBuffer.Add(TimingFormatter.SecondsToTime(GetTime(pix), TimingFormat));

                        TimesSizes.Add(TextRenderer.MeasureText(TimesBuffer[TimesBuffer.Count - 1], this.Font));

                        int pos = x - (TimesSizes[TimesSizes.Count - 1].Width / 2);

                        if (pos < 0)
                        {
                            pos = 0;
                        }
                        else if (Width - pos < TimesSizes[TimesSizes.Count - 1].Width)
                        {
                            pos = Width - TimesSizes[TimesSizes.Count - 1].Width  + 4;
                        }

                        TimesPositionsBuffer.Add(pos);
                    }
                }
            }
        }
        private void CaclulateMillipixels()
        {
            if (this.Width > 0)
            {
                MilliPixel = (int)((mediaDuration * 1000) + 10000) / this.Width;
                if (MilliPixel > 0)
                    timeSpace = (int)((mediaDuration * 1000) / MilliPixel) + 100;
                else
                    timeSpace = 0;
            }
            else
            {
                MilliPixel = timeSpace = 0;
            }
            CalculateTicksBuffer();
            CalculateMediaRect();
        }
        private void CalculateMediaRect()
        {
            if (MilliPixel > 0)
            {
                long width = GetPixelOftime(mediaDuration);
                if (this.Width > width)
                {
                    MediaRectanlge = new Rectangle(0, 0, (int)width, mediaRectHeight);
                }
                else
                {
                    MediaRectanlge = new Rectangle(0, 0, this.Width, mediaRectHeight);
                }
            }
            else
            {
                MediaRectanlge = Rectangle.Empty;
            }
        }
        private double GetTime(int x)
        {
            double tas = timeSpace * MilliPixel;
            double milli = (x * tas) / timeSpace;
            return milli / 1000;
        }
        private int GetPixelOftime(double time)
        {
            double tas = timeSpace * MilliPixel;
            return (int)(((time * 1000) * timeSpace) / tas);
        }
        private void toolTipTimer_Tick(object sender, EventArgs e)
        {
            if (toolTipTimerCounter > 0)
                toolTipTimerCounter--;
            else
            {
                toolTipTimer.Stop();
                showToolTip = true;
                doInvalidateOnTick = true;
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (mediaDuration == 0)
                return;
            // Draw media
            if (MediaRectanlge.Width > 0)
            {
                pe.Graphics.FillRectangle(MediaRecSolideBrush, MediaRectanlge);
            }

            // Draw ticks
            foreach (int x in TicksBuffer)
            {
                pe.Graphics.DrawLine(TicksPen, x, this.Height - 3, x, this.Height);
            }
            for (int i = 0; i < SecsBuffer.Count; i++)
            {
                pe.Graphics.DrawLine(TicksPen2, SecsBuffer[i], this.Height - 5, SecsBuffer[i], this.Height);

                if (SecsBuffer[i] > 0)
                {
                    pe.Graphics.DrawString(TimesBuffer[i], this.Font, TickTextSolideBrush, new PointF(TimesPositionsBuffer[i], 0));
                }
                else
                {
                    pe.Graphics.DrawString("0", this.Font, TickTextSolideBrush, new PointF(0, 0));
                }
            }
            // Draw media position
            if (MediaLineX >= 0 && MediaLineX <= Width)
            {
                pe.Graphics.DrawLine(MediaLinePen2, MediaLineX, 0, MediaLineX, Height);
            }
            //Tool tip, draw only if the text is not null
            if (toolTipText != "" & showToolTip)
            {
                if (toolTipLocation.X < toolTipMaxX)
                {
                    pe.Graphics.FillRectangle(ToolTipSolideBrush, toolTipLocation.X, 1, toolTipSize.Width, toolTipSize.Height + 4);
                    pe.Graphics.DrawLine(ToolTipPen, toolTipLocation.X, this.Height - 15, toolTipLocation.X, this.Height);
                    pe.Graphics.DrawString(toolTipText, this.Font, ToolTipTextSolideBrush, new RectangleF(toolTipLocation.X, 1, toolTipSize.Width, toolTipSize.Height));
                }
                else
                {
                    pe.Graphics.FillRectangle(ToolTipSolideBrush, toolTipMaxX, 1, toolTipSize.Width, toolTipSize.Height + 4);
                    pe.Graphics.DrawLine(ToolTipPen, toolTipLocation.X, this.Height - 15, toolTipLocation.X, this.Height);
                    pe.Graphics.DrawString(toolTipText, this.Font, ToolTipTextSolideBrush, new RectangleF(toolTipMaxX, 1, toolTipSize.Width, toolTipSize.Height));
                }
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CaclulateMillipixels();
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                TimeChangeRequest?.Invoke(this, new TimeChangeArgs(GetTime(e.X + ViewPortOffset), GetTime(MediaLineX)));
            }
            if (!Focused)
            {
                base.Focus();
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            toolTipLocation = e.Location;
            if (!showToolTip)
            {
                toolTipTimerCounter = 3;
                toolTipTimer.Start();
            }
            toolTipText = TimingFormatter.SecondsToTime(GetTime(e.X + ViewPortOffset), TimingFormat);
            // Cursor
            if (e.Button == MouseButtons.Left)
            {
                if (isTimeCursorMove)
                {
                    Cursor = Cursors.VSplit;
                    toolTipLocation = e.Location;
                    showToolTip = true;
                    toolTipText = TimingFormatter.SecondsToTime(GetTime(e.X + ViewPortOffset), TimingFormat);
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                int max = MediaLineX + 3;
                int min = MediaLineX - 3;
                if (e.X >= min && e.X <= max)
                {
                    Cursor = Cursors.VSplit;
                    isTimeCursorMove = true;
                }
                else
                {
                    Cursor = Cursors.Default;
                    isTimeCursorMove = false;
                }
            }

            toolTipSize = TextRenderer.MeasureText(toolTipText, this.Font);
            toolTipMaxX = Width - toolTipSize.Width + 1;
            doInvalidateOnTick = true;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            toolTipText = "";
            showToolTip = false;
            toolTipTimer.Stop();
        }
    }

}
