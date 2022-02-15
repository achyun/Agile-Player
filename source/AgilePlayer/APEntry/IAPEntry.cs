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

namespace APlayer
{
    [Serializable]
    abstract class IAPEntry
    {
        public IAPEntry()
        {
            LoadAttrs();
        }
        public string Name { get; private set; }
        public string ID { get; private set; }
        public List<ColumnItem> Columns { get; protected set; }

        protected string[,] defaultColumns
        {
            get
            {
                return new string[,]  {
          { "Name",          "name" } ,
          { "Size",          "size" } ,
          { "File Type",      "file type" } ,
          { "File Path",      "path" }
                                      };
            }
        }
        /// <summary>
        /// Load attributes for this media format. Called at the constructor when this format is found.
        /// </summary>
        protected virtual void LoadAttrs()
        {
            //this.Supported = true;
            //this.NotImplementedWell = false;
            foreach (Attribute attr in Attribute.GetCustomAttributes(this.GetType()))
            {
                if (attr.GetType() == typeof(APEntryInfoAttribute))
                {
                    APEntryInfoAttribute inf = (APEntryInfoAttribute)attr;
                    this.Name = inf.Name;
                    this.ID = inf.ID;
                }
            }
        }
        public virtual void BuildDefaultColumns(string[] columnIdsToExclude)
        {
            Columns = new List<ColumnItem>();
            for (int i = 0; i < defaultColumns.Length / 2; i++)
            {
                ColumnItem item = new ColumnItem();
                item.ColumnName = defaultColumns[i, 0];
                item.ColumnID = defaultColumns[i, 1];
                item.Width = 60;
                if (columnIdsToExclude != null)
                    item.Visible = !columnIdsToExclude.Contains(defaultColumns[i, 1]);
                else
                    item.Visible = true;
                Columns.Add(item);
            }
        }
    }
}
