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
using System.Threading;
using System.Diagnostics;

namespace APlayer.Core
{
    public partial class APCore
    {
        private static RenderAudioSamples render_audio;
        private static TogglePause render_audio_toggle_pause;
        private static GetIsPlaying render_audio_get_is_playing;
        private static bool render_initialized;
        /// <summary>
        /// Get if the renderers are initialized. This must returns true before the core can be used.
        /// </summary>
        public static bool IsRenderersInitialzed { get { return render_initialized; } }

        internal static void SetupRenderingMethods(RenderAudioSamples renderAudio, TogglePause renderTogglePause, GetIsPlaying renderGetIsPlaying)
        {
            render_initialized = false;
            render_audio = renderAudio;
            render_audio_toggle_pause = renderTogglePause;
            render_audio_get_is_playing = renderGetIsPlaying;
            render_initialized = render_audio != null && render_audio_toggle_pause != null && render_audio_get_is_playing != null;
            if (render_initialized)
                Trace.TraceInformation("Renderer methods initialized successfully.", "APCore");
            else
            {
                Trace.TraceError("ERROR RENDERER INITIALIZING !!", "APCore");
                Trace.TraceError("Failed to initialize the renderers methods. Please use the method 'SetupRenderingMethods' to initialize the renderers methods before you can run the player.", "APCore");
            }
        }

        /// <summary>
        /// Load media file and start the player
        /// </summary>
        /// <param name="filePath">The full media file path </param>
        /// <param name="formatID">The media format id to use to open the media file</param>
        /// <param name="useThread">Indicates if a thread should be used to run the player.</param>
        /// <param name="success">Set to true if media is loaded and player is running, otherwise false.</param>
        public static void LoadFile(string filePath, string formatID, bool useThread, out bool success)
        {
            Trace.WriteLine("Loading file into the player started ...", "APCore");
            if (!render_initialized)
            {
                Trace.TraceError("NO RENDERER INITIALIZED !! PLAYER CANNOT BE INTIALIZED WITHOUT A RENDERER !!", "APCore");
                Trace.TraceError("Please use the method 'SetupRenderingMethods' to initialize the renderers methods before you can use the player.", "APCore");
                success = false;
                return;
            }
            render_audio_toggle_pause(true);

            // Get the media format. Dispose current one if it set
            if (media_format != null)
            {
                Stop();
                media_format.Dispose();
                media_format = null;
            }

            Shutdown();

            Trace.WriteLine("Getting media format with id " + formatID, "APCore");
            media_format = APMain.GetMediaFormat(formatID);

            if (media_format == null)
            {
                Trace.TraceError("ERROR: Cannot open file at: " + filePath, "APCore");
                Trace.TraceError("ERROR: Cannot find media format with id " + formatID + filePath, "APCore");
                success = false;
                return;
            }

            Trace.WriteLine("Media format found: " + media_format.Name, "APCore");

            Trace.WriteLine("Opening file with media format ... ", "APCore");
            media_format.LoadFile(filePath);

            if (media_format.FileLoaded)
            {
                SetTargetCPS(APMain.CoreSettings.CPS_TargetCPS);

                InitiailizePlayer();

                media_format.Position = 0;
                success = true;
                ON = true;
                PAUSED = false;
                isPaused = false;

                if (useThread)
                {

                    Trace.WriteLine("Opening file with media format success. ", "APCore");
                    Trace.WriteLine("Player is running now in a thread.", "APCore");
                    mainThread = new Thread(new ThreadStart(Clock));

                    mainThread.Start();
                }

                APMain.AudioRenderer.SignalToggle(true);
                render_audio_toggle_pause(false);
            }
            else
            {
                Trace.TraceError("ERROR: Cannot open file at: " + filePath, "APCore");
                Trace.TraceError("ERROR: Cannot open the file with media format with id " + formatID + filePath, "APCore");
                success = false;
                return;
            }
        }
        /// <summary>
        /// Record current media file into wav file with target settings, using the engine for downsampling/upsampling/bits-converting if any.
        /// </summary>
        /// <param name="filePath">The file path where to store the file</param>
        public static void RecordToWav(string filePath)
        {
            if (!ON)
                return;
            // 1 stop current media
            Stop();
            // 2 make the wave file
            audio_recorder = new WaveRecorder();
            audio_recorder.Record(filePath, (short)APMain.CoreSettings.Audio_TargetAudioChannels, (short)APMain.CoreSettings.Audio_TargetBitsPerSample, APMain.CoreSettings.Audio_TargetFrequency);
            // Seek to 0 position
            media_format.Position = 0;
            audio_record_mode = true;

            while (audio_recorder.IsRecording)
            {
                ClockPlayer();
            }

            audio_record_mode = false;
            // Stop current media again !
            Stop();
        }
    }
}
