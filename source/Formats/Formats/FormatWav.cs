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
using System.IO;
using System.Text;
using System.Diagnostics;
using APlayer.Core;

namespace APlayer.Formats
{
    [MediaFormatInfo("WAVE", "wav", new string[] { ".wav", ".WAV" }, MediaFormatType.Audio)]
    public class FormatWav : IMediaFormat
    {
        private Stream read_stream;
        protected long sample_pointer;

        /// <summary>
        /// GEt audio format. 1 is PCM
        /// </summary>
        public int AudioFormat { get; private set; }
        public int BlockAlign { get; private set; }
        public int DataSize { get; private set; }
        public long DataPointer { get; private set; }

        public override bool CheckFile(string filePath)
        {
            Trace.WriteLine("Checking file at " + filePath, "WAV");
            if (!File.Exists(filePath))
            {
                Trace.WriteLine("ERROR: file does not exist at " + filePath, "WAV");
                return false;
            }
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            if (!stream.CanRead)
            {
                Trace.WriteLine("ERROR: file cannot be read at " + filePath, "WAV");
                stream.Dispose();
                stream.Close();
                return false;
            }
            if (stream.Length < 50)
            {
                Trace.WriteLine("ERROR: cannot read wave file, file is too small to be an wave file." + filePath, "WAV");
                stream.Dispose();
                stream.Close();
                return false;
            }
            // Start reading !!
            // 1 Read RIFF
            byte[] readData = new byte[4];
            stream.Read(readData, 0, 4);
            if (ASCIIEncoding.ASCII.GetString(readData) != "RIFF")
            {
                Trace.WriteLine("ERROR: cannot read wave file, RIFF check failed.", "WAV");
                stream.Dispose();
                stream.Close();
                return false;
            }
            // 2 Read ChunkSize
            readData = new byte[4];
            stream.Read(readData, 0, 4);
            int ChunkSize = IntFromBytes(readData, true);

            // 3 Read Format
            readData = new byte[4];
            stream.Read(readData, 0, 4);
            if (ASCIIEncoding.ASCII.GetString(readData) != "WAVE")
            {
                Trace.WriteLine("ERROR: cannot read wave file, WAVE check failed.", "WAV");
                stream.Dispose();
                stream.Close();
                return false;
            }

            // Reached here means this file could be a wav file !!
            stream.Dispose();
            stream.Close();
            Trace.WriteLine("Check file success, this file is WAV: " + filePath, "WAV");
            return true;
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
        public override void LoadFile(string filePath)
        {
            // Dispose everything before loading
            Dispose();
            Trace.WriteLine("Loading file at " + filePath, "WAV");
            if (!File.Exists(filePath))
            {
                Trace.WriteLine("ERROR: file does not exist at " + filePath, "WAV");
                return;
            }

            read_stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            if (!read_stream.CanRead)
            {
                Trace.WriteLine("ERROR: file cannot be read at " + filePath, "WAV");
                Dispose();
                return;
            }
            if (read_stream.Length < 50)
            {
                Trace.WriteLine("ERROR: cannot read wave file, file is too small to be an wave file. "+ filePath, "WAV");
                Dispose();
                return;
            }
            // Start reading !!
            // 1 Read RIFF
            byte[] readData = new byte[4];
            read_stream.Read(readData, 0, 4);
            if (ASCIIEncoding.ASCII.GetString(readData) != "RIFF")
            {
                Trace.WriteLine("ERROR: cannot read wave file, RIFF check failed.", "WAV");
                Dispose();
                return;
            }
            // 2 Read ChunkSize
            readData = new byte[4];
            read_stream.Read(readData, 0, 4);
            int ChunkSize = IntFromBytes(readData, true);

            // 3 Read Format
            readData = new byte[4];
            read_stream.Read(readData, 0, 4);
            if (ASCIIEncoding.ASCII.GetString(readData) != "WAVE")
            {
                Trace.WriteLine("ERROR: cannot read wave file, WAVE check failed.", "WAV");
                Dispose();
                return;
            }

            bool found = false;
            // Read the rest of the chuncks.
            while (read_stream.Position < read_stream.Length)
            {
                // Read SubchunkID
                readData = new byte[4];
                read_stream.Read(readData, 0, 4);
                string id = ASCIIEncoding.ASCII.GetString(readData);
                if (id == "fmt ")
                {
                    // This is it, the fmt chunk
                    // Read Subchunk1Size
                    readData = new byte[4];
                    read_stream.Read(readData, 0, 4);
                    int Subchunk1Size = IntFromBytes(readData, true);

                    // Read AudioFormat
                    readData = new byte[2];
                    read_stream.Read(readData, 0, 2);
                    AudioFormat = IntFromBytes(readData, true);
                    Subchunk1Size -= 2;
                    // Read NumChannels
                    readData = new byte[2];
                    read_stream.Read(readData, 0, 2);
                    ChannelsNumber = IntFromBytes(readData, true);
                    Subchunk1Size -= 2;
                    // Read SampleRate
                    readData = new byte[4];
                    read_stream.Read(readData, 0, 4);
                    Frequency = IntFromBytes(readData, true);
                    Subchunk1Size -= 4;
                    // Read ByteRate
                    readData = new byte[4];
                    read_stream.Read(readData, 0, 4);
                    DataRate = IntFromBytes(readData, true);
                    Subchunk1Size -= 4;
                    // Read BlockAlign
                    readData = new byte[2];
                    read_stream.Read(readData, 0, 2);
                    BlockAlign = IntFromBytes(readData, true);
                    Subchunk1Size -= 2;
                    // Read BitsPerSample
                    readData = new byte[2];
                    read_stream.Read(readData, 0, 2);
                    BitsPerSample = IntFromBytes(readData, true);
                    Subchunk1Size -= 2;

                    // Read dummy
                    readData = new byte[Subchunk1Size];
                    read_stream.Read(readData, 0, Subchunk1Size);
                }
                else if (id == "data")
                {
                    // 13 Read Subchunk2Size
                    readData = new byte[4];
                    read_stream.Read(readData, 0, 4);
                    DataSize = IntFromBytes(readData, true);
                    DataPointer = read_stream.Position;
                    CurrentFilePath = filePath;
                    FileLoaded = true;
                    Length = (double)DataSize / (double)DataRate;
                    found = true;

                    SetPosition(0);

                    break;
                }
                else // Unkown
                {
                    // Read and skip this chunck
                    readData = new byte[4];
                    read_stream.Read(readData, 0, 4);
                    int size = IntFromBytes(readData, true);

                    read_stream.Seek(read_stream.Position + size, SeekOrigin.Begin);
                }
            }

            if (!found)
            {
                Console.WriteLine("ERROR: cannot read wav media file, there is no data !!", "WAV");
                Dispose();
            }
        }

        public override double GetPosition()
        {
            return GetTimeAtSamplePoint(sample_pointer - DataPointer);
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
            seconds *= 1000;
            long index = (long)((seconds * DataSize) / (Length * 1000));

            // Make sure it never lands on an odd number
            if ((index & 0x1) != 0)
                index--;
            if (index < 0)
                index = 0;
            // Finally the point within the file

            return DataPointer + index;
        }
        /// <summary>
        /// Get time at specific point. 
        /// </summary>
        /// <param name="sample_point">The sample point in the data, without DataPointer</param>
        /// <returns></returns>
        double GetTimeAtSamplePoint(long sample_point)
        {
            double sec = (sample_point * (Length * 1000)) / DataSize;

            return (sec / 1000);
        }
    }
}
