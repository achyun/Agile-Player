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

// This file uses MP3Sharp
// Decode MP3 files to PCM bitstreams entirely in .NET managed code
// MP3Sharp is licensed under the [LGPL Version 3](https://github.com/ZaneDubya/MP3Sharp/blob/master/license.txt),
// or see (Formats\MP3Sharp\LICENSE (LGPL Version 3).txt).
//
// /***************************************************************************
//  * Copyright (c) 2015, 2021 The Authors.
//  * 
//  * All rights reserved. This program and the accompanying materials
//  * are made available under the terms of the GNU Lesser General Public License
//  * (LGPL) version 3 which accompanies this distribution, and is available at
//  * https://www.gnu.org/licenses/lgpl-3.0.en.html
//  *
//  * This library is distributed in the hope that it will be useful,
//  * but WITHOUT ANY WARRANTY; without even the implied warranty of
//  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  * Lesser General Public License for more details.
//  *
//  ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using APlayer.Core;
using MP3Sharp;

namespace APlayer.Formats
{
    [MediaFormatInfo("MP3", "mp3", new string[] { ".mp3", ".MP3" }, MediaFormatType.Audio)]
    internal class FormatMP3 : IMediaFormat
    {
        MemoryStream read_stream;
        private long sample_pointer;

        public int BlockAlign { get; private set; }

        public override bool CheckFile(string filePath)
        {
            Trace.WriteLine("Checking file at " + filePath, "MP3");
            bool yes = false;
            try
            {
                MP3Stream st = new MP3Stream(filePath);

                if (!st.CanRead)
                {
                    Trace.WriteLine("ERROR: file cannot be read at " + filePath, "MP3");
                    st.Dispose();
                    st.Close();
                    return false;
                }

                yes = st.Length > 0;

                st.Dispose();
                st.Close();
            }
            catch (Exception ex)
            {
                Trace.TraceError("Cannot read mp3 file !!: " + ex.Message, "MP3");
            }

            return yes;
        }

        public override void Dispose()
        {
            FileLoaded = false;
            CurrentFilePath = "";
            if (read_stream != null)
            {
                read_stream.Dispose();
                read_stream.Close();
            }
        }
        private void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while (true)
            {
                try
                {
                    len = input.Read(buffer, 0, 2000);
                    if (len == 0)
                        break;
                    if (((MP3Stream)input).Frequency > Frequency)
                        Frequency = ((MP3Stream)input).Frequency;

                    if (((MP3Stream)input).ChannelCount > ChannelsNumber)
                        ChannelsNumber = ((MP3Stream)input).ChannelCount;

                    output.Write(buffer, 0, len);
                }
                catch
                {
                    break;
                }
            }
            output.Flush();
        }
        public override void LoadFile(string filePath)
        {
            Trace.WriteLine("Loading file at " + filePath, "MP3");

            if (!File.Exists(filePath))
            {
                Trace.WriteLine("ERROR: file does not exist at " + filePath, "MP3");
                return;
            }

            MP3Stream stream = new MP3Stream(filePath);

            if (!stream.CanRead)
            {
                Trace.WriteLine("ERROR: file cannot be read at " + filePath, "MP3");
                Dispose();
                return;
            }
            if (stream.Length == 0)
            {
                Trace.WriteLine("ERROR: cannot read mp3 file, the length is 0 !! " + filePath, "WAV");
                Dispose();
                return;
            }

            read_stream = new MemoryStream();

            ChannelsNumber = stream.ChannelCount;
            BitsPerSample = 16;
            Frequency = stream.Frequency;

            // Copy the stream
            CopyStream(stream, read_stream);

            BlockAlign = ChannelsNumber * (BitsPerSample / 8);
            Length = read_stream.Length / (BlockAlign * Frequency);

            CurrentFilePath = filePath;
            FileLoaded = true;
            sample_pointer = 0;
            stream.Close();
        }

        public override double GetPosition()
        {
            return GetTimeAtSamplePoint(sample_pointer);
        }
        public override void SetPosition(double seconds)
        {
            sample_pointer = GetSamplePoint(seconds);
            read_stream.Seek(sample_pointer, SeekOrigin.Begin);
        }
        public override void GetNextSample(ref int[] sample, out bool success)
        {
            byte[] sampleBlock = new byte[BlockAlign];
            if (sample_pointer + (BlockAlign - 1) < read_stream.Length)
            {
                read_stream.Read(sampleBlock, 0, BlockAlign);
                sample_pointer += BlockAlign;
                success = true;
            }
            else
            {
                success = false;
            }
            switch (ChannelsNumber)
            {
                case 1:// Mono !!
                    {
                        switch (BitsPerSample)
                        {
                            case 8:
                                {
                                    sample[0] = sampleBlock[0];
                                    break;
                                }
                            case 16:
                                {
                                    sample[0] = IntFromBytes(new byte[] { sampleBlock[0], sampleBlock[1] }, false);
                                    break;
                                }
                            case 24:
                                {
                                    sample[0] = IntFromBytes(new byte[] { sampleBlock[0], sampleBlock[1], sampleBlock[2] }, false);
                                    break;
                                }
                            case 32:
                                {
                                    sample[0] = IntFromBytes(new byte[] { sampleBlock[0], sampleBlock[1], sampleBlock[2], sampleBlock[3] }, false);
                                    break;
                                }
                        }
                        break;
                    }
                case 2:// Stereo
                    {
                        switch (BitsPerSample)
                        {
                            case 8:
                                {
                                    sample[0] = sampleBlock[0];// Left
                                    sample[1] = sampleBlock[1];// Right
                                    break;
                                }
                            case 16:
                                {
                                    sample[0] = IntFromBytes(new byte[] { sampleBlock[0], sampleBlock[1] }, false);
                                    sample[1] = IntFromBytes(new byte[] { sampleBlock[2], sampleBlock[3] }, false);
                                    break;
                                }
                            case 24:
                                {
                                    sample[0] = IntFromBytes(new byte[] { sampleBlock[0], sampleBlock[1], sampleBlock[2] }, false);
                                    sample[1] = IntFromBytes(new byte[] { sampleBlock[3], sampleBlock[4], sampleBlock[5] }, false);
                                    break;
                                }
                            case 32:
                                {
                                    sample[0] = IntFromBytes(new byte[] { sampleBlock[0], sampleBlock[1], sampleBlock[2], sampleBlock[3] }, false);
                                    sample[1] = IntFromBytes(new byte[] { sampleBlock[4], sampleBlock[5], sampleBlock[6], sampleBlock[7] }, false);
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Get sample point at specific time
        /// </summary>
        /// <param name="seconds">The time to get the sample point at</param>
        /// <returns>Sample point at specific time, including data pointer</returns>
        long GetSamplePoint(double seconds)
        {
            // Convert input seconds to milliseconds
            seconds *= 1000;
            // Calculate how many sample blocks we have, blocks of BlockAlign (4 bytes for 16 bits stereo for example)
            long sample_blocks = read_stream.Length / BlockAlign;
            // Calculate the sample within the blocks 
            long sample_target = (long)((seconds * sample_blocks) / (Length * 1000));
            // Return into bytes (each block is BlockAlign bytes)
            sample_target *= BlockAlign;

            // Finally the point within the file
            return sample_target;
        }
        /// <summary>
        /// Get time at specific point. 
        /// </summary>
        /// <param name="sample_point">The sample point in the data, without DataPointer</param>
        /// <returns></returns>
        double GetTimeAtSamplePoint(long sample_point)
        {
            double sec = (sample_point * (Length * 1000)) / read_stream.Length;

            return (sec / 1000);
        }
    }
}
