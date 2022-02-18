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
using APlayer.Core;
using System.Runtime.InteropServices;
using SDL2;

namespace APlayer.Renderers
{
    unsafe class SDL2Audio : IAudioRenderer
    {
        private IntPtr current_handle;
        private byte[] audio_samples;
        private int samples_count;
        internal int samples_added;
        private int buffer_size;
        private bool pitch;
        private int buffer_size_6;
        private bool stereo_mode;
        private bool stereo_nes_mode;
        private int buffer_min;
        private int buffer_limit;
        private long w_pos;
        private long r_pos;
        private int sample_l;
        private int sample_r;
        private bool is_rendering;

        private bool ready;
        private bool enabled;
        private double cps_normal;
        private double cps_core_missle;
        private double cps_core_extreme;
        private double cps_pl_faster;
        private int fps_mode;
        private double volume = 0;
        internal SDL.SDL_AudioSpec specs;

        private uint audio_device_index;

        internal bool IsPlaying;
        public string Name
        { get { return "SDL2 Audio"; } }
        public string ID
        { get { return "sdl2.audio"; } }

        public void GetIsPlaying(out bool playing)
        {
            playing = IsPlaying;
        }

        public void Initialize(IntPtr handle)
        {
            current_handle = handle;
            ready = false;
            enabled = true;
            // TODO: setup buffer size
            buffer_size = 5474;
            //buffer_size = 4096;
            /*if (APMain.CoreSettings.Audio_TargetFrequency < 88200)
            {
                buffer_size = APMain.CoreSettings.Audio_RenderBufferInKB * (APMain.CoreSettings.Audio_TargetBitsPerSample / 8) * 1024;
            }
            else
            {
                // We need more buffering in this case !!
                buffer_size = APMain.CoreSettings.Audio_RenderBufferInKB * APMain.CoreSettings.Audio_TargetBitsPerSample * 1024;
            }*/
            buffer_min = 2 * 1024;
            buffer_limit = buffer_min + (2 * 1024);

            //buffer_min = (1024 * 2);
            //buffer_limit = buffer_min * 12;
            Console.WriteLine("SDL: Initializing audio ...");

            //SDL2Settings sdl_settings = new SDL2Settings(System.IO.Path.Combine(Program.WorkingFolder, "sdlsettings.ini"));
            //sdl_settings.LoadSettings();
#if DEBUG
            SDL.SDL_SetHint(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
#endif

            SDL.SDL_Init(SDL.SDL_INIT_AUDIO);

            int c = SDL.SDL_GetNumAudioDrivers();
            for (int i = 0; i < c; i++)
            {
                string n = SDL.SDL_GetAudioDeviceName(i, 0);
                Console.WriteLine(n);
            }

            // Open first device
            string audio_device = SDL.SDL_GetAudioDeviceName(0, 0);
            Console.WriteLine("Device = " + audio_device);

            specs = new SDL.SDL_AudioSpec();
            SDL.SDL_AudioSpec specs1 = new SDL.SDL_AudioSpec();// dummy

            specs.channels = (byte)APMain.CoreSettings.Audio_TargetAudioChannels;
            specs.format = SDL.AUDIO_S16;
            specs.freq = APMain.CoreSettings.Audio_TargetFrequency;

            stereo_mode = APMain.CoreSettings.Audio_TargetAudioChannels == 2;

            specs.samples = (ushort)buffer_size;
            specs.callback = AudioCallback;

            //samples_count = buffer_size * 20;
            if (APMain.CoreSettings.Audio_TargetFrequency < 88200)
            {
                samples_count = APMain.CoreSettings.Audio_RenderBufferInKB * (APMain.CoreSettings.Audio_TargetBitsPerSample / 8) * 1024;
            }
            else
            {
                // We need more buffering in this case !!
                samples_count = APMain.CoreSettings.Audio_RenderBufferInKB * APMain.CoreSettings.Audio_TargetBitsPerSample * 1024;
            }
            samples_count *= (APMain.CoreSettings.Audio_TargetBitsPerSample / 8) * APMain.CoreSettings.Audio_TargetAudioChannels;

            audio_samples = new byte[samples_count];

            //SDL.SDL_OpenAudio(ref specs, out specs1);
            audio_device_index = SDL.SDL_OpenAudioDevice(audio_device, 0, ref specs, out specs1, 0);
            if (audio_device_index == 0)
            {
                Console.WriteLine("ERROR INITAILIZING AUDIO DEVICE");
                // MyNesMain.VideoProvider.WriteErrorNotification("ERROR INITAILIZING AUDIO DEVICE, please configure SDL2 audio settings.", false);
            }
            w_pos = 0;
            r_pos = w_pos + buffer_size;

            IsPlaying = false;

            SDL.SDL_PauseAudioDevice(audio_device_index, 1);

            FixSpeed();

            ready = true;
            samples_added = 0;

            Console.WriteLine("SDL: Audio initialized success.");
        }
        private void FixSpeed()
        {
            fps_mode = 0;
            double target_fps = 0;
            APCore.GetTargetCPS(out target_fps);

            /*cps_core_missle = 1.0 / ((target_fps / 4) + (target_fps * 2) + 2);
            cps_pl_faster = 1.0 / (target_fps - (target_fps / 3));
            cps_normal = 1.0 / target_fps;*/

            cps_core_missle = 1.0 / (target_fps + 18);
            cps_pl_faster = 1.0 / (target_fps - 3);
            cps_normal = 1.0 / target_fps;

            cps_core_extreme = 1.0 / 100;

            APCore.SetClockPerSecondsPeriod(ref cps_core_missle);
        }
        public void Reset()
        {
            ShutDown();
            Initialize(current_handle);
        }

        public void ShutDown()
        {
            SDL.SDL_CloseAudio();
        }

        public void SignalToggle(bool started)
        {
            fps_mode = 0;
            FixSpeed();

            if (started)
            {
                // Noise on shutdown; MISC
                Random r = new Random();
                for (int i = 0; i < audio_samples.Length; i++)
                    audio_samples[i] = (byte)r.Next(0, 20);

                for (int i = 0; i < audio_samples.Length; i++)
                    audio_samples[i] = (byte)r.Next(0, 20);
            }
            else
            {
                TogglePause(true);
            }
        }

        public void SubmitSamples(ref int[][] au_buffer, ref int samples_a)
        {
            if (!enabled)
                return;
            if (!ready)
                return;
            // if (is_rendering)
            //     return;
            // Nes emu call this method at the end of each frame to fill the sound buffer...
            // Code should still work if this method is not called
            for (int i = 0; i < samples_a; i++)
            {
                sample_r = au_buffer[i][0];
                sample_l = au_buffer[i][1];

                audio_samples[w_pos++ % samples_count] = (byte)(sample_r & 0xFF);
                audio_samples[w_pos++ % samples_count] = (byte)((sample_r & 0xFF00) >> 8);

                if (stereo_mode)
                {
                    audio_samples[w_pos++ % samples_count] = (byte)(sample_l & 0xFF);
                    audio_samples[w_pos++ % samples_count] = (byte)((sample_l & 0xFF00) >> 8);
                }
                samples_added++;
            }
        }

        public void TogglePause(bool paused)
        {
            if (!enabled)
                return;
            if (paused)
                Pause();
            else
                Play();
        }
        internal void Play()
        {
            if (!enabled)
                return;
            if (!IsPlaying)
            {
                IsPlaying = true;
                SDL.SDL_PauseAudioDevice(audio_device_index, 0);

                FixSpeed();
            }
        }
        internal void Pause()
        {
            if (!enabled)
                return;
            if (IsPlaying)
            {
                IsPlaying = false;
                SDL.SDL_PauseAudioDevice(audio_device_index, 1);
            }
        }
        private void AudioCallback(IntPtr userdata, IntPtr stream, int len)
        {
            if (!enabled)
                return;
            if (!ready)
                return;
            is_rendering = true;

            // Write it to the data
            for (int i = 0; i < len; i++)
            {
                ((byte*)stream)[i] = audio_samples[r_pos++ % samples_count];
            }

            samples_added -= len / (stereo_mode ? 4 : 2);

            // SPEED CONTROL !!
            if (samples_added >= buffer_limit)
            {
                if (fps_mode != 1)
                {
                    fps_mode = 1;
                    // nes is faster than PL, make PL faster
                    APCore.SetClockPerSecondsPeriod(ref cps_pl_faster);
                    //Console.WriteLine("DirectSound: sound switched to PLAYER FASTER speed mode.");
                }
            }
            else if (samples_added <= buffer_min)
            {
                if (fps_mode != 2)
                {
                    fps_mode = 2;
                    // nes is very slow, make it missle to at least get some samples.
                    APCore.SetClockPerSecondsPeriod(ref cps_core_missle);
                    //Console.WriteLine("DirectSound: RENDERER IS FASTER !! sound switched to NES MISSLE speed mode.");
                }
            }
            else
            {
                if (fps_mode != 0)
                {
                    fps_mode = 0;
                    // between 1000 and 2000, set to normal speed
                    APCore.SetClockPerSecondsPeriod(ref cps_normal);
                    //Console.WriteLine("DirectSound: sound switched to normal speed mode.");
                }
            }

            is_rendering = false;
        }

    }
}
