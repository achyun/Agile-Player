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

            MaxDB = -20 * Math.Log10(1.0 / int.MaxValue);// Max of 32 bit ?
            db_spacing = 15;
            bar_coloring_sp = 60;
            SetValues(0, 0, 16);
        }

        public bool IsStereo { get; set; }
        public double MaxDB { get; set; }
        private double current_max_db;
        private int db_spacing = 40;
        private int bar_coloring_sp;
        double channelLeft;
        double channelRight;

        public void SetValues(double channelLeft_sample, double channelRight_sample, int bits_per_sample)
        {
            // comparing from 0 to 90, log of 1 is 0, so: 20 log (max) - 20 log (1) = 20 log (max / 1) = 20 log (max)!!
            // max is the maximum number we can get from bits, for signed 16 bits is +32767 or -32767, there is no increase for negatives
            // so we use +32767 and [-32767], it is wrong to use ushort max or 0xFFFF because of in signed 16 bits the sound is going above 0 till +32767 and below 0 till -32767
            // then the increase is always from 0 to 32767 regardless of positive or negative.

            // We compare the increase of the input sample to the minimum increase it can be which is the increase of 1 bit 
            // i.e. the value of 20 * log (1) = 0 !!
            // So: DB value = 20 * log10 (input_sample/1) !!
            // Compare the increase of current input sample to the increase of the minimum increase possible, which the increase of the minimum bit number which is 1,
            // (or simply the minimum noise of bits, 1 bit is 1 or log(1))
            // we cannot use 0 because of there is no energy or increase !!, that's true in math because of log(0) = infinity

            // NOTE: this is how decible works:
            // we compare the input power to the minimum power that machine can handle. 

            // FOR PC, it uses bits, maximum power it can handle for example is 16 bits and minimum is 1 bit (very accurate)
            // Then db of a pc always will be 20 log (input_bits_value) - 20 log (1) = 20 log (input_bits_value) !! (range from 0 to max db of bits or 20 log (max_bits_can_handle_value))

            // For 16 bits: 
            // DB MAX = 20 log(32767 / 1) = 90,30 db
            // DB MIN = 20 log(1 / 1) = 0 db
            // the increase of a pc that uses 16 bits signed is from 0 db to 90,30 db

            // DB CURRENT for a sample of value 343 = 20 log(343 / 1) =                          50,70 db (means that it increased from 0 to 50,70 db in the POSITIVE direction)
            // DB CURRENT for a sample of value -5443 = 20 log([-5443] / 1) = 20 log(5443 / 1) = 74,71 db (means that it increased from 0 to 74,71 db in the NEGATIVE direction)

            // FOR Electric device, that uses millivolt, EXAMPLE: let's say we have a device that the maximum power it can handle is 5 volt, and minimum power it can handle
            // (minimum volt value it can control or simply the accuracy of that device when it use volts)
            // is 1 millivolt or 0.001 volt (accuracy of 1 millivolt)

            // Then db of an electric device: 20 log (input_volt_value) - 20 log (0.001) = 20 log (input_volt_value/0.001)

            // SO in our example for 5 volt max : 

            // DB MAX = 20 log(5 / 0.001) = 73,97 db (maximum range)
            // DB MIN = 20 log(0.001 / 0.001) = 0 db (minimum range)
            // the increase of that electric device in this case can be from  0 db to 73,97 db

            // DB CURRENT for 0 volt does not exist (20 log(0/0.001)= infinity), then simply there is no increase or the device is OFF !! does make sense lol
            // DB CURRENT for 0.001 volt = 20 log(0.001 / 0.001) = 0 db it does exist, no increase but the device is ON, does make sense lol
            // DB CURRENT for 0.0000001 volt = 20 log(0.0000001 / 0.001) = -80 db it does exist, it is decreased below 0 but wait a second, the device accuracy is 0.001, it cannot go below this number, it cannot handle below millitvolt,
            // this means -80 db is false or impossible !!

            // DB CURRENT for 0.61 volt = 20 log(0.61 / 0.001)= 55,70 db 
            // DB CURRENT for 1 volt = 20 log(1 / 0.001) = 60 db 

            // DB CURRENT for 2 volt = 20 log(2 / 0.001) =                         66,02 db (means that it increased from 0 to 66,02 db in the POSITIVE direction)
            // DB CURRENT for -2 volt = 20 log([-2] / 0.001) = 20 log(2 / 0.001) = 66,02 db (means that it increased from 0 to 66,02 db in the NEGATIVE direction)

            // DB CURRENT for 3 volt = 20 log(3 / 0.001)= 69,54 db 
            // DB CURRENT for 4 volt = 20 log(4 / 0.001)= 72,04 db 

            // Another example, an electric device with 5 volt max and minimum is 0.00001 volt
            // DB MAX = 20 log(5 / 0.00001) = 113,97 db (maximum range)
            // DB MIN = 20 log(0.00001 / 0.00001) = 0 db (minimum range) (the device is ON, it does exist !!)
            // the increase of that electric device in this case can be from  0 db to 113,97 db, very accurate !!


            // DB CURRENT for 0 volt does not exist, then simply there is no increase or the device is OFF !! does make sense lol
            // DB CURRENT for 0.00001 volt = 20 log(0.00001 / 0.00001) = 0 db it does exist, no increase but the device is ON, does make sense lol

            // DB CURRENT for 0.0000001 volt = 20 log(0.0000001 / 0.00001) = -40 db it does exist, it is decreased below 0 but wait a second, the device accuracy is 0.00001, it cannot go below this number, this means -40 db is false or impossible !!

            // DB CURRENT for 0.61 volt = 20 log(0.61 / 0.00001)= 95,70 db 
            // DB CURRENT for 1 volt = 20 log(1 / 0.00001) = 100 db 

            // DB CURRENT for 2 volt = 20 log(2 / 0.00001) =                           106,02 db (means that it increased from 0 to 106,02 db in the POSITIVE direction)
            // DB CURRENT for -2 volt = 20 log([-2] / 0.00001) = 20 log(2 / 0.00001) = 106,02 db (means that it increased from 0 to 106,02 db in the NEGATIVE direction)

            // DB CURRENT for 3 volt = 20 log(3 / 0.00001)= 109,54 db 
            // DB CURRENT for 4 volt = 20 log(4 / 0.00001)= 112,04 db 

            // NOTES:

            // 1. db of 0 input does not exist, the device is OFF or that thing that we are calculating db for does not exist.

            // 2. db below 0 does exist but false or does not make sense (impossible to happen), because in this case the input value is going beyond the accuracy of the machine or the 'relative' to calculate db !!,
            //    simply for example: for electricity machine of accuracy of 0.001 volt (minimum volt value it can handle), there is no increase of value 0.00001 volt or 0.0005 volt, simply it cannot handle it !!
            //    Same for pc, there always 1 bit, we cannot use 0.5 bit or 0.003 bit... simply does not exit or pc cannot handle below 1 bit !! accuracy of 1 bit !
            //    example: let's have the db of an 0.5 sample input (or digital input into pc) ?! :  20 log (0.5 / 1) = -6,02 db, so the db value of sample of 0.5 or a half-bit does not exist lol

            //    SO: db is always range from 0 to a positive number. 0 db means the device is on but no increase of power. below 0 db is impossible, usually something wrong in the input, simply the input that is given is beyond that machine accuracy).
            //    INFINITY db means the device is off, or it does not exist.

            // 3. For inputs we always take the absolute number (0.5 does not accepted, same for 0.3, 2.3 ...etc, what accepted is 1,2,3,4,5,6 .. 40,43,6456, ....etc)
            //    Then we can say CURRENT DB = 20 log (300/100) = 20 log (3/1) = 20 log (3) = 9,54 db is OK !!
            //         we can say CURRENT DB = 20 log (4/3) = 20 log (1,3333333333333333333333333333333) = 2,49 db is OK !!
            //         we CANNOT say CURRENT DB = 20 log (4.5/3) = 3,52 db IT IS NOT OK !!, simply it does not exist ?!

            //    SO the correct equation: db_val =  20 log( | input | / minimum_input_machine_can_handle);
            //
            //    minimum_input_machine_can_handle must be > 0 and absolute number (1,2,3,4,5,6. ...et);
            //    | input | will get the positive value of the input and absolute (0,1,2,3,4 ..etc) and input must not = 0 (i.e. 0 < minimum_input_machine_can_handle <= input <= maximum_input_machine_can_handle and input is abs (0,1,2,3,4,5,6...etc))

            // For the examples above , we simply convert from volt input into millivolt, so for 5 volt we use 5000 millivolt instead of 5, we use 1 millivolt as minimum volt instead of 0.001 volt.

            switch (bits_per_sample)
            {
                case 8:
                    {
                        int left_val = (int)channelLeft_sample - sbyte.MaxValue;
                        int right_val = (int)channelRight_sample - sbyte.MaxValue;

                        MaxDB = 20 * Math.Log10(sbyte.MaxValue);
                        MaxDB += 8;

                        current_max_db = 20 * Math.Log10(sbyte.MaxValue);//  20 * Log10( [sbyte.MaxValue] / 1)

                        channelLeft = 20 * Math.Log10(Math.Abs(left_val));//  20 * Log10( [left_val] / 1)
                        channelRight = 20 * Math.Log10(Math.Abs(right_val));
                        break;
                    }
                case 16:
                    {
                        MaxDB = 20 * Math.Log10(short.MaxValue);
                        MaxDB += 8;

                        current_max_db = 20 * Math.Log10(short.MaxValue);

                        // No need to calculate the case of 0 sample which is infinity , it will be 0 in c# anyway ..
                        channelLeft = 20 * Math.Log10(Math.Abs(channelLeft_sample));
                        channelRight = 20 * Math.Log10(Math.Abs(channelRight_sample));
                        break;
                    }
                case 24:
                    {
                        MaxDB = 20 * Math.Log10(8388607);
                        MaxDB += 8;

                        current_max_db = 20 * Math.Log10(8388607);

                        channelLeft = 20 * Math.Log10(Math.Abs(channelLeft_sample));
                        channelRight = 20 * Math.Log10(Math.Abs(channelRight_sample));
                        break;
                    }
                case 32:
                    {

                        MaxDB = 20 * Math.Log10(int.MaxValue);
                        MaxDB += 8;

                        current_max_db = 20 * Math.Log10(int.MaxValue);

                        channelLeft = 20 * Math.Log10(Math.Abs(channelLeft_sample));
                        channelRight = 20 * Math.Log10(Math.Abs(channelRight_sample));
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
                int max = dbToPixel(current_max_db);

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
            }
            else
            {
                int left = dbToPixel(channelLeft);
                //pe.Graphics.FillRectangle(Brushes.MediumSeaGreen, new Rectangle(0, Height - left, Width, left));

                for (int i = 0; i < left; i++)
                {
                    int y = Height - i;

                    if (y < bar_coloring_sp)
                    {
                        pe.Graphics.DrawLine(Pens.Red, 0, y, Width, y);
                    }
                    else if (y > Height - bar_coloring_sp)
                    {
                        pe.Graphics.DrawLine(Pens.YellowGreen, 0, y, Width, y);
                    }
                    else
                    {
                        pe.Graphics.DrawLine(Pens.MediumSeaGreen, 0, y, Width, y);
                    }
                }
            }

            int space = db_spacing;

            for (double i = 0; i <= MaxDB; i++)
            {
                int dd = (int)((i * Height) / MaxDB);

                if (dd - space >= 0)
                {
                    pe.Graphics.DrawLine(Pens.Black, 0, Height - dd, Width, Height - dd);

                    if (Height - dd - 13 >= 0)
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
