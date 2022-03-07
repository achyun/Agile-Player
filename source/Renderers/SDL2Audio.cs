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
        private int[] temp_sample;
        private int samples_count;
        internal int samples_added;
        private int buffer_size;
        private bool stereo_mode;
        private int buffer_min;
        private int buffer_limit;
        private long w_pos;
        private long r_pos;
        private bool is_rendering;
        private int channel_numbers;
        private int bits_per_sample;
        private int blocks_align;

        private bool ready;
        private bool enabled;
        private double cps_normal;
        private double cps_core_missle;
        private double cps_core_extreme;
        private double cps_pl_faster;
        private int fps_mode;
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
            // buffer_size = 3 * 1024;
            buffer_size = 4096;
            /*if (APMain.CoreSettings.Audio_TargetFrequency < 88200)
            {
                buffer_size = APMain.CoreSettings.Audio_RenderBufferInKB * (APMain.CoreSettings.Audio_TargetBitsPerSample / 8) * 1024;
            }
            else
            {
                // We need more buffering in this case !!
                buffer_size = APMain.CoreSettings.Audio_RenderBufferInKB * APMain.CoreSettings.Audio_TargetBitsPerSample * 1024;
            }*/


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

            bits_per_sample = APMain.CoreSettings.Audio_TargetBitsPerSample;
            channel_numbers = APMain.CoreSettings.Audio_TargetAudioChannels;

            specs = new SDL.SDL_AudioSpec();
            SDL.SDL_AudioSpec specs1 = new SDL.SDL_AudioSpec();// dummy

            specs.channels = (byte)APMain.CoreSettings.Audio_TargetAudioChannels;
            switch (bits_per_sample)
            {
                case 8:
                    {
                        specs.format = SDL.AUDIO_U8;
                        break;
                    }
                case 16:
                case 24:// 24 Bits is not supported right now with SDL2, it gonna be converted into signed 16 bits
                    {
                        specs.format = SDL.AUDIO_S16;
                        break;
                    }
                case 32:
                    {
                        specs.format = SDL.AUDIO_S32;
                        break;
                    }
            }


            specs.freq = APMain.CoreSettings.Audio_TargetFrequency;

            specs.samples = (ushort)buffer_size;
            specs.callback = AudioCallback;

            blocks_align = channel_numbers * (bits_per_sample / 8);
            //samples_count = buffer_size * 20;
            /*if (APMain.CoreSettings.Audio_TargetFrequency < 88200)
            {
                samples_count = APMain.CoreSettings.Audio_RenderBufferInKB * (APMain.CoreSettings.Audio_TargetBitsPerSample / 8) * 1024;
            }
            else
            {
                // We need more buffering in this case !!
                samples_count = APMain.CoreSettings.Audio_RenderBufferInKB * APMain.CoreSettings.Audio_TargetBitsPerSample * 1024;
            }
            samples_count *= (APMain.CoreSettings.Audio_TargetBitsPerSample / 8) * APMain.CoreSettings.Audio_TargetAudioChannels;*/

            samples_count = 44 * 1024;
            // buffer_min = 1024 * 2;
            // buffer_limit = samples_count + (1024 * 2);
            buffer_min = 512;
            buffer_limit = buffer_size + 512;

            temp_sample = new int[APMain.CoreSettings.Audio_TargetAudioChannels];
            audio_samples = new byte[samples_count];

            //SDL.SDL_OpenAudio(ref specs, out specs1);
            audio_device_index = SDL.SDL_OpenAudioDevice(audio_device, 0, ref specs, out specs1, 0);
            if (audio_device_index == 0)
            {
                Console.WriteLine("ERROR INITAILIZING AUDIO DEVICE");
                // MyNesMain.VideoProvider.WriteErrorNotification("ERROR INITAILIZING AUDIO DEVICE, please configure SDL2 audio settings.", false);
            }
            w_pos = 0;
            r_pos = 0;

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

            cps_core_missle = 1.0 / (target_fps + 29);
            cps_pl_faster = 1.0 / (target_fps - 3);
            //cps_core_missle = 1.0 / (target_fps + 20);
            //cps_pl_faster = 1.0 / (target_fps - 1);
            cps_normal = 1.0 / target_fps;

            cps_core_extreme = 1.0 / 100;

            //APCore.SetClockPerSecondsPeriod(ref cps_core_missle);
        }
        public void Reset()
        {
            ShutDown();
            Initialize(current_handle);
        }

        public void ShutDown()
        {
            SDL.SDL_CloseAudio();
            for (int i = 0; i < audio_samples.Length; i++)
                audio_samples[i] = 0;
        }

        public void SignalToggle(bool started)
        {
            fps_mode = 0;
            FixSpeed();

            if (started)
            {
                for (int i = 0; i < audio_samples.Length; i++)
                    audio_samples[i] = 0;
            }
            else
            {
                samples_added = 0;
                w_pos = 0;
                r_pos = 0;
                for (int i = 0; i < audio_samples.Length; i++)
                    audio_samples[i] = 0;
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
                // TODO: do something in mono, it only take the right channel if the input is stereo
                for (int c = 0; c < channel_numbers; c++)
                {
                    temp_sample[c] = au_buffer[i][c % au_buffer[i].Length];
                }

                switch (channel_numbers)
                {
                    case 1:// Mono
                        {
                            switch (bits_per_sample)
                            {
                                case 8:
                                    {
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[0] & 0xFF);
                                        break;
                                    }
                                case 16:
                                    {
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[0] & 0xFF);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[0] & 0xFF00) >> 8);
                                        break;
                                    }
                                case 24:
                                    {
                                        // 24 Bits is not supported by SDL2 right now, Convert from signed 24 bit into 16 signed byte
                                        // Doing just bit shifting is wrong, the range should be converted from −8388608 to 8388607 into -32768 to 32767.
                                        for (int channel = 0; channel < channel_numbers; channel++)
                                        {
                                            long val_long = temp_sample[channel];

                                            if (val_long > 0)
                                            {
                                                val_long = (val_long * short.MaxValue) / 8388607;
                                            }
                                            else
                                            {
                                                val_long = (val_long * short.MinValue) / -8388608;
                                            }

                                            byte byte_1 = (byte)(val_long & 0xFF);
                                            byte byte_2 = (byte)((val_long & 0xFF00) >> 8);

                                            temp_sample[channel] = (byte_2 << 8) | byte_1;
                                        }
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[0] & 0xFF);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[0] & 0xFF00) >> 8);
                                        break;
                                    }
                                case 32:
                                    {
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[0] & 0xFF);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[0] & 0xFF00) >> 8);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[0] & 0xFF0000) >> 16);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[0] & 0xFF000000) >> 24);
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:// Stereo
                        {
                            switch (bits_per_sample)
                            {
                                case 8:
                                    {
                                        // Right
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[0] & 0xFF);
                                        // Left
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF);
                                        break;
                                    }
                                case 16:
                                    {
                                        // Right
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[0] & 0xFF);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[0] & 0xFF00) >> 8);
                                        // Left
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF00) >> 8);
                                        break;
                                    }
                                case 24:
                                    {
                                        // 24 Bits is not supported by SDL2 right now, Convert from signed 24 bit into 16 signed byte
                                        // Doing just bit shifting is wrong, the range should be converted from −8388608 to 8388607 into -32768 to 32767.
                                        for (int channel = 0; channel < channel_numbers; channel++)
                                        {
                                            long val_long = temp_sample[channel];

                                            if (val_long > 0)
                                            {
                                                val_long = (val_long * short.MaxValue) / 8388607;
                                            }
                                            else
                                            {
                                                val_long = (val_long * short.MinValue) / -8388608;
                                            }

                                            byte byte_1 = (byte)(val_long & 0xFF);
                                            byte byte_2 = (byte)((val_long & 0xFF00) >> 8);

                                            temp_sample[channel] = (byte_2 << 8) | byte_1;
                                        }
                                        // Right
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[0] & 0xFF);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[0] & 0xFF00) >> 8);
                                        // Left
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF00) >> 8);
                                        break;
                                    }
                                case 32:
                                    {
                                        // Mono
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[0] & 0xFF);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[0] & 0xFF00) >> 8);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[0] & 0xFF0000) >> 16);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[0] & 0xFF000000) >> 24);
                                        // Right 
                                        audio_samples[w_pos++ % samples_count] = (byte)(temp_sample[1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF00) >> 8);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF0000) >> 16);
                                        audio_samples[w_pos++ % samples_count] = (byte)((temp_sample[1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF000000) >> 24);
                                        break;
                                    }
                            }
                            break;
                        }
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

            samples_added -= len / blocks_align;

            // SPEED CONTROL !!
            if (samples_added >= buffer_limit)
            {
                if (fps_mode != 1)
                {
                    fps_mode = 1;
                    // nes is faster than PL, make PL faster
                    APCore.SetClockPerSecondsPeriod(ref cps_pl_faster);
                    Console.WriteLine("DirectSound: sound switched to PLAYER FASTER speed mode.");
                }
            }
            else if (samples_added <= buffer_min)
            {
                if (fps_mode != 2)
                {
                    fps_mode = 2;
                    // nes is very slow, make it missle to at least get some samples.
                    APCore.SetClockPerSecondsPeriod(ref cps_core_missle);
                    Console.WriteLine("DirectSound: RENDERER IS FASTER !! sound switched to NES MISSLE speed mode.");
                }
            }
            else
            {
                if (fps_mode != 0)
                {
                    fps_mode = 0;
                    // between 1000 and 2000, set to normal speed
                    APCore.SetClockPerSecondsPeriod(ref cps_normal);
                    Console.WriteLine("DirectSound: sound switched to normal speed mode.");
                }
            }

            is_rendering = false;
        }

    }
}
