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
using System.Diagnostics;
using APlayer.Core;
using SlimDX.DirectSound;
using SlimDX.Multimedia;

namespace APlayer.Renderers
{
    public class SlimDXAudio : IAudioRenderer
    {
        private bool IsPaused;

        public string Name { get { return "SlimDX (DirectSound)"; } }
        public string ID { get { return "slimdx.directsound"; } }

        // slimdx
        public DirectSound _SoundDevice;
        public SecondarySoundBuffer buffer;

        private double temp;
        public bool IsRendering = false;
        public int BufferSize = 44100;
        private int channels_number = 0;
        private int bit_per_sample = 0;
        private int blocks_align = 0;
        private bool isInitialized;
        private int[][] buffer_internal;
        private int buffer_internal_size;
        private int buffer_internal_r_pos;
        private int buffer_internal_w_pos;
        private int fps_mode;
        private static double cps_normal;
        private static double cps_core_missle;
        private static double cps_core_extreme;
        private static double cps_pl_faster;

        // Don`t ask what are those and how i got them :D
        public int samples_period_max = 5156;//2291/2538/2777/5156
        public int samples_period_min = 2703;//1572/1469/1554/2703

        private byte[] buffer_playback;
        private int buffer_playback_size;
        private int buffer_playback_current_pos;
        private int buffer_playback_last_pos;
        private int buffer_playback_w_pos;
        private int buffer_playback_required_samples;
        private int buffer_playback_i;
        private long buffer_playback_currentSample;
        private int buffer_playback_currentSample_l;
        private int buffer_playback_currentSample_r;
        private int buffer_freq;
        private double target_fps;
        private int samples_added;

        System.IntPtr current_handle;

        public void GetIsPlaying(out bool playing)
        {
            playing = !IsPaused;
        }

        public void Initialize(System.IntPtr handle)
        {
            current_handle = handle;
            if (isInitialized)
            {
                Dispose();
            }
            isInitialized = false;

            //  LoadSettings();
            //Create the device
            Trace.WriteLine("DirectSound: Initializing directSound ...");
            _SoundDevice = new DirectSound();
            _SoundDevice.SetCooperativeLevel(current_handle, CooperativeLevel.Normal);

            //Create the wav format
            WaveFormat wav = new WaveFormat();
            wav.FormatTag = WaveFormatTag.Pcm;
            wav.SamplesPerSecond = APMain.CoreSettings.Audio_TargetFrequency;
            wav.Channels = (short)APMain.CoreSettings.Audio_TargetAudioChannels;
            wav.BitsPerSample = (short)APMain.CoreSettings.Audio_TargetBitsPerSample;
            wav.AverageBytesPerSecond = wav.SamplesPerSecond * wav.Channels * (wav.BitsPerSample / 8);
            wav.BlockAlignment = (short)(wav.Channels * wav.BitsPerSample / 8);

            bit_per_sample = wav.BitsPerSample;
            blocks_align = (wav.Channels * wav.BitsPerSample / 8);
            channels_number = APMain.CoreSettings.Audio_TargetAudioChannels;

            // TODO: set renderer buffer size
            // BufferSize = bit_per_sample * 1024;
            if (APMain.CoreSettings.Audio_TargetFrequency < 88200)
            {
                BufferSize = APMain.CoreSettings.Audio_RenderBufferInKB * (bit_per_sample / 8) * 1024;
            }
            else
            {
                // We need more buffering in this case !!
                BufferSize = APMain.CoreSettings.Audio_RenderBufferInKB * bit_per_sample * 1024;
            }

            buffer_internal_r_pos = 0;
            buffer_internal_w_pos = 0;
            samples_added = 0;
            buffer_playback_current_pos = 0;
            buffer_playback_last_pos = 0;
            buffer_playback_w_pos = 0;
            buffer_playback_required_samples = 0;
            buffer_playback_i = 0;
            buffer_playback_currentSample = 0;

            //samples_period_max = BufferSize - (1024 * 2);

            samples_period_min = (1024 * 2);
            samples_period_max = samples_period_min * 12;

            Trace.WriteLine("DirectSound: BufferSize = " + BufferSize + " Byte");
            //Description
            SoundBufferDescription des = new SoundBufferDescription();
            des.Format = wav;
            des.SizeInBytes = BufferSize;
            des.Flags = BufferFlags.GlobalFocus;

            buffer = new SecondarySoundBuffer(_SoundDevice, des);

            Trace.WriteLine("DirectSound: DirectSound initialized OK.");
            isInitialized = true;

            TogglePause(true);
            ShutDown();
        }
        public void Dispose()
        {
            if (isInitialized)
                if (!buffer.Disposed & !IsRendering)
                {
                    buffer.Stop();
                    IsPaused = true;
                }

            isInitialized = false;

            buffer.Dispose(); buffer = null;
            _SoundDevice.Dispose(); _SoundDevice = null;

            GC.Collect();
        }

        public void Reset()
        {
            // 1 Dispose
            Dispose();
            // 2 Initialize
            Initialize(current_handle);
        }

        public void ShutDown()
        {
            // Stop

            //  if (Recorder.IsRecording)
            //      Recorder.Stop();
            // Set buffers
            buffer_internal_size = BufferSize / 2;
            buffer_internal = new int[buffer_internal_size][];
            for (int i = 0; i < buffer_internal_size; i++)
            {
                buffer_internal[i] = new int[channels_number];
            }
            buffer_internal_r_pos = 0;
            buffer_internal_w_pos = 0;
            samples_added = 0;
            buffer_playback_size = BufferSize;
            buffer_playback = new byte[buffer_playback_size];
            buffer_playback_current_pos = 0;
            buffer_playback_last_pos = 0;
            buffer_playback_required_samples = 0;
            buffer_playback_i = 0;
            buffer_playback_currentSample = 0;
            /*// Noise on shutdown; MISC
            Random r = new Random();
            for (int i = 0; i < buffer_internal.Length; i++)
            {
                buffer_internal[i][0] = (byte)r.Next(0, 20);
                buffer_internal[i][1] = (byte)r.Next(0, 20);
            }

            for (int i = 0; i < buffer_playback.Length; i++)
                buffer_playback[i] = (byte)r.Next(0, 20);
            */
            for (int i = 0; i < buffer_internal.Length; i++)
            {
                for (int c = 0; c < buffer_internal[i].Length; c++)
                    buffer_internal[i][c] = 0;
            }

            for (int i = 0; i < buffer_playback.Length; i++)
                buffer_playback[i] = 0;
        }

        public void SignalToggle(bool started)
        {
            if (started)
            {
                /*// Noise on shutdown; MISC
                Random r = new Random();
                for (int i = 0; i < buffer_internal.Length; i++)
                {
                    buffer_internal[i][0] = (byte)r.Next(0, 20);
                    buffer_internal[i][1] = (byte)r.Next(0, 20);
                }

                for (int i = 0; i < buffer_playback.Length; i++)
                    buffer_playback[i] = (byte)r.Next(0, 20);
                */
                for (int i = 0; i < buffer_internal.Length; i++)
                {
                    for (int c = 0; c < buffer_internal[i].Length; c++)
                        buffer_internal[i][c] = 0;
                }

                for (int i = 0; i < buffer_playback.Length; i++)
                    buffer_playback[i] = 0;
            }
            else
            {
                ShutDown();
                TogglePause(true);
            }
        }

        public void SubmitSamples(ref int[][] aud_buffer, ref int samplesAdded)
        {
            if (!isInitialized)
                return;
            IsRendering = true;
            // Get the playback buffer needed samples
            buffer_playback_current_pos = buffer.CurrentWritePosition;
            buffer_playback_w_pos = buffer_playback_last_pos;

            buffer_playback_required_samples = buffer_playback_current_pos - buffer_playback_last_pos;
            if (buffer_playback_required_samples < 0)
                buffer_playback_required_samples = (buffer_playback_size - buffer_playback_last_pos) + buffer_playback_current_pos;

            // fill up the internal buffer using the audio buffer
            for (int i = 0; i < samplesAdded; i++)
            {

                if (buffer_internal_w_pos >= buffer_internal_size)
                    buffer_internal_w_pos = 0;

                // buffer_internal[buffer_internal_w_pos] = buffer_playback_currentSample;

                // TODO: do something in mono, it only take the right channel if the input is stereo
                for (int c = 0; c < channels_number; c++)
                {
                    buffer_internal[buffer_internal_w_pos][c] = aud_buffer[i][c % aud_buffer[i].Length];
                }

                buffer_internal_w_pos++;
                if (buffer_internal_w_pos >= buffer_internal_size)
                    buffer_internal_w_pos = 0;


                samples_added++;
            }

            for (buffer_playback_i = 0; buffer_playback_i < buffer_playback_required_samples; buffer_playback_i += blocks_align)
            {
                // Get the sample from the internal buffer
                if (buffer_internal_r_pos >= buffer_internal_size || buffer_internal_r_pos < 0)
                    buffer_internal_r_pos = 0;

                buffer_internal_r_pos++;
                if (buffer_internal_r_pos >= buffer_internal_size)
                    buffer_internal_r_pos = 0;

                // Put it in the playback buffer
                if (buffer_playback_w_pos >= buffer_playback_size)
                    buffer_playback_w_pos = 0;

                switch (channels_number)
                {
                    case 1:// Mono
                        {
                            switch (bit_per_sample)
                            {
                                case 8:
                                    {
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][0] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;
                                        break;
                                    }
                                case 16:
                                    {
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][0] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF00) >> 8);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;
                                        break;
                                    }
                                case 24:
                                    {
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][0] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF00) >> 8);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF0000) >> 16);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;
                                        break;
                                    }
                                case 32:
                                    {
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][0] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF00) >> 8);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF0000) >> 16);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF000000) >> 24);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:// Stereo
                        {
                            switch (bit_per_sample)
                            {
                                case 8:
                                    {
                                        // Right
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][0] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;
                                        // Left
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;
                                        break;
                                    }
                                case 16:
                                    {
                                        // Right
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][0] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF00) >> 8);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        // Left
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF00) >> 8);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;
                                        break;
                                    }
                                case 24:
                                    {
                                        // Right
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][0] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF00) >> 8);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF0000) >> 16);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        // Left
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF00) >> 8);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF0000) >> 16);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;
                                        break;
                                    }
                                case 32:
                                    {
                                        // Right
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][0] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF00) >> 8);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF0000) >> 16);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][0] & 0xFF000000) >> 24);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;
                                        // Left
                                        buffer_playback[buffer_playback_w_pos] = (byte)(buffer_internal[buffer_internal_r_pos][1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF00) >> 8);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF0000) >> 16);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;

                                        buffer_playback[buffer_playback_w_pos] = (byte)((buffer_internal[buffer_internal_r_pos][1 % APCore.CurrentMediaFormat.ChannelsNumber] & 0xFF000000) >> 24);
                                        buffer_playback_w_pos++;
                                        if (buffer_playback_w_pos >= buffer_playback_size)
                                            buffer_playback_w_pos = 0;
                                        break;
                                    }
                            }
                            break;
                        }
                }

                samples_added--;

            }
            //samples_added = 0;
            buffer.Write(buffer_playback, 0, LockFlags.EntireBuffer);
            buffer_playback_last_pos = buffer_playback_current_pos;


            // SPEED CONTROL !!
            // Adjust the emu speed by changing the frame period.
            // samples_added determines how many samples that left after emu generates them and playback uses them.
            // samples_added < 0 means playback is faster than emu, it take samples more than emu can generate at frame
            // samples_added > 0 means emu is faster than playback, emu generates frames more than playback can play at frame
            // The only solution for sound synchronization is to make the samples count at a rate than no pointers-overflow happens.
            // 2000 <= samples_added <= 2500 is the best rate.
            // SPEED CONTROL !!

            if (samples_added >= samples_period_max)
            {
                if (fps_mode != 1)
                {
                    fps_mode = 1;
                    // nes is faster than PL, make PL faster
                    APCore.SetClockPerSecondsPeriod(ref cps_pl_faster);
                    //Console.WriteLine("DirectSound: sound switched to PLAYER FASTER speed mode.");
                }
            }
            else if (samples_added < samples_period_min)
            {
                if (fps_mode != 2)
                {
                    fps_mode = 2;
                    // nes is very slow, make it missle to at least get some samples.
                    APCore.SetClockPerSecondsPeriod(ref cps_core_missle);
                    //Console.WriteLine("DirectSound: sound switched to NES MISSLE speed mode.");
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

            IsRendering = false;
            //last_time = current_time;
        }

        public void TogglePause(bool paused)
        {
            APCore.GetTargetCPS(out target_fps);

            /*fps_nes_missle = 1.0 / ((target_fps / 4) + (target_fps * 2));
            fps_pl_faster = 1.0 / (target_fps - (target_fps / 3));
            fps_nes_normal = 1.0 / target_fps;*/

            /*fps_nes_missle = 1.0 / 80;
            fps_pl_faster = 1.0 / 30;
            fps_nes_normal = 1.0 / target_fps;*/

            cps_core_missle = 1.0 / ((target_fps / 4) + (target_fps * 2) + 2);
            cps_pl_faster = 1.0 / (target_fps - (target_fps / 3));
            cps_normal = 1.0 / target_fps;

            cps_core_extreme = 1.0 / 100;

            APCore.SetClockPerSecondsPeriod(ref cps_core_missle);

            if (!paused)
            {
                IsPaused = false;
                if (isInitialized)
                    if (!buffer.Disposed & !IsRendering)
                        buffer.Play(0, PlayFlags.Looping);

                Trace.WriteLine("DirectSound: Playing.");
            }
            else
            {
                if (isInitialized)
                    if (!buffer.Disposed & !IsRendering)
                    {
                        buffer.Stop();
                        IsPaused = true;
                    }
                Trace.WriteLine("DirectSound: Stopped.");
            }
        }
    }
}
