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

namespace APlayer.Core
{
    /*Playing section*/
    public partial class APCore
    {
        private static WaveRecorder audio_recorder;
        private static bool audio_record_mode;
        private static int[][] audio_samples;
        private static int audio_w_pos;
        private static int audio_samples_added;
        internal static int audio_samples_count;
        private static bool target_clock_finished;
        private static int audio_wav_fix_mode;
        private static IMediaFormat media_format;
        public static SamplingMode audio_sampling_mode;

        private static double audio_bp_samples_needed;
        private static double audio_bp_samples_required;
        private static double audio_bp_samples_got_timer;
        public static bool audio_bp_bits_per_sample_converting_needed;

        private static double audio_bp_ratio1;
        private static double audio_bp_ratio1_timer;
        private static double audio_bp_ratio2;
        private static double audio_bp_ratio2_timer;
        private static bool audio_sample_obtained;

        private static double audio_bytes_processed_from_source;
        private static double audio_bytes_processed_for_target;
        private static double audio_src_bytes_per_sample;
        private static double audio_trg_bytes_per_sample;

        private static int audio_bit_per_sample;
        private static int audio_target_bit_per_sample;
        private static int audio_channels_number;
        private static int[] audio_t;
        private static int[] audio_x;
        private static int[] audio_y;
        private static double volume;

        public static int[] audio_last_source_sample;
        public static int[] audio_last_target_sample;
        private static bool audio_use_db_fix;

        public static event EventHandler<EventArgs> EndOfSourceReached;

        /// <summary>
        /// Get currently opened media format, it will be null if there is no media format set.
        /// </summary>
        public static IMediaFormat CurrentMediaFormat { get { return media_format; } }

        public static void InitiailizePlayer()
        {
            audio_wav_fix_mode = APMain.CoreSettings.Audio_Wave_Fix_Mode;
            audio_src_bytes_per_sample = media_format.ChannelsNumber * (media_format.BitsPerSample / 8);
            audio_trg_bytes_per_sample = media_format.ChannelsNumber * (APMain.CoreSettings.Audio_TargetBitsPerSample / 8);
            audio_channels_number = media_format.ChannelsNumber;
            audio_bit_per_sample = media_format.BitsPerSample;
            audio_target_bit_per_sample = APMain.CoreSettings.Audio_TargetBitsPerSample;
            audio_use_db_fix = APMain.CoreSettings.Audio_DB_Fix_Enabled;

            // Ratio 1 is how many samples can get per clock from the source
            // Source file frequency / target cps
            audio_bp_samples_needed = (double)media_format.Frequency / pl_time_target_cps;
            audio_bp_samples_required = (double)APMain.CoreSettings.Audio_TargetFrequency / pl_time_target_cps;

            // clocks on gathering samples from the source
            audio_bp_ratio1 = audio_bp_samples_needed / (cps_time_period * 1000);// how many samples each clock

            if (APMain.CoreSettings.Audio_TargetFrequency == media_format.Frequency)
                audio_sampling_mode = SamplingMode.None;
            else if (APMain.CoreSettings.Audio_TargetFrequency > media_format.Frequency)
                audio_sampling_mode = SamplingMode.Upsampling;
            else
                audio_sampling_mode = SamplingMode.Downsampling;

            audio_bp_ratio2 = (double)media_format.Frequency / (double)APMain.CoreSettings.Audio_TargetFrequency;

            audio_bp_ratio1_timer = 0;
            audio_bp_ratio2_timer = 0;

            audio_bp_bits_per_sample_converting_needed = media_format.BitsPerSample != APMain.CoreSettings.Audio_TargetBitsPerSample;

            audio_samples_count = (int)(audio_bp_samples_required * 2);
            audio_samples = new int[audio_samples_count][];
            for (int i = 0; i < audio_samples_count; i++)
                audio_samples[i] = new int[audio_channels_number];

            audio_t = new int[audio_channels_number];
            audio_x = new int[audio_channels_number];
            audio_y = new int[audio_channels_number];
            audio_last_source_sample = new int[audio_channels_number];
            audio_last_target_sample = new int[audio_channels_number];
            audio_w_pos = 0;
            audio_samples_added = 0;
        }
        private static void ApplyWaveFixAndVolume(ref int[] sample_in, out int[] sample_out)
        {
            sample_out = new int[audio_channels_number];
            switch (audio_wav_fix_mode)
            {
                case 0:// None
                    {
                        // No fix, just copy the samples
                        for (int i = 0; i < audio_channels_number; i++)
                        {
                            sample_out[i] = sample_in[i];
                        }
                        break;
                    }
                case 1:// Shift
                    {
                        switch (audio_bit_per_sample)// of the source
                        {
                            case 8:
                                {
                                    // The audio form is already shifted. (all above zero)
                                    for (int i = 0; i < audio_channels_number; i++)
                                    {
                                        sample_out[i] = (int)(sample_in[i] * volume);
                                    }
                                    break;
                                }
                            case 16:
                                {
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = (int)(sample_in[channel] * volume) + short.MaxValue;// this will shift all values above 0, now it ranges between 0 and 65535

                                        val_long = ((val_long * short.MaxValue) / ushort.MaxValue);

                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);

                                        sample_out[channel] = (byte_2 << 8) | byte_1;

                                    }
                                    break;
                                }
                            case 24:
                                {
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = (int)(sample_in[channel] * volume) + 8388608;// this will shift all values above 0, now it ranges between 0 and 16777216

                                        val_long = ((val_long * 8388608) / 16777216);

                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);
                                        byte byte_3 = (byte)((val_long & 0xFF0000) >> 16);

                                        sample_out[channel] = (byte_3 << 16) | (byte_2 << 8) | byte_1;
                                    }
                                    break;
                                }
                            case 32:
                                {
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = (int)(sample_in[channel] * volume) + int.MaxValue;// this will shift all values above 0, now it ranges between 0 and 4294967296

                                        val_long = ((val_long * int.MaxValue) / 4294967296);

                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);
                                        byte byte_3 = (byte)((val_long & 0xFF0000) >> 16);
                                        byte byte_4 = (byte)((val_long & 0xFF000000) >> 24);

                                        sample_out[channel] = (byte_4 << 24) | (byte_3 << 16) | (byte_2 << 8) | byte_1;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
        private static void ConvertBits(ref int[] sample_in, out int[] sample_out)
        {
            sample_out = new int[audio_channels_number];
            switch (audio_bit_per_sample)
            {
                case 8:
                    {
                        switch (audio_target_bit_per_sample)
                        {
                            case 8:
                                {
                                    // Target is as same as the source, do nothing, just copy channels
                                    for (int i = 0; i < audio_channels_number; i++)
                                    {
                                        sample_out[i] = sample_in[i];
                                    }
                                    break;
                                }
                            case 16:
                                {
                                    // Convert from 8 bit into signed 16 bit 
                                    // The range of signed integers that can be represented in 8 bits is −128 to 127.
                                    // But in PCM or RIFF audio, 8 bits are only stored in bytes 0 to 255. 
                                    // In this case we need to reverse the shifting on the wave from to be above zero, from 0-127 to 127-255 range (127 become the center instead of 0) into -32768 to 32767 
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = ((sample_in[channel] & 0xFF) + sbyte.MinValue);

                                        if (val_long > 0)
                                        {
                                            val_long = ((val_long * short.MaxValue) / sbyte.MaxValue);
                                        }
                                        else
                                        {
                                            val_long = ((val_long * short.MinValue) / sbyte.MinValue);
                                        }
                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);

                                        sample_out[channel] = (byte_2 << 8) | byte_1;

                                    }
                                    break;
                                }
                            case 24:
                                {
                                    // Convert from signed 8 bit into signed 24 bit
                                    // The range of signed integers that can be represented in 24 bits is −8,388,608 to 8,388,607.
                                    // Doing just bit shifting is wrong, the range should be converted from -128 to 127 into −8388608 to 8388607.
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = ((sample_in[channel] & 0xFF) + sbyte.MinValue);

                                        if (val_long > 0)
                                        {
                                            val_long = ((val_long * 8388607) / sbyte.MaxValue);
                                        }
                                        else
                                        {
                                            val_long = ((val_long * -8388608) / sbyte.MinValue);
                                        }
                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);
                                        byte byte_3 = (byte)((val_long & 0xFF0000) >> 16);

                                        sample_out[channel] = (byte_3 << 16) | (byte_2 << 8) | byte_1;
                                    }
                                    break;
                                }
                            case 32:
                                {
                                    // Convert from signed 8 bit into signed 32 bit
                                    // The range of signed integers that can be represented in 24 bits is −2147483648 to 2147483647.
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = ((sample_in[channel] & 0xFF) + sbyte.MinValue);

                                        if (val_long > 0)
                                        {
                                            val_long = ((val_long * int.MaxValue) / sbyte.MaxValue);
                                        }
                                        else
                                        {
                                            val_long = ((val_long * int.MinValue) / sbyte.MinValue);
                                        }
                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);
                                        byte byte_3 = (byte)((val_long & 0xFF0000) >> 16);
                                        byte byte_4 = (byte)((val_long & 0xFF000000) >> 24);

                                        sample_out[channel] = (byte_4 << 24) | (byte_3 << 16) | (byte_2 << 8) | byte_1;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 16:
                    {
                        // The player only accept signed-16 bit. If it is not signed, the media-format should take care of that.
                        switch (audio_target_bit_per_sample)
                        {
                            case 8:
                                {
                                    // Convert from signed 16 bit into 8 unsigned byte
                                    // The range of signed integers that can be represented in 8 bits is −128 to 127.
                                    // But in PCM or RIFF audio, 8 bits are only stored in bytes 0 to 255. 
                                    // In this case we need to do shifting on the wave from to be above zero, from -32768 to 32767 range into 0-127 to 127-255 (127 become the center instead of 0)
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = sample_in[channel];

                                        if (val_long > 0)
                                        {
                                            val_long = (val_long * sbyte.MaxValue) / short.MaxValue;
                                            val_long = val_long + sbyte.MaxValue;
                                        }
                                        else
                                        {
                                            val_long = (val_long * sbyte.MinValue) / short.MinValue;
                                            val_long = val_long - sbyte.MinValue;
                                        }
                                        sample_out[channel] = (byte)(val_long & 0xFF);
                                    }
                                    break;
                                }
                            case 16:
                                {
                                    // Target is as same as the source, do nothing, just copy channels
                                    for (int i = 0; i < audio_channels_number; i++)
                                    {
                                        sample_out[i] = sample_in[i];
                                    }
                                    break;
                                }
                            case 24:
                                {
                                    // Convert from signed 16 bit into signed 24 bit
                                    // The range of signed integers that can be represented in 24 bits is −8,388,608 to 8,388,607.
                                    // Doing just bit shifting is wrong, the range should be converted from -32768 to 32767 into −8388608 to 8388607.
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = sample_in[channel];

                                        if (val_long > 0)
                                        {
                                            val_long = ((val_long * 8388607) / short.MaxValue);
                                        }
                                        else
                                        {
                                            val_long = ((val_long * -8388608) / short.MinValue);
                                        }
                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);
                                        byte byte_3 = (byte)((val_long & 0xFF0000) >> 16);

                                        sample_out[channel] = (byte_3 << 16) | (byte_2 << 8) | byte_1;
                                    }
                                    break;
                                }
                            case 32:
                                {
                                    // Convert from signed 16 bit into signed 32 bit
                                    // The range of signed integers that can be represented in 32 bits is −2147483648 to 2147483647.
                                    // Doing just bit shifting is wrong, the range should be converted from -32768 to 32767 into −2147483648 to 2147483647.
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = sample_in[channel];

                                        if (val_long > 0)
                                        {
                                            val_long = ((val_long * int.MaxValue) / short.MaxValue);
                                        }
                                        else
                                        {
                                            val_long = ((val_long * int.MinValue) / short.MinValue);
                                        }
                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);
                                        byte byte_3 = (byte)((val_long & 0xFF0000) >> 16);
                                        byte byte_4 = (byte)((val_long & 0xFF000000) >> 24);

                                        sample_out[channel] = (byte_4 << 24) | (byte_3 << 16) | (byte_2 << 8) | byte_1;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 24:
                    {
                        switch (audio_target_bit_per_sample)
                        {
                            case 8:
                                {
                                    // Convert from signed 24 bit into 8 unsigned byte
                                    // The range of signed integers that can be represented in 8 bits is −128 to 127.
                                    // But in PCM or RIFF audio, 8 bits are only stored in bytes 0 to 255. 
                                    // In this case we need to do shifting on the wave from to be above zero, from −8388608 to 8388607 range into 0-127 to 127-255 (127 become the center instead of 0)
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = sample_in[channel];

                                        if (val_long > 0)
                                        {
                                            val_long = (val_long * sbyte.MaxValue) / 8388607;
                                            val_long = val_long + sbyte.MaxValue;
                                        }
                                        else
                                        {
                                            val_long = (val_long * sbyte.MinValue) / -8388608;
                                            val_long = val_long - sbyte.MinValue;
                                        }
                                        sample_out[channel] = (byte)(val_long & 0xFF);
                                    }
                                    break;
                                }
                            case 16:
                                {
                                    // Convert from signed 24 bit into 16 signed byte
                                    // Doing just bit shifting is wrong, the range should be converted from −8388608 to 8388607 into -32768 to 32767.
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = sample_in[channel];

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

                                        sample_out[channel] = (byte_2 << 8) | byte_1;
                                    }
                                    break;
                                }
                            case 24:
                                {
                                    // Target is as same as the source, do nothing, just copy channels
                                    for (int i = 0; i < audio_channels_number; i++)
                                    {
                                        sample_out[i] = sample_in[i];
                                    }
                                    break;
                                }
                            case 32:
                                {
                                    // Convert from signed 24 bit into 32 signed byte
                                    // Doing just bit shifting is wrong, the range should be converted from −8388608 to 8388607 into −2147483648 to 2147483647.
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = sample_in[channel];

                                        if (val_long > 0)
                                        {
                                            val_long = (val_long * int.MaxValue) / 8388607;
                                        }
                                        else
                                        {
                                            val_long = (val_long * int.MinValue) / -8388608;
                                        }

                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);
                                        byte byte_3 = (byte)((val_long & 0xFF0000) >> 16);
                                        byte byte_4 = (byte)((val_long & 0xFF000000) >> 24);

                                        sample_out[channel] = (byte_4 << 24) | (byte_3 << 16) | (byte_2 << 8) | byte_1;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 32:
                    {
                        switch (audio_target_bit_per_sample)
                        {
                            case 8:
                                {
                                    // Convert from signed 32 bit into 8 unsigned byte
                                    // The range of signed integers that can be represented in 8 bits is −128 to 127.
                                    // But in PCM or RIFF audio, 8 bits are only stored in bytes 0 to 255. 
                                    // In this case we need to do shifting on the wave from to be above zero, from −2147483648 to 2147483647 range into 0-127 to 127-255 (127 become the center instead of 0)
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = sample_in[channel];
                                        if (val_long > 0)
                                        {
                                            val_long = (val_long * sbyte.MaxValue) / int.MaxValue;
                                            val_long = val_long + sbyte.MaxValue;
                                        }
                                        else
                                        {
                                            val_long = (val_long * sbyte.MinValue) / int.MinValue;
                                            val_long = val_long - sbyte.MinValue;
                                        }

                                        sample_out[channel] = (byte)(val_long & 0xFF);
                                    }
                                    break;
                                }
                            case 16:
                                {
                                    // Convert from signed 32 bit into 16 signed byte
                                    // Doing just bit shifting is wrong, the range should be converted from −2147483648 to 2147483647 into -32768 to 32767.
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = sample_in[channel];

                                        if (val_long > 0)
                                        {
                                            val_long = (val_long * short.MaxValue) / int.MaxValue;
                                        }
                                        else
                                        {
                                            val_long = (val_long * short.MinValue) / int.MinValue;
                                        }

                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);

                                        sample_out[channel] = (byte_2 << 8) | byte_1;
                                    }
                                    break;
                                }
                            case 24:
                                {
                                    // Convert from signed 32 bit into 24 signed byte
                                    // Doing just bit shifting is wrong, the range should be converted from −2147483648 to 2147483647 into −8388608 to 8388607.
                                    for (int channel = 0; channel < audio_channels_number; channel++)
                                    {
                                        long val_long = sample_in[channel];

                                        if (val_long > 0)
                                        {
                                            val_long = (val_long * 8388607) / int.MaxValue;
                                        }
                                        else
                                        {
                                            val_long = (val_long * -8388608) / int.MinValue;
                                        }

                                        byte byte_1 = (byte)(val_long & 0xFF);
                                        byte byte_2 = (byte)((val_long & 0xFF00) >> 8);
                                        byte byte_3 = (byte)((val_long & 0xFF0000) >> 16);

                                        sample_out[channel] = (byte_3 << 16) | (byte_2 << 8) | byte_1;
                                    }
                                    break;
                                }
                            case 32:
                                {
                                    // Target is as same as the source, do nothing, just copy channels
                                    for (int i = 0; i < audio_channels_number; i++)
                                    {
                                        sample_out[i] = sample_in[i];
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
        private static void ClockPlayer()
        {
            // Here is clock on cps rate (i.e. 50 cps means 50 clocks in one second, 1000/50 = 20 milliseconds on each)
            audio_bp_ratio1_timer++;

            // Clocks on the source playback frequency, i.e. 44100 Hz for audio cd quality
            // Since this method is clocked each 20 milliseconds, then we need 44100 / 50 = 882 samples each clock, 882 / 20 = 44,1 samples needed each millisecond, or simply 44100 / 1000 = 44,1 
            if (audio_bp_ratio1_timer >= audio_bp_ratio1)
            {
                // Clocks on each millisecond, this engine based on the idea that it collects the amount of samples needed in specific time accurately
                // For a media with 44100 Hz frequency, it needs to collect 44,1 sample each millisecond. That means, this clock happen 20 times each cps clock, collects 44,1 samples on each.
                audio_bp_ratio1_timer -= audio_bp_ratio1;

                // Get data from the source
                media_format.GetNextSample(ref audio_t, out audio_sample_obtained);

                // Apply wave fix if enabled (directly from the source)
                ApplyWaveFixAndVolume(ref audio_t, out audio_x);

                if (audio_bp_bits_per_sample_converting_needed)
                {
                    ConvertBits(ref audio_x, out audio_y);
                }
                else
                {
                    // Copy into y
                    for (int i = 0; i < audio_channels_number; i++)
                    {
                        audio_y[i] = audio_x[i];
                    }
                }

                // Update the last source sample
                for (int i = 0; i < audio_channels_number; i++)
                {
                    audio_last_source_sample[i] = audio_y[i];
                }

                audio_bytes_processed_from_source += audio_src_bytes_per_sample;

                if (!audio_sample_obtained)
                {
                    // Raise event media end reached
                    request_end_of_source_event_raise = true;

                    if (audio_record_mode)
                    {
                        audio_recorder.Stop();
                        audio_record_mode = false;
                    }

                    Stop();
                    return;
                }

                audio_bp_samples_got_timer++;

                // Signal the clocker that we got clock job done
                if (audio_bp_samples_got_timer >= audio_bp_samples_needed)
                {
                    audio_bp_samples_got_timer = 0;
                    target_clock_finished = true;
                }

                switch (audio_sampling_mode)
                {
                    case SamplingMode.None:
                        {
                            if (!audio_record_mode)
                            {
                                // No updsampleing/downsampling. Just put data into the playback buffer
                                // Write into buffer
                                if (audio_w_pos < audio_samples_count)
                                {
                                    for (int i = 0; i < audio_channels_number; i++)
                                    {
                                        audio_samples[audio_w_pos][i] = audio_y[i];
                                        // Update the latest target sample

                                        if (audio_use_db_fix)
                                        {
                                            for (int j = 0; j < audio_channels_number; j++)
                                            {
                                                if (audio_samples[audio_w_pos][j] == 0)
                                                    audio_samples[audio_w_pos][j] = 1;
                                            }
                                        }

                                        // audio_last_target_sample_av[i] += (int)(audio_x[i] * volume);

                                        if (audio_samples[audio_w_pos][i] > audio_last_target_sample[i])
                                            audio_last_target_sample[i] = audio_samples[audio_w_pos][i];
                                        else if (audio_target_bit_per_sample > 8)
                                        {
                                            if (audio_last_target_sample[i] > 0)
                                                audio_last_target_sample[i]--;
                                            else if (audio_last_target_sample[i] < 0)
                                                audio_last_target_sample[i]++;
                                        }
                                    }

                                    audio_w_pos++;
                                    audio_samples_added++;
                                    audio_bytes_processed_for_target += audio_trg_bytes_per_sample;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < audio_channels_number; i++)
                                {
                                    audio_last_target_sample[i] = audio_y[i];
                                }
                                audio_recorder.AddSample(audio_last_target_sample[0], audio_last_target_sample[1 % audio_channels_number]);
                            }

                            break;
                        }
                    case SamplingMode.Downsampling:
                        {
                            // In downsampling, it is wrong to take averages of samples, that destroys the originalaty of the song/track
                            // by destroying or changing the result samples. The only way to take the sample that outputed from the source at the
                            // the point when it is needed to be put in the target buffer. If the source clocked more than once before this point,
                            // take the last sample outputed.
                            // This is a sample-skipping downsampling method, but it is the correct accurate way. Since the target frequency is lower
                            // Than the source frequency, then the result will be a playback with lower quality but correct.

                            // Results also some *HISSING* effect, to fix this problem, prober filters needs to be added

                            // Clocks on the ratio of source frequency / target frequency
                            audio_bp_ratio2_timer++;

                            if (audio_bp_ratio2_timer >= audio_bp_ratio2)
                            {
                                audio_bp_ratio2_timer -= audio_bp_ratio2;

                                // Put it into the buffer after doing downsampling, which is basically take average of played samples so far
                                // Put data into the buffer

                                if (!audio_record_mode)
                                {
                                    // Write into buffer
                                    if (audio_w_pos < audio_samples_count)
                                    {
                                        for (int i = 0; i < audio_channels_number; i++)
                                        {
                                            audio_samples[audio_w_pos][i] = audio_y[i];
                                            // audio_last_target_sample_av[i] += (int)(audio_x[i] * volume);

                                            if (audio_use_db_fix)
                                            {
                                                for (int j = 0; j < audio_channels_number; j++)
                                                {
                                                    if (audio_samples[audio_w_pos][j] == 0)
                                                        audio_samples[audio_w_pos][j] = 1;
                                                }
                                            }

                                            if (audio_samples[audio_w_pos][i] > audio_last_target_sample[i])
                                                audio_last_target_sample[i] = audio_samples[audio_w_pos][i];
                                            else if (audio_target_bit_per_sample > 8)
                                            {
                                                if (audio_last_target_sample[i] > 0)
                                                    audio_last_target_sample[i]--;
                                                else if (audio_last_target_sample[i] < 0)
                                                    audio_last_target_sample[i]++;
                                            }
                                        }

                                        audio_w_pos++;
                                        audio_samples_added++;
                                        audio_bytes_processed_for_target += audio_trg_bytes_per_sample;
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < audio_channels_number; i++)
                                    {
                                        audio_last_target_sample[i] = audio_y[i];
                                    }
                                    audio_recorder.AddSample(audio_last_target_sample[0], audio_last_target_sample[1 % audio_channels_number]);
                                }
                            }

                            break;
                        }
                    case SamplingMode.Upsampling:
                        {

                            while (audio_bp_ratio2_timer < 1)
                            {
                                // Clocks on the ratio of target frequency / source frequency
                                audio_bp_ratio2_timer += audio_bp_ratio2;

                                if (!audio_record_mode)
                                {
                                    // Write into buffer
                                    if (audio_w_pos < audio_samples_count)
                                    {
                                        for (int i = 0; i < audio_channels_number; i++)
                                        {
                                            audio_samples[audio_w_pos][i] = audio_y[i];
                                            // audio_last_target_sample_av[i] += (int)(audio_x[i] * volume);

                                            if (audio_use_db_fix)
                                            {
                                                for (int j = 0; j < audio_channels_number; j++)
                                                {
                                                    if (audio_samples[audio_w_pos][j] == 0)
                                                        audio_samples[audio_w_pos][j] = 1;
                                                }
                                            }

                                            if (audio_samples[audio_w_pos][i] > audio_last_target_sample[i])
                                                audio_last_target_sample[i] = audio_samples[audio_w_pos][i];
                                            else if (audio_target_bit_per_sample > 8)
                                            {
                                                if (audio_last_target_sample[i] > 0)
                                                    audio_last_target_sample[i]--;
                                                else if (audio_last_target_sample[i] < 0)
                                                    audio_last_target_sample[i]++;
                                            }
                                        }

                                        audio_w_pos++;
                                        audio_samples_added++;
                                        audio_bytes_processed_for_target += audio_trg_bytes_per_sample;

                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < audio_channels_number; i++)
                                    {
                                        audio_last_target_sample[i] = audio_y[i];
                                    }
                                    audio_recorder.AddSample(audio_last_target_sample[0], audio_last_target_sample[1 % audio_channels_number]);
                                }
                            }

                            audio_bp_ratio2_timer -= 1;
                            break;
                        }
                }
            }
        }

        public static void Pause()
        {
            PAUSED = true;
        }
        public static void Play()
        {
            PAUSED = false;
        }
        public static void Stop()
        {
            PAUSED = true;
            media_format.Position = 0;
        }
        public static void SetVolume(double vol)
        {
            volume = vol / 100;
        }
        public static void GetVolume(out double vol)
        {
            vol = volume * 100;
        }
    }
}
