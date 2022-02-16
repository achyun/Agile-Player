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
using System.Drawing;
using System.Windows.Forms;

namespace APlayer
{
    public partial class DBMeterControl : Control
    {
        public DBMeterControl()
        {
            InitializeComponent();

            ControlStyles flag = ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint;
            this.SetStyle(flag, true);

            MaxDB = 196;// Max of 32 bit ?
            db_spacing = 15;
            bar_coloring_sp = 40;
            SetValues(0, 0, 16);
        }

        public bool IsStereo { get; set; }
        public double MaxDB { get; set; }
        public bool isAmp { get; set; }
        private int db_spacing = 40;
        private int bar_coloring_sp;
        int channelLeft;
        int channelRight;

        public void SetValues(int channelLeft_sample, int channelRight_sample, int bits_per_sample)
        {
            if (!isAmp)
            {
                switch (bits_per_sample)
                {
                    case 8:
                        {
                            int left_val = (channelLeft_sample & 0xFF) - sbyte.MaxValue;
                            int right_val = (channelRight_sample & 0xFF) - sbyte.MaxValue;

                            MaxDB = 20 * Math.Log10(sbyte.MaxValue);

                            channelLeft = (int)(20 * Math.Log10(Math.Abs(left_val) / sbyte.MaxValue));
                            channelRight = (int)(20 * Math.Log10(Math.Abs(right_val) / sbyte.MaxValue));
                            break;
                        }
                    case 16:
                        {
                            MaxDB = -20 * Math.Log10(1.0 / short.MaxValue);

                            // Fix
                            byte b1 = (byte)((channelLeft_sample & 0xFF00) >> 8);
                            byte b2 = (byte)((channelLeft_sample & 0x00FF) >> 0);

                            double l_lev = (short)(((b2 << 8) | b1) & 0x7FFF);

                            b1 = (byte)((channelRight_sample & 0xFF00) >> 8);
                            b2 = (byte)((channelRight_sample & 0x00FF) >> 0);

                            double r_lev = (short)(((b2 << 8) | b1) & 0x7FFF);

                            channelLeft = (int)(-20 * Math.Log10(l_lev / short.MaxValue));
                            channelRight = (int)(-20 * Math.Log10(r_lev / short.MaxValue));
                            break;
                        }
                    case 24:
                        {
                            MaxDB = 20 * Math.Log10(8388607);
                            channelLeft = (int)(20 * Math.Log10(Math.Abs(channelLeft_sample) / 8388607));
                            channelRight = (int)(20 * Math.Log10(Math.Abs(channelRight_sample) / 8388607));
                            break;
                        }
                    case 32:
                        {
                            MaxDB = 20 * Math.Log10(int.MaxValue);
                            channelLeft = (int)(20 * Math.Log10(Math.Abs(channelLeft_sample) / int.MaxValue));
                            channelRight = (int)(20 * Math.Log10(Math.Abs(channelRight_sample) / int.MaxValue));
                            break;
                        }
                }
            }
            else
            {
                switch (bits_per_sample)
                {
                    case 8:
                        {
                            int left_val = channelLeft_sample - sbyte.MaxValue;
                            int right_val = channelRight_sample - sbyte.MaxValue;

                            channelLeft = (Math.Abs(left_val) * 1000) / sbyte.MaxValue;
                            channelRight = (Math.Abs(right_val) * 1000) / sbyte.MaxValue;
                            break;
                        }
                    case 16:
                        {
                            // Fix
                            byte b1 = (byte)((channelLeft_sample & 0xFF00) >> 8);
                            byte b2 = (byte)((channelLeft_sample & 0x00FF) >> 0);

                            short l_lev = (short)(((b2 << 8) | b1) & 0x7FFF);

                            b1 = (byte)((channelRight_sample & 0xFF00) >> 8);
                            b2 = (byte)((channelRight_sample & 0x00FF) >> 0);

                            short r_lev = (short)(((b2 << 8) | b1) & 0x7FFF);


                            channelLeft = (l_lev * 1000) / short.MaxValue;
                            channelRight = (r_lev * 1000) / short.MaxValue;

                            break;
                        }
                    case 24:
                        {
                            channelLeft = (Math.Abs(channelLeft_sample) * 1000) / 8388607;
                            channelRight = (Math.Abs(channelRight_sample) * 1000) / 8388607;
                            break;
                        }
                    case 32:
                        {
                            channelLeft = (Math.Abs(channelLeft_sample) * 1000) / int.MaxValue;
                            channelRight = (Math.Abs(channelRight_sample) * 1000) / int.MaxValue;
                            break;
                        }
                }
            }

            Invalidate();
        }
        private int ampToPixel(double db)
        {
            return (int)((db * Height) / 1200);
        }
        private int dbToPixel(double db)
        {
            return (int)((db * Height) / MaxDB);
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                isAmp = !isAmp;
            }
        }
        private void RenderDB(PaintEventArgs pe)
        {
            if (IsStereo)
            {
                // Draw both channels
                int left = dbToPixel(channelLeft);
                int right = dbToPixel(channelRight);

                //pe.Graphics.FillRectangle(Brushes.MediumSeaGreen, new Rectangle(0, Height - left, Width / 2, left));

                for (int i = 0; i < left; i++)
                {
                    int y = Height - i;

                    if (y < bar_coloring_sp)
                    {
                        pe.Graphics.DrawLine(Pens.Red, 0, y, Width / 2, y);
                    }
                    else if (y > Height - bar_coloring_sp)
                    {
                        pe.Graphics.DrawLine(Pens.YellowGreen, 0, y, Width / 2, y);
                    }
                    else
                    {
                        pe.Graphics.DrawLine(Pens.MediumSeaGreen, 0, y, Width / 2, y);
                    }
                }
                for (int i = 0; i < right; i++)
                {
                    int y = Height - i;

                    if (y < bar_coloring_sp)
                    {
                        pe.Graphics.DrawLine(Pens.Red, Width / 2, y, Width, y);
                    }
                    else if (y > Height - bar_coloring_sp)
                    {
                        pe.Graphics.DrawLine(Pens.YellowGreen, Width / 2, y, Width, y);
                    }
                    else
                    {
                        pe.Graphics.DrawLine(Pens.MediumSeaGreen, Width / 2, y, Width, y);
                    }
                }
                // pe.Graphics.FillRectangle(Brushes.MediumSeaGreen, new Rectangle(Width / 2, Height - right, Width / 2, right));
            }
            else
            {
                int left = dbToPixel(channelLeft);
                pe.Graphics.FillRectangle(Brushes.MediumSeaGreen, new Rectangle(0, Height - left, Width, left));
            }
            // Draw grid lines

            /*int dd = dbToPixel(0, Height);
            pe.Graphics.DrawLine(Pens.Black, 0, dd, Width, dd);
            pe.Graphics.DrawString("-0dB", Font, Brushes.Black, 1, dd);

            dd = dbToPixel(MaxDB, Height);
            pe.Graphics.DrawLine(Pens.White, 0, dd, Width, dd);
            pe.Graphics.DrawString(MaxDB + "dB", Font, Brushes.White, 1, dd - 13);*/
            int space = db_spacing;

            for (double i = 0; i <= MaxDB; i++)
            {
                int dd = dbToPixel(i);
                /*if (dd - (space / 2) >= 0)
                {
                    pe.Graphics.DrawLine(Pens.Black, 0, Height - dd, Width, Height - dd);
                    //space = dd + (space / 2);
                }*/
                if (dd - space >= 0)
                {
                    pe.Graphics.DrawLine(Pens.Black, 0, Height - dd, Width, Height - dd);
                    pe.Graphics.DrawString("- " + i + " dB", Font, Brushes.Black, 1, Height - dd - 13);
                    space = dd + db_spacing;
                }


            }
            if (IsStereo)
            {
                pe.Graphics.DrawLine(Pens.Black, Width / 2, 0, Width / 2, Height);
                pe.Graphics.DrawString("Left", Font, Brushes.Black, 1, Height - 15);
                pe.Graphics.DrawString("Right", Font, Brushes.Black, (Width / 2) + 1, Height - 15);
            }

            pe.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

        }
        private void RenderAMP(PaintEventArgs pe)
        {
            if (IsStereo)
            {
                // Draw both channels
                int left = ampToPixel(channelLeft);
                int right = ampToPixel(channelRight);

                // pe.Graphics.FillRectangle(Brushes.MediumSeaGreen, new Rectangle(0, Height - left, Width / 2, left));

                for (int i = 0; i < left; i++)
                {
                    int y = Height - i;

                    if (y < bar_coloring_sp)
                    {
                        pe.Graphics.DrawLine(Pens.Red, 0, y, Width / 2, y);
                    }
                    else if (y > Height - bar_coloring_sp)
                    {
                        pe.Graphics.DrawLine(Pens.YellowGreen, 0, y, Width / 2, y);
                    }
                    else
                    {
                        pe.Graphics.DrawLine(Pens.MediumSeaGreen, 0, y, Width / 2, y);
                    }
                }
                for (int i = 0; i < right; i++)
                {
                    int y = Height - i;

                    if (y < bar_coloring_sp)
                    {
                        pe.Graphics.DrawLine(Pens.Red, Width / 2, y, Width, y);
                    }
                    else if (y > Height - bar_coloring_sp)
                    {
                        pe.Graphics.DrawLine(Pens.YellowGreen, Width / 2, y, Width, y);
                    }
                    else
                    {
                        pe.Graphics.DrawLine(Pens.MediumSeaGreen, Width / 2, y, Width, y);
                    }
                }
                //pe.Graphics.FillRectangle(Brushes.MediumSeaGreen, new Rectangle(Width / 2, Height - right, Width / 2, right));
            }
            else
            {
                int left = ampToPixel(channelLeft);
                pe.Graphics.FillRectangle(Brushes.MediumSeaGreen, new Rectangle(0, Height - left, Width, left));
            }
            // Draw grid lines

            /*int dd = dbToPixel(0, Height);
            pe.Graphics.DrawLine(Pens.Black, 0, dd, Width, dd);
            pe.Graphics.DrawString("-0dB", Font, Brushes.Black, 1, dd);

            dd = dbToPixel(MaxDB, Height);
            pe.Graphics.DrawLine(Pens.White, 0, dd, Width, dd);
            pe.Graphics.DrawString(MaxDB + "dB", Font, Brushes.White, 1, dd - 13);*/
            int space = db_spacing;

            for (double i = 0; i <= 1200; i++)
            {
                int dd = ampToPixel(i);
                /*if (dd - (space / 2) >= 0)
                {
                    pe.Graphics.DrawLine(Pens.Black, 0, Height - dd, Width, Height - dd);
                    //space = dd + (space / 2);
                }*/
                if (dd - space >= 0)
                {
                    pe.Graphics.DrawLine(Pens.Black, 0, Height - dd, Width, Height - dd);
                    pe.Graphics.DrawString(((double)i / 1000).ToString("F2") + " A", Font, Brushes.Black, 1, Height - dd - 13);
                    space = dd + db_spacing;
                }


            }
            if (IsStereo)
            {
                pe.Graphics.DrawLine(Pens.Black, Width / 2, 0, Width / 2, Height);
                pe.Graphics.DrawString("Left", Font, Brushes.Black, 1, Height - 15);
                pe.Graphics.DrawString("Right", Font, Brushes.Black, (Width / 2) + 1, Height - 15);
            }

            pe.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (!isAmp)
                RenderDB(pe);
            else
                RenderAMP(pe);
        }
    }
}
