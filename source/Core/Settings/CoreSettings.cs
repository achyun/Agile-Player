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
namespace APlayer.Core
{
    [SettingsInfo("coresettings.ini")]
    public class CoreSettings : ISettings
    {
        public string Audio_RendererID = "";

        public int Audio_TargetFrequency = 44100;
        public int Audio_TargetAudioChannels = 2;
        public int Audio_TargetBitsPerSample = 16;
        public int Audio_RenderBufferInKB = 9;// 9 KB is normal ... 
        public int CPS_TargetCPS = 44;// How many clocks each seconds. Good is 20, 44, 46, 53, 60.
        public bool Audio_DB_Fix_Enabled = false;// Disabled by default
        public int Audio_Wave_Fix_Mode = 0;// 0 disabled, 1 wave fix shift mode
        public int Audio_Volume = 86;// 0 to 100 %
    }
}
