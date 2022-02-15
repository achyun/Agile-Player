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
    public abstract class IMediaFormat
    {
        public IMediaFormat()
        {
            LoadAttrs();
        }
        public string Name { get; private set; }
        public string ID { get; private set; }
        public string[] Extensions { get; private set; }
        public MediaFormatType FormatType { get; private set; }
        /// <summary>
        /// Get if a file is loaded and ready to use. This is set after LoadFile(string mediaFile) method is called and load is a success.
        /// </summary>
        public bool FileLoaded { get; protected set; }
        /// <summary>
        /// Get the current file path. This is set after LoadFile(string mediaFile) method is called and load is a success.
        /// </summary>
        public string CurrentFilePath { get; protected set; }
        /// <summary>
        /// Get the channels number. 1 is Mono, 2 is Stereo ...etc
        /// </summary>
        public int ChannelsNumber { get; protected set; }
        /// <summary>
        /// Get the frequency (or sample rate)
        /// </summary>
        public int Frequency { get; protected set; }
        /// <summary>
        /// Get data/byte rate (bytes/seconds)
        /// </summary>
        public int DataRate { get; protected set; }
        /// <summary>
        /// Get bits per sample
        /// </summary>
        public int BitsPerSample { get; protected set; }
        /// <summary>
        /// Get media length in seconds. 
        /// </summary>
        public double Length { get; protected set; }
        /// <summary>
        /// Get or set media position (or time). 
        /// </summary>
        public double Position { get { return GetPosition(); } set { SetPosition(value); } }

        /// <summary>
        /// Load attributes for this media format. Called at the constructor when this format is found.
        /// </summary>
        protected virtual void LoadAttrs()
        {
            //this.Supported = true;
            //this.NotImplementedWell = false;
            foreach (Attribute attr in Attribute.GetCustomAttributes(this.GetType()))
            {
                if (attr.GetType() == typeof(MediaFormatInfoAttribute))
                {
                    MediaFormatInfoAttribute inf = (MediaFormatInfoAttribute)attr;
                    this.Name = inf.Name;
                    this.ID = inf.ID;
                    this.Extensions = inf.Extensions;
                    this.FormatType = inf.FormatType;
                }
            }
        }
        /// <summary>
        /// Check if a file is for this media format. It is recommended that the file extension matches one of this format's.
        /// </summary>
        /// <param name="filePath">The complete file path</param>
        /// <returns>True if the file matches this format, otherwise false.</returns>
        public abstract bool CheckFile(string filePath);
        /// <summary>
        /// Load media file at specific path. It is recommended to use CheckFile(filePath) first to check if the specific file matches this format. When done, 
        /// FileLoaded and CurrentFilePath will be set if the load is a success, otherwise FileLoaded will be set to false and CurrentFilePath will be set to empty.
        /// </summary>
        /// <param name="filePath">The media file path to load.</param>
        public abstract void LoadFile(string filePath);
        /// <summary>
        /// Destroy the format components if loaded. FileLoaded will be set to false and CurrentFilePath will be set to empty.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Set media position into time
        /// </summary>
        /// <param name="seconds">The time to seek into, it must be larger than 0 and smaller that length.</param>
        public abstract void SetPosition(double seconds);
        /// <summary>
        /// Get current media time.
        /// </summary>
        /// <returns></returns>
        public abstract double GetPosition();
        /// <summary>
        /// Get the next sample
        /// </summary>
        /// <param name="sample">Will be fill with sample, each entry is a channel. i.e. : Left, Right, RearLeft, RearRight... etc</param>
        /// <param name="success">True if the sample obtained successfully, false means it cannot obtain the sample, because of it reaches the end of file for example</param>
        public abstract void GetNextSample(ref int[] sample, out bool success);

        /*HELPER METHODS*/
        protected virtual int IntFromBytes(byte[] data, bool remove_signing)
        {
            int val = 0;
            if (data.Length == 1)
                val = data[0];
            else if (data.Length == 2)
                val = (short)((data[0] << 0) | (data[1] << 8));
            else if (data.Length == 3)
            {
                val = (data[0] << 0) | (data[1] << 8) | (data[2] << 16);

                if (remove_signing)
                {
                    if ((val & 0x800000) != 0)
                        val |= ~0xffffff;
                }
            }
            else if (data.Length == 4)
                val = (data[0] << 0) | (data[1] << 8) | (data[2] << 16) | (data[3] << 24);
            return val;
        }
        protected virtual byte[] BytesFromInt16(short val)
        {
            byte[] data = new byte[2];
            data[0] = (byte)(val & 0xFF);
            data[1] = (byte)((val & 0xFF00) >> 8);
            return data;
        }
        protected virtual byte[] BytesFromInt32(int val)
        {
            byte[] data = new byte[4];
            data[0] = (byte)(val & 0xFF);
            data[1] = (byte)((val & 0xFF00) >> 8);
            data[2] = (byte)((val & 0xFF0000) >> 16);
            data[3] = (byte)((val & 0xFF000000) >> 24);

            return data;
        }
    }
}
