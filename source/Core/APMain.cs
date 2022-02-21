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
using System.Reflection;

namespace APlayer.Core
{
    public sealed class APMain
    {
        /// <summary>
        /// Get the working directory of the program, where settings files are saved for example.
        /// </summary>
        public static string WorkingFolder { get; private set; }
        /// <summary>
        /// Get the application folder where the core/executable file is located
        /// </summary>
        public static string ApplicationFolder { get; private set; }
        /// <summary>
        /// Get the renderer settings
        /// </summary>
        public static CoreSettings CoreSettings { get; private set; }
        /// <summary>
        /// Get a list of available audio renderers
        /// </summary>
        public static List<IAudioRenderer> AudioRenderers { get; private set; }
        /// <summary>
        /// Get current audio renderer. This set at the core initialize.
        /// </summary>
        public static IAudioRenderer AudioRenderer { get; private set; }
        /// <summary>
        /// Get a list of available media formats
        /// </summary>
        public static List<IMediaFormat> MediaFormats { get; private set; }

        public static void Initialize(string app_folder_path, string working_folder_path)
        {
            WorkingFolder = working_folder_path;
            // Create working folder
            Trace.WriteLine("Creating working folder ...", "APMain");
            try
            {
                Directory.CreateDirectory(working_folder_path);
                Trace.WriteLine("Working folder created successfully.", "APMain");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error while creating working folder !!", "APMain");
                Trace.TraceError(ex.Message);
            }

            ApplicationFolder = app_folder_path;

            Trace.WriteLine("Loading renderer settings ...", "APMain");
            CoreSettings = new CoreSettings();
            CoreSettings.LoadSettings();
            Trace.WriteLine("Loading renderer settings success.", "APMain");

            Trace.WriteLine("Locating renderers... ", "APMain");
            AudioRenderers = new List<IAudioRenderer>();
            MediaFormats = new List<IMediaFormat>();

            string[] files = Directory.GetFiles(ApplicationFolder, "*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                try
                {
                    if (Path.GetExtension(file).ToLower() == ".exe" || Path.GetExtension(file).ToLower() == ".dll")
                    {
                        Trace.WriteLine("Reading assembly: " + file);

                        Assembly ass = Assembly.LoadFile(file);
                        if (ass != null)
                        {
                            Type[] types = ass.GetTypes();
                            foreach (Type tp in types)
                            {
                                if (tp.GetInterface("APlayer.Core.IAudioRenderer") != null)
                                {
                                    // This is a video provider !!
                                    IAudioRenderer aprov = Activator.CreateInstance(tp) as IAudioRenderer;
                                    AudioRenderers.Add(aprov);
                                    Trace.WriteLine("Audio renderer added: " + aprov.Name + " [" + aprov.ID + "]");
                                }
                                if (tp.IsSubclassOf(typeof(IMediaFormat)) && !tp.IsAbstract)
                                {
                                    IMediaFormat fr = Activator.CreateInstance(tp) as IMediaFormat;
                                    MediaFormats.Add(fr);
                                    string ex = "";
                                    foreach (string e in fr.Extensions)
                                        ex += e + ", ";
                                    Trace.WriteLine("Media Format added: " + fr.Name + " [" + fr.ID + "](" + ex + ")");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("ERROR: " + ex.ToString());
                }
            }
            Trace.WriteLine("Done.", "APMain");
            Trace.WriteLine("Total of " + AudioRenderers.Count + " audio renderer found.", "APMain");
            Trace.WriteLine("Total of " + MediaFormats.Count + " media format found.", "APMain");

            APCore.SetTargetCPS(CoreSettings.CPS_TargetCPS);
            APCore.SetVolume(CoreSettings.Audio_Volume);
        }
        /// <summary>
        /// This should be called at the application close
        /// </summary>
        public static void Close()
        {
            APCore.Shutdown();

            Trace.WriteLine("Saving core settings ...", "APMain");
            double vol = 0;
            APCore.GetVolume(out vol);
            CoreSettings.Audio_Volume = (int)vol;

            CoreSettings.SaveSettings();
            Trace.WriteLine("Saving core settings success.", "APMain");
        }
        public static IAudioRenderer GetAudioRenderer(string id)
        {
            foreach (IAudioRenderer a in AudioRenderers)
            {
                if (a.ID == id)
                {
                    return a;
                }
            }
            return null;
        }
        public static IMediaFormat GetMediaFormat(string id)
        {
            foreach (IMediaFormat a in MediaFormats)
            {
                if (a.ID == id)
                {
                    return a;
                }
            }
            return null;
        }
        public static void SetupAudioRenderer(IntPtr handle)
        {
            Trace.WriteLine("Looking for the audio renderer that set in the settings...", "APMain");

            AudioRenderer = GetAudioRenderer(CoreSettings.Audio_RendererID);

            if (AudioRenderer == null)
            {
                Trace.TraceError("ERROR: cannot find the audio renderer that set in the settings", "APMain");
                Trace.TraceWarning("Deciding audio renderer ...", "APMain");
                if (AudioRenderers.Count > 0)
                {
                    CoreSettings.Audio_RendererID = AudioRenderers[0].ID;
                    AudioRenderer = AudioRenderers[0];
                    if (AudioRenderer != null)
                    {
                        Trace.WriteLine("Audio renderer is set to " + AudioRenderer.Name + " [" + AudioRenderer.ID + "]", "APMain");
                        AudioRenderer.Initialize(handle);
                        SetupRenderingMethods();
                    }
                    else
                    {
                        Trace.TraceError("ERROR: cannot set audio renderer.", "APMain");
                    }
                }
                else
                {
                    Trace.TraceError("ERROR: cannot set audio renderer, no audio renderers located.", "APMain");
                }
            }
            else
            {
                Trace.WriteLine("Audio renderer set to " + AudioRenderer.Name + " [" + AudioRenderer.ID + "]", "APMain");
                AudioRenderer.Initialize(handle);
                SetupRenderingMethods();
            }
        }
        public static void SetupRenderingMethods()
        {
            if (AudioRenderer != null)
            {
                APCore.SetupRenderingMethods(AudioRenderer.SubmitSamples, AudioRenderer.TogglePause, AudioRenderer.GetIsPlaying);
            }
            else
            {
                Trace.TraceError("ERROR: unable to setup rendering methods, one (or both) of the renderers is not set (video and/or audio renderer)", "APMain");
            }
        }
    }
}
