﻿// This file is part of Agile Player
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

namespace APlayer
{
    /// <summary>
    /// The column item
    /// </summary>
    [Serializable()]
    public class ColumnItem
    {
        private string columnID = "";
        private string columnName = "";
        private bool visible = true;
        private int width = 60;
        /// <summary>
        /// Get or set the column id
        /// </summary>
        public string ColumnID { get { return columnID; } set { columnID = value; } }
        /// <summary>
        /// Get or set the column name
        /// </summary>
        public string ColumnName { get { return columnName; } set { columnName = value; } }
        /// <summary>
        /// Get or set if this column is visible
        /// </summary>
        public bool Visible { get { return visible; } set { visible = value; } }
        /// <summary>
        /// Get or set the width of this column
        /// </summary>
        public int Width { get { return width; } set { width = value; } }
    }
}
