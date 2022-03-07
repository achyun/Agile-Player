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
using System.Diagnostics;
using System.Windows.Forms;
using APlayer.Core;

namespace APlayer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if TRACE
            ConsoleTraceListener list = new ConsoleTraceListener();
            Trace.Listeners.Add(list);
#endif

            string work_folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgilePlayer");
            string app_folder = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            if (app_folder == "")
                app_folder = Path.GetFullPath(@".\");

            // Initialize the core
            APMain.Initialize(app_folder, work_folder);

            // Load app settings
            Trace.WriteLine("Loading application settings .. ");
            AppSettings = new ApplicationSettings();
            AppSettings.LoadSettings();
            Trace.WriteLine("Loading application settings success.");

            Application.Run(new FormMain(args));

            // Reached here means over
            // Save app settings
            Trace.WriteLine("Saving application settings .. ");
            AppSettings.SaveSettings();
            Trace.WriteLine("Saving application settings success.");

            APMain.Close();
        }
        /// <summary>
        /// Get the application settings
        /// </summary>
        public static ApplicationSettings AppSettings { get; private set; }
    }
}
