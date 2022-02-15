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

namespace APlayer.Core
{
    public class MediaFormatInfoAttribute : Attribute
    {
        /// <summary>
        /// Describe media format
        /// </summary>
        /// <param name="name">The name of the media format</param>
        /// <param name="id">The id of the media format</param>
        /// <param name="extensions">The extensions of the media format</param>
        /// <param name="tp">The type of the media format</param>
        public MediaFormatInfoAttribute(string name, string id, string[] extensions, MediaFormatType tp)
        {
            this.Name = name;
            this.ID = id;
            this.Extensions = extensions;
            this.FormatType = tp;
        }
        public string Name { get; private set; }
        public string ID { get; private set; }
        public string[] Extensions { get; private set; }
        public MediaFormatType FormatType { get; private set; }
    }
}
