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
using System.IO;

namespace APlayer.Core
{
    public sealed class FormatsManager
    {
        public static event EventHandler<EventArgs> MediaLoaded;

        public static bool LoadMediaFile(string filePath, bool use_thread)
        {
            if (APCore.ON)
            {
                if (APCore.CurrentMediaFormat != null)
                {
                    if (APCore.CurrentMediaFormat.FileLoaded)
                    {
                        if (APCore.CurrentMediaFormat.CurrentFilePath == filePath)
                        {
                            APCore.CurrentMediaFormat.SetPosition(0);
                            APCore.Play();
                            return true;
                        }
                    }
                }

            }
            // 1 check the file !!
            string[] formatsCanBeUsed = CheckFile(filePath, false);

            // 2 Try all formats that can run the file
            foreach (string f in formatsCanBeUsed)
            {
                // Try the format ... 
                bool success = false;
                APCore.LoadFile(filePath, f, use_thread, out success);

                if (success)
                {
                    // Finish !!
                    MediaLoaded?.Invoke(null, new EventArgs());
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Check if a file is belong to a supported format from file extension, regaldess if header match or not. 
        /// </summary>
        /// <param name="file">The full file path to check</param>
        /// <returns>True if there is at least one format match the file extension, otherwise false.</returns>
        public static bool IsFileSupportedFormat(string file)
        {
            foreach (IMediaFormat formm in APMain.MediaFormats)
            {
                foreach (string ex in formm.Extensions)
                {
                    if (Path.GetExtension(file).ToLower() == ex.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Check the file to see what format it is
        /// </summary>
        /// <param name="FilePath">The media format file</param>
        /// <param name="DeepSearch">If true, the program will check every format even if the extension is not the same, otherwise it will check depending on extension.</param>
        /// <returns>Array of formats ids which could read this file</returns>
        public static string[] CheckFile(string FilePath, bool DeepSearch)
        {
            List<string> found = new List<string>();
            foreach (IMediaFormat formm in APMain.MediaFormats)
            {
                bool yes = false;
                if (!DeepSearch)
                {
                    foreach (string ex in formm.Extensions)
                    {
                        if (Path.GetExtension(FilePath).ToLower() == ex.ToLower())
                        { yes = true; break; }
                    }
                    if (yes)
                    {
                        if (formm.CheckFile(FilePath))
                        {
                            found.Add(formm.ID);
                        }
                    }
                }
                else
                {
                    if (formm.CheckFile(FilePath))
                    {
                        found.Add(formm.ID);
                    }
                }
            }
            return found.ToArray();
        }
        /// <summary>
        /// Get the filter that used in open/save file browser of supported ENABLED format 'disabled formats will NOT included'
        /// </summary>
        /// <returns>Filter includes all supported ENABLED formats</returns>
        public static string GetFilter()
        {
            string _Filter = "Supported media formats|";
            foreach (IMediaFormat formm in APMain.MediaFormats)
            {
                foreach (string ex in formm.Extensions)
                {
                    _Filter += "*" + ex + ";";
                }
            }
            _Filter = _Filter.Substring(0, _Filter.Length - 1) + "|";
            foreach (IMediaFormat formm in APMain.MediaFormats)
            {
                _Filter += formm.Name + "|";
                foreach (string ex in formm.Extensions)
                {
                    if (ex == formm.Extensions[formm.Extensions.Length - 1])
                    { _Filter += "*" + ex; }
                    else { _Filter += "*" + ex + ";"; }
                }
                if (formm != APMain.MediaFormats[APMain.MediaFormats.Count - 1])
                { _Filter += "|"; }
            }
            return _Filter;
        }
    }
}
