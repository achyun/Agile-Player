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
        private int db_spacing = 40;
        private int bar_coloring_sp;
        double channelLeft;
        double channelRight;

        public void SetValues(int channelLeft_sample, int channelRight_sample, int bits_per_sample)
        {
            switch (bits_per_sample)
            {
                case 8:
                    {
                        int left_val = channelLeft_sample - sbyte.MaxValue;
                        int right_val = channelRight_sample - sbyte.MaxValue;

                        MaxDB = 20 * Math.Log10(sbyte.MaxValue) / 2;

                        channelLeft = 20 * Math.Log10(Math.Abs(left_val) / sbyte.MaxValue);
                        channelRight = 20 * Math.Log10(Math.Abs(right_val) / sbyte.MaxValue);
                        break;
                    }
                case 16:
                    {
                        MaxDB = 20 * Math.Log10(short.MaxValue) / 2;
                        channelLeft = 20 * Math.Log10(Math.Abs(channelLeft_sample) / short.MaxValue);
                        channelRight = 20 * Math.Log10(Math.Abs(channelRight_sample) / short.MaxValue);
                      
                        break;
                    }
                case 24:
                    {
                        MaxDB = 20 * Math.Log10(8388607) / 2;
                        channelLeft = 20 * Math.Log10(Math.Abs(channelLeft_sample) / 8388607);
                        channelRight = 20 * Math.Log10(Math.Abs(channelRight_sample) / 8388607);
                        break;
                    }
                case 32:
                    {
                        MaxDB = 20 * Math.Log10(int.MaxValue) / 2;
                        channelLeft = 20 * Math.Log10(Math.Abs(channelLeft_sample) / int.MaxValue);
                        channelRight = 20 * Math.Log10(Math.Abs(channelRight_sample) / int.MaxValue);
                        break;
                    }
            }

            Invalidate();
        }
        private int dbToPixel(double db)
        {
            return (int)((db * Height) / MaxDB);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (IsStereo)
            {
                // Draw both channels
                int left = dbToPixel(channelLeft);
                int right = dbToPixel(channelRight);


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
                    pe.Graphics.DrawString(i + " dB", Font, Brushes.Black, 1, Height - dd - 13);
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
    }
}
