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
using System.Drawing;
using APlayer.Core;

namespace APlayer
{
    [SettingsInfo("settings.ini")]
    internal class ApplicationSettings : ISettings
    {
        // Application
        public string App_Version = "";

        // Window
        public int Win_Location_X = 10;
        public int Win_Location_Y = 10;
        public int Win_Size_W = 645;
        public int Win_Size_H = 489;
        public bool SaveListOnExit = true;

        // Media Bar
        public int MediaBarBackgroundcolor = Color.LightSlateGray.ToArgb();
        public int MediaRecColor = Color.DarkSeaGreen.ToArgb();
        public int MediaTickColor = Color.White.ToArgb();
        public int MediaTickTextColor = Color.White.ToArgb();
        public int MediaMediaLineColor = Color.White.ToArgb();
        public int MediaToolTipColor = Color.RoyalBlue.ToArgb();
        public int MediaToolTipTextColor = Color.White.ToArgb();
        public string MediaBarTimingFormat = "hh:mm:ss.iii";
        public string TimeTextTimingFormat = "hh:mm:ss";

        public override void LoadSettings()
        {
            base.LoadSettings();

            if (App_Version != System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
            {
                // TODO: do something on new version ...
                App_Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}
