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
using APlayer.Core;// We need reference to Core library. Renderers and Formats should be placed next to Core.dll and the engine should locate media formats and renderers automatically.

namespace APlayer
{
    // Simply example how to use the Agile Player Core in a console application. 
    // It plays a file till the end of that file then closes.
    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

#if TRACE
// Add the console listener to see trace outputs in console, since the core uses System.Disagnostics.Trace for tracing. 
            ConsoleTraceListener list = new ConsoleTraceListener();
            Trace.Listeners.Add(list);
#endif

            // Define working folder (where settings is saved) and application folder (where exe file and Core.dll is exist).
            string work_folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AgilePlayer");
            string app_folder = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            if (app_folder == "")
                app_folder = Path.GetFullPath(@".\");

            // Initialize the core, first thing to call
            APMain.Initialize(app_folder, work_folder);

            // Handle can be set to zero, need to set handle of gui if there is any, in this case we don't have one. DirectSound needs the handle of the main form.
            APMain.SetupAudioRenderer(IntPtr.Zero);

            // Open a test file if exist
            if (File.Exists("test.wav"))
            {
                // To open media format, all we need to do is to call this.
                // Use thread is disabled because of we gonna use the current thread for clocking.
                // When it is enabled, the APCore will use a private thread, closed when APMain.Close() or APCore.ShutDown() is called.
                FormatsManager.LoadMediaFile("test.wav", false);
            }

            // Open a file from args
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (File.Exists(args[i]))
                    {
                        FormatsManager.LoadMediaFile(args[i], false);

                    }
                }
            }

            // RUN APP IN THREAD HERE, if use thread is enabled above, here would be like Application.Run().
            // Also, if the application like to use clocks, simply we can put Application.Run() here, then in Application, we use APCore.Clock().
            // APCore.Clock() must be called in thread. Cannot be called in intervals.
            APCore.Clock();

            // Reached here means finished playing, this needs to be called to save core settings and close threads and renderers if any.
            APMain.Close();
        }
    }
}
