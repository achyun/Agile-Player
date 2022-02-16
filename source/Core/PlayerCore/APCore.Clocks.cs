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
using System.Threading;
using System.Diagnostics;

namespace APlayer.Core
{
    /*Clocks control section*/
    public partial class APCore
    {
        public static bool ON;
        public static bool PAUSED;
        public static bool isPaused;

        private static Thread mainThread;

        private static double cps_time_last;// the clocks end time (time point)
        private static double cps_time_start;// the clock start time (time point)
        private static double cps_time_token;// how much time in seconds the rendering process toke
        private static double cps_time_dead;// how much time remains for the target clock rate. For example, if a clock toke 3 milliseconds to complete, we'll have 16-3=13 dead time for 60 cps.
        private static double cps_time_period;// how much time in seconds a clock should take for target fps. i.e. ~16 milliseconds for 60 cps.
        private static double cps_time_frame_time;// the actual clock time after rendering and speed limiting. This should be equal to cps_time_period for perfect playing timing.

        private static double pl_time_target_cps;

        private static double cps_imm_av;
        private static double cps_clks_av;
        private static double cps_clks;
        private static double cps_second_start;

        private static double cps_av_cps_in_second;
        private static double cps_av_can_cps_in_second;
        private static double samples_src_av;
        private static double samples_trg_av;

        private static bool request_end_of_source_event_raise;

        private static bool render_audio_is_playing;

        public static void Clock()
        {
            while (ON)
            {
                if (!PAUSED)
                {
                    ClockPlayer();

                    if (target_clock_finished)
                        ClockFinished();
                }
                else
                {
                    // Pause the renderer
                    render_audio_get_is_playing(out render_audio_is_playing);
                    if (render_audio_is_playing)
                        render_audio_toggle_pause(true);
                    // Relieve the cpu a little bit.
                    Thread.Sleep(100);

                    if (request_end_of_source_event_raise)
                    {
                        request_end_of_source_event_raise = false;
                        EndOfSourceReached?.Invoke(null, new EventArgs());
                    }

                    isPaused = true;// Cleared in frame finish method
                }
            }
        }
        private static void ClockFinished()
        {
            // Called when target samples have been played in specific clock rate (cps).
            isPaused = false;
            target_clock_finished = false;

            // Feed audio renderer with audio data
            render_audio_get_is_playing(out render_audio_is_playing);
            if (!render_audio_is_playing)
                render_audio_toggle_pause(false);

            render_audio(ref audio_samples, ref audio_samples_added);

            audio_w_pos = 0;
            audio_samples_added = 0;

            // Set CPS at one second time
            if ((GetTime() - cps_second_start) >= 1)
            {
                cps_second_start = GetTime();

                if (cps_clks > 0)
                {
                    cps_av_cps_in_second = 1.0 / (cps_clks_av / cps_clks);
                    cps_av_can_cps_in_second = 1.0 / (cps_imm_av / cps_clks);
                    samples_src_av = audio_bytes_processed_from_source;
                    samples_trg_av = audio_bytes_processed_for_target;
                }

                cps_clks_av = cps_imm_av = cps_clks = 0;
                audio_bytes_processed_from_source = audio_bytes_processed_for_target = 0;
            }

            cps_imm_av += cps_time_token;
            cps_clks_av += cps_time_frame_time;
            cps_clks++;

            if (audio_target_bit_per_sample != 16)
                for (int i = 0; i < audio_channels_number; i++)
                {
                    audio_last_target_sample[i] = 0;
                }

            // Clock speed control
            cps_time_token = GetTime() - cps_time_start;

            if (cps_time_token > 0)
            {
                cps_time_dead = cps_time_period - cps_time_token;
                if (cps_time_dead > 0)
                {
                    Thread.Sleep((int)Math.Floor(cps_time_dead * 1000));

                    cps_time_dead = GetTime() - cps_time_start;
                    while (cps_time_period - cps_time_dead > 0)
                    {
                        cps_time_dead = GetTime() - cps_time_start;
                    }
                }

            }
            cps_time_last = GetTime();
            cps_time_frame_time = cps_time_last - cps_time_start;

            // This point is the start of the next clock
            cps_time_start = GetTime();
        }
        public static void Shutdown()
        {
            APMain.AudioRenderer.SignalToggle(false);

            render_audio_get_is_playing(out render_audio_is_playing);
            if (render_audio_is_playing)
                render_audio_toggle_pause(true);
            // Stop the thread
            Trace.WriteLine("Shutting down the player core...", "APCore");
            ON = false;

            // Wait ..
            if (mainThread != null)
            {
                Trace.WriteLine("Aborting thread ..", "APCore");
                mainThread.Abort();
                mainThread = null;
            }
        }

        private static double GetTime()
        {
            return Stopwatch.GetTimestamp() / (double)Stopwatch.Frequency;
        }
        public static void GetSpeedValues(out double clock_time, out double immediate_clock_time)
        {
            clock_time = cps_time_token;
            immediate_clock_time = cps_time_frame_time;
        }
        public static void GetAverageCPSInSecondValues(out double current_cpc, out double cps_can_last)
        {
            current_cpc = cps_av_cps_in_second;
            cps_can_last = cps_av_can_cps_in_second;
        }
        public static void GetAverageBytesProceesedInSecond(out double source_bytes, out double target_bytes)
        {
            source_bytes = samples_src_av;
            target_bytes = samples_trg_av;
        }
        public static void SetClockPerSecondsPeriod(ref double period)
        {
            cps_time_period = period;
        }
        public static void GetTargetCPS(out double cps)
        {
            cps = pl_time_target_cps;
        }
        public static void SetTargetCPS(double cps)
        {
            pl_time_target_cps = cps;
            cps_time_period = 1.0 / pl_time_target_cps;
        }
    }
}
