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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLV;

namespace APlayer
{
    internal class ListComparer : IComparer<ManagedListViewItem>
    {
        public ListComparer(bool aToZ,string col_id)
        {
            this.aToZ = aToZ; this.col_id = col_id;
        }
        private bool aToZ = true;
        private string col_id;

        public int Compare(ManagedListViewItem x, ManagedListViewItem y)
        {
            switch (col_id)
            {
                case "name":
                case "file type":
                case "path":
                    {
                        ManagedListViewSubItem ss_x = x.GetSubItemByID(col_id);
                        ManagedListViewSubItem ss_y = y.GetSubItemByID(col_id);

                        if (aToZ)
                            return (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(ss_x.Text, ss_y.Text);
                        else
                            return (-1 * (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(ss_x.Text, ss_y.Text));
                    }
                case "size":
                    {
                        // 1 get the files
                        string file_x = x.Tag.ToString(); 
                        string file_y = y.Tag.ToString();
                        // 2 get sizes

                      System.IO.  FileInfo Info_x = new System.IO.FileInfo(file_x);
                        System.IO.FileInfo Info_y = new System.IO.FileInfo(file_y);

                        if (aToZ)
                            return (int)(Info_x.Length - Info_y.Length);
                        else
                            return (int)(Info_y.Length - Info_x.Length);
                    }
            }
            return 0;
        }
    }
}
