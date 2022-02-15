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
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using APlayer.Core;

namespace APlayer
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();

            // Set version info
            string ver_m = Assembly.LoadFile(Path.Combine(APMain.ApplicationFolder, "AgilePlayer.exe")).GetName().Version.ToString();
            label_version.Text = "Version " + ver_m;
        }
        // Donate
        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/donate?hosted_button_id=KV25VFRMVKLM2");
        }
        // Close
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:alaahadidfreeware@gmail.com");
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.gnu.org/licenses/translations.en.html");
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Path.Combine(APMain.ApplicationFolder, "GNU GENERAL PUBLIC LICENSE 3.0.html"));
            }
            catch { }
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(Path.Combine(APMain.ApplicationFolder, "Copyright Notice.txt"));
            }
            catch { }
        }
    }
}
